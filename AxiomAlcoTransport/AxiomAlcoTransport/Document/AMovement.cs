using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Документ движения товара.
    /// Абстрактный класс.
    /// </summary>
    [Serializable]
    public abstract class AMovement : ADocument
    {
        #region Защищенные объекты класса.
        /// <summary>
        /// Статус исходящей накладной.
        /// </summary>
        protected MovementStatus status;
        /// <summary>
        /// Идентификатор запроса, полученного в ответ на отправку акта.
        /// </summary>
        protected string sendReplyId;
        /// <summary>
        /// Последнее изменение.
        /// </summary>
        protected DateTime lastChange;
        /// <summary>
        /// Идентификатор в системе ФСРАР.
        /// Только чтение.
        /// </summary>
        protected readonly string identity;
        /// <summary>
        /// Номер.
        /// </summary>
        protected string number;
        /// <summary>
        /// Дата составления.
        /// </summary>
        protected DateTime date;
        /// <summary>
        /// Основание акта.
        /// </summary>
        protected string reason;
        /// <summary>
        /// Описание акта.
        /// </summary>
        protected string note;
        /// <summary>
        /// Список позиций.
        /// Только чтение.
        /// </summary>
        protected readonly List<MovePosition> movePositions;
        /// <summary>
        /// Тело исходящего xml-документа.
        /// </summary>
        protected string xmlOutBody;
        /// <summary>
        /// Регистрационный номер акта, который присваивается во время проведения акта на сервере ЕГАИС.
        /// </summary>
        [OptionalField]
        protected string egaisRegId;
        /// <summary>
        /// Идентификатор запроса, полученного в ответ на отправку акта отмены.
        /// </summary>
        [OptionalField]
        protected string repealReplyId;
        /// <summary>
        /// Тело запроса на отмену проведения акта.
        /// </summary>
        [OptionalField]
        private string xmlRepealBody;
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
        public MovementStatus StatusEnum
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
                    case MovementStatus.Partial: return "новый документ, заполнен частично";
                    case MovementStatus.New: return "новый документ, готов к отправке";
                    case MovementStatus.Sent: return "документ отправлен";
                    case MovementStatus.Confirmed: return "документ подтверждён ЕГАИС";
                    case MovementStatus.Rejected: return "документ отвергнут ЕГАИС";
                    case MovementStatus.Repealed: return "проведение документа отменёно";

                    default: return "неизвестный";
                }
            }
        }
        /// <summary>
        /// Идентификатор запроса, полученного в ответ на отправку документа.
        /// Только чтение.
        /// </summary>
        [DisplayName("Идентификатор запроса, полученного в ответ на отправку документа"), ReadOnly(true), Browsable(false)]
        public string SendReplyId
        {
            get { return sendReplyId; }
        }
        /// <summary>
        /// Последнее изменение.
        /// Только чтение.
        /// </summary>
        [DisplayName("Последнее изменение"), ReadOnly(true), Browsable(false)]
        public DateTime LastChange
        {
            get { return lastChange; }
        }
        /// <summary>
        /// Идентификатор в системе ФСРАР.
        /// Только чтение.
        /// </summary>
        [DisplayName("Идентификатор"), ReadOnly(true), Browsable(true)]
        public string Identity
        {
            get { return identity; }
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
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        /// <summary>
        /// Дата составления.
        /// </summary>
        [DisplayName("Дата составления (короткий формат)"), ReadOnly(true), Browsable(false)]
        public string DateShort
        {
            get { return date.ToString("yyyy-MM-dd"); }
        }
        /// <summary>
        /// Оcнование акта.
        /// </summary>
        [DisplayName("Оcнование акта"), ReadOnly(true), Browsable(true)]
        public string Reason
        {
            get { return reason; }
            set { reason = value; }
        }
        /// <summary>
        /// Комментарий.
        /// </summary>
        [DisplayName("Описание акта"), ReadOnly(true), Browsable(true)]
        public string Note
        {
            get { return note; }
            set { note = value; }
        }
        /// <summary>
        /// Список позиций.
        /// Только чтение.
        /// </summary>
        [DisplayName("Список позиций"), ReadOnly(true), Browsable(false)]
        public List<MovePosition> MovePositions
        {
            get { return movePositions; }
        }
        /// <summary>
        /// Тело исходящего xml-документа.
        /// Только чтение.
        /// </summary>
        [DisplayName("Исходящий документ"), ReadOnly(true), Browsable(false)]
        public string XmlOutBody
        {
            get { return xmlOutBody; }
        }
        /// <summary>
        /// Регистрационный номер акта, который присваивается во время проведения акта на сервере ЕГАИС.
        /// </summary>
        [DisplayName("Идентификатор ЕГАИС"), ReadOnly(true), Browsable(true)]
        public string EgaisRegId
        {
            get { return egaisRegId; }
        }
        /// <summary>
        /// Идентификатор запроса, полученного в ответ на отправку акта отмены.
        /// </summary>
        [DisplayName("Идентификатор запроса, полученного в ответ на отправку акта отмены."), ReadOnly(true), Browsable(false)]
        public string RepealReplyId
        {
            get { return repealReplyId; }
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
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected AMovement()
        {
            status = MovementStatus.Partial;
            sendReplyId = string.Empty;

            lastChange = DateTime.Now;

            identity = string.Format("{0}-{1}", DateTime.Now.ToString("yyMMddHHmmssfff"), Guid.NewGuid().ToString().Replace("-", "").ToUpper());
            number = string.Format("{0}", DateTime.Now.ToString("yyyyMMdd-HHmmssfff"));
            date = DateTime.Now;
            reason = string.Empty;
            note = string.Empty;

            movePositions = new List<MovePosition>();

            xmlOutBody = string.Empty;

            egaisRegId = string.Empty;
            repealReplyId = string.Empty;
            xmlRepealBody = string.Empty;

            VersionEgais = 2;
        }
        #endregion Конструкторы класса.

        #region Внешние методы класса.
        /// <summary>
        /// Установить идентификатор запроса, полученного в ответ на отправку накладной.
        /// </summary>
        public void SetSendReplyId(string replyId)
        {
            sendReplyId = replyId;

            Program.Logger.Info(this, string.Format("У документа движения товара '{0}' установлен новый идентификатор запроса, полученного в ответ на его отправку: '{1}'.", Description, sendReplyId));
        }
        /// <summary>
        /// Установить регистрационный номер акта, который присваивается во время проведения акта на сервере ЕГАИС.
        /// </summary>
        public void SetEgaisRegId(string regId)
        {
            egaisRegId = regId;

            Program.Logger.Info(this, string.Format("У документа движения товара '{0}' установлен новый идентификатор ЕГАИС, полученного в ответ на его отправку: '{1}'.", Description, egaisRegId));
        }
        /// <summary>
        /// Установить идентификатор запроса, полученного в ответ на отправку акта отмены.
        /// </summary>
        public void SetRepealReplyId(string replyId)
        {
            repealReplyId = replyId;

            Program.Logger.Info(this, string.Format("У документа движения товара '{0}' установлен новый идентификатор запроса, полученного в ответ на отправку отмены проведения: '{1}'.", Description, repealReplyId));
        }
        /// <summary>
        /// Изменить статус накладной.
        /// </summary>
        /// <param name="newStatus">Новый статус.</param>
        public void ChangeStatus(MovementStatus newStatus)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка изменить статус документа '{0}' с '{1}' на '{2}'...", Description, status, newStatus));

                changeStatus(newStatus);

                Program.Logger.Info(this, "... изменение статуса документа успешно завершено.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время изменения статуса произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Проверить документ и изменить (по необходимости) его статус.
        /// </summary>
        /// <param name="suppressException">Признак подавления исключения.</param>
        public void Check(bool suppressException = true)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка проверить документ '{0}'...", Description));

                check(suppressException);

                Program.Logger.Info(this, "... проверка документа успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время проверки документа произошла ошибка.", exception);

                if (!suppressException) throw;
            }
        }
        /// <summary>
        /// Добавить исходящий xml-документ.
        /// </summary>
        /// <param name="outXml"></param>
        public void AddOutData(XmlDocument outXml)
        {
            Program.Logger.Info(this, string.Format("Попытка добавить исходящий xml-документ для акта '{0}'...", Description));

            addOutData(outXml);

            Program.Logger.Info(this, string.Format("... добавление исходящего xml-документа длиной {0} символов успешно завершено.", outXml.OuterXml.Length));
        }
        /// <summary>
        /// Добавить исходящий запрос на отмену проведения акта (xml-документ).
        /// </summary>
        /// <param name="repealXml"></param>
        public void AddRepealData(XmlDocument repealXml)
        {
            Program.Logger.Info(this, string.Format("Попытка добавить запрос на отмену проведения акта (xml-документ) '{0}'...", Description));

            addRepealData(repealXml);

            Program.Logger.Info(this, string.Format("... добавление исходящего запросf на отмену проведения акта (xml-документ) длиной {0} символов успешно завершено.", repealXml.OuterXml.Length));
        }
        #endregion Внешние методы класса.

        #region Защищенные методы класса.
        /// <summary>
        /// Изменить статус накладной.
        /// </summary>
        /// <param name="newStatus">Новый статус.</param>
        protected virtual void changeStatus(MovementStatus newStatus)
        {
            status = newStatus;
            lastChange = DateTime.Now;

            treeData = buildTreeData();
        }
        /// <summary>
        /// Проверить документ и изменить (по необходимости) его статус.
        /// </summary>
        /// <param name="suppressException">Признак подавления исключения.</param>
        protected virtual void check(bool suppressException)
        {
            try
            {
                if ((StatusEnum == MovementStatus.Partial) || (StatusEnum == MovementStatus.New))
                {
                    if (string.IsNullOrWhiteSpace(Identity)) throw new Exception("Идентификатор не может быть пустым.");
                    if (string.IsNullOrWhiteSpace(Number)) throw new Exception("Номер не может быть пустым.");

                    if (string.IsNullOrWhiteSpace(DateShort)) throw new Exception("Дата составления не может быть пустой.");
                    if (Date > DateTime.Now.AddMonths(3)) throw new Exception("Дата составления не может быть настолько больше сегодняшнего числа.");
                    if (Date < DateTime.Now.AddMonths(-12)) throw new Exception("Дата составления не может быть настолько меньше сегодняшнего числа.");

                    switch (GetType().Name)
                    {
                        case "ActChargeOn":
                        {
                            if (string.IsNullOrWhiteSpace(Reason)) throw new Exception("Основание для составления акта должно быть заполнено.");

                            List<string> list = new List<string>();

                            int totalQuantity = 0;

                            foreach (MovePosition position in MovePositions)
                            {
                                list.AddRange(position.PDF417Codes);

                                totalQuantity += position.PDF417Codes.Count;
                            }

                            if (list.Distinct().ToList().Count < totalQuantity) throw new Exception("Обнаружены повторяющиеся отсканированные двумерные штрих-коды в общем списке позиций акта.");

                            break;
                        }
                        case "ActChargeOff":
                        case "ActChargeOnShop":
                        case "ActChargeOffShop":
                        {
                            if (string.IsNullOrWhiteSpace(Reason)) throw new Exception("Основание для составления акта должно быть заполнено.");
                            break;
                        }
                    }

                    if (movePositions.Count == 0) throw new Exception("Список позиций акта движения товара пустой. Отправка акта не имеет смысла.");

                    foreach (MovePosition movePosition in MovePositions)
                    {
                        movePosition.Check();
                    }

                    changeStatus(MovementStatus.New);
                }
            }
            catch (Exception)
            {
                changeStatus(MovementStatus.Partial);

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
            lastChange = DateTime.Now;

            treeData = buildTreeData();
        }
        /// <summary>
        /// Добавить исходящий запрос на отмену проведения акта (xml-документ).
        /// </summary>
        /// <param name="repealXml"></param>
        protected virtual void addRepealData(XmlDocument repealXml)
        {
            xmlRepealBody = repealXml.OuterXml;
            lastChange = DateTime.Now;

            treeData = buildTreeData();
        }
        /// <summary>
        /// Заполнить свойства акта на основе существующего.
        /// Метод не виртуальный.
        /// </summary>
        /// <param name="exist">Существующий акт.</param>
        protected void fillData(AMovement exist)
        {
            status = MovementStatus.Partial;
            sendReplyId = string.Empty;

            lastChange = DateTime.Now;

            reason = exist.Reason;
            note = exist.Note;

            movePositions.Clear();

            foreach (MovePosition position in exist.MovePositions)
            {
                movePositions.Add(new MovePosition(position));
            }

            xmlOutBody = string.Empty;

            check(true);
        }
        #endregion Защищенные методы класса.

        #region Переопределение базовых методов.
        /// <summary>
        /// Получение описания объекта.
        /// </summary>
        /// <returns>Описание.</returns>
        protected override string GetDescription()
        {
            return string.Format("'{0}' (Identity = '{1}', Number = '{2}', Date = '{3}')", GetType().FullName, Identity, Number, Date.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        /// <summary>
        /// Построить иерархию данных.
        /// </summary>
        /// <returns>Иерархические данные.</returns>
        protected override TreeData buildTreeData()
        {
            Program.Logger.Info(this, string.Format("Построение (расширенное) иерархических данных в документе '{0}'...", Description));

            TreeData tree = base.buildTreeData();

            // ReSharper disable once UnusedVariable
            TreeData ta = new TreeData(tree, "Статус документа", StatusNote);
            // ReSharper disable once UnusedVariable
            TreeData tb = new TreeData(tree, "Идентификатор", Identity);
            // ReSharper disable once UnusedVariable
            TreeData tc = new TreeData(tree, "Номер", Number);
            // ReSharper disable once UnusedVariable
            TreeData td = new TreeData(tree, "Дата составления", Date.ToString("dd MMMM yyyy (dddd), HH:mm:ss"));
            // ReSharper disable once UnusedVariable
            TreeData tf = new TreeData(tree, "Последнее изменение", lastChange.ToString("dd MMMM yyyy (dddd), HH:mm:ss"));

            #region Исходящий xml-документ.
            {
                if (string.IsNullOrWhiteSpace(xmlOutBody))
                {
                    Program.Logger.Info(this, "Тело исходящего xml-документа пустое. Дополнения не требуется.");
                }
                else
                {
                    Program.Logger.Info(this, "Попытка дополнения иерархических данных исходящим xml-документом...");

                    XmlDocument xmlForm = new XmlDocument();
                    xmlForm.LoadXml(xmlOutBody);

                    TreeData xmlOut = new TreeData(tree, "Исходящий документ", null);

                    if (xmlForm.HasChildNodes) parseXmlNode(xmlForm, xmlOut);

                    Program.Logger.Info(this, "... дополнение иерархических данных исходящим xml-документом успешно завершено.");
                }
            }
            #endregion Исходящий xml-документ.

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

                    TreeData xmlRepeal = new TreeData(tree, "Исходящий запрос на отмену проведения акта", null);

                    if (xmlForm.HasChildNodes) parseXmlNode(xmlForm, xmlRepeal);

                    Program.Logger.Info(this, "... дополнение иерархических данных исходящим запросом отмены проведения (xml-документ) успешно завершено.");
                }
            }
            #endregion Акт отмены.

            Program.Logger.Info(this, "... построение (расширенное) иерархических данных успешно завершено.");

            return tree;
        }
        #endregion Переопределение базовых методов.
    }
}
