namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Статусы входящей накладной.
    /// </summary>
    public enum InWaybillStatus
    {
        /// <summary>
        /// Неполная ТТН.
        /// </summary>
        Partial = 101,
        /// <summary>
        /// Новая ТТН.
        /// </summary>
        New = 102,
        /// <summary>
        /// Подтверждённая ТТН.
        /// </summary>
        Accepted = 103,
        /// <summary>
        /// Отвергнутая ТТН.
        /// </summary>
        Rejected = 104,
        /// <summary>
        /// ТТН с актом расхождения.
        /// </summary>
        Difference = 105,
        /// <summary>
        /// ТТН распроведена.
        /// </summary>
        Repealed = 106
    }
}
