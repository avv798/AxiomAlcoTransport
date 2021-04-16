using System;
using System.Collections.Generic;
using System.Xml;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Входящая ТТН.
    /// </summary>
    [Serializable]
    public class InWaybill : AWaybill
    {
        #region Защищенные объекты класса.
        /// <summary>
        /// Статус входящей накладной.
        /// </summary>
        protected InWaybillStatus status;
        /// <summary>
        /// Акт по накладной.
        /// </summary>
        [OptionalField]
        protected string xmlAct;
        /// <summary>
        /// Идентификатор запроса, полученного в ответ на отправку акта по накладной.
        /// </summary>
        [OptionalField]
        protected string actReplyId;
        /// <summary>
        /// Идентификатор запроса, полученного в ответ на отправку запроса распроведения.
        /// </summary>
        [OptionalField]
        protected string repealReplyId;
        /// <summary>
        /// Статус накладной перед отправкой запроса распроведения.
        /// </summary>
        [OptionalField]
        protected InWaybillStatus prevRepealStatus;
        /// <summary>
        /// Тело запроса на отмену проведения акта.
        /// </summary>
        [OptionalField]
        private string xmlRepealBody;
        #endregion Защищенные объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Статус входящей накладной.
        /// Только чтение.
        /// </summary>
        [DisplayName("Статус"), ReadOnly(true), Browsable(true)]
        public int Status
        {
            get { return (int)status; }
        }
        /// <summary>
        /// Статус входящей накладной.
        /// </summary>
        [DisplayName("Статус"), ReadOnly(true), Browsable(false)]
        public InWaybillStatus StatusEnum
        {
            get { return status; }
        }
        /// <summary>
        /// Описание статуса входящей накладной.
        /// Только чтение.
        /// </summary>
        [DisplayName("Статус"), ReadOnly(true), Browsable(true)]
        public string StatusNote
        {
            get
            {
                switch (StatusEnum)
                {
                    case InWaybillStatus.Partial: return "загружена частично";
                    case InWaybillStatus.New: return "новая";
                    case InWaybillStatus.Accepted: return "подтверждённая";
                    case InWaybillStatus.Rejected: return "отвергнутая";
                    case InWaybillStatus.Difference: return "с актом расхождения";
                    case InWaybillStatus.Repealed: return "распроведённая";
                    default: return "неизвестный";
                }
            }
        }
        /// <summary>
        /// Акт по накладной.
        /// Только чтение.
        /// </summary>
        public string XmlAct
        {
            get { return xmlAct; }
        }
        /// <summary>
        /// Идентификатор запроса, полученного в ответ на отправку акта по накладной.
        /// Только чтение.
        /// </summary>
        public string ActReplyId
        {
            get { return actReplyId; }
        }
        /// <summary>
        /// Идентификатор запроса, полученного в ответ на отправку запроса распроведения.
        /// Только чтение.
        /// </summary>
        public string RepealReplyId
        {
            get { return repealReplyId; }
        }
        /// <summary>
        /// Статус накладной перед отправкой запроса распроведения.
        /// Только чтение.
        /// </summary>
        public InWaybillStatus PrevRepealStatus
        {
            get { return prevRepealStatus; }
        }
        /// <summary>
        /// Тело запроса на отмену проведения акта.
        /// </summary>
        [DisplayName("Исходящий запрос на отмену проведения"), ReadOnly(true), Browsable(false)]
        public string XmlRepealBody
        {
            get { return xmlRepealBody; }
        }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.

        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="node">XML-нода для создания.</param>
        /// <param name="version">Версия документа.</param>
        /// <param name="nameSpaces"></param>
        public InWaybill(XmlNode node, int version, Dictionary<string, string> nameSpaces) : base(node, version, nameSpaces)
        {
            wBRegId = string.Empty;
            xmlFormBRegInfo = string.Empty;
            xmlAct = string.Empty;
            actReplyId = string.Empty;
            repealReplyId = string.Empty;
            xmlRepealBody = string.Empty;
            prevRepealStatus = InWaybillStatus.Partial;
           
            status = InWaybillStatus.Partial;
            lastChange = createDateTime.ToString("dd MMMM yyyy, HH:mm:ss");
        }
        #endregion Конструкторы класса.

        #region Внешние методы класса.
        /// <summary>
        /// Дополнить накладную данными.
        /// </summary>
        /// <param name="xmlActAdd">Акт по накладной.</param>
        public void AddAct(string xmlActAdd)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка дополнить входящую накладную '{0}' актом...", Description));

                addAct(xmlActAdd);

                Program.Logger.Info(this, "... попытка дополнить входящую накладную актом успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время дополнения входящей накладной актом произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Изменить статус накладной.
        /// </summary>
        /// <param name="newStatus">Новый статус.</param>
        public void ChangeStatus(InWaybillStatus newStatus)
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
        /// Изменить статус накладной.
        /// </summary>
        /// <param name="newStatus">Новый статус.</param>
        public void ChangeStatusOnRepeal(InWaybillStatus newStatus)
        {
            prevRepealStatus = StatusEnum;

            ChangeStatus(newStatus);
        }
        /// <summary>
        /// Установить идентификатор запроса, полученного в ответ на отправку акта по накладной.
        /// </summary>
        public void SetActReplyId(string replyId)
        {
            actReplyId = replyId;

            Program.Logger.Info(this, string.Format("У входящей накладной '{0}' установлен новый идентификатор запроса, полученного в ответ на отправку акта по накладной: '{1}'.", Description, actReplyId));
        }
        /// <summary>
        /// Установить идентификатор запроса, полученного в ответ на отправку акта отмены.
        /// </summary>
        public void SetRepealReplyId(string replyId)
        {
            repealReplyId = replyId;

            Program.Logger.Info(this, string.Format("У накладной '{0}' установлен новый идентификатор запроса, полученного в ответ на отправку отмены проведения: '{1}'.", Description, repealReplyId));
        }
        /// <summary>
        /// Добавить исходящий запрос на отмену проведения акта (xml-документ).
        /// </summary>
        /// <param name="repealXml"></param>
        public void AddRepealData(XmlDocument repealXml)
        {
            Program.Logger.Info(this, string.Format("Попытка добавить запрос на отмену проведения накладной (xml-документ) '{0}'...", Description));

            addRepealData(repealXml);

            Program.Logger.Info(this, string.Format("... добавление исходящего запроса на отмену проведения накладной (xml-документ) длиной {0} символов успешно завершено.", repealXml.OuterXml.Length));
        }
        #endregion Внешние методы класса.

        #region Внутренние методы класса.
        /// <summary>
        /// Установка значений "по умолчанию" для вновь добавленных объектов класса.
        /// </summary>
        /// <param name="streamingContext">Контекст</param>
        [OnDeserializing]
        private void setDefaultValues(StreamingContext streamingContext)
        {
            prevRepealStatus = InWaybillStatus.Partial;
        }
        #endregion Внутренние методы класса.

        #region Защищенные методы класса.
        /// <summary>
        /// Дополнить накладную данными.
        /// </summary>
        /// <param name="xmlActAdd">Акт по накладной.</param>
        protected virtual void addAct(string xmlActAdd)
        {
            xmlAct = xmlActAdd;
            lastChange = DateTime.Now.ToString("dd MMMM yyyy, HH:mm:ss");

            treeData = buildTreeData();
        }
        /// <summary>
        /// Изменить статус накладной.
        /// </summary>
        /// <param name="newStatus">Новый статус.</param>
        protected virtual void changeStatus(InWaybillStatus newStatus)
        {
            status = newStatus;
            lastChange = DateTime.Now.ToString("dd MMMM yyyy, HH:mm:ss");

            treeData = buildTreeData();
        }
        /// <summary>
        /// Добавить исходящий запрос на отмену проведения акта (xml-документ).
        /// </summary>
        /// <param name="repealXml"></param>
        protected virtual void addRepealData(XmlDocument repealXml)
        {
            xmlRepealBody = repealXml.OuterXml;
            lastChange = DateTime.Now.ToString("dd MMMM yyyy, HH:mm:ss");

            treeData = buildTreeData();
        }
        #endregion Защищенные методы класса.

        #region Переопределение базовых методов.
        /// <summary>
        /// Дополнить накладную данными.
        /// </summary>
        /// <param name="form">Документ о регистрации движения</param>
        protected override void addData(FormBRegInfo form)
        {
            if (StatusEnum != InWaybillStatus.Partial)
            {
                Program.Logger.Error(string.Format("В дополнении отказано. Дополнить данными можно только накладную, находящуюся в статусе '{0}'.", InWaybillStatus.Partial));

                return;
            }

            base.addData(form);

            changeStatus(InWaybillStatus.New);
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

            if (string.IsNullOrWhiteSpace(xmlAct))
            {
                Program.Logger.Info(this, "Акт по накладной пустой. Дополнения не требуется.");
            }
            else
            {
                Program.Logger.Info(this, "Попытка дополнения иерархических данных актом по накладной...");

                XmlDocument xmlForm = new XmlDocument();
                xmlForm.LoadXml(xmlAct);

                TreeData actTree = new TreeData(tree, "Акт по накладной", null);
                if (xmlForm.HasChildNodes) parseXmlNode(xmlForm, actTree);

                Program.Logger.Info(this, "... дополнение иерархических данных актом по накладной успешно завершено.");
            }

            Program.Logger.Info(this, "... построение (расширенное) иерархических данных успешно завершено.");

            #region Акт отмены.
            {
                if (string.IsNullOrWhiteSpace(xmlRepealBody))
                {
                    Program.Logger.Info(this, "Тело исходящего запроса отмены проведения (xml-документ) пустое. Дополнения не требуется.");
                }
                else
                {
                    Program.Logger.Info(this, "Попытка дополнения иерархических данных исходящим запросом отмены проведения (xml-документ)...");

                    XmlDocument xmlForm = new XmlDocument();
                    xmlForm.LoadXml(xmlRepealBody);

                    TreeData xmlRepeal = new TreeData(tree, "Исходящий запрос на отмену проведения накладной", null);

                    if (xmlForm.HasChildNodes) parseXmlNode(xmlForm, xmlRepeal);

                    Program.Logger.Info(this, "... дополнение иерархических данных исходящим запросом отмены проведения (xml-документ) успешно завершено.");
                }
            }
            #endregion Акт отмены.

            return tree;
        }
        /// <summary>
        /// Получить направление накладной (входящая или исходящая).
        /// </summary>
        /// <returns>Направление накладной (входящая или исходящая)</returns>
        protected override string getDirect()
        {
            return "Входящая";
        }
        #endregion Переопределение базовых методов.

        #region Внешние статические методы класса.
        /// <summary>
        /// Наименование документа.
        /// </summary>
        public static string NativeName
        {
            get { return "Входящая накладная"; }
        }


        #endregion Внешние статические методы класса.
    }
}
