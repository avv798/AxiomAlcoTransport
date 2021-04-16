using System;
using System.ComponentModel;

namespace Axiom.AlcoTransport.Document
{
    /// <summary>
    /// Акт фиксации штрихкодов на балансе организации .
    /// </summary>
    [DisplayName(@"Акт фиксации штрихкодов на балансе организации "), Serializable]
    public class ActFixBarcode : Movement
    {
        #region Конструкторы класса.

        /// <summary>
        /// Конструктор класса "по Lумолчанию".
        /// </summary>
        public ActFixBarcode()
        {
        }

        /// <summary>
        /// Создать новый акт на основе существующего.
        /// </summary>
        /// <param name="exist">Существующий акт.</param>
        public ActFixBarcode(ActFixBarcode exist) : this()
        {
            fillData(exist);
        }

        #endregion Конструкторы класса.

        public override string ToString()
        {
            return base.ToString();
        }
    }
}