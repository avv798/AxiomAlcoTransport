using System;
using System.Windows.Forms;
using System.Xml;
using System.Globalization;

namespace Axiom.AlcoTransport.Utility.Cheques
{
    /// <summary>
    /// Общий класс для алкогольных и пивных чеков.
    /// Абстрактный класс.
    /// </summary>
    public abstract class ACheque
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
        public const int EGAISCodeLength = 68;
        #endregion Внешние константы класса.

        #region Защищённые объекты класса.
        /// <summary>
        /// Установленный в системе разделитель целой и дробной части числа.
        /// </summary>
        protected readonly string decimalSeparator;
        #endregion Защищённые объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Наименование организации.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Адрес организации.
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// ИНН организации.
        /// </summary>
        public string Inn { get; set; }
        /// <summary>
        /// КПП организации.
        /// </summary>
        public string Kpp { get; set; }
        /// <summary>
        /// Заводской номер ККМ.
        /// </summary>
        public string Kassa { get; set; }
        /// <summary>
        /// Номер смены.
        /// </summary>
        public int Shift { get; set; }
        /// <summary>
        /// Номер чека.
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// Дата и время чека.
        /// </summary>
        public DateTime ProcessDateTime { get; set; }
        /// <summary>
        /// Дата и время чека (форматированная строка).
        /// Только чтение.
        /// </summary>
        public string ProcessDateTimeFormatted
        {
            get { return ProcessDateTime.ToString("ddMMyyHHmm"); }
        }
        /// <summary>
        /// Направление чека ("1" - продажа; "-1" - возврат).
        /// </summary>
        public int Direct { get; set; }
        /// <summary>
        /// Количество позиций в чеке.
        /// Только чтение.
        /// </summary>
        public int CountPositions
        {
            get { return getCountPositions(); }
        }
        /// <summary>
        /// Общая сумма в чеке.
        /// Только чтение.
        /// </summary>
        public decimal TotalSum
        {
            get { return getTotalSum(); }
        }
        #endregion Внешние объекты класса.

        #region Внешние статические объекты класса.
        /// <summary>
        /// Путь для сохранения файлов-чеков.
        /// Только чтение.
        /// </summary>
        public static string PathOutRequest
        {
            get { return string.Format("{0}\\EgaisCheque", Application.StartupPath); }
        }
        #endregion Внешние статические объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        protected ACheque()
        {
            decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            Title = string.Empty;
            Address = string.Empty;
            Inn = string.Empty;
            Kpp = string.Empty;
            Kassa = string.Empty;
            Shift = 0;
            Number = 0;
            ProcessDateTime = DateTime.Now;
            Direct = -1;
        }
        #endregion Конструкторы класса.

        #region Защищённые методы класса.
        /// <summary>
        /// Добавить атрибут к указанной XML-ноде.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="node">XML-нода.</param>
        /// <param name="name">Наименование атрибута.</param>
        /// <param name="value">Значение атрибута.</param>
        protected void addAttribute(XmlDocument document, XmlNode node, string name, string value)
        {
            if (document == null) throw new Exception("Object 'xmlCheque' is null.");

            XmlAttribute attribute = document.CreateAttribute(name);
            attribute.Value = value;

            if (node.Attributes == null) throw new Exception("Object 'Node.Attributes' is null.");

            node.Attributes.Append(attribute);
        }
        /// <summary>
        /// Проверить данные чека.
        /// </summary>
        protected void validateChequeData()
        {
            if (string.IsNullOrWhiteSpace(Title)) throw new Exception("Указано некорректное наименование организации (требуется для алкогольного чека).");
            if (string.IsNullOrWhiteSpace(Address)) throw new Exception("Указан некорректный почтовый адрес организации (требуется для алкогольного чека).");
            if (string.IsNullOrWhiteSpace(Inn)) throw new Exception("Указан некорректный ИНН организации (требуется для алкогольного чека).");
            if (string.IsNullOrWhiteSpace(Kpp)) throw new Exception("Указан некорректный КПП организации (требуется для алкогольного чека).");
            if (string.IsNullOrWhiteSpace(Kassa)) throw new Exception("Указан некорректный заводской номер ККМ (требуется для алкогольного чека).");
            if (Shift < 0) throw new Exception("Указан некорректный номер смены (требуется для алкогольного чека).");
            if (Number < 0) throw new Exception("Указан некорректный номер чека (требуется для алкогольного чека).");
            if (ProcessDateTime > DateTime.Now.AddMonths(1)) throw new Exception("Указаны некорректные дата и время (требуется для алкогольного чека).");
        }
        #endregion Защищённые методы класса.

        #region Внешние методы класса.
        /// <summary>
        /// Сформировать xml-документ по чеку.
        /// </summary>
        /// <returns>Xml-документ.</returns>
        public XmlDocument BuildXmlDocument()
        {
            return buildXmlDocument();
        }
        #endregion Внешние методы класса.
        
        #region Абстрактные методы класса.
        /// <summary>
        /// Сформировать xml-документ по чеку.
        /// </summary>
        /// <returns>Xml-документ.</returns>
        protected abstract XmlDocument buildXmlDocument();
        /// <summary>
        /// Получить общую сумму чека.
        /// </summary>
        /// <returns>Общая сумма чека.</returns>
        protected abstract decimal getTotalSum();
        /// <summary>
        /// Получить количество позиций в чеке.
        /// </summary>
        /// <returns>Количество позиций в чеке</returns>
        protected abstract int getCountPositions();
        #endregion Абстрактные методы класса.
    }
}
