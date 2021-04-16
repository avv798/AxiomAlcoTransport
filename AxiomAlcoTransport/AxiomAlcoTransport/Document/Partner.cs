using System;
using System.Xml;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Организация-партнёр.
    /// </summary>
    [Serializable]
    public class Partner : ADocument
    {
        #region Защищенные объекты класса.
        /// <summary>
        /// Идентификатор организации в системе ФСРАР.
        /// Только чтение.
        /// </summary>
        protected readonly string clientRegId;
        /// <summary>
        /// ИНН.
        /// Только чтение.
        /// </summary>
        protected readonly string inn;
        /// <summary>
        /// КПП.
        /// Только чтение.
        /// </summary>
        protected readonly string kpp;
        /// <summary>
        /// Короткое наименование.
        /// Только чтение.
        /// </summary>
        protected readonly string shortName;
        /// <summary>
        /// Статус.
        /// Только чтение.
        /// </summary>
        protected readonly string status;
        #endregion Защищенные объекты класса.

        #region Внешние объекты класса.

        /// <summary>
        /// Идентификатор организации в системе ФСРАР.
        /// Только чтение.
        /// </summary>
        [DisplayName("Идентификатор в системе ФСРАР"), ReadOnly(true), Browsable(true)]
        public string ClientRegId
        {
            get { return clientRegId; }
        }

        /// <summary>
        /// ИНН.
        /// </summary>
        [DisplayName("ИНН"), ReadOnly(true), Browsable(true)]
        public string Inn
        {
            get { return inn; }
        }

        /// <summary>
        /// КПП.
        /// </summary>
        [DisplayName("КПП"), ReadOnly(true), Browsable(true)]
        public string Kpp
        {
            get { return kpp; }
        }

        /// <summary>
        /// Короткое наименование.
        /// </summary>
        [DisplayName("Короткое наименование"), ReadOnly(true), Browsable(true)]
        public string ShortName
        {
            get { return shortName; }
        }

        /// <summary>
        /// Статус.
        /// </summary>
        [DisplayName("Статус"), ReadOnly(true), Browsable(true)]
        public string Status
        {
            get { return status; }
        }

        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected Partner()
        {
            ReplyId = string.Empty;

            clientRegId = string.Empty;
            inn = string.Empty;
            kpp = string.Empty;
            shortName = string.Empty;
            status = string.Empty;
        }

        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="node">XML-нода для создания.</param>
        /// <param name="version">Версия документа.</param>
        public Partner(XmlNode node, int version) : this()
        {
            VersionEgais = version;

            if (VersionEgais == 1)
            {
                xmlBody = node.OuterXml;

                clientRegId = getNodeValue("oref:ClientRegId", node);
                if (string.IsNullOrWhiteSpace(ClientRegId)) throw new Exception("Значение 'oref:ClientRegId' не может быть пустым.");

                inn = getNodeValue("oref:INN", node);
                kpp = getNodeValue("oref:KPP", node);
                shortName = getNodeValue("oref:ShortName", node);
                if (string.IsNullOrWhiteSpace(shortName)) shortName = getNodeValue("oref:FullName", node);

                status = Program.Language.TranslateReference(getNodeValue("oref:State", node));
            }
            else
            {
                if (node["oref:OrgInfoV2"] == null) throw new Exception("XML-нода 'oref:OrgInfoV2' должна присутствовать в документе версии 'v2'.");
                XmlNode clientNode = node["oref:OrgInfoV2"].FirstChild;

                xmlBody = node["oref:OrgInfoV2"].OuterXml;

                if (clientNode == null) throw new Exception("В XML-ноде 'oref:OrgInfoV2' должна присутствовать одна дочерняя нода - 'UL', 'FL', 'FO', 'TS' (в документе версии 'v2').");

                clientRegId = getNodeValue("oref:ClientRegId", clientNode);
                if (string.IsNullOrWhiteSpace(ClientRegId)) throw new Exception("Значение 'oref:ClientRegId' не может быть пустым.");

                inn = getNodeValue("oref:INN", clientNode);
                kpp = getNodeValue("oref:KPP", clientNode);
                shortName = getNodeValue("oref:ShortName", clientNode);
                if (string.IsNullOrWhiteSpace(shortName)) shortName = getNodeValue("oref:FullName", clientNode);

                status = Program.Language.TranslateReference(getNodeValue("oref:State", node));
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
            return string.Format("{0}: '{1}' (ClientRegId = '{2}'; ИНН = '{3}'; КПП = '{4}')", GetType().FullName, ShortName, ClientRegId, Inn, Kpp);
        }
        #endregion Переопределение базовых методов.

        #region Внешние статические методы класса.
        /// <summary>
        /// Наименование документа.
        /// </summary>
        public static string NativeName
        {
            get { return "Справочник организаций-партнёров"; }
        }
        #endregion Внешние статические методы класса.
    }
}
