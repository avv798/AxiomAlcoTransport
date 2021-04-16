using System;
using System.Xml;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// ТТН.
    /// Абстрактный класс.
    /// </summary>
    [Serializable]
    public abstract class AWaybill : ADocument
    {
        #region Защищенные объекты класса.
        /// <summary>
        /// Идентификатор в системе ФСРАР.
        /// </summary>
        protected string identity;
        /// <summary>
        /// Номер.
        /// </summary>
        protected string number;
        /// <summary>
        /// Дата составления.
        /// </summary>
        protected string date;
        /// <summary>
        /// Дата отгрузки.
        /// </summary>
        protected string shippingDate;
        /// <summary>
        /// Отправитель.
        /// </summary>
        protected string shipperName;
        /// <summary>
        /// Получатель.
        /// </summary>
        protected string consigneeName;
        /// <summary>
        /// Уведомление о регистрации движения. Форма "Б".
        /// Оригинальное тело документа.
        /// </summary>
        protected string xmlFormBRegInfo;
        /// <summary>
        /// Идентификатор ТТН (по документу регистрации движения).
        /// </summary>
        protected string wBRegId;
        /// <summary>
        /// Последнее изменение.
        /// </summary>
        protected string lastChange;
        /// <summary>
        /// Идентификатор поставщика.
        /// </summary>
        [OptionalField]
        protected string auxShipperClientRegId;
        /// <summary>
        /// Список пространств имен с префиксами
        /// </summary>
        [OptionalField]
        private readonly Dictionary<string, string> nameSpaces;
        #endregion Защищенные объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Идентификатор организации в системе ФСРАР.
        /// </summary>
        [DisplayName("Идентификатор накладной"), ReadOnly(true), Browsable(true)]
        public string Identity
        {
            get { return identity; }
            set { identity = value; }
        }
        /// <summary>
        /// Номер.
        /// </summary>
        [DisplayName("Номер"), ReadOnly(true), Browsable(true)]
        public string Number
        {
            get { return number; }
            set { number = value; }
        }
        /// <summary>
        /// Дата составления.
        /// </summary>
        [DisplayName("Дата составления"), ReadOnly(true), Browsable(true)]
        public string Date
        {
            get { return date; }
            protected set { date = value; }
        }
        /// <summary>
        /// Дата отгрузки.
        /// </summary>
        [DisplayName("Дата отгрузки"), ReadOnly(true), Browsable(true)]
        public string ShippingDate
        {
            get { return shippingDate; }
            set { shippingDate = value; }
        }
        /// <summary>
        /// Отправитель.
        /// </summary>
        [DisplayName("Отправитель"), ReadOnly(true), Browsable(true)]
        public string ShipperName
        {
            get { return shipperName; }
            set { shipperName = value; }
        }
        /// <summary>
        /// Получатель.
        /// </summary>
        [DisplayName("Получатель"), ReadOnly(true), Browsable(true)]
        public string ConsigneeName
        {
            get { return consigneeName; }
            set { consigneeName = value; }
        }
        /// <summary>
        /// Направление накладной (входящая или исходящая).
        /// Только чтение.
        /// </summary>
        [DisplayName("Направление"), ReadOnly(true), Browsable(true)]
        public string Direct
        {
            get { return getDirect(); }
        }
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
        /// Последнее изменение.
        /// Только чтение.
        /// </summary>
        [DisplayName("Последнее изменение"), ReadOnly(true), Browsable(false)]
        public string LastChange
        {
            get { return lastChange; }
        }
        /// <summary>
        /// Идентификатор поставщика.
        /// Только чтение.
        /// </summary>
        [DisplayName("Идентификатор поставщика."), ReadOnly(true), Browsable(false)]
        protected virtual string AuxShipperClientRegId
        {
            get
            {
                try
                {
                    // По идее, сие значение определяется в конструкторе.
                    // Следовательно данный код не работает, как правило.
                    // Оставлен для совместимости старых версий документов.

                    if (string.IsNullOrWhiteSpace(auxShipperClientRegId))
                    {
                        if (string.IsNullOrWhiteSpace(xmlBody)) return string.Empty;

                        string nameWaybillNode = (VersionEgais == 1) ? "ns:WayBill" : "ns:WayBill_v2";

                        XmlDocument xml = new XmlDocument();
                        xml.LoadXml(xmlBody);

                        if (xml[nameWaybillNode] == null) return string.Empty;
                        if (xml[nameWaybillNode]["wb:Header"] == null) return string.Empty;

                        if (VersionEgais == 1)
                        {
                            auxShipperClientRegId = getNodeValue("oref:ClientRegId", xml[nameWaybillNode]["wb:Header"]["wb:Shipper"]);
                        }
                        else
                        {
                            if (xml[nameWaybillNode]["wb:Header"]["wb:Shipper"] != null)
                            {
                                auxShipperClientRegId = getNodeValue("oref:ClientRegId", xml[nameWaybillNode]["wb:Header"]["wb:Shipper"].FirstChild);
                            }
                        }
                    }

                    return auxShipperClientRegId;
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
        public virtual string AuxCompositeId
        {
            get { return createCompositeId(Identity, Number, AuxShipperClientRegId, Date); }
        }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected AWaybill()
        {
            treeData = null;

            identity = string.Empty;
            number = string.Empty;
            date = string.Empty;
            shippingDate = string.Empty;
            shipperName = string.Empty;
            consigneeName = string.Empty;
            lastChange = string.Empty;
            xmlFormBRegInfo = string.Empty;
            wBRegId = string.Empty;

            ReplyId = string.Empty;
        }

        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="node">XML-нода для создания.</param>
        /// <param name="version">Версия документа.</param>
        /// <param name="nameSpaces"></param>
        protected AWaybill(XmlNode node, int version, Dictionary<string,string> nameSpaces ) : this()
        {
            VersionEgais = version;
            this.nameSpaces = nameSpaces;
            if (node == null) throw new Exception("Значение базовой ноды документа не может быть пустым.");
            string wb;
            if (nameSpaces == null||!nameSpaces.TryGetValue(@"http://fsrar.ru/WEGAIS/TTNSingle_v2".ToLower(), out wb))
                wb = "wb";
            string oref;
            if (nameSpaces == null||!nameSpaces.TryGetValue(@"http://fsrar.ru/WEGAIS/ClientRef_v2".ToLower(), out oref))
                oref = "oref";
            identity = getNodeValue($"{wb}:Identity", node);
            // Значение 'Identity' может отсутствовать.
            // if (string.IsNullOrWhiteSpace(Identity)) throw new Exception("Значение 'wb:Identity' не может быть пустым.");

            if (node[$"{wb}:Header"] == null) throw new Exception("Значение 'wb:Header' не может быть пустым.");

            date = TryParseDateTime(getNodeValue($"{wb}:Date", node[$"{wb}:Header"]));
            shippingDate = getNodeValue($"{wb}:ShippingDate", node[$"{wb}:Header"]);

            number = getNodeValue($"{wb}:NUMBER", node[$"{wb}:Header"]);
            if (string.IsNullOrWhiteSpace(Number)) throw new Exception("Значение 'wb:NUMBER' не может быть пустым.");

            if (VersionEgais == 1)
            {
                if (node[$"{wb}:Header"][$"{wb}:Shipper"] == null) throw new Exception("Значение 'wb:Shipper' не может быть пустым.");
                shipperName = getNodeValue($"{oref}:ShortName", node[$"{wb}:Header"][$"{wb}:Shipper"]);
                if (string.IsNullOrWhiteSpace(shipperName)) shipperName = getNodeValue($"{oref}:FullName", node[$"{wb}:Header"][$"{wb}:Shipper"]);

                if (node[$"{wb}:Header"][$"{wb}:Consignee"] == null) throw new Exception("Значение 'wb:Consignee' не может быть пустым.");
                consigneeName = getNodeValue($"{oref}:ShortName", node[$"{wb}:Header"][$"{wb}:Consignee"]);
            }
            else
            {
                if (node[$"{wb}:Header"][$"{wb}:Shipper"] == null) throw new Exception("Значение 'wb:Shipper' не может быть пустым.");
                if (node[$"{wb}:Header"][$"{wb}:Shipper"].FirstChild == null) throw new Exception("Дочернее значение ноды 'wb:Shipper' не может быть пустым.");

                shipperName = getNodeValue($"{oref}:ShortName", node[$"{wb}:Header"][$"{wb}:Shipper"].FirstChild);
                if (string.IsNullOrWhiteSpace(shipperName)) shipperName = getNodeValue($"{oref}:FullName", node[$"{wb}:Header"][$"{wb}:Shipper"].FirstChild);

                auxShipperClientRegId = getNodeValue($"{oref}:ClientRegId", node[$"{wb}:Header"][$"{wb}:Shipper"].FirstChild);
                if (string.IsNullOrWhiteSpace(shipperName)) throw new Exception($"Значение 'wb:Header\\wb:Shipper\\[child]\\{oref}:ClientRegId' не может быть пустым.");

                if (node[$"{wb}:Header"][$"{wb}:Consignee"] == null) throw new Exception("Значение 'wb:Consignee' не может быть пустым.");
                if (node[$"{wb}:Header"][$"{wb}:Consignee"].FirstChild == null) throw new Exception("Дочернее значение ноды 'wb:Consignee' не может быть пустым.");
                consigneeName = getNodeValue($"{oref}:ShortName", node[$"{wb}:Header"][$"{wb}:Consignee"].FirstChild);
            }

            xmlBody = node.OuterXml;
        }

      
        #endregion Конструкторы класса.

        #region Абстрактные методы класса.
        /// <summary>
        /// Получить направление накладной (входящая или исходящая).
        /// </summary>
        /// <returns>Направление накладной (входящая или исходящая)</returns>
        protected abstract string getDirect();
        #endregion Абстрактные методы класса.

        #region Переопределение базовых методов.
        /// <summary>
        /// Получение описания объекта.
        /// </summary>
        /// <returns>Описание.</returns>
        protected override string GetDescription()
        {
            return string.Format("{0}: '{1}' (AuxCompositeId = '{2}', WBRegId = '{3}', '{4}')", GetType().FullName, Date, AuxCompositeId, WBRegId, Direct);
        }
        #endregion Переопределение базовых методов.

        #region Внешние методы класса.
        /// <summary>
        /// Получить позиции накладной.
        /// </summary>
        /// <returns>Список позиций.</returns>
        public List<Position> GetPositions()
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка получить позиции накладной '{0}'...", Description));

                List<Position> positions = getPositions();

                Program.Logger.Info(this, string.Format("... получение позиций накладной (в количестве {0} шт.) успешно завершено.", positions.Count));

                return positions;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время получения позиций произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Дополнить накладную данными.
        /// </summary>
        /// <param name="form">Документ о регистрации движения</param>
        public void AddData(FormBRegInfo form)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка дополнить накладную '{0}' данными документа о регистрации движения...", Description));

                addData(form);

                Program.Logger.Info(this, "... попытка дополнить накладную данными документа о регистрации движения успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время дополнения накладной данными документа о регистрации движения произошла ошибка.", exception);

                throw;
            }
        }
        #endregion Внешние методы класса.

        #region Защищенные методы класса.
        /// <summary>
        /// Получить позиции накладной.
        /// </summary>
        /// <returns>Список позиций.</returns>
        protected virtual List<Position> getPositions()
        {
            List<Position> positions = new List<Position>();
            
            if (string.IsNullOrWhiteSpace(xmlBody)) return positions;
            string ns, wb, oref, pref, ce, wbr;
            if (nameSpaces == null || !nameSpaces.TryGetValue(Addresses.NsUrl.ToLower(), out ns))
                ns = "ns";
            if (nameSpaces == null || !nameSpaces.TryGetValue(Addresses.Wb2Url.ToLower(), out wb))
                wb = "wb";
            if (nameSpaces == null || !nameSpaces.TryGetValue(Addresses.Oref2Url.ToLower(), out oref))
                oref = "oref";
            if (nameSpaces == null || !nameSpaces.TryGetValue(Addresses.Pref2Url.ToLower().ToLower(), out pref))
                pref = "pref";
            if (nameSpaces == null || !nameSpaces.TryGetValue(Addresses.Ce3Url.ToLower(), out ce))
                ce = "ce";
            if (nameSpaces == null || !nameSpaces.TryGetValue(Addresses.WbrUrl.ToLower(), out wbr))
                wbr = "wbr";
            #region Заполнение...
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(xmlBody);
             
                string nameWaybillNode = (VersionEgais == 1) ? $"{ns}:WayBill" : $"{ns}:WayBill_v{VersionEgais}";

                if (xml[nameWaybillNode] == null) throw new Exception(string.Format("Значение '{0}' не может быть пустым.", nameWaybillNode));
                if (xml[nameWaybillNode][$"{wb}:Content"] == null) throw new Exception("Значение 'wb:Content' не может быть пустым.");

                XmlNode node = xml[nameWaybillNode][$"{wb}:Content"];

                foreach (XmlNode item in node.ChildNodes)
                {
                    string nameInformA;
                    string nameInformB; 
                    string nameInformBItem = (VersionEgais == 1) ? $"{pref}:InformBItem" : $"{pref}:InformF2Item";
                    string nameBRegId;

                    switch (VersionEgais)
                    {
                        case 1:
                            nameInformA = $"{wb}:InformA";
                            nameInformB = $"{wb}:InformB";
                            nameBRegId = $"{pref}:BRegId";
                            break;
                        case 2:
                            nameInformA = $"{wb}:InformF1";
                            nameInformB = $"{wb}:InformF2";
                            nameBRegId = $"{pref}:F2RegId";
                            break;
                        default:
                            nameInformA = $"{wb}:FARegId";
                            nameInformB = $"{wb}:InformF2";
                  
                            nameBRegId = $"{ce}:F2RegId";
                            break;
                    }

                    if (item[$"{wb}:Product"] == null) throw new Exception("Значение 'wb:Product' не может быть пустым.");
                    if (item[nameInformB] == null) throw new Exception(string.Format("Значение '{0}' не может быть пустым.", nameInformB));

                    // Значение 'pref:Producer' может отсутствовать.
                    // if (item[$"{wb}:Product"][$"{pref}:Producer"] == null) throw new Exception("Значение 'pref:Producer' не может быть пустым.");

                    Position position = new Position
                                            {
                                                WaybillInformation = string.Format("Накладная с номером '{0}' ('{1}') от '{2}' ('{3}')", Number, Direct, ShipperName, ShippingDate),
                                                Identity = getNodeValue($"{wb}:Identity", item),
                                                InformBRegId = string.Empty,
                                                Quantity = convertToDecimal(getNodeValue($"{wb}:Quantity", item)),
                                                RealQuantity = convertToDecimal(getNodeValue($"{wb}:Quantity", item)),
                                                Price = convertToDecimal(getNodeValue($"{wb}:Price", item)),
                                                AlcoCode = getNodeValue($"{pref}:AlcCode", item[$"{wb}:Product"]),
                                                FullName = getNodeValue($"{pref}:FullName", item[$"{wb}:Product"]),
                                                ShortName = getNodeValue($"{pref}:ShortName", item[$"{wb}:Product"]),
                                                Capacity = getNodeValue($"{pref}:Capacity", item[$"{wb}:Product"]),
                                                Volume = getNodeValue($"{pref}:AlcVolume", item[$"{wb}:Product"]),
                                                ProductVCode = getNodeValue($"{pref}:ProductVCode", item[$"{wb}:Product"]),
                                                FormARegId = getNodeValue($"{pref}:RegId", item[nameInformA]),
                                                FormBRegId = getNodeValue(nameBRegId, VersionEgais<3? item[nameInformB][nameInformBItem]: item[nameInformB]),
                                                InWaybillNumber = Number,
                                                InWaybillCreateDate = CreateDateTime,
                                                InWaybillDate = Date,
                                                InWaybillShippingDate = ShippingDate,
                                                InWaybillShipper = ShipperName
                                            };

                    if (VersionEgais == 1)
                    {
                        if (item[$"{wb}:Product"][$"{pref}:Producer"] != null)
                        {
                            position.Producer = getNodeValue($"{oref}:ShortName", item[$"{wb}:Product"][$"{pref}:Producer"]);

                            if (string.IsNullOrWhiteSpace(position.Producer)) position.Producer = getNodeValue($"{oref}:FullName", item[$"{wb}:Product"][$"{pref}:Producer"]);
                        }
                    }
                    else
                    {
                        if ((item[$"{wb}:Product"][$"{pref}:Producer"] != null) && (item[$"{wb}:Product"][$"{pref}:Producer"].FirstChild != null))
                        {
                            position.Producer = getNodeValue($"{oref}:ShortName", item[$"{wb}:Product"][$"{pref}:Producer"].FirstChild);

                            if (string.IsNullOrWhiteSpace(position.Producer)) position.Producer = getNodeValue($"{oref}:FullName", item[$"{wb}:Product"][$"{pref}:Producer"].FirstChild);
                        }
                    }
                    
                    if (VersionEgais == 3)
                    {
                        XmlNode formBNode = item[$"{wb}:InformF2"][$"{ce}:MarkInfo"];
                        if (formBNode!=null)
                        foreach (XmlNode boxNode in formBNode.ChildNodes)
                        {
                            if (boxNode != null)
                            {
                                var amcList = new List<Amc>();
                                foreach (XmlNode amc in boxNode[$"{ce}:amclist"].ChildNodes)
                                {
                                    amcList.Add(new Amc {Barcode =  amc.InnerText});

                                }
                                position.BoxInfos.Add(new BoxInfo
                                {
                                    BoxNumber = getNodeValue($"{ce}:boxnumber", boxNode),
                                    AmcList = amcList
                                });
                            }

                        }
                    }
                    positions.Add(position);
                }
            }
            #endregion Заполнение...

            #region Проверка...
            {
                foreach (Position position in positions)
                {
                    position.Check();
                }
            }
            #endregion Проверка...

            if (string.IsNullOrWhiteSpace(xmlFormBRegInfo)) return positions;

            #region Дополнение...
            {
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(xmlFormBRegInfo);

                string nameBNode = (VersionEgais == 1) ? $"{ns}:TTNInformBReg" : $"{ns}:TTNInformF2Reg";

                if (xml[nameBNode] == null) throw new Exception(string.Format("Значение '{0}' не может быть пустым.", nameBNode));
                if (xml[nameBNode][$"{wbr}:Content"] == null) throw new Exception("Значение 'wbr:Content' не может быть пустым.");

                XmlNode node = xml[nameBNode][$"{wbr}:Content"];

                foreach (XmlNode item in node.ChildNodes)
                {
                    string itemIdentity = getNodeValue($"{wbr}:Identity", item);
                    string itemInformBRegId = (VersionEgais == 1) ? getNodeValue($"{wbr}:InformBRegId", item)
                                                                  : getNodeValue($"{wbr}:InformF2RegId", item);

                    foreach (Position position in positions)
                    {
                        if (position.Identity == itemIdentity)
                        {
                            position.InformBRegId = itemInformBRegId;
                            break;
                        }
                    }
                }
            }
            #endregion Дополнение...

            return positions;
        }
        /// <summary>
        /// Дополнить накладную данными.
        /// </summary>
        /// <param name="form">Документ о регистрации движения</param>
        protected virtual void addData(FormBRegInfo form)
        {
            wBRegId = form.WBRegId;
            xmlFormBRegInfo = form.XmlBody;

            lastChange = DateTime.Now.ToString("dd MMMM yyyy, HH:mm:ss");

            treeData = buildTreeData();
        }
        #endregion Защищенные методы класса.
    }
}
