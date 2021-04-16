using System;
using System.IO;
using System.Globalization;
using DevExpress.XtraEditors;
using Axiom.AlcoTransport.Watchtower;

namespace Axiom.AlcoTransport
{
    public partial class LicenceForm : XtraForm
    {
        #region Инициализация главной формы.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public LicenceForm()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                string[] licence = File.ReadAllLines(Program.GetAppSetting("licenceFilename"));

                FsrarId_textEdit.Text = AesCrypto.Decrypt(licence[8]);
                DT_dateEdit.EditValue = DateTime.ParseExact(AesCrypto.Decrypt(licence[9]).Substring(0, 10), "yyyy-MM-dd", CultureInfo.InvariantCulture);

                Program.Logger.Info(this, "... инициализация успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации произошла ошибка.", exception);
            }
        }
        #endregion Инициализация главной формы.

        #region Обработка событий пользовательского интерфейса.
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
        private void LicenceForm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            Program.Logger.Info(this, "Форма закрывается.");
        }
        #endregion Обработка событий пользовательского интерфейса.
    }
}