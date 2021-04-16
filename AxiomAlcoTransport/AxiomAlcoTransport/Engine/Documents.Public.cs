using System;
using System.Collections.Generic;
using Axiom.AlcoTransport.Document;
using Axiom.AlcoTransport.Watchtower.Configuration;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Списки документов приложения.
    /// </summary>
    public partial class Documents
    {
        #region Внешние объекты класса.
        /// <summary>
        /// Список партнёров.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<Partner> Partners
        {
            get { return partners; }
        }
        /// <summary>
        /// Список алкогольной продукции.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<Production> Production
        {
            get { return production; }
        }
        /// <summary>
        /// Входящие накладные.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<InWaybill> InWaybills
        {
            get { return inWaybills; }
        }
        /// <summary>
        /// Квитки ответов сервера.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<Ticket> Tickets
        {
            get { return tickets; }
        }
        /// <summary>
        /// Остатки.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<Rests> RestsList
        {
            get { return restsList; }
        }
        /// <summary>
        /// Остатки в торговом зале.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<ShopRests> ShopRestsList
        {
            get { return shopRestsList; }
        } 
        /// <summary>
        /// Остатки в разрезе штрихкодов.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<BCodeRests> BCodeRestsList => bCodeRestsList;
        /// <summary>
        /// Формы 'А'.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<FormA> FormAList
        {
            get { return formAList; }
        }
        /// <summary>
        /// Формы 'Б'.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<FormB> FormBList
        {
            get { return formBList; }
        }
        /// <summary>
        /// Акты по накладным.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<WaybillAct> WaybillActs
        {
            get { return waybillActs; }
        }
        /// <summary>
        /// Запросы на распроведение накладных.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<WaybillRepeal> WaybillRepeals
        {
            get { return waybillRepeals; }
        }
        /// <summary>
        /// Исходящие накладные.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<OutWaybill> OutWaybills
        {
            get { return outWaybills; }
        }
        /// <summary>
        /// Акты постановки на учёт.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<ActChargeOn> ActChargeOnList
        {
            get { return actChargeOnList; }
        }
        /// <summary>
        /// Акты списания продукции.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<ActChargeOff> ActChargeOffList
        {
            get { return actChargeOffList; }
        }
        /// <summary>
        /// Акты постановки на учёт в торговом зале.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<ActChargeOnShop> ActChargeOnShopList
        {
            get { return actChargeOnShopList; }
        }
        /// <summary>
        /// Акты списания продукции из торгового зала.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<ActChargeOffShop> ActChargeOffShopList
        {
            get { return actChargeOffShopList; }
        }
        /// <summary>
        /// Документы передачи товара в торговый зал.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<TransferToShop> TransferToShopList
        {
            get { return transferToShopList; }
        }
        /// <summary>
        /// Документы возврата товара из торгового зала.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<TransferFromShop> TransferFromShopList
        {
            get { return transferFromShopList; }
        } 
        /// <summary>
        /// Документы актов фиксации штрихкодов.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<ActFixBarcode> ActFixBarcodeList => actFixBarcodeList;
        /// <summary>
        /// Документы актов отмены фиксации штрихкодов.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<ActUnFixBarcode> ActUnFixBarcodeList => actUnFixBarcodeList;

        /// <summary>
        /// Списки необработанных накладных.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<NotAnswer> NotAnswerList
        {
            get { return notAnswerList; }
        }
        /// <summary>
        /// Список онлайн-событий.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<OnlineEvent> OnlineEvents
        {
            get { return onlineEvents; }
        }
        /// <summary>
        /// Список уведомлений о постановке на баланс продукции.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<InventoryBRegInfo> InventoryBRegInfoList
        {
            get { return inventoryBRegInfoList; }
        }
        /// <summary>
        /// Список движений по форме 'Б'.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<HistoryFormB> HistoryFormBList
        {
            get { return historyFormBList; }
        }
        /// <summary>
        /// Список ответов по запросам по штрих-кодам.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<MarkBarcode> MarkBarcodes
        {
            get { return markBarcodes; }
        }
        /// <summary>
        /// Ответы по актам расхождения.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<WaybillTicket> WaybillTickets
        {
            get { return waybillTickets; }
        }
        /// <summary>
        /// Корзина с документами.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<ADocument> Garbage
        {
            get
            {
                if (garbage == null) loadGarbage();

                return garbage;
            }
        }
        /// <summary>
        /// Хранилище.
        /// Только чтение.
        /// </summary>
        public FileStorage Storage
        {
            get { return storage; }
        }
        /// <summary>
        /// Конфигурация.
        /// Только чтение.
        /// </summary>
        public ConfigurationData Configuration
        {
            get { return configuration; }
        }
        /// <summary>
        /// Адреса.
        /// Только чтение.
        /// </summary>
        public Addresses Addresses
        {
            get { return addresses; }
        }
        /// <summary>
        /// Словарь типов марок.
        /// Только чтение.
        /// </summary>
        public MarkTypes MarkTypes
        {
            get { return markTypes; }
        }
        #endregion Внешние объекты класса.

        #region Внешние методы класса.
        /// <summary>
        /// Запустить фоновый опрос сервера.
        /// </summary>
        public void StartBackgroundRequest()
        {
            try
            {
                Program.Logger.Info(this, "Попытка запустить периодический опрос сервера...");

                startBackgroundRequest();

                onlineEvents.Add(new OnlineEvent(this, "Периодический опрос сервера запущен по внешней команде."));

                Program.Logger.Info(this, "... периодический опрос сервера успешно запущен по внешней команде.");
            }
            catch (Exception exception)
            {
                onlineEvents.Add(new OnlineEvent(this, "Во время запуска периодического опроса сервера произошла ошибка.", exception));

                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Остановить фоновый опрос сервера.
        /// </summary>
        public void StopBackgroundRequest()
        {
            try
            {
                Program.Logger.Info(this, "Попытка остановить периодический опрос сервера...");

                stopBackgroundRequest();

                onlineEvents.Add(new OnlineEvent(this, "Периодический опрос сервера остановлен по внешней команде."));

                Program.Logger.Info(this, "... периодический опрос сервера успешно остановлен по внешней команде.");
            }
            catch (Exception exception)
            {
                onlineEvents.Add(new OnlineEvent(this, "Во время остановки периодического опроса сервера произошла ошибка.", exception));

                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Получить все входящие документы.
        /// </summary>
        public void GetDocuments()
        {
            try
            {
                Program.Logger.Info(this, "Попытка получить входящие документы...");

                getDocuments();

                onlineEvents.Add(new OnlineEvent(this, "Получение документов по внешней команде завершено."));

                Program.Logger.Info(this, "... попытка получения входящие документов успешно завершена.");
            }
            catch (Exception exception)
            {
                onlineEvents.Add(new OnlineEvent(this, "Во время получения входящих документов произошла ошибка.", exception));

                Program.Logger.Error("Во время получения входящих документов произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Загрузить базу данных в память приложения.
        /// </summary>
        public void LoadDatabase()
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузить базу данных в память приложения...");

                LoadOnlineEvents();

                loadDatabase();

                onlineEvents.Add(new OnlineEvent(this, "Загрузка базы данных в память приложения успешно завершена."));

                Program.Logger.Info(this, "... загрузка базы данных в память приложения успешно завершена.");
            }
            catch (Exception exception)
            {
                onlineEvents.Add(new OnlineEvent(this, "Во время загрузки базы данных произошла ошибка.", exception));

                Program.Logger.Error("Во время загрузки базы данных произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Очистить исходящий буфер.
        /// </summary>
        public void ClearOutBuffer()
        {
            try
            {
                Program.Logger.Info(this, "Попытка очистить исходящий буфер...");

                clearOutBuffer();

                onlineEvents.Add(new OnlineEvent(this, "Очистка исходящего буфера успешно завершена."));

                Program.Logger.Info(this, "... очистка исходящего буфера успешно завершена.");
            }
            catch (Exception exception)
            {
                onlineEvents.Add(new OnlineEvent(this, "Во время очистки исходящего буфера произошла ошибка.", exception));

                Program.Logger.Error("Во время очистки исходящего буфера произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Очистить исходящий буфер во время старта приложения.
        /// </summary>
        /// <param name="suppressException">Подавить исключение в случае ошибки.</param>
        public void ClearOutBufferOnStartup(bool suppressException = false)
        {
            try
            {
                Program.Logger.Info(this, "Попытка очистить исходящий буфер во время старта приложения...");

                if (!Program.GetBooleanParameter("clearOutBufferOnStartup"))
                {
                    Program.Logger.Info(this, "Очистка исходящего буфера во время старта приложения отключена конфигурационным файлом приложения.");

                    return;
                }

                clearOutBufferOnStartup();

                onlineEvents.Add(new OnlineEvent(this, "Очистка исходящего буфера успешно завершена."));

                Program.Logger.Info(this, "... очистка исходящего буфера во время старта приложения успешно завершена.");
            }
            catch (Exception exception)
            {
                onlineEvents.Add(new OnlineEvent(this, "Во время очистки исходящего буфера произошла ошибка.", exception));

                Program.Logger.Error("Во время очистки исходящего буфера  во время старта приложения произошла ошибка.", exception);

                if (suppressException)
                {
                    Program.Logger.Warn(this, "Исключение подавлено. Приложение продолжит работу.");

                    return;
                }

                throw;
            }
        }
        /// <summary>
        /// Отправить запрос на получение справочника организаций.
        /// </summary>
        /// <param name="inn">ИНН.</param>
        public void RequestPartners(string inn)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка отправить запрос на получение реквизитов организации по ИНН '{0}'...", inn));

                requestPartners(inn);

                onlineEvents.Add(new OnlineEvent(this, string.Format("Запрос на получение реквизитов организации по ИНН '{0}' успешно отправлен.", inn)));

                Program.Logger.Info(this, "... запрос на получение справочника организаций успешно отправлен.");
            }
            catch (Exception exception)
            {
                onlineEvents.Add(new OnlineEvent(this, "Во время отправки запроса на получение реквизитов организации произошла ошибка.", exception));

                Program.Logger.Error("Во время отправки запроса на получение справочника организаций произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Отправить запрос на получение справочника алкогольной продукции.
        /// </summary>
        /// <param name="inn">ИНН.</param>
        public void RequestAP(string inn)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка отправить запрос на получение справочника алкогольной продукции по ИНН '{0}'...", inn));

                requestAP(inn);

                onlineEvents.Add(new OnlineEvent(this, string.Format("Запрос на получение справочника алкогольной продукции по ИНН организации '{0}' успешно отправлен.", inn)));

                Program.Logger.Info(this, "... запрос на получение справочника алкогольной продукции успешно отправлен.");
            }
            catch (Exception exception)
            {
                onlineEvents.Add(new OnlineEvent(this, "Во время отправки запроса на получение справочника алкогольной продукции произошла ошибка.", exception));

                Program.Logger.Error("Во время отправки запроса на получение справочника алкогольной продукции произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Отправить запрос на списка необработанных накладных.
        /// </summary>
        public void RequestNotAnswer()
        {
            try
            {
                Program.Logger.Info(this, "Попытка отправить запрос на получение списка необработанных накладных...");

                requestNotAnswer();

                onlineEvents.Add(new OnlineEvent(this, "Запрос на получение списка необработанных накладных успешно отправлен."));

                Program.Logger.Info(this, "... запрос на получение списка необработанных накладных успешно отправлен.");
            }
            catch (Exception exception)
            {
                onlineEvents.Add(new OnlineEvent(this, "Во время отправки запроса на получение списка необработанных накладных произошла ошибка.", exception));

                Program.Logger.Error("Во время отправки запроса на получение списка необработанных накладных произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Отправить запрос на получение движения по справке 'Б'.
        /// </summary>
        /// <param name="informBRegId">Номер справки 'Б'.</param>
        public void RequestHistoryFormB(string informBRegId)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка отправить запрос на получение движения по справке 'Б' с номером {0}...", informBRegId));

                requestHistoryFormB(informBRegId);

                onlineEvents.Add(new OnlineEvent(this, string.Format("Запрос на получение движения по справке 'Б' по идентификатору '{0}' успешно отправлен.", informBRegId)));

                Program.Logger.Info(this, string.Format("... запрос на получение движения по справке 'Б' с номером {0} успешно отправлен.", informBRegId));
            }
            catch (Exception exception)
            {
                onlineEvents.Add(new OnlineEvent(this, "Во время отправки запроса на получение движения по справке 'Б' произошла ошибка.", exception));

                Program.Logger.Error("Во время отправки запроса на получение движения по справке 'Б' произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Отправить запрос на получение штрих-кодов.
        /// </summary>
        /// <param name="list">Список марок</param>
        public void RequestMarkBarcode(IList<StateMark> list)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка отправить запрос на получение штрих-кодов (количество марок: {0})...", list.Count));

                requestMarkBarcodes(list);

                onlineEvents.Add(new OnlineEvent(this, string.Format("Запрос на получение штрих-кодов (количество марок: {0}) успешно отправлен.", list.Count)));

                Program.Logger.Info(this, string.Format("... запрос на получение штрих-кодов (количество марок: {0}) успешно отправлен.", list.Count));
            }
            catch (Exception exception)
            {
                onlineEvents.Add(new OnlineEvent(this, "Во время отправки запроса на получение  штрих-кодов произошла ошибка.", exception));

                Program.Logger.Error("Во время отправки запроса на получение  штрих-кодов произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Отправить запрос на получение остатков.
        /// </summary>
        public void RequestRests()
        {
            try
            {
                Program.Logger.Info(this, "Попытка отправить запрос на получение справочника остатков на складе организации...");

                requestRests();

                onlineEvents.Add(new OnlineEvent(this, "Запрос на получение справочника остатков на складе организации успешно отправлен."));

                Program.Logger.Info(this, "... запрос на получение справочника остатков на складе организации успешно отправлен.");
            }
            catch (Exception exception)
            {
                onlineEvents.Add(new OnlineEvent(this, "Во время отправки запроса на получение справочника остатков на складе организации произошла ошибка.", exception));

                Program.Logger.Error("Во время отправки запроса на получение справочника остатков на складе организации произошла ошибка.", exception);

                throw;
            }
        }

        /// <summary>
        /// Отправить запрос на получение остатков в торговом зале.
        /// </summary>
        public void RequestShopRests()
        {
            try
            {
                Program.Logger.Info(this,
                    "Попытка отправить запрос на получение справочника остатков в торговом зале...");

                requestShopRests();

                onlineEvents.Add(new OnlineEvent(this,
                    "Запрос на получение справочника остатков в торговом зале успешно отправлен."));

                Program.Logger.Info(this,
                    "... запрос на получение справочника остатков в торговом зале успешно отправлен.");
            }
            catch (Exception exception)
            {
                onlineEvents.Add(new OnlineEvent(this,
                    "Во время отправки запроса на получение справочника остатков в торговом зале произошла ошибка.",
                    exception));

                Program.Logger.Error(
                    "Во время отправки запроса на получение справочника остатков в торговом зале произошла ошибка.",
                    exception);

                throw;
            }
        }

        /// <summary>
        /// Отправить запрос на получение остатков в разрезе штрихкодов.
        /// </summary>
        public void RequestBCodeRests(string form2Name)
        {
            try
            {
                Program.Logger.Info(this, "Попытка отправить запрос на получение остатков в разрезе штрихкодов...");

                requestBCodeRests(form2Name);

                onlineEvents.Add(new OnlineEvent(this, "Запрос на получение остатков в разрезе штрихкодов успешно отправлен."));

                Program.Logger.Info(this, "... запрос на получение остатков в разрезе штрихкодов успешно отправлен.");
            }
            catch (Exception exception)
            {
                onlineEvents.Add(new OnlineEvent(this, "Во время отправки запроса на получение остатков в разрезе штрихкодов произошла ошибка.", exception));

                Program.Logger.Error("Во время отправки запроса на получение остатков в разрезе штрихкодов произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Подтвердить или отказать акт.
        /// </summary>
        /// <param name="waybillAct">Акт.</param>
        /// <param name="isAccepted">Признак - подтверждение или отказ.</param>
        /// <param name="actNumber">Номер документа.</param>
        /// <param name="actComment">Комментарий к акту.</param>
        public void SendConfirmByWaybillAct(WaybillAct waybillAct, bool isAccepted, string actNumber, string actComment)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка отправить документ {0} по акту...", isAccepted ? "подтверждения" : "отказа"));

                sendConfirmByWaybillAct(waybillAct, isAccepted, actNumber, actComment);

                onlineEvents.Add(new OnlineEvent(this, "Подтверждение (отказ) по акту успешно отправлено."));

                Program.Logger.Info(this, string.Format("... документ {0} успешно отправлен.", isAccepted ? "подтверждения" : "отказа"));
            }
            catch (Exception exception)
            {
                onlineEvents.Add(new OnlineEvent(this, "Во время отправки акта произошла ошибка.", exception));

                Program.Logger.Error("Во время отправки акта произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Подтвердить или отказать запрос на распроведение.
        /// </summary>
        /// <param name="waybillRepeal">Запрос.</param>
        /// <param name="isAccepted">Признак - подтверждение или отказ.</param>
        /// <param name="actNumber">Номер документа.</param>
        /// <param name="actComment">Комментарий к акту.</param>
        public void SendConfirmWaybillRepeal(WaybillRepeal waybillRepeal, bool isAccepted, string actNumber, string actComment)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка отправить документ {0} по акту...", isAccepted ? "подтверждения" : "отказа"));

                sendConfirmWaybillRepeal(waybillRepeal, isAccepted, actNumber, actComment);

                onlineEvents.Add(new OnlineEvent(this, "Подтверждение (отказ) по акту успешно отправлено."));

                Program.Logger.Info(this, string.Format("... документ {0} успешно отправлен.", isAccepted ? "подтверждения" : "отказа"));
            }
            catch (Exception exception)
            {
                onlineEvents.Add(new OnlineEvent(this, "Во время отправки акта произошла ошибка.", exception));

                Program.Logger.Error("Во время отправки акта произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Переотправить подтверждение по акту.
        /// </summary>
        /// <param name="waybillAct">Акт.</param>
        public void ResendConfirmAct(WaybillAct waybillAct)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка переотправить подтверждение по акту '{0}'...", waybillAct.Description));

                resendConfirmAct(waybillAct);

                onlineEvents.Add(new OnlineEvent(this, "Повторная отправка подтверждения по акту успешно выполнена."));

                Program.Logger.Info(this, "... переотправление подтверждения по акту завершено.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время повторной отправки произошла ошибка.", exception);

                onlineEvents.Add(new OnlineEvent(this, "Во время повторной отправки произошла ошибка.", exception));

                throw;
            }
        }
        /// <summary>
        /// Отправить акт.
        /// </summary>
        /// <param name="inWaybill">Входящая накладная.</param>
        /// <param name="isAccepted">Признак акта - подтверждение или отказ.</param>
        /// <param name="actNumber">Номер акта.</param>
        /// <param name="actComment">Комментарий к акту.</param>
        public void SendSingleAct(InWaybill inWaybill, bool isAccepted, string actNumber, string actComment)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка отправить акт {0}...", isAccepted ? "подтверждения" : "отказа"));

                sendSingleAct(inWaybill, isAccepted, actNumber, actComment);

                onlineEvents.Add(new OnlineEvent(this, "Акт подтверждения (отказа) успешно отправлен."));

                Program.Logger.Info(this, string.Format("... акт {0} успешно отправлен.", isAccepted ? "подтверждения" : "отказа"));
            }
            catch (Exception exception)
            {
                onlineEvents.Add(new OnlineEvent(this, "Во время отправки акта произошла ошибка.", exception));

                Program.Logger.Error("Во время отправки акта произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Отправить акт.
        /// </summary>
        /// <param name="inWaybill">Входящая накладная.</param>
        /// <param name="positions">Список позиций.</param>
        /// <param name="actNumber">Номер акта.</param>
        /// <param name="actComment">Комментарий к акту.</param>
        public void SendDifferenceAct(InWaybill inWaybill, List<Position> positions, string actNumber, string actComment)
        {
            try
            {
                Program.Logger.Info(this, "Попытка отправить акт расхождения ...");

                sendDifferenceAct(inWaybill, positions, actNumber, actComment);

                onlineEvents.Add(new OnlineEvent(this, "Акт расхождения успешно отправлен."));

                Program.Logger.Info(this, "... акт расхождения успешно отправлен.");
            }
            catch (Exception exception)
            {
                onlineEvents.Add(new OnlineEvent(this, "Во время отправки акта произошла ошибка.", exception));

                Program.Logger.Error("Во время отправки акта произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Переотправить акт по накладной.
        /// </summary>
        /// <param name="inWaybill">Накладная.</param>
        public void ResendAct(InWaybill inWaybill)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка переотправить акт у накладной '{0}'...", inWaybill.Description));

                resendAct(inWaybill);

                onlineEvents.Add(new OnlineEvent(this, "Повторная отправка акта по накладной успешно выполнена."));

                Program.Logger.Info(this, "... переотправление акта завершено.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время повторной отправки произошла ошибка.", exception);

                onlineEvents.Add(new OnlineEvent(this, "Во время повторной отправки произошла ошибка.", exception));

                throw;
            }
        }
        /// <summary>
        /// Распроведение входящей накладной.
        /// </summary>
        /// <param name="inWaybill">Накладная.</param>
        public void Repeal(InWaybill inWaybill)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка отправить запрос на отмену проведения входящей накладной '{0}'...", inWaybill.Description));

                repeal(inWaybill);

                onlineEvents.Add(new OnlineEvent(this, string.Format("Запрос на отмену проведения входящей накладной с номером '{0}', датой '{1}' от '{2}' успешно отправлен.",
                                                                     inWaybill.Number, inWaybill.Date, inWaybill.ShipperName)));

                Program.Logger.Info(this, "... запрос на отмену проведения входящей накладной отправлен.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время отправки запроса на отмену проведения входящей накладной произошла ошибка.", exception);

                onlineEvents.Add(new OnlineEvent(this, "Во время отправки запроса на отмену проведения входящей накладной произошла ошибка.", exception));

                throw;
            }
        }
        /// <summary>
        /// Отправить запрос на получение формы 'А'.
        /// </summary>
        /// <param name="format">Формат формы.</param>
        /// <param name="regFormId">Идентификатор.</param>
        public void RequestForm(FormsFormat format, string regFormId)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка отправить запрос на получение формы '{0}' по идентификатору '{1}'...", format, regFormId));

                requestForm(format, regFormId);

                onlineEvents.Add(new OnlineEvent(this, string.Format("Запрос на получение формы '{0}' по идентификатору '{1}' успешно отправлен.", format == FormsFormat.A ? "№1 (А)" : "№2 (Б)", regFormId)));

                Program.Logger.Info(this, "... запрос на получение формы успешно отправлен.");
            }
            catch (Exception exception)
            {
                onlineEvents.Add(new OnlineEvent(this, "Во время отправки акта произошла ошибка.", exception));

                Program.Logger.Error("Во время отправки запроса на получение формы произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Добавить исходящую накладную в список и записать в хранилище.
        /// </summary>
        /// <param name="outWaybill">Исходящая накладная.</param>
        public void AddAndSaveOutWaybill(OutWaybill outWaybill)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка добавить и сохранить исходящую накладную '{0}'...", outWaybill.Description));

                addAndSaveOutWaybill(outWaybill);

                onlineEvents.Add(new OnlineEvent(this, string.Format("Исходящая накладная с номером '{0}' успешно сохранена в локальной базе данных.", outWaybill.Number)));

                Program.Logger.Info(this, "... добавление и сохранение исходящей накладной успешно завершено.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время сохранения накладной произошла ошибка.", exception);

                onlineEvents.Add(new OnlineEvent(this, "Во время сохранения накладной произошла ошибка.", exception));

                throw;
            }
        }
        /// <summary>
        /// Отправить исходящую накладную получателю.
        /// </summary>
        /// <param name="outWaybill">Накладная.</param>
        public void Send(OutWaybill outWaybill)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка отправить исходящую накладную '{0}'...", outWaybill.Description));

                send(outWaybill);

                onlineEvents.Add(new OnlineEvent(this, string.Format("Исходящая накладная с номером '{0}' для '{1}' успешно отправлена.",
                                                        outWaybill.Number, outWaybill.ConsigneeName)));

                Program.Logger.Info(this, "... исходящая накладная успешно отправлена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время отправления накладной произошла ошибка.", exception);

                onlineEvents.Add(new OnlineEvent(this, "Во время отправки накладной произошла ошибка.", exception));

                throw;
            }
        }
        /// <summary>
        /// Переотправить исходящую накладную получателю.
        /// </summary>
        /// <param name="outWaybill">Накладная.</param>
        public void Resend(OutWaybill outWaybill)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка переотправить исходящую накладную '{0}'...", outWaybill.Description));

                resend(outWaybill);

                onlineEvents.Add(new OnlineEvent(this, string.Format("Исходящая накладная с номером '{0}' для '{1}' успешно переотправлена.",
                                                        outWaybill.Number, outWaybill.ConsigneeName)));

                Program.Logger.Info(this, "... исходящая накладная успешно переотправлена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время повторной отправки накладной произошла ошибка.", exception);

                onlineEvents.Add(new OnlineEvent(this, "Во время повторной отправки произошла ошибка.", exception));

                throw;
            }
        }
        /// <summary>
        /// Отозвать исходящую накладную.
        /// </summary>
        /// <param name="outWaybill">Накладная.</param>
        public void Revoke(OutWaybill outWaybill)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка отозвать исходящую накладную '{0}'...", outWaybill.Description));

                revoke(outWaybill);

                onlineEvents.Add(new OnlineEvent(this, string.Format("Исходящая накладная с номером '{0}' для '{1}' успешно отозвана.",
                                                        outWaybill.Number, outWaybill.ConsigneeName)));

                Program.Logger.Info(this, "... исходящая накладная успешно переотправлена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время отзыва исходящей накладной произошла ошибка.", exception);

                onlineEvents.Add(new OnlineEvent(this, "Во время отзыва исходящей накладной произошла ошибка.", exception));

                throw;
            }
        }
        /// <summary>
        /// Удалить документ.
        /// </summary>
        /// <param name="document">Документ.</param>
        public void Delete(OutWaybill document)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка удалить документ '{0}'...", document.Description));

                delete(document);

                onlineEvents.Add(new OnlineEvent(this, string.Format("Исходящая накладная с номером '{0}' удалена из базы данных.", document.Number)));

                Program.Logger.Info(this, "... удаление документа успешно завершено.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время удаления документа произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Удалить документ.
        /// </summary>
        /// <param name="document">Документ.</param>
        /// <param name="enableLogging">Включение или отключение логгирования.</param>
        public void Delete(Production document, bool enableLogging = true)
        {
            try
            {
                if (enableLogging) Program.Logger.Info(this, string.Format("Попытка удалить документ '{0}'...", document.Description));

                delete(document);

                if (enableLogging) Program.Logger.Info(this, "... удаление документа успешно завершено.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время удаления документа произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Отправить запрос на повторное получение накладной по её идентификатору.
        /// </summary>
        /// <param name="wBRegId">Идентификатор накладной.</param>
        public void QueryResendWaybill(string wBRegId)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка отправить запрос на повторное получение накладной по идентификатору '{0}'...", wBRegId));

                queryResendWaybill(wBRegId);

                onlineEvents.Add(new OnlineEvent(this, string.Format("Запрос на повторное получение накладной по идентификатору '{0}' успешно отправлен", wBRegId)));

                Program.Logger.Info(this, string.Format("... запрос на повторное получение накладной по идентификатору '{0}' успешно отправлен.", wBRegId));
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время отправления запроса на повторное получение накладной произошла ошибка.", exception);

                onlineEvents.Add(new OnlineEvent(this, "Во время отправления запроса на повторное получение накладной произошла ошибка.", exception));

                throw;
            }
        }
        /// <summary>
        /// Перенести документ в корзину.
        /// </summary>
        /// <param name="document">Документ.</param>
        public void MoveToGarbage(ADocument document)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка перенести документ '{0}' в корзину...", document.Description));

                moveToGarbage(document);

                onlineEvents.Add(new OnlineEvent(this, string.Format("Документ '{0}' успешно перенесён в каталог корзины.", document.Description)));

                Program.Logger.Info(this, string.Format("... файл '{0}' успешно перенесён в каталог корзины.", document.FileName));
            }
            catch (Exception exception)
            {
                const string msg = "Во время перенесения документа в корзину произошла ошибка.";

                Program.Logger.Error(msg, exception);

                onlineEvents.Add(new OnlineEvent(this, msg, exception));

                throw;
            }
        }
        /// <summary>
        /// Восстановить документ из корзины.
        /// </summary>
        /// <param name="document">Документ.</param>
        public void RestoreFromGarbage(ADocument document)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка восстановить документ '{0}' из корзины...", document.Description));

                restoreFromGarbage(document);

                onlineEvents.Add(new OnlineEvent(this, string.Format("Документ '{0}' успешно восстановлен из каталога корзины и перенесён в каталог данных.", document.Description)));

                Program.Logger.Info(this, string.Format("... файл '{0}' успешно восстановлен из каталога корзины и перенесён в каталог данных.", document.FileName));
            }
            catch (Exception exception)
            {
                const string msg = "Во время восстановления документа из корзины произошла ошибка.";

                Program.Logger.Error(msg, exception);

                onlineEvents.Add(new OnlineEvent(this, msg, exception));

                throw;
            }
        }
        /// <summary>
        /// Загрузить список событий.
        /// </summary>
        public void LoadOnlineEvents()
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузить список событий...");

                loadOnlineEvents();

                Program.Logger.Info(this, "... список событий успешно загружен.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время загрузки списка событий произошла ошибка.", exception);

                // Загрузка списка событий - операция не очень критичная...
                // Ошибку подавляем.
                // throw;
            }
        }
        /// <summary>
        /// Сохранить список событий.
        /// </summary>
        public void SaveOnlineEvents()
        {
            try
            {
                Program.Logger.Info(this, "Попытка сохранить список событий...");

                saveOnlineEvents();

                Program.Logger.Info(this, "... список событий успешно сохранён.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время сохранения списка событий произошла ошибка.", exception);

                // Сохранение списка событий - операция не очень критичная...
                // Ошибку подавляем.
                // throw;
            }
        }
        /// <summary>
        /// Получить список последних остатков на складе организации.
        /// </summary>
        /// <param name="suppressException">Подавить исключение в случае ошибки.</param>
        /// <returns>Список последних остатков.</returns>
        public List<RestsPosition> GetLastRestsPositions(bool suppressException = false)
        {
            try
            {
                Program.Logger.Info(this, "Попытка получить позиции последнего документа-остатков на складе организации...");

                if (restsList == null) throw new Exception("Список документов остатков на складе организации не создан. Получение позиций невозможно.");
                if (restsList.Count == 0) return new List<RestsPosition>();

                Rests lastRest = restsList[0];

                foreach (Rests rest in restsList)
                {
                    if (rest.CreateDateTime >= lastRest.CreateDateTime) lastRest = rest;
                }

                List<RestsPosition> positions = lastRest.GetPositions(suppressException);

                Program.Logger.Info(this, string.Format("... получение позиций документа-остатков на складе организации (в количестве {0} шт.) успешно завершено.", positions.Count));

                return positions;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время получения позиций последнего документа-остатков на складе организации произошла ошибка.", exception);

                if (suppressException)
                {
                    Program.Logger.Warn(this, "Исключение подавлено. Далее передан пустой список позиций.");

                    return new List<RestsPosition>();
                }

                throw;
            }
        }
        /// <summary>
        /// Получить список последних остатков в торговом зале.
        /// </summary>
        /// <param name="suppressException">Подавить исключение в случае ошибки.</param>
        /// <returns>Список последних остатков.</returns>
        public List<RestsPosition> GetLastShopRestsPositions(bool suppressException = false)
        {
            try
            {
                Program.Logger.Info(this, "Попытка получить позиции последнего документа-остатков в торговом зале...");

                if (shopRestsList == null) throw new Exception("Список документов остатков в торговом зале не создан. Получение позиций невозможно.");
                if (shopRestsList.Count == 0) return new List<RestsPosition>();

                ShopRests lastRest = shopRestsList[0];

                foreach (ShopRests shopRest in shopRestsList)
                {
                    if (shopRest.CreateDateTime >= lastRest.CreateDateTime) lastRest = shopRest;
                }

                List<RestsPosition> positions = lastRest.GetPositions(suppressException);

                Program.Logger.Info(this, string.Format("... получение позиций документа-остатков в торговом зале (в количестве {0} шт.) успешно завершено.", positions.Count));

                return positions;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время получения позиций последнего документа-остатков в торговом зале произошла ошибка.", exception);

                if (suppressException)
                {
                    Program.Logger.Warn(this, "Исключение подавлено. Далее передан пустой список позиций.");

                    return new List<RestsPosition>();
                }

                throw;
            }
        }
        /// <summary>
        /// Добавить акт в список (по необходимости) и записать в хранилище.
        /// </summary>
        /// <param name="act">Акт.</param>
        public void AddAndSave(AMovement act)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка добавить и сохранить акт движения товара '{0}'...", act.Description));

                switch (act.GetType().Name)
                {
                    case "ActChargeOn": { addAndSave(act as ActChargeOn); break; }
                    case "ActChargeOff": { addAndSave(act as ActChargeOff); break; }
                    case "ActChargeOnShop": { addAndSave(act as ActChargeOnShop); break; }
                    case "ActChargeOffShop": { addAndSave(act as ActChargeOffShop); break; }
                    case "TransferToShop": { addAndSave(act as TransferToShop); break; }
                    case "TransferFromShop": { addAndSave(act as TransferFromShop); break; }
                    case "ActFixBarcode": { addAndSave(act as ActFixBarcode); break; }
                    case "ActUnFixBarcode": { addAndSave(act as ActUnFixBarcode); break; }

                    default: throw new Exception(string.Format("Работа с классом '{0}' не поддерживается в данной версии.", act.GetType().FullName));
                }

                onlineEvents.Add(new OnlineEvent(this, string.Format("Акт движения товара с номером '{0}' от '{1}' ('{2}') успешно сохранен в локальной базе данных.", act.Number, act.Date, act.GetType().Name)));

                Program.Logger.Info(this, "... добавление и сохранение акта движения товара успешно завершено.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время сохранения акта движения товара  произошла ошибка.", exception);

                onlineEvents.Add(new OnlineEvent(this, "Во время сохранения акта движения товара произошла ошибка.", exception));

                throw;
            }
        }
        /// <summary>
        /// Отправить акт.
        /// </summary>
        /// <param name="act">Акт.</param>
        public void Send(AMovement act)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка отправить акт движения товара '{0}'...", act.Description));

                switch (act.GetType().Name)
                {
                    case "ActChargeOn": { send(act as ActChargeOn); break; }
                    case "ActChargeOff": { send(act as ActChargeOff); break; }
                    case "ActChargeOnShop": { send(act as ActChargeOnShop); break; }
                    case "ActChargeOffShop": { send(act as ActChargeOffShop); break; }
                    case "TransferToShop": { send(act as TransferToShop); break; }
                    case "TransferFromShop": { send(act as TransferFromShop); break; }
                    case "ActFixBarcode": { send(act as ActFixBarcode); break; }
                    case "ActUnFixBarcode": { send(act as ActUnFixBarcode); break; }

                    default: throw new Exception(string.Format("Работа с классом '{0}' не поддерживается в данной версии.", act.GetType().FullName));
                }

                onlineEvents.Add(new OnlineEvent(this, string.Format("Акт движения товара  с номером '{0}' от '{1}' ('{2}') успешно отправлен.", act.Number, act.Date, act.GetType().Name)));

                Program.Logger.Info(this, "... акт движения товара успешно отправлен.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время отправления акта движения товара произошла ошибка.", exception);

                onlineEvents.Add(new OnlineEvent(this, "Во время отправки акта движения товара произошла ошибка.", exception));

                throw;
            }
        }
        /// <summary>
        /// Отменить проведение акта.
        /// </summary>
        /// <param name="act">Акт.</param>
        public void Repeal(AMovement act)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка отменить проведение акта движения товара '{0}'...", act.Description));

                switch (act.GetType().Name)
                {
                    case "ActChargeOn": { repeal(act as ActChargeOn); break; }
                    case "ActChargeOff": { repeal(act as ActChargeOff); break; }

                    default: throw new Exception(string.Format("Работа с классом '{0}' не поддерживается в данной версии.", act.GetType().FullName));
                }

                onlineEvents.Add(new OnlineEvent(this, string.Format("Запрос на отмену проведения акта движения товара с номером '{0}' от '{1}' ('{2}') успешно отправлен.", act.Number, act.Date, act.GetType().Name)));

                Program.Logger.Info(this, "... акт движения товара успешно отменён.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время отправления запроса на отмену проведения акта движения товара произошла ошибка.", exception);

                onlineEvents.Add(new OnlineEvent(this, "Во время отправления запроса на отмену проведения акта движения товара произошла ошибка.", exception));

                throw;
            }
        }
        /// <summary>
        /// Удалить документ.
        /// </summary>
        /// <param name="act">Акт.</param>
        public void Delete(AMovement act)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка удалить акт движения товара '{0}'...", act.Description));

                switch (act.GetType().Name)
                {
                    case "ActChargeOn": { delete(act as ActChargeOn); break; }
                    case "ActChargeOff": { delete(act as ActChargeOff); break; }
                    case "ActChargeOnShop": { delete(act as ActChargeOnShop); break; }
                    case "ActChargeOffShop": { delete(act as ActChargeOffShop); break; }
                    case "TransferToShop": { delete(act as TransferToShop); break; }
                    case "TransferFromShop": { delete(act as TransferFromShop); break; }

                    default: throw new Exception(string.Format("Работа с классом '{0}' не поддерживается в данной версии.", act.GetType().FullName));
                }


                onlineEvents.Add(new OnlineEvent(this, string.Format("Акт движения товара с номером '{0}' от '{1}' ('{2}') удалена из базы данных.", act.Number, act.Date, act.GetType().Name)));

                Program.Logger.Info(this, "... удаление акта движения товара успешно завершено.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время удаления акта движения товара произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Найти входящую накладную по идентификатору справки 'Б' одной из её позиций.
        /// Если накладная не найдена, то будет возвращено значение 'null'.
        /// </summary>
        /// <param name="formBRegId">Идентификатор справки 'Б'.</param>
        /// <returns>Накладная.</returns>
        public InWaybill FindInWaybill(string formBRegId)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка найти входящую накладную по идентификатору справки 'Б' ('{0}')одной из её позиций...", formBRegId));

                InWaybill inWaybill = findInWaybill(formBRegId);

                Program.Logger.Info(this, string.Format("... найдена входящая накладная: '{0}'...", (inWaybill == null) ? "null": inWaybill.Description));

                Program.Logger.Info(this, "... поиск входящей накладной завершён.");

                return inWaybill;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время поиска входящей накладной произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Осуществить поиск в файлах базы данных.
        /// </summary>
        /// <param name="findtext">Строка для поиска.</param>
        /// <returns>Документы, содержащие указанную строку.</returns>
        public ThreadedBindingList<ADocument> FullTextSearch(string findtext)
        {
            Program.Logger.Info(this, string.Format("Попытка выполнения полнотекстового поиска по строке '{0}'... ", findtext));

            ThreadedBindingList<ADocument> list = fullTextSearch(findtext);

            onlineEvents.Add(new OnlineEvent(this, string.Format("Завершено выполнение полнотекстового поиска по строке '{0}'; найдено документов: {1}.", findtext, list.Count)));

            Program.Logger.Info(this, string.Format("... выполнение полнотекстового поиска по строке '{0}' завершено; найдено документов: {1}.", findtext, list.Count));

            return list;
        }
        /// <summary>
        /// Экспорт документа в систему "AxiTrade".
        /// </summary>
        /// <param name="waybill">Документ.</param>
        /// <param name="filename">Имя файла для экспорта.</param>
        public void ExportToAxiTrade(InWaybill waybill, string filename)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка экспорта документа '{0}' в файл '{1}' формата 'AxiTrade'...", waybill.Description, filename));

                exportToAxiTrade(waybill, filename);

                onlineEvents.Add(new OnlineEvent(this, string.Format("Входящая накладная с номером '{0}' от '{1}' экспортирована в файл '{2}' формата 'AxiTrade'.", waybill.Number, waybill.Date, filename)));

                Program.Logger.Info(this, string.Format("... экспорт документа '{0}' в файл '{1}' формата 'AxiTrade' успешно завершён.", waybill.Description, filename));
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время экспорта документа в формат 'AxiTrade' произошла ошибка.", exception);

                throw;
            }
        }
        #endregion Внешние методы класса.
    }
}
