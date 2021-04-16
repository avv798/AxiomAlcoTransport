using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using DevExpress.Data.Helpers;
using DevExpress.DataProcessing;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Функционал позиции исходящей накладной.
    /// </summary>
    [Serializable]
    public class OutPosition
    {
        #region Защищенные объекты класса.
        /// <summary>
        /// Тело документа алкогольной продукции.
        /// Только чтение.
        /// </summary>
        protected readonly string xmlProductionBody;
        /// <summary>
        /// Код продукта.
        /// </summary>
        [OptionalField]
        protected string productVCode;
        [OptionalField]
        private List<BoxInfo> boxInfos;

        #endregion Защищенные объекты класса.

        #region Внешние объекты класса.

        [DisplayName("Идентификатор позиции"), ReadOnly(true), Browsable(true)]
        public int Identity { get; set; }

        [DisplayName("Наименование"), ReadOnly(true), Browsable(true)]
        public string Title { get; set; }

        [DisplayName("Код алкогольной продукции"), ReadOnly(true), Browsable(true)]
        public string AlcoCode { get; set; }

        [DisplayName("Код продукта"), ReadOnly(true), Browsable(true)]
        public string ProductVCode
        {
            get
            {
                if (string.IsNullOrWhiteSpace(productVCode))
                {
                    if (string.IsNullOrWhiteSpace(xmlProductionBody)) return string.Empty;

                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(xmlProductionBody);

                    if (xml["rap:Product"] == null) return string.Empty;

                    productVCode = ADocument.GetNodeValue("pref:ProductVCode", xml["rap:Product"]);
                }

                return productVCode;
            }
            set
            {
                productVCode = value;
            }
        }

        [DisplayName("Ёмкость"), ReadOnly(true), Browsable(true)]
        public string Capacity { get; set; }

        [DisplayName("Крепость"), ReadOnly(true), Browsable(true)]
        public string Volume { get; set; }

        [DisplayName("Количество"), ReadOnly(false), Browsable(true)]
        public decimal Quantity { get; set; }

        [DisplayName("Цена"), ReadOnly(false), Browsable(true)]
        public decimal Price { get; set; }

        [DisplayName("Цена с учётом количества"), ReadOnly(true), Browsable(true)]
        public decimal TotalPrice
        {
            get { return Quantity * Price; }
        }

        [DisplayName("Справка 'А' (идентификатор)"), ReadOnly(false), Browsable(true)]
        public string FormARegId { get; set; }

        [DisplayName("Справка 'Б' (идентификатор)"), ReadOnly(false), Browsable(true)]
        public string FormBRegId { get; set; }

        [DisplayName("Номер партии"), ReadOnly(false), Browsable(true)]
        public string Party { get; set; }

        [DisplayName("Производитель"), ReadOnly(true), Browsable(true)]
        public string ProducerTitle { get; set; }

        [DisplayName("Статус"), ReadOnly(true), Browsable(true)]
        public int Status
        {
            get { return checkWithSuppress(); }
        }

        /// <summary>
        /// Тело документа алкогольной продукции.
        /// Только чтение.
        /// </summary>
        [DisplayName("Тело документа алкогольной продукции."), ReadOnly(true), Browsable(false)]
        public string XmlProductionBody
        {
            get { return xmlProductionBody; }
        }

        /// <summary>
        /// Список штрих-кодов PDF-417
        /// Только чтение.
        /// </summary>
        [DisplayName("Список штрихкодов."), ReadOnly(false), Browsable(true)]
        public List<BoxInfo> BoxInfos {
            get { return boxInfos; }
            set { boxInfos = value; }
        }


        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected OutPosition()
        {
            
            xmlProductionBody = string.Empty;

            Identity = 0;
            Title = string.Empty;
            AlcoCode = string.Empty;
            ProductVCode = string.Empty;
            Capacity = string.Empty;
            Volume = string.Empty;
            Quantity = 0.0m;
            Price = 0.0m;
            FormARegId = string.Empty;
            FormBRegId = string.Empty;
            Party = string.Empty;
            ProducerTitle = string.Empty;
            boxInfos = new List<BoxInfo>();
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="production">Алкогольная продукция.</param>
        public OutPosition(Production production) : this()
        {
            xmlProductionBody = production.XmlBody;

            Identity = 0;
            Title = string.IsNullOrEmpty(production.ShortName) ? production.FullName : production.ShortName;
            AlcoCode = production.AlcCode;
            ProductVCode = production.ProductVCode;
            Capacity = production.Capacity;
            Volume = production.AlcVolume;
            Quantity = 1.0m;
            Price = 0.0m;
            FormARegId = string.Empty;
            FormBRegId = string.Empty;
            Party = string.Empty;
            ProducerTitle = production.ProducerName;
        }

        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="rests">Остаток.</param>
        /// <param name="boxInfosFromBarcodes"></param>
        public OutPosition(RestsPosition rests) : this()
        {
            xmlProductionBody = rests.XmlProductionBody;

            Identity = 0;
            Title = rests.Title;
            AlcoCode = rests.AlcoCode;
            ProductVCode = rests.ProductVCode;
            Capacity = rests.Capacity;
            Volume = rests.Volume;
            Quantity = 1.0m;
            Price = 0.0m;
            FormARegId = rests.FormARegId;
            FormBRegId = rests.FormBRegId;
            Party = string.Empty;
            ProducerTitle = rests.Producer;
            boxInfos = boxInfosFromBarcodes(rests.Barcodes);
        }

        private List<BoxInfo> boxInfosFromBarcodes(List<string> restsBarcodes)
        {
            var result = new List<BoxInfo>();
            var amcList = new List<Amc>();
            amcList.AddRange(restsBarcodes.Select(s => new Amc{Barcode = s}));
            result.Add(new BoxInfo(){AmcList = amcList});
            return result;
        }

        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="position">Позиция.</param>
        public OutPosition(OutPosition position) : this()
        {
            xmlProductionBody = position.XmlProductionBody;

            Identity = position.Identity;
            Title = position.Title;
            AlcoCode = position.AlcoCode;
            ProductVCode = position.ProductVCode;
            Capacity = position.Capacity;
            Volume = position.Volume;
            Quantity = position.Quantity;
            Price = position.Price;
            FormARegId = position.FormARegId;
            FormBRegId = position.FormBRegId;
            Party = position.Party;
            ProducerTitle = position.ProducerTitle;
            boxInfos = position.boxInfos;
        }
        #endregion Конструкторы класса.

        #region Внешние методы класса.
        /// <summary>
        /// Проверить позицию накладной.
        /// </summary>
        public void Check()
        {
            check();
        }
        /// <summary>
        /// Клонировать позицию.
        /// </summary>
        /// <returns>Новая позиция.</returns>
        public OutPosition Clone()
        {
            return clone();
        }
        /// <summary>
        /// Преобразовать исходящую позицию в общую.
        /// </summary>
        /// <returns>Позиция накладной.</returns>
        public Position Convert()
        {
            return convert();
        }
        #endregion Внешние методы класса.

        #region Защищенные методы класса.
        /// <summary>
        /// Проверка с подавлением исключений.
        /// </summary>
        /// <returns>Результат.</returns>
        protected virtual int checkWithSuppress()
        {
            try
            {
                check();

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// Проверить позицию накладной.
        /// </summary>
        protected virtual void check()
        {
            if (string.IsNullOrWhiteSpace(xmlProductionBody)) throw new Exception("Выбранный документ 'Алкогольная продукция' не может быть пустым.");

            if (Identity < 1) throw new Exception("Идентификатор позиции не может быть быть нулевым или отрицательным.");

            if (string.IsNullOrWhiteSpace(AlcoCode)) throw new Exception(string.Format("Код алкогольной продукции не может быть пустым (позиция №{0}).", Identity));

            if (Quantity <= 0.0m) throw new Exception(string.Format("Величина 'Количество' в позиции не может быть нулевой или отрицательной (позиция №{0}).", Identity));
            if (Quantity > 10000.0m) throw new Exception(string.Format("Величина 'Количество' в позиции превышает разумное значение (позиция №{0}).", Identity));

            if (Price <= 0.0m) throw new Exception(string.Format("Величина 'Цена' в позиции не может быть нулевой или отрицательной (позиция №{0}).", Identity));
            if (Price > 1000000000.0m) throw new Exception(string.Format("Величина 'Цена' в позиции превышает разумное значение (позиция №{0}).", Identity));

            if (FormARegId.Length > 50) throw new Exception(string.Format("Длина идентификатора справки 'А' превышает максимально допустимый размер (позиция №{0}).", Identity));
            if (string.IsNullOrWhiteSpace(FormARegId)) throw new Exception(string.Format("Идентификатор справки 'А' не может быть пустым.\r\n" +
                                                                                         "В позиции исходящей товарно-транспортной накладной " +
                                                                                         "обязательно нужно указывать регистрационный номер " +
                                                                                         "справки 'А' (позиция №{0}).", Identity));

            if (FormBRegId.Length > 50) throw new Exception(string.Format("Длина идентификатора справки 'B' превышает максимально допустимый размер (позиция №{0}).", Identity));
            if (string.IsNullOrWhiteSpace(FormBRegId)) throw new Exception(string.Format("Идентификатор справки 'Б' не может быть пустым.\r\n" +
                                                                                         "В позиции исходящей товарно-транспортной накладной " +
                                                                                         "обязательно нужно указывать идентификатор записи предыдущей отгрузки " +
                                                                                         "(по которой продукция поступила на склад) (позиция №{0}).", Identity));

            if (Party.Length > 50) throw new Exception(string.Format("Длина параметра 'Номер партии' превышает максимально допустимый размер (позиция №{0}).", Identity));
        }
        /// <summary>
        /// Клонировать позицию.
        /// </summary>
        /// <returns>Новая позиция.</returns>
        protected virtual OutPosition clone()
        {
            return new OutPosition(this);
        }
        /// <summary>
        /// Преобразовать исходящую позицию в общую.
        /// </summary>
        /// <returns>Позиция накладной.</returns>
        protected virtual Position convert()
        {
            Position position = new Position
                                    {
                                        Identity = Identity.ToString(CultureInfo.InvariantCulture),
                                        FullName = Title,
                                        ShortName = Title,
                                        AlcoCode = AlcoCode,
                                        ProductVCode = ProductVCode,
                                        Capacity = Capacity,
                                        Volume = Volume,
                                        Quantity = Quantity,
                                        Price = Price,
                                        FormARegId = FormARegId,
                                        FormBRegId = FormBRegId,
                                        Producer = ProducerTitle
                                    };
            if (boxInfos != null)
                BoxInfos.AddRange(boxInfos);

            return position;
        }
        #endregion Защищенные методы класса.
    }
}
