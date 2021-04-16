using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Axiom.AlcoTransport.Engine.BarcodeCheck;
using DevExpress.XtraEditors;
using Axiom.AlcoTransport.Watchtower.Configuration;

namespace Axiom.AlcoTransport
{
    public partial class PositionViewForm : XtraForm
    {
        #region Внутренние объекты класса.

        /// <summary>
        /// Накладная.
        /// Только чтение.
        /// </summary>
        private readonly AWaybill waybill;

        /// <summary>
        /// Проверка штрихкодов в накладной
        /// </summary>
        private readonly BarCodeChecker barCodeChecker;

        private readonly Documents documents;

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
        public PositionViewForm()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                waybill = null;
                documents = null;

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
        /// <param name="documents"></param>
        /// <param name="waybill">Накладная.</param>
        public PositionViewForm(Documents documents, AWaybill waybill) : this()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации с параметрами... ");

                this.waybill = waybill;
                this.documents = documents;
                var positions = waybill.GetPositions();
                Document_BindingSource.DataSource = positions;
                Document_gridView.ExpandAllGroups();

                barCodeChecker = new BarCodeChecker(positions);
                Barcodes_GridControl.DataSource = barCodeChecker.BarCodePositions;
                ToggleCheckModeButton.Enabled = waybill is InWaybill;

                Program.Logger.Info(this, "... инициализация с параметрами успешно завершена.");
            }
            catch (Exception exception)
            {
                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка.\r\n{0}",
                    exception.Message));

                Program.Logger.Error("Во время инициализации произошла ошибка.", exception);
            }
        }

        #endregion Инициализация главной формы.

        #region Обработка событий пользовательского интерфейса.

        private void PositionViewForm_FormClosed(object sender, FormClosedEventArgs e)
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

        private void PositionViewForm_Load(object sender, EventArgs e)
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

        private void Close_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.",
                    exception);
            }
        }

        private void Print_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Document_gridControl.IsPrintingAvailable) Document_gridControl.ShowRibbonPrintPreview();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'",
                    exception));
            }
        }

        private void Find_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Document_gridView.OptionsFind.AlwaysVisible = !Document_gridView.OptionsFind.AlwaysVisible;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }

        private void Group_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Document_gridView.OptionsView.ShowGroupPanel = !Document_gridView.OptionsView.ShowGroupPanel;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }

        private void Filter_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Document_gridView.Columns.Count > 1)
                    Document_gridView.ShowFilterEditor(Document_gridView.Columns[1]);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }

        private void Columns_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Document_gridView.ShowCustomization();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }

        private void Refresh_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Document_gridView.RefreshData();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
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
                Program.Logger.Error("Во время выполнения операции загрузки раскладки грида произошла ошибка.",
                    exception);
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
                Program.Logger.Error("Во время выполнения операции сохранения раскладки грида произошла ошибка.",
                    exception);
            }
        }

        #endregion Раскладка грида.

        #region Контроль пришедших штрихкодов

        /// <summary>
        /// Переход в режим контроля штрихкодов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToggleCheckModeButton_CheckedChanged(object sender, EventArgs e)
        {
            checkerDifferencesLabel.Visible = barcode_TextEdit.Visible = ToggleCheckModeButton.Checked;
            if (ToggleCheckModeButton.Checked)
                barCodeChecker.StartCheck();
            else barCodeChecker.StopCheck();
            RefreshCheckerState();
        }

        /// <summary>
        /// Обновление состояния контролов проверки штрихкодов
        /// </summary>
        private void RefreshCheckerState()
        {
            checkerDifferencesLabel.Text = $@"Расхождений : {barCodeChecker.Differences}";
            createDiffAct_Button.Enabled = barCodeChecker.Differences > 0 && ((InWaybill)waybill).StatusEnum == InWaybillStatus.New;
            MainForm.InLogErrorContext(() => Barcodes_GridView.RefreshData());
        }

       
        /// <summary>
        /// Нажатие кнопки на форме- активируем контрол ввода штрихкода и запускаем поиск штрихкода 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PositionViewForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                barcode_TextEdit.Focus();
                barcode_TextEdit.SelectAll();
            }
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    Program.Logger.Info(this, $"Ввод штрихода.{barcode_TextEdit.Text}");
                    barCodeChecker.AddInputBarcode(barcode_TextEdit.Text);
                    RefreshCheckerState();
                }
                catch (Exception exception)
                {
                    var errorDescription = "Во время ввода штрихкода произошла ошибка.\r\n";
                    MainForm.ShowErrorMessage($"{errorDescription}{exception.Message}");
                    Program.Logger.Error(errorDescription, exception);
                }
                
            }
        }
        /// <summary>
        /// Удаление введенного штрихкода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void inputBarcodeItemButtonEdit_ButtonClick(object sender,
            DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            barCodeChecker.RemoveBarcode(((ButtonEdit) sender).Text);
            RefreshCheckerState();
        }
        /// <summary>
        /// Отрисовка совпадающией позиции зеленым в гриде
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Barcodes_GridView_CustomDrawCell(object sender,
            DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            var currentPosition = (BarcodePosition) Barcodes_GridView.GetRow(e.RowHandle);
            if (currentPosition.Barcode == currentPosition.BarcodeInput)
                e.Appearance.BackColor = Color.LightGreen;
        }
        /// <summary>
        /// Создание акта расхождений на основе отсканированной информации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createDiffAct_Button_Click(object sender, EventArgs e)
        {
            new ActDifferenceForm(documents, (InWaybill)waybill, barCodeChecker.BarCodePositions).ShowDialog();
        }

        #endregion

       
    }
}