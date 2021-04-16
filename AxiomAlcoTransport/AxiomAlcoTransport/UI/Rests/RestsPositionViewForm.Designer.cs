namespace Axiom.AlcoTransport
{
    partial class RestsPositionViewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RestsPositionViewForm));
            this.Close_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Form16_imageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.Diff_pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.Document_BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Document_gridControl = new DevExpress.XtraGrid.GridControl();
            this.Document_gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPositionRestsDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFormARegId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.WordWrap_repositoryItemMemoEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colFormBRegId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShortName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFullName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAlcoCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCapacity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductVCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProducer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colImporter = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Status_repositoryItemImageComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.Print_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Find_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Group_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Filter_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Columns_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Refresh_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.FindInWaybill_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.MakeBCRestRequest_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.Form16_imageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Diff_pictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Document_BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Document_gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Document_gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WordWrap_repositoryItemMemoEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Status_repositoryItemImageComboBox)).BeginInit();
            this.SuspendLayout();
            // 
            // Close_simpleButton
            // 
            this.Close_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Close_simpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close_simpleButton.ImageOptions.ImageIndex = 6;
            this.Close_simpleButton.ImageOptions.ImageList = this.Form16_imageCollection;
            this.Close_simpleButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftTop;
            this.Close_simpleButton.Location = new System.Drawing.Point(974, 610);
            this.Close_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Close_simpleButton.Name = "Close_simpleButton";
            this.Close_simpleButton.Size = new System.Drawing.Size(149, 28);
            this.Close_simpleButton.TabIndex = 8;
            this.Close_simpleButton.Text = "Закрыть";
            this.Close_simpleButton.Click += new System.EventHandler(this.Close_simpleButton_Click);
            // 
            // Form16_imageCollection
            // 
            this.Form16_imageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("Form16_imageCollection.ImageStream")));
            this.Form16_imageCollection.Images.SetKeyName(0, "document_view.png");
            this.Form16_imageCollection.Images.SetKeyName(1, "funnel_new.png");
            this.Form16_imageCollection.Images.SetKeyName(2, "magic-wand.png");
            this.Form16_imageCollection.Images.SetKeyName(3, "column_preferences.png");
            this.Form16_imageCollection.Images.SetKeyName(4, "refresh.png");
            this.Form16_imageCollection.Images.SetKeyName(5, "find_text.png");
            this.Form16_imageCollection.Images.SetKeyName(6, "delete2.png");
            this.Form16_imageCollection.Images.SetKeyName(7, "document_find.png");
            // 
            // Diff_pictureEdit
            // 
            this.Diff_pictureEdit.EditValue = ((object)(resources.GetObject("Diff_pictureEdit.EditValue")));
            this.Diff_pictureEdit.Location = new System.Drawing.Point(14, 15);
            this.Diff_pictureEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Diff_pictureEdit.Name = "Diff_pictureEdit";
            this.Diff_pictureEdit.Properties.AllowFocused = false;
            this.Diff_pictureEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Diff_pictureEdit.Properties.Appearance.Options.UseBackColor = true;
            this.Diff_pictureEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Diff_pictureEdit.Properties.ReadOnly = true;
            this.Diff_pictureEdit.Properties.ShowMenu = false;
            this.Diff_pictureEdit.Size = new System.Drawing.Size(69, 76);
            this.Diff_pictureEdit.TabIndex = 4;
            // 
            // Document_BindingSource
            // 
            this.Document_BindingSource.DataSource = typeof(Axiom.AlcoTransport.RestsPosition);
            // 
            // Document_gridControl
            // 
            this.Document_gridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Document_gridControl.DataSource = this.Document_BindingSource;
            this.Document_gridControl.EmbeddedNavigator.Buttons.Append.Enabled = false;
            this.Document_gridControl.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.Document_gridControl.EmbeddedNavigator.Buttons.CancelEdit.Enabled = false;
            this.Document_gridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.Document_gridControl.EmbeddedNavigator.Buttons.Edit.Enabled = false;
            this.Document_gridControl.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.Document_gridControl.EmbeddedNavigator.Buttons.EndEdit.Enabled = false;
            this.Document_gridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.Document_gridControl.EmbeddedNavigator.Buttons.NextPage.Enabled = false;
            this.Document_gridControl.EmbeddedNavigator.Buttons.NextPage.Visible = false;
            this.Document_gridControl.EmbeddedNavigator.Buttons.PrevPage.Enabled = false;
            this.Document_gridControl.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
            this.Document_gridControl.EmbeddedNavigator.Buttons.Remove.Enabled = false;
            this.Document_gridControl.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.Document_gridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Document_gridControl.EmbeddedNavigator.ShowToolTips = false;
            this.Document_gridControl.EmbeddedNavigator.TextStringFormat = "Запись {0} из {1}";
            this.Document_gridControl.Location = new System.Drawing.Point(90, 50);
            this.Document_gridControl.MainView = this.Document_gridView;
            this.Document_gridControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Document_gridControl.Name = "Document_gridControl";
            this.Document_gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.Status_repositoryItemImageComboBox,
            this.WordWrap_repositoryItemMemoEdit});
            this.Document_gridControl.Size = new System.Drawing.Size(1034, 529);
            this.Document_gridControl.TabIndex = 0;
            this.Document_gridControl.UseEmbeddedNavigator = true;
            this.Document_gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Document_gridView});
            // 
            // Document_gridView
            // 
            this.Document_gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colPositionRestsDate,
            this.colQuantity,
            this.colFormARegId,
            this.colFormBRegId,
            this.colTitle,
            this.colShortName,
            this.colFullName,
            this.colAlcoCode,
            this.colCapacity,
            this.colVolume,
            this.colProductVCode,
            this.colProducer,
            this.colImporter});
            this.Document_gridView.GridControl = this.Document_gridControl;
            this.Document_gridView.GroupCount = 1;
            this.Document_gridView.Name = "Document_gridView";
            this.Document_gridView.OptionsBehavior.ReadOnly = true;
            this.Document_gridView.OptionsLayout.StoreAllOptions = true;
            this.Document_gridView.OptionsPrint.ExpandAllGroups = false;
            this.Document_gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.Document_gridView.OptionsView.EnableAppearanceEvenRow = true;
            this.Document_gridView.OptionsView.RowAutoHeight = true;
            this.Document_gridView.OptionsView.ShowFooter = true;
            this.Document_gridView.OptionsView.ShowGroupPanel = false;
            this.Document_gridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colPositionRestsDate, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colId, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colId
            // 
            this.colId.AppearanceCell.Options.UseTextOptions = true;
            this.colId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colId.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colId.AppearanceHeader.Options.UseTextOptions = true;
            this.colId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            this.colId.OptionsColumn.ReadOnly = true;
            this.colId.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Id", "{0}")});
            this.colId.Visible = true;
            this.colId.VisibleIndex = 0;
            this.colId.Width = 82;
            // 
            // colPositionRestsDate
            // 
            this.colPositionRestsDate.AppearanceCell.Options.UseTextOptions = true;
            this.colPositionRestsDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPositionRestsDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPositionRestsDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colPositionRestsDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPositionRestsDate.FieldName = "PositionRestsDate";
            this.colPositionRestsDate.Name = "colPositionRestsDate";
            this.colPositionRestsDate.OptionsColumn.ReadOnly = true;
            this.colPositionRestsDate.Visible = true;
            this.colPositionRestsDate.VisibleIndex = 1;
            // 
            // colQuantity
            // 
            this.colQuantity.AppearanceCell.Options.UseTextOptions = true;
            this.colQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQuantity.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colQuantity.AppearanceHeader.Options.UseTextOptions = true;
            this.colQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.OptionsColumn.ReadOnly = true;
            this.colQuantity.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Quantity", "Всего: {0:n2}")});
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 2;
            this.colQuantity.Width = 120;
            // 
            // colFormARegId
            // 
            this.colFormARegId.AppearanceCell.Options.UseTextOptions = true;
            this.colFormARegId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFormARegId.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFormARegId.AppearanceHeader.Options.UseTextOptions = true;
            this.colFormARegId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFormARegId.ColumnEdit = this.WordWrap_repositoryItemMemoEdit;
            this.colFormARegId.FieldName = "FormARegId";
            this.colFormARegId.Name = "colFormARegId";
            this.colFormARegId.OptionsColumn.ReadOnly = true;
            this.colFormARegId.Width = 53;
            // 
            // WordWrap_repositoryItemMemoEdit
            // 
            this.WordWrap_repositoryItemMemoEdit.Name = "WordWrap_repositoryItemMemoEdit";
            // 
            // colFormBRegId
            // 
            this.colFormBRegId.AppearanceCell.Options.UseTextOptions = true;
            this.colFormBRegId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFormBRegId.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFormBRegId.AppearanceHeader.Options.UseTextOptions = true;
            this.colFormBRegId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFormBRegId.ColumnEdit = this.WordWrap_repositoryItemMemoEdit;
            this.colFormBRegId.FieldName = "FormBRegId";
            this.colFormBRegId.Name = "colFormBRegId";
            this.colFormBRegId.OptionsColumn.ReadOnly = true;
            this.colFormBRegId.Width = 54;
            // 
            // colTitle
            // 
            this.colTitle.AppearanceCell.Options.UseTextOptions = true;
            this.colTitle.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colTitle.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTitle.AppearanceHeader.Options.UseTextOptions = true;
            this.colTitle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTitle.ColumnEdit = this.WordWrap_repositoryItemMemoEdit;
            this.colTitle.FieldName = "Title";
            this.colTitle.Name = "colTitle";
            this.colTitle.OptionsColumn.ReadOnly = true;
            this.colTitle.Visible = true;
            this.colTitle.VisibleIndex = 1;
            this.colTitle.Width = 187;
            // 
            // colShortName
            // 
            this.colShortName.AppearanceCell.Options.UseTextOptions = true;
            this.colShortName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colShortName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colShortName.AppearanceHeader.Options.UseTextOptions = true;
            this.colShortName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colShortName.ColumnEdit = this.WordWrap_repositoryItemMemoEdit;
            this.colShortName.FieldName = "ShortName";
            this.colShortName.Name = "colShortName";
            this.colShortName.OptionsColumn.ReadOnly = true;
            // 
            // colFullName
            // 
            this.colFullName.AppearanceCell.Options.UseTextOptions = true;
            this.colFullName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colFullName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFullName.AppearanceHeader.Options.UseTextOptions = true;
            this.colFullName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFullName.ColumnEdit = this.WordWrap_repositoryItemMemoEdit;
            this.colFullName.FieldName = "FullName";
            this.colFullName.Name = "colFullName";
            this.colFullName.OptionsColumn.ReadOnly = true;
            // 
            // colAlcoCode
            // 
            this.colAlcoCode.AppearanceCell.Options.UseTextOptions = true;
            this.colAlcoCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAlcoCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colAlcoCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colAlcoCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAlcoCode.ColumnEdit = this.WordWrap_repositoryItemMemoEdit;
            this.colAlcoCode.FieldName = "AlcoCode";
            this.colAlcoCode.Name = "colAlcoCode";
            this.colAlcoCode.OptionsColumn.ReadOnly = true;
            this.colAlcoCode.Width = 68;
            // 
            // colCapacity
            // 
            this.colCapacity.AppearanceCell.Options.UseTextOptions = true;
            this.colCapacity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCapacity.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colCapacity.AppearanceHeader.Options.UseTextOptions = true;
            this.colCapacity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCapacity.FieldName = "Capacity";
            this.colCapacity.Name = "colCapacity";
            this.colCapacity.OptionsColumn.ReadOnly = true;
            this.colCapacity.Visible = true;
            this.colCapacity.VisibleIndex = 3;
            this.colCapacity.Width = 74;
            // 
            // colVolume
            // 
            this.colVolume.AppearanceCell.Options.UseTextOptions = true;
            this.colVolume.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colVolume.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colVolume.AppearanceHeader.Options.UseTextOptions = true;
            this.colVolume.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colVolume.FieldName = "Volume";
            this.colVolume.Name = "colVolume";
            this.colVolume.OptionsColumn.ReadOnly = true;
            this.colVolume.Visible = true;
            this.colVolume.VisibleIndex = 4;
            this.colVolume.Width = 71;
            // 
            // colProductVCode
            // 
            this.colProductVCode.AppearanceCell.Options.UseTextOptions = true;
            this.colProductVCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductVCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProductVCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductVCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductVCode.FieldName = "ProductVCode";
            this.colProductVCode.Name = "colProductVCode";
            this.colProductVCode.OptionsColumn.ReadOnly = true;
            this.colProductVCode.Width = 62;
            // 
            // colProducer
            // 
            this.colProducer.AppearanceCell.Options.UseTextOptions = true;
            this.colProducer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colProducer.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProducer.AppearanceHeader.Options.UseTextOptions = true;
            this.colProducer.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProducer.ColumnEdit = this.WordWrap_repositoryItemMemoEdit;
            this.colProducer.FieldName = "Producer";
            this.colProducer.Name = "colProducer";
            this.colProducer.OptionsColumn.ReadOnly = true;
            this.colProducer.Visible = true;
            this.colProducer.VisibleIndex = 5;
            this.colProducer.Width = 103;
            // 
            // colImporter
            // 
            this.colImporter.AppearanceCell.Options.UseTextOptions = true;
            this.colImporter.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.colImporter.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colImporter.AppearanceHeader.Options.UseTextOptions = true;
            this.colImporter.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colImporter.ColumnEdit = this.WordWrap_repositoryItemMemoEdit;
            this.colImporter.FieldName = "Importer";
            this.colImporter.Name = "colImporter";
            this.colImporter.OptionsColumn.ReadOnly = true;
            this.colImporter.Visible = true;
            this.colImporter.VisibleIndex = 6;
            this.colImporter.Width = 124;
            // 
            // Status_repositoryItemImageComboBox
            // 
            this.Status_repositoryItemImageComboBox.AutoHeight = false;
            this.Status_repositoryItemImageComboBox.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Status_repositoryItemImageComboBox.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Status_repositoryItemImageComboBox.HotTrackItems = false;
            this.Status_repositoryItemImageComboBox.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Загружена частично", 101, 3),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Новая", 102, 2),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Подтверждена", 103, 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Отвергнута", 104, 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Акт расхождений", 105, 4)});
            this.Status_repositoryItemImageComboBox.Name = "Status_repositoryItemImageComboBox";
            this.Status_repositoryItemImageComboBox.ReadOnly = true;
            this.Status_repositoryItemImageComboBox.UseCtrlScroll = false;
            // 
            // Print_simpleButton
            // 
            this.Print_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Print_simpleButton.ImageOptions.ImageIndex = 0;
            this.Print_simpleButton.ImageOptions.ImageList = this.Form16_imageCollection;
            this.Print_simpleButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftTop;
            this.Print_simpleButton.Location = new System.Drawing.Point(831, 15);
            this.Print_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Print_simpleButton.Name = "Print_simpleButton";
            this.Print_simpleButton.Size = new System.Drawing.Size(112, 28);
            this.Print_simpleButton.TabIndex = 2;
            this.Print_simpleButton.Text = "Печать";
            this.Print_simpleButton.ToolTip = "Открыть диалог предварительного просмотра документа перед печатью.";
            this.Print_simpleButton.ToolTipTitle = "Печать";
            this.Print_simpleButton.Click += new System.EventHandler(this.Print_simpleButton_Click);
            // 
            // Find_simpleButton
            // 
            this.Find_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Find_simpleButton.ImageOptions.ImageIndex = 5;
            this.Find_simpleButton.ImageOptions.ImageList = this.Form16_imageCollection;
            this.Find_simpleButton.Location = new System.Drawing.Point(950, 15);
            this.Find_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Find_simpleButton.Name = "Find_simpleButton";
            this.Find_simpleButton.Size = new System.Drawing.Size(29, 28);
            this.Find_simpleButton.TabIndex = 3;
            this.Find_simpleButton.ToolTip = "Показать или скрыть панель поиска.";
            this.Find_simpleButton.ToolTipTitle = "Искать в списке";
            this.Find_simpleButton.Click += new System.EventHandler(this.Find_simpleButton_Click);
            // 
            // Group_simpleButton
            // 
            this.Group_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Group_simpleButton.ImageOptions.ImageIndex = 2;
            this.Group_simpleButton.ImageOptions.ImageList = this.Form16_imageCollection;
            this.Group_simpleButton.Location = new System.Drawing.Point(986, 15);
            this.Group_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Group_simpleButton.Name = "Group_simpleButton";
            this.Group_simpleButton.Size = new System.Drawing.Size(29, 28);
            this.Group_simpleButton.TabIndex = 4;
            this.Group_simpleButton.ToolTip = "Показать или скрыть панель группировки.";
            this.Group_simpleButton.ToolTipTitle = "Панель группировки";
            this.Group_simpleButton.Click += new System.EventHandler(this.Group_simpleButton_Click);
            // 
            // Filter_simpleButton
            // 
            this.Filter_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Filter_simpleButton.ImageOptions.ImageIndex = 1;
            this.Filter_simpleButton.ImageOptions.ImageList = this.Form16_imageCollection;
            this.Filter_simpleButton.Location = new System.Drawing.Point(1022, 15);
            this.Filter_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Filter_simpleButton.Name = "Filter_simpleButton";
            this.Filter_simpleButton.Size = new System.Drawing.Size(29, 28);
            this.Filter_simpleButton.TabIndex = 5;
            this.Filter_simpleButton.ToolTip = "Показать конструктор фильтра.";
            this.Filter_simpleButton.ToolTipTitle = "Конструктор фильтр";
            this.Filter_simpleButton.Click += new System.EventHandler(this.Filter_simpleButton_Click);
            // 
            // Columns_simpleButton
            // 
            this.Columns_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Columns_simpleButton.ImageOptions.ImageIndex = 3;
            this.Columns_simpleButton.ImageOptions.ImageList = this.Form16_imageCollection;
            this.Columns_simpleButton.Location = new System.Drawing.Point(1058, 15);
            this.Columns_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Columns_simpleButton.Name = "Columns_simpleButton";
            this.Columns_simpleButton.Size = new System.Drawing.Size(29, 28);
            this.Columns_simpleButton.TabIndex = 6;
            this.Columns_simpleButton.ToolTip = "Открыть диалог выбора колонок.";
            this.Columns_simpleButton.ToolTipTitle = "Выбор колонок";
            this.Columns_simpleButton.Click += new System.EventHandler(this.Columns_simpleButton_Click);
            // 
            // Refresh_simpleButton
            // 
            this.Refresh_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Refresh_simpleButton.ImageOptions.ImageIndex = 4;
            this.Refresh_simpleButton.ImageOptions.ImageList = this.Form16_imageCollection;
            this.Refresh_simpleButton.Location = new System.Drawing.Point(1094, 15);
            this.Refresh_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Refresh_simpleButton.Name = "Refresh_simpleButton";
            this.Refresh_simpleButton.Size = new System.Drawing.Size(29, 28);
            this.Refresh_simpleButton.TabIndex = 7;
            this.Refresh_simpleButton.ToolTip = "Обновить список позиций.";
            this.Refresh_simpleButton.ToolTipTitle = "Обновить";
            this.Refresh_simpleButton.Click += new System.EventHandler(this.Refresh_simpleButton_Click);
            // 
            // labelControl6
            // 
            this.labelControl6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl6.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl6.Appearance.Options.UseFont = true;
            this.labelControl6.Appearance.Options.UseTextOptions = true;
            this.labelControl6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl6.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl6.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl6.LineVisible = true;
            this.labelControl6.Location = new System.Drawing.Point(90, 587);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.ShowToolTips = false;
            this.labelControl6.Size = new System.Drawing.Size(1034, 16);
            this.labelControl6.TabIndex = 8;
            // 
            // FindInWaybill_simpleButton
            // 
            this.FindInWaybill_simpleButton.ImageOptions.ImageIndex = 7;
            this.FindInWaybill_simpleButton.ImageOptions.ImageList = this.Form16_imageCollection;
            this.FindInWaybill_simpleButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftTop;
            this.FindInWaybill_simpleButton.Location = new System.Drawing.Point(90, 14);
            this.FindInWaybill_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FindInWaybill_simpleButton.Name = "FindInWaybill_simpleButton";
            this.FindInWaybill_simpleButton.Size = new System.Drawing.Size(189, 28);
            this.FindInWaybill_simpleButton.TabIndex = 1;
            this.FindInWaybill_simpleButton.Text = "Найти накладную";
            this.FindInWaybill_simpleButton.ToolTip = "Найти входящую товарно-транспортную накладную по идентификатору справки \'Б\' позиц" +
    "ии остатков.";
            this.FindInWaybill_simpleButton.ToolTipTitle = "Печать";
            this.FindInWaybill_simpleButton.Click += new System.EventHandler(this.FindInWaybill_simpleButton_Click);
            // 
            // MakeBCRestRequest_simpleButton
            // 
            this.MakeBCRestRequest_simpleButton.ImageOptions.ImageList = this.Form16_imageCollection;
            this.MakeBCRestRequest_simpleButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftTop;
            this.MakeBCRestRequest_simpleButton.Location = new System.Drawing.Point(299, 14);
            this.MakeBCRestRequest_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MakeBCRestRequest_simpleButton.Name = "MakeBCRestRequest_simpleButton";
            this.MakeBCRestRequest_simpleButton.Size = new System.Drawing.Size(275, 28);
            this.MakeBCRestRequest_simpleButton.TabIndex = 9;
            this.MakeBCRestRequest_simpleButton.Text = "Создать запрос на остаки по штрихкодам";
            this.MakeBCRestRequest_simpleButton.ToolTip = "Найти входящую товарно-транспортную накладную по идентификатору справки \'Б\' позиц" +
    "ии остатков.";
            this.MakeBCRestRequest_simpleButton.ToolTipTitle = "Печать";
            this.MakeBCRestRequest_simpleButton.Click += new System.EventHandler(this.makeBcRestRequest_Click);
            // 
            // RestsPositionViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Close_simpleButton;
            this.ClientSize = new System.Drawing.Size(1143, 654);
            this.Controls.Add(this.MakeBCRestRequest_simpleButton);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.Find_simpleButton);
            this.Controls.Add(this.Group_simpleButton);
            this.Controls.Add(this.Filter_simpleButton);
            this.Controls.Add(this.Columns_simpleButton);
            this.Controls.Add(this.Refresh_simpleButton);
            this.Controls.Add(this.FindInWaybill_simpleButton);
            this.Controls.Add(this.Print_simpleButton);
            this.Controls.Add(this.Document_gridControl);
            this.Controls.Add(this.Close_simpleButton);
            this.Controls.Add(this.Diff_pictureEdit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 540);
            this.Name = "RestsPositionViewForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Список позиций документа-остатков ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RestsPositionViewForm_FormClosed);
            this.Load += new System.EventHandler(this.RestsPositionViewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Form16_imageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Diff_pictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Document_BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Document_gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Document_gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WordWrap_repositoryItemMemoEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Status_repositoryItemImageComboBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton Close_simpleButton;
        private DevExpress.XtraEditors.PictureEdit Diff_pictureEdit;
        private System.Windows.Forms.BindingSource Document_BindingSource;
        private DevExpress.XtraGrid.GridControl Document_gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView Document_gridView;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox Status_repositoryItemImageComboBox;
        private DevExpress.Utils.ImageCollection Form16_imageCollection;
        private DevExpress.XtraEditors.SimpleButton Print_simpleButton;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit WordWrap_repositoryItemMemoEdit;
        private DevExpress.XtraEditors.SimpleButton Find_simpleButton;
        private DevExpress.XtraEditors.SimpleButton Group_simpleButton;
        private DevExpress.XtraEditors.SimpleButton Filter_simpleButton;
        private DevExpress.XtraEditors.SimpleButton Columns_simpleButton;
        private DevExpress.XtraEditors.SimpleButton Refresh_simpleButton;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colPositionRestsDate;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colFormARegId;
        private DevExpress.XtraGrid.Columns.GridColumn colFormBRegId;
        private DevExpress.XtraGrid.Columns.GridColumn colTitle;
        private DevExpress.XtraGrid.Columns.GridColumn colShortName;
        private DevExpress.XtraGrid.Columns.GridColumn colFullName;
        private DevExpress.XtraGrid.Columns.GridColumn colAlcoCode;
        private DevExpress.XtraGrid.Columns.GridColumn colCapacity;
        private DevExpress.XtraGrid.Columns.GridColumn colVolume;
        private DevExpress.XtraGrid.Columns.GridColumn colProductVCode;
        private DevExpress.XtraGrid.Columns.GridColumn colProducer;
        private DevExpress.XtraGrid.Columns.GridColumn colImporter;
        private DevExpress.XtraEditors.SimpleButton FindInWaybill_simpleButton;
        private DevExpress.XtraEditors.SimpleButton MakeBCRestRequest_simpleButton;
    }
}