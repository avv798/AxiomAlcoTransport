namespace Axiom.AlcoTransport
{
    partial class WaybillRequestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaybillRequestForm));
            this.Cancel_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Diff_pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.Ok_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Caption_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.Document_BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Document_gridControl = new DevExpress.XtraGrid.GridControl();
            this.Document_gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIdentity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDirect = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShippingDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShipperName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConsigneeName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCreateDateTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colUrl = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Status_repositoryItemImageComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.Act16_imageCollection = new DevExpress.Utils.ImageCollection(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Diff_pictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Document_BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Document_gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Document_gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Status_repositoryItemImageComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Act16_imageCollection)).BeginInit();
            this.SuspendLayout();
            // 
            // Cancel_simpleButton
            // 
            this.Cancel_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel_simpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_simpleButton.Location = new System.Drawing.Point(639, 346);
            this.Cancel_simpleButton.Name = "Cancel_simpleButton";
            this.Cancel_simpleButton.Size = new System.Drawing.Size(128, 23);
            this.Cancel_simpleButton.TabIndex = 3;
            this.Cancel_simpleButton.Text = "Отмена";
            this.Cancel_simpleButton.Click += new System.EventHandler(this.Cancel_simpleButton_Click);
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
            // Ok_simpleButton
            // 
            this.Ok_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Ok_simpleButton.Location = new System.Drawing.Point(505, 346);
            this.Ok_simpleButton.Name = "Ok_simpleButton";
            this.Ok_simpleButton.Size = new System.Drawing.Size(128, 23);
            this.Ok_simpleButton.TabIndex = 2;
            this.Ok_simpleButton.Text = "Выбрать";
            this.Ok_simpleButton.Click += new System.EventHandler(this.Ok_simpleButton_Click);
            // 
            // Caption_labelControl
            // 
            this.Caption_labelControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Caption_labelControl.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Caption_labelControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.Caption_labelControl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.Caption_labelControl.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.Caption_labelControl.Location = new System.Drawing.Point(78, 13);
            this.Caption_labelControl.Name = "Caption_labelControl";
            this.Caption_labelControl.Size = new System.Drawing.Size(393, 13);
            this.Caption_labelControl.TabIndex = 5;
            this.Caption_labelControl.Text = "Выберите нужную накладную для получения справки формы \'X\'...";
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl2.LineVisible = true;
            this.labelControl2.Location = new System.Drawing.Point(78, 327);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.ShowToolTips = false;
            this.labelControl2.Size = new System.Drawing.Size(689, 13);
            this.labelControl2.TabIndex = 6;
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl4.LineVisible = true;
            this.labelControl4.Location = new System.Drawing.Point(77, 32);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.ShowToolTips = false;
            this.labelControl4.Size = new System.Drawing.Size(689, 13);
            this.labelControl4.TabIndex = 6;
            // 
            // Document_BindingSource
            // 
            this.Document_BindingSource.DataSource = typeof(Axiom.AlcoTransport.AWaybill);
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
            this.Document_gridControl.Location = new System.Drawing.Point(77, 51);
            this.Document_gridControl.MainView = this.Document_gridView;
            this.Document_gridControl.Name = "Document_gridControl";
            this.Document_gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.Status_repositoryItemImageComboBox});
            this.Document_gridControl.Size = new System.Drawing.Size(689, 270);
            this.Document_gridControl.TabIndex = 8;
            this.Document_gridControl.UseEmbeddedNavigator = true;
            this.Document_gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Document_gridView});
            // 
            // Document_gridView
            // 
            this.Document_gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIdentity,
            this.colNumber,
            this.colDate,
            this.colDirect,
            this.colShippingDate,
            this.colShipperName,
            this.colConsigneeName,
            this.colCreateDateTime,
            this.colUrl});
            this.Document_gridView.GridControl = this.Document_gridControl;
            this.Document_gridView.Name = "Document_gridView";
            this.Document_gridView.OptionsBehavior.ReadOnly = true;
            this.Document_gridView.OptionsLayout.StoreAllOptions = true;
            this.Document_gridView.OptionsPrint.ExpandAllGroups = false;
            this.Document_gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.Document_gridView.OptionsView.EnableAppearanceEvenRow = true;
            this.Document_gridView.OptionsView.ShowGroupPanel = false;
            this.Document_gridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colCreateDateTime, DevExpress.Data.ColumnSortOrder.Descending)});
            // 
            // colIdentity
            // 
            this.colIdentity.AppearanceCell.Options.UseTextOptions = true;
            this.colIdentity.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIdentity.AppearanceHeader.Options.UseTextOptions = true;
            this.colIdentity.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colIdentity.FieldName = "Identity";
            this.colIdentity.Name = "colIdentity";
            this.colIdentity.OptionsColumn.ReadOnly = true;
            this.colIdentity.Visible = true;
            this.colIdentity.VisibleIndex = 1;
            // 
            // colNumber
            // 
            this.colNumber.AppearanceCell.Options.UseTextOptions = true;
            this.colNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNumber.AppearanceHeader.Options.UseTextOptions = true;
            this.colNumber.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNumber.FieldName = "Number";
            this.colNumber.Name = "colNumber";
            this.colNumber.OptionsColumn.ReadOnly = true;
            this.colNumber.Visible = true;
            this.colNumber.VisibleIndex = 3;
            // 
            // colDate
            // 
            this.colDate.AppearanceCell.Options.UseTextOptions = true;
            this.colDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDate.FieldName = "Date";
            this.colDate.Name = "colDate";
            this.colDate.OptionsColumn.ReadOnly = true;
            // 
            // colDirect
            // 
            this.colDirect.AppearanceCell.Options.UseTextOptions = true;
            this.colDirect.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDirect.AppearanceHeader.Options.UseTextOptions = true;
            this.colDirect.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDirect.FieldName = "Direct";
            this.colDirect.Name = "colDirect";
            this.colDirect.Visible = true;
            this.colDirect.VisibleIndex = 2;
            // 
            // colShippingDate
            // 
            this.colShippingDate.AppearanceCell.Options.UseTextOptions = true;
            this.colShippingDate.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colShippingDate.AppearanceHeader.Options.UseTextOptions = true;
            this.colShippingDate.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colShippingDate.FieldName = "ShippingDate";
            this.colShippingDate.Name = "colShippingDate";
            this.colShippingDate.OptionsColumn.ReadOnly = true;
            // 
            // colShipperName
            // 
            this.colShipperName.AppearanceCell.Options.UseTextOptions = true;
            this.colShipperName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colShipperName.AppearanceHeader.Options.UseTextOptions = true;
            this.colShipperName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colShipperName.FieldName = "ShipperName";
            this.colShipperName.Name = "colShipperName";
            this.colShipperName.OptionsColumn.ReadOnly = true;
            this.colShipperName.Visible = true;
            this.colShipperName.VisibleIndex = 4;
            // 
            // colConsigneeName
            // 
            this.colConsigneeName.AppearanceCell.Options.UseTextOptions = true;
            this.colConsigneeName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colConsigneeName.AppearanceHeader.Options.UseTextOptions = true;
            this.colConsigneeName.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colConsigneeName.FieldName = "ConsigneeName";
            this.colConsigneeName.Name = "colConsigneeName";
            this.colConsigneeName.OptionsColumn.ReadOnly = true;
            this.colConsigneeName.Visible = true;
            this.colConsigneeName.VisibleIndex = 5;
            // 
            // colCreateDateTime
            // 
            this.colCreateDateTime.AppearanceCell.Options.UseTextOptions = true;
            this.colCreateDateTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreateDateTime.AppearanceHeader.Options.UseTextOptions = true;
            this.colCreateDateTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreateDateTime.DisplayFormat.FormatString = "dd MMMM yyyy, HH:mm:ss";
            this.colCreateDateTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colCreateDateTime.FieldName = "CreateDateTime";
            this.colCreateDateTime.Name = "colCreateDateTime";
            this.colCreateDateTime.OptionsColumn.ReadOnly = true;
            this.colCreateDateTime.Visible = true;
            this.colCreateDateTime.VisibleIndex = 0;
            // 
            // colUrl
            // 
            this.colUrl.AppearanceCell.Options.UseTextOptions = true;
            this.colUrl.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUrl.AppearanceHeader.Options.UseTextOptions = true;
            this.colUrl.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colUrl.FieldName = "Url";
            this.colUrl.Name = "colUrl";
            this.colUrl.OptionsColumn.ReadOnly = true;
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
            // Act16_imageCollection
            // 
            this.Act16_imageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("Act16_imageCollection.ImageStream")));
            this.Act16_imageCollection.Images.SetKeyName(0, "down_minus.png");
            this.Act16_imageCollection.Images.SetKeyName(1, "up_plus.png");
            // 
            // WaybillRequestForm
            // 
            this.AcceptButton = this.Ok_simpleButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_simpleButton;
            this.ClientSize = new System.Drawing.Size(784, 381);
            this.Controls.Add(this.Document_gridControl);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.Caption_labelControl);
            this.Controls.Add(this.Ok_simpleButton);
            this.Controls.Add(this.Cancel_simpleButton);
            this.Controls.Add(this.Diff_pictureEdit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 420);
            this.Name = "WaybillRequestForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Выбор товарно-транспортной накладной";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WaybillRequestForm_FormClosed);
            this.Load += new System.EventHandler(this.WaybillRequestForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Diff_pictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Document_BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Document_gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Document_gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Status_repositoryItemImageComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Act16_imageCollection)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton Cancel_simpleButton;
        private DevExpress.XtraEditors.PictureEdit Diff_pictureEdit;
        private DevExpress.XtraEditors.SimpleButton Ok_simpleButton;
        private DevExpress.XtraEditors.LabelControl Caption_labelControl;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private System.Windows.Forms.BindingSource Document_BindingSource;
        private DevExpress.XtraGrid.GridControl Document_gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView Document_gridView;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox Status_repositoryItemImageComboBox;
        private DevExpress.Utils.ImageCollection Act16_imageCollection;
        private DevExpress.XtraGrid.Columns.GridColumn colIdentity;
        private DevExpress.XtraGrid.Columns.GridColumn colNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colDate;
        private DevExpress.XtraGrid.Columns.GridColumn colShippingDate;
        private DevExpress.XtraGrid.Columns.GridColumn colShipperName;
        private DevExpress.XtraGrid.Columns.GridColumn colConsigneeName;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDateTime;
        private DevExpress.XtraGrid.Columns.GridColumn colUrl;
        private DevExpress.XtraGrid.Columns.GridColumn colDirect;
    }
}