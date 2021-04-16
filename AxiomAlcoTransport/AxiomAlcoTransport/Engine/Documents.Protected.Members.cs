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
        #region Защищенные объекты класса.
        /// <summary>
        /// Объект синхронизации.
        /// Только чтение.
        /// </summary>
        protected readonly Locker locker;
        /// <summary>
        /// Адреса.
        /// Только чтение.
        /// </summary>
        protected readonly Addresses addresses;
        /// <summary>
        /// Дисковое хранилище.
        /// Только чтение.
        /// </summary>
        protected readonly FileStorage storage;
        /// <summary>
        /// Конфигурация.
        /// Только чтение.
        /// </summary>
        protected readonly ConfigurationData configuration;
        /// <summary>
        /// Список партнёров.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<Partner> partners;
        /// <summary>
        /// Алкогольная продукция.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<Production> production;
        /// <summary>
        /// Входящие накладные.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<InWaybill> inWaybills;
        /// <summary>
        /// Исходящие накладные.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<OutWaybill> outWaybills;
        /// <summary>
        /// Квитки ответов сервера.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<Ticket> tickets;
        /// <summary>
        /// Документы регистрации движения.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<FormBRegInfo> formBRegInfos;
        /// <summary>
        /// Документы остатков.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<Rests> restsList;
        /// <summary>
        /// Документы остатков в торговом зале.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<ShopRests> shopRestsList; 
        /// <summary>
        /// Документы остатков по штрихкодам.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<BCodeRests> bCodeRestsList;
        /// <summary>
        /// Формы 'А'.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<FormA> formAList;
        /// <summary>
        /// Формы 'Б'.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<FormB> formBList;
        /// <summary>
        /// Список движений по форме 'Б'.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<HistoryFormB> historyFormBList;
        /// <summary>
        /// Список ответов по запросам по штрих-кодам.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<MarkBarcode> markBarcodes;
        /// <summary>
        /// Акты по накладным.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<WaybillAct> waybillActs;
        /// <summary>
        /// Запросы на распроведение накладных.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<WaybillRepeal> waybillRepeals;
        /// <summary>
        /// Подтверждение запросов на распроведение накладных.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<WaybillRepealConfirm> waybillRepealConfirms;
        /// <summary>
        /// Акты постановки на учёт.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<ActChargeOn> actChargeOnList;
        /// <summary>
        /// Список уведомлений о постановке на баланс продукции.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<InventoryBRegInfo> inventoryBRegInfoList;
        /// <summary>
        /// Акты списания продукции.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<ActChargeOff> actChargeOffList;
        /// <summary>
        /// Акты постановки на учёт в торговом зале.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<ActChargeOnShop> actChargeOnShopList;
        /// <summary>
        /// Акты списания продукции из торгового зала.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<ActChargeOffShop> actChargeOffShopList;  
        /// <summary>
        /// Акты фиксации штрихкодов.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<ActFixBarcode> actFixBarcodeList;  
        /// <summary>
        /// Акты отмены фиксации штрихкодов.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<ActUnFixBarcode> actUnFixBarcodeList;
        /// <summary>
        /// Документы передачи товара в торговый зал.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<TransferToShop> transferToShopList;
        /// <summary>
        /// Документы возврата товара из торгового зала.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<TransferFromShop> transferFromShopList;
        /// <summary>
        /// Списки необработанных накладных.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<NotAnswer> notAnswerList;
        /// <summary>
        /// Ответы по актам расхождения.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<WaybillTicket> waybillTickets;
        /// <summary>
        /// Таймер для периодического опроса.
        /// </summary>
        protected readonly DispatcherTimer requestTimer;
        /// <summary>
        /// Список онлайн-событий.
        /// Только чтение.
        /// </summary>
        protected readonly ThreadedBindingList<OnlineEvent> onlineEvents;
        /// <summary>
        /// Корзина с документами.
        /// </summary>
        protected ThreadedBindingList<ADocument> garbage;
        /// <summary>
        /// Признак удаления существующей входящей накладной (при совпадении идентификационных реквизитов).
        /// Только чтение.
        /// </summary>
        protected readonly bool deleteExistingInWaybill;
        /// <summary>
        /// Словарь типов марок.
        /// Только чтение.
        /// </summary>
        protected readonly MarkTypes markTypes;
        /// <summary>
        /// Используемая версия документов ЕГАИС.
        /// Только чтение.
        /// </summary>
        protected readonly int documentsVersion;
        #endregion Защищенные объекты класса.
    }
}
