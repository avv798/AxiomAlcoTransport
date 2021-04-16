using System;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Документ возврата товара из торгового зала.
    /// </summary>
    [DisplayName("Акты возврата товара из торгового зала"), Serializable]
    public class TransferFromShop : Movement
    {
        #region Конструкторы класса.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public TransferFromShop()
        {
            
        }
        /// <summary>
        /// Создать новый акт на основе существующего.
        /// </summary>
        /// <param name="exist">Существующий акт.</param>
        public TransferFromShop(TransferFromShop exist) : this()
        {
            fillData(exist);
        }
        #endregion Конструкторы класса.
    }
}
