using System;
using System.Xml;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Форма 'Б'.
    /// </summary>
    [Serializable]
    public class FormB : ADocument
    {
        #region Защищенные объекты класса.
        /// <summary>
        /// Регистрационный номер справки 'Б'.
        /// Только чтение.
        /// </summary>
        protected readonly string informBRegId;
        /// <summary>
        /// Номер накладной.
        /// Только чтение.
        /// </summary>
        protected readonly string ttnNumber;
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

        /// <summary>
        /// Номер накладной.
        /// Только чтение.
        /// </summary>
        [DisplayName("Номер накладной"), ReadOnly(true), Browsable(true)]
        public string TtnNumber
        {
            get { return ttnNumber; }
        }

        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected FormB()
        {
            ReplyId = string.Empty;

            informBRegId = string.Empty;
            ttnNumber = string.Empty;
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="node">XML-нода для создания.</param>
        /// <param name="version">Версия документа.</param>
        public FormB(XmlNode node, int version) : this()
        {
            xmlBody = node.OuterXml;

            VersionEgais = version;

            if (VersionEgais == 1)
            {
                informBRegId = getNodeValue("rfb:InformBRegId", node);
                if (string.IsNullOrWhiteSpace(InformBRegId)) throw new Exception("Значение 'rfb:InformBRegId' не может быть пустым.");
                ttnNumber = getNodeValue("rfb:TTNNumber", node);
                if (string.IsNullOrWhiteSpace(TtnNumber)) throw new Exception("Значение 'rfb:TTNNumber' не может быть пустым.");
            }
            else
            {
                informBRegId = getNodeValue("rfb:InformF2RegId", node);
                if (string.IsNullOrWhiteSpace(InformBRegId)) throw new Exception("Значение 'rfb:InformF2RegId' не может быть пустым.");
                ttnNumber = getNodeValue("rfb:TTNNumber", node);
                if (string.IsNullOrWhiteSpace(TtnNumber)) throw new Exception("Значение 'rfb:TTNNumber' не может быть пустым.");
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
            return string.Format("{0}: InformBRegId = {1}; TtnNumber = {2}", GetType().FullName, InformBRegId, TtnNumber);
        }
        #endregion Переопределение базовых методов.

        #region Внешние статические методы класса.
        /// <summary>
        /// Наименование документа.
        /// </summary>
        public static string NativeName
        {
            get { return "Форма типа 'Б' (№2)"; }
        }
        #endregion Внешние статические методы класса.

    }
}
