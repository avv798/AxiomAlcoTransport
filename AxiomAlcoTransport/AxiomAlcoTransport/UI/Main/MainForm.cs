using System;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;
using System.Windows.Threading;
using System.Collections.Generic;
using Axiom.AlcoTransport.Document;
using Microsoft.Win32;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Alerter;
using Axiom.AlcoTransport.Watchtower.Configuration;

namespace Axiom.AlcoTransport
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        #region Внутренние объекты класса.
        /// <summary>
        /// Конфигурация приложения.
        /// </summary>
        private ConfigurationData configuration;
        /// <summary>
        /// Хранилище.
        /// </summary>
        private FileStorage storage;
        /// <summary>
        /// Адреса сервера УТМ.
        /// </summary>
        private Addresses addresses;
        /// <summary>
        /// Список документов.
        /// </summary>
        private Documents documents;
        #endregion Внутренние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public MainForm()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                #region Обработка необработанных исключений.
                AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;
                #endregion Обработка необработанных исключений.
                
                InitializeComponent();

                setItemsEnabled(false);

                Program.Logger.Info(this, "... инициализация успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации произошла ошибка.", exception);
            }
        }
		#endregion Конструкторы класса.

        #region Внутренние методы класса.
        /// <summary>
        /// Загрузить конфигурацию.
        /// </summary>
        private void loadConfiguration()
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузить конфигурацию приложения...");

                configuration = ConfigurationHelper.LoadConfiguration(Program.GetAppSetting("configurationFilename"));
                configuration.Check();

                addresses = new Addresses(configuration.UtmAddress, configuration.UtmPort);

                Db_barStaticItem.Caption = string.Format("Локальная база данных: \"{0}\"", configuration.PathToLocalDatabase);
                Version_barStaticItem.Caption = string.Format("Версия: {0}", GetNumbersOfVersion(3));
                Address_barStaticItem.Caption = string.Format("Адрес сервера УТМ: \"{0}\"", addresses.Main);

                storage = new FileStorage(configuration.PathToLocalDatabase);
                documents = new Documents(configuration, addresses, storage);

                storage.Prepare();
                storage.Backup();

                documents.LoadDatabase();
                documents.DocumentReceivedEvent += handleDocumentReceived;

                documents.ClearOutBufferOnStartup();

                if (EnableInterval_barButtonItem.Down) documents.StartBackgroundRequest();

                Program.Logger.Info(this, string.Format("... загрузка конфигурации приложения успешно завершена. Данные конфигурации: '{0}'.", configuration));
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Ошибка загрузки конфигурации приложения.", exception);

                throw;
            }
        }
        /// <summary>
        /// Обработка события "получение нового документа".
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Аргументы.</param>
        private void handleDocumentReceived(object sender, DocumentReceivedEventArgs e)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка показать область напоминаний для события '{0}'...", e.GetType().FullName));

                if (InvokeRequired)
                {
                    Program.Logger.Info(this, "... вызов из фонового процесса ...");

                    BeginInvoke(new showNotificationDelegate(showNotification), e.List);
                }
                else
                {
                    Program.Logger.Info(this, "... вызов из основного процесса ...");

                    showNotification(e.List);
                }

                Program.Logger.Info(this, "... область напоминаний успешно показана.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Неожиданное исключение во время показа области напоминаний.", exception);
            }
        }
        /// <summary>
        /// Делегат для показа области напоминаний.
        /// </summary>
        /// <param name="list">Список документов.</param>
        private delegate void showNotificationDelegate(List<string> list);
        /// <summary>
        /// Показать область напоминания.
        /// </summary>
        /// <param name="list">Список документов.</param>
        private void showNotification(List<string> list)
        {
            try
            {
                Program.Logger.Info(this, "Попытка показать форму напоминаний...");

                string message = string.Format("Сообщение от {0}.\r\nПолучены новые документы:\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                if (list != null)
                {
                    message = list.Aggregate(message, (current, s) => (current + string.Format("  '{0}'\r\n", s)));
                }
                else
                {
                    message += "  'Неизвестный тип документа'.";
                }

                AlertInfo info = new AlertInfo("ЕГАИС. Служба доставки документов", message.Trim());

                if ((Notify_imageCollection.Images != null) && (Notify_imageCollection.Images.Count > 0))
                {
                    info.Image = Notify_imageCollection.Images[0];
                }

                Main_alertControl.Show(this, info);

                Program.Logger.Info(this, "... форма напоминаний успешно показана.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Неожиданное исключение во время показа области напоминаний.", exception);
            }
        }
        /// <summary>
        /// Загрузка оформления.
        /// </summary>
        private void loadVisualSettings()
        {
            try
            {
                string skinName = Constants.DefaultVisualStyle;

                try
                {
                    RegistryKey registry = Registry.CurrentUser.OpenSubKey(Constants.RegistryPathVisualSettings);
                    if (registry == null) throw new Exception(string.Format("Не найден путь в реестре '{0}'.", Constants.RegistryPathVisualSettings));

                    skinName = registry.GetValue("ActiveSkinName").ToString();

                    registry.Close();
                }
                catch (Exception exception)
                {
                    Program.Logger.Error("Во время поиска конфигурации в реестре произошла ошибка.", exception);
                }

                Main_defaultLookAndFeel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
                Main_defaultLookAndFeel.LookAndFeel.SetSkinStyle(skinName);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время загрузки оформления произошла ошибка.", exception);
            }
        }
        /// <summary>
        /// Сохранение оформления.
        /// </summary>
        private void saveVisualSettings()
        {
            try
            {
                RegistryKey registry = Registry.CurrentUser.CreateSubKey(Constants.RegistryPathVisualSettings);
                if (registry == null) throw new Exception(string.Format("Не найден путь в реестре '{0}'.", Constants.RegistryPathVisualSettings));

                registry.SetValue("ActiveSkinName", Main_defaultLookAndFeel.LookAndFeel.ActiveSkinName);

                registry.Close();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время сохранения оформления произошла ошибка.", exception);
            }
        }
        /// <summary>
        /// Установка статусов доступности управляющим элементам.
        /// </summary>
        /// <param name="enabled">Признак доступности.</param>
        private void setItemsEnabled(bool enabled)
        {
            try
            {
                foreach (BarItem item in Main_ribbon.Items)
                {
                    if (item is BarStaticItem) continue;

                    if (item.Tag != null)
                    {
                        if (item.Tag.ToString() == "NotImplemented") continue;
                    }

                    item.Enabled = enabled;
                }

                About_barButtonItem.Enabled = true;
                Close_barButtonItem.Enabled = true;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время активации управляющих элементов произошла ошибка.", exception);
            }
        }
        /// <summary>
        /// Установка доступности кнопок ленты.
        /// </summary>
        private void setButtonEnabled()
        {
            try
            {
                All_barMdiChildrenListItem.Enabled
                    = CloseCurrent_barButtonItem.Enabled
                    = CloseAll_barButtonItem.Enabled
                    = (Main_xtraTabbedMdiManager.Pages.Count > 0);

                #region GC...
                GC.Collect();
                #endregion GC...
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Ошибка установки доступности кнопок ленты.", exception);
            }
        }
        #endregion Внутренние методы класса.

        #region Периодическая запись статистической информации.
        /// <summary>
        /// Таймер для периодической записи статистической информации.
        /// </summary>
        private DispatcherTimer statisticTimer;
        private void onStatisticTimedEvent(object source, EventArgs e)
        {
            logStatistics();
        }
        /// <summary>
        /// Сохранение статистических данных.
        /// </summary>
        private void logStatistics()
        {
            try
            {
                Program.Logger.Info(this, "Попытка сбора статистики работы приложения...");

                string stat = "... сбор статистических параметров...";

                stat += string.Format("\r\n\t\t Версия операционной системы: '{0}' (64 bits: '{1}').", Environment.OSVersion, Environment.Is64BitOperatingSystem);
                stat += string.Format("\r\n\t\t Версия CLR: '{0}'.", Environment.Version);
                stat += string.Format("\r\n\t\t Наименование компьютера: '{0}'.", Environment.MachineName);
                stat += string.Format("\r\n\t\t Пользователь: '{0}\\{1}'.", Environment.UserDomainName, Environment.UserName);
                stat += string.Format("\r\n\t\t Приложение запущено из каталога: '{0}'.", Application.ExecutablePath);
                stat += string.Format("\r\n\t\t Наименование приложения: '{0}'.", Application.ProductName);
                stat += string.Format("\r\n\t\t Версия приложения: '{0}'.", Application.ProductVersion);
                stat += string.Format("\r\n\t\t Идентификатор процесса: {0}.", Process.GetCurrentProcess().Id);
                stat += string.Format("\r\n\t\t Наименование процесса: '{0}'.", Process.GetCurrentProcess().ProcessName);
                stat += string.Format("\r\n\t\t Время запуска связанного процесса: {0}.", Process.GetCurrentProcess().StartTime.ToString("yyyy-MM-dd (dddd) HH:mm:ss.FFF"));
                stat += string.Format("\r\n\t\t Время работы приложения: {0} сек.", DateTime.Now.Subtract(Process.GetCurrentProcess().StartTime).TotalSeconds.ToString("N2"));
                stat += string.Format("\r\n\t\t Базовый приоритет связанного процесса: {0}.", Process.GetCurrentProcess().BasePriority);
                stat += string.Format("\r\n\t\t Количество потоков, выполняющихся в связанном процессе: {0}.", Process.GetCurrentProcess().Threads.Count);
                stat += string.Format("\r\n\t\t Число дескрипторов операционной системы, открытых процессом: {0}.", Process.GetCurrentProcess().HandleCount);
                stat += string.Format("\r\n\t\t Полное время процессора для процесса: {0} сек.", Process.GetCurrentProcess().TotalProcessorTime.TotalSeconds.ToString("N2"));
                stat += string.Format("\r\n\t\t Пользовательское время процессора для этого процесса: {0} сек.", Process.GetCurrentProcess().UserProcessorTime.TotalSeconds.ToString("N2"));
                stat += string.Format("\r\n\t\t Использование физической памяти связанного процесса: {0} кБ.", Process.GetCurrentProcess().WorkingSet64 / 1024);
                stat += string.Format("\r\n\t\t Максимальное количество физической памяти, используемой связанным процессом: {0} кБ.", Process.GetCurrentProcess().PeakWorkingSet64 / 1024);
                stat += string.Format("\r\n\t\t Количество открытых дочерних окон в MDI-интерфейсе: {0} шт.", MdiChildren.Count());

                Program.Logger.Info(this, stat);

                Program.Logger.Info(this, "... сбор статистики успешно завершён.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("В процессе сбора статистики произошла ошибка.", exception);
            }
        }
        #endregion Периодическая проверка работоспособности терминала.

        #region Обработка необработанных исключений.
        private void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Program.Logger.Fatal(string.Format("{0}: Фатальная исключение. Источник: '{1}'.", GetType().FullName, sender), (Exception)e.ExceptionObject);
        }
        #endregion Обработка необработанных исключений.

		#region Обработка событий пользовательского интерфейса.
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                logStatistics();

                Program.Logger.Info(this, "Попытка загрузки... ");

                SplashAccessor.Show();

                loadVisualSettings();

                loadConfiguration();

                showHomeForm();

                Main_ribbon.SelectedPage = AllDocument_ribbonPage;

                #region Таймер периодической записи статистической информации.
                statisticTimer = new DispatcherTimer();
                statisticTimer.Tick += onStatisticTimedEvent;
                statisticTimer.Interval = new TimeSpan(0, 0, 300);
                statisticTimer.Start();
                #endregion Таймер периодической записи статистической информации.

                string firstLoading = string.Empty;

                if (documents != null)
                {
                    documents.OnlineEvents.Add(new OnlineEvent(this, string.Format("Начало работы приложения (идентификатор процесса - '{0}').", Process.GetCurrentProcess().Id)));

                    try
                    {
                        documents.GetDocuments();
                    }
                    catch (Exception innerException)
                    {
                        firstLoading = innerException.Message;

                        Program.Logger.Error("Во время получения документов произошла ошибка.", innerException);
                    }
                }

                setItemsEnabled(true);

                Clock_timer.Enabled = true;

                SplashAccessor.Close();
                Cursor = Cursors.Default;

                if (!string.IsNullOrWhiteSpace(firstLoading))
                {
                    ShowErrorMessage(string.Format("Во время получения документов произошла ошибка.\r\n{0}", firstLoading));
                }

                Program.Logger.Info(this, "... загрузка успешно завершена.");

                logStatistics();
            }
            catch (Exception exception)
            {
                setItemsEnabled(false);

                Cursor = Cursors.Default;

                const string msg = "Во время загрузки произошла ошибка.";
                Program.Logger.Error(msg, exception);

                SplashAccessor.Close();
                Cursor = Cursors.Default;

                ShowErrorMessage(string.Format("{0}\r\n{1}", msg, exception.Message));
            }
            finally
            {
                SplashAccessor.Close();
                Cursor = Cursors.Default;
            }
        }
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (DialogResult.Yes == XtraMessageBox.Show("Завершить работу приложения?"
                                                          , "Завершение работы"
                                                          , MessageBoxButtons.YesNo
                                                          , MessageBoxIcon.Question))
                    return;

                e.Cancel = true;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
                ShowErrorMessage(exception);
            }
        }
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                if (documents != null)
                {
                    documents.OnlineEvents.Add(new OnlineEvent(this, string.Format("Завершение работы приложения (идентификатор процесса - '{0}', время работы - {1} сек.).",
                                                                                   Process.GetCurrentProcess().Id,
                                                                                   DateTime.Now.Subtract(Process.GetCurrentProcess().StartTime).TotalSeconds.ToString("N2"))));
                    documents.SaveOnlineEvents();
                }

                saveVisualSettings();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        private void About_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                (new AboutForm()).ShowDialog(this);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
                ShowErrorMessage(exception);
            }
        }
        private void Close_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            Close();
        }
        private void Configuration_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                (new OptionsForm(configuration)).ShowDialog(this);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
                ShowErrorMessage(exception);
            }
        }
        private void Home_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showHomeForm();
        }
        private void Productions_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showProductionForm();
        }
        private void Partner_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showPartnerForm();
        }
        private void CloseCurrent_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (ActiveMdiChild != null) ActiveMdiChild.Close();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время закрытия дочернего окна произошла ошибка.", exception);
                ShowErrorMessage(exception);
            }
        }
        private void CloseAll_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка закрыть все окна... ");

                foreach (Form child in MdiChildren)
                {
                    child.Close();
                }

                Program.Logger.Info(this, "... попытка закрыть все окна успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время закрытия всех дочерних окон произошла ошибка.", exception);
                ShowErrorMessage(exception);
            }
        }
        private void Main_xtraTabbedMdiManager_PageAdded(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            setButtonEnabled();
        }
        private void Main_xtraTabbedMdiManager_PageRemoved(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            setButtonEnabled();
        }
        private void InWaybill_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showInWaybillForm();
        }
        private void GetRequestIn_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                SplashAccessor.Show();

                Cursor = Cursors.WaitCursor;

                Ribbon.Enabled = false;

                documents.GetDocuments();
            }
            catch (Exception exception)
            {
                const string msg = "Во время получения входящих документов произошла ошибка.";
                Program.Logger.Error(msg, exception);

                Ribbon.Enabled = true;
                SplashAccessor.Close();
                Cursor = Cursors.Default;

                ShowErrorMessage(string.Format("{0}\r\n{1}", msg, exception.Message));
            }
            finally
            {
                Ribbon.Enabled = true;
                Cursor = Cursors.Default;

                SplashAccessor.Close();
            }
        }
        private void CLearOutCache_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (DialogResult.No == XtraMessageBox.Show("Очистить исходящий буфер запросов, отправленных на сервер?\r\n" +
                                                           "В этом случае необработанные сервером запросы могут остаться без ожидаемого ответа."
                                                          , "Подтверждение операции"
                                                          , MessageBoxButtons.YesNo
                                                          , MessageBoxIcon.Question)) return;

                SplashAccessor.Show();

                Cursor = Cursors.WaitCursor;

                Ribbon.Enabled = false;

                documents.ClearOutBuffer();
            }
            catch (Exception exception)
            {
                const string msg = "Во время очистки буфера исходящих запросов произошла ошибка.";
                Program.Logger.Error(msg, exception);

                Ribbon.Enabled = true;
                SplashAccessor.Close();
                Cursor = Cursors.Default;

                ShowErrorMessage(string.Format("{0}\r\n{1}", msg, exception.Message));
            }
            finally
            {
                Ribbon.Enabled = true;
                Cursor = Cursors.Default;

                SplashAccessor.Close();
            }
        }
        private void Tickets_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showTicketForm();
        }
        private void VisualDefault_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (XtraMessageBox.Show("Вы уверены, что хотите восстановить оформление приложения в значение \"по умолчанию\"?\r\n" +
                                        "Все пользовательские настройки таблиц данных (фильтры, группировки, сортировки и т.д.) будут удалены.",
                                        "Подтверждение операции",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.No) return;

                Program.Logger.Info(this, "Попытка восстановления оформления приложения...");

                if (MdiChildren.Any())
                {
                    foreach (Form child in MdiChildren)
                    {
                        child.Close();
                    }
                }

                try
                {
                    Registry.CurrentUser.DeleteSubKeyTree(Constants.RegistryPathVisualSettings);
                }
                catch (Exception exception)
                {
                    Program.Logger.Error("Во время сброса оформления (при работе с реестром) произошла ошибка.", exception);
                }

                Main_defaultLookAndFeel.LookAndFeel.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
                Main_defaultLookAndFeel.LookAndFeel.SetSkinStyle(Constants.DefaultVisualStyle);

                showHomeForm();

                Program.Logger.Info(this, "... восстановление оформления успешно завершено.");

                XtraMessageBox.Show("Оформление приложения успешно восстановлено в значение 'по умолчанию'.",
                                    "Восстановление оформления",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время восстановления оформления произошла ошибка.", exception);

                ShowErrorMessage(string.Format("Во время восстановления оформления произошла ошибка: '{0}'.", exception.Message));
            }
        }
        private void EnableInterval_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (EnableInterval_barButtonItem.Down)
                {
                    documents.StartBackgroundRequest();
                }
                else
                {
                    documents.StopBackgroundRequest();
                }
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void LicenceInfo_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                (new LicenceForm()).ShowDialog(this);
            }
            catch (Exception exception)
            {
                Program.Logger.Error(this, exception.Message);
                ShowErrorMessage(exception);
            }
        }
        private void Rests_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showRestsForm<Rests>();
        }
        private void RestsShop_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showRestsForm<ShopRests>();
        }
        private void BarcodeRest_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showRestsForm<BCodeRests>();
        }
        private void AnswerAct_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showWaybillActForm();
        }
        private void CancelWaybill_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showWaybillRepealForm();
        }
        private void FormA_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showFormAForm();
        }
        private void FormB_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showFormBForm();
        }
        private void OnlineEvents_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showOnlineEventForm();
        }
        private void Clock_timer_Tick(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    DateTime now = DateTime.Now;

                    Date_barStaticItem.Caption = now.ToString("dd MMMM yyyy г. (dddd)");
                    Time_barStaticItem.Caption = now.ToString("HH:mm:ss");
                }
                catch (Exception exception)
                {
                    Program.Logger.Error("Во время обработки события часового таймера произошла ошибка.", exception);

                    Date_barStaticItem.Visibility = BarItemVisibility.Never;
                    Time_barStaticItem.Visibility = BarItemVisibility.Never;

                    Clock_timer.Enabled = false;
                }
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Необработанное исключение в событии часового таймера.", exception);
            }
        }
        private void OutWaybill_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showOutWaybillForm();
        }
        private void Main_alertControl_AlertClick(object sender, AlertClickEventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка активации приложения из области напоминания...");

                e.AlertForm.Close();

                WindowState = FormWindowState.Maximized;
                Activate();

                Program.Logger.Info(this, "... активация приложения успешно выполнена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Неожиданное исключение во время принудительного закрытия области напоминаний.", exception);
            }
        }
        private void Copyright_barStaticItem_ItemDoubleClick(object sender, ItemClickEventArgs e)
        {
            OpenCompanySite();
        }
        private void Help_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenUserGuide();
        }
        private void Garbage_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showGarbageForm();
        }
        private void ActAdd_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showMovementForm<ActChargeOn>();
        }
        private void ActDel_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showMovementForm<ActChargeOff>();
        }
        private void ToShop_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showMovementForm<TransferToShop>();
        }
        private void FromShop_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showMovementForm<TransferFromShop>();
        }
        private void AddToShop_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showMovementForm<ActChargeOnShop>();
        }
        private void RemoveFromShop_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showMovementForm<ActChargeOffShop>();
        }
        private void RawWaybill_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showNotAnswerForm();
        }
        private void HistoryB_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showHistoryFormBForm();
        }
        private void TickectWaybill_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showWaybillTicketForm();
        }
        private void MarkBarcode_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showMarkBarcodeForm();
        }
        private void barCodeAct_ButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showMovementForm<ActFixBarcode>();
        }

        private void BarcodeActCancel_ButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showMovementForm<ActUnFixBarcode>();
        }
        private void FullTextSearch_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                (new FullTextSearchForm(documents)).ShowDialog(this);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
                ShowErrorMessage(exception);
            }
        }
        private void MainForm_MdiChildActivate(object sender, EventArgs e)
        {
            try
            {
                if (ActiveMdiChild == null) return;

                if (ActiveMdiChild is HomeForm)
                {
                    (ActiveMdiChild as HomeForm).RefreshImportantDocuments();
                }
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        #endregion Обработка событий пользовательского интерфейса.

        
    }
}