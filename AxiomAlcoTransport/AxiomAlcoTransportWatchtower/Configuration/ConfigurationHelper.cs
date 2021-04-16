using System;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace Axiom.AlcoTransport.Watchtower.Configuration
{
    /// <summary>
    /// Общий класс, реализующий вспомогательный функционал.
    /// </summary>
    public static class ConfigurationHelper
    {
        #region Внешние статические методы класса.
        /// <summary>
        /// Проверка наличия записанной конфигурации.
        /// </summary>
        /// <param name="filename">Полное имя файла для загрузки.</param>
        /// <returns>Признак наличия записанной конфигурации.</returns>
        public static bool ConfigurationAlreadyExists(string filename)
        {
            try
            {
                if (!File.Exists(filename))
                {
                    throw new FileNotFoundException("Файл конфигурации не найден.", filename);
                }

                return true;
            }
            catch (Exception exception)
            {
                Trace.TraceError(string.Format("ConfigurationHelper: Во время проверки конфигурации произошла ошибка: '{0}'.", exception));

                return false;
            }
        }
        /// <summary>
        /// Сохранить конфигурацию.
        /// </summary>
        /// <param name="data">Данные для сохранения.</param>
        /// <param name="filename">Полное имя файла для сохранения.</param>
        public static void SaveConfiguration(ConfigurationData data, string filename)
        {
            try
            {
                XmlDocument xml = new XmlDocument();

                XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
                xml.AppendChild(declaration);

                XmlNode main = xml.CreateElement("AxiomAlcoTransport");
                xml.AppendChild(main);

                XmlNode cfg = xml.CreateElement("Configuration");
                main.AppendChild(cfg);

                addParameter(xml, cfg, "ProcessDateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                addParameter(xml, cfg, "FsrarId", data.FsrarId);
                addParameter(xml, cfg, "Inn", data.Inn);
                addParameter(xml, cfg, "UtmAddress", data.UtmAddress);
                addParameter(xml, cfg, "UtmPort", data.UtmPort);
                addParameter(xml, cfg, "UtmTimeoutShort", data.UtmTimeoutShort.ToString("D"));
                addParameter(xml, cfg, "UtmTimeoutLong", data.UtmTimeoutLong.ToString("D"));
                addParameter(xml, cfg, "IntervalDataRequest", data.IntervalDataRequest.ToString("D"));
                addParameter(xml, cfg, "PathLocalDB", data.PathToLocalDatabase);

                xml.Save(filename);
            }
            catch (Exception exception)
            {
                Trace.TraceError(string.Format("ConfigurationHelper: Во время сохранения конфигурации произошла ошибка: '{0}'.", exception));

                throw;
            }
        }
        /// <summary>
        /// Загрузить конфигурацию.
        /// </summary>
        /// <param name="filename">Полное имя файла для загрузки.</param>
        public static ConfigurationData LoadConfiguration(string filename)
        {
            try
            {
                if (!ConfigurationAlreadyExists(filename)) throw new Exception(string.Format("Не найден файл конфигурации приложения ('{0}').", filename));

                XmlDocument xml = new XmlDocument();
                xml.Load(filename);

                if (xml.DocumentElement == null) throw new Exception("Не найдена главная xml-нода в файле конфигурации.");
                if (xml.DocumentElement["Configuration"] == null) throw new Exception("Не найдена xml-нода 'Configuration' в файле конфигурации.");

                ConfigurationData data = new ConfigurationData();

                foreach (XmlNode node in xml.DocumentElement["Configuration"].ChildNodes)
                {
                    switch (node.Name.ToLower())
                    {
                        case "fsrarid": { data.FsrarId = node.InnerText; break; }
                        case "inn": { data.Inn = node.InnerText; break; }
                        case "utmaddress": { data.UtmAddress = node.InnerText; break; }
                        case "utmport": { data.UtmPort = node.InnerText; break; }
                        case "utmtimeoutshort": { data.UtmTimeoutShort = convertToInt(node.InnerText); break; }
                        case "utmtimeoutlong": { data.UtmTimeoutLong = convertToInt(node.InnerText); break; }
                        case "intervaldatarequest": { data.IntervalDataRequest = convertToInt(node.InnerText); break; }
                        case "pathlocaldb": { data.PathToLocalDatabase = node.InnerText; break; }
                    }
                }

                return data;
            }
            catch (Exception exception)
            {
                Trace.TraceError(string.Format("ConfigurationHelper: Во время загрузки конфигурации произошла ошибка: '{0}'.", exception));

                return new ConfigurationData();
            }
        }
        #endregion Внешние статические методы класса.
        
        #region Внутренние статические методы класса.
        /// <summary>
        /// Добавить ноду-параметр к указанной XML-ноде.
        /// </summary>
        /// <param name="xmlDocument">XML-документ.</param>
        /// <param name="xmlNode">XML-нода.</param>
        /// <param name="name">Наименование параметра.</param>
        /// <param name="value">Значение параметра.</param>
        private static void addParameter(XmlDocument xmlDocument, XmlNode xmlNode, string name, string value)
        {
            if (xmlDocument == null) throw new Exception("Object 'xmlDocument' is null.");
            if (xmlNode == null) throw new Exception("Object 'xmlNode' is null.");

            XmlNode parameter = xmlDocument.CreateElement(name);
            parameter.InnerText = value;
            xmlNode.AppendChild(parameter);
        }
        /// <summary>
        /// Преобразование строки.
        /// </summary>
        /// <param name="str">Исходная строка.</param>
        /// <param name="errorValue">Значение в случае неверной строки.</param>
        /// <returns>Результат.</returns>
        private static int convertToInt(string str, int errorValue = -1)
        {
            try
            {
                return Convert.ToInt32(str);
            }
            catch (Exception)
            {
                return errorValue;
            }
        }
        #endregion Внутренние статические методы класса.
    }
}
