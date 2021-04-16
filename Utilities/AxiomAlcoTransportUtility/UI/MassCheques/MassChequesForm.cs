using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Xml;
using AxiomAuxiliary.Transports;
using DevExpress.XtraEditors;
using Axiom.AlcoTransport.Utility.Cheques;

namespace Axiom.AlcoTransport.Utility
{
    public partial class MassChequesForm : XtraForm
    {
        #region Внутренние объекты класса.
        /// <summary>
        /// Список марок.
        /// Только чтение.
        /// </summary>
        private readonly BindingList<FileCheque> files;
        /// <summary>
        /// Адрес УТМ.
        /// Только чтение.
        /// </summary>
        private readonly string utmAddress;
        /// <summary>
        /// Тайм-аут.
        /// Только чтение.
        /// </summary>
        private readonly int timeout;
        #endregion Внутренние объекты класса.

        #region Инициализация главной формы.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public MassChequesForm()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                files = new BindingList<FileCheque>();
                utmAddress = "http://localhost:8080";
                timeout = 5000;

                Program.Logger.Info(this, "... инициализация успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации произошла ошибка.", exception);
            }
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        public MassChequesForm(string utmAddress, int timeout) : this()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации с параметрами... ");

                this.utmAddress = utmAddress;
                this.timeout = timeout;

