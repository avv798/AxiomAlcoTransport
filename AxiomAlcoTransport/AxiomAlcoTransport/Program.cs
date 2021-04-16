using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Configuration;
using System.Globalization;
using DevExpress.LookAndFeel;
using DevExpress.XtraEditors;
using DevExpress.XtraSplashScreen;
using AxiomAuxiliary.Loggers;
using Axiom.AlcoTransport.Watchtower;
using Axiom.AlcoTransport.Watchtower.Crypto;
using Axiom.AlcoTransport.Watchtower.Configuration;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Главный класс приложения.
    /// </summary>
    static class Program
    {
        #region Внешние статические объекты главного окна.
        /// <summary>
        /// Функционал логгирования.
        /// Статический объект.
        /// Только чтение.
        /// </summary>
        public static readonly ILogger Logger = new Logger();
        /// <summary>
        /// Переводчик.
        /// </summary>
        public static readonly Language Language = new Language();
        #endregion Внешние статические объекты главного окна.

        #region Внешние статические методы класса.
        /// <summary>
        /// Получить параметр приложения.
        /// </summary>
        /// <param name="nameSetting">Наименование параметра.</param>
        /// <returns>Значение параметра.</returns>
        public static string GetAppSetting(string nameSetting)
        {
            string value = ConfigurationManager.AppSettings[nameSetting];

            if (string.IsNullOrEmpty(value)) throw new Exception(string.Format("Параметр с наименованием '{0}' не найден в файле конфигурации приложения.", nameSetting));

            return value;
        }
        /// <summary>
        /// Получить параметр.
        /// </summary>
        /// <param name="name">Наименование параметра.</param>
        /// <param name="defaultValue">Значение "по умолчанию".</param>
        /// <returns>Значение параметра.</returns>
        public static bool GetBooleanParameter(string name, bool defaultValue = true)
        {
            try
            {
                return (GetAppSetting(name).ToLower() == "true");
            }
            catch (Exception exception)
            {
                Logger.Error(string.Format("Ошибка загрузки параметра '{0}'.", name), exception);

                return defaultValue;
            }
        }
        #endregion Внешние статические методы класса.

        #region Метод 'Main'.
        /// <summary>
        /// Метод "main". Точка входа.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Logger.Info("Начало работы приложения.");

                #region Установка языковой культуры приложения.

                Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru");

                #endregion Установка языковой культуры приложения.

                #region Загрузка дополнительных стилей оформления.

                DevExpress.UserSkins.BonusSkins.Register();
                DevExpress.Skins.SkinManager.EnableFormSkins();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                #endregion Загрузка дополнительных стилей оформления.

                #region Проверка конфигурации и лицензии; загрузка главного окна.

                if (configurationAlreadyExists())
                {
                    #region Блокировка запуска второй копии приложения.
                    if (instanceAlreadyExists()) Process.GetCurrentProcess().Kill();
                    #endregion Блокировка запуска второй копии приложения.

                    UserLookAndFeel.Default.SetSkinStyle(Constants.DefaultVisualStyle);
                    SplashScreenManager.RegisterUserSkins(typeof(DevExpress.UserSkins.BonusSkins).Assembly);

                    if (ValidateLicence(true))
                    {
                        MainForm mainForm = new MainForm
                        {
                            Size = new Size((Screen.PrimaryScreen.WorkingArea.Width < 1280) ? 800 : 1280,
                                            (Screen.PrimaryScreen.WorkingArea.Height < 768) ? 600 : 768)
                        };

                        Application.Run(mainForm);
                    }
                    else
                    {
                        Application.Run(new NotFoundForm("Обнаружено нарушение лицензионного соглашения. Обратитесь в службу технической поддержки."));
                    }
                }
                else
                {
                    Application.Run(new NotFoundForm());
                }

                #endregion Проверка конфигурации и лицензии; загрузка главного окна.

                Logger.Info("Завершение работы приложения.");
            }
            catch (Exception exception)
            {
                Logger.Fatal("Объект 'Program'. Фатальное исключение. Работа приложения будет завершена.", exception);

                MainForm.ShowErrorMessage(string.Format("Фатальная ошибка в приложении.\r\n{0}\r\n\r\nРабота приложения будет завершена.", exception.Message));
            }
        }
        #endregion Метод 'Main'.

        #region Блокировка запуска второй копии приложения.
        /// <summary>
        /// Мьютекс для блокировки запуска второй копии приложения.
        /// </summary>
        // ReSharper disable once NotAccessedField.Local
        private static Mutex oneInstanceMutex;
        /// <summary>
        /// Проверка наличия мьютекса в системе.
        /// </summary>
        /// <returns>Признак наличия мьютекса.</returns>
        private static bool instanceAlreadyExists()
        {
            bool createdNew;

            ConfigurationData configuration = ConfigurationHelper.LoadConfiguration(GetAppSetting("configurationFilename"));
            string name = string.Format("Axiom.AlcoTransport.{0}.InVinoVeritas", configuration.FsrarId);

            oneInstanceMutex = new Mutex(false, name, out createdNew);

            return (!createdNew);
        }
        #endregion Блокировка запуска второй копии приложения.

        #region Проверка наличия записанной конфигурации.
        /// <summary>
        /// Проверка наличия записанной конфигурации.
        /// </summary>
        /// <returns>Признак наличия записанной конфигурации.</returns>
        private static bool configurationAlreadyExists()
        {
            return ConfigurationHelper.ConfigurationAlreadyExists(GetAppSetting("configurationFilename"));
        }
        #endregion Проверка наличия записанной конфигурации.

        #region Лицензия.
        /// <summary>
        /// Проверить валидность лицензии.
        /// </summary>
        /// <param name="showMessage">Показывать или нет предупреждение об истечении срока лицензии.</param>
        /// <returns>Результат проверки.</returns>
        public static bool ValidateLicence(bool showMessage = false)
        {
            try
            {
                string[] licence = File.ReadAllLines(GetAppSetting("licenceFilename"));

                #region Синтаксический анализ...

                string hash = Md5Helper.GetHash(Constants.Motto + licence[8] + licence[9]);
                if (hash != licence[10]) throw new Exception("Хэш-код не соответствует валидному.");

                ConfigurationData configuration = ConfigurationHelper.LoadConfiguration(GetAppSetting("configurationFilename"));
                if (AesCrypto.Decrypt(licence[8]) != configuration.FsrarId) throw new Exception("Идентификатор ФС РАР не соответствует лицензии.");

                DateTime dt = (DateTime.ParseExact(AesCrypto.Decrypt(licence[9]).Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture)).AddDays(1);

                if (dt < DateTime.Now) throw new Exception("Срок лицензии истёк.");

                #endregion Синтаксический анализ...

                #region Предупреждение...
                if (showMessage)
                {
                    if (dt.Subtract(DateTime.Now).Days < 7)
                    {
                        XtraMessageBox.Show(
                            "До истечения срока лицензионного соглашения осталось менее семи дней.\r\n" +
                            "По истечению срока лицензионного соглашения работа приложения будет невозможна.\r\n\r\n" +
                            "Обратитесь в службу технической поддержки.",
                            "Предупреждение",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                }
                #endregion Предупреждение...

                return true;
            }
            catch (Exception exception)
            {
                Logger.Error("Объект 'Program'. Во время валидации лицензии произошла ошибка.", exception);

                return false;
            }
        }
        #endregion Лицензия.
    }
}
