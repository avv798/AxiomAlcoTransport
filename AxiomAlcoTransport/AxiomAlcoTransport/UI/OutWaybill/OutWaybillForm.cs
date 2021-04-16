using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Ribbon;
using Axiom.AlcoTransport.Watchtower.Configuration;

namespace Axiom.AlcoTransport
{
	public partial class OutWaybillForm : XtraForm
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
		public OutWaybillForm()
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
        public OutWaybillForm(Documents documents): this()
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
        private void OutWaybillForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузки... ");

                Cursor = MdiParent.Cursor = Cursors.WaitCursor;

                loadGridLayot();

                Document_bindingSource.DataSource = documents.OutWaybills;

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
        private void OutWaybillForm_Activated(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка активации... ");

                Cursor = MdiParent.Cursor = Cursors.WaitCursor;
                MainRibbonControl.SelectedPage = OutWaybill_ribbonPage;

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
        private void OutWaybillForm_FormClosed(object sender, FormClosedEventArgs e)
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
        private void Add_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                createEditorWaybill(new OutWaybill());
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения создания накладной произошла ошибка.", exception);

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception));
            }
        }
        private void Clone_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (Document_bindingSource.Current == null) return;

                OutWaybill selected = (OutWaybill)Document_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                createEditorWaybill(selected.Clone());
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения клонирования накладной произошла ошибка.", exception);

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception));
            }
        }
        private void Edit_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            openEditorWaybill();
        }
        private void Remove_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (Document_bindingSource.Current == null) return;

                OutWaybill selected = (OutWaybill)Document_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                if ((selected.StatusEnum == OutWaybillStatus.Partial) || (selected.StatusEnum == OutWaybillStatus.Ready))
                {
                    if (DialogResult.Yes == XtraMessageBox.Show(string.Format("Удалить исходящую накладную с номером '{0}' от '{1}'?\r\n" +
                                                                              "Восстановление удалённых документов невозможно."
                                                                              , selected.Number, selected.OutDate.ToString("dd MMMM yyyy (dddd), HH:mm:ss"))
                                                              , "Подтверждение операции"
                                                              , MessageBoxButtons.YesNo
                                                              , MessageBoxIcon.Question))
                    { 
                        documents.Delete(selected);

                        Document_gridView.RefreshData();

                        XtraMessageBox.Show("Исходящая накладная успешно удалена.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Операция удаления разрешена для исходящих накладных, находящихся " +
                                        "в состоянии \"новая, заполнена частично\" или \"новая, готова к отправке\".",
                                        "Выполнение операции невозможно", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения удаления накладной произошла ошибка.", exception);

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception));
            }
        }
        private void Revoke_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (Document_bindingSource.Current == null) return;

                OutWaybill selected = (OutWaybill)Document_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                if (selected.StatusEnum == OutWaybillStatus.Confirmed)
                {
                    if (DialogResult.Yes == XtraMessageBox.Show(string.Format("Отозвать товарно-транспортную накладную можно только в том случае, если " +
                                                                              "получатель накладной не успел её обработать (подтвердить или отвергнуть).\r\n" +
                                                                              "В таком случае, отправитель формирует акт отказа по своей же собственной " +
                                                                              "накладной; в ответ от сервера ЕГАИС должны быть получены три документа:\r\n" +
                                                                              " - квитанция о приёме акта отказа;\r\n" +
                                                                              " - сам акт отказа;\r\n" +
                                                                              " - квитанция с уведомлением об отмене накладной.\r\n" +
                                                                              "\r\n" +
                                                                              "Отозвать исходящую накладную с номером '{0}' от '{1}' для '{2}'?\r\n"
                                                                              , selected.Number
                                                                              , selected.OutDate.ToString("dd MMMM yyyy (dddd), HH:mm:ss")
                                                                              , selected.ConsigneeName)
                                                              , "Подтверждение операции"
                                                              , MessageBoxButtons.YesNo
                                                              , MessageBoxIcon.Question))
                    {
                        SplashAccessor.Show();
                        Cursor = Cursors.WaitCursor;
                        MainRibbonControl.Enabled = false;

                        documents.Revoke(selected);

                        Document_gridView.RefreshData();

                        MainRibbonControl.Enabled = true;
                        Cursor = Cursors.Default;
                        SplashAccessor.Close();

                        XtraMessageBox.Show("Исходящая накладная успешно отозвана.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Отозвать можно только накладную, находящуюся в состоянии \"отправлена, зарегистрирована в ЕГАИС\".",
                                        "Выполнение операции невозможно", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        private void Document_gridControl_DoubleClick(object sender, EventArgs e)
        {
            openEditorWaybill();
        }
        private void Refresh_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Cursor = MdiParent.Cursor = Cursors.WaitCursor;

                Document_gridView.RefreshData();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обновления списка произошла ошибка.", exception);
            }
            finally
            {
                Cursor = MdiParent.Cursor = Cursors.Default;
            }
        }
        private void Check_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (Document_bindingSource.Current == null) return;

                OutWaybill selected = (OutWaybill)Document_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                if ((selected.StatusEnum == OutWaybillStatus.Partial)
                    || (selected.StatusEnum == OutWaybillStatus.Ready))
                {
                    selected.Check(false);
                    findPairNumbers(selected);

                    XtraMessageBox.Show("Проверка успешно завершена. Товарно-транспортная накладная заполнена корректно.",
                                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время проверки накладной произошла ошибка.", exception);

                XtraMessageBox.Show(string.Format("Во время проверки накладной найдена ошибка.\r\n{0}", exception.Message),
                                    "Обнаружена ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void Send_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (Document_bindingSource.Current == null) return;

                OutWaybill selected = (OutWaybill)Document_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                if (selected.StatusEnum != OutWaybillStatus.Ready)
                {
                    XtraMessageBox.Show("Отправить можно только такую накладную, которая находится в статусе 'новая, готова к отправке'.",
                                        "Выполнение операции невозможно", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                if (DialogResult.Yes == XtraMessageBox.Show(string.Format("Отправить накладную с номером '{0}' для '{1}'?",
                                                            selected.Number, selected.ConsigneeName),
                                                            "Подтверждение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    SplashAccessor.Show();
                    Cursor = Cursors.WaitCursor;
                    MainRibbonControl.Enabled = false;

                    selected.Check(false);
                    findPairNumbers(selected);

                    documents.Send(selected);

                    Document_gridView.RefreshData();

                    MainRibbonControl.Enabled = true;
                    Cursor = Cursors.Default;
                    SplashAccessor.Close();

                    XtraMessageBox.Show(string.Format("Накладная с номером '{0}' для '{1}' успешно отправлена.",
                                        selected.Number, selected.ConsigneeName),
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
        private void Resend_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (Document_bindingSource.Current == null) return;

                OutWaybill selected = (OutWaybill)Document_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                if ((selected.StatusEnum == OutWaybillStatus.Partial)
                    || (selected.StatusEnum == OutWaybillStatus.Ready)
                    || (selected.StatusEnum == OutWaybillStatus.Revoked)
                    || (selected.StatusEnum == OutWaybillStatus.Rejected))
                {
                    XtraMessageBox.Show("Переотправить можно накладную, которая уже была ранее отправлена.",
                                        "Выполнение операции невозможно", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                if (DialogResult.Yes == XtraMessageBox.Show(string.Format("Отправить повторно накладную с номером '{0}' для '{1}'?",
                                                            selected.Number, selected.ConsigneeName),
                                                            "Подтверждение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    SplashAccessor.Show();
                    Cursor = Cursors.WaitCursor;
                    MainRibbonControl.Enabled = false;

                    documents.Resend(selected);

                    Document_gridView.RefreshData();

                    SplashAccessor.Close();
                    Cursor = Cursors.Default;
                    MainRibbonControl.Enabled = true;

                    XtraMessageBox.Show(string.Format("Накладная с номером '{0}' для '{1}' успешно переотправлена.",
                                        selected.Number, selected.ConsigneeName),
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
        private void ShowPosition_barButtonItem_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            showPosition();
        }
        #endregion Обработка событий пользовательского интерфейса.

        #region Внутренние методы класса.
        /// <summary>
        /// Создать новую накладную в редакторе.
        /// </summary>
        /// <param name="outWaybill">Новая накладная.</param>
	    private void createEditorWaybill(OutWaybill outWaybill)
	    {
            try
            {
                outWaybill.Number = string.Format("{0}-{1}", documents.Configuration.FsrarId, DateTime.Now.ToString("yyyyMMdd-HHmmssfff"));

                (new OutWaybillEditorForm(documents, outWaybill, Document_gridView)).ShowDialog();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время создания накладной произошла ошибка.", exception);

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception));
            }
	    }
        /// <summary>
        /// Открыть форму редактирования накладной.
        /// </summary>
	    private void openEditorWaybill()
	    {
            try
            {
                if (Document_bindingSource.Current == null) return;

                OutWaybill selected = (OutWaybill)Document_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                (new OutWaybillEditorForm(documents, selected, Document_gridView)).ShowDialog();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время редактирования накладной произошла ошибка.", exception);

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception));
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
        /// Показать список позиций.
        /// </summary>
        private void showPosition()
        {
            try
            {
                if (Document_bindingSource.Current == null) return;

                OutWaybill selected = (OutWaybill)Document_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                (new PositionViewForm(null, selected)).ShowDialog();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка: '{0}'.", exception.Message));
            }
        }
        /// <summary>
        /// Попытка найти парные номера в списке исходящих накладных.
        /// </summary>
        /// <param name="outWaybill">Накладная для проверки.</param>
	    private void findPairNumbers(OutWaybill outWaybill)
	    {
            try
            {
                if ((outWaybill.StatusEnum == OutWaybillStatus.Confirmed)
                    || (outWaybill.StatusEnum == OutWaybillStatus.Sent)
                    || (outWaybill.StatusEnum == OutWaybillStatus.Revoked)
                    || (outWaybill.StatusEnum == OutWaybillStatus.Rejected)) return;

                if (documents.OutWaybills.Any(x => ((outWaybill.SurrogateId != x.SurrogateId)
                                                    && (outWaybill.Number == x.Number)
                                                    && (x.StatusEnum != OutWaybillStatus.Revoked)
                                                    && (x.StatusEnum != OutWaybillStatus.Rejected))))
                {
                    throw new Exception(string.Format("В списке присутствуют накладные с одинаковыми номерами: '{0}'.", outWaybill.Number));
                }
            }
            catch(Exception exception)
            {
                Program.Logger.Error("Во время поиска парных номеров в списке исходящих накладных произошла ошибка.", exception);

                throw;
            }
	    }
        #endregion Внутренние методы класса.
    }
}