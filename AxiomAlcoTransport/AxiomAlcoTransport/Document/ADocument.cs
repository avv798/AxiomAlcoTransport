using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Xml;

namespace Axiom.AlcoTransport
{
    /// <summary>
    ///     Предок всех документов.
    ///     Абстрактный класс.
    /// </summary>
    [Serializable]
    public abstract class ADocument
    {
        #region Внутренние объекты класса.

        /// <summary>
        ///     Версия документа.
        /// </summary>
        [OptionalField] private int versionEgais;

        #endregion Внутренние объекты класса.

        #region Конструкторы класса.

        /// <summary>
        ///     Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected ADocument()
        {
            surrogateId = Guid.NewGuid();
            createDateTime = DateTime.Now;
            treeData = null;
            xmlBody = string.Empty;
            AlreadyReading = false;
            Url = string.Empty;
            ReplyId = string.Empty;
            additionalComment = string.Empty;
            versionEgais = 1;
        }

        #endregion Конструкторы класса.

        #region Внешние методы класса.

        /// <summary>
        ///     Установить дополнительный комментарий документа.
        /// </summary>
        /// <param name="comment">Дополнительный комментарий.</param>
        public void SetAdditionalComment(string comment)
        {
            if (string.IsNullOrWhiteSpace(comment)) return;

            var commentUpdates = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {comment}";

            additionalComment = string.IsNullOrWhiteSpace(additionalComment)
                ? commentUpdates
                : $"{additionalComment} \r\n\r\n{commentUpdates}";

        }

        #endregion Внешние методы класса.

        #region Защищенные объекты класса.

        /// <summary>
        ///     Иерархические данные.
        ///     Формируются динамически с возможным изменением от версии к версии.
        /// </summary>
        [NonSerialized] protected TreeData treeData;

        /// <summary>
        ///     Суррогатный идентификатор.
        ///     Только чтение.
        /// </summary>
        protected readonly Guid surrogateId;

        /// <summary>
        ///     Дата и время создания.
        ///     Только чтение.
        /// </summary>
        protected readonly DateTime createDateTime;

        /// <summary>
        ///     Оригинальное XML-тело документа.
        /// </summary>
        protected string xmlBody;

        /// <summary>
        ///     Дополнительный комментарий к документу.
        /// </summary>
        [OptionalField] protected string additionalComment;

        #endregion Защищенные объекты класса.

        #region Внешние объекты класса.

        /// <summary>
        ///     Суррогатный идентификатор.
        ///     Только чтение.
        /// </summary>
        [DisplayName("Суррогатный идентификатор")]
        [ReadOnly(true)]
        [Browsable(false)]
        public Guid SurrogateId => surrogateId;

        /// <summary>
        ///     Дата и время создания.
        ///     Только чтение.
        /// </summary>
        [DisplayName("Дата и время получения")]
        [ReadOnly(true)]
        [Browsable(true)]
        public DateTime CreateDateTime => createDateTime;

        /// <summary>
        ///     Признак того, что документ прочитан (обработан).
        /// </summary>
        [DisplayName("Прочитано")]
        [ReadOnly(true)]
        [Browsable(false)]
        public bool AlreadyReading { get; set; }

        /// <summary>
        ///     Адрес, с которого получен документ.
        /// </summary>
        [DisplayName("Исходный адрес")]
        [ReadOnly(true)]
        [Browsable(true)]
        public string Url { get; set; }

        /// <summary>
        ///     Идентификатор запроса, в ответ на который пришёл данный документ.
        ///     Данный идентификатор может отсутствовать (например, во входящих накладных).
        /// </summary>
        [DisplayName("Идентификатор запроса, в ответ на который пришёл данный документ")]
        [ReadOnly(true)]
        [Browsable(false)]
        public string ReplyId { get; set; }

        /// <summary>
        ///     Оригинальное XML-тело документа.
        ///     Только чтение.
        /// </summary>
        [DisplayName("Оригинальное XML-тело документа")]
        [ReadOnly(true)]
        [Browsable(false)]
        public string XmlBody => xmlBody;

        /// <summary>
        ///     Описание объекта.
        ///     Только чтение.
        /// </summary>
        [DisplayName("Описание документа")]
        [ReadOnly(true)]
        [Browsable(true)]
        public string Description => GetDescription();

        /// <summary>
        ///     Описание объекта.
        ///     Только чтение.
        /// </summary>
        [DisplayName("Описание документа")]
        [ReadOnly(true)]
        [Browsable(true)]
        public string PartiallyTranslatedDescription
        {
            get
            {
                var desc = GetDescription();

                desc = desc.Replace("Identity", Program.Language.Translate("Identity"))
                    .Replace("Number", Program.Language.Translate("Number"))
                    .Replace("Date", Program.Language.Translate("Date"))
                    .Replace("WBRegId", Program.Language.Translate("WBRegId"));

                return desc;
            }
        }

