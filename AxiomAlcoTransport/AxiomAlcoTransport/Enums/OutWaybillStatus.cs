namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Статусы исходящей накладной.
    /// </summary>
    public enum OutWaybillStatus
    {
        /// <summary>
        /// Неполная ТТН.
        /// </summary>
        Partial = 200,
        /// <summary>
        /// ТТН готова к отправке.
        /// </summary>
        Ready = 201,
        /// <summary>
        /// Отправленная ТТН.
        /// </summary>
        Sent = 202,
        /// <summary>
        /// ТТН принята в ЕГАИС.
        /// </summary>
        Confirmed = 203,
        /// <summary>
        /// ТТН отозвана из ЕГАИС.
        /// </summary>
        Revoked = 204,
        /// <summary>
        /// ТТН отвергнута ЕГАИС.
        /// </summary>
        Rejected = 205
    }
}
