using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    public class Amc
    {
        /// <summary>
        /// Штрихкод
        /// </summary>
        [DisplayName("Штрихкод"), ReadOnly(true), Browsable(true)]
        public string Barcode { get; set; }  
        /// <summary>
        /// Фактический штрихкод
        /// </summary>
        [DisplayName("Фактический штрихкод"), ReadOnly(false), Browsable(true)]
        public string RealBarcode { get; set; }
    }
}