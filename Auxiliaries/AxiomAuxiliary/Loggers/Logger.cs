using System;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace AxiomAuxiliary.Loggers
{
    /// <summary>
    /// Класс - функционал логгирования (на основе 'log4net').
    /// </summary>
    public sealed class Logger : ILogger
    {
        #region Внутренние объекты класса.
        /// <summary>
        /// Ядро логгера (на основе 'log4net').
        /// Только чтение.
        /// </summary>
        private readonly log4net.ILog log;
        #endregion Внутренние объекты класса.

        #region Внешние методы класса.
        /// <summary>
        /// Сохранить сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        public void Info(string message)
        {
            log.Info(message);
        }
        /// <summary>
        /// Сохранить сообщение.
        /// </summary>
        /// <param name="source">Источник.</param>
        /// <param name="message">Текст сообщения.</param>
        public void Info(object source, string message)
        {
            log.Info(string.Format("{0}: {1}", (source == null) ? "null source" : source.GetType().ToString(), message));
        }
        /// <summary>
        /// Сохранить сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="exception">Исключение.</param>
        public void Info(string message, Exception exception)
        {
            log.Info(message, exception);
        }
        /// <summary>
        /// Сохранить сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        public void Warn(string message)
        {
            log.Warn(message);
        }
        /// <summary>
        /// Сохранить сообщение.
        /// </summary>
        /// <param name="source">Источник.</param>
        /// <param name="message">Текст сообщения.</param>
        public void Warn(object source, string message)
        {
            log.Warn(string.Format("{0}: {1}", (source == null) ? "null source" : source.GetType().ToString(), message));
        }
        /// <summary>
        /// Сохранить сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="exception">Исключение.</param>
        public void Warn(string message, Exception exception)
        {
            log.Warn(message, exception);
        }
        /// <summary>
        /// Сохранить сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        public void Error(string message)
        {
            log.Error(message);
        }
        /// <summary>
        /// Сохранить сообщение.
        /// </summary>
        /// <param name="source">Источник.</param>
        /// <param name="message">Текст сообщения.</param>
        public void Error(object source, string message)
        {
            log.Error(string.Format("{0}: {1}", (source == null) ? "null source" : source.GetType().ToString(), message));
        }
        /// <summary>
        /// Сохранить сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="exception">Исключение.</param>
        public void Error(string message, Exception exception)
        {
            log.Error(message, exception);
        }
        /// <summary>
        /// Сохранить сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        public void Fatal(string message)
        {
            log.Fatal(message);
        }
        /// <summary>
        /// Сохранить сообщение.
        /// </summary>
        /// <param name="source">Источник.</param>
        /// <param name="message">Текст сообщения.</param>
        public void Fatal(object source, string message)
        {
            log.Fatal(string.Format("{0}: {1}", (source == null) ? "null source" : source.GetType().ToString(), message));
        }
        /// <summary>
        /// Сохранить сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        /// <param name="exception">Исключение.</param>
        public void Fatal(string message, Exception exception)
        {
            log.Fatal(message, exception);
        }
        #endregion Внешние методы класса.

        #region Конструкторы класса.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public Logger()
        {
            Type type = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType;
            log = log4net.LogManager.GetLogger(type);

            Info(string.Format("Объект '{0}' успешно создан.", (type != null) ? type.FullName : "[unknown type]"));
        }
        #endregion Конструкторы класса.
    }
}
