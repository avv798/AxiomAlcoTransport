namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Статусы акта по накладной.
    /// </summary>
    public enum WaybillActStatus
    {
        /// <summary>
        /// Новый акт.
        /// </summary>
        New = 0,
        /// <summary>
        /// Подтверждённый акт.
        /// </summary>
        Accepted = 1,
        /// <summary>
        /// Отвергнутый акт.
        /// </summary>
        Rejected = 2,
        /// <summary>
        /// Информационное сообщение.
        /// </summary>
        Information = 3
    }
}
