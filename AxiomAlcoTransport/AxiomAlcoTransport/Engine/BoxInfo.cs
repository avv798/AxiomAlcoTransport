using System.Collections.Generic;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    public class BoxInfo
    {
        /// <summary>
        /// Группа штрихкодов
        /// </summary>
        [DisplayName("Номер коробки"), ReadOnly(true), Browsable(true)]
        public string BoxNumber { get; set; }

        /// <summary>
        /// Список штрихкодов
        /// </summary>
        [DisplayName("Список штрихкодов"), ReadOnly(true), Browsable(true)]
        public List<Amc> AmcList { get; set; } = new List<Amc>();
    }
}