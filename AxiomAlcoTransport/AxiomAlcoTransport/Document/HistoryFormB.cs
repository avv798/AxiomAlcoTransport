using System;
using System.Xml;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Движение по форме 'Б'.
    /// </summary>
    [Serializable]
    public class HistoryFormB : ADocument
    {
        #region Защищенные объекты класса.
        /// <summary>
        /// Регистрационный номер справки 'Б'.
        /// Только чтение.
        /// </summary>
        protected readonly string informBRegId;
        #endregion Защищенные объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Регистрационный номер справки 'Б'.
        /// Только чтение.
        /// </summary>
        [DisplayName("Регистрационный номер справки 'Б'"), ReadOnly(true), Browsable(true)]
        public string InformBRegId
        {
            get { return informBRegId; }
        }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected HistoryFormB()
        {
            ReplyId = string.Empty;

            informBRegId = string.Empty;
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="node">XML-нода для создания.</param>
        /// <param name="version">Версия документа.</param>
        public HistoryFormB(XmlNode node, int version) : this()
        {
            xmlBody = node.OuterXml;

            VersionEgais = version;

            if (VersionEgais == 1)
            {
                informBRegId = getNodeValue("hf:InformBRegId", node);
                if (string.IsNullOrWhiteSpace(InformBRegId)) throw new Exception("Значение 'hf:InformBRegId' не может быть пустым.");
            }
            else
            {
                informBRegId = getNodeValue("hf:InformF2RegId", node);
                if (string.IsNullOrWhiteSpace(InformBRegId)) throw new Exception("Значение 'hf:InformF2RegId' не может быть пустым.");
            }
        }
        #endregion Конструкторы класса.

        #region Переопределение базовых методов.
        /// <summary>
        /// Получение описания объекта.
        /// </summary>
        /// <returns>Описание.</returns>
        protected override string GetDescription()
        {
            return string.Format("{0}: InformBRegId = {1} ({2})", GetType().FullName, InformBRegId, CreateDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        #endregion Переопределение базовых методов.

        #region Внешние статические методы класса.
        /// <summary>
        /// Наименование документа.
        /// </summary>
        public static string NativeName
        {
            get { return "Движение по форме 'Б'"; }
        }
        #endregion Внешние статические методы класса.
    }
}
