using System;
using System.Xml;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Список необработанных накладных.
    /// </summary>
    [Serializable]
    public class NotAnswer : ADocument
    {
        #region Защищенные объекты класса.
        /// <summary>
        /// Дата (as is), на которую получен документ.
        /// Только чтение.
        /// </summary>
        [OptionalField]
        protected readonly string replyDate;
        #endregion Защищенные объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Дата (as is), на которую получен документ.
        /// Только чтение.
        /// </summary>
        [DisplayName("Дата (as is), на которую получен документ"), ReadOnly(true), Browsable(true)]
        public string ReplyDate
        {
            get { return replyDate; }
        }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected NotAnswer()
        {
            ReplyId = string.Empty;

            replyDate = string.Empty;
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="node">XML-нода для создания.</param>
        public NotAnswer(XmlNode node) : this()
        {
            xmlBody = node.OuterXml;

            replyDate = TryParseDateTime(getNodeValue("ttn:ReplyDate", node));
        }
        #endregion Конструкторы класса.

        #region Переопределение базовых методов.
        /// <summary>
        /// Получение описания объекта.
        /// </summary>
        /// <returns>Описание.</returns>
        protected override string GetDescription()
        {
            return string.Format("{0}: список необработанных накладных от {1}.", GetType().FullName, CreateDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        #endregion Переопределение базовых методов.

        #region Внешние статические методы класса.
        /// <summary>
        /// Наименование документа.
        /// </summary>
        public static string NativeName
        {
            get { return "Список необработанных накладных"; }
        }
        #endregion Внешние статические методы класса.
    }
}
