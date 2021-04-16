using System;
using System.Windows.Forms;
using Microsoft.Win32;
using DevExpress.XtraEditors;
using Axiom.AlcoTransport.Watchtower.Configuration;

namespace Axiom.AlcoTransport.Configuration
{
    public partial class MainForm : XtraForm
    {
        #region Внутренние объекты класса.
        /// <summary>
        /// Признак изменения данных.
        /// </summary>
        private bool isChanged;
        #endregion Внутренние объекты класса.

        #region Инициализация главной формы.
        public MainForm()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                isChanged = false;

                Program.Logger.Info(this, "... инициализация успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации произошла ошибка.", exception);
            }
        }
        #endregion Инициализация главной формы.

        #region Завершение работы главной формы.
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
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
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!confirmCloseForm()) e.Cancel = true;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время закрытия формы произошла ошибка", exception);
            }
        }
        #endregion Завершение работы главной формы.

        #region Внутренние методы класса.
        /// <summary>
        /// Подтверждение закрытия формы.
        /// </summary>
        /// <returns>Признак закрытия.</returns>
        private bool confirmCloseForm()
        {
            if (!isChanged) return true;

            DialogResult result = XtraMessageBox.Show("Сохранить изменения перед завершением работы?"
                                                        , "Завершение работы"
                                                        , MessageBoxButtons.YesNoCancel
                                                        , MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (validateData())
                {
                    saveConfiguration();

                    return true;
                }

                XtraMessageBox.Show("Некоторые конфигурационные данные являются некорректными. Сохранение невозможно.",
                                    "Ошибка сохранения",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);

                return false;
            }

            if (result == DialogResult.No)
            {
                return true;
            }

            return false;
        }
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
        /// <returns>Признак валидности.</returns>
        private bool validateData()
        {
            isChanged = true;

            bool isValid = ((!string.IsNullOrWhiteSpace(FsrarId_textEdit.Text))
                                && (!string.IsNullOrWhiteSpace(Inn_textEdit.Text))
                                && (!string.IsNullOrWhiteSpace(Address_textEdit.Text))
                                && (!string.IsNullOrWhiteSpace(Port_textEdit.Text))
                                && (!string.IsNullOrWhiteSpace(Path_buttonEdit.Text)));

            Save_simpleButton.Enabled = isValid;

            return isValid;
        }
        /// <summary>
        /// Сохранить конфигурацию.
        /// </summary>
        private void saveConfiguration()
        {
            try
            {
                Program.Logger.Info(this, "Попытка сохранить конфигурацию приложения...");

                if (Config_saveFileDialog.ShowDialog(this) != DialogResult.OK)
                {
                    Program.Logger.Info(this, "... сохранение конфигурации отменено по внешней команде.");

                    return;
                }

                ConfigurationData data = new ConfigurationData
                                             {
                                                 FsrarId = FsrarId_textEdit.Text,
                                                 Inn = Inn_textEdit.Text,
                                                 UtmAddress = Address_textEdit.Text,
                                                 UtmPort = Port_textEdit.Text,
                                                 UtmTimeoutShort = (int)TimeoutShort_spinEdit.Value,
                                                 UtmTimeoutLong = (int)TimeoutLong_spinEdit.Value,
                                                 IntervalDataRequest = (int)IntervalRequest_spinEdit.Value,
                                                 PathToLocalDatabase = Path_buttonEdit.Text
                                             };

                ConfigurationHelper.SaveConfiguration(data, Config_saveFileDialog.FileName);

                Program.Logger.Info(this, string.Format("... конфигурация успешно сохранена: '{0}' в файле '{1}'.", data, Config_saveFileDialog.FileName));

                isChanged = false;
                Save_simpleButton.Enabled = false;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время сохранения конфигурации произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Выбрать каталог локальной БД.
        /// </summary>
        private void chooseFolder()
        {
            try
            {
                if (Db_folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    Path_buttonEdit.Text = Db_folderBrowserDialog.SelectedPath;
                }
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выбора каталога для локальной базы данных произошла ошибка.", exception);
            }
        }
        /// <summary>
        /// Обработчик изменения данных.
        /// </summary>
        private void changedData()
        {
            try
            {
                validateData();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        #endregion Внутренние методы класса.

        #region Обработка событий пользовательского интерфейса.
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                Program.Logger.Info(this, "Попытка загрузки... ");

                loadVisualSettings();

                Program.Logger.Info(this, "... загрузка успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время загрузки произошла ошибка.", exception);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void Close_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        private void Save_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                saveConfiguration();

                // XtraMessageBox.Show("Файл конфигурации успешно сохранён.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;

                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);

                XtraMessageBox.Show(string.Format("Во время сохранения конфигурации произошла ошибка:\r\n'{0}'.", exception.Message),
                                    "Ошибка сохранения",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void Path_buttonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                chooseFolder();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        private void FsrarId_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            changedData();
        }
        private void Inn_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            changedData();
        }
        private void Address_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            changedData();
        }
        private void Port_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            changedData();
        }
        private void Path_buttonEdit_EditValueChanged(object sender, EventArgs e)
        {
            changedData();
        }
        private void TimeoutShort_spinEdit_EditValueChanged(object sender, EventArgs e)
        {
            changedData();
        }
        private void TimeoutLong_spinEdit_EditValueChanged(object sender, EventArgs e)
        {
            changedData();
        }
        private void IntervalRequest_spinEdit_EditValueChanged(object sender, EventArgs e)
        {
            changedData();
        }
        #endregion Обработка событий пользовательского интерфейса.
    }
}