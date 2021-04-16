using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Axiom.AlcoTransport.Utility.Cheques;
using DevExpress.XtraEditors;

namespace Axiom.AlcoTransport.Utility
{
    public partial class AlcoPositionForm : XtraForm
    {
        #region Внутренние объекты класса.
        /// <summary>
        /// Позиция для редактирования.
        /// </summary>
        private AlcoPosition position;
        /// <summary>
        /// Цвет элемента.
        /// Только чтение.
        /// </summary>
        private readonly Color color;
        #endregion Внутренние объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Позиция для редактирования.
        /// Только чтение.
        /// </summary>
        public AlcoPosition Position
        {
            get { return position; }
        }
        #endregion Внешние объекты класса.

        #region Инициализация главной формы.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public AlcoPositionForm()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                position = null;
                color = Barcode_textEdit.BackColor;

                Program.Logger.Info(this, "... инициализация успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации произошла ошибка.", exception);
            }
        }
        #endregion Инициализация главной формы.

        #region Обработка событий пользовательского интерфейса.
        private void AlcoPositionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Logger.Info(this, "Форма закрывается.");
        }
        private void AlcoPositionForm_Load(object sender, EventArgs e)
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

                position = new AlcoPosition
                                {
                                    Barcode = correctKeyboardLayout(Barcode_textEdit.Text),
                                    Ean = Ean_textEdit.Text,
                                    Volume = Volume_spinEdit.Value,
                                    Price = Price_spinEdit.Value
                                };

                Close();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        private void Barcode_textEdit_TextChanged(object sender, EventArgs e)
        {
            validateData();
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
                                            && (IsPDF417CodeValid(Barcode_textEdit.Text))
                                            && (Ean_textEdit.Text.Length > 0)
                                            && (Volume_spinEdit.Value > 0)
                                            && (Price_spinEdit.Value > 0));

                Barcode_textEdit.BackColor = (IsPDF417CodeValid(Barcode_textEdit.Text) ? color : Color.Salmon);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        /// <summary>
        /// Корректировка кода PDF-417 в зависимости от раскладки клавиатуры.
        /// </summary>
        /// <param name="code">Код для корректировки.</param>
        /// <returns>Скорректированный код.</returns>
        private string correctKeyboardLayout(string code)
        {
            StringBuilder builder = new StringBuilder();

            const string englishLow = "qwertyuiopasdfghjklzxcvbnm";
            const string russianLow = "йцукенгшщзфывапролдячсмить";
            string englishUp = englishLow.ToUpper();
            string russianUp = russianLow.ToUpper();

            Program.Logger.Info(this, string.Format("Попытка корректировки кода PDF-417 '{0}' в зависимости раскладки клавиатуры...", code));

            if (code.Any(russianLow.Contains) || code.Any(russianUp.Contains))
            {
                foreach (char c in code)
                {
                    int up = russianUp.IndexOf(c);

                    if (up >= 0)
                    {
                        builder.Append(englishUp[up]);
                    }
                    else
                    {
                        int low = russianLow.IndexOf(c);

                        builder.Append((low > 0) ? englishLow[low] : c);
                    }
                }
            }
            else
            {
                Program.Logger.Info(this, "... код в корректировке не нуждается.");

                return code;
            }

            string result = builder.ToString();

            Program.Logger.Info(this, string.Format("... корректировка кода PDF-417 в зависимости раскладки клавиатуры успешно завершена:\r\n\t\t'{0}' -> '{1}'.", code, result));

            return result;
        }
        #endregion Внутренние методы класса.

        #region Внешние статические методы класса.
        /// <summary>
        /// Проверка кода PDF-417 на соответствие требований ФСРАР.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <returns>Результат проверки.</returns>
        public static bool IsPDF417CodeValid(string code)
        {
            return ((!string.IsNullOrWhiteSpace(code))
                    && (code.Length == ACheque.EGAISCodeLength));
        }
        #endregion Внешние статические методы класса.
    }
}