namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Тип акцизной марки.
    /// </summary>
    public class MarkType
    {
        #region Внешние объекты класса.
        /// <summary>
        /// Код.
        /// Только чтение.
        /// </summary>
        public string Code { get; protected set; }
        /// <summary>
        /// Наименование.
        /// Только чтение.
        /// </summary>
        public string Title { get; protected set; }
        /// <summary>
        /// Описание.
        /// Только чтение.
        /// </summary>
        public string Description
        {
            get { return string.Format("{0}: {1}", Code, Title); }
        }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected MarkType()
        {
            Code = string.Empty;
            Title = string.Empty;
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <param name="title">Наименование.</param>
        public MarkType(string code, string title)
        {
            Code = code;
            Title = title;
        }
        #endregion Конструкторы класса.

        #region Переопределение базовых методов.
        /// <summary>
        /// Показать строковое описание.
        /// </summary>
        /// <returns>Описание.</returns>
        public override string ToString()
        {
            return Description;
        }
        #endregion Переопределение базовых методов.
    }
}
