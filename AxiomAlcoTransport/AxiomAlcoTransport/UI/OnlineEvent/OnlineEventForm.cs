﻿using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars.Ribbon;
using Axiom.AlcoTransport.Watchtower.Configuration;

namespace Axiom.AlcoTransport
{
	public partial class OnlineEventForm : XtraForm
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
		public OnlineEventForm()
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
        public OnlineEventForm(Documents documents): this()
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
        private void OnlineEventForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузки... ");

                Cursor = MdiParent.Cursor = Cursors.WaitCursor;

                loadGridLayot();

                Document_bindingSource.DataSource = documents.OnlineEvents;

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
        private void OnlineEventForm_Activated(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка активации... ");

                Cursor = MdiParent.Cursor = Cursors.WaitCursor;
                MainRibbonControl.SelectedPage = OnlineEvent_ribbonPage;

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
        private void OnlineEventForm_FormClosed(object sender, FormClosedEventArgs e)
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
        #endregion Обработка событий пользовательского интерфейса.

        #region Внутренние методы класса.
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
        #endregion Внутренние методы класса.
    }
}