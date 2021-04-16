using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Axiom.AlcoTransport
{
    public partial class FullTextSearchForm : XtraForm
    {
        #region Внутренние объекты класса.
        /// <summary>
        /// Управление документами.
        /// Только чтение.
        /// </summary>
        private readonly Documents documents;
        #endregion Внутренние объекты класса.

        #region Инициализация главной формы.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public FullTextSearchForm()
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
        public FullTextSearchForm(Documents documents) : this()
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
        private void FullTextSearchForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Logger.Info(this, "Форма закрывается.");
        }
        private void FullTextSearchForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузки формы... ");

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
                Close();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        private void Find_textEdit_TextChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void Find_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                Program.Logger.Info(this, string.Format("Попытка выполнения полнотекстового поиска по строке '{0}'... ", Find_textEdit.Text));

                ThreadedBindingList<ADocument> results = documents.FullTextSearch(Find_textEdit.Text);

                Program.Logger.Info(this, string.Format("... выполнение полнотекстового поиска по строке '{0}' завершено; найдено документов: {1}.", Find_textEdit.Text, results.Count));

                Document_bindingSource.DataSource = results;

                Documents_gridView.CollapseAllGroups();

                Cursor = Cursors.Default;

                XtraMessageBox.Show(string.Format("Операция полнотекстового поиска завершена.\r\nНайдено документов: {0}.", results.Count), "Завершение поиска", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;

                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception.Message));
            }
            finally
            {
                Cursor = Cursors.Default;
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
                Find_simpleButton.Enabled = (Find_textEdit.Text.Length > 2);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        #endregion Внутренние методы класса.
    }
}