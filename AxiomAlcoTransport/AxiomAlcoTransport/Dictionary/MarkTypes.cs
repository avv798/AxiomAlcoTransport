using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Функционал списка типов акцизных марок.
    /// </summary>
    public class MarkTypes
    {
        #region Защищенные объекты класса.
        /// <summary>
        /// Словарь.
        /// </summary>
        protected List<MarkType> dictionary;
        #endregion Защищенные объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Словарь.
        /// Создаётся во время первого вызова.
        /// Только чтение.
        /// </summary>
        public List<MarkType> Dictionary
        {
            get
            {
                if (dictionary == null)
                {
                    dictionary = new List<MarkType>();

                    load();
                }

                return dictionary;
            }
        }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public MarkTypes()
        {
            Program.Logger.Info(this, "Попытка создать объект...");

            dictionary = null;

            Program.Logger.Info(this, "... объект успешно создан.");
        }
        #endregion Конструкторы класса.

        #region Защищенные методы класса.
        /// <summary>
        /// Загрузить словарь.
        /// </summary>
        protected void load()
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузить словарь типов марок...");

                if (dictionary == null) throw new Exception("Объект словаря типов марок не создан.");

                string filename = Program.GetAppSetting("markTypesFilename");

                if (!File.Exists(filename))
                {
                    Program.Logger.Error(this, string.Format("Файл словаря типов марок '{0}' не найден.", filename));

                    return;
                }

                XmlDocument xml = new XmlDocument();
                xml.Load(filename);

                if (xml.DocumentElement == null) return;
                if (xml.DocumentElement["MarkTypes"] == null) return;

                foreach (XmlNode node in xml.DocumentElement["MarkTypes"].ChildNodes)
                {
                    if (node.Name.ToLower() != "marktype") continue;

                    string code = ADocument.GetAttributeValue("code", node);
                    string title = ADocument.GetAttributeValue("title", node);

                    if (string.IsNullOrWhiteSpace(code)) continue;
                    if (string.IsNullOrWhiteSpace(title)) continue;

                    dictionary.Add(new MarkType(code, title));
                }

                Program.Logger.Info(this, string.Format("... загружено типов марок: {0} шт...", dictionary.Count));

                Program.Logger.Info(this, string.Format("... загрузка словаря типов марок из файла '{0}' успешно завершена.", filename));
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время загрузки словаря типов марок произошла ошибка.", exception);
            }
        }
        #endregion Защищенные методы класса.
    }
}
