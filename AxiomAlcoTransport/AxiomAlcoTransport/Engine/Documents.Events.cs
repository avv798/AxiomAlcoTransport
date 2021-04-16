namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Списки документов приложения.
    /// </summary>
    public partial class Documents
    {
        #region Событие "получение нового документа".
        /// <summary>
        /// Делегат события.
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Аргументы.</param>
        public delegate void DocumentReceivedEventHandler(object sender, DocumentReceivedEventArgs e);
        /// <summary>
        /// Событие - получение нового документа (документов).
        /// </summary>
        public event DocumentReceivedEventHandler DocumentReceivedEvent;
        #endregion Событие "получение нового документа".
    }
}
