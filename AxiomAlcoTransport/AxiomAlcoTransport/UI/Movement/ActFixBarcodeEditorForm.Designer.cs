using Axiom.AlcoTransport.Document;

namespace Axiom.AlcoTransport.UI.Movement
{
    partial class ActFixBarcodeEditorForm<T>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActChargeOffEditorForm));
            this.barcodesView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLength = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deleteItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.Position_gridControl = new DevExpress.XtraGrid.GridControl();
            this.Position_bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Position_gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colStatusOutPosition = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Status_repositoryItemImageComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.Editor16_imageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.colIdentity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.WordWrapTo_repositoryItemMemoEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colProducerTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAlcoCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductVCode1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCapacityOut = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVolume = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Decimal_repositoryItemSpinEdit = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colFormBRegId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Close_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Main_xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
            this.Common_xtraTabPage = new DevExpress.XtraTab.XtraTabPage();
            this.Attention_pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.Date_dateEdit = new DevExpress.XtraEditors.DateEdit();
            this.Status_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.Identity_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.Date_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.Status_textEdit = new DevExpress.XtraEditors.TextEdit();
            this.Identity_textEdit = new DevExpress.XtraEditors.TextEdit();
            this.Note_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.Number_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.Note_textEdit = new DevExpress.XtraEditors.TextEdit();
            this.Number_textEdit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl25 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.Content_xtraTabPage = new DevExpress.XtraTab.XtraTabPage();
            this.Content_splitContainerControl = new DevExpress.XtraEditors.SplitContainerControl();
            this.Source_gridControl = new DevExpress.XtraGrid.GridControl();
            this.SourcePosition_bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Source_gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPositionRestsDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.WordWrapRests_repositoryItemMemoEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colTitle1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFormARegId1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFormBRegId1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShortName1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFullName1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAlcoCode1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCapacity1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVolume1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProductVCode2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProducer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colImporter = new DevExpress.XtraGrid.Columns.GridColumn();
            this.APFind_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.APGroup_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.APFilter_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.APColumns_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.APRefresh_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.scanBarcode_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.FindPosition_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.ColumnsPosition_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.PrintPosition_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.RefreshPosition_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.CheckPosition_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Add_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Remove_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Tree_xtraTabPage = new DevExpress.XtraTab.XtraTabPage();
            this.Detail_treeList = new DevExpress.XtraTreeList.TreeList();
            this.Title_treeListColumn = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.Value_treeListColumn = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.WordWrap_repositoryItemMemoEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.ExpandTree_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.CollapseTree_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Print_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.CaptionB_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.Ttn_pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.SaveAndClose_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.barcodesView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deleteItemButtonEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Position_gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Position_bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Position_gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Status_repositoryItemImageComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Editor16_imageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WordWrapTo_repositoryItemMemoEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Decimal_repositoryItemSpinEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Main_xtraTabControl)).BeginInit();
            this.Main_xtraTabControl.SuspendLayout();
            this.Common_xtraTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Attention_pictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date_dateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date_dateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Status_textEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Identity_textEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Note_textEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Number_textEdit.Properties)).BeginInit();
            this.Content_xtraTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Content_splitContainerControl)).BeginInit();
            this.Content_splitContainerControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Source_gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourcePosition_bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Source_gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WordWrapRests_repositoryItemMemoEdit)).BeginInit();
            this.Tree_xtraTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Detail_treeList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WordWrap_repositoryItemMemoEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ttn_pictureEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // barcodesView
            // 
            this.barcodesView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLength});
            this.barcodesView.GridControl = this.Position_gridControl;
            this.barcodesView.Name = "barcodesView";
            this.barcodesView.OptionsDetail.ShowDetailTabs = false;
            this.barcodesView.OptionsView.ShowColumnHeaders = false;
            this.barcodesView.OptionsView.ShowGroupPanel = false;
            // 
            // colLength
            // 
            this.colLength.ColumnEdit = this.deleteItemButtonEdit;
            this.colLength.FieldName = "Column";
            this.colLength.Name = "colLength";
            this.colLength.OptionsColumn.ReadOnly = true;
            this.colLength.Visible = true;
            this.colLength.VisibleIndex = 0;
            // 
            // deleteItemButtonEdit
            // 
            this.deleteItemButtonEdit.AutoHeight = false;
            this.deleteItemButtonEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.deleteItemButtonEdit.Name = "deleteItemButtonEdit";
            this.deleteItemButtonEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.deleteItemButtonEdit_ButtonClick);
            // 
            // Position_gridControl
            // 
            this.Position_gridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Position_gridControl.DataSource = this.Position_bindingSource;
            this.Position_gridControl.EmbeddedNavigator.Buttons.Append.Enabled = false;
            this.Position_gridControl.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.Position_gridControl.EmbeddedNavigator.Buttons.CancelEdit.Enabled = false;
            this.Position_gridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.Position_gridControl.EmbeddedNavigator.Buttons.Edit.Enabled = false;
            this.Position_gridControl.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.Position_gridControl.EmbeddedNavigator.Buttons.EndEdit.Enabled = false;
            this.Position_gridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.Position_gridControl.EmbeddedNavigator.Buttons.NextPage.Enabled = false;
            this.Position_gridControl.EmbeddedNavigator.Buttons.NextPage.Visible = false;
            this.Position_gridControl.EmbeddedNavigator.Buttons.PrevPage.Enabled = false;
            this.Position_gridControl.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
            this.Position_gridControl.EmbeddedNavigator.Buttons.Remove.Enabled = false;
            this.Position_gridControl.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.Position_gridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Position_gridControl.EmbeddedNavigator.ShowToolTips = false;
            this.Position_gridControl.EmbeddedNavigator.TextStringFormat = "Запись {0} из {1}";
            gridLevelNode1.LevelTemplate = this.barcodesView;
            gridLevelNode1.RelationName = "PDF417Codes";
            this.Position_gridControl.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.Position_gridControl.Location = new System.Drawing.Point(10, 47);
            this.Position_gridControl.MainView = this.Position_gridView;
            this.Position_gridControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Position_gridControl.Name = "Position_gridControl";
            this.Position_gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.Status_repositoryItemImageComboBox,
            this.WordWrapTo_repositoryItemMemoEdit,
            this.Decimal_repositoryItemSpinEdit,
            this.deleteItemButtonEdit});
            this.Position_gridControl.Size = new System.Drawing.Size(700, 168);
            this.Position_gridControl.TabIndex = 8;
            this.Position_gridControl.UseEmbeddedNavigator = true;
            this.Position_gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Position_gridView,
            this.barcodesView});
            // 
            // Position_bindingSource
            // 
            this.Position_bindingSource.DataSource = typeof(Axiom.AlcoTransport.MovePosition);
            // 
            // Position_gridView
            // 
            this.Position_gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colStatusOutPosition,
            this.colIdentity,
            this.colTitle,
            this.colProducerTitle,
            this.colAlcoCode,
            this.colProductVCode1,
            this.colCapacityOut,
            this.colVolume,
            this.colQuantity,
            this.colFormBRegId});
            this.Position_gridView.GridControl = this.Position_gridControl;
            this.Position_gridView.Name = "Position_gridView";
            this.Position_gridView.OptionsDetail.ShowDetailTabs = false;
            this.Position_gridView.OptionsLayout.StoreAllOptions = true;
            this.Position_gridView.OptionsPrint.ExpandAllGroups = false;
            this.Position_gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.Position_gridView.OptionsView.EnableAppearanceEvenRow = true;
            this.Position_gridView.OptionsView.RowAutoHeight = true;
            this.Position_gridView.OptionsView.ShowFooter = true;
            this.Position_gridView.OptionsView.ShowGroupPanel = false;
            this.Position_gridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colIdentity, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.Position_gridView.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.Position_gridView_CellValueChanged);
            // 
            // colStatusOutPosition
            // 
            this.colStatusOutPosition.AppearanceCell.Options.UseTextOptions = true;
            this.colStatusOutPosition.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatusOutPosition.AppearanceHeader.Options.UseTextOptions = true;
            this.colStatusOutPosition.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colStatusOutPosition.ColumnEdit = this.Status_repositoryItemImageComboBox;
            this.colStatusOutPosition.FieldName = "Status";
            this.colStatusOutPosition.MaxWidth = 28;
            this.colStatusOutPosition.MinWidth = 28;
            this.colStatusOutPosition.Name = "colStatusOutPosition";
            this.colStatusOutPosition.OptionsColumn.AllowEdit = false;
            this.colStatusOutPosition.OptionsColumn.ReadOnly = true;
            this.colStatusOutPosition.Visible = true;
            this.colStatusOutPosition.VisibleIndex = 0;
            this.colStatusOutPosition.Width = 54;
            // 
            // Status_repositoryItemImageComboBox
            // 
            this.Status_repositoryItemImageComboBox.AutoHeight = false;
            this.Status_repositoryItemImageComboBox.GlyphAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Status_repositoryItemImageComboBox.HotTrackItems = false;
            this.Status_repositoryItemImageComboBox.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Позиция не заполнена", 0, 7),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Позиция заполнена", 1, 5)});
            this.Status_repositoryItemImageComboBox.Name = "Status_repositoryItemImageComboBox";
            this.Status_repositoryItemImageComboBox.ReadOnly = true;
            this.Status_repositoryItemImageComboBox.SmallImages = this.Editor16_imageCollection;
            this.Status_repositoryItemImageComboBox.UseCtrlScroll = false;
            // 
            // Editor16_imageCollection
            // 
            this.Editor16_imageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("Editor16_imageCollection.ImageStream")));
            this.Editor16_imageCollection.Images.SetKeyName(0, "arrow_down_green.png");
            this.Editor16_imageCollection.Images.SetKeyName(1, "arrow_up_blue.png");
            this.Editor16_imageCollection.Images.SetKeyName(2, "checks.png");
            this.Editor16_imageCollection.Images.SetKeyName(3, "delete2.png");
            this.Editor16_imageCollection.Images.SetKeyName(4, "disk_green.png");
            this.Editor16_imageCollection.Images.SetKeyName(5, "check.png");
            this.Editor16_imageCollection.Images.SetKeyName(6, "check2.png");
            this.Editor16_imageCollection.Images.SetKeyName(7, "delete.png");
            this.Editor16_imageCollection.Images.SetKeyName(8, "refresh.png");
            this.Editor16_imageCollection.Images.SetKeyName(9, "document_view.png");
            this.Editor16_imageCollection.Images.SetKeyName(10, "down_plus.png");
            this.Editor16_imageCollection.Images.SetKeyName(11, "up_minus.png");
            this.Editor16_imageCollection.Images.SetKeyName(12, "find_text.png");
            this.Editor16_imageCollection.Images.SetKeyName(13, "magic-wand.png");
            this.Editor16_imageCollection.Images.SetKeyName(14, "funnel_new.png");
            this.Editor16_imageCollection.Images.SetKeyName(15, "column_preferences.png");
            this.Editor16_imageCollection.Images.SetKeyName(16, "document_find.png");
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
            this.colIdentity.VisibleIndex = 1;
            this.colIdentity.Width = 39;
            // 
            // colTitle
            // 
            this.colTitle.AppearanceCell.Options.UseTextOptions = true;
            this.colTitle.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTitle.AppearanceHeader.Options.UseTextOptions = true;
            this.colTitle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTitle.ColumnEdit = this.WordWrapTo_repositoryItemMemoEdit;
            this.colTitle.FieldName = "Title";
            this.colTitle.Name = "colTitle";
            this.colTitle.OptionsColumn.AllowEdit = false;
            this.colTitle.OptionsColumn.ReadOnly = true;
            this.colTitle.Visible = true;
            this.colTitle.VisibleIndex = 2;
            this.colTitle.Width = 191;
            // 
            // WordWrapTo_repositoryItemMemoEdit
            // 
            this.WordWrapTo_repositoryItemMemoEdit.Name = "WordWrapTo_repositoryItemMemoEdit";
            // 
            // colProducerTitle
            // 
            this.colProducerTitle.AppearanceCell.Options.UseTextOptions = true;
            this.colProducerTitle.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProducerTitle.AppearanceHeader.Options.UseTextOptions = true;
            this.colProducerTitle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProducerTitle.FieldName = "ProducerTitle";
            this.colProducerTitle.Name = "colProducerTitle";
            this.colProducerTitle.OptionsColumn.AllowEdit = false;
            this.colProducerTitle.OptionsColumn.ReadOnly = true;
            this.colProducerTitle.Width = 126;
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
            // colProductVCode1
            // 
            this.colProductVCode1.AppearanceCell.Options.UseTextOptions = true;
            this.colProductVCode1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductVCode1.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductVCode1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductVCode1.FieldName = "ProductVCode";
            this.colProductVCode1.Name = "colProductVCode1";
            this.colProductVCode1.OptionsColumn.AllowEdit = false;
            this.colProductVCode1.OptionsColumn.ReadOnly = true;
            // 
            // colCapacityOut
            // 
            this.colCapacityOut.AppearanceCell.Options.UseTextOptions = true;
            this.colCapacityOut.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCapacityOut.AppearanceHeader.Options.UseTextOptions = true;
            this.colCapacityOut.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCapacityOut.FieldName = "Capacity";
            this.colCapacityOut.Name = "colCapacityOut";
            this.colCapacityOut.OptionsColumn.AllowEdit = false;
            this.colCapacityOut.OptionsColumn.ReadOnly = true;
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
            // 
            // colQuantity
            // 
            this.colQuantity.AppearanceCell.Options.UseTextOptions = true;
            this.colQuantity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQuantity.AppearanceHeader.Options.UseTextOptions = true;
            this.colQuantity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQuantity.ColumnEdit = this.Decimal_repositoryItemSpinEdit;
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Quantity", "Всего: {0:n2}")});
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 3;
            this.colQuantity.Width = 95;
            // 
            // Decimal_repositoryItemSpinEdit
            // 
            this.Decimal_repositoryItemSpinEdit.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.Decimal_repositoryItemSpinEdit.AutoHeight = false;
            this.Decimal_repositoryItemSpinEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Decimal_repositoryItemSpinEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.Decimal_repositoryItemSpinEdit.MaxValue = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.Decimal_repositoryItemSpinEdit.Name = "Decimal_repositoryItemSpinEdit";
            // 
            // colFormBRegId
            // 
            this.colFormBRegId.AppearanceCell.Options.UseTextOptions = true;
            this.colFormBRegId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFormBRegId.AppearanceHeader.Options.UseTextOptions = true;
            this.colFormBRegId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFormBRegId.FieldName = "FormBRegId";
            this.colFormBRegId.Name = "colFormBRegId";
            this.colFormBRegId.Visible = true;
            this.colFormBRegId.VisibleIndex = 4;
            this.colFormBRegId.Width = 102;
            // 
            // Close_simpleButton
            // 
            this.Close_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Close_simpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close_simpleButton.ImageOptions.ImageIndex = 3;
            this.Close_simpleButton.ImageOptions.ImageList = this.Editor16_imageCollection;
            this.Close_simpleButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.Close_simpleButton.Location = new System.Drawing.Point(766, 608);
            this.Close_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Close_simpleButton.Name = "Close_simpleButton";
            this.Close_simpleButton.Size = new System.Drawing.Size(187, 33);
            this.Close_simpleButton.TabIndex = 2;
            this.Close_simpleButton.Text = "Закрыть";
            this.Close_simpleButton.Click += new System.EventHandler(this.Close_simpleButton_Click);
            // 
            // Main_xtraTabControl
            // 
            this.Main_xtraTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Main_xtraTabControl.Location = new System.Drawing.Point(85, 10);
            this.Main_xtraTabControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Main_xtraTabControl.Name = "Main_xtraTabControl";
            this.Main_xtraTabControl.SelectedTabPage = this.Common_xtraTabPage;
            this.Main_xtraTabControl.Size = new System.Drawing.Size(867, 588);
            this.Main_xtraTabControl.TabIndex = 0;
            this.Main_xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.Common_xtraTabPage,
            this.Content_xtraTabPage,
            this.Tree_xtraTabPage});
            // 
            // Common_xtraTabPage
            // 
            this.Common_xtraTabPage.Controls.Add(this.Attention_pictureEdit);
            this.Common_xtraTabPage.Controls.Add(this.Date_dateEdit);
            this.Common_xtraTabPage.Controls.Add(this.Status_labelControl);
            this.Common_xtraTabPage.Controls.Add(this.Identity_labelControl);
            this.Common_xtraTabPage.Controls.Add(this.Date_labelControl);
            this.Common_xtraTabPage.Controls.Add(this.Status_textEdit);
            this.Common_xtraTabPage.Controls.Add(this.Identity_textEdit);
            this.Common_xtraTabPage.Controls.Add(this.Note_labelControl);
            this.Common_xtraTabPage.Controls.Add(this.Number_labelControl);
            this.Common_xtraTabPage.Controls.Add(this.Note_textEdit);
            this.Common_xtraTabPage.Controls.Add(this.Number_textEdit);
            this.Common_xtraTabPage.Controls.Add(this.labelControl10);
            this.Common_xtraTabPage.Controls.Add(this.labelControl25);
            this.Common_xtraTabPage.Controls.Add(this.labelControl1);
            this.Common_xtraTabPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Common_xtraTabPage.Name = "Common_xtraTabPage";
            this.Common_xtraTabPage.Size = new System.Drawing.Size(861, 557);
            this.Common_xtraTabPage.Text = "Общие свойства";
            // 
            // Attention_pictureEdit
            // 
            this.Attention_pictureEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Attention_pictureEdit.Cursor = System.Windows.Forms.Cursors.Default;
            this.Attention_pictureEdit.EditValue = ((object)(resources.GetObject("Attention_pictureEdit.EditValue")));
            this.Attention_pictureEdit.Location = new System.Drawing.Point(770, 130);
            this.Attention_pictureEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Attention_pictureEdit.Name = "Attention_pictureEdit";
            this.Attention_pictureEdit.Properties.AllowFocused = false;
            this.Attention_pictureEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Attention_pictureEdit.Properties.Appearance.Options.UseBackColor = true;
            this.Attention_pictureEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Attention_pictureEdit.Properties.ReadOnly = true;
            this.Attention_pictureEdit.Properties.ShowMenu = false;
            this.Attention_pictureEdit.Size = new System.Drawing.Size(23, 25);
            this.Attention_pictureEdit.TabIndex = 10;
            this.Attention_pictureEdit.ToolTip = "В силу некоторых особенностей функционирования системы ЕГАИС, убедительная рекоме" +
    "ндация: делать номер документа уникальным.";
            this.Attention_pictureEdit.ToolTipIconType = DevExpress.Utils.ToolTipIconType.Information;
            this.Attention_pictureEdit.ToolTipTitle = "Номер документа";
            // 
            // Date_dateEdit
            // 
            this.Date_dateEdit.EditValue = new System.DateTime(2015, 12, 1, 13, 3, 9, 0);
            this.Date_dateEdit.Location = new System.Drawing.Point(260, 162);
            this.Date_dateEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Date_dateEdit.Name = "Date_dateEdit";
            this.Date_dateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Date_dateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Date_dateEdit.Properties.DisplayFormat.FormatString = "dd MMMM yyyy (dddd)";
            this.Date_dateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.Date_dateEdit.Properties.EditFormat.FormatString = "dd MMMM yyyy (dddd)";
            this.Date_dateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.Date_dateEdit.Properties.Mask.EditMask = "dd MMMM yyyy (dddd)";
            this.Date_dateEdit.Size = new System.Drawing.Size(243, 23);
            this.Date_dateEdit.TabIndex = 3;
            this.Date_dateEdit.EditValueChanged += new System.EventHandler(this.Date_dateEdit_EditValueChanged);
            // 
            // Status_labelControl
            // 
            this.Status_labelControl.Location = new System.Drawing.Point(12, 31);
            this.Status_labelControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Status_labelControl.Name = "Status_labelControl";
            this.Status_labelControl.Size = new System.Drawing.Size(109, 16);
            this.Status_labelControl.TabIndex = 3;
            this.Status_labelControl.Text = "Статус документа:";
            // 
            // Identity_labelControl
            // 
            this.Identity_labelControl.Location = new System.Drawing.Point(12, 81);
            this.Identity_labelControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Identity_labelControl.Name = "Identity_labelControl";
            this.Identity_labelControl.Size = new System.Drawing.Size(242, 16);
            this.Identity_labelControl.TabIndex = 3;
            this.Identity_labelControl.Text = "Идентификатор документа (клиентский):";
            // 
            // Date_labelControl
            // 
            this.Date_labelControl.Location = new System.Drawing.Point(12, 166);
            this.Date_labelControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Date_labelControl.Name = "Date_labelControl";
            this.Date_labelControl.Size = new System.Drawing.Size(177, 16);
            this.Date_labelControl.TabIndex = 4;
            this.Date_labelControl.Text = "Дата составления документа:";
            // 
            // Status_textEdit
            // 
            this.Status_textEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Status_textEdit.Location = new System.Drawing.Point(325, 27);
            this.Status_textEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Status_textEdit.Name = "Status_textEdit";
            this.Status_textEdit.Properties.MaxLength = 200;
            this.Status_textEdit.Properties.ReadOnly = true;
            this.Status_textEdit.Size = new System.Drawing.Size(292, 23);
            this.Status_textEdit.TabIndex = 0;
            // 
            // Identity_textEdit
            // 
            this.Identity_textEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Identity_textEdit.Location = new System.Drawing.Point(325, 78);
            this.Identity_textEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Identity_textEdit.Name = "Identity_textEdit";
            this.Identity_textEdit.Properties.MaxLength = 50;
            this.Identity_textEdit.Properties.ReadOnly = true;
            this.Identity_textEdit.Size = new System.Drawing.Size(292, 23);
            this.Identity_textEdit.TabIndex = 1;
            this.Identity_textEdit.EditValueChanged += new System.EventHandler(this.Identity_textEdit_EditValueChanged);
            // 
            // Note_labelControl
            // 
            this.Note_labelControl.Location = new System.Drawing.Point(12, 202);
            this.Note_labelControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Note_labelControl.Name = "Note_labelControl";
            this.Note_labelControl.Size = new System.Drawing.Size(163, 16);
            this.Note_labelControl.TabIndex = 3;
            this.Note_labelControl.Text = "Дополнительное описание:";
            // 
            // Number_labelControl
            // 
            this.Number_labelControl.Location = new System.Drawing.Point(12, 134);
            this.Number_labelControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Number_labelControl.Name = "Number_labelControl";
            this.Number_labelControl.Size = new System.Drawing.Size(107, 16);
            this.Number_labelControl.TabIndex = 3;
            this.Number_labelControl.Text = "Номер документа:";
            // 
            // Note_textEdit
            // 
            this.Note_textEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Note_textEdit.Location = new System.Drawing.Point(260, 198);
            this.Note_textEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Note_textEdit.Name = "Note_textEdit";
            this.Note_textEdit.Properties.MaxLength = 200;
            this.Note_textEdit.Size = new System.Drawing.Size(533, 23);
            this.Note_textEdit.TabIndex = 5;
            this.Note_textEdit.EditValueChanged += new System.EventHandler(this.Note_textEdit_EditValueChanged);
            // 
            // Number_textEdit
            // 
            this.Number_textEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Number_textEdit.Location = new System.Drawing.Point(260, 130);
            this.Number_textEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Number_textEdit.Name = "Number_textEdit";
            this.Number_textEdit.Properties.MaxLength = 50;
            this.Number_textEdit.Size = new System.Drawing.Size(502, 23);
            this.Number_textEdit.TabIndex = 2;
            this.Number_textEdit.EditValueChanged += new System.EventHandler(this.Number_textEdit_EditValueChanged);
            // 
            // labelControl10
            // 
            this.labelControl10.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl10.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl10.Appearance.Options.UseFont = true;
            this.labelControl10.Appearance.Options.UseTextOptions = true;
            this.labelControl10.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl10.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl10.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl10.LineVisible = true;
            this.labelControl10.Location = new System.Drawing.Point(12, 229);
            this.labelControl10.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.ShowToolTips = false;
            this.labelControl10.Size = new System.Drawing.Size(781, 16);
            this.labelControl10.TabIndex = 5;
            // 
            // labelControl25
            // 
            this.labelControl25.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl25.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl25.Appearance.Options.UseFont = true;
            this.labelControl25.Appearance.Options.UseTextOptions = true;
            this.labelControl25.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl25.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl25.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl25.LineVisible = true;
            this.labelControl25.Location = new System.Drawing.Point(12, 110);
            this.labelControl25.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl25.Name = "labelControl25";
            this.labelControl25.ShowToolTips = false;
            this.labelControl25.Size = new System.Drawing.Size(781, 16);
            this.labelControl25.TabIndex = 5;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl1.LineVisible = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 54);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.ShowToolTips = false;
            this.labelControl1.Size = new System.Drawing.Size(781, 16);
            this.labelControl1.TabIndex = 5;
            // 
            // Content_xtraTabPage
            // 
            this.Content_xtraTabPage.Controls.Add(this.Content_splitContainerControl);
            this.Content_xtraTabPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Content_xtraTabPage.Name = "Content_xtraTabPage";
            this.Content_xtraTabPage.Size = new System.Drawing.Size(861, 557);
            this.Content_xtraTabPage.Text = "Состав документа";
            // 
            // Content_splitContainerControl
            // 
            this.Content_splitContainerControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Content_splitContainerControl.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.Content_splitContainerControl.Horizontal = false;
            this.Content_splitContainerControl.Location = new System.Drawing.Point(3, 4);
            this.Content_splitContainerControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Content_splitContainerControl.Name = "Content_splitContainerControl";
            this.Content_splitContainerControl.Panel1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.Content_splitContainerControl.Panel1.Controls.Add(this.Source_gridControl);
            this.Content_splitContainerControl.Panel1.Controls.Add(this.APFind_simpleButton);
            this.Content_splitContainerControl.Panel1.Controls.Add(this.APGroup_simpleButton);
            this.Content_splitContainerControl.Panel1.Controls.Add(this.APFilter_simpleButton);
            this.Content_splitContainerControl.Panel1.Controls.Add(this.APColumns_simpleButton);
            this.Content_splitContainerControl.Panel1.Controls.Add(this.APRefresh_simpleButton);
            this.Content_splitContainerControl.Panel1.MinSize = 144;
            this.Content_splitContainerControl.Panel1.ShowCaption = true;
            this.Content_splitContainerControl.Panel1.Text = "Исходные данные для составления";
            this.Content_splitContainerControl.Panel2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Default;
            this.Content_splitContainerControl.Panel2.Controls.Add(this.scanBarcode_simpleButton);
            this.Content_splitContainerControl.Panel2.Controls.Add(this.FindPosition_simpleButton);
            this.Content_splitContainerControl.Panel2.Controls.Add(this.ColumnsPosition_simpleButton);
            this.Content_splitContainerControl.Panel2.Controls.Add(this.PrintPosition_simpleButton);
            this.Content_splitContainerControl.Panel2.Controls.Add(this.RefreshPosition_simpleButton);
            this.Content_splitContainerControl.Panel2.Controls.Add(this.CheckPosition_simpleButton);
            this.Content_splitContainerControl.Panel2.Controls.Add(this.Add_simpleButton);
            this.Content_splitContainerControl.Panel2.Controls.Add(this.Remove_simpleButton);
            this.Content_splitContainerControl.Panel2.Controls.Add(this.Position_gridControl);
            this.Content_splitContainerControl.Panel2.MinSize = 144;
            this.Content_splitContainerControl.Panel2.ShowCaption = true;
            this.Content_splitContainerControl.Panel2.Text = "Список позиций в документе";
            this.Content_splitContainerControl.Size = new System.Drawing.Size(855, 550);
            this.Content_splitContainerControl.SplitterPosition = 282;
            this.Content_splitContainerControl.TabIndex = 0;
            // 
            // Source_gridControl
            // 
            this.Source_gridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Source_gridControl.DataSource = this.SourcePosition_bindingSource;
            this.Source_gridControl.EmbeddedNavigator.Buttons.Append.Enabled = false;
            this.Source_gridControl.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.Source_gridControl.EmbeddedNavigator.Buttons.CancelEdit.Enabled = false;
            this.Source_gridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.Source_gridControl.EmbeddedNavigator.Buttons.Edit.Enabled = false;
            this.Source_gridControl.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.Source_gridControl.EmbeddedNavigator.Buttons.EndEdit.Enabled = false;
            this.Source_gridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.Source_gridControl.EmbeddedNavigator.Buttons.NextPage.Enabled = false;
            this.Source_gridControl.EmbeddedNavigator.Buttons.NextPage.Visible = false;
            this.Source_gridControl.EmbeddedNavigator.Buttons.PrevPage.Enabled = false;
            this.Source_gridControl.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
            this.Source_gridControl.EmbeddedNavigator.Buttons.Remove.Enabled = false;
            this.Source_gridControl.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.Source_gridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Source_gridControl.EmbeddedNavigator.ShowToolTips = false;
            this.Source_gridControl.EmbeddedNavigator.TextStringFormat = "Запись {0} из {1}";
            this.Source_gridControl.Location = new System.Drawing.Point(10, 42);
            this.Source_gridControl.MainView = this.Source_gridView;
            this.Source_gridControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Source_gridControl.Name = "Source_gridControl";
            this.Source_gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.WordWrapRests_repositoryItemMemoEdit});
            this.Source_gridControl.Size = new System.Drawing.Size(700, 135);
            this.Source_gridControl.TabIndex = 12;
            this.Source_gridControl.UseEmbeddedNavigator = true;
            this.Source_gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Source_gridView});
            this.Source_gridControl.DoubleClick += new System.EventHandler(this.Source_gridControl_DoubleClick);
            // 
            // SourcePosition_bindingSource
            // 
            this.SourcePosition_bindingSource.AllowNew = false;
            this.SourcePosition_bindingSource.DataSource = typeof(Axiom.AlcoTransport.RestsPosition);
            // 
            // Source_gridView
            // 
            this.Source_gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colPositionRestsDate,
            this.colTitle1,
            this.colQuantity1,
            this.colFormARegId1,
            this.colFormBRegId1,
            this.colShortName1,
            this.colFullName1,
            this.colAlcoCode1,
            this.colCapacity1,
            this.colVolume1,
            this.colProductVCode2,
            this.colProducer,
            this.colImporter});
            this.Source_gridView.GridControl = this.Source_gridControl;
            this.Source_gridView.Name = "Source_gridView";
            this.Source_gridView.OptionsBehavior.ReadOnly = true;
            this.Source_gridView.OptionsLayout.StoreAllOptions = true;
            this.Source_gridView.OptionsPrint.ExpandAllGroups = false;
            this.Source_gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.Source_gridView.OptionsView.EnableAppearanceEvenRow = true;
            this.Source_gridView.OptionsView.RowAutoHeight = true;
            this.Source_gridView.OptionsView.ShowGroupPanel = false;
            // 
            // colId
            // 
            this.colId.AppearanceCell.Options.UseTextOptions = true;
            this.colId.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colId.AppearanceHeader.Options.UseTextOptions = true;
            this.colId.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            this.colId.OptionsColumn.ReadOnly = true;
            this.colId.Visible = true;
            this.colId.VisibleIndex = 0;
            this.colId.Width = 50;
            // 
            // colPositionRestsDate
            // 
            this.colPositionRestsDate.AppearanceCell.Options.UseTextOptions = true;
            this.colPositionRestsDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPositionRestsDate.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colPositionRestsDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colPositionRestsDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colPositionRestsDate.ColumnEdit = this.WordWrapRests_repositoryItemMemoEdit;
            this.colPositionRestsDate.FieldName = "PositionRestsDate";
            this.colPositionRestsDate.Name = "colPositionRestsDate";
            this.colPositionRestsDate.OptionsColumn.ReadOnly = true;
            this.colPositionRestsDate.Visible = true;
            this.colPositionRestsDate.VisibleIndex = 1;
            this.colPositionRestsDate.Width = 130;
            // 
            // WordWrapRests_repositoryItemMemoEdit
            // 
            this.WordWrapRests_repositoryItemMemoEdit.Name = "WordWrapRests_repositoryItemMemoEdit";
            // 
            // colTitle1
            // 
            this.colTitle1.AppearanceCell.Options.UseTextOptions = true;
            this.colTitle1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colTitle1.AppearanceHeader.Options.UseTextOptions = true;
            this.colTitle1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colTitle1.ColumnEdit = this.WordWrapRests_repositoryItemMemoEdit;
            this.colTitle1.FieldName = "Title";
            this.colTitle1.Name = "colTitle1";
            this.colTitle1.OptionsColumn.ReadOnly = true;
            this.colTitle1.Visible = true;
            this.colTitle1.VisibleIndex = 2;
            this.colTitle1.Width = 211;
            // 
            // colQuantity1
            // 
            this.colQuantity1.AppearanceCell.Options.UseTextOptions = true;
            this.colQuantity1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQuantity1.AppearanceHeader.Options.UseTextOptions = true;
            this.colQuantity1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colQuantity1.FieldName = "Quantity";
            this.colQuantity1.Name = "colQuantity1";
            this.colQuantity1.OptionsColumn.ReadOnly = true;
            this.colQuantity1.Visible = true;
            this.colQuantity1.VisibleIndex = 3;
            this.colQuantity1.Width = 81;
            // 
            // colFormARegId1
            // 
            this.colFormARegId1.AppearanceCell.Options.UseTextOptions = true;
            this.colFormARegId1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFormARegId1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFormARegId1.AppearanceHeader.Options.UseTextOptions = true;
            this.colFormARegId1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFormARegId1.ColumnEdit = this.WordWrapRests_repositoryItemMemoEdit;
            this.colFormARegId1.FieldName = "FormARegId";
            this.colFormARegId1.Name = "colFormARegId1";
            this.colFormARegId1.OptionsColumn.ReadOnly = true;
            // 
            // colFormBRegId1
            // 
            this.colFormBRegId1.AppearanceCell.Options.UseTextOptions = true;
            this.colFormBRegId1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFormBRegId1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFormBRegId1.AppearanceHeader.Options.UseTextOptions = true;
            this.colFormBRegId1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFormBRegId1.ColumnEdit = this.WordWrapRests_repositoryItemMemoEdit;
            this.colFormBRegId1.FieldName = "FormBRegId";
            this.colFormBRegId1.Name = "colFormBRegId1";
            this.colFormBRegId1.OptionsColumn.ReadOnly = true;
            // 
            // colShortName1
            // 
            this.colShortName1.AppearanceCell.Options.UseTextOptions = true;
            this.colShortName1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colShortName1.AppearanceHeader.Options.UseTextOptions = true;
            this.colShortName1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colShortName1.ColumnEdit = this.WordWrapRests_repositoryItemMemoEdit;
            this.colShortName1.FieldName = "ShortName";
            this.colShortName1.Name = "colShortName1";
            this.colShortName1.OptionsColumn.ReadOnly = true;
            // 
            // colFullName1
            // 
            this.colFullName1.AppearanceCell.Options.UseTextOptions = true;
            this.colFullName1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colFullName1.AppearanceHeader.Options.UseTextOptions = true;
            this.colFullName1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colFullName1.ColumnEdit = this.WordWrapRests_repositoryItemMemoEdit;
            this.colFullName1.FieldName = "FullName";
            this.colFullName1.Name = "colFullName1";
            this.colFullName1.OptionsColumn.ReadOnly = true;
            // 
            // colAlcoCode1
            // 
            this.colAlcoCode1.AppearanceCell.Options.UseTextOptions = true;
            this.colAlcoCode1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAlcoCode1.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colAlcoCode1.AppearanceHeader.Options.UseTextOptions = true;
            this.colAlcoCode1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAlcoCode1.ColumnEdit = this.WordWrapRests_repositoryItemMemoEdit;
            this.colAlcoCode1.FieldName = "AlcoCode";
            this.colAlcoCode1.Name = "colAlcoCode1";
            this.colAlcoCode1.OptionsColumn.ReadOnly = true;
            // 
            // colCapacity1
            // 
            this.colCapacity1.AppearanceCell.Options.UseTextOptions = true;
            this.colCapacity1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCapacity1.AppearanceHeader.Options.UseTextOptions = true;
            this.colCapacity1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCapacity1.FieldName = "Capacity";
            this.colCapacity1.Name = "colCapacity1";
            this.colCapacity1.OptionsColumn.ReadOnly = true;
            this.colCapacity1.Visible = true;
            this.colCapacity1.VisibleIndex = 4;
            this.colCapacity1.Width = 79;
            // 
            // colVolume1
            // 
            this.colVolume1.AppearanceCell.Options.UseTextOptions = true;
            this.colVolume1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colVolume1.AppearanceHeader.Options.UseTextOptions = true;
            this.colVolume1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colVolume1.FieldName = "Volume";
            this.colVolume1.Name = "colVolume1";
            this.colVolume1.OptionsColumn.ReadOnly = true;
            this.colVolume1.Visible = true;
            this.colVolume1.VisibleIndex = 5;
            this.colVolume1.Width = 84;
            // 
            // colProductVCode2
            // 
            this.colProductVCode2.AppearanceCell.Options.UseTextOptions = true;
            this.colProductVCode2.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductVCode2.AppearanceHeader.Options.UseTextOptions = true;
            this.colProductVCode2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProductVCode2.FieldName = "ProductVCode";
            this.colProductVCode2.Name = "colProductVCode2";
            this.colProductVCode2.OptionsColumn.ReadOnly = true;
            // 
            // colProducer
            // 
            this.colProducer.AppearanceCell.Options.UseTextOptions = true;
            this.colProducer.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProducer.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colProducer.AppearanceHeader.Options.UseTextOptions = true;
            this.colProducer.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colProducer.ColumnEdit = this.WordWrapRests_repositoryItemMemoEdit;
            this.colProducer.FieldName = "Producer";
            this.colProducer.Name = "colProducer";
            this.colProducer.OptionsColumn.ReadOnly = true;
            this.colProducer.Visible = true;
            this.colProducer.VisibleIndex = 6;
            this.colProducer.Width = 207;
            // 
            // colImporter
            // 
            this.colImporter.AppearanceCell.Options.UseTextOptions = true;
            this.colImporter.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colImporter.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colImporter.AppearanceHeader.Options.UseTextOptions = true;
            this.colImporter.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colImporter.ColumnEdit = this.WordWrapRests_repositoryItemMemoEdit;
            this.colImporter.FieldName = "Importer";
            this.colImporter.Name = "colImporter";
            this.colImporter.OptionsColumn.ReadOnly = true;
            // 
            // APFind_simpleButton
            // 
            this.APFind_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.APFind_simpleButton.ImageOptions.ImageIndex = 12;
            this.APFind_simpleButton.ImageOptions.ImageList = this.Editor16_imageCollection;
            this.APFind_simpleButton.Location = new System.Drawing.Point(534, 6);
            this.APFind_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.APFind_simpleButton.Name = "APFind_simpleButton";
            this.APFind_simpleButton.Size = new System.Drawing.Size(29, 28);
            this.APFind_simpleButton.TabIndex = 1;
            this.APFind_simpleButton.ToolTip = "Показать или скрыть панель поиска.";
            this.APFind_simpleButton.ToolTipTitle = "Искать в списке";
            this.APFind_simpleButton.Click += new System.EventHandler(this.APFind_simpleButton_Click);
            // 
            // APGroup_simpleButton
            // 
            this.APGroup_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.APGroup_simpleButton.ImageOptions.ImageIndex = 13;
            this.APGroup_simpleButton.ImageOptions.ImageList = this.Editor16_imageCollection;
            this.APGroup_simpleButton.Location = new System.Drawing.Point(571, 6);
            this.APGroup_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.APGroup_simpleButton.Name = "APGroup_simpleButton";
            this.APGroup_simpleButton.Size = new System.Drawing.Size(29, 28);
            this.APGroup_simpleButton.TabIndex = 2;
            this.APGroup_simpleButton.ToolTip = "Показать или скрыть панель группировки.";
            this.APGroup_simpleButton.ToolTipTitle = "Панель группировки";
            this.APGroup_simpleButton.Click += new System.EventHandler(this.APGroup_simpleButton_Click);
            // 
            // APFilter_simpleButton
            // 
            this.APFilter_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.APFilter_simpleButton.ImageOptions.ImageIndex = 14;
            this.APFilter_simpleButton.ImageOptions.ImageList = this.Editor16_imageCollection;
            this.APFilter_simpleButton.Location = new System.Drawing.Point(607, 6);
            this.APFilter_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.APFilter_simpleButton.Name = "APFilter_simpleButton";
            this.APFilter_simpleButton.Size = new System.Drawing.Size(29, 28);
            this.APFilter_simpleButton.TabIndex = 3;
            this.APFilter_simpleButton.ToolTip = "Показать конструктор фильтра.";
            this.APFilter_simpleButton.ToolTipTitle = "Конструктор фильтр";
            this.APFilter_simpleButton.Click += new System.EventHandler(this.APFilter_simpleButton_Click);
            // 
            // APColumns_simpleButton
            // 
            this.APColumns_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.APColumns_simpleButton.ImageOptions.ImageIndex = 15;
            this.APColumns_simpleButton.ImageOptions.ImageList = this.Editor16_imageCollection;
            this.APColumns_simpleButton.Location = new System.Drawing.Point(643, 6);
            this.APColumns_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.APColumns_simpleButton.Name = "APColumns_simpleButton";
            this.APColumns_simpleButton.Size = new System.Drawing.Size(29, 28);
            this.APColumns_simpleButton.TabIndex = 4;
            this.APColumns_simpleButton.ToolTip = "Открыть диалог выбора колонок.";
            this.APColumns_simpleButton.ToolTipTitle = "Выбор колонок";
            this.APColumns_simpleButton.Click += new System.EventHandler(this.APColumns_simpleButton_Click);
            // 
            // APRefresh_simpleButton
            // 
            this.APRefresh_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.APRefresh_simpleButton.ImageOptions.ImageIndex = 8;
            this.APRefresh_simpleButton.ImageOptions.ImageList = this.Editor16_imageCollection;
            this.APRefresh_simpleButton.Location = new System.Drawing.Point(679, 6);
            this.APRefresh_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.APRefresh_simpleButton.Name = "APRefresh_simpleButton";
            this.APRefresh_simpleButton.Size = new System.Drawing.Size(29, 28);
            this.APRefresh_simpleButton.TabIndex = 3;
            this.APRefresh_simpleButton.ToolTip = "Обновить список алкогольной продукции.";
            this.APRefresh_simpleButton.ToolTipTitle = "Обновить";
            this.APRefresh_simpleButton.Click += new System.EventHandler(this.APRefresh_simpleButton_Click);
            // 
            // scanBarcode_simpleButton
            // 
            this.scanBarcode_simpleButton.ImageOptions.ImageList = this.Editor16_imageCollection;
            this.scanBarcode_simpleButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            this.scanBarcode_simpleButton.Location = new System.Drawing.Point(368, 11);
            this.scanBarcode_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.scanBarcode_simpleButton.Name = "scanBarcode_simpleButton";
            this.scanBarcode_simpleButton.Size = new System.Drawing.Size(168, 28);
            this.scanBarcode_simpleButton.TabIndex = 9;
            this.scanBarcode_simpleButton.Text = "Сканировать штрихкод";
            this.scanBarcode_simpleButton.Click += new System.EventHandler(this.scanBarcode_simpleButton_Click);
            // 
            // FindPosition_simpleButton
            // 
            this.FindPosition_simpleButton.ImageOptions.ImageIndex = 12;
            this.FindPosition_simpleButton.ImageOptions.ImageList = this.Editor16_imageCollection;
            this.FindPosition_simpleButton.Location = new System.Drawing.Point(222, 11);
            this.FindPosition_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FindPosition_simpleButton.Name = "FindPosition_simpleButton";
            this.FindPosition_simpleButton.Size = new System.Drawing.Size(29, 28);
            this.FindPosition_simpleButton.TabIndex = 2;
            this.FindPosition_simpleButton.ToolTip = "Показать или скрыть панель поиска.";
            this.FindPosition_simpleButton.ToolTipTitle = "Искать в списке";
            this.FindPosition_simpleButton.Click += new System.EventHandler(this.FindPosition_simpleButton_Click);
            // 
            // ColumnsPosition_simpleButton
            // 
            this.ColumnsPosition_simpleButton.ImageOptions.ImageIndex = 15;
            this.ColumnsPosition_simpleButton.ImageOptions.ImageList = this.Editor16_imageCollection;
            this.ColumnsPosition_simpleButton.Location = new System.Drawing.Point(258, 11);
            this.ColumnsPosition_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ColumnsPosition_simpleButton.Name = "ColumnsPosition_simpleButton";
            this.ColumnsPosition_simpleButton.Size = new System.Drawing.Size(29, 28);
            this.ColumnsPosition_simpleButton.TabIndex = 3;
            this.ColumnsPosition_simpleButton.ToolTip = "Открыть диалог выбора колонок.";
            this.ColumnsPosition_simpleButton.ToolTipTitle = "Выбор колонок";
            this.ColumnsPosition_simpleButton.Click += new System.EventHandler(this.ColumnsPosition_simpleButton_Click);
            // 
            // PrintPosition_simpleButton
            // 
            this.PrintPosition_simpleButton.ImageOptions.ImageIndex = 9;
            this.PrintPosition_simpleButton.ImageOptions.ImageList = this.Editor16_imageCollection;
            this.PrintPosition_simpleButton.Location = new System.Drawing.Point(294, 11);
            this.PrintPosition_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.PrintPosition_simpleButton.Name = "PrintPosition_simpleButton";
            this.PrintPosition_simpleButton.Size = new System.Drawing.Size(29, 28);
            this.PrintPosition_simpleButton.TabIndex = 4;
            this.PrintPosition_simpleButton.ToolTip = "Открыть диалог предварительного просмотра документа перед печатью.";
            this.PrintPosition_simpleButton.ToolTipTitle = "Печать";
            this.PrintPosition_simpleButton.Click += new System.EventHandler(this.PrintPosition_simpleButton_Click);
            // 
            // RefreshPosition_simpleButton
            // 
            this.RefreshPosition_simpleButton.ImageOptions.ImageIndex = 8;
            this.RefreshPosition_simpleButton.ImageOptions.ImageList = this.Editor16_imageCollection;
            this.RefreshPosition_simpleButton.Location = new System.Drawing.Point(330, 11);
            this.RefreshPosition_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.RefreshPosition_simpleButton.Name = "RefreshPosition_simpleButton";
            this.RefreshPosition_simpleButton.Size = new System.Drawing.Size(29, 28);
            this.RefreshPosition_simpleButton.TabIndex = 5;
            this.RefreshPosition_simpleButton.ToolTip = "Обновить список позиций в документе.";
            this.RefreshPosition_simpleButton.ToolTipTitle = "Обновить";
            this.RefreshPosition_simpleButton.Click += new System.EventHandler(this.RefreshPosition_simpleButton_Click);
            // 
            // CheckPosition_simpleButton
            // 
            this.CheckPosition_simpleButton.ImageOptions.ImageIndex = 2;
            this.CheckPosition_simpleButton.ImageOptions.ImageList = this.Editor16_imageCollection;
            this.CheckPosition_simpleButton.Location = new System.Drawing.Point(185, 11);
            this.CheckPosition_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CheckPosition_simpleButton.Name = "CheckPosition_simpleButton";
            this.CheckPosition_simpleButton.Size = new System.Drawing.Size(29, 28);
            this.CheckPosition_simpleButton.TabIndex = 1;
            this.CheckPosition_simpleButton.ToolTip = "Проверить список позиций в документе";
            this.CheckPosition_simpleButton.ToolTipTitle = "Проверить";
            this.CheckPosition_simpleButton.Click += new System.EventHandler(this.CheckPosition_simpleButton_Click);
            // 
            // Add_simpleButton
            // 
            this.Add_simpleButton.ImageOptions.ImageIndex = 0;
            this.Add_simpleButton.ImageOptions.ImageList = this.Editor16_imageCollection;
            this.Add_simpleButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            this.Add_simpleButton.Location = new System.Drawing.Point(10, 11);
            this.Add_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Add_simpleButton.Name = "Add_simpleButton";
            this.Add_simpleButton.Size = new System.Drawing.Size(168, 28);
            this.Add_simpleButton.TabIndex = 0;
            this.Add_simpleButton.Text = "Добавить в состав";
            this.Add_simpleButton.Click += new System.EventHandler(this.Add_simpleButton_Click);
            // 
            // Remove_simpleButton
            // 
            this.Remove_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Remove_simpleButton.ImageOptions.ImageIndex = 1;
            this.Remove_simpleButton.ImageOptions.ImageList = this.Editor16_imageCollection;
            this.Remove_simpleButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.Remove_simpleButton.Location = new System.Drawing.Point(542, 11);
            this.Remove_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Remove_simpleButton.Name = "Remove_simpleButton";
            this.Remove_simpleButton.Size = new System.Drawing.Size(168, 28);
            this.Remove_simpleButton.TabIndex = 7;
            this.Remove_simpleButton.Text = "Удалить из состава";
            this.Remove_simpleButton.Click += new System.EventHandler(this.Remove_simpleButton_Click);
            // 
            // Tree_xtraTabPage
            // 
            this.Tree_xtraTabPage.Controls.Add(this.Detail_treeList);
            this.Tree_xtraTabPage.Controls.Add(this.ExpandTree_simpleButton);
            this.Tree_xtraTabPage.Controls.Add(this.CollapseTree_simpleButton);
            this.Tree_xtraTabPage.Controls.Add(this.Print_simpleButton);
            this.Tree_xtraTabPage.Controls.Add(this.CaptionB_labelControl);
            this.Tree_xtraTabPage.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Tree_xtraTabPage.Name = "Tree_xtraTabPage";
            this.Tree_xtraTabPage.Size = new System.Drawing.Size(861, 557);
            this.Tree_xtraTabPage.Text = "Детальная информация";
            // 
            // Detail_treeList
            // 
            this.Detail_treeList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Detail_treeList.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.Title_treeListColumn,
            this.Value_treeListColumn});
            this.Detail_treeList.Location = new System.Drawing.Point(15, 54);
            this.Detail_treeList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Detail_treeList.Name = "Detail_treeList";
            this.Detail_treeList.OptionsBehavior.ReadOnly = true;
            this.Detail_treeList.OptionsCustomization.AllowBandMoving = false;
            this.Detail_treeList.OptionsCustomization.AllowColumnMoving = false;
            this.Detail_treeList.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.Detail_treeList.OptionsSelection.MultiSelect = true;
            this.Detail_treeList.OptionsView.EnableAppearanceEvenRow = true;
            this.Detail_treeList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.WordWrap_repositoryItemMemoEdit});
            this.Detail_treeList.Size = new System.Drawing.Size(778, 481);
            this.Detail_treeList.TabIndex = 8;
            // 
            // Title_treeListColumn
            // 
            this.Title_treeListColumn.AppearanceCell.Options.UseTextOptions = true;
            this.Title_treeListColumn.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.Title_treeListColumn.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.Title_treeListColumn.AppearanceHeader.Options.UseTextOptions = true;
            this.Title_treeListColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Title_treeListColumn.Caption = "Наименование";
            this.Title_treeListColumn.FieldName = "Наименование";
            this.Title_treeListColumn.Name = "Title_treeListColumn";
            this.Title_treeListColumn.OptionsColumn.AllowEdit = false;
            this.Title_treeListColumn.OptionsColumn.AllowMove = false;
            this.Title_treeListColumn.OptionsColumn.ReadOnly = true;
            this.Title_treeListColumn.Visible = true;
            this.Title_treeListColumn.VisibleIndex = 0;
            this.Title_treeListColumn.Width = 64;
            // 
            // Value_treeListColumn
            // 
            this.Value_treeListColumn.AppearanceCell.Options.UseTextOptions = true;
            this.Value_treeListColumn.AppearanceCell.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.Value_treeListColumn.AppearanceHeader.Options.UseTextOptions = true;
            this.Value_treeListColumn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.Value_treeListColumn.Caption = "Значение";
            this.Value_treeListColumn.ColumnEdit = this.WordWrap_repositoryItemMemoEdit;
            this.Value_treeListColumn.FieldName = "Значение";
            this.Value_treeListColumn.Name = "Value_treeListColumn";
            this.Value_treeListColumn.OptionsColumn.AllowMove = false;
            this.Value_treeListColumn.OptionsColumn.ReadOnly = true;
            this.Value_treeListColumn.Visible = true;
            this.Value_treeListColumn.VisibleIndex = 1;
            // 
            // WordWrap_repositoryItemMemoEdit
            // 
            this.WordWrap_repositoryItemMemoEdit.Name = "WordWrap_repositoryItemMemoEdit";
            // 
            // ExpandTree_simpleButton
            // 
            this.ExpandTree_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ExpandTree_simpleButton.ImageOptions.ImageIndex = 10;
            this.ExpandTree_simpleButton.ImageOptions.ImageList = this.Editor16_imageCollection;
            this.ExpandTree_simpleButton.Location = new System.Drawing.Point(576, 18);
            this.ExpandTree_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ExpandTree_simpleButton.Name = "ExpandTree_simpleButton";
            this.ExpandTree_simpleButton.Size = new System.Drawing.Size(27, 28);
            this.ExpandTree_simpleButton.TabIndex = 4;
            this.ExpandTree_simpleButton.ToolTip = "Свернуть все дочерние ветки документа.";
            this.ExpandTree_simpleButton.ToolTipTitle = "Свернуть";
            this.ExpandTree_simpleButton.Click += new System.EventHandler(this.ExpandTree_simpleButton_Click);
            // 
            // CollapseTree_simpleButton
            // 
            this.CollapseTree_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CollapseTree_simpleButton.ImageOptions.ImageIndex = 11;
            this.CollapseTree_simpleButton.ImageOptions.ImageList = this.Editor16_imageCollection;
            this.CollapseTree_simpleButton.Location = new System.Drawing.Point(610, 18);
            this.CollapseTree_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CollapseTree_simpleButton.Name = "CollapseTree_simpleButton";
            this.CollapseTree_simpleButton.Size = new System.Drawing.Size(27, 28);
            this.CollapseTree_simpleButton.TabIndex = 5;
            this.CollapseTree_simpleButton.ToolTip = "Свернуть все дочерние ветки документа.";
            this.CollapseTree_simpleButton.ToolTipTitle = "Свернуть";
            this.CollapseTree_simpleButton.Click += new System.EventHandler(this.CollapseTree_simpleButton_Click);
            // 
            // Print_simpleButton
            // 
            this.Print_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Print_simpleButton.ImageOptions.ImageIndex = 9;
            this.Print_simpleButton.ImageOptions.ImageList = this.Editor16_imageCollection;
            this.Print_simpleButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftTop;
            this.Print_simpleButton.Location = new System.Drawing.Point(644, 18);
            this.Print_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Print_simpleButton.Name = "Print_simpleButton";
            this.Print_simpleButton.Size = new System.Drawing.Size(149, 28);
            this.Print_simpleButton.TabIndex = 7;
            this.Print_simpleButton.Text = "Печать";
            this.Print_simpleButton.ToolTip = "Открыть диалог предварительного просмотра документа перед печатью.";
            this.Print_simpleButton.ToolTipTitle = "Печать";
            this.Print_simpleButton.Click += new System.EventHandler(this.Print_simpleButton_Click);
            // 
            // CaptionB_labelControl
            // 
            this.CaptionB_labelControl.Location = new System.Drawing.Point(15, 25);
            this.CaptionB_labelControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CaptionB_labelControl.Name = "CaptionB_labelControl";
            this.CaptionB_labelControl.Size = new System.Drawing.Size(371, 16);
            this.CaptionB_labelControl.TabIndex = 6;
            this.CaptionB_labelControl.Text = "Подробная информация о акте (включая служебные данные):";
            // 
            // Ttn_pictureEdit
            // 
            this.Ttn_pictureEdit.Cursor = System.Windows.Forms.Cursors.Default;
            this.Ttn_pictureEdit.EditValue = ((object)(resources.GetObject("Ttn_pictureEdit.EditValue")));
            this.Ttn_pictureEdit.Location = new System.Drawing.Point(9, 10);
            this.Ttn_pictureEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Ttn_pictureEdit.Name = "Ttn_pictureEdit";
            this.Ttn_pictureEdit.Properties.AllowFocused = false;
            this.Ttn_pictureEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Ttn_pictureEdit.Properties.Appearance.Options.UseBackColor = true;
            this.Ttn_pictureEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Ttn_pictureEdit.Properties.ReadOnly = true;
            this.Ttn_pictureEdit.Properties.ShowMenu = false;
            this.Ttn_pictureEdit.Size = new System.Drawing.Size(69, 76);
            this.Ttn_pictureEdit.TabIndex = 4;
            // 
            // SaveAndClose_simpleButton
            // 
            this.SaveAndClose_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveAndClose_simpleButton.ImageOptions.ImageIndex = 5;
            this.SaveAndClose_simpleButton.ImageOptions.ImageList = this.Editor16_imageCollection;
            this.SaveAndClose_simpleButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.SaveAndClose_simpleButton.Location = new System.Drawing.Point(568, 608);
            this.SaveAndClose_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SaveAndClose_simpleButton.Name = "SaveAndClose_simpleButton";
            this.SaveAndClose_simpleButton.Size = new System.Drawing.Size(187, 33);
            this.SaveAndClose_simpleButton.TabIndex = 1;
            this.SaveAndClose_simpleButton.Text = "Сохранить и закрыть";
            this.SaveAndClose_simpleButton.Click += new System.EventHandler(this.SaveAndClose_simpleButton_Click);
            // 
            // ActFixBarcodeEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Close_simpleButton;
            this.ClientSize = new System.Drawing.Size(967, 654);
            this.Controls.Add(this.SaveAndClose_simpleButton);
            this.Controls.Add(this.Main_xtraTabControl);
            this.Controls.Add(this.Ttn_pictureEdit);
            this.Controls.Add(this.Close_simpleButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 570);
            this.Name = "ActFixBarcodeEditorForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактирование акта списания товара со склада организации";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ActFixBarcodeEditorForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ActFixBarcodeEditorForm_FormClosed);
            this.Load += new System.EventHandler(this.ActFixBarcodeEditorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barcodesView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deleteItemButtonEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Position_gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Position_bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Position_gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Status_repositoryItemImageComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Editor16_imageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WordWrapTo_repositoryItemMemoEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Decimal_repositoryItemSpinEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Main_xtraTabControl)).EndInit();
            this.Main_xtraTabControl.ResumeLayout(false);
            this.Common_xtraTabPage.ResumeLayout(false);
            this.Common_xtraTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Attention_pictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date_dateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Date_dateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Status_textEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Identity_textEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Note_textEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Number_textEdit.Properties)).EndInit();
            this.Content_xtraTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Content_splitContainerControl)).EndInit();
            this.Content_splitContainerControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Source_gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SourcePosition_bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Source_gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WordWrapRests_repositoryItemMemoEdit)).EndInit();
            this.Tree_xtraTabPage.ResumeLayout(false);
            this.Tree_xtraTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Detail_treeList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WordWrap_repositoryItemMemoEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ttn_pictureEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton Close_simpleButton;
        private DevExpress.XtraTab.XtraTabControl Main_xtraTabControl;
        private DevExpress.XtraTab.XtraTabPage Common_xtraTabPage;
        private DevExpress.XtraEditors.PictureEdit Ttn_pictureEdit;
        private DevExpress.XtraEditors.DateEdit Date_dateEdit;
        private DevExpress.XtraEditors.LabelControl Identity_labelControl;
        private DevExpress.XtraEditors.LabelControl Date_labelControl;
        private DevExpress.XtraEditors.TextEdit Identity_textEdit;
        private DevExpress.XtraEditors.LabelControl Number_labelControl;
        private DevExpress.XtraEditors.TextEdit Number_textEdit;
        private DevExpress.XtraTab.XtraTabPage Content_xtraTabPage;
        private DevExpress.XtraEditors.LabelControl Note_labelControl;
        private DevExpress.XtraEditors.TextEdit Note_textEdit;
        private DevExpress.XtraEditors.LabelControl Status_labelControl;
        private DevExpress.XtraEditors.TextEdit Status_textEdit;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraGrid.GridControl Position_gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView Position_gridView;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox Status_repositoryItemImageComboBox;
        private DevExpress.XtraEditors.SimpleButton Remove_simpleButton;
        private DevExpress.Utils.ImageCollection Editor16_imageCollection;
        private DevExpress.XtraEditors.SimpleButton Add_simpleButton;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit WordWrapTo_repositoryItemMemoEdit;
        private DevExpress.XtraEditors.SplitContainerControl Content_splitContainerControl;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.SimpleButton SaveAndClose_simpleButton;
        private DevExpress.XtraGrid.Columns.GridColumn colIdentity;
        private DevExpress.XtraGrid.Columns.GridColumn colTitle;
        private DevExpress.XtraGrid.Columns.GridColumn colProducerTitle;
        private DevExpress.XtraGrid.Columns.GridColumn colAlcoCode;
        private DevExpress.XtraGrid.Columns.GridColumn colCapacityOut;
        private DevExpress.XtraGrid.Columns.GridColumn colVolume;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colFormBRegId;
        private DevExpress.XtraEditors.SimpleButton RefreshPosition_simpleButton;
        private DevExpress.XtraEditors.SimpleButton CheckPosition_simpleButton;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit Decimal_repositoryItemSpinEdit;
        private DevExpress.XtraGrid.Columns.GridColumn colStatusOutPosition;
        private DevExpress.XtraEditors.LabelControl labelControl25;
        private DevExpress.XtraEditors.SimpleButton PrintPosition_simpleButton;
        private DevExpress.XtraEditors.SimpleButton APRefresh_simpleButton;
        private DevExpress.XtraEditors.SimpleButton APFind_simpleButton;
        private DevExpress.XtraEditors.SimpleButton APGroup_simpleButton;
        private DevExpress.XtraEditors.SimpleButton APFilter_simpleButton;
        private DevExpress.XtraEditors.SimpleButton APColumns_simpleButton;
        private DevExpress.XtraEditors.SimpleButton FindPosition_simpleButton;
        private DevExpress.XtraEditors.SimpleButton ColumnsPosition_simpleButton;
        private DevExpress.XtraEditors.PictureEdit Attention_pictureEdit;
        private DevExpress.XtraGrid.Columns.GridColumn colProductVCode1;
        private System.Windows.Forms.BindingSource SourcePosition_bindingSource;
        private DevExpress.XtraGrid.GridControl Source_gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView Source_gridView;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colPositionRestsDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit WordWrapRests_repositoryItemMemoEdit;
        private DevExpress.XtraGrid.Columns.GridColumn colTitle1;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity1;
        private DevExpress.XtraGrid.Columns.GridColumn colFormARegId1;
        private DevExpress.XtraGrid.Columns.GridColumn colFormBRegId1;
        private DevExpress.XtraGrid.Columns.GridColumn colShortName1;
        private DevExpress.XtraGrid.Columns.GridColumn colFullName1;
        private DevExpress.XtraGrid.Columns.GridColumn colAlcoCode1;
        private DevExpress.XtraGrid.Columns.GridColumn colCapacity1;
        private DevExpress.XtraGrid.Columns.GridColumn colVolume1;
        private DevExpress.XtraGrid.Columns.GridColumn colProductVCode2;
        private DevExpress.XtraGrid.Columns.GridColumn colProducer;
        private DevExpress.XtraGrid.Columns.GridColumn colImporter;
        private System.Windows.Forms.BindingSource Position_bindingSource;
        private DevExpress.XtraTab.XtraTabPage Tree_xtraTabPage;
        private DevExpress.XtraTreeList.TreeList Detail_treeList;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Title_treeListColumn;
        private DevExpress.XtraTreeList.Columns.TreeListColumn Value_treeListColumn;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit WordWrap_repositoryItemMemoEdit;
        private DevExpress.XtraEditors.SimpleButton ExpandTree_simpleButton;
        private DevExpress.XtraEditors.SimpleButton CollapseTree_simpleButton;
        private DevExpress.XtraEditors.SimpleButton Print_simpleButton;
        private DevExpress.XtraEditors.LabelControl CaptionB_labelControl;
        private DevExpress.XtraEditors.SimpleButton scanBarcode_simpleButton;
        private DevExpress.XtraGrid.Views.Grid.GridView barcodesView;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit deleteItemButtonEdit;
        private DevExpress.XtraGrid.Columns.GridColumn colLength;
    }
}