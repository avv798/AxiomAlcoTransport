using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Axiom.AlcoTransport
{
	/// <summary>
	/// Форма-представление домашней страницы приложения.
	/// </summary>
	public partial class HomeForm : XtraForm
	{
        #region Внутренние константы класса.
        /// <summary>
        /// Минимальная высота формы.
        /// Константа.
        /// </summary>
	    private const int minHeight = 570;
        #endregion Внутренние константы класса.

		#region Внутренние объекты класса.
		/// <summary>
		/// Цвет шрифта для восстановления.
		/// Только чтение.
		/// </summary>
		private readonly Color prevColor;
        /// <summary>
        /// Списки адресов.
        /// Только чтение.
        /// </summary>
	    private readonly Addresses addresses;
        /// <summary>
        /// Ядро.
        /// Только чтение.
        /// </summary>
	    private readonly Documents documents;
        /// <summary>
        /// Главная форма.
        /// Только чтение.
        /// </summary>
	    private readonly MainForm mainForm;
		#endregion Внутренние объекты класса.

		#region Конструкторы класса.
		/// <summary>
		/// Конструктор класса "по умолчанию".
		/// </summary>
		public HomeForm()
		{
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                prevColor = ProjectName_labelControl.ForeColor;
                addresses = null;
                documents = null;
                mainForm = null;

                setVisibleStatus(Size.Height >= minHeight);

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
	    /// <param name="mainForm">Главная форма.</param>
	    /// <param name="documents">Ядро.</param>
	    public HomeForm(MainForm mainForm, Documents documents) : this()
		{
            try
            {
                Program.Logger.Info(this, "Попытка инициализации с параметрами... ");

                this.mainForm = mainForm;
                this.documents = documents;
                addresses = documents.Addresses;

                FsrarId_textEdit.Text = documents.Configuration.FsrarId;
                Inn_textEdit.Text = documents.Configuration.Inn;

                this.documents.DocumentReceivedEvent += handleDocumentReceived;

                Program.Logger.Info(this, "... инициализация с параметрами успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации произошла ошибка.", exception);
            }
		}
		#endregion Конструкторы класса.

		#region Обработка событий пользовательского интерфейса.
        private void HomeForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузки... ");

                Cursor = MdiParent.Cursor = Cursors.WaitCursor;

                refreshImportantDocuments();

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
        private void About_hyperLinkEdit_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
		{
			try
			{
				new AboutForm().ShowDialog(this);
			}
			catch (Exception exception)
			{
				Program.Logger.Error(this, exception.Message);
				MainForm.ShowErrorMessage(exception);
			}
		}
		private void ProjectName_labelControl_MouseMove(object sender, MouseEventArgs e)
		{
			ProjectName_labelControl.ForeColor = Color.Blue;
		}
		private void AppName_labelControl_MouseMove(object sender, MouseEventArgs e)
		{
			AppName_labelControl.ForeColor = Color.Blue;
		}
		private void Home_labelControl_MouseMove(object sender, MouseEventArgs e)
		{
			Home_labelControl.ForeColor = Color.Blue;
		}
		private void ProjectName_labelControl_MouseLeave(object sender, EventArgs e)
		{
			ProjectName_labelControl.ForeColor
				= AppName_labelControl.ForeColor
				= Home_labelControl.ForeColor = prevColor;
		}
        private void HomeForm_Activated(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка активации... ");

                refreshImportantDocuments();

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
        private void HomeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Форма закрывается.");
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
        private void Licence_hyperLinkEdit_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            try
            {
                (new LicenceForm()).ShowDialog(this);
            }
            catch (Exception exception)
            {
                Program.Logger.Error(this, exception.Message);
                MainForm.ShowErrorMessage(exception);
            }
        }
        private void MainUrl_hyperLinkEdit_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            try
            {
                e.EditValue = addresses.Main;
            }
            catch (Exception exception)
            {
                Program.Logger.Error(this, exception.Message);   
            }
        }
        private void InUrl_hyperLinkEdit_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            try
            {
                e.EditValue = addresses.GetRequestIn;
            }
            catch (Exception exception)
            {
                Program.Logger.Error(this, exception.Message);
            }

        }
        private void OutUrl_hyperLinkEdit_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            try
            {
                e.EditValue = addresses.GetRequestOut;
            }
            catch (Exception exception)
            {
                Program.Logger.Error(this, exception.Message);
            }
        }
        private void ProjectName_labelControl_Click(object sender, EventArgs e)
        {
            try
            {
                MainForm.OpenCompanySite();
            }
            catch (Exception exception)
            {
                Program.Logger.Error(this, exception.Message);
            }
        }
        private void Informa_pictureEdit_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                MainForm.OpenCompanySite();
            }
            catch (Exception exception)
            {
                Program.Logger.Error(this, exception.Message);
            }
        }
        private void Help_hyperLinkEdit_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            try
            {
                MainForm.OpenUserGuide();
            }
            catch (Exception exception)
            {
                Program.Logger.Error(this, exception.Message);
            }
        }
        private void HomeForm_Resize(object sender, EventArgs e)
        {
            try
            {
                setVisibleStatus(Size.Height >= minHeight);
            }
            catch (Exception exception)
            {
                Program.Logger.Error(this, exception.Message);
            }
        }
        private void InWaybill_hyperLinkEdit_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            mainForm.ShowInWaybillForm();
        }
        private void InWaybillCount_hyperLinkEdit_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            mainForm.ShowInWaybillForm();
        }
        private void ActsWaybill_hyperLinkEdit_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            mainForm.ShowWaybillActForm();
        }
        private void ActsWaybillCount_hyperLinkEdit_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            mainForm.ShowWaybillActForm();
        }
        private void DiffActs_hyperLinkEdit_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            mainForm.ShowWaybillTicketForm();
        }
        private void DiffActsCount_hyperLinkEdit_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            mainForm.ShowWaybillTicketForm();
        }
        private void Repeal_hyperLinkEdit_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            mainForm.ShowWaybillRepealForm();
        }
        private void RepealCount_hyperLinkEdit_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            mainForm.ShowWaybillRepealForm();
        }
        private void Rests_hyperLinkEdit_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            mainForm.ShowRestsForm<Rests>();
        }
        private void RestsCount_hyperLinkEdit_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            mainForm.ShowRestsForm<Rests>();
        }
        private void RestsShop_hyperLinkEdit_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            mainForm.ShowRestsForm<ShopRests>();
        }
        private void RestsShopCount_hyperLinkEdit_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            mainForm.ShowRestsForm<ShopRests>();
        }
        private void Tickets_hyperLinkEdit_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            mainForm.ShowTicketForm();
        }
        private void TicketsCount_hyperLinkEdit_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            mainForm.ShowTicketForm();
        }
        private void Refresh_pictureEdit_Click(object sender, EventArgs e)
        {
            try
            {
                refreshImportantDocuments();
            }
            catch (Exception exception)
            {
                Program.Logger.Error(this, exception.Message);
            }
        }
        #endregion Обработка событий пользовательского интерфейса.

        #region Внутренние методы класса.
        /// <summary>
        /// Установить видимость компонентов.
        /// </summary>
        /// <param name="status">Видимость компонентов.</param>
	    private void setVisibleStatus(bool status)
	    {
	        BottomSeparator_labelControl.Visible = status;
	        Licence_hyperLinkEdit.Visible = status;
	        Help_hyperLinkEdit.Visible = status;
	        About_hyperLinkEdit.Visible = status;
	    }
        /// <summary>
        /// Обработка события "получение нового документа".
        /// </summary>
        /// <param name="sender">Отправитель.</param>
        /// <param name="e">Аргументы.</param>
        private void handleDocumentReceived(object sender, DocumentReceivedEventArgs e)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка обновить список важных документов для события '{0}'...", e.GetType().FullName));

                if (InvokeRequired)
                {
                    Program.Logger.Info(this, "\t... вызов из фонового процесса ...");

                    BeginInvoke(new refreshImportantDocumentsDelegate(refreshImportantDocuments));
                }
                else
                {
                    Program.Logger.Info(this, "\t... вызов из основного процесса ...");

                    refreshImportantDocuments();
                }

                Program.Logger.Info(this, "... список важных документов для события успешно обновлён.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Неожиданное исключение во время попытки обновления списка важных документов.", exception);
            }
        }
        /// <summary>
        /// Делегат для обновления списка важных документов.
        /// </summary>
        private delegate void refreshImportantDocumentsDelegate();
        /// <summary>
        /// Обновить список важных документов.
        /// </summary>
        private void refreshImportantDocuments()
        {
            try
            {
                Program.Logger.Info(this, "Попытка обновить список важных документов...");

                ImportantDocs_labelControl.Appearance.ImageIndex = 0;

                setHyperLinksStyle(documents.InWaybills.Count, documents.InWaybills.Count(x => (!x.AlreadyReading)), InWaybill_hyperLinkEdit, InWaybillCount_hyperLinkEdit);
                setHyperLinksStyle(documents.WaybillActs.Count, documents.WaybillActs.Count(x => (!x.AlreadyReading)), ActsWaybill_hyperLinkEdit, ActsWaybillCount_hyperLinkEdit);
                setHyperLinksStyle(documents.WaybillTickets.Count, documents.WaybillTickets.Count(x => (!x.AlreadyReading)), DiffActs_hyperLinkEdit, DiffActsCount_hyperLinkEdit);
                setHyperLinksStyle(documents.WaybillRepeals.Count, documents.WaybillRepeals.Count(x => (!x.AlreadyReading)), Repeal_hyperLinkEdit, RepealCount_hyperLinkEdit);
                setHyperLinksStyle(documents.RestsList.Count, documents.RestsList.Count(x => (!x.AlreadyReading)), Rests_hyperLinkEdit, RestsCount_hyperLinkEdit);
                setHyperLinksStyle(documents.ShopRestsList.Count, documents.ShopRestsList.Count(x => (!x.AlreadyReading)), RestsShop_hyperLinkEdit, RestsShopCount_hyperLinkEdit);
                setHyperLinksStyle(documents.Tickets.Count, documents.Tickets.Count(x => (!x.AlreadyReading)), Tickets_hyperLinkEdit, TicketsCount_hyperLinkEdit);

                Program.Logger.Info(this, "... список важных документов успешно обновлён.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Неожиданное исключение во время показа области напоминаний.", exception);
            }
        }
	    /// <summary>
	    /// Обновить гиперссылку.
	    /// </summary>
        /// <param name="total">Общее количество.</param>
        /// <param name="unread">Непрочитанных.</param>
	    /// <param name="nameEdit">Наименование.</param>
	    /// <param name="countEdit">Количество.</param>
	    private void setHyperLinksStyle(int total, int unread, HyperLinkEdit nameEdit, HyperLinkEdit countEdit)
	    {
            if (unread > 0) ImportantDocs_labelControl.Appearance.ImageIndex = 1;

            countEdit.Text = string.Format("{0} / {1}", unread, total);

            nameEdit.Font = (unread > 0) ? new Font(nameEdit.Font, FontStyle.Bold | FontStyle.Underline)
                                         : new Font(nameEdit.Font, FontStyle.Regular | FontStyle.Underline);

            countEdit.Font = (unread > 0) ? new Font(countEdit.Font, FontStyle.Bold | FontStyle.Underline)
                                          : new Font(countEdit.Font, FontStyle.Regular | FontStyle.Underline);
        }
        #endregion Внутренние методы класса.

        #region Внешние методы класса.

	    /// <summary>
	    /// Обновить список важных документов.
	    /// </summary>
	    public void RefreshImportantDocuments()
	    {
	        refreshImportantDocuments();
	    }
        #endregion Внешние методы класса.
    }
}