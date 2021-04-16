using System;
using System.Runtime.Serialization;
using System.Xml;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Ответ по акту расхождения.
    /// </summary>
    [Serializable]
    public class WaybillTicket : ADocument
    {
        #region Защищенные объекты класса.
        /// <summary>
        /// Результат.
        /// Только чтение.
        /// </summary>
        protected readonly string isConfirm;
        /// <summary>
        /// Результат.
        /// Только чтение.
        /// </summary>
        [OptionalField]
        protected readonly string isConfirmOriginal;
        /// <summary>
        /// Дата (as is), на которую получен документ.
        /// Только чтение.
        /// </summary>
        protected readonly string ticketDate;
        /// <summary>
        /// Номер ответа.
        /// Только чтение.
        /// </summary>
        protected readonly string ticketNumber;
        /// <summary>
        /// Идентификатор (ЕГАИС) накладной.
        /// Только чтение.
        /// </summary>
        protected readonly string wBRegId;
        /// <summary>
        /// Комментарий к ответу.
        /// Только чтение.
        /// </summary>
        protected readonly string note;
        #endregion Защищенные объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Результат.
        /// Только чтение.
        /// </summary>
        [DisplayName("Результат"), ReadOnly(true), Browsable(true)]
        public string IsConfirm
        {
            get { return isConfirm; }
        }
        /// <summary>
        /// Результат.
        /// Только чтение.
        /// </summary>
        [DisplayName("Результат (оригинал)"), ReadOnly(true), Browsable(false)]
        public string IsConfirmOriginal
        {
            get { return isConfirmOriginal; }
        }
        /// <summary>
        /// Дата (as is), на которую получен документ.
        /// Только чтение.
        /// </summary>
        [DisplayName("Дата (as is), на которую получен документ"), ReadOnly(true), Browsable(true)]
        public string TicketDate
        {
            get { return ticketDate; }
        }
        /// <summary>
        /// Номер ответа.
        /// Только чтение.
        /// </summary>
        [DisplayName("Номер ответа"), ReadOnly(true), Browsable(true)]
        public string TicketNumber
        {
            get { return ticketNumber; }
        }
        /// <summary>
        /// Идентификатор (ЕГАИС) накладной.
        /// Только чтение.
        /// </summary>
        [DisplayName("Идентификатор (ЕГАИС) накладной"), ReadOnly(true), Browsable(true)]
        public string WBRegId
        {
            get { return wBRegId; }
        }
        /// <summary>
        /// Комментарий к ответу.
        /// Только чтение.
        /// </summary>
        [DisplayName("Комментарий к ответу"), ReadOnly(true), Browsable(true)]
        public string Note
        {
            get { return note; }
        }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected WaybillTicket()
        {
            ReplyId = string.Empty;

            isConfirm = string.Empty;
            ticketDate = string.Empty;
            ticketNumber = string.Empty;
            wBRegId = string.Empty;
            note = string.Empty;
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="node">XML-нода для создания.</param>
        public WaybillTicket(XmlNode node) : this()
        {
            xmlBody = node.OuterXml;

            if (node["wt:Header"] == null) throw new Exception("Нода 'wt:Header' не может отсутствовать в ответе по акту расхождения.");

            isConfirm = Program.Language.TranslateReference(TryParseDateTime(getNodeValue("wt:IsConfirm", node["wt:Header"])));
            isConfirmOriginal = TryParseDateTime(getNodeValue("wt:IsConfirm", node["wt:Header"]));
            ticketDate = TryParseDateTime(getNodeValue("wt:TicketDate", node["wt:Header"]));
            ticketNumber = TryParseDateTime(getNodeValue("wt:TicketNumber", node["wt:Header"]));
            wBRegId = TryParseDateTime(getNodeValue("wt:WBRegId", node["wt:Header"]));
            note = TryParseDateTime(getNodeValue("wt:Note", node["wt:Header"]));
        }
        #endregion Конструкторы класса.

        #region Переопределение базовых методов.
        /// <summary>
        /// Получение описания объекта.
        /// </summary>
        /// <returns>Описание.</returns>
        protected override string GetDescription()
        {
            return string.Format("{0}: ответ по акту расхождения на накладную с номером {1}.", GetType().FullName, WBRegId);
        }
        #endregion Переопределение базовых методов.

        #region Внешние статические методы класса.
        /// <summary>
        /// Наименование документа.
        /// </summary>
        public static string NativeName
        {
            get { return "Ответ по акту расхождения"; }
        }
        #endregion Внешние статические методы класса.
    }
}
