using System;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using AxiomAuxiliary.Transports;
using Axiom.AlcoTransport.Utility.Quittances;

namespace Axiom.AlcoTransport.Utility
{
    public partial class MainForm : XtraForm
    {
        #region Инициализация главной формы.
        public MainForm()
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
        #endregion Инициализация главной формы.

        #region Обработка событий пользовательского интерфейса.
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                Program.Logger.Info(this, "Попытка загрузки... ");

                loadVisualSettings();
                validateData();

                Text = string.Format("Informa. In Vino Veritas. Administrator's Utility (version {0})", GetNumbersOfVersion(3));
                Main_xtraTabControl.SelectedTabPageIndex = 0;

                Program.Logger.Info(this, "... загрузка успешно завершена.");
            }
            catch (Exception exception)
            {
                const string message = "Во время загрузки произошла ошибка.";

                Program.Logger.Error(message, exception);

                Cursor = Cursors.Default;

                ShowErrorMessage(string.Format("{0}\r\n\r\n{1}", message, exception.Message));
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
        private void Address_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void Port_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void TimeoutShort_spinEdit_EditValueChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void CheckConnection_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                Program.Logger.Info(this, "Попытка проверки связи... ");

                checkConnection();

                Program.Logger.Info(this, "... проверка связи завершена.");
            }
            catch (Exception exception)
            {
                const string message = "Во время проверки связи произошла ошибка.";

                Program.Logger.Error(message, exception);

                Cursor = Cursors.Default;

                ShowErrorMessage(string.Format("{0}\r\n\r\n{1}", message, exception.Message));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void AlcoCheque_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Операция формирования алкогольного чека... ");

                using (AlcoChequeForm form = new AlcoChequeForm(buildAddress(), (int)TimeoutShort_spinEdit.Value))
                {
                    form.ShowDialog();
                }

                Program.Logger.Info(this, "... операция формирования алкогольного чека завершена.");
            }
            catch (Exception exception)
            {
                const string message = "Во время формирования алкогольного чека произошла ошибка.";

                Program.Logger.Error(message, exception);

                ShowErrorMessage(string.Format("{0}\r\n\r\n{1}", message, exception.Message));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void BeerCheque_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Операция формирования пивного чека... ");

                using (BeerChequeForm form = new BeerChequeForm(buildAddress(), (int)TimeoutShort_spinEdit.Value))
                {
                    form.ShowDialog();
                }

                Program.Logger.Info(this, "... операция формирования пивного чека завершена.");
            }
            catch (Exception exception)
            {
                const string message = "Во время формирования пивного чека произошла ошибка.";

                Program.Logger.Error(message, exception);

                ShowErrorMessage(string.Format("{0}\r\n\r\n{1}", message, exception.Message));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void MassCheques_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Операция массовой обработки чеков... ");

                using (MassChequesForm form = new MassChequesForm(buildAddress(), (int)TimeoutShort_spinEdit.Value))
                {
                    form.ShowDialog();
                }

                Program.Logger.Info(this, "... операция массовой обработки чеков завершена.");
            }
            catch (Exception exception)
            {
                const string message = "Во время операции массовой обработки чеков произошла ошибка.";

                Program.Logger.Error(message, exception);

                ShowErrorMessage(string.Format("{0}\r\n\r\n{1}", message, exception.Message));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void Config_pictureEdit_Click(object sender, EventArgs e)
        {
            OpenCompanySite();
        }
        private void SupportVersion1_simpleButton_Click(object sender, EventArgs e)
        {
            sendVersionQuittance(1);
        }
        private void SupportVersion2_simpleButton_Click(object sender, EventArgs e)
        {
            sendVersionQuittance(2);
        }

        private void SupportVersion3_simpleButton_Click(object sender, EventArgs e)
        {
            sendVersionQuittance(3);
        }
        #endregion Обработка событий пользовательского интерфейса.

        #region Внутренние методы класса.
        /// <summary>
        /// Отправить уведомление.
        /// </summary>
        /// <param name="versionNumber">Номер версии.</param>
        private void sendVersionQuittance(int versionNumber)
        {
            try
            {
                string caption = string.Format("Для того, чтобы отправить на сервер ЕГАИС уведомление о возможности приемки и обработки документов, " +
                                               "составленных по версии 'v{0}', укажите идентификатор организации в системе ФС РАР.\r\n\r\n" +
                                               "Данная операция может повлечь серьёзные последствия, вплоть до невозможности обмена документами " +
                                               "с сервером ЕГАИС.", versionNumber);

                using (IdRequestForm form = new IdRequestForm(caption))
                {
                    if ((form.ShowDialog(this)) == DialogResult.OK)
                    {
                        Program.Logger.Info(this, string.Format("Попытка формирования и отправки уведомления об использовании версии 'v{0}'... ", versionNumber));

                        Cursor = Cursors.WaitCursor;

                        XmlDocument xmlDocument;

                        #region Xml...
                        {
                            Program.Logger.Info(this, "\t... начато формирование xml-документа квитанции... ");

                            VersionQuittance quittance = new VersionQuittance(form.Id, versionNumber);

                            xmlDocument = quittance.GetXmlDocument();

                            Program.Logger.Info(this, string.Format("\t... формирование xml-документа квитанции завершено; текст xml-документа: '{0}'... ", xmlDocument.OuterXml));
                        }
                        #endregion Xml...

                        #region File...
                        {
                            if (!Directory.Exists(VersionQuittance.PathOutRequest)) Directory.CreateDirectory(VersionQuittance.PathOutRequest);
                            string filename = string.Format("{0}\\informa.util.egais.version.quittance.{1}.xml", VersionQuittance.PathOutRequest, DateTime.Now.ToString("yyyyMMddHHmmssfff"));

                            Program.Logger.Info(this, string.Format("\t... попытка сохранения xml-документа квитанции в файле '{0}'... ", filename));
                            xmlDocument.Save(filename);
                            Program.Logger.Info(this, string.Format("\t... сохранения xml-документа квитанции в файле '{0}'... ", filename));
                        }
                        #endregion File...

                        #region Sending...
                        {
                            int timeout = (int) TimeoutShort_spinEdit.Value;
                            string address = string.Format("{0}/opt/in/InfoVersionTTN", buildAddress());
                            Program.Logger.Info(this, string.Format("\t... попытка отправить xml-документ на сервер УТМ по адресу '{0}'... ", address));

                            string answer = HttpTransport.UploadFile(address, xmlDocument, "xml_file", "text/xml; charset=utf-8", timeout);

                            Program.Logger.Info(this, string.Format("\t... отправка xml-документа квитанции по адресу '{0}' завершена... ", address));
                            Program.Logger.Info(this, string.Format("\t... от сервера УТМ получен ответ: '{0}'... ", answer));
                        }
                        #endregion Sending...

                        Cursor = Cursors.Default;

                        Program.Logger.Info(this, string.Format("... формирование и отправки уведомления об использовании версии 'v{0}' завершено.", versionNumber));

                        XtraMessageBox.Show(string.Format("Формирование и отправка уведомления об использовании версии 'v{0}' завершено.",versionNumber),
                                            "Завершение операции", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception exception)
            {
                string message = string.Format("Во время выполнения операции формирования и отправки уведомления об использовании версии 'v{0}' произошла ошибка.", versionNumber);

                Program.Logger.Error(message, exception);

                Cursor = Cursors.Default;

                ShowErrorMessage(string.Format("{0}\r\n\r\n{1}", message, exception.Message));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        #endregion Внутренние методы класса.

    }
}