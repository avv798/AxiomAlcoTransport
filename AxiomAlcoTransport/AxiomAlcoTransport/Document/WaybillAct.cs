using System;
using System.Runtime.Serialization;
using System.Xml;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Акт по накладной.
    /// </summary>
    [Serializable]
    public class WaybillAct : ADocument
    {
        #region Защищенные объекты класса.
        /// <summary>
        /// Статус акта по накладной.
        /// </summary>
        protected WaybillActStatus status;
        /// <summary>
        /// Последнее изменение.
        /// </summary>
        protected string lastChange;
        /// <summary>
        /// Идентификатор ТТН (по документу регистрации движения).
        /// </summary>
        protected readonly string wBRegId;
        /// <summary>
        /// Тип акта.
        /// </summary>
        protected readonly string actType;
        /// <summary>
        /// Номер акта.
        /// </summary>
        protected readonly string actNumber;
        /// <summary>
        /// Дата составления акта.
        /// </summary>
        protected readonly string actDate;
        /// <summary>
        /// Комментарий к акту.
        /// </summary>
        protected readonly string actNote;
        /// <summary>
        /// Акт подтверждения.
        /// </summary>
        [OptionalField]
        protected string xmlConfirmAct;
        /// <summary>
        /// Идентификатор запроса, полученного в ответ на отправку акта подтверждения.
        /// </summary>
        [OptionalField]
        protected string confirmActReplyId;
        #endregion Защищенные объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Статус акта по накладной.
        /// Только чтение.
        /// </summary>
        [DisplayName("Статус"), ReadOnly(true), Browsable(true)]
        public int Status
        {
            get { return (int)StatusEnum; }
        }
        /// <summary>
        /// Статус акта по накладной.
        /// Только чтение.
        /// </summary>
        [DisplayName("Статус"), ReadOnly(true), Browsable(false)]
        public WaybillActStatus StatusEnum
        {
            get
            {
                if ((status == WaybillActStatus.New) && (!IsContentExist))
                {
                    status = WaybillActStatus.Information;
                }

                return status;
            }
        }
        /// <summary>
        /// Статус акта по накладной.
        /// Только чтение.
        /// </summary>
        [DisplayName("Статус"), ReadOnly(true), Browsable(true)]
        public string StatusNote
        {
            get
            {
                switch (StatusEnum)
                {
                    case WaybillActStatus.New: return "новый акт, требует обработки";
                    case WaybillActStatus.Accepted: return "подтверждённый акт";
                    case WaybillActStatus.Rejected: return "отвергнутый акт";
                    case WaybillActStatus.Information: return "информационное сообщение";

                    default: return "неизвестный";
                }
            }
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
        /// Признак существования контента в акте.
        /// </summary>
        [DisplayName("Последнее изменение"), ReadOnly(true), Browsable(false)]
        public bool IsContentExist
        {
            get { return isContentExist(); }
        }
        /// <summary>
        /// Тип акта.
        /// </summary>
        [DisplayName("Тип акта (оригинал)"), ReadOnly(true), Browsable(true)]
        public string ActType
        {
            get { return actType; }
        }
        /// <summary>
        /// Тип акта.
        /// </summary>
        [DisplayName("Тип акта"), ReadOnly(true), Browsable(true)]
        public string ActTypeTranslate
        {
            get { return Program.Language.TranslateReference(actType); }
        }
        /// <summary>
        /// Номер акта.
        /// </summary>
        [DisplayName("Номер акта"), ReadOnly(true), Browsable(true)]
        public string ActNumber
        {
            get { return actNumber; }
        }
        /// <summary>
        /// Дата составления акта.
        /// </summary>
        [DisplayName("Дата составления акта"), ReadOnly(true), Browsable(true)]
        public string ActDate
        {
            get { return actDate; }
        }
        /// <summary>
        /// Комментарий к акту.
        /// </summary>
        [DisplayName("Комментарий к акту"), ReadOnly(true), Browsable(true)]
        public string ActNote
        {
            get { return actNote; }
        }
        /// <summary>
        /// Акт подтверждения.
        /// Только чтение.
        /// </summary>
        public string XmlConfirmAct
        {
            get { return xmlConfirmAct; }
        }
        /// <summary>
        /// Идентификатор запроса, полученного в ответ на отправку акта подтверждения.
        /// Только чтение.
        /// </summary>
        public string ConfirmActReplyId
        {
            get { return confirmActReplyId; }
        }

        public bool ChangeOwnership { get; set; }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected WaybillAct()
        {
            ReplyId = string.Empty;

            status = WaybillActStatus.New;
            lastChange = string.Empty;

            wBRegId = string.Empty;
            actType = string.Empty;
            actNumber = string.Empty;
            actDate = string.Empty;
            actNote = string.Empty;

            xmlConfirmAct = string.Empty;
            confirmActReplyId = string.Empty;
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="node">XML-нода для создания.</param>
        /// <param name="version">Версия документа.</param>
        public WaybillAct(XmlNode node, int version) : this()
        {
            xmlBody = node.OuterXml;

            VersionEgais = version;

            if (node == null) throw new Exception("Значение 'ns:WayBillAct' не может быть пустым.");
            if (node["wa:Header"] == null) throw new Exception("Значение 'wa:Header' не может быть пустым.");

            actType = getNodeValue("wa:IsAccept", node["wa:Header"]);
            actNumber = getNodeValue("wa:ACTNUMBER", node["wa:Header"]);
            actDate = TryParseDateTime(getNodeValue("wa:ActDate", node["wa:Header"]));
            actNote = getNodeValue("wa:Note", node["wa:Header"]);
            wBRegId = getNodeValue("wa:WBRegId", node["wa:Header"]);

            if ((status == WaybillActStatus.New) && (!IsContentExist)) status = WaybillActStatus.Information;
        }
        #endregion Конструкторы класса.

        #region Внешние методы класса.
        /// <summary>
        /// Дополнить акт данными.
        /// </summary>
        /// <param name="xmlActAdd">Подтверждение.</param>
        public void AddConfirmAct(string xmlActAdd)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка дополнить акт '{0}' документом подтверждения...", Description));

                addConfirmAct(xmlActAdd);

                Program.Logger.Info(this, "... попытка дополнить акт документом подтверждения успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время дополнения акта произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Изменить статус.
        /// </summary>
        /// <param name="newStatus">Новый статус.</param>
        public void ChangeStatus(WaybillActStatus newStatus)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка изменить статус акта '{0}' с '{1}' на '{2}'...", Description, status, newStatus));

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
        /// Установить идентификатор запроса, полученного в ответ на отправку акта подтверждения.
        /// </summary>
        public void SetConfirmActReplyId(string replyId)
        {
            confirmActReplyId = replyId;

            Program.Logger.Info(this, string.Format("У акта подтверждения '{0}' установлен новый идентификатор запроса, полученного в ответ на отправку: '{1}'.", Description, confirmActReplyId));
        }
        #endregion Внешние методы класса.

        #region Защищенные методы класса.
        /// <summary>
        /// Изменить статус накладной.
        /// </summary>
        /// <param name="newStatus">Новый статус.</param>
        protected virtual void changeStatus(WaybillActStatus newStatus)
        {
            status = newStatus;
            lastChange = DateTime.Now.ToString("dd MMMM yyyy, HH:mm:ss");

            treeData = buildTreeData();
        }
        /// <summary>
        /// Дополнить акт данными.
        /// </summary>
        /// <param name="xmlActAdd">Подтверждение.</param>
        protected virtual void addConfirmAct(string xmlActAdd)
        {
            xmlConfirmAct = xmlActAdd;
            lastChange = DateTime.Now.ToString("dd MMMM yyyy, HH:mm:ss");

            treeData = buildTreeData();
        }
        /// <summary>
        /// Проверка, есть ли секция контента в акте.
        /// </summary>
        /// <returns></returns>
        protected virtual bool isContentExist()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlBody);

            string nameNode = (VersionEgais == 1) ? "ns:WayBillAct" : "ns:WayBillAct_v2";

            if (xml[nameNode] == null) return false;
            if (xml[nameNode]["wa:Content"] == null) return false;

            return xml[nameNode]["wa:Content"].HasChildNodes;
        }
        #endregion Защищенные методы класса.

        #region Переопределение базовых методов.
        /// <summary>
        /// Получение описания объекта.
        /// </summary>
        /// <returns>Описание.</returns>
        protected override string GetDescription()
        {
            return string.Format("{0}: WBRegId = '{1}' ('{2}')", GetType().FullName, wBRegId, StatusEnum);
        }
        /// <summary>
        /// Построить иерархию данных.
        /// </summary>
        /// <returns>Иерархические данные.</returns>
        protected override TreeData buildTreeData()
        {
            Program.Logger.Info(this, "Попытка дополнения иерархических данных...");

            TreeData tree = base.buildTreeData();

            // ReSharper disable once UnusedVariable
            TreeData statusData = new TreeData(tree, "Статус акта", StatusNote);
            // ReSharper disable once UnusedVariable
            TreeData last = new TreeData(tree, "Последнее изменение", LastChange);

            if (string.IsNullOrWhiteSpace(xmlConfirmAct))
            {
                Program.Logger.Info(this, "Акт по накладной пустой. Дополнения не требуется.");
            }
            else
            {
                Program.Logger.Info(this, "Попытка дополнения иерархических данных актом по накладной...");

                XmlDocument xmlForm = new XmlDocument();
                xmlForm.LoadXml(xmlConfirmAct);

                TreeData actTree = new TreeData(tree, "Акт подтверждения", null);
                if (xmlForm.HasChildNodes) parseXmlNode(xmlForm, actTree);

                Program.Logger.Info(this, "... дополнение иерархических данных актом по накладной успешно завершено.");
            }

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
            get { return "Акт по накладной"; }
        }
        #endregion Внешние статические методы класса.
    }
}
