namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Статусы документов распроведения накладной.
    /// </summary>
    public enum WaybillRepealStatus
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
    }
}
