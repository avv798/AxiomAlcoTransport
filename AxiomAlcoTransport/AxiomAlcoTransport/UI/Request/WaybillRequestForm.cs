using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Axiom.AlcoTransport.Watchtower.Configuration;

namespace Axiom.AlcoTransport
{
    public partial class WaybillRequestForm : XtraForm
    {
        #region Внутренние объекты класса.
        /// <summary>
        /// Накладная.
        /// </summary>
        private AWaybill waybill;
        /// <summary>
        /// Управление документами.
        /// Только чтение.
        /// </summary>
        private readonly Documents documents;
        /// <summary>
        /// Список накладных.
        /// Только чтение.
        /// </summary>
        private readonly ThreadedBindingList<AWaybill> list;
        #endregion Внутренние объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Накладная.
        /// Только чтение.
        /// </summary>
        public AWaybill Waybill
        {
            get { return waybill; }
        }
        #endregion Внешние объекты класса.

        #region Инициализация главной формы.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public WaybillRequestForm()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                documents = null;
                waybill = null;

                list = new ThreadedBindingList<AWaybill>();

                Program.Logger.Info(this, "... инициализация успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации произошла ошибка.", exception);
            }
        }

        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="documents">Управление документами.</param>
        /// <param name="caption">Заголовок-комментарий..</param>
        public WaybillRequestForm(Documents documents, string caption) : this()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации с параметрами... ");

                this.documents = documents;
                Caption_labelControl.Text = caption;

                foreach (InWaybill inWaybill in documents.InWaybills)
                {
                    list.Add(inWaybill);
                }
                foreach (OutWaybill outWaybill in documents.OutWaybills)
                {
                    list.Add(outWaybill);
                }

                Ok_simpleButton.Enabled = (list.Count > 0);

                Document_BindingSource.DataSource = list;

                Program.Logger.Info(this, "... инициализация с параметрами успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации произошла ошибка.", exception);
            }
        }
        #endregion Инициализация главной формы.

        #region Обработка событий пользовательского интерфейса.
        private void WaybillRequestForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузки формы... ");

                loadGridLayot();

                Program.Logger.Info(this, "... загрузка формы успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время загрузки произошла ошибка.", exception);
            }
        }
        private void WaybillRequestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Форма закрывается.");

                saveGridLayot();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время закрытия произошла ошибка.", exception);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void Cancel_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        private void Ok_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Document_BindingSource.Current == null) return;

                AWaybill selected = (AWaybill)Document_BindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                waybill = selected;

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);

                MainForm.ShowErrorMessage(string.Format("Во время выбора товарно-транспортной накладной произошла ошибка: '{0}'.", exception.Message));
            }
        }
        #endregion Обработка событий пользовательского интерфейса.

        #region Раскладка грида.
        /// <summary>
        /// Путь в реестре для сохранения раскладки грида.
        /// Только чтение.
        /// </summary>
        private string registryPathVisualSettings
        {
            get { return string.Format("{0}\\{1}", Constants.RegistryPathVisualSettings, GetType().Name); }
        }
        /// <summary>
        /// Загрузка оформления редактора.
        /// </summary>
        private void loadGridLayot()
        {
            try
            {
                Document_gridView.RestoreLayoutFromRegistry(registryPathVisualSettings);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции загрузки раскладки грида произошла ошибка.", exception);
            }
        }
        /// <summary>
        /// Сохранение оформления редактора.
        /// </summary>
        private void saveGridLayot()
        {
            try
            {
                Document_gridView.SaveLayoutToRegistry(registryPathVisualSettings);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции сохранения раскладки грида произошла ошибка.", exception);
            }
        }
        #endregion Раскладка грида.
    }
}