using System.ComponentModel;

namespace Axiom.AlcoTransport.Engine.BarcodeCheck
{
    public class BarcodePosition
    {
        [DisplayName(@"Идентификатор позиции")]
        [ReadOnly(true)]
        [Browsable(true)]
        public string Identity { get; set; }

        [DisplayName(@"Идентификатор справки 'Б' позиции по документу регистрации движения")]
        [ReadOnly(true)]
        [Browsable(true)]
        public string InformBRegId { get; set; }

        [DisplayName(@"Полное наименование")]
        [ReadOnly(true)]
        [Browsable(true)]
        public string FullName { get; set; }

        [DisplayName(@"Код алкогольной продукции")]
        [ReadOnly(true)]
        [Browsable(true)]
        public string AlcoCode { get; set; }

        [DisplayName(@"Штрихкод")]
        [ReadOnly(true)]
        [Browsable(true)]
        public string Barcode { get; set; }

        [DisplayName(@"Введенный штрихкод")]
        [ReadOnly(true)]
        [Browsable(true)]
        public string BarcodeInput { get; set; }

    }
}