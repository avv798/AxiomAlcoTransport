using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Axiom.AlcoTransport
{
    public partial class WaybillRegIdRequestForm : XtraForm
    {
        #region Внутренние объекты класса.
        /// <summary>
        /// Идентификатор.
        /// </summary>
        private string wBRegId;
        #endregion Внутренние объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Идентификатор.
        /// Только чтение.
        /// </summary>
        public string WBRegId
        {
            get { return wBRegId; }
        }
        #endregion Внешние объекты класса.

        #region Инициализация главной формы.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public WaybillRegIdRequestForm()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                wBRegId = string.Empty;

                Program.Logger.Info(this, "... инициализация успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации произошла ошибка.", exception);
            }
        }
        #endregion Инициализация главной формы.

        #region Обработка событий пользовательского интерфейса.
        private void WaybillRegIdRequestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Logger.Info(this, "Форма закрывается.");
        }
        private void WaybillRegIdRequestForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузки формы... ");

                Number_textEdit.Text = "TTN-";

                validateData();

                Program.Logger.Info(this, "... загрузка формы успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время загрузки произошла ошибка.", exception);
            }
        }
        private void Cancel_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        private void Ok_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.OK;
                wBRegId = Number_textEdit.Text;

                Close();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }

        }
        private void Number_textEdit_EditValueChanged(object sender, EventArgs e)
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
                Ok_simpleButton.Enabled = (!string.IsNullOrWhiteSpace(Number_textEdit.Text));
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        #endregion Внутренние методы класса.
    }
}