namespace Axiom.AlcoTransport
{
    partial class MarksRequestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarksRequestForm));
            this.Cancel_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Editor16_imageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.Accept_pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.Ok_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.Caption_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.Add_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Remove_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Document_bindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Marks_gridControl = new DevExpress.XtraGrid.GridControl();
            this.Marks_gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMarkTypeCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMarkTypeTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.WordWrapTo_repositoryItemMemoEdit = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            this.colSeries = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Status_repositoryItemImageComboBox = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.Decimal_repositoryItemSpinEdit = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.Editor16_imageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Accept_pictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Document_bindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Marks_gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Marks_gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.WordWrapTo_repositoryItemMemoEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Status_repositoryItemImageComboBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Decimal_repositoryItemSpinEdit)).BeginInit();
            this.SuspendLayout();
            // 
            // Cancel_simpleButton
            // 
            this.Cancel_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel_simpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_simpleButton.ImageOptions.ImageIndex = 3;
            this.Cancel_simpleButton.ImageOptions.ImageList = this.Editor16_imageCollection;
            this.Cancel_simpleButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.Cancel_simpleButton.Location = new System.Drawing.Point(559, 450);
            this.Cancel_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Cancel_simpleButton.Name = "Cancel_simpleButton";
            this.Cancel_simpleButton.Size = new System.Drawing.Size(149, 33);
            this.Cancel_simpleButton.TabIndex = 2;
            this.Cancel_simpleButton.Text = "Отмена";
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
            // 
            // Accept_pictureEdit
            // 
            this.Accept_pictureEdit.EditValue = ((object)(resources.GetObject("Accept_pictureEdit.EditValue")));
            this.Accept_pictureEdit.Location = new System.Drawing.Point(14, 15);
            this.Accept_pictureEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Accept_pictureEdit.Name = "Accept_pictureEdit";
            this.Accept_pictureEdit.Properties.AllowFocused = false;
            this.Accept_pictureEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Accept_pictureEdit.Properties.Appearance.Options.UseBackColor = true;
            this.Accept_pictureEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Accept_pictureEdit.Properties.ReadOnly = true;
            this.Accept_pictureEdit.Properties.ShowMenu = false;
            this.Accept_pictureEdit.Size = new System.Drawing.Size(69, 76);
            this.Accept_pictureEdit.TabIndex = 4;
            // 
            // Ok_simpleButton
            // 
            this.Ok_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Ok_simpleButton.ImageOptions.ImageIndex = 5;
            this.Ok_simpleButton.ImageOptions.ImageList = this.Editor16_imageCollection;
            this.Ok_simpleButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.Ok_simpleButton.Location = new System.Drawing.Point(402, 450);
            this.Ok_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Ok_simpleButton.Name = "Ok_simpleButton";
            this.Ok_simpleButton.Size = new System.Drawing.Size(149, 33);
            this.Ok_simpleButton.TabIndex = 1;
            this.Ok_simpleButton.Text = "Ок";
            this.Ok_simpleButton.Click += new System.EventHandler(this.Ok_simpleButton_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl4.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl4.Appearance.Options.UseFont = true;
            this.labelControl4.Appearance.Options.UseTextOptions = true;
            this.labelControl4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl4.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl4.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl4.LineVisible = true;
            this.labelControl4.Location = new System.Drawing.Point(90, 108);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.ShowToolTips = false;
            this.labelControl4.Size = new System.Drawing.Size(617, 16);
            this.labelControl4.TabIndex = 6;
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
            this.Caption_labelControl.Location = new System.Drawing.Point(91, 16);
            this.Caption_labelControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Caption_labelControl.Name = "Caption_labelControl";
            this.Caption_labelControl.Size = new System.Drawing.Size(617, 85);
            this.Caption_labelControl.TabIndex = 5;
            this.Caption_labelControl.Text = resources.GetString("Caption_labelControl.Text");
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl3.LineVisible = true;
            this.labelControl3.Location = new System.Drawing.Point(90, 427);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.ShowToolTips = false;
            this.labelControl3.Size = new System.Drawing.Size(617, 16);
            this.labelControl3.TabIndex = 6;
            // 
            // Add_simpleButton
            // 
            this.Add_simpleButton.ImageOptions.ImageIndex = 0;
            this.Add_simpleButton.ImageOptions.ImageList = this.Editor16_imageCollection;
            this.Add_simpleButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.Add_simpleButton.Location = new System.Drawing.Point(90, 132);
            this.Add_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Add_simpleButton.Name = "Add_simpleButton";
            this.Add_simpleButton.Size = new System.Drawing.Size(112, 31);
            this.Add_simpleButton.TabIndex = 3;
            this.Add_simpleButton.Text = "Добавить";
            this.Add_simpleButton.Click += new System.EventHandler(this.Add_simpleButton_Click);
            // 
            // Remove_simpleButton
            // 
            this.Remove_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Remove_simpleButton.ImageOptions.ImageIndex = 2;
            this.Remove_simpleButton.ImageOptions.ImageList = this.Editor16_imageCollection;
            this.Remove_simpleButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.Remove_simpleButton.Location = new System.Drawing.Point(596, 132);
            this.Remove_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Remove_simpleButton.Name = "Remove_simpleButton";
            this.Remove_simpleButton.Size = new System.Drawing.Size(112, 31);
            this.Remove_simpleButton.TabIndex = 4;
            this.Remove_simpleButton.Text = "Удалить";
            this.Remove_simpleButton.Click += new System.EventHandler(this.Remove_simpleButton_Click);
            // 
            // Document_bindingSource
            // 
            this.Document_bindingSource.DataSource = typeof(Axiom.AlcoTransport.StateMark);
            // 
            // Marks_gridControl
            // 
            this.Marks_gridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Marks_gridControl.DataSource = this.Document_bindingSource;
            this.Marks_gridControl.EmbeddedNavigator.Buttons.Append.Enabled = false;
            this.Marks_gridControl.EmbeddedNavigator.Buttons.Append.Visible = false;
            this.Marks_gridControl.EmbeddedNavigator.Buttons.CancelEdit.Enabled = false;
            this.Marks_gridControl.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
            this.Marks_gridControl.EmbeddedNavigator.Buttons.Edit.Enabled = false;
            this.Marks_gridControl.EmbeddedNavigator.Buttons.Edit.Visible = false;
            this.Marks_gridControl.EmbeddedNavigator.Buttons.EndEdit.Enabled = false;
            this.Marks_gridControl.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            this.Marks_gridControl.EmbeddedNavigator.Buttons.NextPage.Enabled = false;
            this.Marks_gridControl.EmbeddedNavigator.Buttons.NextPage.Visible = false;
            this.Marks_gridControl.EmbeddedNavigator.Buttons.PrevPage.Enabled = false;
            this.Marks_gridControl.EmbeddedNavigator.Buttons.PrevPage.Visible = false;
            this.Marks_gridControl.EmbeddedNavigator.Buttons.Remove.Enabled = false;
            this.Marks_gridControl.EmbeddedNavigator.Buttons.Remove.Visible = false;
            this.Marks_gridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Marks_gridControl.EmbeddedNavigator.ShowToolTips = false;
            this.Marks_gridControl.EmbeddedNavigator.TextStringFormat = "Запись {0} из {1}";
            this.Marks_gridControl.Location = new System.Drawing.Point(90, 172);
            this.Marks_gridControl.MainView = this.Marks_gridView;
            this.Marks_gridControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Marks_gridControl.Name = "Marks_gridControl";
            this.Marks_gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.Status_repositoryItemImageComboBox,
            this.WordWrapTo_repositoryItemMemoEdit,
            this.Decimal_repositoryItemSpinEdit});
            this.Marks_gridControl.Size = new System.Drawing.Size(618, 252);
            this.Marks_gridControl.TabIndex = 0;
            this.Marks_gridControl.UseEmbeddedNavigator = true;
            this.Marks_gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.Marks_gridView});
            // 
            // Marks_gridView
            // 
            this.Marks_gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMarkTypeCode,
            this.colMarkTypeTitle,
            this.colSeries,
            this.colNumber});
            this.Marks_gridView.GridControl = this.Marks_gridControl;
            this.Marks_gridView.Name = "Marks_gridView";
            this.Marks_gridView.OptionsBehavior.ReadOnly = true;
            this.Marks_gridView.OptionsLayout.StoreAllOptions = true;
            this.Marks_gridView.OptionsPrint.ExpandAllGroups = false;
            this.Marks_gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.Marks_gridView.OptionsView.EnableAppearanceEvenRow = true;
            this.Marks_gridView.OptionsView.RowAutoHeight = true;
            this.Marks_gridView.OptionsView.ShowFooter = true;
            this.Marks_gridView.OptionsView.ShowGroupPanel = false;
            // 
            // colMarkTypeCode
            // 
            this.colMarkTypeCode.AppearanceCell.Options.UseTextOptions = true;
            this.colMarkTypeCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMarkTypeCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colMarkTypeCode.AppearanceHeader.Options.UseTextOptions = true;
            this.colMarkTypeCode.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMarkTypeCode.FieldName = "MarkTypeCode";
            this.colMarkTypeCode.Name = "colMarkTypeCode";
            this.colMarkTypeCode.OptionsColumn.ReadOnly = true;
            this.colMarkTypeCode.Visible = true;
            this.colMarkTypeCode.VisibleIndex = 0;
            // 
            // colMarkTypeTitle
            // 
            this.colMarkTypeTitle.AppearanceCell.Options.UseTextOptions = true;
            this.colMarkTypeTitle.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colMarkTypeTitle.AppearanceHeader.Options.UseTextOptions = true;
            this.colMarkTypeTitle.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colMarkTypeTitle.ColumnEdit = this.WordWrapTo_repositoryItemMemoEdit;
            this.colMarkTypeTitle.FieldName = "MarkTypeTitle";
            this.colMarkTypeTitle.Name = "colMarkTypeTitle";
            this.colMarkTypeTitle.OptionsColumn.ReadOnly = true;
            this.colMarkTypeTitle.Visible = true;
            this.colMarkTypeTitle.VisibleIndex = 1;
            this.colMarkTypeTitle.Width = 244;
            // 
            // WordWrapTo_repositoryItemMemoEdit
            // 
            this.WordWrapTo_repositoryItemMemoEdit.Name = "WordWrapTo_repositoryItemMemoEdit";
            // 
            // colSeries
            // 
            this.colSeries.AppearanceCell.Options.UseTextOptions = true;
            this.colSeries.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSeries.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colSeries.AppearanceHeader.Options.UseTextOptions = true;
            this.colSeries.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colSeries.FieldName = "Series";
            this.colSeries.Name = "colSeries";
            this.colSeries.OptionsColumn.ReadOnly = true;
            this.colSeries.Visible = true;
            this.colSeries.VisibleIndex = 2;
            this.colSeries.Width = 64;
            // 
            // colNumber
            // 
            this.colNumber.AppearanceCell.Options.UseTextOptions = true;
            this.colNumber.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNumber.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colNumber.AppearanceHeader.Options.UseTextOptions = true;
            this.colNumber.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colNumber.FieldName = "Number";
            this.colNumber.Name = "colNumber";
            this.colNumber.OptionsColumn.ReadOnly = true;
            this.colNumber.Visible = true;
            this.colNumber.VisibleIndex = 3;
            this.colNumber.Width = 128;
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
            // MarksRequestForm
            // 
            this.AcceptButton = this.Ok_simpleButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_simpleButton;
            this.ClientSize = new System.Drawing.Size(728, 494);
            this.Controls.Add(this.Marks_gridControl);
            this.Controls.Add(this.Remove_simpleButton);
            this.Controls.Add(this.Add_simpleButton);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.Caption_labelControl);
            this.Controls.Add(this.Ok_simpleButton);
            this.Controls.Add(this.Cancel_simpleButton);
            this.Controls.Add(this.Accept_pictureEdit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 440);
            this.Name = "MarksRequestForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Список марок для получения штрих-кодов";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WaybillMarksRequestForm_FormClosed);
            this.Load += new System.EventHandler(this.WaybillMarksRequestForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Editor16_imageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Accept_pictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Document_bindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Marks_gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Marks_gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.WordWrapTo_repositoryItemMemoEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Status_repositoryItemImageComboBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Decimal_repositoryItemSpinEdit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton Cancel_simpleButton;
        private DevExpress.XtraEditors.PictureEdit Accept_pictureEdit;
        private DevExpress.XtraEditors.SimpleButton Ok_simpleButton;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl Caption_labelControl;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.Utils.ImageCollection Editor16_imageCollection;
        private DevExpress.XtraEditors.SimpleButton Add_simpleButton;
        private DevExpress.XtraEditors.SimpleButton Remove_simpleButton;
        private System.Windows.Forms.BindingSource Document_bindingSource;
        private DevExpress.XtraGrid.GridControl Marks_gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView Marks_gridView;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit WordWrapTo_repositoryItemMemoEdit;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox Status_repositoryItemImageComboBox;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit Decimal_repositoryItemSpinEdit;
        private DevExpress.XtraGrid.Columns.GridColumn colMarkTypeCode;
        private DevExpress.XtraGrid.Columns.GridColumn colMarkTypeTitle;
        private DevExpress.XtraGrid.Columns.GridColumn colSeries;
        private DevExpress.XtraGrid.Columns.GridColumn colNumber;
    }
}