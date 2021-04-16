using System;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Акт постановки товара на баланс в торговом зале.
    /// </summary>
    [DisplayName("Акты постановки товара на баланс в торговом зале"), Serializable]
    public class ActChargeOnShop : Movement
    {
        #region Конструкторы класса.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public ActChargeOnShop()
        {
            
        }
        /// <summary>
        /// Создать новый акт на основе существующего.
        /// </summary>
        /// <param name="exist">Существующий акт.</param>
        public ActChargeOnShop(ActChargeOnShop exist) : this()
        {
            fillData(exist);
        }
        #endregion Конструкторы класса.
    }
}
