using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Axiom.AlcoTransport.Watchtower.Configuration;

namespace Axiom.AlcoTransport
{
    public partial class OutWaybillEditorForm : XtraForm
    {
        #region Внутренние объекты класса.
        /// <summary>
        /// Список документов.
        /// Только чтение.
        /// </summary>
        private readonly Documents documents;
        /// <summary>
        /// Редактируемая накладная.
        /// Только чтение.
        /// </summary>
        private readonly OutWaybill outWaybill;
        /// <summary>
        /// Грид с накладными.
        /// Только чтение.
        /// </summary>
        private readonly GridView gridView;
        /// <summary>
        /// Признак изменения данных.
        /// </summary>
        private bool dataChanged;
        /// <summary>
        /// Идентификатор отправителя.
        /// </summary>
        private string shipperClientRegId;
        /// <summary>
        /// Короткое имя отправителя.
        /// </summary>
        private string shipperShortName;
        /// <summary>
        /// Идентификатор получателя.
        /// </summary>
        private string consigneeClientRegId;
        /// <summary>
        /// Короткое имя получателя.
        /// </summary>
        private string consigneeShortName;
        /// <summary>
        /// Список позиций.
        /// Только чтение.
        /// </summary>
        private readonly ThreadedBindingList<OutPosition> positions;
        #endregion Внутренние объекты класса.

        #region Инициализация главной формы.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public OutWaybillEditorForm()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                documents = null;
                outWaybill = null;
                positions = new ThreadedBindingList<OutPosition>();

                dataChanged = false;

                shipperClientRegId = string.Empty;
                shipperShortName = string.Empty;
                consigneeClientRegId = string.Empty;
                consigneeShortName = string.Empty;

                Type_comboBoxEdit.Properties.Items.Clear();
                Type_comboBoxEdit.Properties.Items.Add(new Pair("WBReturnToMe", "Возврат мне"));
                Type_comboBoxEdit.Properties.Items.Add(new Pair("WBReturnFromMe", "Возврат от меня"));
                Type_comboBoxEdit.Properties.Items.Add(new Pair("WBInvoiceToMe", "Приход"));
                Type_comboBoxEdit.Properties.Items.Add(new Pair("WBInvoiceFromMe", "Расход"));

                UnitType_comboBoxEdit.Properties.Items.Clear();
                UnitType_comboBoxEdit.Properties.Items.Add(new Pair("Packed", "Упакованная"));
                UnitType_comboBoxEdit.Properties.Items.Add(new Pair("Unpacked", "Неупакованная"));

                TranType_comboBoxEdit.Properties.Items.Clear();
                TranType_comboBoxEdit.Properties.Items.Add(new Pair("410", "Воздушный транспорт"));
                TranType_comboBoxEdit.Properties.Items.Add(new Pair("411", "Водный транспорт"));
                TranType_comboBoxEdit.Properties.Items.Add(new Pair("412", "Железнодорожный транспорт"));
                TranType_comboBoxEdit.Properties.Items.Add(new Pair("413", "Автомобильный транспорт"));
                TranType_comboBoxEdit.Properties.Items.Add(new Pair("419", "Иные транспортные средства"));

                Date_dateEdit.DateTime = DateTime.Now;
                ShippingDate_dateEdit.DateTime = DateTime.Now;

                SourceType_radioGroup.SelectedIndex = 1;

                Production_gridControl.Visible = SourceType_radioGroup.SelectedIndex == 0;
                Rests_gridControl.Visible = SourceType_radioGroup.SelectedIndex == 1;

                Main_xtraTabControl.SelectedTabPageIndex = 0;

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
        /// <param name="documents">Список документов.</param>
        /// <param name="outWaybill">Накладная для редактирования или просмотра.</param>
        /// <param name="gridView">Грид с накладными.</param>
        public OutWaybillEditorForm(Documents documents, OutWaybill outWaybill, GridView gridView) : this()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации с параметрами... ");

                this.documents = documents;
                this.outWaybill = outWaybill;
                this.gridView = gridView;

