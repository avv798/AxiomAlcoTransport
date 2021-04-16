using System;
using System.ComponentModel;
using System.Xml;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Ответные квитки - 'Ticket' и 'CryptoTicket'.
    /// </summary>
    [Serializable]
    public class Ticket : ADocument
    {
        #region Внешние объекты класса.
        /// <summary>
        /// Тип ответа.
        /// </summary>
        [DisplayName("Тип ответа"), ReadOnly(true), Browsable(true)]
        public string TicketType { get; set; }
        /// <summary>
        /// Дата ответа (as is).
        /// </summary>
        [DisplayName("Дата ответа (as is)"), ReadOnly(true), Browsable(true)]
        public string TicketDate { get; set; }
        /// <summary>
        /// Тип документа.
        /// </summary>
        [DisplayName("Тип документа (as is)"), ReadOnly(true), Browsable(true)]
        public string DocType { get; set; }
        /// <summary>
        /// Окончательное решение (без перевода).
        /// </summary>
        [DisplayName("Окончательное решение (без перевода)"), ReadOnly(true), Browsable(true)]
        public string Conclusion { get; set; }
        /// <summary>
        /// Окончательное решение.
        /// Только чтение.
        /// </summary>
        [DisplayName("Окончательное решение"), ReadOnly(true), Browsable(true)]
        public string ConclusionTranslate
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Conclusion))
                {
                    if (string.IsNullOrWhiteSpace(xmlBody)) return string.Empty;

                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(xmlBody);

                    if (xml["ns:Ticket"] == null) return string.Empty;
                    if (xml["ns:Ticket"]["tc:OperationResult"] == null) return string.Empty;

                    parseOperationNode(xml["ns:Ticket"]["tc:OperationResult"]);
                }

                return Program.Language.TranslateReference(Conclusion);
            }
        }
        /// <summary>
        /// Дата решения.
        /// </summary>
        [DisplayName("Дата решения (as is)"), ReadOnly(true), Browsable(true)]
        public string ConclusionDate { get; set; }
        /// <summary>
        /// Комментарий к решению.
        /// </summary>
        [DisplayName("Комментарий к решению"), ReadOnly(true), Browsable(true)]
        public string ConclusionComment { get; set; }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected Ticket()
        {
            ReplyId = string.Empty;

            TicketDate = string.Empty;
            DocType = string.Empty;
            Conclusion = string.Empty;
            ConclusionDate = string.Empty;
            ConclusionComment = string.Empty;
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="node">XML-нода для создания.</param>
        public Ticket(XmlNode node) : this()
        {
            xmlBody = node.OuterXml;

            if (node == null) throw new Exception("Значение 'ns:Ticket' не может быть пустым.");

            TicketDate = TryParseDateTime(getNodeValue("tc:TicketDate", node));
            DocType = getNodeValue("tc:DocType", node);

            if (node["tc:Result"] != null) parseResultNode(node["tc:Result"]); 
            if (node["tc:OperationResult"] != null) parseOperationNode(node["tc:OperationResult"]);
        }
        #endregion Конструкторы класса.

        #region Переопределение базовых методов.
        /// <summary>
        /// Получение описания объекта.
        /// </summary>
        /// <returns>Описание.</returns>
        protected override string GetDescription()
        {
            return string.Format("{0}: '{1}' - '{2}'", GetType().FullName, Conclusion, ConclusionComment);
        }
        #endregion Переопределение базовых методов.

        #region Внешние методы класса.
        /// <summary>
        /// Получить (при наличии) регистрационный номер, который присваивается во время проведения документа на сервере ЕГАИС.
        /// </summary>
        /// <returns></returns>
        public string GetRegId()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(xmlBody)) return string.Empty;

                XmlDocument xml = new XmlDocument();
                xml.LoadXml(xmlBody);

                if (xml["ns:Ticket"] == null) return string.Empty;

                return getNodeValue("tc:RegID", xml["ns:Ticket"]);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("При попытке получить регистрационный номер документа в квитанции сервера произошла ошибка.", exception);

                return string.Empty;
            }
        }
        /// <summary>
        /// Получить (при наличии) наименование операции.
        /// </summary>
        /// <returns></returns>
        public string GetOperationName()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(xmlBody)) return string.Empty;

                XmlDocument xml = new XmlDocument();
                xml.LoadXml(xmlBody);

                if (xml["ns:Ticket"] == null) return string.Empty;
                if (xml["ns:Ticket"]["tc:OperationResult"] == null) return string.Empty;

                return getNodeValue("tc:OperationName", xml["ns:Ticket"]["tc:OperationResult"]);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("При попытке получить наименование операции в квитанции сервера произошла ошибка.", exception);

                return string.Empty;
            }
        }
        #endregion Внешние методы класса.

        #region Внутренние методы класса.
        /// <summary>
        /// Провести анализ результативной ноды квитанции.
        /// </summary>
        /// <param name="node">Нода с результатом.</param>
        private void parseResultNode(XmlNode node)
        {
            Conclusion = getNodeValue("tc:Conclusion", node);
            ConclusionDate = TryParseDateTime(getNodeValue("tc:ConclusionDate", node));
            ConclusionComment = getNodeValue("tc:Comments", node);
        }
        /// <summary>
        /// Провести анализ ещё одной результативной ноды квитанции.
        /// </summary>
        /// <param name="node">Нода с результатом.</param>
        private void parseOperationNode(XmlNode node)
        {
            Conclusion = getNodeValue("tc:OperationResult", node);
            ConclusionDate = TryParseDateTime(getNodeValue("tc:OperationDate", node));
            ConclusionComment = getNodeValue("tc:OperationComment", node);
        }
        #endregion Внутренние методы класса.

        #region Внешние статические методы класса.
        /// <summary>
        /// Наименование документа.
        /// </summary>
        public static string NativeName
        {
            get { return "Ответная квитанция сервера ЕГАИС"; }
        }
        #endregion Внешние статические методы класса.
    }
}
