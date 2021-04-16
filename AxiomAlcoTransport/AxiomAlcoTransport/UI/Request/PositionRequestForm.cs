using System;
using System.Windows.Forms;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using Axiom.AlcoTransport.Watchtower.Configuration;

namespace Axiom.AlcoTransport
{
    public partial class PositionRequestForm : XtraForm
    {
        #region Внутренние объекты класса.
        /// <summary>
        /// Позиция накладной.
        /// </summary>
        private Position position;
        /// <summary>
        /// Управление документами.
        /// Только чтение.
        /// </summary>
        private readonly Documents documents;
        /// <summary>
        /// Список накладных.
        /// Только чтение.
        /// </summary>
        private readonly ThreadedBindingList<AWaybill> waybills;
        #endregion Внутренние объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Позиция накладной.
        /// Только чтение.
        /// </summary>
        public Position Position
        {
            get { return position; }
        }
        #endregion Внешние объекты класса.

        #region Инициализация главной формы.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public PositionRequestForm()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                documents = null;
                position = null;

                waybills = new ThreadedBindingList<AWaybill>();

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
        public PositionRequestForm(Documents documents, string caption) : this()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации с параметрами... ");

                this.documents = documents;
                Caption_labelControl.Text = caption;

                foreach (InWaybill inWaybill in documents.InWaybills)
                {
                    waybills.Add(inWaybill);
                }

                foreach (OutWaybill outWaybill in documents.OutWaybills)
                {
                    // Пока не очень понятно, нужно ли показывать накладные со статусом "Sent"...
                    // Пусть пока отображаются, особого вреда не будет.
                    if ((outWaybill.StatusEnum == OutWaybillStatus.Sent) 
                        || (outWaybill.StatusEnum == OutWaybillStatus.Confirmed))
                    {
                        waybills.Add(outWaybill);
                    }
                }

                Waybill_bindingSource.DataSource = waybills;

                Ok_simpleButton.Enabled = (waybills.Count > 0);

                Program.Logger.Info(this, "... инициализация с параметрами успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации произошла ошибка.", exception);
            }
        }
        #endregion Инициализация главной формы.

        #region Обработка событий пользовательского интерфейса.
        private void PositionRequestForm_Load(object sender, EventArgs e)
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
        private void PositionRequestForm_FormClosed(object sender, FormClosedEventArgs e)
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
                if (Waybill_bindingSource.Current == null) return;

                Position selected = (Position)Position_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                position = selected;

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);

                MainForm.ShowErrorMessage(string.Format("Во время выбора позиции товарно-транспортной накладной произошла ошибка: '{0}'.", exception.Message));
            }
        }
        private void Waybill_gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                if (Waybill_bindingSource.Current == null) return;

                AWaybill selectedWaybill = (AWaybill)Waybill_bindingSource.Current;

                if (selectedWaybill == null) throw new Exception("Ошибка получения текущего документа.");

                TtnId_textEdit.Text = string.Format("{0} (от '{1}')", selectedWaybill.Number, selectedWaybill.ShipperName);
                List<Position> list = selectedWaybill.GetPositions();

                Ok_simpleButton.Enabled = (list.Count > 0);

                Position_bindingSource.DataSource = list;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
                Cursor = Cursors.Default;

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка: '{0}'.", exception.Message));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void Position_gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                if (Position_bindingSource.Current == null) return;

                Position selectedPosition = (Position)Position_bindingSource.Current;

                if (selectedPosition == null) throw new Exception("Ошибка получения текущего документа.");

                PositionId_textEdit.Text = string.Format("{0} ('{1}')", selectedPosition.Identity, selectedPosition.Title);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка: '{0}'.", exception.Message));
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
            get { return string.Format("{0}\\{1}\\", Constants.RegistryPathVisualSettings, GetType().Name); }
        }
        /// <summary>
        /// Загрузка оформления редактора.
        /// </summary>
        private void loadGridLayot()
        {
            try
            {
                Waybill_gridView.RestoreLayoutFromRegistry(registryPathVisualSettings + "Waybill");
                Position_gridView.RestoreLayoutFromRegistry(registryPathVisualSettings + "Position");
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
                Waybill_gridView.SaveLayoutToRegistry(registryPathVisualSettings + "Waybill");
                Position_gridView.SaveLayoutToRegistry(registryPathVisualSettings + "Position");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции сохранения раскладки грида произошла ошибка.", exception);
            }
        }
        #endregion Раскладка грида.
    }
}