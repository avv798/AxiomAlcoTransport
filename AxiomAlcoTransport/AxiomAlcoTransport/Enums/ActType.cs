namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Типы актов на входящую накладную.
    /// </summary>
    public enum ActType
    {
        /// <summary>
        /// Акт подтверждения.
        /// </summary>
        Accept = 1,
        /// <summary>
        /// Акт отказа.
        /// </summary>
        Reject = 2,
        /// <summary>
        /// Акт расхождения.
        /// </summary>
        Difference = 3,
    }
}
