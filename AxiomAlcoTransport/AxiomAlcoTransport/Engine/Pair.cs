namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Вспомогательный класс представления пары данных (ключ - значение).
    /// </summary>
    public class Pair
    {
        #region Защищённые объекты класса.
        /// <summary>
        /// Ключ.
        /// Только чтение.
        /// </summary>
        protected readonly string key;
        /// <summary>
        /// Значение.
        /// Только чтение.
        /// </summary>
        protected readonly string value;
        #endregion Защищённые объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Ключ.
        /// Только чтение.
        /// </summary>
        public string Key
        {
            get { return key; }
        }
        /// <summary>
        /// Значение.
        /// Только чтение.
        /// </summary>
        public string Value
        {
            get { return value; }
        }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected Pair()
        {
            key = string.Empty;
            value = string.Empty;
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <param name="value">Значение.</param>
        public Pair(string key, string value) : this()
        {
            this.key = key;
            this.value = value;
        }
        #endregion Конструкторы класса.

        #region Переопределение базовых методов.
        /// <summary>
        /// Показать строковое описание.
        /// </summary>
        /// <returns>Описание.</returns>
        public override string ToString()
        {
            return Value;
        }
        #endregion Переопределение базовых методов.
    }
}