        /// <summary>
        ///     Тип объекта (с переводом).
        ///     Только чтение.
        /// </summary>
        [DisplayName("Тип документа")]
        [ReadOnly(true)]
        [Browsable(true)]
        public string DocumentType => Program.Language.TranslateReference(GetType().Name);

        /// <summary>
        ///     Наименование файла.
        ///     Только чтение.
        /// </summary>
        [DisplayName("Наименование файла")]
        [ReadOnly(true)]
        [Browsable(false)]
        public string FileName => $"{GetType().FullName}.{surrogateId}.{FileStorage.NativeExtension}";

        /// <summary>
        ///     Иерархические данные.
        ///     Только чтение.
        /// </summary>
        [DisplayName("Иерархические данные")]
        [ReadOnly(true)]
        [Browsable(false)]
        public TreeData TreeData
        {
            get
            {
                if (treeData != null) return treeData;

                treeData = buildTreeData();

                return treeData;
            }
        }

        /// <summary>
        ///     Дополнительный комментарий к документу.
        ///     Только чтение.
        /// </summary>
        [DisplayName("Дополнительный комментарий")]
        [ReadOnly(true)]
        [Browsable(true)]
        public string AdditionalComment => additionalComment;

        /// <summary>
        ///     Версия документа.
        /// </summary>
        [DisplayName("Версия документа")]
        [ReadOnly(true)]
        [Browsable(true)]
        public int VersionEgais
        {
            get => versionEgais == 0 ? 1 : versionEgais;
            set => versionEgais = value;
        }

        #endregion Внешние объекты класса.

        #region Защищенные методы класса.

        /// <summary>
        ///     Получить описание объекта.
        /// </summary>
        /// <returns>Описание.</returns>
        protected virtual string GetDescription()
        {
            return
                $"Тип: '{GetType().FullName}'; адрес: '{Url}'; дата и время создания: '{CreateDateTime:yyyy-MM-dd HH:mm:ss.fff}'";
        }

        /// <summary>
        ///     Получить значение ноды.
        ///     Если нода не будет найдена, будет возвращено значение "string.Empty".
        /// </summary>
        /// <param name="nodeName">Наименование ноды.</param>
        /// <param name="node">Родительская нода.</param>
        /// <returns>Значение ноды.</returns>
        protected static string getNodeValue(string nodeName, XmlNode node)
        {
            if (node == null) return string.Empty;

            try
            {
                if (node[nodeName] == null) return string.Empty;

                return node[nodeName].InnerText;
            }
            catch (Exception exception)
            {
                Program.Logger.Error($"Во время поиска ноды '{nodeName}' произошла ошибка.",
                    exception);

                return string.Empty;
            }
        }

        /// <summary>
        ///     Построить иерархию данных.
        /// </summary>
        /// <returns>Иерархические данные.</returns>
        protected virtual TreeData buildTreeData()
        {
            Program.Logger.Info(this,
                $"Попытка построения иерархических данных в документе '{Description}'...");

            var tree = new TreeData(null, null);

            #region Служебная информация...

            var serviceInfo = new TreeData(tree, "Служебная информация о документе", null);
            // ReSharper disable once UnusedVariable
            var ta = new TreeData(serviceInfo, "Дата и время получения документа",
                CreateDateTime.ToString("dd MMMM yyyy (dddd), HH:mm:ss"));
            // ReSharper disable once UnusedVariable
            var tb = new TreeData(serviceInfo, "Внутренний идентификатор документа", SurrogateId.ToString());
            // ReSharper disable once UnusedVariable
            var tc = new TreeData(serviceInfo, "Тип документа", GetType().FullName);
            // ReSharper disable once UnusedVariable
            var td = new TreeData(serviceInfo, "Версия документа (ЕГАИС)", $"v{VersionEgais}");
            // ReSharper disable once UnusedVariable
            var te = new TreeData(serviceInfo, "Наименование файла в базе данных", FileName);
            // ReSharper disable once UnusedVariable
            var tf = new TreeData(serviceInfo, "Исходный адрес (URL), с которого загружен документ", Url);
            // ReSharper disable once UnusedVariable
            var tg = new TreeData(serviceInfo, "Идентификатор запроса (если есть), в ответ на который получен документ",
                ReplyId);

            #endregion Служебная информация...

            if (string.IsNullOrWhiteSpace(xmlBody)) return tree;

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xmlBody);

            if (xmlDocument.HasChildNodes) parseXmlNode(xmlDocument, tree);

            Program.Logger.Info(this, "... построение иерархических данных успешно завершено.");

            return tree;
        }

