namespace Axiom.AlcoTransport.Utility.Quittances
{
    /// <summary>
    /// Вспомогательный класс представления адресов сервера УТМ.
    /// </summary>
    public class LittleAddresses
    {
        #region Защищенные объекты класса.
        /// <summary>
        /// Пространство имён "xsi".
        /// Только чтение.
        /// </summary>
        protected readonly LittlePrefix xsi;
        /// <summary>
        /// Пространство имён "ns".
        /// Только чтение.
        /// </summary>
        protected readonly LittlePrefix ns;
        /// <summary>
        /// Пространство имён "qp".
        /// Только чтение.
        /// </summary>
        protected readonly LittlePrefix qp;
        #endregion Защищенные объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Пространство имён "xsi".
        /// Только чтение.
        /// </summary>
        public LittlePrefix Xsi
        {
            get { return xsi; }
        }
        /// <summary>
        /// Пространство имён "ns".
        /// Только чтение.
        /// </summary>
        public LittlePrefix Ns
        {
            get { return ns; }
        }
        /// <summary>
        /// Пространство имён "qp".
        /// Только чтение.
        /// </summary>
        public LittlePrefix Qp
        {
            get { return qp; }
        }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public LittleAddresses()
        {
            xsi = new LittlePrefix { Prefix = "xsi", Uri = "http://www.w3.org/2001/XMLSchema-instance" };
            ns = new LittlePrefix { Prefix = "ns", Uri = "http://fsrar.ru/WEGAIS/WB_DOC_SINGLE_01" };
            qp = new LittlePrefix { Prefix = "qp", Uri = "http://fsrar.ru/WEGAIS/InfoVersionTTN" };
        }
        #endregion Конструкторы класса.
    }
}
