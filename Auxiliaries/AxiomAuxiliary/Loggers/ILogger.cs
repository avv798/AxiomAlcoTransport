using System;

namespace AxiomAuxiliary.Loggers
{
    /// <summary>
    /// Общий интерфейс функционала логирования.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Сохранить сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        void Info(string message);

        /// <summary>
        /// Сохранить сообщение.
        /// </summary>
        /// <param name="source">Источник.</param>
        /// <param name="message">Текст сообщения.</param>
        void Info(object source, string message);

        /// <summary>
        /// Сохранить сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="exception">Исключение.</param>
        void Info(string message, Exception exception);

        /// <summary>
        /// Сохранить сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        void Warn(string message);

        /// <summary>
        /// Сохранить сообщение.
        /// </summary>
        /// <param name="source">Источник.</param>
        /// <param name="message">Текст сообщения.</param>
        void Warn(object source, string message);

        /// <summary>
        /// Сохранить сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="exception">Исключение.</param>
        void Warn(string message, Exception exception);

        /// <summary>
        /// Сохранить сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        void Error(string message);

        /// <summary>
        /// Сохранить сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="exception">Исключение.</param>
        void Error(string message, Exception exception);

        /// <summary>
        /// Сохранить сообщение.
        /// </summary>
        /// <param name="source">Источник.</param>
        /// <param name="message">Текст сообщения.</param>
        void Error(object source, string message);

        /// <summary>
        /// Сохранить сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        void Fatal(string message);

        /// <summary>
        /// Сохранить сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="exception">Исключение.</param>
        void Fatal(string message, Exception exception);

        /// <summary>
        /// Сохранить сообщение.
        /// </summary>
        /// <param name="source">Источник.</param>
        /// <param name="message">Текст сообщения.</param>
        void Fatal(object source, string message);
    }
}