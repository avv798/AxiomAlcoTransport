using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Axiom.AlcoTransport
{
    public partial class ActSingleForm : XtraForm
    {
        #region Внутренние объекты класса.
        /// <summary>
        /// Признак подтверждающего акта.
        /// Только чтение.
        /// </summary>
        private readonly bool isAccepted;
        /// <summary>
        /// Входящая накладная.
        /// Только чтение.
        /// </summary>
        private readonly InWaybill inWaybill;
        /// <summary>
        /// Управление документами.
        /// Только чтение.
        /// </summary>
        private readonly Documents documents;
        #endregion Внутренние объекты класса.

        #region Инициализация главной формы.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public ActSingleForm()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                documents = null;
                inWaybill = null;
                isAccepted = true;

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
        /// <param name="documents">Управление документами.</param>
        /// <param name="inWaybill">Входящая накладная.</param>
        /// <param name="isAccepted">Признак подтверждающего акта.</param>
        public ActSingleForm(Documents documents, InWaybill inWaybill, bool isAccepted) : this()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации с параметрами... ");

                this.documents = documents;
                this.inWaybill = inWaybill;
                this.isAccepted = isAccepted;

                Program.Logger.Info(this, "... инициализация с параметрами успешно завершена.");
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
        private void ActSingleForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Logger.Info(this, "Форма закрывается.");
        }
        private void ActSingleForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузки формы... ");

                Accept_pictureEdit.Visible = isAccepted;
                Reject_pictureEdit.Visible = !isAccepted;

                Text = string.Format("Отправить акт {0}", isAccepted ? "подтверждения" : "отказа");

                Caption_labelControl.Text = string.Format("Создать и отправить акт {0} по входящей накладной " +
                                                          "за номером '{1}' " +
                                                          "(номер по документу регистрации движения - '{2}'; " +
                                                          "отправитель - '{3}', " +
                                                          "дата составления - '{4}', " +
                                                          "дата отгрузки продукции - '{5}'), " +
                                                          "полученной '{6}'.",
                                                           isAccepted ? "подтверждения" : "отказа",
                                                           inWaybill.Identity,
                                                           inWaybill.WBRegId,
                                                           inWaybill.ShipperName,
                                                           inWaybill.Date,
                                                           inWaybill.ShippingDate,
                                                           inWaybill.CreateDateTime);

                Number_textEdit.Text = string.Format("{0}-{1}-{2}", inWaybill.WBRegId, (isAccepted) ? "accepted" : "rejected", DateTime.Now.ToString("yyMMddHHmmss"));
                Comment_textEdit.Text = isAccepted ? "Подтверждение приёма продукции." : string.Empty;

                validateData();

                Program.Logger.Info(this, "... загрузка формы успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время загрузки произошла ошибка.", exception);
            }
        }
        private void Send_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                SplashAccessor.Show();

                documents.SendSingleAct(inWaybill, isAccepted, Number_textEdit.Text, Comment_textEdit.Text);

                SplashAccessor.Close();
                Cursor = Cursors.Default;

                XtraMessageBox.Show(string.Format("Акт {0} успешно отправлен.", isAccepted ? "подтверждения" : "отказа"),
                                    "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);

                Close();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время отправки акта произошла ошибка.", exception);

                SplashAccessor.Close();
                Cursor = Cursors.Default;

                MainForm.ShowErrorMessage(string.Format("Во время отправки акта произошла ошибка: '{0}'.", exception.Message));
            }
        }
        private void Number_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void Comment_textEdit_EditValueChanged(object sender, EventArgs e)
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
                Send_simpleButton.Enabled = ((!string.IsNullOrWhiteSpace(Number_textEdit.Text)) && (!string.IsNullOrWhiteSpace(Comment_textEdit.Text)));
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        #endregion Внутренние методы класса.
    }
}