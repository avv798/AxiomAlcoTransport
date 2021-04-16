using System;
using System.Xml;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Подтверждение запроса на распроведение накладной.
    /// </summary>
    [Serializable]
    public class WaybillRepealConfirm : ADocument
    {
        #region Защищенные объекты класса.
        /// <summary>
        /// Идентификатор ТТН (по документу регистрации движения).
        /// </summary>
        protected readonly string wBRegId;
        /// <summary>
        /// Номер запроса.
        /// </summary>
        protected readonly string confirmNumber;
        /// <summary>
        /// Дата составления запроса.
        /// </summary>
        protected readonly string confirmDate;
        /// <summary>
        /// Комментарий.
        /// </summary>
        protected readonly string note;
        /// <summary>
        /// Вердикт.
        /// </summary>
        protected readonly string isConfirm;
        #endregion Защищенные объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Идентификатор ТТН (по документу регистрации движения).
        /// Только чтение.
        /// </summary>
        [DisplayName("Идентификатор ТТН (по документу регистрации движения)"), ReadOnly(true), Browsable(true)]
        public string WBRegId
        {
            get { return wBRegId; }
        }
        /// <summary>
        /// Номер запроса.
        /// </summary>
        [DisplayName("Номер запроса"), ReadOnly(true), Browsable(true)]
        public string ConfirmNumber
        {
            get { return confirmNumber; }
        }
        /// <summary>
        /// Дата составления запроса.
        /// </summary>
        [DisplayName("Дата составления запроса"), ReadOnly(true), Browsable(true)]
        public string ConfirmDate
        {
            get { return confirmDate; }
        }
        /// <summary>
        /// Комментарий.
        /// </summary>
        [DisplayName("Комментарий"), ReadOnly(true), Browsable(true)]
        public string Note
        {
            get { return note; }
        }
        /// <summary>
        /// Вердикт.
        /// </summary>
        [DisplayName("Вердикт"), ReadOnly(true), Browsable(true)]
        public string IsConfirm
        {
            get { return isConfirm; }
        }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected WaybillRepealConfirm()
        {
            ReplyId = string.Empty;

            isConfirm = string.Empty;
            note = string.Empty;
            wBRegId = string.Empty;
            confirmNumber = string.Empty;
            confirmDate = string.Empty;
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="node">XML-нода для создания.</param>
        public WaybillRepealConfirm(XmlNode node) : this()
        {
            xmlBody = node.OuterXml;

            VersionEgais = 2;

            if (node == null) throw new Exception("Значение 'ns:ConfirmRepealWB' не может быть пустым.");
            if (node["wt:Header"] == null) throw new Exception("Значение 'ns:ConfirmRepealWB/wt:Header' не может быть пустым.");

            isConfirm = getNodeValue("wt:IsConfirm", node["wt:Header"]);
            confirmNumber = getNodeValue("wt:ConfirmNumber", node["wt:Header"]);
            confirmDate = TryParseDateTime(getNodeValue("wt:ConfirmDate", node["wt:Header"]));
            wBRegId = getNodeValue("wt:WBRegId", node["wt:Header"]);
            note = getNodeValue("wt:Note", node["wt:Header"]);
        }
        #endregion Конструкторы класса.

        #region Переопределение базовых методов.
        /// <summary>
        /// Получение описания объекта.
        /// </summary>
        /// <returns>Описание.</returns>
        protected override string GetDescription()
        {
            return string.Format("{0}: WBRegId = '{1}' ('{2}')", GetType().FullName, wBRegId, IsConfirm);
        }
        #endregion Переопределение базовых методов.

        #region Внешние статические методы класса.
        /// <summary>
        /// Наименование документа.
        /// </summary>
        public static string NativeName
        {
            get { return "Подтверждение запроса на распроведение накладной"; }
        }
        #endregion Внешние статические методы класса.
    }
}
