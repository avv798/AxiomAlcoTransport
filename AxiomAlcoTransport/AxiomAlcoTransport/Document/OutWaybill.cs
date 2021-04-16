using System;
using System.Xml;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Исходящая ТТН.
    /// </summary>
    [Serializable]
    public class OutWaybill : AWaybill
    {
        #region Защищенные объекты класса.
        /// <summary>
        /// Статус исходящей накладной.
        /// </summary>
        protected OutWaybillStatus status;
        /// <summary>
        /// Идентификатор отправителя.
        /// </summary>
        protected string shipperClientRegId;
        /// <summary>
        /// Идентификатор получателя.
        /// </summary>
        protected string consigneeClientRegId;
        /// <summary>
        /// Идентификатор поставщика (может отсутствовать).
        /// </summary>
        protected string supplierClientRegId;
        /// <summary>
        /// Основание для отправки.
        /// </summary>
        protected string baseWaybill;
        /// <summary>
        /// Описание.
        /// </summary>
        protected string noteWaybill;
        /// <summary>
        /// Дата составления накладной.
        /// </summary>
        protected DateTime outDate;
        /// <summary>
        /// Дата отгрузки продукции.
        /// </summary>
        protected DateTime outShippingDate;
        /// <summary>
        /// Тип накладной.
        /// </summary>
        protected string typeWaybill;
        /// <summary>
        /// Тип продукции.
        /// </summary>
        protected string unitType;
        /// <summary>
        /// Тип перевозки.
        /// </summary>
        protected string tranType;
        /// <summary>
        /// Включить в накладную информацию о поставщике.
        /// </summary>
        protected bool includeSupplier;
        /// <summary>
        /// Тело исходящего xml-документа.
        /// </summary>
        [OptionalField]
        protected string xmlOutBody;
        /// <summary>
        /// Тело исходящего акта отзыва накладной (xml-документ).
        /// </summary>
        [OptionalField]
        protected string xmlRevokeAct;
        /// <summary>
        /// Идентификатор запроса на отзыв накладной.
        /// </summary>
        [OptionalField]
        protected string revokeReplyId;
        /// <summary>
        /// Идентификатор запроса, полученного в ответ на отправку накладной.
        /// </summary>
        [OptionalField]
        protected string sendReplyId;
        /// <summary>
        /// Список позиций накладной.
        /// Только чтение.
        /// </summary>
        protected readonly List<OutPosition> outPositions;
        /// <summary>
        /// Словарь данных о транспорте.
        /// Только чтение.
        /// </summary>
        protected readonly Dictionary<string, string> transportDictionary;
        /// <summary>
        /// Словарь дополнительных данных.
        /// Только чтение.
        /// </summary>
        protected readonly Dictionary<string, string> dictionary;
        #endregion Защищенные объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Статус исходящей накладной.
        /// Только чтение.
        /// </summary>
        [DisplayName("Статус"), ReadOnly(true), Browsable(true)]
        public int Status
        {
            get { return (int)status; }

        }
        /// <summary>
        /// Статус исходящей накладной.
        /// </summary>
        [DisplayName("Статус"), ReadOnly(true), Browsable(false)]
        public OutWaybillStatus StatusEnum
        {
            get { return status; }
            set { status = value; }
        }
        /// <summary>
        /// Описание статуса исходящей накладной.
        /// Только чтение.
        /// </summary>
        [DisplayName("Статус"), ReadOnly(true), Browsable(true)]
        public string StatusNote
        {
            get
            {
                switch (StatusEnum)
                {
                    case OutWaybillStatus.Partial: return "новая, заполнена частично";
                    case OutWaybillStatus.Ready: return "новая, готова к отправке";
                    case OutWaybillStatus.Sent: return "отправлена получателю";
                    case OutWaybillStatus.Confirmed: return "отправлена, зарегистрирована в ЕГАИС";
                    case OutWaybillStatus.Revoked: return "накладная отозвана отправителем";
                    case OutWaybillStatus.Rejected: return "накладная отвергнута сервером ЕГАИС";
                    default: return "неизвестный";
                }
            }
        }
        /// <summary>
        /// Идентификатор отправителя.
        /// </summary>
        [ReadOnly(true), Browsable(false)]
        public string ShipperClientRegId
        {
            get { return shipperClientRegId; }
            set { shipperClientRegId = value; }
        }
        /// <summary>
        /// Идентификатор получателя.
        /// </summary>
        [ReadOnly(true), Browsable(false)]
        public string ConsigneeClientRegId
        {
            get { return consigneeClientRegId; }
            set { consigneeClientRegId = value; }
        }
        /// <summary>
        /// Идентификатор поставщика (может отсутствовать).
        /// </summary>
        [ReadOnly(true), Browsable(false)]
        public string SupplierClientRegId
        {
            get { return supplierClientRegId; }
            set { supplierClientRegId = value; }
        }
        /// <summary>
        /// Основание для отправки.
        /// </summary>
        [ReadOnly(true), Browsable(false)]
        public string BaseWaybill
        {
            get { return baseWaybill; }
            set { baseWaybill = value; }
        }
        /// <summary>
        /// Описание.
        /// </summary>
        [ReadOnly(true), Browsable(false)]
        public string NoteWaybill
        {
            get { return noteWaybill; }
            set { noteWaybill = value; }
        }
        /// <summary>
        /// Словарь дополнительных данных.
        /// Только чтение.
        /// </summary>
        public Dictionary<string, string> Dictionary
        {
            get { return dictionary; }
        }
        /// <summary>
        /// Словарь данных о транспорте.
        /// Только чтение.
        /// </summary>
        public Dictionary<string, string> Transport
        {
            get { return transportDictionary; }
        }
        /// <summary>
        /// Дата составления накладной.
        /// </summary>
        [DisplayName("Дата составления"), ReadOnly(true), Browsable(true)]
        public DateTime OutDate
        {
            get { return outDate; }
            set
            {
                outDate = value;
                Date = value.ToString("yyyy-MM-dd");
            }
        }
        /// <summary>
        /// Дата отгрузки продукции.
        /// </summary>
        [DisplayName("Дата отгрузки"), ReadOnly(true), Browsable(true)]
        public DateTime OutShippingDate
        {
            get { return outShippingDate; }
            set
            {
                outShippingDate = value;
                ShippingDate = value.ToString("yyyy-MM-dd");
            }
        }
        /// <summary>
        /// Тип накладной.
        /// </summary>
        [ReadOnly(true), Browsable(false)]
        public string TypeWaybill
        {
            get { return typeWaybill; }
            set { typeWaybill = value; }
        }
        /// <summary>
        /// Тип продукции.
        /// </summary>
        [ReadOnly(true), Browsable(false)]
        public string UnitType
        {
            get { return unitType; }
            set { unitType = value; }
        }
        /// <summary>
        /// Тип перевозки.
        /// </summary>
        [ReadOnly(true), Browsable(false)]
        public string TranType
        {
            get { return tranType; }
            set { tranType = value; }
        }
        /// <summary>
        /// Включить в накладную информацию о поставщике.
        /// </summary>
        [ReadOnly(true), Browsable(false)]
        public bool IncludeSupplier
        {
            get { return includeSupplier; }
            set { includeSupplier = value; }
        }
        /// <summary>
        /// Идентификатор поставщика.
        /// Только чтение.
        /// </summary>
        [DisplayName("Идентификатор поставщика"), ReadOnly(true), Browsable(false)]
        protected override string AuxShipperClientRegId
        {
            get { return shipperClientRegId; }
        }
        /// <summary>
        /// Список позиций накладной.
        /// Только чтение.
        /// </summary>
        [DisplayName("Список позиций накладной"), ReadOnly(true), Browsable(false)]
        public List<OutPosition> OutPositions
        {
            get { return outPositions; }
        }
        /// <summary>
        /// Тело исходящего xml-документа.
        /// Только чтение.
        /// </summary>
        public string XmlOutBody
        {
            get { return xmlOutBody; }
        }
        /// <summary>
        /// Тело исходящего акта отзыва накладной (xml-документ).
        /// Только чтение.
        /// </summary>
        public string XmlRevokeAct
        {
            get { return xmlRevokeAct; }
        }
        /// <summary>
        /// Композитный идентификатор.
        /// Только чтение.
        /// </summary>
        public override string AuxCompositeId
        {
            get { return createCompositeId(Identity, Number, AuxShipperClientRegId, OutDate); }
        }
        /// <summary>
        /// Идентификатор запроса, полученного в ответ на отправку накладной.
        /// Только чтение.
        /// </summary>
        public string SendReplyId
        {
            get { return sendReplyId; }
        }
        /// <summary>
        /// Идентификатор запроса, полученного в ответ на отправку запроса на отзыв накладной.
        /// Только чтение.
        /// </summary>
        public string RevokeReplyId
        {
            get { return revokeReplyId; }
        }
        public bool ChangeOwnership { get; set; }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public OutWaybill()
        {
            // В документах второй версии поставщик не указывается.
            // Посему - удаляем этот функционал, в том числе - в UI.
            // Методы и свойства, посвящённые "поставщику" остаются в классе для совместимости.

            VersionEgais = Documents.GetDocumentsVersion();

            status = OutWaybillStatus.Partial;
            identity = string.Format("{0}-{1}", DateTime.Now.ToString("yyMMddHHmmssfff"), Guid.NewGuid().ToString().Replace("-", "").ToUpper());
            number = string.Format("{0}", DateTime.Now.ToString("yyyyMMdd-HHmmssfff"));
            OutDate = DateTime.Now;
            OutShippingDate = DateTime.Now;
            baseWaybill = string.Empty;
            noteWaybill = string.Empty;

            shipperClientRegId = string.Empty;
            consigneeClientRegId = string.Empty;
            supplierClientRegId = string.Empty;

            typeWaybill = "WBInvoiceFromMe";    // Тип накладной - "Расход".
            unitType = "Packed";                // Тип продукции - "Упакованная".
            tranType = "413";                   // Тип перевозки - "Автомобильная".
            includeSupplier = false;            // "По умолчанию" информация о поставщике не указывается.

            lastChange = DateTime.Now.ToString("dd MMMM yyyy, HH:mm:ss");

            outPositions = new List<OutPosition>();
            dictionary = new Dictionary<string, string>();
            transportDictionary = new Dictionary<string, string>();

            transportDictionary["TRAN_COMPANY"] = string.Empty;
            transportDictionary["TRANSPORT_TYPE"] = "car";
            transportDictionary["TRAN_TRAILER"] = string.Empty;
            transportDictionary["TRAN_CUSTOMER"] = string.Empty;
            transportDictionary["TRAN_DRIVER"] = string.Empty;
            transportDictionary["TRAN_LOADPOINT"] = string.Empty;
            transportDictionary["TRAN_UNLOADPOINT"] = string.Empty;
            transportDictionary["TRAN_REDIRECT"] = string.Empty;
            transportDictionary["TRAN_FORWARDER"] = string.Empty;

            xmlOutBody = string.Empty;
            xmlRevokeAct = string.Empty;
            revokeReplyId = string.Empty;

            sendReplyId = string.Empty;
        }
        #endregion Конструкторы класса.

        #region Переопределение базовых методов.
        /// <summary>
        /// Дополнить накладную данными.
        /// </summary>
        /// <param name="form">Документ о регистрации движения</param>
        protected override void addData(FormBRegInfo form)
        {
            if (StatusEnum != OutWaybillStatus.Sent)
            {
                Program.Logger.Error(string.Format("В дополнении отказано. Дополнить данными можно только накладную, находящуюся в статусе '{0}'.", OutWaybillStatus.Sent));

                return;
            }

            base.addData(form);

            changeStatus(OutWaybillStatus.Confirmed);
        }
        /// <summary>
        /// Получить направление накладной (входящая или исходящая).
        /// </summary>
        /// <returns>Направление накладной (входящая или исходящая)</returns>
        protected override string getDirect()
        {
            return "Исходящая";
        }
        /// <summary>
        /// Построить иерархию данных.
        /// </summary>
        /// <returns>Иерархические данные.</returns>
        protected override TreeData buildTreeData()
        {
            Program.Logger.Info(this, string.Format("Построение (расширенное) иерархических данных в накладной '{0}'...", Description));

            TreeData tree = base.buildTreeData();

            // ReSharper disable once UnusedVariable
            TreeData statusData = new TreeData(tree, "Статус накладной", StatusNote);
            // ReSharper disable once UnusedVariable
            TreeData last = new TreeData(tree, "Последнее изменение", lastChange);
            // ReSharper disable once UnusedVariable
            TreeData compositeId = new TreeData(tree, "Композитный идентификатор", AuxCompositeId);

            #region Исходящий xml-документ.
            {
                if (string.IsNullOrWhiteSpace(XmlOutBody))
                {
                    Program.Logger.Info(this, "Тело исходящего xml-документа пустое. Дополнения не требуется.");
                }
                else
                {
                    Program.Logger.Info(this, "Попытка дополнения иерархических данных исходящим xml-документом...");

                    XmlDocument xmlForm = new XmlDocument();
                    xmlForm.LoadXml(XmlOutBody);

                    TreeData xmlOut = new TreeData(tree, "Исходящий документ", null);

                    if (xmlForm.HasChildNodes) parseXmlNode(xmlForm, xmlOut);

                    Program.Logger.Info(this, "... дополнение иерархических данных исходящим xml-документом успешно завершено.");
                }
            }
            #endregion Исходящий xml-документ.

            #region Документ регистрации движения.
            {
                if (string.IsNullOrWhiteSpace(xmlFormBRegInfo))
                {
                    Program.Logger.Info(this, "Документ регистрации движения пустой. Дополнения не требуется.");
                }
                else
                {
                    Program.Logger.Info(this, "Попытка дополнения иерархических данных документом регистрации движения...");

                    XmlDocument xmlForm = new XmlDocument();
                    xmlForm.LoadXml(xmlFormBRegInfo);

                    if (xmlForm.HasChildNodes) parseXmlNode(xmlForm, tree);

                    Program.Logger.Info(this, "... дополнение иерархических данных документом регистрации движения успешно завершено.");
                }
            }
            #endregion Документ регистрации движения.

            #region Исходящий акт отзыва.
            {
                if (string.IsNullOrWhiteSpace(xmlRevokeAct))
                {
                    Program.Logger.Info(this, "Акт отзыва накладной пустой. Дополнения не требуется.");
                }
                else
                {
                    Program.Logger.Info(this, "Попытка дополнения иерархических данных актом отзыва накладной...");

                    XmlDocument xmlAct = new XmlDocument();
                    xmlAct.LoadXml(xmlRevokeAct);

                    TreeData treeAct = new TreeData(tree, "Акт отзыва накладной", null);

                    if (xmlAct.HasChildNodes) parseXmlNode(xmlAct, treeAct);

                    Program.Logger.Info(this, "... дополнение иерархических данных актом отзыва успешно завершено.");
                }
            }
            #endregion Исходящий акт отзыва.

            Program.Logger.Info(this, "... построение (расширенное) иерархических данных успешно завершено.");

            return tree;
        }
        /// <summary>
        /// Получить позиции накладной.
        /// </summary>
        /// <returns>Список позиций.</returns>
        protected override List<Position> getPositions()
        {
            List<Position> positions = OutPositions.Select(outPosition => outPosition.Convert()).ToList();

            #region Проверка & дополнение...
            {
                foreach (Position position in positions)
                {
                    position.WaybillInformation = string.Format("Накладная с номером '{0}' ('{1}') для '{2}' ('{3}')", Number, Direct, ConsigneeName, OutShippingDate.ToString("yyyy-MM-dd"));
                    position.Check();
                }
            }
            #endregion Проверка & дополнение...

            #region Дополнение 'Б'...
            {
                if (string.IsNullOrWhiteSpace(xmlFormBRegInfo)) return positions;

                XmlDocument xml = new XmlDocument();
                xml.LoadXml(xmlFormBRegInfo);

                string nameBNode = (VersionEgais == 1) ? "ns:TTNInformBReg" : "ns:TTNInformF2Reg";

                if (xml[nameBNode] == null) throw new Exception(string.Format("Значение '{0}' не может быть пустым.", nameBNode));
                if (xml[nameBNode]["wbr:Content"] == null) throw new Exception("Значение 'wbr:Content' не может быть пустым.");

                XmlNode node = xml[nameBNode]["wbr:Content"];

                foreach (XmlNode item in node.ChildNodes)
                {
                    string itemIdentity = getNodeValue("wbr:Identity", item);
                    string itemInformBRegId = (VersionEgais == 1) ? getNodeValue("wbr:InformBRegId", item)
                                                                  : getNodeValue("wbr:InformF2RegId", item);

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
            #endregion Дополнение 'Б'...

            return positions;
        }
        #endregion Переопределение базовых методов.

        #region Внешние методы класса.
        /// <summary>
        /// Изменить статус накладной.
        /// </summary>
        /// <param name="newStatus">Новый статус.</param>
        public void ChangeStatus(OutWaybillStatus newStatus)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка изменить статус накладной '{0}' с '{1}' на '{2}'...", Description, status, newStatus));

                changeStatus(newStatus);

                Program.Logger.Info(this, "... изменение статуса успешно завершено.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время изменения статуса произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Клонировать накладную.
        /// </summary>
        /// <returns>Клонированная накладная.</returns>
        public OutWaybill Clone()
        {
            OutWaybill outWaybill = new OutWaybill
                                        {
                                            TypeWaybill = TypeWaybill,
                                            UnitType = UnitType,
                                            OutDate = DateTime.Now,
                                            OutShippingDate = DateTime.Now,
                                            BaseWaybill = BaseWaybill,
                                            NoteWaybill = NoteWaybill,
                                            TranType = TranType,
                                            ShipperClientRegId = ShipperClientRegId,
                                            ShipperName = ShipperName,
                                            ConsigneeClientRegId = ConsigneeClientRegId,
                                            ConsigneeName = ConsigneeName,
                                            IncludeSupplier = IncludeSupplier,
                                            SupplierClientRegId = SupplierClientRegId,
                                            StatusEnum = (status == OutWaybillStatus.Partial) ? OutWaybillStatus.Partial : OutWaybillStatus.Ready
                                        };

            foreach (KeyValuePair<string, string> pair in Dictionary)
            {
                outWaybill.Dictionary[pair.Key] = pair.Value;
            }

            foreach (KeyValuePair<string, string> pair in Transport)
            {
                outWaybill.Transport[pair.Key] = pair.Value;
            }

            foreach (OutPosition outPosition in OutPositions.OrderBy(x => x.Identity))
            {
                outWaybill.OutPositions.Add(outPosition.Clone());
            }

            outWaybill.Check();
               
            return outWaybill;
        }
        /// <summary>
        /// Проверить накладную и изменить (по необходимости) её статус.
        /// </summary>
        /// <param name="suppressException">Признак подавления исключения.</param>
        public void Check(bool suppressException = true)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка проверить накладную '{0}'...", Description));

                check(suppressException);

                Program.Logger.Info(this, "... проверка накладной успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время проверки накладной произошла ошибка.", exception);

                if (!suppressException) throw;
            }
        }
        /// <summary>
        /// Добавить исходящий xml-документ.
        /// </summary>
        /// <param name="outXml"></param>
        public void AddOutData(XmlDocument outXml)
        {
            Program.Logger.Info(this, string.Format("Попытка добавить исходящий xml-документ для накладной '{0}'...", Description));

            addOutData(outXml);

            Program.Logger.Info(this, string.Format("... добавление исходящего xml-документа длиной {0} символов успешно завершено.", outXml.OuterXml.Length));
        }
        /// <summary>
        /// Добавить акт отзыва.
        /// </summary>
        /// <param name="xmlAct">Акт отзыва.</param>
        public void AddRevokeAct(XmlDocument xmlAct)
        {
            Program.Logger.Info(this, string.Format("Попытка добавить акт отзыва для накладной '{0}'...", Description));

            addRevokeAct(xmlAct);

            Program.Logger.Info(this, string.Format("... добавление акта отзыва длиной {0} символов успешно завершено.", xmlAct.OuterXml.Length));
        }
        /// <summary>
        /// Установить идентификатор запроса, полученного в ответ на отправку накладной.
        /// </summary>
        public void SetSendReplyId(string replyId)
        {
            sendReplyId = replyId;

            Program.Logger.Info(this, string.Format("У исходящей накладной '{0}' установлен новый идентификатор запроса, полученного в ответ на отправку накладной: '{1}'.", Description, sendReplyId));
        }
        /// <summary>
        /// Установить идентификатор запроса, полученного в ответ на отправку накладной.
        /// </summary>
        public void SetRevokeReplyId(string replyId)
        {
            revokeReplyId = replyId;

            Program.Logger.Info(this, string.Format("У исходящей накладной '{0}' установлен новый идентификатор запроса, полученного в ответ на запрос отзыва накладной: '{1}'.", Description, revokeReplyId));
        }
        #endregion Внешние методы класса.

        #region Защищенные методы класса.
        /// <summary>
        /// Изменить статус накладной.
        /// </summary>
        /// <param name="newStatus">Новый статус.</param>
        protected virtual void changeStatus(OutWaybillStatus newStatus)
        {
            status = newStatus;
            lastChange = DateTime.Now.ToString("dd MMMM yyyy, HH:mm:ss");

            treeData = buildTreeData();
        }
        /// <summary>
        /// Проверить накладную и изменить (по необходимости) её статус.
        /// </summary>
        /// <param name="suppressException">Признак подавления исключения.</param>
        protected virtual void check(bool suppressException)
        {
            try
            {
                if ((StatusEnum == OutWaybillStatus.Sent)
                    || (StatusEnum == OutWaybillStatus.Confirmed)
                     || (StatusEnum == OutWaybillStatus.Rejected)
                     || (StatusEnum == OutWaybillStatus.Revoked)) return;

                lastChange = DateTime.Now.ToString("dd MMMM yyyy, HH:mm:ss");

                if (string.IsNullOrWhiteSpace(Identity)) throw new Exception("Идентификатор накладной не может быть пустым.");
                if (string.IsNullOrWhiteSpace(Number)) throw new Exception("Номер накладной не может быть пустым.");
                if (Number.Length > 49) throw new Exception("Номер накладной не может превышать в длину 49 символов.");
                
                if (string.IsNullOrWhiteSpace(Date)) throw new Exception("Дата составления не может быть пустой.");
                if (OutDate > DateTime.Now.AddMonths(3)) throw new Exception("Дата составления не может быть настолько больше сегодняшнего числа.");
                if (OutDate < DateTime.Now.AddMonths(-12)) throw new Exception("Дата составления не может быть настолько меньше сегодняшнего числа.");

                if (string.IsNullOrWhiteSpace(ShippingDate)) throw new Exception("Дата отгрузки не может быть пустой.");
                if (OutShippingDate > DateTime.Now.AddMonths(3)) throw new Exception("Дата отгрузки не может быть настолько больше сегодняшнего числа.");
                if (OutShippingDate < DateTime.Now.AddMonths(-12)) throw new Exception("Дата отгрузки не может быть настолько меньше сегодняшнего числа.");

                if (string.IsNullOrWhiteSpace(TypeWaybill)) throw new Exception("Тип накладной не может быть пустым.");
                if (string.IsNullOrWhiteSpace(UnitType)) throw new Exception("Тип продукции не может быть пустым.");

                if (string.IsNullOrWhiteSpace(ShipperClientRegId)) throw new Exception("Не указан отправитель данной товарно-транспортной накладной.");
                if (string.IsNullOrWhiteSpace(ConsigneeClientRegId)) throw new Exception("Не указан получатель данной товарно-транспортной накладной.");

                if (IncludeSupplier)
                {
                    if (string.IsNullOrWhiteSpace(SupplierClientRegId)) throw new Exception("Не указан конкретный поставщик товара.");
                }

                if (string.IsNullOrWhiteSpace(TranType)) throw new Exception("Не указан тип перевозки товара.");

                if (outPositions.Count == 0) throw new Exception("Список позиций товарно-транспортной накладной пустой. Отправка не имеет смысла.");

                foreach (OutPosition outPosition in OutPositions)
                {
                    outPosition.Check();
                }

                changeStatus(OutWaybillStatus.Ready);
            }
            catch (Exception)
            {
                changeStatus(OutWaybillStatus.Partial);

                if (!suppressException) throw;
            }
        }
        /// <summary>
        /// Добавить исходящий xml-документ.
        /// </summary>
        /// <param name="outXml"></param>
        protected virtual void addOutData(XmlDocument outXml)
        {
            xmlOutBody = outXml.OuterXml;
            lastChange = DateTime.Now.ToString("dd MMMM yyyy, HH:mm:ss");

            treeData = buildTreeData();
        }
        /// <summary>
        /// Добавить акт отзыва.
        /// </summary>
        /// <param name="xmlAct">Акт отзыва.</param>
        protected virtual void addRevokeAct(XmlDocument xmlAct)
        {
            xmlRevokeAct = xmlAct.OuterXml;
            lastChange = DateTime.Now.ToString("dd MMMM yyyy, HH:mm:ss");

            treeData = buildTreeData();
        }
        #endregion Защищенные методы класса.

        #region Внешние статические методы класса.
        /// <summary>
        /// Наименование документа.
        /// </summary>
        public static string NativeName
        {
            get { return "Исходящая накладная"; }
        }

        

        #endregion Внешние статические методы класса.
    }
}

