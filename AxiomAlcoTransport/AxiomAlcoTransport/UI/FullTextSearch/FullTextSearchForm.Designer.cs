namespace Axiom.AlcoTransport
{
    partial class FullTextSearchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FullTextSearchForm));
            this.Cancel_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Editor16_imageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.Accept_pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.Caption_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.Document_bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Documents_gridControl = new DevExpress.XtraGrid.GridControl();
            this.Documents_gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCreateDateTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDocumentType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.WordWrap_repositoryItemMemoEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAdditionalComment = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.Find_textEdit = new DevExpress.XtraEditors.TextEdit();
            this.Find_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.Editor16_imageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Accept_pictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Document_bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Documents_gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Documents_gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WordWrap_repositoryItemMemoEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Find_textEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // Cancel_simpleButton
            // 
            this.Cancel_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel_simpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_simpleButton.ImageIndex = 3;
            this.Cancel_simpleButton.ImageList = this.Editor16_imageCollection;
            this.Cancel_simpleButton.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.Cancel_simpleButton.Location = new System.Drawing.Point(559, 406);
            this.Cancel_simpleButton.Name = "Cancel_simpleButton";
            this.Cancel_simpleButton.Size = new System.Drawing.Size(128, 27);
            this.Cancel_simpleButton.TabIndex = 3;
            this.Cancel_simpleButton.Text = "Закрыть";
            this.Cancel_simpleButton.Click += new System.EventHandler(this.Cancel_simpleButton_Click);
            // 
            // Editor16_imageCollection
            // 
            this.Editor16_imageCollection.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("Editor16_imageCollection.ImageStream")));
            this.Editor16_imageCollection.Images.SetKeyName(0, "add.png");
            this.Editor16_imageCollection.Images.SetKeyName(1, "add2.png");
            this.Editor16_imageCollection.Images.SetKeyName(2, "delete.png");
            this.Editor16_imageCollection.Images.SetKeyName(3, "delete2.png");
            this.Editor16_imageCollection.Images.SetKeyName(4, "check.png");
            this.Editor16_imageCollection.Images.SetKeyName(5, "check2.png");
            this.Editor16_imageCollection.Images.SetKeyName(6, "find.png");
            // 
            // Accept_pictureEdit
            // 
            this.Accept_pictureEdit.EditValue = ((object)(resources.GetObject("Accept_pictureEdit.EditValue")));
            this.Accept_pictureEdit.Location = new System.Drawing.Point(12, 12);
            this.Accept_pictureEdit.Name = "Accept_pictureEdit";
            this.Accept_pictureEdit.Properties.AllowFocused = false;
            this.Accept_pictureEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Accept_pictureEdit.Properties.Appearance.Options.UseBackColor = true;
            this.Accept_pictureEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Accept_pictureEdit.Properties.ReadOnly = true;
            this.Accept_pictureEdit.Properties.ShowMenu = false;
            this.Accept_pictureEdit.Size = new System.Drawing.Size(59, 62);
            this.Accept_pictureEdit.TabIndex = 4;
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
            this.labelControl4.Location = new System.Drawing.Point(77, 88);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.ShowToolTips = false;
            this.labelControl4.Size = new System.Drawing.Size(609, 13);
            this.labelControl4.TabIndex = 6;
            // 
            // Caption_labelControl
            // 
            this.Caption_labelControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Caption_labelControl.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.Caption_labelControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.Caption_labelControl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.Caption_labelControl.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.Caption_labelControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.Caption_labelControl.Location = new System.Drawing.Point(78, 13);
            this.Caption_labelControl.Name = "Caption_labelControl";
            this.Caption_labelControl.Size = new System.Drawing.Size(609, 69);
            this.Caption_labelControl.TabIndex = 5;
            this.Caption_labelControl.Text = resources.GetString("Caption_labelControl.Text");
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl3.LineVisible = true;
            this.labelControl3.Location = new System.Drawing.Point(77, 387);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.ShowToolTips = false;
            this.labelControl3.Size = new System.Drawing.Size(609, 13);
            this.labelControl3.TabIndex = 6;
            // 
            // Document_bindingSource
            // 
            this.Document_bindingSource.DataSource = typeof(Axiom.AlcoTransport.ADocument);
            // 
            // Documents_gridControl
            // 
            this.Documents_gridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Documents_gridControl.DataSource = this.Document_bindingSource;
            this.Documents_gridControl.EmbeddedNavigator.Buttons.Append.Enabled = false;
            this.Documents_gridControl.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.Documents_gridControl.EmbeddedNavigator.Buttons.CancelEdit.Enabled = false;
            this.Documents_gridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.Documents_gridControl.EmbeddedNavigator.Buttons.Edit.Enabled = false;
            this.Documents_gridControl.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.Documents_gridControl.EmbeddedNavigator.Buttons.EndEdit.Enabled = false;
            this.Documents_gridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.Documents_gridControl.EmbeddedNavigator.Buttons.NextPage.Enabled = false;
            this.Documents_gridControl.EmbeddedNavigator.Buttons.NextPage.Visible = false;
            this.Documents_gridControl.EmbeddedNavigator.Buttons.PrevPage.Enabled = false;
            this.Documents_gridControl.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
            this.Documents_gridControl.EmbeddedNavigator.Buttons.Remove.Enabled = false;
            this.Documents_gridControl.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.Documents_gridControl.EmbeddedNavigator.ShowToolTips = false;
            this.Documents_gridControl.EmbeddedNavigator.TextStringFormat = "Запись {0} из {1}";
            this.Documents_gridControl.Location = new System.Drawing.Point(77, 154);
            this.Documents_gridControl.MainView = this.Documents_gridView;
            this.Documents_gridControl.Name = "Documents_gridControl";
            this.Documents_gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.WordWrap_repositoryItemMemoEdit});
            this.Documents_gridControl.Size = new System.Drawing.Size(610, 231);
            this.Documents_gridControl.TabIndex = 2;
            this.Documents_gridControl.UseEmbeddedNavigator = true;
            this.Documents_gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Documents_gridView});
            // 
            // Documents_gridView
            // 
            this.Documents_gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCreateDateTime,
            this.colDocumentType,
            this.colDescription,
            this.colAdditionalComment});
            this.Documents_gridView.GridControl = this.Documents_gridControl;
            this.Documents_gridView.GroupCount = 1;
            this.Documents_gridView.Name = "Documents_gridView";
            this.Documents_gridView.OptionsBehavior.ReadOnly = true;
            this.Documents_gridView.OptionsLayout.StoreAllOptions = true;
            this.Documents_gridView.OptionsPrint.ExpandAllGroups = false;
            this.Documents_gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.Documents_gridView.OptionsSelection.MultiSelect = true;
            this.Documents_gridView.OptionsView.EnableAppearanceEvenRow = true;
            this.Documents_gridView.OptionsView.RowAutoHeight = true;
            this.Documents_gridView.OptionsView.ShowGroupedColumns = true;
            this.Documents_gridView.OptionsView.ShowGroupPanel = false;
            this.Documents_gridView.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colDocumentType, DevExpress.Data.ColumnSortOrder.Ascending),
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colCreateDateTime, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colCreateDateTime
            // 
            this.colCreateDateTime.AppearanceCell.Options.UseTextOptions = true;
            this.colCreateDateTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreateDateTime.AppearanceHeader.Options.UseTextOptions = true;
            this.colCreateDateTime.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colCreateDateTime.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.colCreateDateTime.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colCreateDateTime.FieldName = "CreateDateTime";
            this.colCreateDateTime.Name = "colCreateDateTime";
            this.colCreateDateTime.OptionsColumn.AllowEdit = false;
            this.colCreateDateTime.OptionsColumn.AllowShowHide = false;
            this.colCreateDateTime.OptionsColumn.ReadOnly = true;
            this.colCreateDateTime.Visible = true;
            this.colCreateDateTime.VisibleIndex = 0;
            this.colCreateDateTime.Width = 161;
            // 
            // colDocumentType
            // 
            this.colDocumentType.AppearanceCell.Options.UseTextOptions = true;
            this.colDocumentType.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDocumentType.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colDocumentType.AppearanceHeader.Options.UseTextOptions = true;
            this.colDocumentType.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDocumentType.ColumnEdit = this.WordWrap_repositoryItemMemoEdit;
            this.colDocumentType.FieldName = "DocumentType";
            this.colDocumentType.Name = "colDocumentType";
            this.colDocumentType.OptionsColumn.AllowShowHide = false;
            this.colDocumentType.OptionsColumn.ReadOnly = true;
            this.colDocumentType.Visible = true;
            this.colDocumentType.VisibleIndex = 1;
            this.colDocumentType.Width = 113;
            // 
            // WordWrap_repositoryItemMemoEdit
            // 
            this.WordWrap_repositoryItemMemoEdit.Name = "WordWrap_repositoryItemMemoEdit";
            // 
            // colDescription
            // 
            this.colDescription.AppearanceHeader.Options.UseTextOptions = true;
            this.colDescription.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colDescription.ColumnEdit = this.WordWrap_repositoryItemMemoEdit;
            this.colDescription.FieldName = "PartiallyTranslatedDescription";
            this.colDescription.Name = "colDescription";
            this.colDescription.OptionsColumn.AllowShowHide = false;
            this.colDescription.OptionsColumn.ReadOnly = true;
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 2;
            this.colDescription.Width = 314;
            // 
            // colAdditionalComment
            // 
            this.colAdditionalComment.AppearanceHeader.Options.UseTextOptions = true;
            this.colAdditionalComment.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colAdditionalComment.ColumnEdit = this.WordWrap_repositoryItemMemoEdit;
            this.colAdditionalComment.FieldName = "AdditionalComment";
            this.colAdditionalComment.Name = "colAdditionalComment";
            this.colAdditionalComment.OptionsColumn.ReadOnly = true;
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl1.LineVisible = true;
            this.labelControl1.Location = new System.Drawing.Point(78, 135);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.ShowToolTips = false;
            this.labelControl1.Size = new System.Drawing.Size(609, 13);
            this.labelControl1.TabIndex = 6;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(77, 114);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(101, 13);
            this.labelControl2.TabIndex = 7;
            this.labelControl2.Text = "Строка для поиска:";
            // 
            // Find_textEdit
            // 
            this.Find_textEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Find_textEdit.Location = new System.Drawing.Point(195, 111);
            this.Find_textEdit.Name = "Find_textEdit";
            this.Find_textEdit.Size = new System.Drawing.Size(393, 20);
            this.Find_textEdit.TabIndex = 0;
            this.Find_textEdit.TextChanged += new System.EventHandler(this.Find_textEdit_TextChanged);
            // 
            // Find_simpleButton
            // 
            this.Find_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Find_simpleButton.Enabled = false;
            this.Find_simpleButton.ImageIndex = 6;
            this.Find_simpleButton.ImageList = this.Editor16_imageCollection;
            this.Find_simpleButton.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftTop;
            this.Find_simpleButton.Location = new System.Drawing.Point(594, 107);
            this.Find_simpleButton.Name = "Find_simpleButton";
            this.Find_simpleButton.Size = new System.Drawing.Size(92, 27);
            this.Find_simpleButton.TabIndex = 1;
            this.Find_simpleButton.Text = "Найти";
            this.Find_simpleButton.Click += new System.EventHandler(this.Find_simpleButton_Click);
            // 
            // FullTextSearchForm
            // 
            this.AcceptButton = this.Find_simpleButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_simpleButton;
            this.ClientSize = new System.Drawing.Size(704, 441);
            this.Controls.Add(this.Find_simpleButton);
            this.Controls.Add(this.Find_textEdit);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.Documents_gridControl);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.Caption_labelControl);
            this.Controls.Add(this.Cancel_simpleButton);
            this.Controls.Add(this.Accept_pictureEdit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(720, 480);
            this.Name = "FullTextSearchForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Полнотекстовый поиск документов";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FullTextSearchForm_FormClosed);
            this.Load += new System.EventHandler(this.FullTextSearchForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Editor16_imageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Accept_pictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Document_bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Documents_gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Documents_gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WordWrap_repositoryItemMemoEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Find_textEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton Cancel_simpleButton;
        private DevExpress.XtraEditors.PictureEdit Accept_pictureEdit;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl Caption_labelControl;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.Utils.ImageCollection Editor16_imageCollection;
        private System.Windows.Forms.BindingSource Document_bindingSource;
        private DevExpress.XtraGrid.GridControl Documents_gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView Documents_gridView;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit Find_textEdit;
        private DevExpress.XtraEditors.SimpleButton Find_simpleButton;
        private DevExpress.XtraGrid.Columns.GridColumn colCreateDateTime;
        private DevExpress.XtraGrid.Columns.GridColumn colDocumentType;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colAdditionalComment;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit WordWrap_repositoryItemMemoEdit;
    }
}