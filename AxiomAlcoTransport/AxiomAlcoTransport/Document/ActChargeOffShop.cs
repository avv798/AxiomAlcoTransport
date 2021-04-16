using System;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Акт списания товара из торгового зала.
    /// </summary>
    [DisplayName("Акты списания товара из торгового зала"), Serializable]
    public class ActChargeOffShop : Movement
    {
        #region Конструкторы класса.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public ActChargeOffShop()
        {
            
        }
        /// <summary>
        /// Создать новый акт на основе существующего.
        /// </summary>
        /// <param name="exist">Существующий акт.</param>
        public ActChargeOffShop(ActChargeOffShop exist) : this()
        {
            fillData(exist);
        }
        #endregion Конструкторы класса.
    }
}
