using System;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Он-лайн событие.
    /// </summary>
    [Serializable]
    public class OnlineEvent
    {
        #region Внешние объекты класса.

        [DisplayName("Статус"), ReadOnly(true), Browsable(false)]
        public OnlineEventStatus Status { get; protected set; }

        [DisplayName("Статус"), ReadOnly(true), Browsable(true)]
        public int StatusInt
        {
            get { return (int)Status; }
        }

        [DisplayName("Дата и время события"), ReadOnly(true), Browsable(true)]
        public DateTime Timestamp { get; protected set; }

        [DisplayName("Источник"), ReadOnly(true), Browsable(true)]
        public string Source { get; protected set; }

        [DisplayName("Сообщение"), ReadOnly(true), Browsable(true)]
        public string Message { get; protected set; }
       
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected OnlineEvent()
        {
            Status = OnlineEventStatus.Unknown;
            Timestamp = DateTime.Now;
            Source = "неизвестный";
            Message = string.Empty;
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="status">Статус.</param>
        /// <param name="source">Источник.</param>
        /// <param name="message">Сообщение.</param>
        public OnlineEvent(OnlineEventStatus status, string source, string message) : this()
        {
            Status = status;
            Source = source;
            Message = message;

            #region Орфография.

            Source = Source.Replace("Axiom.", "Informa.");

            Message = Message.Replace("\r", " ").Replace("\n", " ").Trim();

            if (!(Message.EndsWith(".") || Message.EndsWith("?") || Message.EndsWith("!")))
            {
                Message += ".";
            }

            #endregion Орфография.
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="source">Источник.</param>
        /// <param name="message">Сообщение.</param>
        public OnlineEvent(string source, string message) : this(OnlineEventStatus.Information, source, message) { }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="source">Источник.</param>
        /// <param name="message">Сообщение.</param>
        public OnlineEvent(object source, string message) : this(OnlineEventStatus.Information, source.GetType().FullName, message) { }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="status">Статус.</param>
        /// <param name="source">Источник.</param>
        /// <param name="message">Сообщение.</param>
        public OnlineEvent(OnlineEventStatus status, object source, string message) : this(status, source.GetType().FullName, message) { }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="source">Источник.</param>
        /// <param name="message">Сообщение.</param>
        /// <param name="exception">Исключение.</param>
        public OnlineEvent(object source, string message, Exception exception)
            : this(OnlineEventStatus.Error, source.GetType().FullName, string.Format("{0} {1}", message, exception.Message)) { }
        #endregion Конструкторы класса.
    }
}
