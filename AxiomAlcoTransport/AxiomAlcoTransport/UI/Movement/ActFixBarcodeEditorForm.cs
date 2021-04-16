using System;
using System.Linq;
using System.Windows.Forms;
using Axiom.AlcoTransport.Document;
using Axiom.AlcoTransport.Watchtower.Configuration;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;

namespace Axiom.AlcoTransport.UI.Movement
{
    public partial class ActFixBarcodeEditorForm<T> : XtraForm where T:AMovement
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
        private readonly T act;

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
        public ActFixBarcodeEditorForm()
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
        public ActFixBarcodeEditorForm(Documents documents, T act) : this()
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
        private string RegistryPathVisualSettings
            => $"{Constants.RegistryPathVisualSettings}\\{GetType().Name}\\";

        /// <summary>
        /// Загрузка оформления редактора.
        /// </summary>
        private void LoadGridLayout()
        {
            MainForm.InLogErrorContext(() =>
            {
                Position_gridView.RestoreLayoutFromRegistry(RegistryPathVisualSettings + "Position");
                Source_gridView.RestoreLayoutFromRegistry(RegistryPathVisualSettings + "Rests");
            });
        }

        /// <summary>
        /// Сохранение оформления редактора.
        /// </summary>
        private void SaveGridLayout()
        {
            MainForm.InLogErrorContext(() =>
            {
                Position_gridView.SaveLayoutToRegistry(RegistryPathVisualSettings + "Position");
                Source_gridView.SaveLayoutToRegistry(RegistryPathVisualSettings + "Rests");
            });
        }

        #endregion Раскладка грида.

        #region Обработка событий пользовательского интерфейса.

        private void ActFixBarcodeEditorForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузки формы... ");
                Text = $@"Редактирование акта {(act is ActUnFixBarcode? "отмены":string.Empty)} фиксации штрихкодов";
                Cursor = Cursors.WaitCursor;

                if (documents == null) throw new Exception("Список документов не может быть пустым.");
                if (documents.Configuration == null)
                    throw new Exception("Конфигурация приложения не может быть пустой.");

                SourcePosition_bindingSource.DataSource = documents.GetLastRestsPositions(true);

                if (act == null) throw new Exception("Документ не может быть пустым объектом.");

                LoadGridLayout();

                FillFormByActInfo();

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

