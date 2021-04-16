namespace Axiom.AlcoTransport.Watchtower.Configuration
{
     /// <summary>
     /// Константы приложения.
     /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Девиз.
        /// Константа.
        /// </summary>
        public const string Motto = "Axioma. In Vino Veritas";
        /// <summary>
        /// Общий путь в реестре для сохранения настроек.
        /// Константа.
        /// </summary>
        public const string RegistryPathCommon = "Software\\Informa\\Axiom\\AlcoTransport";
        /// <summary>
        /// Путь в реестре для сохранения конфигурационных настроек.
        /// Константа.
        /// </summary>
        public const string RegistryPathConfiguration = RegistryPathCommon + "\\Configuration";
        /// <summary>
        /// Путь в реестре для сохранения визуальных пользовательских настроек.
        /// Константа.
        /// Внутреннее представление.
        /// </summary>
        public const string RegistryPathVisualSettings = RegistryPathCommon + "\\VisualSettings";
        /// <summary>
        /// Стиль оформления "по умолчанию".
        /// </summary>
        public const string DefaultVisualStyle = "Office 2010 Silver";
    }
}
