using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;
using DevExpress.XtraEditors;
using Axiom.AlcoTransport.Watchtower.Configuration;

namespace Axiom.AlcoTransport
{
    public partial class TransferFromShopEditorForm : XtraForm
    {
        #region Внутренние объекты класса.
        /// <summary>
        /// Список документов.
        /// Только чтение.
        /// </summary>
        private readonly Documents documents;
        /// <summary>
        /// Редактируемый документ.
        /// Только чтение.
        /// </summary>
        private readonly TransferFromShop act;
        /// <summary>
        /// Признак изменения данных.
        /// </summary>
        private bool dataChanged;
        /// <summary>
        /// Список позиций.
        /// Только чтение.
        /// </summary>
        private readonly ThreadedBindingList<MovePosition> positions;
        #endregion Внутренние объекты класса.

        #region Инициализация главной формы.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public TransferFromShopEditorForm()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                documents = null;
                act = null;
                positions = new ThreadedBindingList<MovePosition>();

                dataChanged = false;

                Date_dateEdit.DateTime = DateTime.Now;

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
        /// <param name="act">Акт для редактирования.</param>
        public TransferFromShopEditorForm(Documents documents, TransferFromShop act) : this()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации с параметрами... ");

                this.documents = documents;
                this.act = act;

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
                Position_gridView.RestoreLayoutFromRegistry(registryPathVisualSettings + "Position");
                Source_gridView.RestoreLayoutFromRegistry(registryPathVisualSettings + "Rests");
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
                Position_gridView.SaveLayoutToRegistry(registryPathVisualSettings + "Position");
                Source_gridView.SaveLayoutToRegistry(registryPathVisualSettings + "Rests");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции сохранения раскладки грида произошла ошибка.", exception);
            }
        }
        #endregion Раскладка грида.

        #region Обработка событий пользовательского интерфейса.
        private void TransferFromShopEditorForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузки формы... ");

                Cursor = Cursors.WaitCursor;

                if (documents == null) throw new Exception("Список документов не может быть пустым.");
                if (documents.Configuration == null) throw new Exception("Конфигурация приложения не может быть пустой.");

                SourcePosition_bindingSource.DataSource = documents.GetLastShopRestsPositions(true);

                if (act == null) throw new Exception("Документ не может быть пустым объектом.");

                loadGridLayot();

                fillFormByActInfo();

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
        private void TransferFromShopEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (dataChanged)
                {
                    DialogResult result = XtraMessageBox.Show("Документ был изменен. Сохранить сделанные изменения?",
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
                Program.Logger.Error("Во время сохранения документа произошла ошибка.", exception);
                MainForm.ShowErrorMessage(exception);
            }
        }
        private void TransferFromShopEditorForm_FormClosed(object sender, FormClosedEventArgs e)
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
        private void SaveAndClose_simpleButton_Click(object sender, EventArgs e)
        {
            save();
            close();
        }
        private void Close_simpleButton_Click(object sender, EventArgs e)
        {
            close();
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
        private void Source_gridControl_DoubleClick(object sender, EventArgs e)
        {
            if (Source_gridControl.Enabled) addPosition();
        }
        private void Add_simpleButton_Click(object sender, EventArgs e)
        {
            if (Source_gridControl.Enabled) addPosition();
        }
        private void CheckPosition_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (positions.Count == 0)
                {
                    XtraMessageBox.Show("Список позиций пустой. Проверка не проводится.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                foreach (MovePosition MovePosition in positions)
                {
                    MovePosition.Check();
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

                MovePosition selected = (MovePosition)Position_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                positions.Remove(selected);

                int i = 1;

                foreach (MovePosition MovePosition in positions)
                {
                    MovePosition.Identity = i;

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
        private void APFind_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Source_gridView.OptionsFind.AlwaysVisible = !Source_gridView.OptionsFind.AlwaysVisible;
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
                Source_gridView.OptionsView.ShowGroupPanel = !Source_gridView.OptionsView.ShowGroupPanel;
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
                if (Source_gridView.Columns.Count > 2) Source_gridView.ShowFilterEditor(Source_gridView.Columns[2]);
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
                Source_gridView.ShowCustomization();
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
                Source_gridView.RefreshData();
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
        #endregion Обработка событий пользовательского интерфейса.

        #region Внутренние методы класса.
        /// <summary>
        /// Сохранить документ.
        /// </summary>
        private void save()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                Program.Logger.Info(this, string.Format("Попытка сохранить документ '{0}'...", act.Description));

                fillActByFormInfo();
                
                act.Check();
                documents.AddAndSave(act);

                Status_textEdit.Text = act.StatusNote;

                dataChanged = false;

                Program.Logger.Info(this, string.Format("... сохранение документа '{0}' успешно завершено.", act.Description));
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;

                Program.Logger.Error("Во время сохранения документа произошла ошибка.", exception);
                MainForm.ShowErrorMessage(string.Format("Во время сохранения документа произошла ошибка:\r\n'{0}'", exception));
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
        /// Заполнить форму информацией о документе.
        /// </summary>
        private void fillFormByActInfo()
        {
            try
            {
                Status_textEdit.Text = act.StatusNote;
                Identity_textEdit.Text = act.Identity;
                Number_textEdit.Text = act.Number;

                Date_dateEdit.EditValue = act.Date;

                positions.Clear();

                foreach (MovePosition MovePosition in act.MovePositions.OrderBy(x => x.Identity))
                {
                    positions.Add(MovePosition.Clone());
                }

                Detail_treeList.DataSource = act.TreeData;
                MainForm.ExpandDetailView(Detail_treeList);

                setControlsEnable();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время заполнения информации о документе произошла ошибка.", exception);
            }
        }
        /// <summary>
        /// Заполнить документ информацией с формы.
        /// </summary>
        private void fillActByFormInfo()
        {
            act.Number = Number_textEdit.Text;
            act.Date = (DateTime)Date_dateEdit.EditValue;

            act.MovePositions.Clear();

            foreach (MovePosition MovePosition in positions.OrderBy(x => x.Identity))
            {
                act.MovePositions.Add(MovePosition.Clone());
            }

            act.Check();
        }
        /// <summary>
        /// Управление элементами на форме в зависимости от статуса документа.
        /// </summary>
        private void setControlsEnable()
        {
            if ((act.StatusEnum == MovementStatus.Partial) || (act.StatusEnum == MovementStatus.New))
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

                Text = "Просмотр акта возврата из торгового зала организации";
            }
        }
        /// <summary>
        /// Добавить позицию.
        /// </summary>
        private void addPosition()
        {
            try
            {
                if (SourcePosition_bindingSource.Current == null) return;

                RestsPosition selected = (RestsPosition)SourcePosition_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                dataChange();

                #region Действия, если такая продукция уже присутствует в документе.

                if (positions.Any(MovePosition => (MovePosition.AlcoCode == selected.AlcoCode) && (MovePosition.FormBRegId == selected.FormBRegId)))
                {
                    XtraMessageBox.Show("В позициях документа уже присутствует такая продукция. " +
                                        "В данном случае вам необходимо изменить количество товара.",
                                        "Выполнение операции невозможно", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    return;
                }

                #endregion Действия, если такая продукция уже присутствует в документе.

                positions.Add(new MovePosition(MovementType.TransferFromShop, selected));

                int i = 1;

                foreach (MovePosition MovePosition in positions)
                {
                    MovePosition.Identity = i;

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
        #endregion Внутренние методы класса.
    }
}
