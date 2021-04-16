using System;
using System.Xml;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Уведомление о регистрации движения. Форма "Б".
    /// </summary>
    [Serializable]
    public class FormBRegInfo : ADocument
    {
        #region Защищённые объекты класса.
        /// <summary>
        /// Идентификатор в системе ФСРАР.
        /// Только чтение.
        /// </summary>
        protected readonly string identity;
        /// <summary>
        /// Идентификатор ТТН (по документу регистрации движения).
        /// Только чтение.
        /// </summary>
        protected readonly string wBRegId;
        /// <summary>
        /// Номер накладной.
        /// </summary>
        [OptionalField]
        protected string wBNumber;
        /// <summary>
        /// Дата накладной.
        /// </summary>
        [OptionalField]
        protected string wBDate;
        /// <summary>
        /// Идентификатор поставщика.
        /// </summary>
        [OptionalField]
        protected string shipperClientRegId;
        /// <summary>
        /// Признак "свободности" документа.
        /// </summary>
        [OptionalField]
        protected bool isFreeDocument;
        #endregion Защищённые объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Идентификатор в системе ФСРАР.
        /// Только чтение.
        /// </summary>
        [DisplayName("Идентификатор накладной"), ReadOnly(true), Browsable(true)]
        public string Identity
        {
            get { return identity; }
        }
        /// <summary>
        /// Идентификатор ТТН (по форме 'Б').
        /// Только чтение.
        /// </summary>
        [DisplayName("Идентификатор ТТН (по форме 'Б')"), ReadOnly(true), Browsable(true)]
        public string WBRegId
        {
            get { return wBRegId; }
        }
        /// <summary>
        /// Номер накладной.
        /// Только чтение.
        /// </summary>
        [DisplayName("Номер накладной"), ReadOnly(true), Browsable(true)]
        public string WBNumber
        {
            get
            {
                try
                {
                    // По идее, сие значение определяется в конструкторе.
                    // Следовательно данный код не работает, как правило.
                    // Оставлен для совместимости старых версий документов.

                    if (string.IsNullOrWhiteSpace(wBNumber))
                    {
                        if (string.IsNullOrWhiteSpace(xmlBody)) return string.Empty;

                        string nameInformNode = (VersionEgais == 1) ? "ns:TTNInformBReg" : "ns:TTNInformF2Reg";

                        XmlDocument xml = new XmlDocument();
                        xml.LoadXml(xmlBody);

                        if (xml[nameInformNode] == null) return string.Empty;

                        wBNumber = getNodeValue("wbr:WBNUMBER", xml[nameInformNode]["wbr:Header"]);
                    }
                    
                    return wBNumber;
                }
                catch (Exception exception)
                {
                    Program.Logger.Error(this, string.Format("Во время получения номера накладной произошла ошибка: '{0}'.", exception));

                    return string.Empty;
                }
            }
        }
        /// <summary>
        /// Дата составления накладной.
        /// Только чтение.
        /// </summary>
        [DisplayName("Дата составления накладной"), ReadOnly(true), Browsable(true)]
        public string WBDate
        {
            get
            {
                try
                {
                    // По идее, сие значение определяется в конструкторе.
                    // Следовательно данный код не работает, как правило.
                    // Оставлен для совместимости старых версий документов.

                    if (string.IsNullOrWhiteSpace(wBDate))
                    {
                        if (string.IsNullOrWhiteSpace(xmlBody)) return string.Empty;

                        string nameInformNode = (VersionEgais == 1) ? "ns:TTNInformBReg" : "ns:TTNInformF2Reg";

                        XmlDocument xml = new XmlDocument();
                        xml.LoadXml(xmlBody);

                        if (xml[nameInformNode] == null) return string.Empty;

                        wBDate = getNodeValue("wbr:WBDate", xml[nameInformNode]["wbr:Header"]);
                    }

                    return wBDate;
                }
                catch (Exception exception)
                {
                    Program.Logger.Error(this, string.Format("Во время получения даты накладной произошла ошибка: '{0}'.", exception));

                    return string.Empty;
                }
            }
        }
        /// <summary>
        /// Идентификатор поставщика.
        /// Только чтение.
        /// </summary>
        [DisplayName("Идентификатор поставщика."), ReadOnly(true), Browsable(false)]
        public string ShipperClientRegId
        {
            get
            {
                try
                {
                    // По идее, сие значение определяется в конструкторе.
                    // Следовательно данный код не работает, как правило.
                    // Оставлен для совместимости старых версий документов.

                    if (string.IsNullOrWhiteSpace(shipperClientRegId))
                    {
                        if (string.IsNullOrWhiteSpace(xmlBody)) return string.Empty;

                        string nameInformNode = (VersionEgais == 1) ? "ns:TTNInformBReg" : "ns:TTNInformF2Reg";

                        XmlDocument xml = new XmlDocument();
                        xml.LoadXml(xmlBody);

                        if (xml[nameInformNode] == null) return string.Empty;
                        if (xml[nameInformNode]["wbr:Header"] == null) return string.Empty;

                        if (VersionEgais == 1)
                        {
                            shipperClientRegId = getNodeValue("oref:ClientRegId", xml[nameInformNode]["wbr:Header"]["wbr:Shipper"]);
                        }
                        else
                        {
                            if (xml[nameInformNode]["wbr:Header"]["wbr:Shipper"] != null)
                            {
                                shipperClientRegId = getNodeValue("oref:ClientRegId", xml[nameInformNode]["wbr:Header"]["wbr:Shipper"].FirstChild);
                            }
                        }
                    }

                    return shipperClientRegId;
                }
                catch (Exception exception)
                {
                    Program.Logger.Error(this, string.Format("Во время получения идентификатора поставщика произошла ошибка: '{0}'.", exception));

                    return string.Empty;
                }
            }
        }
        /// <summary>
        /// Композитный идентификатор.
        /// Только чтение.
        /// </summary>
        public string AuxCompositeId
        {
            get { return createCompositeId(Identity, WBNumber, ShipperClientRegId, WBDate); }
        }
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
        protected FormBRegInfo()
        {
            ReplyId = string.Empty;

            identity = string.Empty;
            wBRegId = string.Empty;

            isFreeDocument = false;
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="node">XML-нода для создания.</param>
        /// <param name="version">Версия документа.</param>
        public FormBRegInfo(XmlNode node, int version) : this()
        {
            xmlBody = node.OuterXml;

            VersionEgais = version;

            if (node["wbr:Header"] == null) throw new Exception("Значение 'wbr:Header' не может быть пустым.");

            identity = getNodeValue("wbr:Identity", node["wbr:Header"]);
            // Значение 'Identity' может отсутствовать.
            // if (string.IsNullOrWhiteSpace(Identity)) throw new Exception("Значение 'wbr:Identity' не может быть пустым.");

            wBRegId = getNodeValue("wbr:WBRegId", node["wbr:Header"]);
            if (string.IsNullOrWhiteSpace(WBRegId)) throw new Exception("Значение 'wbr:WBRegId' не может быть пустым.");

            wBNumber = getNodeValue("wbr:WBNUMBER", node["wbr:Header"]);
            if (string.IsNullOrWhiteSpace(wBNumber)) throw new Exception("Значение 'wbr:WBNumber' не может быть пустым.");

            wBDate = getNodeValue("wbr:WBDate", node["wbr:Header"]);
            if (string.IsNullOrWhiteSpace(wBDate)) throw new Exception("Значение 'wbr:WBDate' не может быть пустым.");

            if (VersionEgais == 1)
            {
                shipperClientRegId = getNodeValue("oref:ClientRegId", node["wbr:Header"]["wbr:Shipper"]);
                if (string.IsNullOrWhiteSpace(shipperClientRegId)) throw new Exception("Значение 'wbr:Shipper\\oref:ClientRegId' не может быть пустым.");
            }
            else
            {
                if (node["wbr:Header"]["wbr:Shipper"] == null) throw new Exception("Значение 'wbr:Shipper' не может быть пустым.");

                shipperClientRegId = getNodeValue("oref:ClientRegId", node["wbr:Header"]["wbr:Shipper"].FirstChild);
                if (string.IsNullOrWhiteSpace(shipperClientRegId)) throw new Exception("Значение 'wbr:Shipper\\oref:ClientRegId' не может быть пустым.");
            }

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

            Program.Logger.Info(this, string.Format("Документу регистрации движения ('{0}') установлен 'несвободный' статус.", Description));
        }
        #endregion Внешние методы класса.

        #region Переопределение базовых методов.
        /// <summary>
        /// Получение описания объекта.
        /// </summary>
        /// <returns>Описание.</returns>
        protected override string GetDescription()
        {
            return string.Format("{0}: AuxCompositeId = '{1}'", GetType().FullName, AuxCompositeId);
        }
        /// <summary>
        /// Построить иерархию данных.
        /// </summary>
        /// <returns>Иерархические данные.</returns>
        protected override TreeData buildTreeData()
        {
            Program.Logger.Info(this, string.Format("Дополнение иерархических данных в документе регистрации движения '{0}'...", Description));

            TreeData tree = base.buildTreeData();

            // ReSharper disable once UnusedVariable
            TreeData compositeId = new TreeData(tree, "Композитный идентификатор", AuxCompositeId);

            Program.Logger.Info(this, "... дополнение иерархических данных успешно завершено.");

            return tree;
        }
        #endregion Переопределение базовых методов.

        #region Внешние статические методы класса.
        /// <summary>
        /// Наименование документа.
        /// </summary>
        public static string NativeName
        {
            get { return "Документ регистрации движения"; }
        }
        #endregion Внешние статические методы класса.
    }
}
