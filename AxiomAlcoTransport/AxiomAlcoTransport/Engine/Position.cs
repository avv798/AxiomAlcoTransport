using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Алкогольная позиция в накладной (используется, в основном, для входящих накладных).
    /// </summary>
    public class Position
    {
        #region Внутренние объекты класса
        private readonly List<BoxInfo> boxInfos;
        #endregion

        #region Конструктор

        public Position()
        {
            boxInfos = new List<BoxInfo>();
        }
        #endregion
        #region Внешние объекты класса.

        [DisplayName("Идентификатор позиции"), ReadOnly(true), Browsable(true)]
        public string Identity { get; set; }

        [DisplayName("Идентификатор справки 'Б' позиции по документу регистрации движения"), ReadOnly(true), Browsable(true)]
        public string InformBRegId { get; set; }

        [DisplayName("Наименование"), ReadOnly(true), Browsable(true)]
        public string Title
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ShortName)) return FullName;
                
                return ShortName;
            }
        }

        [DisplayName("Короткое наименование"), ReadOnly(true), Browsable(true)]
        public string ShortName { get; set; }

        [DisplayName("Полное наименование"), ReadOnly(true), Browsable(true)]
        public string FullName { get; set; }

        [DisplayName("Код алкогольной продукции"), ReadOnly(true), Browsable(true)]
        public string AlcoCode { get; set; }
        
        [DisplayName("Ёмкость"), ReadOnly(true), Browsable(true)]
        public string Capacity { get; set; }

        [DisplayName("Крепость"), ReadOnly(true), Browsable(true)]
        public string Volume { get; set; }

        [DisplayName("Производитель"), ReadOnly(true), Browsable(true)]
        public string Producer { get; set; }

        [DisplayName("Количество по накладной"), ReadOnly(true), Browsable(true)]
        public decimal Quantity { get; set; }

        [DisplayName("Цена по накладной"), ReadOnly(true), Browsable(true)]
        public decimal Price { get; set; }

        [DisplayName("Общая цена по накладной"), ReadOnly(true), Browsable(true)]
        public decimal TotalPrice
        {
            get { return Quantity * Price; }
        }

        [DisplayName("Общая цена по реальному количеству"), ReadOnly(true), Browsable(true)]
        public decimal TotalRealPrice
        {
            get { return RealQuantity * Price; }
        }

        [DisplayName("Реальное количество"), ReadOnly(false), Browsable(true)]
        public decimal RealQuantity { get; set; }

        [DisplayName("Идентификатор справки 'А'"), ReadOnly(true), Browsable(true)]
        public string FormARegId { get; set; }

        [DisplayName("Идентификатор справки 'Б'"), ReadOnly(true), Browsable(true)]
        public string FormBRegId { get; set; }

        [DisplayName("Информация о накладной"), ReadOnly(true), Browsable(true)]
        public string WaybillInformation { get; set; }

        [DisplayName("Код продукта"), ReadOnly(true), Browsable(true)]
        public string ProductVCode { get; set; }

        [DisplayName("Номер входящей накладной"), ReadOnly(true), Browsable(true)]
        public string InWaybillNumber { get; set; }

        [DisplayName("Дата получения входящей накладной"), ReadOnly(true), Browsable(true)]
        public DateTime InWaybillCreateDate { get; set; }

        [DisplayName("Дата составления входящей накладной"), ReadOnly(true), Browsable(true)]
        public string InWaybillDate { get; set; }

        [DisplayName("Дата отгрузки по входящей накладной"), ReadOnly(true), Browsable(true)]
        public string InWaybillShippingDate { get; set; }

        [DisplayName("Отправитель накладной"), ReadOnly(true), Browsable(true)]
        public string InWaybillShipper { get; set; }
        
        /// <summary>
        /// Список штрих-кодов PDF-417
        /// Только чтение.
        /// </summary>
        [DisplayName("Список штрихкодов по группам"), ReadOnly(true), Browsable(true)]
        public List<BoxInfo> BoxInfos => boxInfos;

        #endregion Внешние объекты класса.

        #region Внешние методы класса.
        /// <summary>
        /// Проверить состав позиции.
        /// </summary>
        public void Check()
        {
            if (string.IsNullOrWhiteSpace(Identity)) throw new Exception("Элемент 'Identity' у алкогольной позиции не может быть пустым.");

            // Пока не очень понятно, нужна ли такая проверка.
            // if (string.IsNullOrWhiteSpace(InformBRegId)) throw new Exception("Элемент 'InformBRegId' у алкогольной позиции не может быть пустым.");

            if (string.IsNullOrWhiteSpace(ShortName)
                && string.IsNullOrWhiteSpace(FullName)) throw new Exception("Элемент 'Наименование' у алкогольной позиции не может быть пустым.");
            
            if (Quantity < 0) throw new Exception("Элемент 'Количество' у алкогольной позиции не может быть отрицательным.");
            if (RealQuantity < 0) throw new Exception("Элемент 'Реальное количество' у алкогольной позиции не может быть отрицательным.");
            if (RealQuantity > Quantity) throw new Exception("Элемент 'Реальное количество' у алкогольной позиции не может быть больше элемента 'Количество'.\r\n\r\n" +
                                                             "Примечание.\r\n" +
                                                             "Актом расхождения в ЕГАИС может оформляться только недостача продукции. " +
                                                             "Излишки поставленной продукции оформляются отдельной товарно-транспортной накладной.");
        }
        #endregion Внешние методы класса.
    }
}
