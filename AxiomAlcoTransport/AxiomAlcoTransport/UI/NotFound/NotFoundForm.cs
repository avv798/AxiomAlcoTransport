using System;
using Microsoft.Win32;
using Axiom.AlcoTransport.Watchtower.Configuration;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Информационная форма.
    /// </summary>
    public partial class NotFoundForm : DevExpress.XtraEditors.XtraForm
    {
        #region Инициализация формы и её закрытие.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public NotFoundForm()
        {
            Program.Logger.Info(this, "Попытка инициализировать форму...");

            InitializeComponent();

            Program.Logger.Info(this, "... форма успешно инициализирована.");
        }
        /// <summary>
        /// Конструктор класса "с параметрами".
        /// </summary>
        /// <param name="message">Сообщение.</param>
        public NotFoundForm(string message) : this()
        {
            Program.Logger.Info(this, "Попытка инициализировать форму с параметрами...");

            Error_labelControl.Text = message;

            Program.Logger.Info(this, "... форма успешно инициализирована с параметрами.");
        }
        private void Close_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время закрытия формы произошла ошибка", exception);
            }
        }
        private void ConfigurationNotFoundForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузить форму 'ConfigurationNotFoundForm' ...");

                loadVisualSettings();

                Program.Logger.Info(this, "... форма 'ConfigurationNotFoundForm' успешно загружена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время загрузки формы произошла ошибка", exception);
            }
        }
        private void ConfigurationNotFoundForm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Форма закрывается.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время закрытия формы произошла ошибка", exception);
            }
        }
        #endregion Инициализация формы и её закрытие.

        #region Внутренние методы класса.
        /// <summary>
        /// Загрузка оформления.
        /// </summary>
        private void loadVisualSettings()
        {
            try
            {
                string skinName = Constants.DefaultVisualStyle;

                try
                {
                    RegistryKey registry = Registry.CurrentUser.OpenSubKey(Constants.RegistryPathVisualSettings);
                    if (registry == null) throw new Exception(string.Format("Не найден путь в реестре '{0}'.", Constants.RegistryPathVisualSettings));

                    skinName = registry.GetValue("ActiveSkinName").ToString();

                    registry.Close();
                }
                catch (Exception exception)
                {
                    Program.Logger.Error("Во время поиска конфигурации в реестре произошла ошибка.", exception);
                }

                NotFound_defaultLookAndFeel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
                NotFound_defaultLookAndFeel.LookAndFeel.SetSkinStyle(skinName);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время загрузки оформления произошла ошибка.", exception);
            }
        }
        #endregion Внутренние методы класса.
    }
}