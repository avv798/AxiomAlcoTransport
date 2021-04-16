using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Ribbon;
using Axiom.AlcoTransport.Watchtower.Configuration;

namespace Axiom.AlcoTransport
{
	public partial class InWaybillForm : XtraForm
	{
		#region Внутренние объекты класса.
        /// <summary>
        /// Список партнёров.
        /// Только чтение.
        /// </summary>
	    private readonly Documents documents;
        /// <summary>
        /// Путь в реестре для сохранения раскладки грида.
        /// Только чтение.
        /// </summary>
        private string registryPathVisualSettings
        {
            get { return string.Format("{0}\\{1}", Constants.RegistryPathVisualSettings, GetType().Name); }
        }
		#endregion Внутренние объекты класса.

		#region Внешние объекты класса.
        /// <summary>
        /// Основная лента приложения.
        /// Полный доступ.
        /// </summary>
        public RibbonControl MainRibbonControl { get; set; }
        #endregion Внешние объекты класса.

		#region Конструкторы класса.
		/// <summary>
		/// Конструктор класса "по умолчанию".
		/// </summary>
		public InWaybillForm()
		{
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                #region Включение или отключение экспорта в "AxiTrade"...
                if (Program.GetBooleanParameter("enableExportToAxiTrade", false))
                {
                    ExportToAxiTrade_barButtonItem.Enabled = true;
                    ExportToAxiTrade_barButtonItem.Visibility = BarItemVisibility.Always;
                }
                else
                {
                    ExportToAxiTrade_barButtonItem.Enabled = false;
                    ExportToAxiTrade_barButtonItem.Visibility = BarItemVisibility.Never;
                }
                #endregion Включение или отключение экспорта в "AxiTrade"...

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
        public InWaybillForm(Documents documents): this()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации с параметрами... ");

                this.documents = documents;

                Program.Logger.Info(this, "... инициализация с параметрами успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации произошла ошибка.", exception);
            }
        }
		#endregion Конструкторы класса.

		#region Обработка событий пользовательского интерфейса.
        private void InWaybillForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузки... ");

                Cursor = MdiParent.Cursor = Cursors.WaitCursor;

                loadGridLayot();

                Document_bindingSource.DataSource = documents.InWaybills;