                Program.Logger.Info(this, "... инициализация с параметрами успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации с параметрами произошла ошибка.", exception);
            }
        }
        #endregion Инициализация главной формы.

        #region Обработка событий пользовательского интерфейса.
        private void MassChequesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Logger.Info(this, "Форма закрывается.");
        }
        private void MassChequesForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузки формы... ");

                Document_bindingSource.DataSource = files;
                Direct_radioGroup.SelectedIndex = 2;

                validateData();

                Program.Logger.Info(this, "... загрузка формы успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время загрузки произошла ошибка.", exception);
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
        private void Send_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка формирования и отправки чеков... ");

                Cursor = Cursors.WaitCursor;

                sendCheques();

                validateData();

                Cursor = Cursors.Default;

                Program.Logger.Info(this, "... формирование и отправка чеков завершена.");

                XtraMessageBox.Show("Операции обработки, корректировки и отправка файлов-чеков в УТМ ЕГАИС успешно завершены.",
                                    "Завершение операции", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception exception)
            {
                const string message = "Во время выполнения операции формирования и отправки чеков произошла ошибка.";

                Program.Logger.Error(message, exception);

                Cursor = Cursors.Default;

                MainForm.ShowErrorMessage(string.Format("{0}\r\n\r\n{1}", message, exception.Message));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void Add_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Cheques_openFileDialog.ShowDialog(this) != DialogResult.OK) return;

                Cursor = Cursors.WaitCursor;

                foreach (string filename in Cheques_openFileDialog.FileNames)
                {
                    if (files.Any(file => (file.Filename.ToLower() == filename.ToLower()))) continue;

                    files.Add(new FileCheque(filename));
                }

                validateData();

                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;

                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void Remove_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<FileCheque> selected = Cheque_gridView.GetSelectedRows().Select(i => (FileCheque)Cheque_gridView.GetRow(i)).ToList();

                if (selected.Count == 0) return;

                if (DialogResult.Yes == XtraMessageBox.Show(string.Format("Удалить выбранные файлы (в количестве {0} шт.) из списка обработки?", selected.Count)
                                                                          , "Подтверждение операции"
                                                                          , MessageBoxButtons.YesNo
                                                                          , MessageBoxIcon.Question))
                {
                    Cursor = Cursors.WaitCursor;

                    Program.Logger.Info(this, string.Format("Производится попытка удаления файлов (в количестве {0} шт.) из списка обработки...", selected.Count));

                    foreach (FileCheque file in selected)
                    {
                        files.Remove(file);
                    }

                    Program.Logger.Info(this, string.Format("... попытка удаления файлов (в количестве {0} шт.) из списка обработки успешно завершена.", selected.Count));

                    Cheque_gridView.ClearSelection();

                    Cursor = Cursors.Default;
                }

                Cheque_gridView.RefreshData();

                validateData();
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;

                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void Title_checkEdit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Title_textEdit.Enabled = Title_checkEdit.Checked;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        private void Address_checkEdit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Address_textEdit.Enabled = Address_checkEdit.Checked;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        private void Inn_checkEdit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Inn_textEdit.Enabled = Inn_checkEdit.Checked;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        private void Kpp_checkEdit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Kpp_textEdit.Enabled = Kpp_checkEdit.Checked;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        private void Kassa_checkEdit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Kassa_textEdit.Enabled = Kassa_checkEdit.Checked;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        private void Direct_checkEdit_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Direct_radioGroup.Enabled = Direct_checkEdit.Checked;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        private void Cheque_gridControl_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (Document_bindingSource.Current == null) return;

                FileCheque selected = (FileCheque)Document_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                System.Diagnostics.Process.Start(selected.Filename);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка: '{0}'.", exception.Message));
            }
        }
        #endregion Обработка событий пользовательского интерфейса.

        #region Внутренние методы класса.
        /// <summary>
        /// Проверить данные.
        /// </summary>
        private void validateData()
        {
            try
            {
                Send_simpleButton.Enabled = files.Any(file => (file.Status.ToLower() == "new"));
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        /// <summary>
        /// Отправить чеки.
        /// </summary>
        private void sendCheques()
        {
            foreach (FileCheque file in files)
            {
                if (file.Status.ToLower() != "new") continue;

                try
                {
                    Program.Logger.Info(this, string.Format("\t... попытка отправить файл-чек '{0}'...", file.Filename));

                    switch (file.TypeCheque.ToLower())
                    {
                        case "alco":
                        {
                            sendAlcoCheque(file);
                            break;
                        }
                        case "beer":
                        {
                            sendBeerCheque(file);
                            break;
                        }
                    }

                    file.SetStatus("processed");

                    Program.Logger.Info(this, string.Format("\t... файл-чек '{0}' отправлен...", file.Filename));
                }
                catch (Exception exception)
                {
                    Program.Logger.Error(string.Format("Во время отправления файла-чека '{0}' произошла ошибка.", file.Filename), exception);

                    file.Comment = exception.Message;
                    file.SetStatus("processed_error");
                }

                Cheque_gridView.RefreshData();
                Refresh();
            }
        }
        /// <summary>
        /// Отправить чек.
        /// </summary>
        /// <param name="file">Файл.</param>
        private void sendAlcoCheque(FileCheque file)
        {
            if (file == null) throw new Exception("Файл чека пустой.");
            if (file.AlcoCheque == null) throw new Exception("Данные алкогольного чека не заполнены.");

            AlcoCheque cheque = file.AlcoCheque;

            correctCheque(cheque);

            sendCheque(cheque, string.Format("alco.from_{0}", file.ShortFilename));
        }
        /// <summary>
        /// Отправить чек.
        /// </summary>
        /// <param name="file">Файл.</param>
        private void sendBeerCheque(FileCheque file)
        {
            if (file == null) throw new Exception("Файл чека пустой.");
            if (file.BeerCheque == null) throw new Exception("Данные пивного чека не заполнены.");

            BeerCheque cheque = file.BeerCheque;

            correctCheque(cheque);

            sendCheque(cheque, string.Format("beer.from_{0}", file.ShortFilename));
        }
        /// <summary>
        /// Скорректировать атрибуты чека.
        /// </summary>
        /// <param name="cheque">Чек.</param>
        private void correctCheque(ACheque cheque)
        {
            Program.Logger.Info(this, "\t\t... начата корректировка xml-документа чека...");

            if ((Title_checkEdit.Checked) && (Title_textEdit.Text.Length > 0))
            {
                cheque.Title = Title_textEdit.Text;

                Program.Logger.Info(this, string.Format("\t\t\t... скорректировано наименование организации на '{0}'...", cheque.Title));
            }

            if ((Address_checkEdit.Checked) && (Address_textEdit.Text.Length > 0))
            {
                cheque.Address = Address_textEdit.Text;

                Program.Logger.Info(this, string.Format("\t\t\t... скорректирован почтовый адрес организации на '{0}'...", cheque.Address));
            }

            if ((Inn_checkEdit.Checked) && (Inn_textEdit.Text.Length > 0))
            {
                cheque.Inn = Inn_textEdit.Text;

                Program.Logger.Info(this, string.Format("\t\t\t... скорректирован ИНН организации на '{0}'...", cheque.Inn));
            }

            if ((Kpp_checkEdit.Checked) && (Kpp_textEdit.Text.Length > 0))
            {
                cheque.Kpp = Kpp_textEdit.Text;

                Program.Logger.Info(this, string.Format("\t\t\t... скорректирован КПП организации на '{0}'...", cheque.Kpp));
            }

            if ((Kassa_checkEdit.Checked) && (Kassa_textEdit.Text.Length > 0))
            {
                cheque.Kassa = Kassa_textEdit.Text;

                Program.Logger.Info(this, string.Format("\t\t\t... скорректирован заводской номер ККМ на '{0}'...", cheque.Kassa));
            }

            if (Direct_checkEdit.Checked)
            {
                int oldDirect = cheque.Direct;
                
                switch (Direct_radioGroup.SelectedIndex)
                {
                    case 0: cheque.Direct = 1; break;
                    case 1: cheque.Direct = -1; break;
                    case 2: cheque.Direct *= (-1); break;

                    default: throw new Exception("Неизвестное значение типа чека.");
                }

                Program.Logger.Info(this, string.Format("\t\t\t... скорректировано направление чека с '{0}' на '{1}'...", oldDirect, cheque.Direct));

                if (cheque.Direct > 0)
                {
                    if (cheque.Number > 10000000)
                    {
                        int old = cheque.Number;

                        cheque.Number -= 10000000;

                        Program.Logger.Info(this, string.Format("\t\t\t\t... скорректирован номер чека с '{0}' на '{1}'...", old, cheque.Number));
                    }
                }

                if (cheque.Direct < 0)
                {
                    if (cheque.Number < 1000000)
                    {
                        int old = cheque.Number;

                        cheque.Number += 10000000;

                        Program.Logger.Info(this, string.Format("\t\t\t\t... скорректирован номер чека с '{0}' на '{1}'...", old, cheque.Number));
                    }
                }
            }
       
            Program.Logger.Info(this, "\t\t... корректировка xml-документа чека завершена...");
        }
        /// <summary>
        /// Отправить чек.
        /// </summary>
        /// <param name="cheque">Чек.</param>
        /// <param name="prefix">Тип чека.</param>
        private void sendCheque(ACheque cheque, string prefix)
        {
            XmlDocument xmlDocument;

            #region Xml...
            {
                Program.Logger.Info(this, "\t\t... начато формирование xml-документа чека...");

                xmlDocument = cheque.BuildXmlDocument();

                Program.Logger.Info(this, string.Format("\t\t... формирование xml-документа чека завершено; текст xml-документа: '{0}'...", xmlDocument.OuterXml));
            }
            #endregion Xml...

            #region File...
            {
                if (!Directory.Exists(ACheque.PathOutRequest)) Directory.CreateDirectory(ACheque.PathOutRequest);
                string filename = string.Format("{0}\\informa.util.egais.{1}.{2}.xml", ACheque.PathOutRequest, DateTime.Now.ToString("yyyyMMddHHmmssfff"), prefix);

                Program.Logger.Info(this, string.Format("\t\t... попытка сохранения xml-документа чека в файле '{0}'... ", filename));

                xmlDocument.Save(filename);

                Program.Logger.Info(this, string.Format("\t\t... сохранение xml-документа чека в файле '{0}'... ", filename));
            }
            #endregion File...

            #region Sending...
            {
                string address = string.Format("{0}/xml/", utmAddress);
                Program.Logger.Info(this, string.Format("\t\t... попытка отправить xml-документ на сервер УТМ по адресу '{0}'...", address));

                string answer = HttpTransport.UploadFile(address, xmlDocument, "xml_file", "text/xml; charset=utf-8", timeout);

                Program.Logger.Info(this, string.Format("\t\t... отправка xml-документа чека по адресу '{0}' завершена...", address));
                Program.Logger.Info(this, string.Format("\t\t... от сервера УТМ получен ответ: '{0}'... ", answer));
            }
            #endregion Sending...
        }
        #endregion Внутренние методы класса.
    }
}