                Program.Logger.Info(this, "... инициализация с параметрами успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации произошла ошибка.", exception);
            }
        }
        #endregion Инициализация главной формы.

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
                Consignee_gridView.RestoreLayoutFromRegistry(registryPathVisualSettings + "Consignee");
                Production_gridView.RestoreLayoutFromRegistry(registryPathVisualSettings + "Production");
                Position_gridView.RestoreLayoutFromRegistry(registryPathVisualSettings + "Position");
                Rests_gridView.RestoreLayoutFromRegistry(registryPathVisualSettings + "Rests");
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
                Consignee_gridView.SaveLayoutToRegistry(registryPathVisualSettings + "Consignee");
                Production_gridView.SaveLayoutToRegistry(registryPathVisualSettings + "Production");
                Position_gridView.SaveLayoutToRegistry(registryPathVisualSettings + "Position");
                Rests_gridView.SaveLayoutToRegistry(registryPathVisualSettings + "Rests");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции сохранения раскладки грида произошла ошибка.", exception);
            }
        }
        #endregion Раскладка грида.

        #region Обработка событий пользовательского интерфейса.
        private void OutWaybillEditorForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузки формы... ");

                Cursor = Cursors.WaitCursor;

                if (documents == null) throw new Exception("Список документов не может быть пустым.");
                if (documents.Configuration == null) throw new Exception("Конфигурация приложения не может быть пустой.");

                Consignee_bindingSource.DataSource = documents.Partners;
                Production_bindingSource.DataSource = documents.Production;
                RestsPosition_bindingSource.DataSource = documents.GetLastRestsPositions(true);

                if (outWaybill == null) throw new Exception("Накладная не может быть пустым объектом.");

                loadGridLayot();

                fillShipperInfo();
                fillFormByOutWaybillInfo();

                Position_bindingSource.DataSource = positions;

                dataChanged = false;

                Program.Logger.Info(this, "... загрузка формы успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время загрузки произошла ошибка.", exception);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void OutWaybillEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (dataChanged)
                {
                    DialogResult result = XtraMessageBox.Show("Исходящая накладная была изменена. Сохранить сделанные изменения?",
                                                              "Сохранение данных", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    { 
                        save();

                        return;
                    }

                    if (result == DialogResult.No)
                    {
                        return;
                    }

                    e.Cancel = true;
                }
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время сохранения накладной произошла ошибка.", exception);
                MainForm.ShowErrorMessage(exception);
            }
        }
        private void OutWaybillEditorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Форма закрывается.");

                if (gridView != null) gridView.RefreshData();

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
        private void Save_simpleButton_Click(object sender, EventArgs e)
        {
            save();
        }
        private void SaveAndClose_simpleButton_Click(object sender, EventArgs e)
        {
            save();
            close();
        }
        private void Close_simpleButton_Click(object sender, EventArgs e)
        {
            close();
        }
        private void Main_xtraTabControl_SelectedPageChanged(object sender, TabPageChangedEventArgs e)
        {
            #region Fucking devexpress controls...
            if (e.Page == Consignee_xtraTabPage)
            {
                if (!string.IsNullOrWhiteSpace(consigneeClientRegId))
                {
                    int row = Consignee_gridView.LocateByValue("ClientRegId", consigneeClientRegId);
                    if (row != GridControl.InvalidRowHandle) Consignee_gridView.FocusedRowHandle = row;
                }
            }
            #endregion Fucking devexpress controls...
        }
        private void Identity_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            dataChange();
        }
        private void Number_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            dataChange();
        }
        private void Date_dateEdit_EditValueChanged(object sender, EventArgs e)
        {
            dataChange();
        }
        private void Type_comboBoxEdit_SelectedValueChanged(object sender, EventArgs e)
        {
            dataChange();
        }
        private void UnitType_comboBoxEdit_SelectedValueChanged(object sender, EventArgs e)
        {
            dataChange();
        }
        private void Base_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            dataChange();
        }
        private void Note_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            dataChange();
        }
        private void SelectCons_simpleButton_Click(object sender, EventArgs e)
        {
            selectConsignee();
        }
        private void Consignee_gridControl_DoubleClick(object sender, EventArgs e)
        {
            selectConsignee();
        }
        private void Production_gridControl_DoubleClick(object sender, EventArgs e)
        {
            if ((Production_gridControl.Enabled) && (Rests_gridControl.Enabled))
            {
                addPosition();
            }
        }
        private void Rests_gridControl_DoubleClick(object sender, EventArgs e)
        {
            if ((Production_gridControl.Enabled) && (Rests_gridControl.Enabled))
            {
                addPosition();
            }
        }
        private void Add_simpleButton_Click(object sender, EventArgs e)
        {
            if ((Production_gridControl.Enabled) && (Rests_gridControl.Enabled))
            {
                addPosition();
            }
        }
        private void CheckPosition_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Production_bindingSource.Current == null) return;

                if (positions.Count == 0)
                {
                    XtraMessageBox.Show("Список позиций пустой. Проверка не проводится.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                foreach (OutPosition outPosition in positions)
                {
                    outPosition.Check();
                }

                XtraMessageBox.Show("Список позиций успешно проверен. Ошибок не найдено.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения проверки списка позиций произошла ошибка.", exception);

                XtraMessageBox.Show(string.Format("Во время проверки списка обнаружена ошибка.\r\n{0}", exception.Message),
                                    "Обнаружена ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
        private void RefreshPosition_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Position_gridView.RefreshData();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void Position_gridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                dataChange();
                Position_gridView.RefreshData();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void Remove_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Position_bindingSource.Current == null) return;

                dataChange();

                OutPosition selected = (OutPosition)Position_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                positions.Remove(selected);

                int i = 1;

                foreach (OutPosition outPosition in positions)
                {
                    outPosition.Identity = i;

                    ++i;
                }

                Position_gridView.RefreshData();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception));
            }
        }
        private void TranType_comboBoxEdit_SelectedValueChanged(object sender, EventArgs e)
        {
            dataChange();
        }
        private void TranCompany_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            dataChange();
        }
        private void TranCar_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            dataChange();
        }
        private void TranTrailer_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            dataChange();
        }
        private void TranCustomer_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            dataChange();
        }
        private void TranDriver_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            dataChange();
        }
        private void TranLoad_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            dataChange();
        }
        private void TranUnload_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            dataChange();
        }
        private void TranRedirect_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            dataChange();
        }
        private void TranForw_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            dataChange();
        }
        private void Print_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Detail_treeList.IsPrintingAvailable) Detail_treeList.ShowRibbonPrintPreview();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception));
            }
        }
        private void PrintPosition_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Position_gridControl.IsPrintingAvailable) Position_gridControl.ShowRibbonPrintPreview();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception));
            }
        }
        private void CollapseTree_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Detail_treeList.CollapseAll();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void ExpandTree_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                MainForm.ExpandDetailView(Detail_treeList);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void ConsFind_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Consignee_gridView.OptionsFind.AlwaysVisible = !Consignee_gridView.OptionsFind.AlwaysVisible;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void ConsGroup_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Consignee_gridView.OptionsView.ShowGroupPanel = !Consignee_gridView.OptionsView.ShowGroupPanel;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void ConsFilter_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Consignee_gridView.Columns.Count > 1) Consignee_gridView.ShowFilterEditor(Consignee_gridView.Columns[1]);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void ConsColumns_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Consignee_gridView.ShowCustomization();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void ConsRefresh_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Consignee_gridView.RefreshData();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void APFind_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (SourceType_radioGroup.SelectedIndex == 0) Production_gridView.OptionsFind.AlwaysVisible = !Production_gridView.OptionsFind.AlwaysVisible;
                else
                if (SourceType_radioGroup.SelectedIndex == 1) Rests_gridView.OptionsFind.AlwaysVisible = !Rests_gridView.OptionsFind.AlwaysVisible;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void APGroup_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (SourceType_radioGroup.SelectedIndex == 0) Production_gridView.OptionsView.ShowGroupPanel = !Production_gridView.OptionsView.ShowGroupPanel;
                else
                if (SourceType_radioGroup.SelectedIndex == 1) Rests_gridView.OptionsView.ShowGroupPanel = !Rests_gridView.OptionsView.ShowGroupPanel;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void APFilter_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (SourceType_radioGroup.SelectedIndex == 0)
                {
                    if (Production_gridView.Columns.Count > 1) Production_gridView.ShowFilterEditor(Production_gridView.Columns[1]);
                }
                else if (SourceType_radioGroup.SelectedIndex == 1) 
                {
                    if (Rests_gridView.Columns.Count > 2) Rests_gridView.ShowFilterEditor(Rests_gridView.Columns[2]);
                }
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void APColumns_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (SourceType_radioGroup.SelectedIndex == 0) Production_gridView.ShowCustomization();
                else
                if (SourceType_radioGroup.SelectedIndex == 1) Rests_gridView.ShowCustomization();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void APRefresh_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (SourceType_radioGroup.SelectedIndex == 0) Production_gridView.RefreshData();
                else
                if (SourceType_radioGroup.SelectedIndex == 1) Rests_gridView.RefreshData();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void ColumnsPosition_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Position_gridView.ShowCustomization();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void FindPosition_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Position_gridView.OptionsFind.AlwaysVisible = !Position_gridView.OptionsFind.AlwaysVisible;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void FindAB_simpleButton_Click(object sender, EventArgs e)
        {
            findAB();
        }
        private void SourceType_radioGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Production_gridControl.Visible = SourceType_radioGroup.SelectedIndex == 0;
                Rests_gridControl.Visible = SourceType_radioGroup.SelectedIndex == 1;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время изменения источника данных произошла ошибка.", exception);
                MainForm.ShowErrorMessage(string.Format("Во время изменения источника данных произошла ошибка.\r\n{0}", exception.Message));
            }
        }
        private void scanBarcode_simpleButton_Click(object sender, EventArgs e)
        {
            if (Position_bindingSource.Current == null) return;
            MainForm.InLogErrorContext(() =>
            {
                var selected = GetSelectedPosition();

                var boxInfos = selected.BoxInfos;
                if (boxInfos.Count == 0)
                {
                    boxInfos.Add(new BoxInfo());
                }


                PDF417RequestForm pdf417Form = new PDF417RequestForm(boxInfos[0].AmcList.Select(amc => amc.Barcode).ToList(),
                    "Отсканируйте штрих-код PDF-417");

                if (pdf417Form.ShowDialog(this) == DialogResult.OK)
                {
                    boxInfos[0].AmcList.Add(new Amc {Barcode = pdf417Form.PDF417 });
                }
                else
                {
                    Position_gridView.RefreshData();
                }
            });
        }

        private OutPosition GetSelectedPosition()
        {
            OutPosition selected = (OutPosition)Position_bindingSource.Current;
            if (selected == null) throw new Exception("Ошибка получения текущей позиции.");
            return selected;
        }

        #endregion Обработка событий пользовательского интерфейса.

        #region Внутренние методы класса.
        /// <summary>
        /// Сохранить накладную.
        /// </summary>
        private void save()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                Program.Logger.Info(this, string.Format("Попытка сохранить исходящую накладную '{0}'...", outWaybill.Description));

                fillOutWaybillByFormInfo();
                outWaybill.Check();

                documents.AddAndSaveOutWaybill(outWaybill);

                Status_textEdit.Text = outWaybill.StatusNote;

                if (gridView != null) gridView.RefreshData();

                dataChanged = false;

                Program.Logger.Info(this, "... сохранение исходящей накладной успешно завершено.");
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;

                Program.Logger.Error("Во время сохранения накладной произошла ошибка.", exception);
                MainForm.ShowErrorMessage(string.Format("Во время сохранения накладной произошла ошибка:\r\n'{0}'", exception));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// Закрыть форму.
        /// </summary>
        private void close()
        {
            try
            {
                Close();

                if (gridView != null) gridView.RefreshData();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        /// <summary>
        /// Обработка изменения данных.
        /// </summary>
        private void dataChange()
        {
            dataChanged = true;
        }
        /// <summary>
        /// Выбрать получателя.
        /// </summary>
        private void selectConsignee()
        {
            try
            {
                if (Consignee_bindingSource.Current == null) return;

                Partner selected = (Partner)Consignee_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                consigneeClientRegId = selected.ClientRegId;
                consigneeShortName = selected.ShortName;

                ConsigneeId_textEdit.Text = string.Format("{0} ({1})", selected.ShortName, selected.ClientRegId);

                dataChange();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception));
            }
        }
        /// <summary>
        /// Заполнить информацию о поставщике.
        /// </summary>
        private void fillShipperInfo()
        {
            try
            {
                Partner shipper = documents.Partners.FirstOrDefault(partner => partner.ClientRegId == documents.Configuration.FsrarId);

                if (shipper == null) throw new Exception("В справочнике организаций отсутствует информация по организации-отправителю.");

                shipperClientRegId = ShipperClientRegId_textEdit.Text = shipper.ClientRegId;
                shipperShortName = ShipShortName_textEdit.Text = shipper.ShortName;
                ShipInn_textEdit.Text = shipper.Inn;
                ShipKpp_textEdit.Text = shipper.Kpp;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время заполнения информации об отправителе произошла ошибка.", exception);
            }
        }
        /// <summary>
        /// Заполнить форму информацией о накладной.
        /// </summary>
        private void fillFormByOutWaybillInfo()
        {
            try
            {
                Status_textEdit.Text = outWaybill.StatusNote;
                WBRegId_textEdit.Text = string.IsNullOrEmpty(outWaybill.WBRegId) ? "не присвоен" : outWaybill.WBRegId;
                Identity_textEdit.Text = outWaybill.Identity;
                Number_textEdit.Text = outWaybill.Number;

                Date_dateEdit.EditValue = outWaybill.OutDate;
                ShippingDate_dateEdit.EditValue = outWaybill.OutShippingDate;

                setSelectedCombo(outWaybill.TypeWaybill, Type_comboBoxEdit);
                setSelectedCombo(outWaybill.UnitType, UnitType_comboBoxEdit);

                Base_textEdit.Text = outWaybill.BaseWaybill;
                Note_textEdit.Text = outWaybill.NoteWaybill;

                consigneeClientRegId = outWaybill.ConsigneeClientRegId;
                consigneeShortName = outWaybill.ConsigneeName;
            
                foreach (Partner partner in documents.Partners)
                {
                    if ((!string.IsNullOrWhiteSpace(consigneeClientRegId))
                        && (partner.ClientRegId == consigneeClientRegId))
                    {
                        ConsigneeId_textEdit.Text = string.Format("{0} ({1})", partner.ShortName, partner.ClientRegId);
                    }
                }

                positions.Clear();
                foreach (OutPosition outPosition in outWaybill.OutPositions.OrderBy(x => x.Identity))
                {
                    positions.Add(outPosition.Clone());
                }

                setSelectedCombo(outWaybill.TranType, TranType_comboBoxEdit);
                TranCompany_textEdit.Text = outWaybill.Transport["TRAN_COMPANY"];
                TranCar_textEdit.Text = outWaybill.Transport["TRAN_CAR"];
                TranTrailer_textEdit.Text = outWaybill.Transport["TRAN_TRAILER"];
                TranCustomer_textEdit.Text = outWaybill.Transport["TRAN_CUSTOMER"];
                TranDriver_textEdit.Text = outWaybill.Transport["TRAN_DRIVER"];
                TranLoad_textEdit.Text = outWaybill.Transport["TRAN_LOADPOINT"];
                TranUnload_textEdit.Text = outWaybill.Transport["TRAN_UNLOADPOINT"];
                TranRedirect_textEdit.Text = outWaybill.Transport["TRAN_REDIRECT"];
                TranForw_textEdit.Text = outWaybill.Transport["TRAN_FORWARDER"];

                Detail_treeList.DataSource = outWaybill.TreeData;
                MainForm.ExpandDetailView(Detail_treeList);

                setControlsEnable();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время заполнения информации о накладной произошла ошибка.", exception);
            }
        }
        /// <summary>
        /// Заполнить накладную информацией с формы.
        /// </summary>
        private void fillOutWaybillByFormInfo()
        {
            outWaybill.Number = Number_textEdit.Text;
            outWaybill.OutDate = (DateTime)Date_dateEdit.EditValue;
            outWaybill.OutShippingDate = (DateTime)ShippingDate_dateEdit.EditValue;

            outWaybill.TypeWaybill = ((Pair)Type_comboBoxEdit.SelectedItem).Key;
            outWaybill.UnitType = ((Pair)UnitType_comboBoxEdit.SelectedItem).Key;

            outWaybill.BaseWaybill = Base_textEdit.Text;
            outWaybill.NoteWaybill = Note_textEdit.Text;

            outWaybill.ShipperClientRegId = shipperClientRegId;
            outWaybill.ShipperName = shipperShortName;
            outWaybill.ConsigneeClientRegId = consigneeClientRegId;
            outWaybill.ConsigneeName = consigneeShortName;

            outWaybill.TranType = ((Pair)TranType_comboBoxEdit.SelectedItem).Key;

            outWaybill.Transport["TRAN_COMPANY"] = TranCompany_textEdit.Text;
            //outWaybill.Transport["TRAN_CAR"] = TranCar_textEdit.Text;
            outWaybill.Transport["TRAN_TRAILER"] = TranTrailer_textEdit.Text;
            outWaybill.Transport["TRAN_CUSTOMER"] = TranCustomer_textEdit.Text;
            outWaybill.Transport["TRAN_DRIVER"] = TranDriver_textEdit.Text;
            outWaybill.Transport["TRAN_LOADPOINT"] = TranLoad_textEdit.Text;
            outWaybill.Transport["TRAN_UNLOADPOINT"] = TranUnload_textEdit.Text;
            outWaybill.Transport["TRAN_REDIRECT"] = TranRedirect_textEdit.Text;
            outWaybill.Transport["TRAN_FORWARDER"] = TranForw_textEdit.Text;

            outWaybill.OutPositions.Clear();
            foreach (OutPosition outPosition in positions.OrderBy(x => x.Identity))
            {
                outWaybill.OutPositions.Add(outPosition.Clone());
            }

            outWaybill.Check();
        }
        /// <summary>
        /// Установить выделенное значение на комбобоксе.
        /// </summary>
        /// <param name="key">Ключ.</param>
        /// <param name="combo">Комбобокс.</param>
        private void setSelectedCombo(string key, ComboBoxEdit combo)
        {
            foreach (object item in combo.Properties.Items)
            {
                Pair pair = item as Pair;
                if (pair == null) continue;
                if (pair.Key != key) continue;

                combo.SelectedItem = item;
                break;
            }
        }
        /// <summary>
        /// Управление элементами на форме в зависимости от статуса накладной.
        /// </summary>
        private void setControlsEnable()
        {
            if ((outWaybill.StatusEnum == OutWaybillStatus.Partial) || (outWaybill.StatusEnum == OutWaybillStatus.Ready))
            {
                Position_gridView.OptionsBehavior.Editable = true;
                Position_gridView.OptionsBehavior.ReadOnly = false;
            }
            else
            {
                foreach (XtraTabPage page in Main_xtraTabControl.TabPages)
                {
                    foreach (Control control in page.Controls)
                    {
                        BaseEdit edit = control as BaseEdit;

                        if (edit != null)
                        {
                            edit.Properties.ReadOnly = true;
                        }
                        else
                        {
                            if (!(control is LabelControl)) control.Enabled = false;
                        }

                        if (control is CheckEdit) control.Enabled = false;
                    }
                }

                Save_simpleButton.Enabled = false;
                Save_simpleButton.Visible = false;
                SaveAndClose_simpleButton.Enabled = false;
                SaveAndClose_simpleButton.Visible = false;

                Content_splitContainerControl.Enabled = true;
                foreach (Control control in Content_splitContainerControl.Controls) control.Enabled = false;
                Content_splitContainerControl.SplitterPosition = 440;
                Content_splitContainerControl.Panel2.Enabled = true;
                foreach (Control control in Content_splitContainerControl.Panel2.Controls) control.Enabled = false;
                Position_gridControl.Enabled = true;
                Position_gridView.OptionsBehavior.Editable = false;
                Position_gridView.OptionsBehavior.ReadOnly = true;
                FindPosition_simpleButton.Enabled = true;
                ColumnsPosition_simpleButton.Enabled = true;
                PrintPosition_simpleButton.Enabled = true;
                RefreshPosition_simpleButton.Enabled = true;
                Detail_treeList.Enabled = true;
                Print_simpleButton.Enabled = true;
                ExpandTree_simpleButton.Enabled = true;
                CollapseTree_simpleButton.Enabled = true;
                Close_simpleButton.Enabled = true;

                Text = "Просмотр товарно-транспортной накладной";
            }
        }
        /// <summary>
        /// Добавить позицию.
        /// </summary>
        private void addPosition()
        {
            try
            {
                if (SourceType_radioGroup.SelectedIndex == 0)
                {
                    if (Production_bindingSource.Current == null) return;

                    Production selected = (Production) Production_bindingSource.Current;

                    if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                    dataChange();

                    #region Действия, если такая продукция уже присутствует в накладной.

                    if (positions.Any(outPosition => outPosition.AlcoCode == selected.AlcCode))
                    {
                        if (DialogResult.No == XtraMessageBox.Show("В позициях накладной уже присутствует товар с указанным кодом алкогольной продукции.\r\n" +
                                                                   "В данном случае вам необходимо или изменить количество товара в существующей позиции, " +
                                                                   "или указать новый идентификатор справки \"Б\" в новой позиции накладной.\r\n\r\n" +
                                                                   "Добавить новую позицию в накладную?",
                                                                   "Добавление новой позиции", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        return;
                    }

                    #endregion Действия, если такая продукция уже присутствует в накладной.

                    positions.Add(new OutPosition(selected));
                }
                else if (SourceType_radioGroup.SelectedIndex == 1)
                {
                    if (RestsPosition_bindingSource.Current == null) return;

                    RestsPosition selected = (RestsPosition)RestsPosition_bindingSource.Current;

                    if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                    dataChange();

                    #region Действия, если такая продукция уже присутствует в накладной.

                    if (positions.Any(outPosition => ((outPosition.AlcoCode == selected.AlcoCode) && (outPosition.FormBRegId == selected.FormBRegId))))
                    {
                        XtraMessageBox.Show("В позициях накладной уже присутствует товар с указанным кодом алкогольной продукции. " +
                                            "В данном случае вам необходимо изменить количество товара.",
                                            "Выполнение операции невозможно", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                        return;
                    }

                    #endregion Действия, если такая продукция уже присутствует в накладной.

                    positions.Add(new OutPosition(selected));
                }

                int i = 1;

                foreach (OutPosition outPosition in positions)
                {
                    outPosition.Identity = i;

                    ++i;
                }

                Position_gridView.RefreshData();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception));
            }
        }
        /// <summary>
        /// Найти идентификаторы справок 'А' и 'Б'.
        /// </summary>
        private void findAB()
        {
            try
            {
                if (Position_bindingSource.Current == null) return;

                OutPosition selected = (OutPosition)Position_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");
                if (string.IsNullOrWhiteSpace(selected.AlcoCode)) throw new Exception("У выбранной позиции отсутствует обязательной поле 'Код алкогольной продукции'.");
                
                FindABForm form = new FindABForm(documents, selected);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    dataChange();

                    selected.FormARegId = form.FormARegId;
                    selected.FormBRegId = form.FormBRegId;
                }

                Position_gridView.RefreshData();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка.\r\n{0}", exception.Message));
            }
        }
        #endregion Внутренние методы класса.

       
    }
}
