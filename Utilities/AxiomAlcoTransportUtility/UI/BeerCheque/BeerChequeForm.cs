using System;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.ComponentModel;
using DevExpress.XtraEditors;
using AxiomAuxiliary.Transports;
using Axiom.AlcoTransport.Utility.Cheques;

namespace Axiom.AlcoTransport.Utility
{
    public partial class BeerChequeForm : XtraForm
    {
        #region Внутренние объекты класса.
        /// <summary>
        /// Список марок.
        /// Только чтение.
        /// </summary>
        private readonly BindingList<BeerPosition> positions;
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

        #region Внешние объекты класса.
        /// <summary>
        /// Список марок.
        /// Только чтение.
        /// </summary>
        public BindingList<BeerPosition> Positions
        {
            get { return positions; }
        }
        #endregion Внешние объекты класса.

        #region Инициализация главной формы.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public BeerChequeForm()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                positions = new BindingList<BeerPosition>();
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
        public BeerChequeForm(string utmAddress, int timeout) : this()
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
        private void BeerChequeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Logger.Info(this, "Форма закрывается.");
        }
        private void BeerChequeForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузки формы... ");

                Document_bindingSource.DataSource = Positions;

                DateTime now = DateTime.Now;

                Date_dateEdit.EditValue = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, 0);
                Time_timeEdit.EditValue = new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second, 0);

                Direct_radioGroup.SelectedIndex = 1;

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
                Program.Logger.Info(this, "Попытка формирования и отправки пивного чека... ");

                Cursor = Cursors.WaitCursor;

                ACheque cheque = sendCheque();

                Cursor = Cursors.Default;

                XtraMessageBox.Show(string.Format("Пивной чек (направление: '{0}'; количество позиций: {1}; общая сумма: {2} руб.) успешно отправлен на сервер УТМ.",
                                                    ((cheque.Direct == 1) ? "продажа" : "возврат"),cheque.CountPositions, cheque.TotalSum.ToString("F2")),
                                    "Успешное завершение операции", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Program.Logger.Info(this, "... формирование и отправка пивного чека завершена.");
            }
            catch (Exception exception)
            {
                const string message = "Во время выполнения операции формирования и отправки чека произошла ошибка.";

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
                BeerPosition position;

                using (BeerPositionForm form = new BeerPositionForm())
                {
                    if (form.ShowDialog() != DialogResult.OK) return;
                    
                    position = form.Position;
                }

                positions.Add(position);

                Cheque_gridView.RefreshData();

                validateData();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception));
            }
        }
        private void Remove_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Document_bindingSource.Current == null) return;

                BeerPosition selected = (BeerPosition)Document_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                positions.Remove(selected);
                
                Cheque_gridView.RefreshData();

                validateData();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception));
            }
        }
        private void Title_textEdit_TextChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void Address_textEdit_TextChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void Inn_textEdit_TextChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void Kpp_textEdit_TextChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void Shift_spinEdit_EditValueChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void Number_spinEdit_EditValueChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void Kassa_textEdit_TextChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void Direct_radioGroup_EditValueChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void Date_dateEdit_EditValueChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void Time_timeEdit_EditValueChanged(object sender, EventArgs e)
        {
            validateData();
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
                Send_simpleButton.Enabled = ((Positions.Count > 0)
                                           && (!string.IsNullOrWhiteSpace(Title_textEdit.Text))
                                           && (!string.IsNullOrWhiteSpace(Address_textEdit.Text))
                                           && (!string.IsNullOrWhiteSpace(Inn_textEdit.Text))
                                           && (!string.IsNullOrWhiteSpace(Kpp_textEdit.Text))
                                           && (!string.IsNullOrWhiteSpace(Kassa_textEdit.Text))
                                           && (Shift_spinEdit.Value > 0)
                                           && (Number_spinEdit.Value > 0)
                                           && ((DateTime)Date_dateEdit.EditValue) < DateTime.Now.AddMonths(1));
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        /// <summary>
        /// Отправить чек.
        /// </summary>
        /// <returns>Отправленный чек.</returns>
        private BeerCheque sendCheque()
        {
            BeerCheque cheque;
            XmlDocument xmlDocument;

            #region Creating...
            {
                Program.Logger.Info(this, "\t... начато создание пивного чека... ");

                DateTime date = (DateTime) Date_dateEdit.EditValue;
                DateTime time = (DateTime) Time_timeEdit.EditValue;

                cheque = new BeerCheque
                                {
                                    Title = Title_textEdit.Text,
                                    Address = Address_textEdit.Text,
                                    Inn = Inn_textEdit.Text,
                                    Kpp = Kpp_textEdit.Text,
                                    Shift = (int) Shift_spinEdit.Value,
                                    Number = (int) Number_spinEdit.Value,
                                    Kassa = Kassa_textEdit.Text,
                                    ProcessDateTime = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second, 0),
                                    Direct = (Direct_radioGroup.SelectedIndex == 0) ? 1 : (-1)
                                };

                cheque.Positions.Clear();
                foreach (BeerPosition position in positions)
                {
                    cheque.Positions.Add(position);
                }

                Program.Logger.Info(this, "\t... создание пивного чека завершено... ");
            }
            #endregion Creating...

            #region Xml...
            {
                Program.Logger.Info(this, "\t... начато формирование xml-документа пивного чека... ");

                xmlDocument = cheque.BuildXmlDocument();

                Program.Logger.Info(this, string.Format("\t... формирование xml-документа пивного чека завершено; текст xml-документа: '{0}'... ", xmlDocument.OuterXml));
            }
            #endregion Xml...

            #region File...
            { 
                if (!Directory.Exists(ACheque.PathOutRequest)) Directory.CreateDirectory(ACheque.PathOutRequest);
                string filename = string.Format("{0}\\informa.util.egais.beer.{1}.xml", ACheque.PathOutRequest, DateTime.Now.ToString("yyyyMMddHHmmssfff"));

                Program.Logger.Info(this, string.Format("\t... попытка сохранения xml-документа пивного чека в файле '{0}'... ", filename));
                xmlDocument.Save(filename);
                Program.Logger.Info(this, string.Format("\t... сохранения xml-документа пивного чека в файле '{0}'... ", filename));
            }
            #endregion File...

            #region Sending...
            {
                string address = string.Format("{0}/xml/", utmAddress);
                Program.Logger.Info(this, string.Format("\t... попытка отправить xml-документ на сервер УТМ по адресу '{0}'... ", address));

                string answer = HttpTransport.UploadFile(address, xmlDocument, "xml_file", "text/xml; charset=utf-8", timeout);

                Program.Logger.Info(this, string.Format("\t... отправка xml-документа пивного чека по адресу '{0}' завершена... ", address));
                Program.Logger.Info(this, string.Format("\t... от сервера УТМ получен ответ: '{0}'... ", answer));
            }
            #endregion Sending...

            return cheque;
        }
        #endregion Внутренние методы класса.
    }
}