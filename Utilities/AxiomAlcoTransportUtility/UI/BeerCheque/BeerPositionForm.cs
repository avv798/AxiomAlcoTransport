using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Axiom.AlcoTransport.Utility.Cheques;

namespace Axiom.AlcoTransport.Utility
{
    public partial class BeerPositionForm : XtraForm
    {
        #region Внутренние объекты класса.
        /// <summary>
        /// Позиция для редактирования.
        /// </summary>
        private BeerPosition position;
        #endregion Внутренние объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Позиция для редактирования.
        /// Только чтение.
        /// </summary>
        public BeerPosition Position
        {
            get { return position; }
        }
        #endregion Внешние объекты класса.

        #region Инициализация главной формы.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public BeerPositionForm()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                position = null;

                Program.Logger.Info(this, "... инициализация успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации произошла ошибка.", exception);
            }
        }
        #endregion Инициализация главной формы.

        #region Обработка событий пользовательского интерфейса.
        private void BeerPositionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Logger.Info(this, "Форма закрывается.");
        }
        private void BeerPositionForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузки формы... ");

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

                position = new BeerPosition
                                {
                                    Title = Title_textEdit.Text,
                                    Code = Code_textEdit.Text,
                                    Ean = Ean_textEdit.Text,
                                    AlcoStrength = Strength_spinEdit.Value,
                                    Volume = Volume_spinEdit.Value,
                                    Price = Price_spinEdit.Value,
                                    Count = (int)Count_spinEdit.Value
                                };

                Close();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        private void Ean_textEdit_TextChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void Volume_spinEdit_EditValueChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void Price_spinEdit_EditValueChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void Title_textEdit_TextChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void Code_textEdit_TextChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void Strength_spinEdit_EditValueChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void Count_spinEdit_EditValueChanged(object sender, EventArgs e)
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
                Ok_simpleButton.Enabled = ((!string.IsNullOrWhiteSpace(Ean_textEdit.Text))
                                            && (Ean_textEdit.Text.Length > 0)
                                            && (!string.IsNullOrWhiteSpace(Title_textEdit.Text))
                                            && (Title_textEdit.Text.Length > 0)
                                            && (!string.IsNullOrWhiteSpace(Code_textEdit.Text))
                                            && (Code_textEdit.Text.Length > 0)
                                            && (Strength_spinEdit.Value > 0)
                                            && (Volume_spinEdit.Value > 0)
                                            && (Price_spinEdit.Value > 0)
                                            && (Count_spinEdit.Value > 0));
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        #endregion Внутренние методы класса.
    }
}