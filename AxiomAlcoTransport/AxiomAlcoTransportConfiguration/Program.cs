using System;
using System.Threading;
using System.Windows.Forms;
using AxiomAuxiliary.Loggers;

namespace Axiom.AlcoTransport.Configuration
{
    static class Program
    {
        #region Внешние статические объекты главного окна.
        /// <summary>
        /// Функционал логгирования.
        /// Статический объект.
        /// Только чтение.
        /// </summary>
        public static readonly ILogger Logger = new Logger();
        #endregion Внешние статические объекты главного окна.

        /// <summary>
        /// Метод "main". Точка входа.
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region Установка языковой культуры приложения.
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("ru");
            #endregion Установка языковой культуры приложения.

            #region Загрузка дополнительных стилей оформления.
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            #endregion Загрузка дополнительных стилей оформления.

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
