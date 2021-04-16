using System;

namespace Axiom.AlcoTransport.Watchtower.Configuration
{
    /// <summary>
    /// Данные конфигурации.
    /// </summary>
    public class ConfigurationData
    {
        #region Внешние объекты класса.
        /// <summary>
        /// Идентификатор организации в ФС РАР.
        /// </summary>
        public string FsrarId { get; set; }
        /// <summary>
        /// ИНН организации.
        /// </summary>
        public string Inn { get; set; }
        /// <summary>
        /// Адрес сервера УТМ.
        /// </summary>
        public string UtmAddress { get; set; }
        /// <summary>
        /// Порт сервера УТМ.
        /// </summary>
        public string UtmPort { get; set; }
        /// <summary>
        /// Тайм-аут соединения с сервером УТМ (для коротких запросов; в секундах).
        /// </summary>
        public int UtmTimeoutShort { get; set; }
        /// <summary>
        /// Тайм-аут соединения с сервером УТМ (для больших запросов; в секундах).
        /// </summary>
        public int UtmTimeoutLong { get; set; }
        /// <summary>
        /// Путь к локальной БД.
        /// </summary>
        public string PathToLocalDatabase { get; set; }
        /// <summary>
        /// Период опроса сервера УТМ (в секундах).
        /// </summary>
        public int IntervalDataRequest { get; set; }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public ConfigurationData()
        {
            FsrarId = string.Empty;
            Inn = string.Empty;
            UtmAddress = string.Empty;
            UtmPort = string.Empty;
            UtmTimeoutShort = -1;
            UtmTimeoutLong = -1;
            PathToLocalDatabase = string.Empty;
            IntervalDataRequest = -1;
        }
        #endregion Конструкторы класса.

        #region Внешние методы класса.
        /// <summary>
        /// Проверить состав конфигурации.
        /// </summary>
        public void Check()
        {
            if (string.IsNullOrWhiteSpace(FsrarId)) throw new Exception("Конфигурационный параметр 'FsrarId' не может быть пустым.");
            if (string.IsNullOrWhiteSpace(Inn)) throw new Exception("Конфигурационный параметр 'Inn' не может быть пустым.");
            if (string.IsNullOrWhiteSpace(UtmAddress)) throw new Exception("Конфигурационный параметр 'UtmAddress' не может быть пустым.");
            if (string.IsNullOrWhiteSpace(UtmPort)) throw new Exception("Конфигурационный параметр 'UtmPort' не может быть пустым.");
            if (string.IsNullOrWhiteSpace(PathToLocalDatabase)) throw new Exception("Конфигурационный параметр 'PathToLocalDatabase' не может быть пустым.");

            if ((UtmTimeoutLong < 100) || (UtmTimeoutLong > 1200000)) throw new Exception("Конфигурационный параметр 'UtmTimeoutLong' имеет некорректное значение.");
            if ((UtmTimeoutShort < 100) || (UtmTimeoutShort > 1200000)) throw new Exception("Конфигурационный параметр 'UtmTimeoutShort' имеет некорректное значение.");
            if ((IntervalDataRequest < 30) || (IntervalDataRequest > 86400)) throw new Exception("Конфигурационный параметр 'IntervalDataRequest' имеет некорректное значение.");
        }
        #endregion Внешние методы класса.

        #region Переопределение системных методов.
        /// <summary>
        /// Описание конфигурации.
        /// </summary>
        /// <returns>Строка.</returns>
        public override string ToString()
        {
            return string.Format("идентификатор ФСРАР: '{0}'; " +
                                 "ИНН: '{1}'; " +
                                 "сервер: '{2}:{3}'; " +
                                 "тайм-ауты: '{4}/{5}'; " +
                                 "интервал опроса: '{6}'; " +
                                 "путь к локальной базе данных: '{7}'",
                                 FsrarId, Inn, UtmAddress, UtmPort, UtmTimeoutShort, UtmTimeoutLong, IntervalDataRequest, PathToLocalDatabase);
        }
        #endregion Переопределение системных методов.
    }
}
