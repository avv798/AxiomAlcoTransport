namespace Axiom.AlcoTransport.Utility
{
    public partial class MainForm
    {
        #region Внутренние методы класса.
        /// <summary>
        /// Проверить связь.
        /// </summary>
        private void checkConnection()
        {
            System.Diagnostics.Process.Start(buildAddress());
        }
        /// <summary>
        /// Построить адрес УТМ.
        /// </summary>
        /// <returns>Адрес УТМ.</returns>
        private string buildAddress()
        {
            // Именно так: без слэша в конце адреса.
            return string.Format("http://{0}:{1}", Address_textEdit.Text, Port_textEdit.Text);
        }
        #endregion Внутренние методы класса.
    }
}