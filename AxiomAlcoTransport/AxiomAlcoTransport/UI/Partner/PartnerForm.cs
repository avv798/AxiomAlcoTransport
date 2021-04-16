using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Ribbon;
using Axiom.AlcoTransport.Watchtower.Configuration;

namespace Axiom.AlcoTransport
{
	public partial class PartnerForm : XtraForm
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
		public PartnerForm()
		{
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

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
        public PartnerForm(Documents documents): this()
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
        private void PartnerForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузки... ");

                Cursor = MdiParent.Cursor = Cursors.WaitCursor;

                loadGridLayot();

                Document_bindingSource.DataSource = documents.Partners;

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
        private void PartnerForm_Activated(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка активации... ");

                Cursor = MdiParent.Cursor = Cursors.WaitCursor;
                MainRibbonControl.SelectedPage = Partner_ribbonPage;

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
        private void PartnerForm_FormClosed(object sender, FormClosedEventArgs e)
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
        private void RequestPartners_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                using (InnRequestForm form = new InnRequestForm("Укажите ИНН организации-партнёра, " +
                                                                "реквизиты которой вы хотите получить."))
                {
                    if (form.ShowDialog() != DialogResult.OK) return;

                    SplashAccessor.Show();
                    Cursor = Cursors.WaitCursor;
                    MainRibbonControl.Enabled = false;

                    documents.RequestPartners(form.Inn);

                    XtraMessageBox.Show("Запрос на получение реквизитов организации-партнёра успешно отправлен.",
                                        "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                const string msg = "Во время отправки запроса на получение справочника организаций произошла ошибка.";
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
        private void SearchPanel_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
        private void FilterCtor_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
        private void Columns_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
        private void Footer_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
        private void GroupPanel_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
        private void ExpandGroup_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
        private void CollapseGroup_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
        private void SetRead_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            changeDocumentReadingStatus(true);
        }
        private void SetNew_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            changeDocumentReadingStatus(false);
        }
        private void Document_gridView_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e == null) return;
                if (e.Appearance == null) return;

                Partner current = (Partner)Document_gridView.GetRow(e.RowHandle);
                if (current == null) return;

                if (current.AlreadyReading) return;

                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время отрисовки произошла ошибка.", exception);
            }
        }
        private void Copy_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
        private void PreviewList_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
        private void CopyDetail_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
        private void PreviewDocument_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
        private void XlsExport_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            exportData(ExportFormat.xls, Export_saveFileDialog);
        }
        private void PdfExport_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            exportData(ExportFormat.pdf, Export_saveFileDialog);
        }
        private void HtmlExport_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            exportData(ExportFormat.html, Export_saveFileDialog);
        }
        private void RtfExport_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            exportData(ExportFormat.rtf, Export_saveFileDialog);
        }
        private void TxtExport_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            exportData(ExportFormat.txt, Export_saveFileDialog);
        }
        private void SearchInDoc_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
        private void ExpandNodes_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
        private void CollapseNodes_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
        private void Document_gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            try
            {
                Cursor = MdiParent.Cursor = Cursors.WaitCursor;

                if (Document_bindingSource.Current == null) return;

                Partner selected = (Partner)Document_bindingSource.Current;

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
        private void Refresh_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Cursor = MdiParent.Cursor = Cursors.WaitCursor;

                if (Document_bindingSource.Current == null) return;

                Partner selected = (Partner)Document_bindingSource.Current;

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
        private void Garbage_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                List<Partner> selected = Document_gridView.GetSelectedRows().Select(i => (Partner)Document_gridView.GetRow(i)).ToList();

                if (selected.Count == 0) return;

                if (DialogResult.Yes == XtraMessageBox.Show(string.Format("Переместить выбранные документы (в количестве {0} шт.) в корзину?", selected.Count)
                                                                          , "Подтверждение операции"
                                                                          , MessageBoxButtons.YesNo
                                                                          , MessageBoxIcon.Question))
                {
                    SplashAccessor.Show();
                    Cursor = Cursors.WaitCursor;
                    MainRibbonControl.Enabled = false;

                    Program.Logger.Info(this, string.Format("Производится попытка переноса в корзину документов (в количестве {0} шт.)...", selected.Count));

                    foreach (Partner document in selected)
                    {
                        documents.MoveToGarbage(document);
                    }

                    Program.Logger.Info(this, string.Format("... попытка переноса в корзину документов (в количестве {0} шт.) успешно завершена.", selected.Count));

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
                    Partner p = (Partner)Document_gridView.GetRow(i);

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
        private void exportData(ExportFormat format, SaveFileDialog dialog)
        {
            try
            {
                dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

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

                    default: throw new NotImplementedException("Формат \"" + format + "\" не поддерживается в данной версии.");
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
        #endregion Внутренние методы класса.
    }
}