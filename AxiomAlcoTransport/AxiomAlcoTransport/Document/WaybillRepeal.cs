using System;
using System.Xml;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Запрос на распроведение накладной.
    /// </summary>
    [Serializable]
    public class WaybillRepeal : ADocument
    {
        #region Защищенные объекты класса.
        /// <summary>
        /// Статус акта по накладной.
        /// </summary>
        protected WaybillRepealStatus status;
        /// <summary>
        /// Последнее изменение.
        /// </summary>
        protected string lastChange;
        /// <summary>
        /// Идентификатор ТТН (по документу регистрации движения).
        /// </summary>
        protected readonly string wBRegId;
        /// <summary>
        /// Номер запроса.
        /// </summary>
        protected readonly string requestNumber;
        /// <summary>
        /// Дата составления запроса.
        /// </summary>
        protected readonly string requestDate;
        /// <summary>
        /// Акт подтверждения.
        /// </summary>
        protected string xmlConfirmAct;
        /// <summary>
        /// Идентификатор запроса, полученного в ответ на отправку акта подтверждения.
        /// </summary>
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
        public WaybillRepealStatus StatusEnum
        {
            get { return status; }
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
                    case WaybillRepealStatus.New: return "новый запрос, требует обработки";
                    case WaybillRepealStatus.Accepted: return "подтверждённый запрос";
                    case WaybillRepealStatus.Rejected: return "отвергнутый запрос";

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
        /// Номер запроса.
        /// </summary>
        [DisplayName("Номер запроса"), ReadOnly(true), Browsable(true)]
        public string RequestNumber
        {
            get { return requestNumber; }
        }
        /// <summary>
        /// Дата составления запроса.
        /// </summary>
        [DisplayName("Дата составления запроса"), ReadOnly(true), Browsable(true)]
        public string RequestDate
        {
            get { return requestDate; }
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
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected WaybillRepeal()
        {
            ReplyId = string.Empty;

            status = WaybillRepealStatus.New;
            lastChange = string.Empty;

            wBRegId = string.Empty;
            requestNumber = string.Empty;
            requestDate = string.Empty;

            xmlConfirmAct = string.Empty;
            confirmActReplyId = string.Empty;
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="node">XML-нода для создания.</param>
        public WaybillRepeal(XmlNode node) : this()
        {
            xmlBody = node.OuterXml;

            VersionEgais = 2;

            if (node == null) throw new Exception("Значение 'ns:RequestRepealWB' не может быть пустым.");

            requestNumber = getNodeValue("qp:RequestNumber", node);
            requestDate = TryParseDateTime(getNodeValue("qp:RequestDate", node));
            wBRegId = getNodeValue("qp:WBRegId", node);

            status = WaybillRepealStatus.New;
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
                Program.Logger.Info(this, string.Format("Попытка дополнить запрос '{0}' документом подтверждения...", Description));

                addConfirmAct(xmlActAdd);

                Program.Logger.Info(this, "... попытка дополнить запрос документом подтверждения успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время дополнения запроса произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Изменить статус.
        /// </summary>
        /// <param name="newStatus">Новый статус.</param>
        public void ChangeStatus(WaybillRepealStatus newStatus)
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
        protected virtual void changeStatus(WaybillRepealStatus newStatus)
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
                Program.Logger.Info(this, "Запрос распроведения накладной пустой. Дополнения не требуется.");
            }
            else
            {
                Program.Logger.Info(this, "Попытка дополнения иерархических данных запросом на распроведение накладной...");

                XmlDocument xmlForm = new XmlDocument();
                xmlForm.LoadXml(xmlConfirmAct);

                TreeData actTree = new TreeData(tree, "Акт подтверждения", null);
                if (xmlForm.HasChildNodes) parseXmlNode(xmlForm, actTree);

                Program.Logger.Info(this, "... дополнение иерархических данных запросом на распроведение накладной успешно завершено.");
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
            get { return "Запрос на распроведение накладной"; }
        }
        #endregion Внешние статические методы класса.
    }
}
