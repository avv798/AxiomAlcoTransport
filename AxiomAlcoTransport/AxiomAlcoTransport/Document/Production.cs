using System;
using System.Runtime.Serialization;
using System.Xml;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Алкогольная продукция ("AP").
    /// </summary>
    [Serializable]
    public class Production : ADocument
    {
        #region Защищенные объекты класса.
        /// <summary>
        /// Идентификатор организации в системе ФСРАР.
        /// Только чтение.
        /// </summary>
        protected readonly string alcCode;
        /// <summary>
        /// Ёмкость.
        /// Только чтение.
        /// </summary>
        protected readonly string capacity;
        /// <summary>
        /// Крепость.
        /// Только чтение.
        /// </summary>
        protected readonly string alcVolume;
        /// <summary>
        /// Полное наименование.
        /// Только чтение.
        /// </summary>
        protected readonly string fullName;
        /// <summary>
        /// Короткое наименование.
        /// Только чтение.
        /// </summary>
        protected readonly string shortName;
        /// <summary>
        /// Производитель.
        /// Только чтение.
        /// </summary>
        protected readonly string producerName;
        /// <summary>
        /// Импортёр.
        /// Только чтение.
        /// </summary>
        protected readonly string importerName;
        /// <summary>
        /// Тип алкогольной продукции.
        /// Только чтение.
        /// </summary>
        [OptionalField]
        private readonly string productionType;
        /// <summary>
        /// Код продукта.
        /// </summary>
        [OptionalField]
        protected string productVCode;
        #endregion Защищенные объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Идентификатор организации в системе ФСРАР.
        /// Только чтение.
        /// </summary>
        [DisplayName("Идентификатор в системе ФСРАР"), ReadOnly(true), Browsable(true)]
        public string AlcCode
        {
            get { return alcCode; }
        }
        /// <summary>
        /// Ёмкость.
        /// Только чтение.
        /// </summary>
        [DisplayName("Ёмкость"), ReadOnly(true), Browsable(true)]
        public string Capacity
        {
            get { return capacity; }
        }
        /// <summary>
        /// Крепость.
        /// Только чтение.
        /// </summary>
        [DisplayName("Крепость"), ReadOnly(true), Browsable(true)]
        public string AlcVolume
        {
            get { return alcVolume; }
        }
        /// <summary>
        /// Полное наименование.
        /// Только чтение.
        /// </summary>
        [DisplayName("Полное наименование"), ReadOnly(true), Browsable(true)]
        public string FullName
        {
            get { return fullName; }
        }
        /// <summary>
        /// Короткое наименование.
        /// Только чтение.
        /// </summary>
        [DisplayName("Короткое наименование"), ReadOnly(true), Browsable(true)]
        public string ShortName
        {
            get { return shortName; }
        }
        /// <summary>
        /// Производитель.
        /// Только чтение.
        /// </summary>
        [DisplayName("Производитель"), ReadOnly(true), Browsable(true)]
        public string ProducerName
        {
            get { return producerName; }
        }
        /// <summary>
        /// Импортёр.
        /// Только чтение.
        /// </summary>
        [DisplayName("Импортёр"), ReadOnly(true), Browsable(true)]
        public string ImporterName
        {
            get { return importerName; }
        }
        /// <summary>
        /// Тип алкогольной продукции.
        /// Только чтение.
        /// </summary>
        [DisplayName("Тип продукции"), ReadOnly(true), Browsable(true)]
        public string ProductionTypeDescription
        {
            get { return string.IsNullOrWhiteSpace(productionType) ? "Алкогольная продукция" : productionType; }
        }
        /// <summary>
        /// Код продукта.
        /// Только чтение.
        /// </summary>
        [DisplayName("Код продукта"), ReadOnly(true), Browsable(true)]
        public string ProductVCode
        {
            get
            {
                try
                {
                    // По идее, сие значение определяется в конструкторе.
                    // Следовательно данный код не работает, как правило.
                    // Оставлен для совместимости старых версий документов.

                    if (string.IsNullOrWhiteSpace(productVCode))
                    {
                        if (string.IsNullOrWhiteSpace(xmlBody)) return string.Empty;

                        XmlDocument xml = new XmlDocument();
                        xml.LoadXml(xmlBody);

                        if (xml["rap:Product"] == null) return string.Empty;

                        productVCode = getNodeValue("pref:ProductVCode", xml["rap:Product"]);
                    }

                    return productVCode;
                }
                catch (Exception exception)
                {
                    Program.Logger.Error(this, string.Format("Во время получения кода алкогольной продукции произошла ошибка: '{0}'.", exception));

                    return string.Empty;
                }
            }
        }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected Production()
        {
            ReplyId = string.Empty;

            alcCode = string.Empty;
            capacity = string.Empty;
            alcVolume = string.Empty;
            fullName = string.Empty;
            shortName = string.Empty;
            producerName = string.Empty;
            importerName = string.Empty;

            productVCode = string.Empty;
        }

        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="node">XML-нода для создания.</param>
        /// <param name="version">Версия документа.</param>
        /// <param name="productionType">Тип алкогольной продукции.</param>
        public Production(XmlNode node, int version, ProductionType productionType) : this()
        {
            xmlBody = node.OuterXml;

            VersionEgais = version;

            switch (productionType)
            {
                case ProductionType.Alcohol: { this.productionType = "Алкогольная продукция"; break; }
                case ProductionType.Spirit: { this.productionType = "Спирт"; break; }
                case ProductionType.SpiritContainer: { this.productionType = "Спиртосодержащая продукция"; break; }

                default: throw new Exception(string.Format("Неизвестный тип алкогольной продукции ('{0}').", productionType));
            }

            if (VersionEgais == 1)
            {
                alcCode = getNodeValue("pref:AlcCode", node);
                if (string.IsNullOrWhiteSpace(AlcCode)) throw new Exception("Значение 'pref:AlcCode' не может быть пустым.");

                fullName = getNodeValue("pref:FullName", node);
                shortName = getNodeValue("pref:ShortName", node);

                capacity = getNodeValue("pref:Capacity", node);
                alcVolume = getNodeValue("pref:AlcVolume", node);
                productVCode = getNodeValue("pref:ProductVCode", node);

                producerName = getNodeValue("oref:ShortName", node["pref:Producer"]);
                if (string.IsNullOrWhiteSpace(producerName)) producerName = getNodeValue("oref:FullName", node["pref:Producer"]);

                importerName = getNodeValue("oref:ShortName", node["pref:Importer"]);
                if (string.IsNullOrWhiteSpace(importerName)) importerName = getNodeValue("oref:FullName", node["pref:Importer"]);
            }
            else
            {
                alcCode = getNodeValue("pref:AlcCode", node);
                if (string.IsNullOrWhiteSpace(AlcCode)) throw new Exception("Значение 'pref:AlcCode' не может быть пустым.");

                fullName = getNodeValue("pref:FullName", node);
                shortName = getNodeValue("pref:ShortName", node);

                capacity = getNodeValue("pref:Capacity", node);
                alcVolume = getNodeValue("pref:AlcVolume", node);
                productVCode = getNodeValue("pref:ProductVCode", node);

                if ((node["pref:Producer"] != null) && (node["pref:Producer"].FirstChild != null))
                {
                    producerName = getNodeValue("oref:ShortName", node["pref:Producer"].FirstChild);
                    if (string.IsNullOrWhiteSpace(producerName)) producerName = getNodeValue("oref:FullName", node["pref:Producer"].FirstChild);
                }

                if ((node["pref:Importer"] != null) && (node["pref:Importer"].FirstChild != null))
                {
                    importerName = getNodeValue("oref:ShortName", node["pref:Importer"].FirstChild);
                    if (string.IsNullOrWhiteSpace(importerName)) importerName = getNodeValue("oref:FullName", node["pref:Importer"].FirstChild);
                }
            }
        }
        #endregion Конструкторы класса.

        #region Защищенные методы класса.
        #endregion Защищенные методы класса.

        #region Переопределение базовых методов.
        /// <summary>
        /// Получение описания объекта.
        /// </summary>
        /// <returns>Описание.</returns>
        protected override string GetDescription()
        {
            return string.Format("{0}: '{1}' (AlcoCode = '{2}')", GetType().FullName, FullName, AlcCode);
        }
        #endregion Переопределение базовых методов.

        #region Внешние статические методы класса.
        /// <summary>
        /// Наименование документа.
        /// </summary>
        public static string NativeName
        {
            get { return "Справочник алкогольной продукции"; }
        }
        #endregion Внешние статические методы класса.
    }
}
