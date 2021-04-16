using System;
using System.Collections.Generic;
using System.Xml;
using System.ComponentModel;
using Axiom.AlcoTransport.Document;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Остатки на дату.
    /// </summary>
    [Serializable]
    public abstract class ARests : ADocument
    {
        #region Защищенные объекты класса.

        /// <summary>
        /// Дата (as is), на которую получены остатки.
        /// Только чтение.
        /// </summary>
        protected readonly string restsDate;

        /// <summary>
        /// Имя базовой xml-ноды для синтаксического анализа исходного xml-файла.
        /// </summary>
        protected string xmlNodeName
        {
            get
            {
                if (GetType() == typeof(Rests)) return ((VersionEgais == 1) ? "ns:ReplyRests" : "ns:ReplyRests_v2");
                if (GetType() == typeof(ShopRests)) return "ns:ReplyRestsShop_v2";
                if (GetType() == typeof(BCodeRests)) return "ns:ReplyRestBCode";

                throw new Exception(string.Format("Класс '{0}' не поддерживается в данной версии.", GetType().FullName));
            }
        }

        #endregion Защищенные объекты класса.

        #region Внешние объекты класса.

        /// <summary>
        /// Дата (as is), на которую получены остатки.
        /// Только чтение.
        /// </summary>
        [DisplayName("Дата (as is), на которую получены остатки"), ReadOnly(true), Browsable(true)]
        public string RestsDate
        {
            get { return restsDate; }
        }

        #endregion Внешние объекты класса.

        #region Конструкторы класса.

        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected ARests()
        {
            ReplyId = string.Empty;

            restsDate = string.Empty;
        }

        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="node">XML-нода для создания.</param>
        protected ARests(XmlNode node) : this()
        {
            xmlBody = node.OuterXml;

            restsDate = TryParseDateTime(getNodeValue("rst:RestsDate", node));
            if (string.IsNullOrWhiteSpace(RestsDate))
                throw new Exception("Значение 'rst:RestsDate' не может быть пустым.");
        }

        #endregion Конструкторы класса.

        #region Переопределение базовых методов.

        /// <summary>
        /// Получение описания объекта.
        /// </summary>
        /// <returns>Описание.</returns>
        protected override string GetDescription()
        {
            return string.Format("{0}: '{1}'", GetType().FullName, RestsDate);
        }

        #endregion Переопределение базовых методов.

        #region Внешние методы класса.

        /// <summary>
        /// Получить позиции накладной.
        /// </summary>
        /// <param name="suppressException">Подавить исключение в случае ошибки.</param>
        /// <returns>Список позиций.</returns>
        public List<RestsPosition> GetPositions(bool suppressException = false)
        {
            try
            {
                Program.Logger.Info(this,
                    string.Format("Попытка получить позиции документа-остатков '{0}'...", Description));

                List<RestsPosition> positions = getPositions();

                Program.Logger.Info(this,
                    string.Format("... получение позиций документа-остатков (в количестве {0} шт.) успешно завершено.",
                        positions.Count));

                return positions;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время получения позиций документа-остатков произошла ошибка.", exception);

                if (suppressException)
                {
                    Program.Logger.Warn(this, "Исключение подавлено. Далее передан пустой список позиций.");

                    return new List<RestsPosition>();
                }

                throw;
            }
        }

        #endregion Внешние методы класса.

        #region Защищенные методы класса.

        /// <summary>
        /// Получить позиции.
        /// </summary>
        /// <returns>Список позиций.</returns>
        protected virtual List<RestsPosition> getPositions()
        {
            List<RestsPosition> positions = new List<RestsPosition>();

            if (string.IsNullOrWhiteSpace(xmlBody)) return positions;

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlBody);

            if (xml[xmlNodeName] == null)
                throw new Exception(string.Format("Значение ноды '{0}' не может быть пустым.", xmlNodeName));

            XmlNode node;
            int i = 0;

            switch (VersionEgais)
            {
                case 1:

                    #region Version 1...

                    if (xml[xmlNodeName]["rst:Products"] == null)
                        throw new Exception("Значение 'rst:Products' не может быть пустым.");

                    node = xml[xmlNodeName]["rst:Products"];
                    foreach (XmlNode item in node.ChildNodes)
                    {
                        if (item["rst:Product"] == null)
                            throw new Exception("Значение 'rst:Product' не может быть пустым.");

                        RestsPosition position = new RestsPosition
                        {
                            Id = i + 1,
                            PositionRestsDate = RestsDate,
                            Quantity = convertToDecimal(getNodeValue("rst:Quantity", item)),
                            FormARegId = getNodeValue("rst:InformARegId", item),
                            FormBRegId = getNodeValue("rst:InformBRegId", item),
                            AlcoCode = getNodeValue("pref:AlcCode", item["rst:Product"]),
                            FullName = getNodeValue("pref:FullName", item["rst:Product"]),
                            ShortName = getNodeValue("pref:ShortName", item["rst:Product"]),
                            Capacity = getNodeValue("pref:Capacity", item["rst:Product"]),
                            Volume = getNodeValue("pref:AlcVolume", item["rst:Product"]),
                            ProductVCode = getNodeValue("pref:ProductVCode", item["rst:Product"]),
                            XmlProductionBody = item["rst:Product"].OuterXml
                        };

                        if (item["rst:Product"]["pref:Producer"] != null)
                        {
                            position.Producer = getNodeValue("oref:ShortName", item["rst:Product"]["pref:Producer"]);

                            if (string.IsNullOrWhiteSpace(position.Producer))
                                position.Producer = getNodeValue("oref:FullName", item["rst:Product"]["pref:Producer"]);
                        }

                        if (item["rst:Product"]["pref:Importer"] != null)
                        {
                            position.Importer = getNodeValue("oref:ShortName", item["rst:Product"]["pref:Importer"]);

                            if (string.IsNullOrWhiteSpace(position.Importer))
                                position.Importer = getNodeValue("oref:FullName", item["rst:Product"]["pref:Importer"]);
                        }

                        positions.Add(position);

                        ++i;
                    }
                    break;

                    #endregion Version 1...

                case 2:
                    if (xml[xmlNodeName]["rst:Products"] == null)
                        throw new Exception("Значение 'rst:Products' не может быть пустым.");

                    node = xml[xmlNodeName]["rst:Products"];
                    foreach (XmlNode item in node.ChildNodes)
                    {
                        if (item["rst:Product"] == null)
                            throw new Exception("Значение 'rst:Product' не может быть пустым.");

                        RestsPosition position = new RestsPosition
                        {
                            Id = i + 1,
                            PositionRestsDate = RestsDate,
                            Quantity = convertToDecimal(getNodeValue("rst:Quantity", item)),
                            FormARegId = getNodeValue("rst:InformF1RegId", item),
                            FormBRegId = getNodeValue("rst:InformF2RegId", item),
                            AlcoCode = getNodeValue("pref:AlcCode", item["rst:Product"]),
                            FullName = getNodeValue("pref:FullName", item["rst:Product"]),
                            ShortName = getNodeValue("pref:ShortName", item["rst:Product"]),
                            Capacity = getNodeValue("pref:Capacity", item["rst:Product"]),
                            Volume = getNodeValue("pref:AlcVolume", item["rst:Product"]),
                            ProductVCode = getNodeValue("pref:ProductVCode", item["rst:Product"]),
                            XmlProductionBody = item["rst:Product"].OuterXml
                        };

                        if (item["rst:Product"]["pref:Producer"] != null)
                        {
                            XmlNode child = item["rst:Product"]["pref:Producer"].FirstChild;

                            if (child != null)
                            {
                                position.Producer = getNodeValue("oref:ShortName", child);

                                if (string.IsNullOrWhiteSpace(position.Producer))
                                    position.Producer = getNodeValue("oref:FullName", child);
                            }
                        }

                        if (item["rst:Product"]["pref:Importer"] != null)
                        {
                            XmlNode child = item["rst:Product"]["pref:Importer"].FirstChild;

                            if (child != null)
                            {
                                position.Importer = getNodeValue("oref:ShortName", child);

                                if (string.IsNullOrWhiteSpace(position.Importer))
                                    position.Importer = getNodeValue("oref:FullName", child);
                            }
                        }

                        positions.Add(position);

                        ++i;
                    }
                    break;
                default:

                    node = xml[xmlNodeName]["rst:MarkInfo"];
                    if (node == null) throw new Exception("Значение 'rst:MarkInfo' не может быть пустым.");
                    RestsPosition restPosition = new RestsPosition
                    {
                        Id = 1,
                        PositionRestsDate = RestsDate,
                        FormBRegId = getNodeValue("rst:Inform2RegId", node),
                    };

                    foreach (XmlNode amc in node.ChildNodes)
                    {
                        restPosition.Barcodes.Add(amc.InnerText);
                    }

                    positions.Add(restPosition);

                    break;
            }

            return positions;
        }

        #endregion Защищенные методы класса.
    }
}