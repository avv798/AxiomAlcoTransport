using System;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Акт списания товара.
    /// </summary>
    [DisplayName("Акты списания товара"), Serializable]
    public class ActChargeOff : Movement
    {
        #region Конструкторы класса.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public ActChargeOff()
        {
            
        }
        /// <summary>
        /// Создать новый акт на основе существующего.
        /// </summary>
        /// <param name="exist">Существующий акт.</param>
        public ActChargeOff(ActChargeOff exist) : this()
        {
            fillData(exist);
        }
        #endregion Конструкторы класса.
    }
}
