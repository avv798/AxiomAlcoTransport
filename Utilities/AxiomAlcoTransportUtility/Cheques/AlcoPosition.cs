using System.ComponentModel;

namespace Axiom.AlcoTransport.Utility.Cheques
{
    /// <summary>
    /// Алкогольная позиция.
    /// </summary>
    public class AlcoPosition : APosition
    {
        #region Внешние объекты класса.
        /// <summary>
        /// Код "PDF-417".
        /// </summary>
        [DisplayName("Код PDF-417"), ReadOnly(true), Browsable(true)]
        public string Barcode { get; set; }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public AlcoPosition()
        {
            Barcode = string.Empty;
        }
        #endregion Конструкторы класса.
    }
}
