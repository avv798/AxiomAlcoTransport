using System;
using System.ComponentModel;

namespace Axiom.AlcoTransport.Document
{
    /// <summary>
    /// Акт фиксации штрихкодов на балансе организации .
    /// </summary>
    [DisplayName(@"Акт отмены фиксации штрихкодов на балансе организации "), Serializable]
    public class ActUnFixBarcode : Movement
    {
        #region Конструкторы класса.

        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public ActUnFixBarcode()
        {
        }

        /// <summary>
        /// Создать новый акт на основе существующего.
        /// </summary>
        /// <param name="exist">Существующий акт.</param>
        public ActUnFixBarcode(ActUnFixBarcode exist) : this()
        {
            fillData(exist);
        }

        #endregion Конструкторы класса.
    }
}