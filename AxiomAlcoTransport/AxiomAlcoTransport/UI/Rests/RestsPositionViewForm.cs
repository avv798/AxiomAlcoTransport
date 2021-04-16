using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Axiom.AlcoTransport.Watchtower.Configuration;

namespace Axiom.AlcoTransport
{
    public partial class RestsPositionViewForm : XtraForm
    {
        #region Внутренние объекты класса.
        /// <summary>
        /// Остатки.
        /// Только чтение.
        /// </summary>
        private readonly ARests rests;
        /// <summary>
        /// Управление документами.
        /// Только чтение.
        /// </summary>
        private readonly Documents documents;
        #endregion Внутренние объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Накладная.
        /// Только чтение.
        /// </summary>
        public ARests Rests
        {
            get { return rests; }
        }
        #endregion Внешние объекты класса.

        #region Инициализация главной формы.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public RestsPositionViewForm()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                rests = null;

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
        /// <param name="rests">Остатки.</param>
        /// <param name="documents">Управление документами.</param>
        /// <param name="enableSearchInWaybill">Показывать кнопку поиска входящей накладной.</param>
        public RestsPositionViewForm(ARests rests, Documents documents, bool enableSearchInWaybill = false) : this()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации с параметрами... ");

                this.rests = rests;

                Document_BindingSource.DataSource = rests.GetPositions();
                Document_gridView.ExpandAllGroups();

                this.documents = documents;

                FindInWaybill_simpleButton.Enabled = FindInWaybill_simpleButton.Visible = MakeBCRestRequest_simpleButton.Visible = enableSearchInWaybill;
                
                Program.Logger.Info(this, "... инициализация с параметрами успешно завершена.");
            }
            catch (Exception exception)
            {
                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка.\r\n{0}", exception.Message));

                Program.Logger.Error("Во время инициализации произошла ошибка.", exception);
            }
        }
        #endregion Инициализация главной формы.

        #region Обработка событий пользовательского интерфейса.
        private void RestsPositionViewForm_FormClosed(object sender, FormClosedEventArgs e)
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
        private void RestsPositionViewForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузки формы... ");

                Text = string.Format("Список позиций документа-остатков на '{0}'", rests.RestsDate);

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
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
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

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception));
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
                if (Document_gridView.Columns.Count > 1) Document_gridView.ShowFilterEditor(Document_gridView.Columns[1]);
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
        private void FindInWaybill_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка поиска накладной по идентификатору справки 'Б'... ");

                if (Document_BindingSource.Current == null) return;

                RestsPosition selected = (RestsPosition)Document_BindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                if (string.IsNullOrWhiteSpace(selected.FormBRegId)) throw new Exception("Поиск по пустому идентификатору справки 'Б' невозможен.");

                SplashAccessor.Show();
                Cursor = Cursors.WaitCursor;

                InWaybill inWaybill = documents.FindInWaybill(selected.FormBRegId);

                SplashAccessor.Close();
                Cursor = Cursors.Default;

                if (inWaybill == null)
                {
                    XtraMessageBox.Show(string.Format("К сожалению, не удалось найти входящую товарно-транспортную накладную " +
                                                      "по указанному идентификатору справки 'Б' ('{0}').\r\n" +
                                                      "Вероятно, данная алкогольная продукция была поставлена на баланс склада " +
                                                      "организации с помощью 'Акта постановки на баланс'.", selected.FormBRegId),
                                        "Входящая накладная не найдена", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    (new InWaybillViewForm(inWaybill)).ShowDialog();
                }

                Program.Logger.Info(this, "... поиска накладной по идентификатору справки 'Б' завершён.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время поиска накладной по идентификатору справки 'Б' произошла ошибка.", exception);

                SplashAccessor.Close();
                Cursor = Cursors.Default;
            }
            finally
            {
                SplashAccessor.Close();
                Cursor = Cursors.Default;
            }
        }
        private void makeBcRestRequest_Click(object sender, EventArgs e)
        {
            var rowIndex = Document_gridView.GetSelectedRows().First();
            if (rowIndex != -1 && !string.IsNullOrEmpty(Document_gridView.GetRowCellDisplayText(rowIndex, "FormBRegId")))
            {
                var formBRegId = Document_gridView.GetRowCellDisplayText(rowIndex, "FormBRegId");

                try
                {
                    if (DialogResult.Yes ==
                        XtraMessageBox.Show(
                            $"Отправить на сервер запрос на получение документа-остатков по справке 'B' с идентификатором {formBRegId}?"
                            , "Подтверждение операции"
                            , MessageBoxButtons.YesNo
                            , MessageBoxIcon.Question))
                    {
                        SplashAccessor.Show();
                        Cursor = Cursors.WaitCursor;
                        
                        documents.RequestBCodeRests(formBRegId);
                    }
                }
                catch (Exception exception)
                {
                    const string msg =
                        "Во время отправки запроса на получение документа-остатков по штрихкодам произошла ошибка.";
                    Program.Logger.Error(msg, exception);

                    SplashAccessor.Close();
                    Cursor = Cursors.Default;

                    MainForm.ShowErrorMessage(string.Format("{0}\r\n{1}", msg, exception.Message));
                }
                finally
                {
                    Cursor = Cursors.Default;

                    SplashAccessor.Close();
                }

            }

            else
            {
                MainForm.ShowErrorMessage("Выберите запись с корректным идентификатором справки 'B'");
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