using System.ComponentModel;

namespace Axiom.AlcoTransport.Utility.Cheques
{
    /// <summary>
    /// Пивная позиция.
    /// </summary>
    public class BeerPosition : APosition
    {
        #region Внешние объекты класса.
        [DisplayName("Наименование продукции"), ReadOnly(true), Browsable(true)]
        public string Title { get; set; }
        [DisplayName("Код вида продукции"), ReadOnly(true), Browsable(true)]
        public string Code { get; set; }
        [DisplayName("Крепость (%)"), ReadOnly(true), Browsable(true)]
        public decimal AlcoStrength { get; set; }
        [DisplayName("Количество"), ReadOnly(true), Browsable(true)]
        public int Count { get; set; }
        [DisplayName("Общая цена"), ReadOnly(true), Browsable(true)]
        public decimal TotalPrice
        {
            get { return Count * Price; }
        }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public BeerPosition()
        {
            Title = string.Empty;
            Code = string.Empty;
            AlcoStrength = 0.0m;
            Count = 0;
        }
        #endregion Конструкторы класса.
    }
}
