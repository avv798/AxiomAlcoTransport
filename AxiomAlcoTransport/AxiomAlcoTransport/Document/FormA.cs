using System;
using System.Xml;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Форма 'А'.
    /// </summary>
    [Serializable]
    public class FormA : ADocument
    {
        #region Защищенные объекты класса.
        /// <summary>
        /// Регистрационный номер справки 'А'.
        /// Только чтение.
        /// </summary>
        protected readonly string informARegId;
        /// <summary>
        /// Номер накладной.
        /// Только чтение.
        /// </summary>
        protected readonly string ttnNumber;
        #endregion Защищенные объекты класса.

        #region Внешние объекты класса.

        /// <summary>
        /// Регистрационный номер справки 'А'.
        /// Только чтение.
        /// </summary>
        [DisplayName("Регистрационный номер справки 'А'"), ReadOnly(true), Browsable(true)]
        public string InformARegId
        {
            get { return informARegId; }
        }

        /// <summary>
        /// Номер накладной.
        /// Только чтение.
        /// </summary>
        [DisplayName("Номер документа"), ReadOnly(true), Browsable(true)]
        public string TtnNumber
        {
            get { return ttnNumber; }
        }

        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected FormA()
        {
            ReplyId = string.Empty;

            informARegId = string.Empty;
            ttnNumber = string.Empty;
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="node">XML-нода для создания.</param>
        /// <param name="version">Версия документа.</param>
        public FormA(XmlNode node, int version) : this()
        {
            xmlBody = node.OuterXml;

            VersionEgais = version;

            if (VersionEgais == 1)
            {
                informARegId = getNodeValue("rfa:InformARegId", node);
                if (string.IsNullOrWhiteSpace(InformARegId)) throw new Exception("Значение 'rfa:InformARegId' не может быть пустым.");
                ttnNumber = getNodeValue("rfa:TTNNumber", node);
                if (string.IsNullOrWhiteSpace(TtnNumber)) throw new Exception("Значение 'rfa:TTNNumber' не может быть пустым.");
            }
            else
            {
                informARegId = getNodeValue("rfa:InformF1RegId", node);
                if (string.IsNullOrWhiteSpace(InformARegId)) throw new Exception("Значение 'rfa:InformF1RegId' не может быть пустым.");
                ttnNumber = getNodeValue("rfa:OriginalDocNumber", node);
                if (string.IsNullOrWhiteSpace(TtnNumber)) throw new Exception("Значение 'rfa:OriginalDocNumber' не может быть пустым.");
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
            return string.Format("{0}: InformARegId = {1}; TtnNumber = {2}", GetType().FullName, InformARegId, TtnNumber);
        }
        #endregion Переопределение базовых методов.

        #region Внешние статические методы класса.
        /// <summary>
        /// Наименование документа.
        /// </summary>
        public static string NativeName
        {
            get { return "Форма типа 'А' (№1)"; }
        }
        #endregion Внешние статические методы класса.
    }
}
