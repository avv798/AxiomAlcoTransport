using System;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Документ передачи товара в торговый зал.
    /// </summary>
    [DisplayName("Акты передачи товара в торговый зал"), Serializable]
    public class TransferToShop : Movement
    {
        #region Конструкторы класса.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public TransferToShop()
        {
            
        }
        /// <summary>
        /// Создать новый акт на основе существующего.
        /// </summary>
        /// <param name="exist">Существующий акт.</param>
        public TransferToShop(TransferToShop exist) : this()
        {
            fillData(exist);
        }
        #endregion Конструкторы класса.

    }
}
