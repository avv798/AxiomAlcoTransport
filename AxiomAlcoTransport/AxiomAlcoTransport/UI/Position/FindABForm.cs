using System;
using System.Windows.Forms;
using System.Collections.Generic;
using DevExpress.XtraEditors;
using Axiom.AlcoTransport.Watchtower.Configuration;

namespace Axiom.AlcoTransport
{
    public partial class FindABForm : XtraForm
    {
        #region Внутренние объекты класса.
        /// <summary>
        /// Позиция.
        /// Только чтение.
        /// </summary>
        private readonly OutPosition position;
        /// <summary>
        /// Список документов.
        /// Только чтение.
        /// </summary>
        private readonly Documents documents;
        /// <summary>
        /// Список предполагаемых позиций.
        /// Только чтение.
        /// </summary>
        private readonly List<Position> positions;
        /// <summary>
        /// Идентификатор справки 'А'.
        /// </summary>
        private string formARegId;
        /// <summary>
        /// Идентификатор справки 'Б'.
        /// </summary>
        private string formBRegId;
        #endregion Внутренние объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Идентификатор справки 'А'.
        /// Только чтение.
        /// </summary>
        public string FormARegId
        {
            get { return formARegId; }
        }
        /// <summary>
        /// Идентификатор справки 'Б'.
        /// Только чтение.
        /// </summary>
        public string FormBRegId
        {
            get { return formBRegId; }
        }
        #endregion Внешние объекты класса.

        #region Инициализация главной формы.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public FindABForm()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                formARegId = string.Empty;
                formBRegId = string.Empty;

                documents = null;
                position = null;
                positions = new List<Position>();

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
        /// <param name="documents">Документы.</param>
        /// <param name="position">Позиция.</param>
        public FindABForm(Documents documents, OutPosition position) : this()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации с параметрами... ");

                this.documents = documents;
                this.position = position;

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
        private void FindABForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузки формы... ");

                loadGridLayot();

                loadPositions();
                Document_BindingSource.DataSource = positions;

                Program.Logger.Info(this, "... загрузка формы успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время загрузки произошла ошибка.", exception);
                MainForm.ShowErrorMessage(string.Format("Во время загрузки произошла ошибка: '{0}'.", exception.Message));
            }
        }
        private void Choose_simpleButton_Click(object sender, EventArgs e)
        {
            choose();
        }
        private void Document_gridControl_DoubleClick(object sender, EventArgs e)
        {
            choose();
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
        private void FindABForm_FormClosed(object sender, FormClosedEventArgs e)
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

        #region Внутренние методы класса.
        /// <summary>
        /// Загрузить список предполагаемых позиций.
        /// </summary>
        private void loadPositions()
        {
            if (position == null) throw new Exception("Выбранная позиция - пустая.");
            if (string.IsNullOrWhiteSpace(position.AlcoCode)) throw new Exception("У выбранной позиции отсутствует обязательной поле 'Код алкогольной продукции' ('AlcCode').");

            foreach (InWaybill inWaybill in documents.InWaybills)
            {
                if ((inWaybill.StatusEnum == InWaybillStatus.Partial)
                    || (inWaybill.StatusEnum == InWaybillStatus.Rejected)) continue;

                foreach (Position item in inWaybill.GetPositions())
                {
                    if ((item.AlcoCode == position.AlcoCode)
                        && (item.Quantity >= 0.0m)
                        && (item.Price >= 0.0m)
                        && (!string.IsNullOrWhiteSpace(item.FormARegId))
                        && (!string.IsNullOrWhiteSpace(item.InformBRegId)))
                    {
                        item.Check();

                        positions.Add(item);
                    }
                }
            }

            Choose_simpleButton.Enabled = (positions.Count != 0);
        }
        /// <summary>
        /// Заполнить позицию выбранными идентификаторами.
        /// </summary>
        private void fillOutData()
        {
            if (Document_BindingSource.Current == null) return;

            Position selected = (Position)Document_BindingSource.Current;

            if (selected == null) throw new Exception("Ошибка получения текущего документа.");

            formARegId = selected.FormARegId;
            formBRegId = selected.InformBRegId;
        }
        /// <summary>
        /// Выбрать позицию и закрыть окно.
        /// </summary>
        private void choose()
        {
            try
            {
                if (positions.Count == 0) return;

                fillOutData();

                DialogResult = DialogResult.OK;

                Close();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
                MainForm.ShowErrorMessage(string.Format("Во время обработки события пользовательского интерфейса произошла ошибка: '{0}'.", exception.Message));
            }
        }
        #endregion Внутренние методы класса.
    }
}