using System;
using System.Windows.Forms;
using System.Xml;

namespace Axiom.AlcoTransport.Utility.Quittances
{
    /// <summary>
    /// Обработка версионных уведомлений.
    /// </summary>
    public class VersionQuittance
    {
        #region Внешние статические объекты класса.
        /// <summary>
        /// Путь для сохранения файлов-чеков.
        /// Только чтение.
        /// </summary>
        public static string PathOutRequest
        {
            get { return string.Format("{0}\\EgaisQuittance", Application.StartupPath); }
        }
        #endregion Внешние статические объекты класса.

        #region Защищенные объекты класса.
        /// <summary>
        /// Номер версии.
        /// Только чтение.
        /// </summary>
        protected readonly int versionNumber;
        /// <summary>
        /// FSRAR ID.
        /// Только чтение.
        /// </summary>
        protected readonly string fsrarId;
        #endregion Защищенные объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Номер версии.
        /// Только чтение.
        /// </summary>
        public int VersionNumber
        {
            get { return versionNumber; }
        }
        /// <summary>
        /// FSRAR ID.
        /// Только чтение.
        /// </summary>
        public string FsrarId
        {
            get { return fsrarId; }
        }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected VersionQuittance()
        {
            fsrarId = string.Empty;
            versionNumber = -1;
        }
        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        /// <param name="fsrarId">FSRAR ID.</param>
        /// <param name="versionNumber">Номер версии.</param>
        public VersionQuittance(string fsrarId, int versionNumber) : this()
        {
            this.fsrarId = fsrarId;

            if ((versionNumber < 1) || (versionNumber > 3))
            {
                throw new Exception("Указанный номер версии не поддерживается.");
            }

            this.versionNumber = versionNumber;
        }
        #endregion Конструкторы класса.

        #region Внешние методы класса.
        /// <summary>
        /// Получить XML-документ.
        /// </summary>
        /// <returns>XML-документ</returns>
        public XmlDocument GetXmlDocument()
        {
            return getXmlDocument();
        }
        #endregion Внешние методы класса.

        #region Защищенные методы класса.
        /// <summary>
        /// Получить XML-документ.
        /// </summary>
        /// <returns>XML-документ</returns>
        protected virtual XmlDocument getXmlDocument()
        {
            LittleAddresses addresses = new LittleAddresses();
            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            XmlNode docsNode = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docsNode, "Version", "1.0");
            addAttribute(xml, docsNode, "xmlns:" + addresses.Xsi.Prefix, addresses.Xsi.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Ns.Prefix, addresses.Ns.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Qp.Prefix, addresses.Qp.Uri);

            xml.AppendChild(docsNode);

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docsNode.AppendChild(owner);

            XmlNode FsrarIdNode = xml.CreateElement(addresses.Ns.Prefix, "FSRAR_ID", addresses.Ns.Uri);
            FsrarIdNode.InnerText = fsrarId;
            owner.AppendChild(FsrarIdNode);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docsNode.AppendChild(doc);

            XmlNode infoVersion = xml.CreateElement(addresses.Ns.Prefix, "InfoVersionTTN", addresses.Ns.Uri);
            doc.AppendChild(infoVersion);

            XmlNode clientId = xml.CreateElement(addresses.Qp.Prefix, "ClientId", addresses.Qp.Uri);
            clientId.InnerText = fsrarId;
            infoVersion.AppendChild(clientId);

            XmlNode typeUsed = xml.CreateElement(addresses.Qp.Prefix, "WBTypeUsed", addresses.Qp.Uri);
            typeUsed.InnerText = (versionNumber > 1) ? $"WayBill_v{versionNumber}" : "WayBill";
            infoVersion.AppendChild(typeUsed);

            return xml;
        }
        /// <summary>
        /// Добавить атрибут к указанной XML-ноде.
        /// </summary>
        /// <param name="document"></param>
        /// <param name="node">XML-нода.</param>
        /// <param name="name">Наименование атрибута.</param>
        /// <param name="value">Значение атрибута.</param>
        protected void addAttribute(XmlDocument document, XmlNode node, string name, string value)
        {
            if (document == null) throw new Exception("Object 'xmlDocument' is null.");

            XmlAttribute attribute = document.CreateAttribute(name);
            attribute.Value = value;

            if (node.Attributes == null) throw new Exception("Object 'Node.Attributes' is null.");

            node.Attributes.Append(attribute);
        }
        #endregion Защищенные методы класса.
    }
}
