using System;
using System.Collections.Generic;
using System.Xml;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Остатки на дату в торговом зале.
    /// </summary>
    [DisplayName("Справочник остатков в торговом зале"), Serializable]
    public class ShopRests : BaseRests
    {
        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected ShopRests()
        {
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="node">XML-нода для создания.</param>
        public ShopRests(XmlNode node) : base(node)
        {
            VersionEgais = 2;
        }
        #endregion Конструкторы класса.

        #region Внешние статические методы класса.
        /// <summary>
        /// Наименование документа.
        /// </summary>
        public static string NativeName
        {
            get { return "Справочник остатков в торговом зале"; }
        }
        #endregion Внешние статические методы класса.

        #region Переопределение базовых методов.
        /// <summary>
        /// Получение описания объекта.
        /// </summary>
        /// <returns>Описание.</returns>
        protected override string GetDescription()
        {
            return string.Format("{0} {1}", base.GetDescription(), "(торговый зал)");
        }
        /// <summary>
        /// Получить позиции.
        /// </summary>
        /// <returns>Список позиций.</returns>
        protected override List<RestsPosition> getPositions()
        {
            List<RestsPosition> positions = new List<RestsPosition>();

            if (string.IsNullOrWhiteSpace(xmlBody)) return positions;

            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlBody);

            if (xml[xmlNodeName] == null) throw new Exception(string.Format("Значение ноды '{0}' не может быть пустым.", xmlNodeName));
            if (xml[xmlNodeName]["rst:Products"] == null) throw new Exception("Значение 'rst:Products' не может быть пустым.");

            XmlNode node = xml[xmlNodeName]["rst:Products"];

            int i = 0;

            foreach (XmlNode item in node.ChildNodes)
            {
                if (item["rst:Product"] == null) throw new Exception("Значение 'rst:Product' не может быть пустым.");

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

                if (string.IsNullOrWhiteSpace(position.FormARegId))
                {
                    position.FormARegId = getNodeValue("rst:InformF1RegId", item);
                }

                if (string.IsNullOrWhiteSpace(position.FormBRegId))
                {
                    position.FormBRegId = getNodeValue("rst:InformF2RegId", item);
                }

                if (item["rst:Product"]["pref:Producer"] != null)
                {
                    XmlNode child = item["rst:Product"]["pref:Producer"].FirstChild;

                    if (child != null)
                    {
                        position.Producer = getNodeValue("oref:ShortName", child);

                        if (string.IsNullOrWhiteSpace(position.Producer)) position.Producer = getNodeValue("oref:FullName", child);
                    }
                }

                if (item["rst:Product"]["pref:Importer"] != null)
                {
                    XmlNode child = item["rst:Product"]["pref:Importer"].FirstChild;

                    if (child != null)
                    {
                        position.Importer = getNodeValue("oref:ShortName", child);

                        if (string.IsNullOrWhiteSpace(position.Importer)) position.Importer = getNodeValue("oref:FullName", child);
                    }
                }

                positions.Add(position);

                ++i;
            }

            return positions;
        }
        #endregion Переопределение базовых методов.
    }
}
