using System;
using DevExpress.XtraEditors;
using Axiom.AlcoTransport.Watchtower.Configuration;

namespace Axiom.AlcoTransport
{
    public partial class OptionsForm : XtraForm
    {
        #region Инициализация главной формы.
        public OptionsForm()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                Program.Logger.Info(this, "... инициализация успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации произошла ошибка.", exception);
            }
        }
        public OptionsForm(ConfigurationData data) : this()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации с параметрами... ");

                FsrarId_textEdit.Text = data.FsrarId;
                Inn_textEdit.Text = data.Inn;
                Address_textEdit.Text = data.UtmAddress;
                Port_textEdit.Text = data.UtmPort;
                TimeoutShort_spinEdit.Value = data.UtmTimeoutShort;
                TimeoutLong_spinEdit.Value = data.UtmTimeoutLong;
                IntervalRequest_spinEdit.Value = data.IntervalDataRequest;
                Path_textEdit.Text = data.PathToLocalDatabase;

                Program.Logger.Info(this, "... инициализация  с параметрами успешно завершена.");
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
        private void OptionsForm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            Program.Logger.Info(this, "Форма закрывается.");
        }
        #endregion Обработка событий пользовательского интерфейса.
    }
}