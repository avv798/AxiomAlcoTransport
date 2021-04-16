namespace Axiom.AlcoTransport
{
    partial class MarkEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarkEditorForm));
            this.Cancel_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Editor16_imageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.Accept_pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.Ok_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.Caption_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.Type_comboBoxEdit = new DevExpress.XtraEditors.ComboBoxEdit();
            this.Type_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.Base_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.Series_textEdit = new DevExpress.XtraEditors.TextEdit();
            this.Note_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.Number_textEdit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.Editor16_imageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Accept_pictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Type_comboBoxEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Series_textEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Number_textEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // Cancel_simpleButton
            // 
            this.Cancel_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel_simpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_simpleButton.ImageOptions.ImageIndex = 3;
            this.Cancel_simpleButton.ImageOptions.ImageList = this.Editor16_imageCollection;
            this.Cancel_simpleButton.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.Cancel_simpleButton.Location = new System.Drawing.Point(302, 241);
            this.Cancel_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Cancel_simpleButton.Name = "Cancel_simpleButton";
            this.Cancel_simpleButton.Size = new System.Drawing.Size(149, 33);
            this.Cancel_simpleButton.TabIndex = 4;
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
            this.Ok_simpleButton.Location = new System.Drawing.Point(146, 241);
            this.Ok_simpleButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Ok_simpleButton.Name = "Ok_simpleButton";
            this.Ok_simpleButton.Size = new System.Drawing.Size(149, 33);
            this.Ok_simpleButton.TabIndex = 3;
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
            this.labelControl4.Location = new System.Drawing.Point(89, 75);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.ShowToolTips = false;
            this.labelControl4.Size = new System.Drawing.Size(360, 16);
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
            this.Caption_labelControl.Size = new System.Drawing.Size(360, 62);
            this.Caption_labelControl.TabIndex = 5;
            this.Caption_labelControl.Text = "Укажите тип марки - акцизная марка (АМ) или федеральная специальная марка (ФСМ), " +
    "а также её серию и номер.";
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl3.LineVisible = true;
            this.labelControl3.Location = new System.Drawing.Point(89, 204);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.ShowToolTips = false;
            this.labelControl3.Size = new System.Drawing.Size(362, 16);
            this.labelControl3.TabIndex = 6;
            // 
            // Type_comboBoxEdit
            // 
            this.Type_comboBoxEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Type_comboBoxEdit.EditValue = "";
            this.Type_comboBoxEdit.Location = new System.Drawing.Point(190, 95);
            this.Type_comboBoxEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Type_comboBoxEdit.Name = "Type_comboBoxEdit";
            this.Type_comboBoxEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.Type_comboBoxEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Type_comboBoxEdit.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.Type_comboBoxEdit.Size = new System.Drawing.Size(259, 23);
            this.Type_comboBoxEdit.TabIndex = 0;
            this.Type_comboBoxEdit.EditValueChanged += new System.EventHandler(this.Type_comboBoxEdit_EditValueChanged);
            // 
            // Type_labelControl
            // 
            this.Type_labelControl.Location = new System.Drawing.Point(89, 98);
            this.Type_labelControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Type_labelControl.Name = "Type_labelControl";
            this.Type_labelControl.Size = new System.Drawing.Size(66, 16);
            this.Type_labelControl.TabIndex = 10;
            this.Type_labelControl.Text = "Тип марки:";
            // 
            // Base_labelControl
            // 
            this.Base_labelControl.Location = new System.Drawing.Point(89, 149);
            this.Base_labelControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Base_labelControl.Name = "Base_labelControl";
            this.Base_labelControl.Size = new System.Drawing.Size(80, 16);
            this.Base_labelControl.TabIndex = 11;
            this.Base_labelControl.Text = "Серия марки:";
            // 
            // Series_textEdit
            // 
            this.Series_textEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Series_textEdit.Location = new System.Drawing.Point(190, 145);
            this.Series_textEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Series_textEdit.Name = "Series_textEdit";
            this.Series_textEdit.Properties.MaxLength = 3;
            this.Series_textEdit.Size = new System.Drawing.Size(105, 23);
            this.Series_textEdit.TabIndex = 1;
            this.Series_textEdit.TextChanged += new System.EventHandler(this.Series_textEdit_TextChanged);
            // 
            // Note_labelControl
            // 
            this.Note_labelControl.Location = new System.Drawing.Point(89, 181);
            this.Note_labelControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Note_labelControl.Name = "Note_labelControl";
            this.Note_labelControl.Size = new System.Drawing.Size(81, 16);
            this.Note_labelControl.TabIndex = 12;
            this.Note_labelControl.Text = "Номер марки:";
            // 
            // Number_textEdit
            // 
            this.Number_textEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Number_textEdit.Location = new System.Drawing.Point(190, 177);
            this.Number_textEdit.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Number_textEdit.Name = "Number_textEdit";
            this.Number_textEdit.Properties.MaxLength = 9;
            this.Number_textEdit.Size = new System.Drawing.Size(197, 23);
            this.Number_textEdit.TabIndex = 2;
            this.Number_textEdit.TextChanged += new System.EventHandler(this.Number_textEdit_TextChanged);
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
            this.labelControl1.Location = new System.Drawing.Point(87, 122);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.ShowToolTips = false;
            this.labelControl1.Size = new System.Drawing.Size(360, 16);
            this.labelControl1.TabIndex = 13;
            // 
            // MarkEditorForm
            // 
            this.AcceptButton = this.Ok_simpleButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_simpleButton;
            this.ClientSize = new System.Drawing.Size(471, 284);
            this.Controls.Add(this.Type_comboBoxEdit);
            this.Controls.Add(this.Type_labelControl);
            this.Controls.Add(this.Base_labelControl);
            this.Controls.Add(this.Series_textEdit);
            this.Controls.Add(this.Note_labelControl);
            this.Controls.Add(this.Number_textEdit);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.Caption_labelControl);
            this.Controls.Add(this.Ok_simpleButton);
            this.Controls.Add(this.Cancel_simpleButton);
            this.Controls.Add(this.Accept_pictureEdit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 270);
            this.Name = "MarkEditorForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактирование марки";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WaybillMarkEditorForm_FormClosed);
            this.Load += new System.EventHandler(this.WaybillMarkEditorForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Editor16_imageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Accept_pictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Type_comboBoxEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Series_textEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Number_textEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton Cancel_simpleButton;
        private DevExpress.XtraEditors.PictureEdit Accept_pictureEdit;
        private DevExpress.XtraEditors.SimpleButton Ok_simpleButton;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl Caption_labelControl;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.Utils.ImageCollection Editor16_imageCollection;
        private DevExpress.XtraEditors.ComboBoxEdit Type_comboBoxEdit;
        private DevExpress.XtraEditors.LabelControl Type_labelControl;
        private DevExpress.XtraEditors.LabelControl Base_labelControl;
        private DevExpress.XtraEditors.TextEdit Series_textEdit;
        private DevExpress.XtraEditors.LabelControl Note_labelControl;
        private DevExpress.XtraEditors.TextEdit Number_textEdit;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}