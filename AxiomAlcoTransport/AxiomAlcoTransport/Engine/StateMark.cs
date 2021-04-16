using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Акцизная марка (АМ) или федеральная специальная марка (ФСМ).
    /// </summary>
    public class StateMark
    {
        #region Внешние объекты класса.
        /// <summary>
        /// Тип марки.
        /// Только чтение.
        /// </summary>
        [DisplayName("Наименование"), ReadOnly(true), Browsable(false)]
        public MarkType MarkType { get; protected set; }
        /// <summary>
        /// Код типа марки.
        /// Только чтение.
        /// </summary>
        [DisplayName("Код типа"), ReadOnly(true), Browsable(true)]
        public string MarkTypeCode
        {
            get { return MarkType.Code; }
        }
        /// <summary>
        /// Код типа марки.
        /// Только чтение.
        /// </summary>
        [DisplayName("Наименование типа"), ReadOnly(true), Browsable(true)]
        public string MarkTypeTitle
        {
            get { return MarkType.Title; }
        }
        /// <summary>
        /// Серия марки.
        /// Только чтение.
        /// </summary>
        [DisplayName("Серия"), ReadOnly(true), Browsable(true)]
        public string Series { get; protected set; }
        /// <summary>
        /// Номер марки.
        /// Только чтение.
        /// </summary>
        [DisplayName("Номер"), ReadOnly(true), Browsable(true)]
        public string Number { get; protected set; }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected StateMark()
        {
            MarkType = new MarkType(string.Empty, string.Empty);
            Series = string.Empty;
            Number = string.Empty;
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="markType">Тип марки.</param>
        /// <param name="series">Серия.</param>
        /// <param name="number">Номер.</param>
        public StateMark(MarkType markType, string series, string number)
        {
            MarkType = markType;
            Series = series;
            Number = number;
        }
        #endregion Конструкторы класса.
    }
}
