using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using Microsoft.Win32;
using DevExpress.XtraEditors;
using Axiom.AlcoTransport.Watchtower;
using Axiom.AlcoTransport.Watchtower.Crypto;
using Axiom.AlcoTransport.Watchtower.Configuration;

namespace Axiom.AlcoTransport.Licence
{
    public partial class MainForm : XtraForm
    {
        #region Инициализация главной формы.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion Инициализация главной формы.

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
                    Trace.TraceError("Во время поиска конфигурации в реестре произошла ошибка.", exception);
                }

                Main_defaultLookAndFeel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
                Main_defaultLookAndFeel.LookAndFeel.SetSkinStyle(skinName);
            }
            catch (Exception exception)
            {
                Trace.TraceError("Во время загрузки оформления произошла ошибка.", exception);
            }
        }
        /// <summary>
        /// Проверить данные.
        /// </summary>
        private void validateData()
        {
            try
            {
                Generate_simpleButton.Enabled = ((!string.IsNullOrWhiteSpace(FsrarId_textEdit.Text))
                                                    && ((DateTime)DT_dateEdit.EditValue > DateTime.Now));
            }
            catch (Exception exception)
            {
                Trace.TraceError("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        /// <summary>
        /// Сгенерировать файл лицензии.
        /// </summary>
        private void generate()
        {
            string one = AesCrypto.Encrypt(FsrarId_textEdit.Text);
            string two = AesCrypto.Encrypt(((DateTime)DT_dateEdit.EditValue).ToString("yyyy-MM-dd") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            string three = Md5Helper.GetHash(Constants.Motto + one + two);

            string note = string.Format("=============================\r\n" +     // 0
                                        "Informa.AlcoTransport.Licence\r\n" +     // 1
                                        "=============================\r\n" +     // 2
                                        "File created at: '{0}'.\r\n" +           // 3
                                        "=============================\r\n" +     // 4
                                        "FSRAR_ID: '{1}'.\r\n" +                  // 5
                                        "Expiration date: '{2}'.\r\n" +           // 6
                                        "=============================\r\n" +     // 7
                                        "{3}\r\n" +                               // 8  (!!!)
                                        "{4}\r\n" +                               // 9  (!!!)
                                        "{5}\r\n" +                               // 10 (!!!)
                                        "=============================\r\n",      // 11
                                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                        FsrarId_textEdit.Text,
                                        ((DateTime)DT_dateEdit.EditValue).ToString("dd MMMM yyyy (dddd)"),
                                        one,
                                        two,
                                        three);

            File.WriteAllText(Licence_saveFileDialog.FileName, note);
        }
        #endregion Внутренние методы класса.

        #region Обработка событий пользовательского интерфейса.
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                loadVisualSettings();

                DT_dateEdit.EditValue = DateTime.Now.AddDays(1);

                validateData();
            }
            catch (Exception exception)
            {
                Trace.TraceError("Во время загрузки произошла ошибка.", exception);

                Cursor = Cursors.Default;

                XtraMessageBox.Show(string.Format("Во время загрузки приложения произошла ошибка: '{0}'.", exception.Message),
                                    "Ошибка генерации лицензии",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void Close_simpleButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void FsrarId_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void DT_dateEdit_EditValueChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void Generate_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Licence_saveFileDialog.ShowDialog(this) != DialogResult.OK) return;

                {
                    generate();

                    XtraMessageBox.Show("Файл лицензии успешно сгенерирован.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                Trace.TraceError("Во время генерации лицензии произошла ошибка.", exception);

                XtraMessageBox.Show(string.Format("Во время генерации лицензии произошла ошибка: '{0}'.", exception.Message),
                                    "Ошибка генерации лицензии",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
            }
        }
        #endregion Обработка событий пользовательского интерфейса.
    }
}