                Program.Logger.Info(this, "... загрузка успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время загрузки произошла ошибка.", exception);
            }
            finally
            {
                Cursor = MdiParent.Cursor = Cursors.Default;
            }
        }
        private void InWaybillForm_Activated(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка активации... ");

                Cursor = MdiParent.Cursor = Cursors.WaitCursor;
                MainRibbonControl.SelectedPage = InWaybill_ribbonPage;

                Program.Logger.Info(this, "... активация успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время активации произошла ошибка.", exception);

                Cursor = MdiParent.Cursor = Cursors.Default;

                MainForm.ShowErrorMessage(exception);
            }
            finally
            {
                Cursor = MdiParent.Cursor = Cursors.Default;
            }
        }
        private void InWaybillForm_FormClosed(object sender, FormClosedEventArgs e)
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
                Cursor = MdiParent.Cursor = Cursors.Default;
            }
        }
        private void SearchPanel_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Document_gridView.OptionsFind.AlwaysVisible = !Document_gridView.OptionsFind.AlwaysVisible;
                SearchPanel_barButtonItem.Down = Document_gridView.OptionsFind.AlwaysVisible;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void FilterCtor_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (Document_gridView.Columns.Count > 1) Document_gridView.ShowFilterEditor(Document_gridView.Columns[1]);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void Columns_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
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
        private void Footer_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Document_gridView.OptionsView.ShowFooter = !Document_gridView.OptionsView.ShowFooter;
                Footer_barButtonItem.Down = Document_gridView.OptionsView.ShowFooter;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void GroupPanel_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Document_gridView.OptionsView.ShowGroupPanel = !Document_gridView.OptionsView.ShowGroupPanel;
                GroupPanel_barButtonItem.Down = Document_gridView.OptionsView.ShowGroupPanel;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void ExpandGroup_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Document_gridView.ExpandAllGroups();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void CollapseGroup_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Document_gridView.CollapseAllGroups();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void SetRead_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            changeDocumentReadingStatus(true);
        }
        private void SetNew_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            changeDocumentReadingStatus(false);
        }
        private void Document_gridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e == null) return;
                if (e.Appearance == null) return;

                InWaybill current = (InWaybill)Document_gridView.GetRow(e.RowHandle);
                if (current == null) return;

                if (current.AlreadyReading) return;

                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время отрисовки произошла ошибка.", exception);
            }
        }
        private void Copy_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Document_gridView.CopyToClipboard();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void PreviewList_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (Document_gridControl.IsPrintingAvailable) Document_gridControl.ShowRibbonPrintPreview();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception));
            }
        }
        private void CopyDetail_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (Detail_treeList.Selection.Count == 1)
                {
                    Clipboard.SetText(Detail_treeList.FocusedNode.GetDisplayText(Detail_treeList.FocusedColumn));
                }
                else
                {
                    Detail_treeList.CopyToClipboard();
                }
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void PreviewDocument_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
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
        private void XlsExport_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            exportData(ExportFormat.xls, Export_saveFileDialog);
        }
        private void PdfExport_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            exportData(ExportFormat.pdf, Export_saveFileDialog);
        }
        private void HtmlExport_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            exportData(ExportFormat.html, Export_saveFileDialog);
        }
        private void RtfExport_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            exportData(ExportFormat.rtf, Export_saveFileDialog);
        }
        private void TxtExport_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            exportData(ExportFormat.txt, Export_saveFileDialog);
        }
        private void SearchInDoc_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Detail_treeList.ShowFindPanel();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void ExpandNodes_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
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
        private void CollapseNodes_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
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
        private void Accept_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            actHandler(ActType.Accept);
        }
        private void Reject_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            actHandler(ActType.Reject);
        }
        private void DifferenceAct_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            actHandler(ActType.Difference);
        }
        private void Resend_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (Document_bindingSource.Current == null) return;

                InWaybill selected = (InWaybill)Document_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                if ((selected.StatusEnum == InWaybillStatus.New)
                    || (selected.StatusEnum == InWaybillStatus.Partial))
                {
                    XtraMessageBox.Show("Переотправить акт можно только по тем накладных, " +
                                        "для которых уже был сформирован акт подтверждения или отказа.",
                                        "Выполнение операции невозможно", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                if (DialogResult.Yes == XtraMessageBox.Show("Переотправить акт для выбранной накладной?",
                                                            "Подтверждение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    SplashAccessor.Show();
                    Cursor = Cursors.WaitCursor;
                    MainRibbonControl.Enabled = false;

                    documents.ResendAct(selected);

                    Document_gridView.RefreshData();
                    Detail_treeList.DataSource = selected.TreeData;

                    XtraMessageBox.Show("Документ успешно переотправлен.",
                                        "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                MainRibbonControl.Enabled = true;
                SplashAccessor.Close();
                Cursor = Cursors.Default;

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка: '{0}'.", exception.Message));
            }
            finally
            {
                MainRibbonControl.Enabled = true;
                Cursor = Cursors.Default;

                SplashAccessor.Close();
            }
        }
        private void Document_gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                Cursor = MdiParent.Cursor = Cursors.WaitCursor;

                if (Document_bindingSource.Current == null) return;

                InWaybill selected = (InWaybill)Document_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                Detail_treeList.DataSource = selected.TreeData;
                MainForm.ExpandDetailView(Detail_treeList);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
                Cursor = MdiParent.Cursor = Cursors.Default;

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка: '{0}'.", exception.Message));
            }
            finally
            {
                Cursor = MdiParent.Cursor = Cursors.Default;
            }
        }
        private void Refresh_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = MdiParent.Cursor = Cursors.WaitCursor;

                if (Document_bindingSource.Current == null) return;

                InWaybill selected = (InWaybill)Document_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                Detail_treeList.DataSource = selected.TreeData;
                MainForm.ExpandDetailView(Detail_treeList);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции обновления детальных данных произошла ошибка.", exception);
                Cursor = MdiParent.Cursor = Cursors.Default;
            }
            finally
            {
                Cursor = MdiParent.Cursor = Cursors.Default;
            }
        }
        private void ShowPosition_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            showPosition();
        }
        private void Document_gridView_DoubleClick(object sender, EventArgs e)
        {
            showPosition();
        }
        private void ResendInWaybill_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                using (WaybillRegIdRequestForm form = new WaybillRegIdRequestForm())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        SplashAccessor.Show();
                        Cursor = Cursors.WaitCursor;
                        MainRibbonControl.Enabled = false;

                        documents.QueryResendWaybill(form.WBRegId);

                        MainRibbonControl.Enabled = true;
                        Cursor = Cursors.Default;
                        SplashAccessor.Close();

                        XtraMessageBox.Show("Запрос на повторное получение товарно-транспортной накладной успешно отправлен.",
                                            "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                MainRibbonControl.Enabled = true;
                Cursor = Cursors.Default;
                SplashAccessor.Close();

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка: '{0}'.", exception.Message));
            }
        }
        private void Garbage_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                List<InWaybill> selected = Document_gridView.GetSelectedRows().Select(i => (InWaybill) Document_gridView.GetRow(i)).Where(w => w.StatusEnum == InWaybillStatus.New).ToList();

                if ((selected.Count == 0) && (Document_gridView.GetSelectedRows().Length > 0))
                {
                    XtraMessageBox.Show("Переместить можно только такие накладные, которые находятся в статусе 'новая'.",
                                        "Выполнение операции невозможно", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (selected.Count == 0) return;

                if (DialogResult.Yes == XtraMessageBox.Show(string.Format("Переместить выбранные товарно-транспортные накладные (в количестве {0} шт.) в корзину?\r\n" +
                                                                          "Переместить можно только такие накладные, которые находятся в статусе 'новая'.", selected.Count)
                                                                          , "Подтверждение операции"
                                                                          , MessageBoxButtons.YesNo
                                                                          , MessageBoxIcon.Question))
                {
                    SplashAccessor.Show();
                    Cursor = Cursors.WaitCursor;
                    MainRibbonControl.Enabled = false;

                    Program.Logger.Info(this, string.Format("Производится попытка переноса в корзину входящих накладных (в количестве {0} шт.)...", selected.Count));

                    foreach (InWaybill document in selected)
                    {
                        documents.MoveToGarbage(document);
                    }

                    Program.Logger.Info(this, string.Format("... попытка переноса в корзину входящих накладных (в количестве {0} шт.) успешно завершена.", selected.Count));

                    Document_gridView.ClearSelection();
                }

                Document_gridView.RefreshData();
            }
            catch (Exception exception)
            {
                const string msg = "Во время перемещения документов в корзину произошла ошибка.";
                Program.Logger.Error(msg, exception);

                MainRibbonControl.Enabled = true;
                SplashAccessor.Close();
                Cursor = Cursors.Default;

                MainForm.ShowErrorMessage(string.Format("{0}\r\n{1}", msg, exception.Message));
            }
            finally
            {
                MainRibbonControl.Enabled = true;
                Cursor = Cursors.Default;

                SplashAccessor.Close();
            }
        }
        private void ExportToAxiTrade_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            exportToAxiTrade();
        }
        private void InCancel_barButtonItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (Document_bindingSource.Current == null) return;

                InWaybill selected = (InWaybill)Document_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                if ((selected.StatusEnum != InWaybillStatus.Accepted)
                    && (selected.StatusEnum != InWaybillStatus.Rejected)
                    && (selected.StatusEnum != InWaybillStatus.Difference))
                {
                    XtraMessageBox.Show("Отправить запрос на отмену проведения можно только для тех накладных, " +
                                        "для которых уже был сформирован акт подтверждения, расхождения или отказа.",
                                        "Выполнение операции невозможно", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                if (DialogResult.Yes == XtraMessageBox.Show(string.Format("Отправить запрос на отмену проведения акта по входящей накладной " +
                                                                          "с номером '{0}' за дату '{1}' от '{2}'?\r\n\r\n" +
                                                                          "После отправки такого запроса отправитель товарно-транспортной " +
                                                                          "накладной должен будет либо подтвердить отмену проведения, " +
                                                                          "либо отвергнуть отмену проведения указанной накладной.",
                                                                          selected.Number, selected.Date, selected.ShipperName),
                                                            "Подтверждение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    SplashAccessor.Show();
                    Cursor = Cursors.WaitCursor;
                    MainRibbonControl.Enabled = false;

                    documents.Repeal(selected);

                    Document_gridView.RefreshData();
                    Detail_treeList.DataSource = selected.TreeData;

                    XtraMessageBox.Show("Запрос на отмену проведения акта по накладной успешно отправлен.",
                                        "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                MainRibbonControl.Enabled = true;
                SplashAccessor.Close();
                Cursor = Cursors.Default;

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка: '{0}'.", exception.Message));
            }
            finally
            {
                MainRibbonControl.Enabled = true;
                Cursor = Cursors.Default;

                SplashAccessor.Close();
            }

        }
        #endregion Обработка событий пользовательского интерфейса.

        #region Внутренние методы класса.
        /// <summary>
        /// Изменить статус документа.
        /// </summary>
        /// <param name="status">Новый статус.</param>
	    private void changeDocumentReadingStatus(bool status)
	    {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка изменить статус документов на '{0}прочитанный'... ", status ? "" : "не"));

                Cursor = MdiParent.Cursor = Cursors.WaitCursor;

                foreach (int i in Document_gridView.GetSelectedRows())
                {
                    InWaybill p = (InWaybill)Document_gridView.GetRow(i);

                    if (p.AlreadyReading == status) continue;
                    
                    p.AlreadyReading = status;
                    documents.Storage.Save(p);

                    Program.Logger.Info(this, string.Format("... у документа '{0}' изменён статус на '{1}прочитанный'... ",
                                                            p.Description, p.AlreadyReading ? "" : "не"));
                }

                Document_gridView.RefreshData();

                Program.Logger.Info(this, "... изменение статуса у документов завершено успешно.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
                Cursor = MdiParent.Cursor = Cursors.Default;

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка: '{0}'.", exception.Message));
            }
            finally
            {
                Cursor = MdiParent.Cursor = Cursors.Default;
            }
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
        /// <summary>
        /// Экспортировать данные.
        /// </summary>
        /// <param name="format">Формат.</param>
        /// <param name="dialog">Диалог.</param>
        private void exportData(ExportFormat format, SaveFileDialog dialog)
        {
            try
            {
                // dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                switch (format)
                {
                    case ExportFormat.xls:
                        {
                            dialog.DefaultExt = "xls";
                            dialog.Filter = "Файлы Microsoft Excel (*.xls)|*.xls|Все файлы (*.*)|*.*";
                            break;
                        }
                    case ExportFormat.pdf:
                        {
                            dialog.DefaultExt = "pdf";
                            dialog.Filter = "Файлы Adobe Acrobat (*.pdf)|*.pdf|Все файлы (*.*)|*.*";
                            break;
                        }
                    case ExportFormat.html:
                        {
                            dialog.DefaultExt = "html";
                            dialog.Filter = "Файлы HTML (*.html)|*.html|Все файлы (*.*)|*.*";
                            break;
                        }
                    case ExportFormat.rtf:
                        {
                            dialog.DefaultExt = "rtf";
                            dialog.Filter = "Файлы Rich Text Format (*.rtf)|*.rtf|Все файлы (*.*)|*.*";
                            break;
                        }
                    case ExportFormat.txt:
                        {
                            dialog.DefaultExt = "txt";
                            dialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                            break;
                        }

                    default: throw new Exception("Формат \"" + format + "\" не поддерживается в данной версии.");
                }

                if (dialog.ShowDialog(this) != DialogResult.OK) return;

                Cursor = Cursors.WaitCursor;

                switch (format)
                {
                    case ExportFormat.xls:
                        {
                            Detail_treeList.ExportToXls(dialog.FileName);
                            break;
                        }

                    case ExportFormat.pdf:
                        {
                            Detail_treeList.ExportToPdf(dialog.FileName);
                            break;
                        }
                    case ExportFormat.html:
                        {
                            Detail_treeList.ExportToHtml(dialog.FileName);
                            break;
                        }
                    case ExportFormat.rtf:
                        {
                            Detail_treeList.ExportToRtf(dialog.FileName);
                            break;
                        }
                    case ExportFormat.txt:
                        {
                            Detail_treeList.ExportToText(dialog.FileName);
                            break;
                        }

                    default:
                        throw new NotImplementedException("Формат \"" + format + "\" не поддерживается в данной версии.");
                }

                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        /// <summary>
        /// Создать и отправить акт.
        /// </summary>
        /// <param name="actType">Типа акта.</param>
	    private void actHandler(ActType actType)
	    {
            try
            {
                if (Document_bindingSource.Current == null) return;

                InWaybill selected = (InWaybill)Document_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                if (selected.StatusEnum != InWaybillStatus.New)
                {
                    XtraMessageBox.Show("Отправить акт можно только для накладных, находящихся в статусе \"Новая\".",
                                        "Выполнение операции невозможно", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                switch (actType)
                {
                    case ActType.Accept:
                    {
                        (new ActSingleForm(documents, selected, true)).ShowDialog();
                        break;
                    }
                    case ActType.Reject:
                    {
                        (new ActSingleForm(documents, selected, false)).ShowDialog();
                        break;
                    }
                    case ActType.Difference:
                    {
                        (new ActDifferenceForm(documents, selected)).ShowDialog();
                        break;
                    }
                    default:
                    {
                        throw new Exception("Внутренняя ошибка. Неизвестный тип акта.");
                    }
                }

                Document_gridView.RefreshData();
                Detail_treeList.DataSource = selected.TreeData;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка: '{0}'.", exception.Message));
            }
        }
        /// <summary>
        /// Показать список позиций.
        /// </summary>
	    private void showPosition()
	    {
            try
            {
                if (Document_bindingSource.Current == null) return;

                InWaybill selected = (InWaybill)Document_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                (new PositionViewForm(documents, selected)).ShowDialog();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка: '{0}'.", exception.Message));
            }
	    }
        /// <summary>
        /// Экспортировать документ в формат "AxiTrade".
        /// </summary>
        private void exportToAxiTrade()
	    {
            try
            {
                if (Document_bindingSource.Current == null) return;

                InWaybill selected = (InWaybill)Document_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                // Export_saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                Export_saveFileDialog.DefaultExt = "xml";
                Export_saveFileDialog.Filter = "XML-файлы (*.xml)|*.xml|Все файлы (*.*)|*.*";

                if (Export_saveFileDialog.ShowDialog(this) != DialogResult.OK) return;

                Cursor = Cursors.WaitCursor;

                Program.Logger.Info(this, "Попытка экспорта документа в систему 'AxiTrade'...");

                Program.Logger.Info(this, string.Format("... документ: '{0}'...", selected.Description));
                Program.Logger.Info(this, string.Format("... файл для экспорта: '{0}'...", Export_saveFileDialog.FileName));

                documents.ExportToAxiTrade(selected, Export_saveFileDialog.FileName);

                Program.Logger.Info(this, "... экспорт документа в систему 'AxiTrade' успешно завершён.");
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;

                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка: '{0}'.", exception.Message));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
	    }
        #endregion Внутренние методы класса.
    }
}