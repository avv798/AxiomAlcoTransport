namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Типы документов движения товара.
    /// </summary>
    public enum MovementType
    {
        /// <summary>
        /// Неизвестный тип документа.
        /// </summary>
        Unknown,
        /// <summary>
        /// Акт постановки на баланс.
        /// </summary>
        ActChargeOn,
        /// <summary>
        /// Акт списания.
        /// </summary>
        ActChargeOff,
        /// <summary>
        /// Акт постановки на баланс в торговом зале.
        /// </summary>
        ActChargeOnShop,
        /// <summary>
        /// Акт списания из торгового зала.
        /// </summary>
        ActChargeOffShop,
        /// <summary>
        /// Акт передачи в торговый зал.
        /// </summary>
        TransferToShop,
        /// <summary>
        /// Акт возврата из торгового зала.
        /// </summary>
        TransferFromShop,
        /// <summary>
        /// Акт фиксации штрихкодов.
        /// </summary>
        ActFixBarcode,
        /// <summary>
        /// Акт отмены фиксации штрихкодов.
        /// </summary>
        ActUnFixBarcode
    }
}
