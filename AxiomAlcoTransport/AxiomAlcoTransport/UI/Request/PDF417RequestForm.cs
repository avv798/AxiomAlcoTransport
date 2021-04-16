using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;

namespace Axiom.AlcoTransport
{
    public partial class PDF417RequestForm : XtraForm
    {
        #region Внешние константы класса.
        /// <summary>
        /// Длина кода PDF-417 по документации ЕГАИС.
        /// <remarks>
        /// Состав информации на марке определяется приказом Росалкогольрегулирования N33н от 12.05.2010.
        /// При осуществлении продажи требуется сканировать двумерный штриховой код.
        /// Штрих код имеет формат PDF-417. Так же требуется сканировать EAN-код.
        /// Пример набора символов, содержащихся в штрихкоде PDF-417 имеет вид (без кавычек):
        /// "19N00000XOPN13MM66T0HVF311210120003676539219152175585956302712947109".
        /// Символы представлены цифрами либо строчными латинскими буквами. Длина набора символов – 68 единиц.
        /// </remarks>
        /// </summary>
        public const int EGAISCodeLengthOld = 68;
        public const int EGAISCodeLengthNew = 150;
        #endregion Внешние константы класса.

        #region Внутренние объекты класса.
        /// <summary>
        /// Код PDF-417.
        /// </summary>
        private string pdf417;
        /// <summary>
        /// Заголовок.
        /// </summary>
        private readonly string caption;
        /// <summary>
        /// Список для проверки повторяющихся кодов.
        /// </summary>
        private readonly IList<string> list;
        /// <summary>
        /// Цвет элемента.
        /// Только чтение.
        /// </summary>
        private readonly Color color;
        #endregion Внутренние объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Код PDF-417.
        /// Только чтение.
        /// </summary>
        public string PDF417
        {
            get { return pdf417; }
        }
        #endregion Внешние объекты класса.

        #region Инициализация главной формы.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public PDF417RequestForm()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                pdf417 = string.Empty;
                caption = string.Empty;
                list = new List<string>();

                color = Number_textEdit.BackColor;

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
        /// <param name="list">Список для проверки повторяющихся кодов.</param>
        /// <param name="caption">Заголовок</param>
        public PDF417RequestForm(IList<string> list, string caption) : this()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации с параметрами... ");

                this.list = list;
                this.caption = caption;

                Program.Logger.Info(this, "... инициализация с параметрами успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации произошла ошибка.", exception);
            }
        }
        #endregion Инициализация главной формы.

        #region Обработка событий пользовательского интерфейса.
        private void PDF417RequestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Logger.Info(this, "Форма закрывается.");
        }
        private void PDF417RequestForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузки формы... ");

                Text = caption;
                
                Number_textEdit.Text = string.Empty;

                validateData();

                Number_textEdit.BackColor = color;

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
                if (list != null)
                {
                    string newCode = correctKeyboardLayout(Number_textEdit.Text);

                    if (list.Any(code => code == newCode))
                    {
                        Exist_toolTipController.ShowHint("В данной партии продукции такой штрих-код уже был отсканирован.",
                                                         "Повторный штрих-код",
                                                         Number_textEdit,
                                                         ToolTipLocation.BottomCenter);

                        Number_textEdit.Text = string.Empty;

                        return;
                    }

                    DialogResult = DialogResult.OK;
                    pdf417 = newCode;
                }

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
        private void PDF417RequestForm_Activated(object sender, EventArgs e)
        {
            try
            {
                setCorrectFocus();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        private void Number_textEdit_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!IsPDF417CodeValid(Number_textEdit.Text))
                {
                    Number_textEdit.Text = string.Empty;
                }
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
                bool valid = IsPDF417CodeValid(Number_textEdit.Text);

                Ok_simpleButton.Enabled = valid;

                Number_textEdit.BackColor = (valid ? color : Color.Salmon);
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
        /// <summary>
        /// Установить фокус на поле ввода.
        /// </summary>
        private void setCorrectFocus()
        {
            Number_textEdit.Focus();
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
                    && ((code.Length == EGAISCodeLengthOld) || (code.Length == EGAISCodeLengthNew)));
        }
        #endregion Внешние статические методы класса.
    }
}