using System.ComponentModel;

namespace Axiom.AlcoTransport.Utility.Cheques
{
    /// <summary>
    /// Общий класс для позиции алкогольного и пивного чека.
    /// Абстрактный класс.
    /// </summary>
    public abstract class APosition
    {
        #region Внешние объекты класса.
        /// <summary>
        /// Цена.
        /// </summary>
        [DisplayName("Цена"), ReadOnly(true), Browsable(true)]
        public decimal Price { get; set; }
        /// <summary>
        /// Объём.
        /// </summary>
        [DisplayName("Объём"), ReadOnly(true), Browsable(true)]
        public decimal Volume { get; set; }
        /// <summary>
        /// Код "EAN".
        /// </summary>
        [DisplayName("Код EAN13"), ReadOnly(true), Browsable(true)]
        public string Ean { get; set; }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        protected APosition()
        {
            Price = 0.0m;
            Volume = 0.0m;
            Ean = string.Empty;
        }
        #endregion Конструкторы класса.
    }
}