        private void ActFixBarcodeEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainForm.InLogErrorContext(() =>
            {
                if (dataChanged)
                {
                    DialogResult result = XtraMessageBox.Show("Документ был изменен. Сохранить сделанные изменения?",
                        "Сохранение данных", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        Save();

                        return;
                    }

                    if (result == DialogResult.No)
                    {
                        return;
                    }

                    e.Cancel = true;
                }
            }, true);
        }

        private void ActFixBarcodeEditorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Форма закрывается.");

                SaveGridLayout();
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
            Save();
            CloseForm();
        }

        private void Close_simpleButton_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        private void Identity_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            DataChange();
        }

        private void Number_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            DataChange();
        }

        private void Date_dateEdit_EditValueChanged(object sender, EventArgs e)
        {
            DataChange();
        }

        private void Base_comboBoxEdit_EditValueChanged(object sender, EventArgs e)
        {
            DataChange();
        }

        private void Note_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            DataChange();
        }

        private void Source_gridControl_DoubleClick(object sender, EventArgs e)
        {
            if (Source_gridControl.Enabled) AddPosition();
        }

        private void Add_simpleButton_Click(object sender, EventArgs e)
        {
            if (Source_gridControl.Enabled) AddPosition();
        }

        private void CheckPosition_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (positions.Count == 0)
                {
                    XtraMessageBox.Show("Список позиций пустой. Проверка не проводится.", "Информация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }

                foreach (MovePosition movePosition in positions)
                {
                    movePosition.Check();
                }

                XtraMessageBox.Show("Список позиций успешно проверен. Ошибок не найдено.", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения проверки списка позиций произошла ошибка.", exception);

                XtraMessageBox.Show(
                    $"Во время проверки списка обнаружена ошибка.\r\n{exception.Message}",
                    "Обнаружена ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RefreshPosition_simpleButton_Click(object sender, EventArgs e)
        {
            MainForm.InLogErrorContext(() => { Position_gridView.RefreshData(); });
        }

        private void Position_gridView_CellValueChanged(object sender,
            DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            MainForm.InLogErrorContext(() =>
            {
                DataChange();
                Position_gridView.RefreshData();
            });
        }

        private void Remove_simpleButton_Click(object sender, EventArgs e)
        {
            MainForm.InLogErrorContext(() =>
            {
                if (Position_bindingSource.Current == null) return;

                DataChange();

                MovePosition selected = (MovePosition) Position_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                positions.Remove(selected);

                int i = 1;

                foreach (MovePosition movePosition in positions)
                {
                    movePosition.Identity = i;

                    ++i;
                }

                Position_gridView.RefreshData();
            }, true);
        }

        private void PrintPosition_simpleButton_Click(object sender, EventArgs e)
        {
            MainForm.InLogErrorContext(() =>
            {
                if (Position_gridControl.IsPrintingAvailable) Position_gridControl.ShowRibbonPrintPreview();
            }, true);
        }

        private void APFind_simpleButton_Click(object sender, EventArgs e)
        {
            MainForm.InLogErrorContext(
                () => { Source_gridView.OptionsFind.AlwaysVisible = !Source_gridView.OptionsFind.AlwaysVisible; });
        }

        private void APGroup_simpleButton_Click(object sender, EventArgs e)
        {
            MainForm.InLogErrorContext(
                () => { Source_gridView.OptionsView.ShowGroupPanel = !Source_gridView.OptionsView.ShowGroupPanel; });
        }

        private void APFilter_simpleButton_Click(object sender, EventArgs e)
        {
            MainForm.InLogErrorContext(() =>
            {
                if (Source_gridView.Columns.Count > 2) Source_gridView.ShowFilterEditor(Source_gridView.Columns[2]);
            });
        }

        private void APColumns_simpleButton_Click(object sender, EventArgs e)
        {
            MainForm.InLogErrorContext(() => { Source_gridView.ShowCustomization(); });
        }

        private void APRefresh_simpleButton_Click(object sender, EventArgs e)
        {
            MainForm.InLogErrorContext(() => { Source_gridView.RefreshData(); });
        }

        private void ColumnsPosition_simpleButton_Click(object sender, EventArgs e)
        {
            MainForm.InLogErrorContext(() => { Position_gridView.ShowCustomization(); });
        }

        private void FindPosition_simpleButton_Click(object sender, EventArgs e)
        {
            MainForm.InLogErrorContext(
                () => { Position_gridView.OptionsFind.AlwaysVisible = !Position_gridView.OptionsFind.AlwaysVisible; });
        }

        private void ExpandTree_simpleButton_Click(object sender, EventArgs e)
        {
            MainForm.InLogErrorContext(() => { MainForm.ExpandDetailView(Detail_treeList); });
        }

        private void CollapseTree_simpleButton_Click(object sender, EventArgs e)
        {
            MainForm.InLogErrorContext(() => { Detail_treeList.CollapseAll(); });
        }

        private void Print_simpleButton_Click(object sender, EventArgs e)
        {
            MainForm.InLogErrorContext(() =>
            {
                if (Detail_treeList.IsPrintingAvailable) Detail_treeList.ShowRibbonPrintPreview();
            }, true);
        }

        private void scanBarcode_simpleButton_Click(object sender, EventArgs e)
        {
            if (Position_bindingSource.Current == null) return;
            MainForm.InLogErrorContext(() =>
            {
                var selected = GetSelectedMovePosition();

                PDF417RequestForm pdf417Form = new PDF417RequestForm(selected.PDF417Codes,
                    "Отсканируйте штрих-код PDF-417");

                if (pdf417Form.ShowDialog(this) == DialogResult.OK)
                {
                    selected.PDF417Codes.Add(pdf417Form.PDF417);
                }
                else
                {
                    Position_gridView.RefreshData();
                }
            });
        }

        private void deleteItemButtonEdit_ButtonClick(object sender,
            DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (Position_bindingSource.Current == null) return;
            MainForm.InLogErrorContext(() =>
            {
                var selected = GetSelectedMovePosition();
                selected.PDF417Codes.Remove(((ButtonEdit) sender).Text);
                Position_gridView.RefreshData();
                var seletedRow = Position_gridView.GetSelectedRows().FirstOrDefault();
                Position_gridView.SetMasterRowExpanded(seletedRow, false);
                Position_gridView.SetMasterRowExpanded(seletedRow, true);
            });
        }

        private MovePosition GetSelectedMovePosition()
        {
            MovePosition selected = (MovePosition) Position_bindingSource.Current;
            if (selected == null) throw new Exception("Ошибка получения текущей позиции.");
            return selected;
        }

        #endregion Обработка событий пользовательского интерфейса.

        #region Внутренние методы класса.

        /// <summary>
        /// Сохранить документ.
        /// </summary>
        private void Save()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                Program.Logger.Info(this, $"Попытка сохранить документ '{act.Description}'...");

                FillActByFormInfo();

                act.Check();
                documents.AddAndSave(act);

                Status_textEdit.Text = act.StatusNote;

                dataChanged = false;

                Program.Logger.Info(this,
                    $"... сохранение документа '{act.Description}' успешно завершено.");
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;

                Program.Logger.Error("Во время сохранения документа произошла ошибка.", exception);
                MainForm.ShowErrorMessage($"Во время сохранения документа произошла ошибка:\r\n'{exception}'");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Закрыть форму.
        /// </summary>
        private void CloseForm()
        {
            MainForm.InLogErrorContext(Close);
        }

        /// <summary>
        /// Обработка изменения данных.
        /// </summary>
        private void DataChange()
        {
            dataChanged = true;
        }

        /// <summary>
        /// Заполнить форму информацией о документе.
        /// </summary>
        private void FillFormByActInfo()
        {
            MainForm.InLogErrorContext(() =>
            {
                Status_textEdit.Text = act.StatusNote;
                Identity_textEdit.Text = act.Identity;
                Number_textEdit.Text = act.Number;

                Date_dateEdit.EditValue = act.Date;

                Note_textEdit.Text = act.Note;

                positions.Clear();

                foreach (MovePosition movePosition in act.MovePositions.OrderBy(x => x.Identity))
                {
                    positions.Add(movePosition.Clone());
                }

                Detail_treeList.DataSource = act.TreeData;
                MainForm.ExpandDetailView(Detail_treeList);

                SetControlsEnable();
            });
        }

        /// <summary>
        /// Заполнить документ информацией с формы.
        /// </summary>
        private void FillActByFormInfo()
        {
            act.Number = Number_textEdit.Text;
            act.Date = (DateTime) Date_dateEdit.EditValue;

            act.Note = Note_textEdit.Text;

            act.MovePositions.Clear();

            foreach (MovePosition movePosition in positions.OrderBy(x => x.Identity))
            {
                act.MovePositions.Add(movePosition.Clone());
            }

            act.Check();
        }

        /// <summary>
        /// Управление элементами на форме в зависимости от статуса документа.
        /// </summary>
        private void SetControlsEnable()
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

                Text = @"Просмотр акта фиксации штрихкодов";
            }
        }

        /// <summary>
        /// Добавить позицию.
        /// </summary>
        private void AddPosition()
        {
            MainForm.InLogErrorContext(() =>
            {
                if (SourcePosition_bindingSource.Current == null) return;

                RestsPosition selected = (RestsPosition) SourcePosition_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                DataChange();

                #region Действия, если такая продукция уже присутствует в документе.

                if (
                    positions.Any(
                        movePosition =>
                            (movePosition.AlcoCode == selected.AlcoCode) &&
                            (movePosition.FormBRegId == selected.FormBRegId)))
                {
                    XtraMessageBox.Show("В позициях документа уже присутствует такая продукция. " +
                                        "В данном случае вам необходимо изменить количество товара.",
                        "Выполнение операции невозможно", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                #endregion Действия, если такая продукция уже присутствует в документе.

                positions.Add(new MovePosition(MovementType.ActChargeOff, selected));

                int i = 1;

                foreach (MovePosition movePosition in positions)
                {
                    movePosition.Identity = i;

                    ++i;
                }

                Position_gridView.RefreshData();
            }, true);
        }

        #endregion Внутренние методы класса.
    }
}