namespace Axiom.AlcoTransport
{
    partial class ActDifferenceForm
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActDifferenceForm));
            this.boxInfo_gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBoxNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Document_gridControl = new DevExpress.XtraGrid.GridControl();
            this.Document_BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Document_gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIdentity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInformBRegId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.WordWrap_repositoryItemMemoEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colShortName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFullName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductVCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAlcoCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCapacity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProducer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRealQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Decimal_repositoryItemSpinEdit = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colFormARegId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFormBRegId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTotalRealPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barcodes_gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBarcode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRealBarcode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Cancel_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Act16_imageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.Diff_pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.Send_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Caption_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.Number_textEdit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.Comment_textEdit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.Guide_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.Find_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Group_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Filter_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Columns_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Refresh_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Print_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.positionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.barcodePositionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.boxInfo_gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Document_gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Document_BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Document_gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WordWrap_repositoryItemMemoEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Decimal_repositoryItemSpinEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barcodes_gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Act16_imageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Diff_pictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Number_textEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Comment_textEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.positionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barcodePositionBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // boxInfo_gridView
            // 
            this.boxInfo_gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBoxNumber});
            this.boxInfo_gridView.GridControl = this.Document_gridControl;
            this.boxInfo_gridView.Name = "boxInfo_gridView";
            this.boxInfo_gridView.OptionsDetail.ShowDetailTabs = false;
            this.boxInfo_gridView.OptionsView.ShowGroupPanel = false;
            // 
            // colBoxNumber
            // 
            this.colBoxNumber.FieldName = "BoxNumber";
            this.colBoxNumber.Name = "colBoxNumber";
            this.colBoxNumber.OptionsColumn.ReadOnly = true;
            this.colBoxNumber.Visible = true;
            this.colBoxNumber.VisibleIndex = 0;
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
            this.Document_gridControl.EmbeddedNavigator.ShowToolTips = false;
            this.Document_gridControl.EmbeddedNavigator.TextStringFormat = "Запись {0} из {1}";
            gridLevelNode1.LevelTemplate = this.boxInfo_gridView;
            gridLevelNode2.LevelTemplate = this.barcodes_gridView;
            gridLevelNode2.RelationName = "AmcList";
            gridLevelNode1.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            gridLevelNode1.RelationName = "BoxInfos";
            this.Document_gridControl.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.Document_gridControl.Location = new System.Drawing.Point(78, 162);
            this.Document_gridControl.MainView = this.Document_gridView;
            this.Document_gridControl.Name = "Document_gridControl";
            this.Document_gridControl.Size = new System.Drawing.Size(575, 227);
            this.Document_gridControl.TabIndex = 8;
            this.Document_gridControl.UseEmbeddedNavigator = true;
            this.Document_gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Document_gridView,
            this.barcodes_gridView,
            this.boxInfo_gridView});
            // 
            // Document_BindingSource
            // 
            this.Document_BindingSource.DataSource = typeof(Axiom.AlcoTransport.Position);
            // 
            // Document_gridView
            // 
            this.Document_gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIdentity,
            this.colInformBRegId,
            this.colTitle,
            this.colShortName,
            this.colFullName,
            this.colProductVCode,
            this.colAlcoCode,
            this.colCapacity,
            this.colVolume,
            this.colProducer,
            this.colQuantity,
            this.colRealQuantity,
            this.colFormARegId,
            this.colFormBRegId,
            this.colPrice,
            this.colTotalPrice,
            this.colTotalRealPrice});
            this.Document_gridView.GridControl = this.Document_gridControl;
            this.Document_gridView.Name = "Document_gridView";
            this.Document_gridView.OptionsDetail.ShowDetailTabs = false;
            this.Document_gridView.OptionsLayout.StoreAllOptions = true;
            this.Document_gridView.OptionsPrint.ExpandAllGroups = false;
            this.Document_gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.Document_gridView.OptionsView.EnableAppearanceEvenRow = true;
            this.Document_gridView.OptionsView.RowAutoHeight = true;
            this.Document_gridView.OptionsView.ShowFooter = true;
            this.Document_gridView.OptionsView.ShowGroupPanel = false;
            this.Document_gridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.Document_gridView_CellValueChanged);
            // 
            // colIdentity
            // 
            this.colIdentity.AppearanceCell.Options.UseTextOptions = true;
            this.colIdentity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIdentity.AppearanceHeader.Options.UseTextOptions = true;
            this.colIdentity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIdentity.FieldName = "Identity";
            this.colIdentity.Name = "colIdentity";
            this.colIdentity.OptionsColumn.AllowEdit = false;
            this.colIdentity.OptionsColumn.ReadOnly = true;
            this.colIdentity.Visible = true;
            this.colIdentity.VisibleIndex = 0;
            this.colIdentity.Width = 46;
            // 
            // colInformBRegId
            // 
            this.colInformBRegId.AppearanceCell.Options.UseTextOptions = true;
            this.colInformBRegId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colInformBRegId.AppearanceHeader.Options.UseTextOptions = true;
            this.colInformBRegId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colInformBRegId.FieldName = "InformBRegId";
            this.colInformBRegId.Name = "colInformBRegId";
            this.colInformBRegId.OptionsColumn.AllowEdit = false;
            this.colInformBRegId.OptionsColumn.ReadOnly = true;
            // 
            // colTitle
            // 
            this.colTitle.AppearanceCell.Options.UseTextOptions = true;
            this.colTitle.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTitle.AppearanceHeader.Options.UseTextOptions = true;
            this.colTitle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTitle.ColumnEdit = this.WordWrap_repositoryItemMemoEdit;
            this.colTitle.FieldName = "Title";
            this.colTitle.Name = "colTitle";
            this.colTitle.OptionsColumn.AllowEdit = false;
            this.colTitle.OptionsColumn.ReadOnly = true;
            this.colTitle.Visible = true;
            this.colTitle.VisibleIndex = 1;
            this.colTitle.Width = 135;
            // 
            // WordWrap_repositoryItemMemoEdit
            // 
            this.WordWrap_repositoryItemMemoEdit.Name = "WordWrap_repositoryItemMemoEdit";
            // 
            // colShortName
            // 
            this.colShortName.AppearanceCell.Options.UseTextOptions = true;
            this.colShortName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colShortName.AppearanceHeader.Options.UseTextOptions = true;
            this.colShortName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colShortName.ColumnEdit = this.WordWrap_repositoryItemMemoEdit;
            this.colShortName.FieldName = "ShortName";
            this.colShortName.Name = "colShortName";
            this.colShortName.OptionsColumn.AllowEdit = false;
            this.colShortName.OptionsColumn.ReadOnly = true;
            // 
            // colFullName
            // 
            this.colFullName.AppearanceCell.Options.UseTextOptions = true;
            this.colFullName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFullName.AppearanceHeader.Options.UseTextOptions = true;
            this.colFullName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFullName.ColumnEdit = this.WordWrap_repositoryItemMemoEdit;
            this.colFullName.FieldName = "FullName";
            this.colFullName.Name = "colFullName";
            this.colFullName.OptionsColumn.AllowEdit = false;
            this.colFullName.OptionsColumn.ReadOnly = true;
            // 
            // colProductVCode
            // 
            this.colProductVCode.AppearanceCell.Options.UseTextOptions = true;
            this.colProductVCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductVCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductVCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductVCode.FieldName = "ProductVCode";
            this.colProductVCode.Name = "colProductVCode";
            this.colProductVCode.OptionsColumn.AllowEdit = false;
            this.colProductVCode.OptionsColumn.ReadOnly = true;
            this.colProductVCode.Visible = true;
            this.colProductVCode.VisibleIndex = 2;
            this.colProductVCode.Width = 60;
            // 
            // colAlcoCode
            // 
            this.colAlcoCode.AppearanceCell.Options.UseTextOptions = true;
            this.colAlcoCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAlcoCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colAlcoCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAlcoCode.FieldName = "AlcoCode";
            this.colAlcoCode.Name = "colAlcoCode";
            this.colAlcoCode.OptionsColumn.AllowEdit = false;
            this.colAlcoCode.OptionsColumn.ReadOnly = true;
            // 
            // colCapacity
            // 
            this.colCapacity.AppearanceCell.Options.UseTextOptions = true;
            this.colCapacity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCapacity.AppearanceHeader.Options.UseTextOptions = true;
            this.colCapacity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCapacity.FieldName = "Capacity";
            this.colCapacity.Name = "colCapacity";
            this.colCapacity.OptionsColumn.AllowEdit = false;
            this.colCapacity.OptionsColumn.ReadOnly = true;
            this.colCapacity.Visible = true;
            this.colCapacity.VisibleIndex = 3;
            this.colCapacity.Width = 57;
            // 
            // colVolume
            // 
            this.colVolume.AppearanceCell.Options.UseTextOptions = true;
            this.colVolume.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colVolume.AppearanceHeader.Options.UseTextOptions = true;
            this.colVolume.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colVolume.FieldName = "Volume";
            this.colVolume.Name = "colVolume";
            this.colVolume.OptionsColumn.AllowEdit = false;
            this.colVolume.OptionsColumn.ReadOnly = true;
            this.colVolume.Visible = true;
            this.colVolume.VisibleIndex = 4;
            this.colVolume.Width = 58;
            // 
            // colProducer
            // 
            this.colProducer.AppearanceCell.Options.UseTextOptions = true;
            this.colProducer.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProducer.AppearanceHeader.Options.UseTextOptions = true;
            this.colProducer.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProducer.ColumnEdit = this.WordWrap_repositoryItemMemoEdit;
            this.colProducer.FieldName = "Producer";
            this.colProducer.Name = "colProducer";
            this.colProducer.OptionsColumn.AllowEdit = false;
            this.colProducer.OptionsColumn.ReadOnly = true;
            this.colProducer.Visible = true;
            this.colProducer.VisibleIndex = 5;
            this.colProducer.Width = 109;
            // 
            // colQuantity
            // 
            this.colQuantity.AppearanceCell.Options.UseTextOptions = true;
            this.colQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQuantity.AppearanceHeader.Options.UseTextOptions = true;
            this.colQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQuantity.DisplayFormat.FormatString = "N3";
            this.colQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.OptionsColumn.AllowEdit = false;
            this.colQuantity.OptionsColumn.ReadOnly = true;
            this.colQuantity.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Quantity", "Всего: {0:n2}")});
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 6;
            this.colQuantity.Width = 81;
            // 
            // colRealQuantity
            // 
            this.colRealQuantity.AppearanceCell.Options.UseTextOptions = true;
            this.colRealQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colRealQuantity.AppearanceHeader.Options.UseTextOptions = true;
            this.colRealQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colRealQuantity.ColumnEdit = this.Decimal_repositoryItemSpinEdit;
            this.colRealQuantity.DisplayFormat.FormatString = "N3";
            this.colRealQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colRealQuantity.FieldName = "RealQuantity";
            this.colRealQuantity.Name = "colRealQuantity";
            this.colRealQuantity.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "RealQuantity", "Всего: {0:n2}")});
            this.colRealQuantity.Visible = true;
            this.colRealQuantity.VisibleIndex = 7;
            this.colRealQuantity.Width = 87;
            // 
            // Decimal_repositoryItemSpinEdit
            // 
            this.Decimal_repositoryItemSpinEdit.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.Decimal_repositoryItemSpinEdit.AutoHeight = false;
            this.Decimal_repositoryItemSpinEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Decimal_repositoryItemSpinEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.Decimal_repositoryItemSpinEdit.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.Decimal_repositoryItemSpinEdit.Name = "Decimal_repositoryItemSpinEdit";
            // 
            // colFormARegId
            // 
            this.colFormARegId.AppearanceCell.Options.UseTextOptions = true;
            this.colFormARegId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFormARegId.AppearanceHeader.Options.UseTextOptions = true;
            this.colFormARegId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFormARegId.FieldName = "FormARegId";
            this.colFormARegId.Name = "colFormARegId";
            this.colFormARegId.OptionsColumn.AllowEdit = false;
            this.colFormARegId.OptionsColumn.ReadOnly = true;
            // 
            // colFormBRegId
            // 
            this.colFormBRegId.AppearanceCell.Options.UseTextOptions = true;
            this.colFormBRegId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFormBRegId.AppearanceHeader.Options.UseTextOptions = true;
            this.colFormBRegId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFormBRegId.FieldName = "FormBRegId";
            this.colFormBRegId.Name = "colFormBRegId";
            this.colFormBRegId.OptionsColumn.AllowEdit = false;
            this.colFormBRegId.OptionsColumn.ReadOnly = true;
            // 
            // colPrice
            // 
            this.colPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPrice.DisplayFormat.FormatString = "N2";
            this.colPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPrice.FieldName = "Price";
            this.colPrice.Name = "colPrice";
            this.colPrice.OptionsColumn.AllowEdit = false;
            this.colPrice.OptionsColumn.ReadOnly = true;
            this.colPrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Price", "Всего: {0:n2}")});
            this.colPrice.Width = 77;
            // 
            // colTotalPrice
            // 
            this.colTotalPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotalPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotalPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotalPrice.DisplayFormat.FormatString = "N2";
            this.colTotalPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalPrice.FieldName = "TotalPrice";
            this.colTotalPrice.Name = "colTotalPrice";
            this.colTotalPrice.OptionsColumn.AllowEdit = false;
            this.colTotalPrice.OptionsColumn.ReadOnly = true;
            this.colTotalPrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalPrice", "Всего: {0:n2}")});
            // 
            // colTotalRealPrice
            // 
            this.colTotalRealPrice.AppearanceCell.Options.UseTextOptions = true;
            this.colTotalRealPrice.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotalRealPrice.AppearanceHeader.Options.UseTextOptions = true;
            this.colTotalRealPrice.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTotalRealPrice.DisplayFormat.FormatString = "N2";
            this.colTotalRealPrice.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTotalRealPrice.FieldName = "TotalRealPrice";
            this.colTotalRealPrice.Name = "colTotalRealPrice";
            this.colTotalRealPrice.OptionsColumn.AllowEdit = false;
            this.colTotalRealPrice.OptionsColumn.ReadOnly = true;
            this.colTotalRealPrice.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "TotalRealPrice", "Всего: {0:n2}")});
            this.colTotalRealPrice.Visible = true;
            this.colTotalRealPrice.VisibleIndex = 8;
            this.colTotalRealPrice.Width = 122;
            // 
            // barcodes_gridView
            // 
            this.barcodes_gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBarcode,
            this.colRealBarcode});
            this.barcodes_gridView.GridControl = this.Document_gridControl;
            this.barcodes_gridView.Name = "barcodes_gridView";
            this.barcodes_gridView.OptionsDetail.ShowDetailTabs = false;
            this.barcodes_gridView.OptionsView.ShowGroupPanel = false;
            this.barcodes_gridView.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.barcodes_gridView_ValidateRow);
            // 
            // colBarcode
            // 
            this.colBarcode.FieldName = "Barcode";
            this.colBarcode.Name = "colBarcode";
            this.colBarcode.OptionsColumn.AllowEdit = false;
            this.colBarcode.OptionsColumn.ReadOnly = true;
            this.colBarcode.Visible = true;
            this.colBarcode.VisibleIndex = 0;
            // 
            // colRealBarcode
            // 
            this.colRealBarcode.FieldName = "RealBarcode";
            this.colRealBarcode.Name = "colRealBarcode";
            this.colRealBarcode.Visible = true;
            this.colRealBarcode.VisibleIndex = 1;
            // 
            // Cancel_simpleButton
            // 
            this.Cancel_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel_simpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_simpleButton.ImageOptions.ImageIndex = 9;
            this.Cancel_simpleButton.ImageOptions.ImageList = this.Act16_imageCollection;
            this.Cancel_simpleButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.Cancel_simpleButton.Location = new System.Drawing.Point(525, 414);
            this.Cancel_simpleButton.Name = "Cancel_simpleButton";
            this.Cancel_simpleButton.Size = new System.Drawing.Size(128, 23);
            this.Cancel_simpleButton.TabIndex = 10;
            this.Cancel_simpleButton.Text = "Отмена";
            this.Cancel_simpleButton.Click += new System.EventHandler(this.Close_simpleButton_Click);
            // 
            // Act16_imageCollection
            // 
            this.Act16_imageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("Act16_imageCollection.ImageStream")));
            this.Act16_imageCollection.Images.SetKeyName(0, "down_minus.png");
            this.Act16_imageCollection.Images.SetKeyName(1, "up_plus.png");
            this.Act16_imageCollection.Images.SetKeyName(2, "column_preferences.png");
            this.Act16_imageCollection.Images.SetKeyName(3, "funnel_new.png");
            this.Act16_imageCollection.Images.SetKeyName(4, "refresh.png");
            this.Act16_imageCollection.Images.SetKeyName(5, "magic-wand.png");
            this.Act16_imageCollection.Images.SetKeyName(6, "document_view.png");
            this.Act16_imageCollection.Images.SetKeyName(7, "find_text.png");
            this.Act16_imageCollection.Images.SetKeyName(8, "check2.png");
            this.Act16_imageCollection.Images.SetKeyName(9, "delete2.png");
            // 
            // Diff_pictureEdit
            // 
            this.Diff_pictureEdit.EditValue = ((object)(resources.GetObject("Diff_pictureEdit.EditValue")));
            this.Diff_pictureEdit.Location = new System.Drawing.Point(12, 12);
            this.Diff_pictureEdit.Name = "Diff_pictureEdit";
            this.Diff_pictureEdit.Properties.AllowFocused = false;
            this.Diff_pictureEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Diff_pictureEdit.Properties.Appearance.Options.UseBackColor = true;
            this.Diff_pictureEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Diff_pictureEdit.Properties.ReadOnly = true;
            this.Diff_pictureEdit.Properties.ShowMenu = false;
            this.Diff_pictureEdit.Size = new System.Drawing.Size(59, 62);
            this.Diff_pictureEdit.TabIndex = 4;
            // 
            // Send_simpleButton
            // 
            this.Send_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Send_simpleButton.ImageOptions.ImageIndex = 8;
            this.Send_simpleButton.ImageOptions.ImageList = this.Act16_imageCollection;
            this.Send_simpleButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.Send_simpleButton.Location = new System.Drawing.Point(392, 414);
            this.Send_simpleButton.Name = "Send_simpleButton";
            this.Send_simpleButton.Size = new System.Drawing.Size(128, 23);
            this.Send_simpleButton.TabIndex = 9;
            this.Send_simpleButton.Text = "Отправить";
            this.Send_simpleButton.Click += new System.EventHandler(this.Send_simpleButton_Click);
            // 
            // Caption_labelControl
            // 
            this.Caption_labelControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Caption_labelControl.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Caption_labelControl.Appearance.Options.UseFont = true;
            this.Caption_labelControl.Appearance.Options.UseTextOptions = true;
            this.Caption_labelControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.Caption_labelControl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.Caption_labelControl.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.Caption_labelControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.Caption_labelControl.Location = new System.Drawing.Point(78, 13);
            this.Caption_labelControl.Name = "Caption_labelControl";
            this.Caption_labelControl.Size = new System.Drawing.Size(575, 73);
            this.Caption_labelControl.TabIndex = 5;
            this.Caption_labelControl.Text = "Отправить акт по входящей накладной... ";
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl2.LineVisible = true;
            this.labelControl2.Location = new System.Drawing.Point(78, 395);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.ShowToolTips = false;
            this.labelControl2.Size = new System.Drawing.Size(575, 13);
            this.labelControl2.TabIndex = 6;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(78, 93);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(62, 13);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "Номер акта:";
            // 
            // Number_textEdit
            // 
            this.Number_textEdit.Location = new System.Drawing.Point(145, 90);
            this.Number_textEdit.Name = "Number_textEdit";
            this.Number_textEdit.Properties.MaxLength = 50;
            this.Number_textEdit.Size = new System.Drawing.Size(268, 20);
            this.Number_textEdit.TabIndex = 0;
            this.Number_textEdit.EditValueChanged += new System.EventHandler(this.Number_textEdit_EditValueChanged);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(418, 93);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(107, 13);
            this.labelControl3.TabIndex = 7;
            this.labelControl3.Text = "Комментарий к акту:";
            // 
            // Comment_textEdit
            // 
            this.Comment_textEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Comment_textEdit.Location = new System.Drawing.Point(528, 90);
            this.Comment_textEdit.Name = "Comment_textEdit";
            this.Comment_textEdit.Properties.MaxLength = 200;
            this.Comment_textEdit.Size = new System.Drawing.Size(125, 20);
            this.Comment_textEdit.TabIndex = 1;
            this.Comment_textEdit.EditValueChanged += new System.EventHandler(this.Comment_textEdit_EditValueChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Appearance.Options.UseFont = true;
            this.labelControl5.Appearance.Options.UseTextOptions = true;
            this.labelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl5.LineVisible = true;
            this.labelControl5.Location = new System.Drawing.Point(78, 115);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.ShowToolTips = false;
            this.labelControl5.Size = new System.Drawing.Size(575, 13);
            this.labelControl5.TabIndex = 6;
            // 
            // Guide_labelControl
            // 
            this.Guide_labelControl.Appearance.Options.UseTextOptions = true;
            this.Guide_labelControl.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.Guide_labelControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.Guide_labelControl.Location = new System.Drawing.Point(78, 130);
            this.Guide_labelControl.Name = "Guide_labelControl";
            this.Guide_labelControl.Size = new System.Drawing.Size(495, 25);
            this.Guide_labelControl.TabIndex = 9;
            this.Guide_labelControl.Text = "Выберите позицию в накладной и укажите нужное значение в колонке \"Реальное количе" +
    "ство\".";
            // 
            // Find_simpleButton
            // 
            this.Find_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Find_simpleButton.ImageOptions.ImageIndex = 7;
            this.Find_simpleButton.ImageOptions.ImageList = this.Act16_imageCollection;
            this.Find_simpleButton.Location = new System.Drawing.Point(505, 132);
            this.Find_simpleButton.Name = "Find_simpleButton";
            this.Find_simpleButton.Size = new System.Drawing.Size(25, 23);
            this.Find_simpleButton.TabIndex = 3;
            this.Find_simpleButton.ToolTip = "Показать или скрыть панель поиска.";
            this.Find_simpleButton.ToolTipTitle = "Искать в списке";
            this.Find_simpleButton.Click += new System.EventHandler(this.Find_simpleButton_Click);
            // 
            // Group_simpleButton
            // 
            this.Group_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Group_simpleButton.ImageOptions.ImageIndex = 5;
            this.Group_simpleButton.ImageOptions.ImageList = this.Act16_imageCollection;
            this.Group_simpleButton.Location = new System.Drawing.Point(536, 132);
            this.Group_simpleButton.Name = "Group_simpleButton";
            this.Group_simpleButton.Size = new System.Drawing.Size(25, 23);
            this.Group_simpleButton.TabIndex = 4;
            this.Group_simpleButton.ToolTip = "Показать или скрыть панель группировки.";
            this.Group_simpleButton.ToolTipTitle = "Панель группировки";
            this.Group_simpleButton.Click += new System.EventHandler(this.Group_simpleButton_Click);
            // 
            // Filter_simpleButton
            // 
            this.Filter_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Filter_simpleButton.ImageOptions.ImageIndex = 3;
            this.Filter_simpleButton.ImageOptions.ImageList = this.Act16_imageCollection;
            this.Filter_simpleButton.Location = new System.Drawing.Point(567, 132);
            this.Filter_simpleButton.Name = "Filter_simpleButton";
            this.Filter_simpleButton.Size = new System.Drawing.Size(25, 23);
            this.Filter_simpleButton.TabIndex = 5;
            this.Filter_simpleButton.ToolTip = "Показать конструктор фильтра.";
            this.Filter_simpleButton.ToolTipTitle = "Конструктор фильтр";
            this.Filter_simpleButton.Click += new System.EventHandler(this.Filter_simpleButton_Click);
            // 
            // Columns_simpleButton
            // 
            this.Columns_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Columns_simpleButton.ImageOptions.ImageIndex = 2;
            this.Columns_simpleButton.ImageOptions.ImageList = this.Act16_imageCollection;
            this.Columns_simpleButton.Location = new System.Drawing.Point(597, 132);
            this.Columns_simpleButton.Name = "Columns_simpleButton";
            this.Columns_simpleButton.Size = new System.Drawing.Size(25, 23);
            this.Columns_simpleButton.TabIndex = 6;
            this.Columns_simpleButton.ToolTip = "Открыть диалог выбора колонок.";
            this.Columns_simpleButton.ToolTipTitle = "Выбор колонок";
            this.Columns_simpleButton.Click += new System.EventHandler(this.Columns_simpleButton_Click);
            // 
            // Refresh_simpleButton
            // 
            this.Refresh_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Refresh_simpleButton.ImageOptions.ImageIndex = 4;
            this.Refresh_simpleButton.ImageOptions.ImageList = this.Act16_imageCollection;
            this.Refresh_simpleButton.Location = new System.Drawing.Point(628, 132);
            this.Refresh_simpleButton.Name = "Refresh_simpleButton";
            this.Refresh_simpleButton.Size = new System.Drawing.Size(25, 23);
            this.Refresh_simpleButton.TabIndex = 7;
            this.Refresh_simpleButton.ToolTip = "Обновить список позиций.";
            this.Refresh_simpleButton.ToolTipTitle = "Обновить";
            this.Refresh_simpleButton.Click += new System.EventHandler(this.Refresh_simpleButton_Click);
            // 
            // Print_simpleButton
            // 
            this.Print_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Print_simpleButton.ImageOptions.ImageIndex = 6;
            this.Print_simpleButton.ImageOptions.ImageList = this.Act16_imageCollection;
            this.Print_simpleButton.Location = new System.Drawing.Point(473, 132);
            this.Print_simpleButton.Name = "Print_simpleButton";
            this.Print_simpleButton.Size = new System.Drawing.Size(25, 23);
            this.Print_simpleButton.TabIndex = 2;
            this.Print_simpleButton.ToolTip = "Открыть диалог предварительного просмотра документа перед печатью.";
            this.Print_simpleButton.ToolTipTitle = "Печать";
            this.Print_simpleButton.Click += new System.EventHandler(this.Print_simpleButton_Click);
            // 
            // positionBindingSource
            // 
            this.positionBindingSource.DataSource = typeof(Axiom.AlcoTransport.Position);
            // 
            // barcodePositionBindingSource
            // 
            this.barcodePositionBindingSource.DataSource = typeof(Axiom.AlcoTransport.Engine.BarcodeCheck.BarcodePosition);
            // 
            // ActDifferenceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_simpleButton;
            this.ClientSize = new System.Drawing.Size(670, 449);
            this.Controls.Add(this.Find_simpleButton);
            this.Controls.Add(this.Group_simpleButton);
            this.Controls.Add(this.Filter_simpleButton);
            this.Controls.Add(this.Columns_simpleButton);
            this.Controls.Add(this.Refresh_simpleButton);
            this.Controls.Add(this.Print_simpleButton);
            this.Controls.Add(this.Guide_labelControl);
            this.Controls.Add(this.Document_gridControl);
            this.Controls.Add(this.Comment_textEdit);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.Number_textEdit);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.Caption_labelControl);
            this.Controls.Add(this.Send_simpleButton);
            this.Controls.Add(this.Cancel_simpleButton);
            this.Controls.Add(this.Diff_pictureEdit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 488);
            this.Name = "ActDifferenceForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Отправить акт расхождения";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ActDifferenceForm_FormClosed);
            this.Load += new System.EventHandler(this.ActDifferenceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.boxInfo_gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Document_gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Document_BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Document_gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WordWrap_repositoryItemMemoEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Decimal_repositoryItemSpinEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barcodes_gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Act16_imageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Diff_pictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Number_textEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Comment_textEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.positionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barcodePositionBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton Cancel_simpleButton;
        private DevExpress.XtraEditors.PictureEdit Diff_pictureEdit;
        private DevExpress.XtraEditors.SimpleButton Send_simpleButton;
        private DevExpress.XtraEditors.LabelControl Caption_labelControl;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit Number_textEdit;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit Comment_textEdit;
        private System.Windows.Forms.BindingSource Document_BindingSource;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraGrid.GridControl Document_gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView Document_gridView;
        private DevExpress.XtraGrid.Columns.GridColumn colIdentity;
        private DevExpress.XtraGrid.Columns.GridColumn colInformBRegId;
        private DevExpress.XtraGrid.Columns.GridColumn colTitle;
        private DevExpress.XtraGrid.Columns.GridColumn colShortName;
        private DevExpress.XtraGrid.Columns.GridColumn colFullName;
        private DevExpress.XtraGrid.Columns.GridColumn colAlcoCode;
        private DevExpress.XtraGrid.Columns.GridColumn colCapacity;
        private DevExpress.XtraGrid.Columns.GridColumn colVolume;
        private DevExpress.XtraGrid.Columns.GridColumn colProducer;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colRealQuantity;
        private DevExpress.XtraEditors.LabelControl Guide_labelControl;
        private DevExpress.Utils.ImageCollection Act16_imageCollection;
        private DevExpress.XtraGrid.Columns.GridColumn colFormARegId;
        private DevExpress.XtraGrid.Columns.GridColumn colFormBRegId;
        private DevExpress.XtraGrid.Columns.GridColumn colPrice;
        private DevExpress.XtraEditors.SimpleButton Find_simpleButton;
        private DevExpress.XtraEditors.SimpleButton Group_simpleButton;
        private DevExpress.XtraEditors.SimpleButton Filter_simpleButton;
        private DevExpress.XtraEditors.SimpleButton Columns_simpleButton;
        private DevExpress.XtraEditors.SimpleButton Refresh_simpleButton;
        private DevExpress.XtraEditors.SimpleButton Print_simpleButton;
        private DevExpress.XtraGrid.Columns.GridColumn colProductVCode;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colTotalRealPrice;
        private System.Windows.Forms.BindingSource barcodePositionBindingSource;
        private System.Windows.Forms.BindingSource positionBindingSource;
        private DevExpress.XtraGrid.Views.Grid.GridView boxInfo_gridView;
        private DevExpress.XtraGrid.Columns.GridColumn colBoxNumber;
        private DevExpress.XtraGrid.Views.Grid.GridView barcodes_gridView;
        private DevExpress.XtraGrid.Columns.GridColumn colBarcode;
        private DevExpress.XtraGrid.Columns.GridColumn colRealBarcode;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit WordWrap_repositoryItemMemoEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit Decimal_repositoryItemSpinEdit;
    }
}