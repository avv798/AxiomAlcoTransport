using System;
using Microsoft.Win32;
using Axiom.AlcoTransport.Watchtower.Configuration;

namespace Axiom.AlcoTransport.Utility
{
	public partial class MainForm
	{
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

                    object skin = registry.GetValue("ActiveSkinName");
                    if (skin != null) skinName = skin.ToString();

                    registry.Close();
                }
                catch (Exception exception)
                {
                    Program.Logger.Error("Во время поиска конфигурации в реестре произошла ошибка.", exception);
                }

                Main_defaultLookAndFeel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
                Main_defaultLookAndFeel.LookAndFeel.SetSkinStyle(skinName);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время загрузки оформления произошла ошибка.", exception);
            }
        }
        /// <summary>
        /// Проверить данные.
        /// </summary>
        private void validateData()
        {
            try
            {
                bool isValid = ((!string.IsNullOrWhiteSpace(Address_textEdit.Text))
                                && (!string.IsNullOrWhiteSpace(Port_textEdit.Text)));

                CheckConnection_simpleButton.Enabled = isValid;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время валидации данных на форме произошла ошибка.", exception);
            }
        }
        #endregion Внутренние методы класса.
	}
}