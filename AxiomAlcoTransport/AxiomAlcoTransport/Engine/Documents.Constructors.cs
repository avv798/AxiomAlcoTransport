using System;
using System.Collections.Generic;
using System.Windows.Threading;
using Axiom.AlcoTransport.Document;
using Axiom.AlcoTransport.Watchtower.Configuration;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Списки документов приложения.
    /// </summary>
    public partial class Documents
    {
        #region Внешние константы класса.
        /// <summary>
        /// Количество событий к сохранению.
        /// Константа.
        /// </summary>
        public const int CountToSaveOnlineEvents = 3072;
        /// <summary>
        /// Пограничная дата ЕГАИС для производства или ввоза продукции.
        /// Только чтение.
        /// </summary>
        public static readonly DateTime BorderBottlingDate = new DateTime(2012, 5, 22, 0, 0, 0);
        #endregion Внешние константы класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected Documents()
        {
            Program.Logger.Info(this, "Попытка инициализации объекта (по умолчанию)...");

            locker = new Locker();

            markTypes = new MarkTypes();
            requestTimer = new DispatcherTimer();

            tickets = new ThreadedBindingList<Ticket>();
            restsList = new ThreadedBindingList<Rests>();
            formAList = new ThreadedBindingList<FormA>();
            formBList = new ThreadedBindingList<FormB>();
            partners = new ThreadedBindingList<Partner>();
            inWaybills = new ThreadedBindingList<InWaybill>();
            production = new ThreadedBindingList<Production>();
            outWaybills = new ThreadedBindingList<OutWaybill>();
            waybillActs = new ThreadedBindingList<WaybillAct>();
            shopRestsList = new ThreadedBindingList<ShopRests>();
            notAnswerList = new ThreadedBindingList<NotAnswer>();
            markBarcodes = new ThreadedBindingList<MarkBarcode>();
            bCodeRestsList = new ThreadedBindingList<BCodeRests>();
            formBRegInfos = new ThreadedBindingList<FormBRegInfo>();
            actChargeOnList = new ThreadedBindingList<ActChargeOn>();
            waybillTickets = new ThreadedBindingList<WaybillTicket>();
            waybillRepeals = new ThreadedBindingList<WaybillRepeal>();
            actChargeOffList = new ThreadedBindingList<ActChargeOff>();
            historyFormBList = new ThreadedBindingList<HistoryFormB>();
            actFixBarcodeList = new ThreadedBindingList<ActFixBarcode>();
            transferToShopList = new ThreadedBindingList<TransferToShop>();
            actChargeOnShopList = new ThreadedBindingList<ActChargeOnShop>();
            actUnFixBarcodeList = new ThreadedBindingList<ActUnFixBarcode>();
            actChargeOffShopList = new ThreadedBindingList<ActChargeOffShop>();
            transferFromShopList = new ThreadedBindingList<TransferFromShop>();
            inventoryBRegInfoList = new ThreadedBindingList<InventoryBRegInfo>();
            waybillRepealConfirms = new ThreadedBindingList<WaybillRepealConfirm>();

            deleteExistingInWaybill = Program.GetBooleanParameter("deleteExistingInWaybill", false);

            onlineEvents = new ThreadedBindingList<OnlineEvent>
                                {
                                    new OnlineEvent(this, "Инициализация (по умолчанию) ядра успешно завершена.")
                                };

            documentsVersion = GetDocumentsVersion();

            Program.Logger.Info(this, string.Format("\t... используемая версия документов ЕГАИС: {0}.", documentsVersion));

            Program.Logger.Info(this, "... объект инициализирован (по умолчанию) успешно.");
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="configuration">Конфигурация.</param>
        /// <param name="addresses">Адреса.</param>
        /// <param name="storage">Дисковое хранилище.</param>
        public Documents(ConfigurationData configuration, Addresses addresses, FileStorage storage) : this()
        {
            Program.Logger.Info(this, "Попытка инициализации объекта (с параметрами)...");

            this.addresses = addresses;
            this.configuration = configuration;
            this.storage = storage;

            requestTimer.Tick += onRequestTimedEvent;
            requestTimer.Interval = new TimeSpan(0, 0, configuration.IntervalDataRequest);

            onlineEvents.Add(new OnlineEvent(this, "Инициализация ядра (с параметрами) успешно завершена."));

            Program.Logger.Info(this, "... объект инициализирован (с параметрами) успешно.");
        }
        #endregion Конструкторы класса.

        #region Методы инициализации.
        /// <summary>
        /// Получить версию используемой документации из конфигурации приложения.
        /// </summary>
        /// <returns>Версия используемой документации.</returns>
        public static int GetDocumentsVersion()
        {
            try
            {
                string version = Program.GetAppSetting("egaisDocumentsVersion").ToLower();

                Program.Logger.Info($"\t... найдено значение параметра 'egaisDocumentsVersion': '{version}'.");

                return new Dictionary<string, int>{{"v1",1},{"v2",2},{"v3",3}, { "v4", 4 } }[version];
            }
            catch (Exception exception)
            {
                Program.Logger.Warn(
                    $"\t... параметр 'egaisDocumentsVersion' конфигурации приложения не найден или найден с ошибкой: {exception}.");

                return 2;
            }
        }
        #endregion Методы инициализации.
    }
}
