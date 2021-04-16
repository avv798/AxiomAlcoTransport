using System;
using System.Xml;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Уведомление о постановке на баланс продукции.
    /// </summary>
    [Serializable]
    public class InventoryBRegInfo : ADocument
    {
        #region Защищённые объекты класса.
        /// <summary>
        /// Признак "свободности" документа.
        /// </summary>
        protected bool isFreeDocument;
        #endregion Защищённые объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Признак "свободности" документа.
        /// Только чтение.
        /// </summary>
        public bool IsFreeDocument
        {
            get { return isFreeDocument; }
        }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected InventoryBRegInfo()
        {
            ReplyId = string.Empty;

            isFreeDocument = false;
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="node">XML-нода для создания.</param>
        public InventoryBRegInfo(XmlNode node) : this()
        {
            xmlBody = node.OuterXml;

            isFreeDocument = true;
        }
        #endregion Конструкторы класса.

        #region Внешние методы класса.
        /// <summary>
        /// Установить документу "несвободный" статус.
        /// </summary>
        public void SetNonFreeStatus()
        {
            isFreeDocument = false;

            Program.Logger.Info(this, string.Format("Уведомлению о постановке на баланс продукции ('{0}') установлен 'несвободный' статус.", Description));
        }
        #endregion Внешние методы класса.

        #region Переопределение базовых методов.
        /// <summary>
        /// Получение описания объекта.
        /// </summary>
        /// <returns>Описание.</returns>
        protected override string GetDescription()
        {
            return string.Format("{0}: ReplyId = '{1}'", GetType().FullName, ReplyId);
        }
        #endregion Переопределение базовых методов.

        #region Внешние статические методы класса.
        /// <summary>
        /// Наименование документа.
        /// </summary>
        public static string NativeName
        {
            get { return "Уведомление о постановке на баланс продукции"; }
        }
        #endregion Внешние статические методы класса.
    }
}
