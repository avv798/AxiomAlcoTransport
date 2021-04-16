using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Функционал корректора и переводчика.
    /// </summary>
    public class Language
    {
        #region Защищенные объекты класса.
        /// <summary>
        /// Словарь.
        /// Только чтение.
        /// </summary>
        protected readonly Dictionary<string, string> dictionary;
        /// <summary>
        /// Справочники.
        /// Только чтение.
        /// </summary>
        protected readonly Dictionary<string, string> reference;
        #endregion Защищенные объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public Language()
        {
            Program.Logger.Info(this, "Попытка создать объект...");

            dictionary = new Dictionary<string, string>();
            reference = new Dictionary<string, string>();

            load();

            Program.Logger.Info(this, string.Format("... объект успешно создан; размер словаря " +
                                                    "(количество слов в основном словаре и в справочнике): {0}/{1}.",
                                                    dictionary.Count, reference.Count));
        }
        #endregion Конструкторы класса.

        #region Внешние методы класса.
        /// <summary>
        /// Перевести слово.
        /// </summary>
        /// <param name="word">Слово для перевода.</param>
        /// <param name="removePrefix">Удалить префикс (перед двоеточием).</param>
        /// <returns>Перевод.</returns>
        public string Translate(string word, bool removePrefix = true)
        {
            return translate(word, removePrefix);
        }
        /// <summary>
        /// Перевести слово из справочника.
        /// </summary>
        /// <param name="word">Слово для перевода.</param>
        /// <returns>Перевод.</returns>
        public string TranslateReference(string word)
        {
            return translateReference(word);
        }
        #endregion Внешние методы класса.

        #region Защищенные методы класса.
        /// <summary>
        /// Перевести слово.
        /// </summary>
        /// <param name="word">Слово для перевода.</param>
        /// <param name="removePrefix">Удалить префикс (перед двоеточием).</param>
        /// <returns>Перевод.</returns>
        protected virtual string translate(string word, bool removePrefix)
        {
            if (string.IsNullOrWhiteSpace(word)) return word;

            if (removePrefix)
            {
                if (word.Contains(":"))
                {
                    word = word.Remove(0, word.IndexOf(":", StringComparison.Ordinal) + 1);
                }
            }

            return (dictionary.ContainsKey(word.ToLower())) ? dictionary[word.ToLower()] : word;
        }
        /// <summary>
        /// Перевести слово из справочника.
        /// </summary>
        /// <param name="word">Слово для перевода.</param>
        /// <returns>Перевод.</returns>
        protected virtual string translateReference(string word)
        {
            if (string.IsNullOrWhiteSpace(word)) return word;

            return (reference.ContainsKey(word.ToLower())) ? reference[word.ToLower()] : word;
        }
        /// <summary>
        /// Загрузить словарь.
        /// </summary>
        protected void load()
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузить словарь ...");

                string filename = Program.GetAppSetting("languageFilename");

                if (!File.Exists(filename))
                {
                    Program.Logger.Error(this, string.Format("Файл словаря '{0}' не найден.", filename));

                    return;
                }

                XmlDocument xml = new XmlDocument();
                xml.Load(filename);

                if (xml.DocumentElement == null) return;
                if (xml.DocumentElement["language"] == null) return;

                foreach (XmlNode node in xml.DocumentElement["language"].ChildNodes)
                {
                    if (node.Name.ToLower() != "word") continue;

                    string key = ADocument.GetAttributeValue("original", node).ToLower();
                    string value = ADocument.GetAttributeValue("translate", node);

                    if (string.IsNullOrWhiteSpace(key)) continue;
                    if (string.IsNullOrWhiteSpace(value)) continue;

                    dictionary[key] = value;
                }

                if (xml.DocumentElement["reference"] == null) return;

                foreach (XmlNode node in xml.DocumentElement["reference"].ChildNodes)
                {
                    if (node.Name.ToLower() != "word") continue;

                    string key = ADocument.GetAttributeValue("original", node).ToLower();
                    string value = ADocument.GetAttributeValue("translate", node);

                    if (string.IsNullOrWhiteSpace(key)) continue;
                    if (string.IsNullOrWhiteSpace(value)) continue;

                    reference[key] = value;
                }

                Program.Logger.Info(this, string.Format("... загрузка из файла '{0}' успешно завершена.", filename));
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время загрузки словаря произошла ошибка.", exception);
            }
        }
        #endregion Защищенные методы класса.
    }
}
