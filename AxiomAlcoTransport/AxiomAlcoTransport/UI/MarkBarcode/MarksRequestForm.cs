using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Axiom.AlcoTransport
{
    public partial class MarksRequestForm : XtraForm
    {
        #region Внутренние объекты класса.
        /// <summary>
        /// Список марок.
        /// Только чтение.
        /// </summary>
        private readonly ThreadedBindingList<StateMark> marks;
        /// <summary>
        /// Управление документами.
        /// Только чтение.
        /// </summary>
        private readonly Documents documents;
        #endregion Внутренние объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Список марок.
        /// Только чтение.
        /// </summary>
        public ThreadedBindingList<StateMark> Marks
        {
            get { return marks; }
        }
        #endregion Внешние объекты класса.

        #region Инициализация главной формы.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public MarksRequestForm()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                marks = new ThreadedBindingList<StateMark>();

                Program.Logger.Info(this, "... инициализация успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации произошла ошибка.", exception);
            }
        }
        public MarksRequestForm(Documents documents) : this()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации с параметрами... ");

                this.documents = documents;

                Program.Logger.Info(this, "... инициализация с параметрами успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации с параметрами произошла ошибка.", exception);
            }
        }
        #endregion Инициализация главной формы.

        #region Обработка событий пользовательского интерфейса.
        private void WaybillMarksRequestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Logger.Info(this, "Форма закрывается.");
        }
        private void WaybillMarksRequestForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузки формы... ");

                Document_bindingSource.DataSource = Marks;

                validateData();

                Program.Logger.Info(this, "... загрузка формы успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время загрузки произошла ошибка.", exception);
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
                DialogResult = DialogResult.OK;

                Close();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }

        }
        private void Add_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                StateMark mark;

                using (MarkEditorForm form = new MarkEditorForm(documents))
                {
                    if (form.ShowDialog() != DialogResult.OK) return;
                    
                    mark = form.Mark;
                }

                if (marks.Any(stateMark => (stateMark.MarkTypeCode == mark.MarkTypeCode)
                                           && (stateMark.Series == mark.Series)
                                           && (stateMark.Number == mark.Number)))
                {
                    XtraMessageBox.Show("Марка с указанными атрибутами уже присутствует в списке.", "Повтор марки", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                marks.Add(mark);

                Marks_gridView.RefreshData();

                validateData();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception));
            }
        }
        private void Remove_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Document_bindingSource.Current == null) return;

                StateMark selected = (StateMark)Document_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                marks.Remove(selected);
                
                Marks_gridView.RefreshData();

                validateData();
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
        /// Проверить данные.
        /// </summary>
        private void validateData()
        {
            try
            {
                Ok_simpleButton.Enabled = (Marks.Count > 0);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        #endregion Внутренние методы класса.
    }
}