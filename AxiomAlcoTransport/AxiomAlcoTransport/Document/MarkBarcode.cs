using System;
using System.Xml;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Штрих-коды марок (АМ и ФСМ).
    /// </summary>
    [Serializable]
    public class MarkBarcode : ADocument
    {
        #region Защищенные объекты класса.
        /// <summary>
        /// Дата (as is) получения списка штрих-кодов.
        /// Только чтение.
        /// </summary>
        protected readonly string barcodesDate;
        /// <summary>
        /// Номер запроса.
        /// Только чтение.
        /// </summary>
        protected readonly string queryNumber;
        #endregion Защищенные объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Дата (as is) получения списка штрих-кодов.
        /// Только чтение.
        /// </summary>
        [DisplayName("Дата (as is) получения списка штрих-кодов"), ReadOnly(true), Browsable(true)]
        public string BarcodesDate
        {
            get { return barcodesDate; }
        }
        /// <summary>
        /// Номер запроса.
        /// Только чтение.
        /// </summary>
        [DisplayName("Номер запроса"), ReadOnly(true), Browsable(true)]
        public string QueryNumber
        {
            get { return queryNumber; }
        }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected MarkBarcode()
        {
            ReplyId = string.Empty;
            barcodesDate = string.Empty;
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="node">XML-нода для создания.</param>
        public MarkBarcode(XmlNode node) : this()
        {
            xmlBody = node.OuterXml;

            queryNumber = getNodeValue("bk:QueryNumber", node);
            barcodesDate = TryParseDateTime(getNodeValue("bk:Date", node));
        }
        #endregion Конструкторы класса.

        #region Переопределение базовых методов.
        /// <summary>
        /// Получение описания объекта.
        /// </summary>
        /// <returns>Описание.</returns>
        protected override string GetDescription()
        {
            return string.Format("{0}: BarcodesDate = {1}, QueryNumber = {2}", GetType().FullName, BarcodesDate, QueryNumber);
        }
        #endregion Переопределение базовых методов.

        #region Внешние статические методы класса.
        /// <summary>
        /// Наименование документа.
        /// </summary>
        public static string NativeName
        {
            get { return "Список штрих-кодов акцизных марок"; }
        }
        #endregion Внешние статические методы класса.
    }
}
