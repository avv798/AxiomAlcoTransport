using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Axiom.AlcoTransport.Utility
{
    public partial class IdRequestForm : XtraForm
    {
        #region Внутренние объекты класса.
        /// <summary>
        /// FSRAR ID.
        /// </summary>
        private string id;
        #endregion Внутренние объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// FSRAR ID.
        /// Только чтение.
        /// </summary>
        public string Id
        {
            get { return id; }
        }
        #endregion Внешние объекты класса.

        #region Инициализация главной формы.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public IdRequestForm()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                id = string.Empty;

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
        /// <param name="caption">Заголовок формы.</param>
        public IdRequestForm(string caption) : this()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации с параметрами... ");

                if (!string.IsNullOrWhiteSpace(caption)) Caption_labelControl.Text = caption;

                Program.Logger.Info(this, "... инициализация с параметрами успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации произошла ошибка.", exception);
            }
        }
        #endregion Инициализация главной формы.

        #region Обработка событий пользовательского интерфейса.
        private void InnRequestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Logger.Info(this, "Форма закрывается.");
        }
        private void InnRequestForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузки формы... ");

                Number_textEdit.Text = string.Empty;

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
                id = Number_textEdit.Text;

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
                Ok_simpleButton.Enabled = ((!string.IsNullOrWhiteSpace(Number_textEdit.Text))
                                            && ((Number_textEdit.Text.Length > 5))
                                            && ((Number_textEdit.Text.Length < 33))
                                            && (Number_textEdit.Text.All(Char.IsDigit)));
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        #endregion Внутренние методы класса.
    }
}