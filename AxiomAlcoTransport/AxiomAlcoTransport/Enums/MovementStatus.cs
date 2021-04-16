namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Статусы документов движения товара.
    /// </summary>
    public enum MovementStatus
    {
        /// <summary>
        /// Неполный документ.
        /// </summary>
        Partial = 500,
        /// <summary>
        /// Новый документ.
        /// </summary>
        New = 501,
        /// <summary>
        /// Отправленный документ.
        /// </summary>
        Sent = 502,
        /// <summary>
        /// Подтверждённый документ.
        /// </summary>
        Confirmed = 503,
        /// <summary>
        /// Отвергнутый документ.
        /// </summary>
        Rejected = 504,
        /// <summary>
        /// Отменённый документ.
        /// </summary>
        Repealed = 505
    }
}