        /// <summary>
        ///     Провести синтаксический анализ ноды.
        /// </summary>
        /// <param name="node">Нода.</param>
        /// <param name="tree">Иерархические данные.</param>
        protected virtual void parseXmlNode(XmlNode node, TreeData tree)
        {
            if (node.HasChildNodes)
            {
                foreach (XmlNode child in node.ChildNodes)
                    if (child.HasChildNodes)
                    {
                        if (child.ChildNodes.Count == 1 && child.ChildNodes[0].NodeType == XmlNodeType.Text)
                        {
                            var name = child.Name;

                            #region Fucking EGAIS...

                            if (name.ToLower() == "wb:identity")
                                if (child.ParentNode != null && child.ParentNode.Name.ToLower() == "wb:position")
                                    name += "Position";

                            #endregion Fucking EGAIS...

                            // ReSharper disable once UnusedVariable
                            var row = new TreeData(tree, name, child.InnerText);
                        }
                        else
                        {
                            var next = new TreeData(tree, child.Name, string.Empty);

                            parseXmlNode(child, next);
                        }
                    }
                    else
                    {
                        // ReSharper disable once UnusedVariable
                        var row = new TreeData(tree, child.Name, child.InnerText);
                    }
            }
            else
            {
                // ReSharper disable once UnusedVariable
                var row = new TreeData(tree, node.Name, node.InnerText);
            }
        }

        /// <summary>
        ///     Создать составной идентификатор для документа.
        /// </summary>
        /// <param name="identity">Идентификатор.</param>
        /// <param name="number">Номер.</param>
        /// <param name="clientRegId">Идентификатор организации.</param>
        /// <param name="date">Дата составления.</param>
        /// <returns></returns>
        protected virtual string createCompositeId(string identity, string number, string clientRegId, string date)
        {
            if (string.IsNullOrWhiteSpace(identity)
                && string.IsNullOrWhiteSpace(number)
                && string.IsNullOrWhiteSpace(clientRegId)
                && string.IsNullOrWhiteSpace(date))
                throw new Exception("Ошибка при создании составного идентификатора для документа.");

            return
                $"InVinoVeritas-{identity ?? string.Empty}-{number ?? string.Empty}-{date ?? string.Empty}-{clientRegId ?? string.Empty}";
        }

        /// <summary>
        ///     Создать составной идентификатор для документа.
        /// </summary>
        /// <param name="identity">Идентификатор.</param>
        /// <param name="number">Номер.</param>
        /// <param name="clientRegId">Идентификатор организации.</param>
        /// <param name="date">Дата составления.</param>
        /// <returns></returns>
        protected virtual string createCompositeId(string identity, string number, string clientRegId, DateTime date)
        {
            return createCompositeId(identity, number, clientRegId, date.ToString("yyyy-MM-dd"));
        }

        /// <summary>
        ///     Преобразовать строку в число.
        /// </summary>
        /// <param name="str">Строка.</param>
        /// <returns>Результат.</returns>
        protected virtual decimal convertToDecimal(string str)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(str)) throw new Exception("Строка для преобразования пустая.");

                return Convert.ToDecimal(str.Trim().Replace(" ", "").Replace(",", "."),
                    new NumberFormatInfo {NumberDecimalSeparator = ".", NumberGroupSeparator = string.Empty});
            }
            catch (Exception exception)
            {
                Program.Logger.Error(this,
                    $"При преобразовании строки '{str}' в число произошла ошибка '{exception}'.");

                throw;
            }
        }

        #endregion Защищенные методы класса.

        #region Внешние статические методы класса.

        /// <summary>
        ///     Получить значение атрибута.
        ///     Если атрибут не будет найден, будет возвращено значение "string.Empty".
        /// </summary>
        /// <param name="attributeName">Наименование атрибута.</param>
        /// <param name="node">Нода.</param>
        /// <returns>Значение атрибута.</returns>
        public static string GetAttributeValue(string attributeName, XmlNode node)
        {
            if (node == null
                || node.Attributes == null
                || node.Attributes.Count == 0)
                return string.Empty;

            try
            {
                return node.Attributes[attributeName].InnerText;
            }
            catch (Exception exception)
            {
                Program.Logger.Error($"Во время поиска атрибута '{attributeName}' произошла ошибка.",
                    exception);

                return string.Empty;
            }
        }

        /// <summary>
        ///     Попытаться разобрать строку, содержащую дату и время.
        /// </summary>
        /// <param name="raw">Исходная строка.</param>
        /// <returns>Возможно, разобранная строка.</returns>
        public static string TryParseDateTime(string raw)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(raw)) return raw;

                if (raw.StartsWith("20")
                    && raw.Contains("T")
                    && raw.Contains(":"))
                {
                    var parse = Convert.ToDateTime(raw);

                    return parse.ToString("dd MMMM yyyy, HH:mm:ss");
                }

                return raw;
            }
            catch (Exception exception)
            {
                Program.Logger.Error(
                    $"Во время синтаксического анализа строки '{raw}' произошла ошибка.", exception);

                return raw;
            }
        }

        /// <summary>
        ///     Получить значение ноды.
        ///     Если нода не будет найдена, будет возвращено значение "string.Empty".
        /// </summary>
        /// <param name="nodeName">Наименование ноды.</param>
        /// <param name="node">Родительская нода.</param>
        /// <returns>Значение ноды.</returns>
        public static string GetNodeValue(string nodeName, XmlNode node)
        {
            return getNodeValue(nodeName, node);
        }

        #endregion Внешние статические методы класса.
    }
}