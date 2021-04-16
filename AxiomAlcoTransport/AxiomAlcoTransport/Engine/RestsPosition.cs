using System.Collections.Generic;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Алкогольная позиция в остатках.
    /// </summary>
    public class RestsPosition
    {
        #region Внешние объекты класса.

        [DisplayName("Номер позиции"), ReadOnly(true), Browsable(true)]
        public int Id { get; set; }

        [DisplayName("Дата, на которую указаны остатки"), ReadOnly(true), Browsable(true)]
        public string PositionRestsDate { get; set; }

        [DisplayName("Количество"), ReadOnly(true), Browsable(true)]
        public decimal Quantity { get; set; }

        [DisplayName("Идентификатор справки 'А'"), ReadOnly(true), Browsable(true)]
        public string FormARegId { get; set; }

        [DisplayName("Идентификатор справки 'Б'"), ReadOnly(true), Browsable(true)]
        public string FormBRegId { get; set; }

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

        [DisplayName("Код вида продукции"), ReadOnly(true), Browsable(true)]
        public string ProductVCode { get; set; }

        [DisplayName("Производитель"), ReadOnly(true), Browsable(true)]
        public string Producer { get; set; }

        [DisplayName("Импортёр"), ReadOnly(true), Browsable(true)]
        public string Importer { get; set; }
        
        [DisplayName("Xml-тело исходного документа"), ReadOnly(true), Browsable(false)]
        public string XmlProductionBody { get; set; }
        /// <summary>
        /// Список штрихкодов
        /// </summary>
        [DisplayName("Список штрихкодов"), ReadOnly(true), Browsable(true)]
        public List<string> Barcodes { get; set; } = new List<string>();

        #endregion Внешние объекты класса.
    }
}
