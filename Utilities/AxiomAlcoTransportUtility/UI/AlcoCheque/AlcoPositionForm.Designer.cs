namespace Axiom.AlcoTransport.Utility
{
    partial class AlcoPositionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlcoPositionForm));
            this.Cancel_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Editor16_imageCollection = new DevExpress.Utils.ImageCollection(this.components);
            this.Position_pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.Ok_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.Base_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.Barcode_textEdit = new DevExpress.XtraEditors.TextEdit();
            this.Note_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.Ean_textEdit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.Volume_spinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.Price_spinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.Caption_labelControl = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.Editor16_imageCollection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Position_pictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Barcode_textEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ean_textEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Volume_spinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Price_spinEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // Cancel_simpleButton
            // 
            this.Cancel_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel_simpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_simpleButton.ImageIndex = 3;
            this.Cancel_simpleButton.ImageList = this.Editor16_imageCollection;
            this.Cancel_simpleButton.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.Cancel_simpleButton.Location = new System.Drawing.Point(479, 242);
            this.Cancel_simpleButton.Name = "Cancel_simpleButton";
            this.Cancel_simpleButton.Size = new System.Drawing.Size(128, 27);
            this.Cancel_simpleButton.TabIndex = 5;
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
            // Position_pictureEdit
            // 
            this.Position_pictureEdit.EditValue = ((object)(resources.GetObject("Position_pictureEdit.EditValue")));
            this.Position_pictureEdit.Location = new System.Drawing.Point(12, 12);
            this.Position_pictureEdit.Name = "Position_pictureEdit";
            this.Position_pictureEdit.Properties.AllowFocused = false;
            this.Position_pictureEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Position_pictureEdit.Properties.Appearance.Options.UseBackColor = true;
            this.Position_pictureEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Position_pictureEdit.Properties.ReadOnly = true;
            this.Position_pictureEdit.Properties.ShowMenu = false;
            this.Position_pictureEdit.Size = new System.Drawing.Size(59, 62);
            this.Position_pictureEdit.TabIndex = 7;
            // 
            // Ok_simpleButton
            // 
            this.Ok_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Ok_simpleButton.ImageIndex = 5;
            this.Ok_simpleButton.ImageList = this.Editor16_imageCollection;
            this.Ok_simpleButton.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.LeftCenter;
            this.Ok_simpleButton.Location = new System.Drawing.Point(345, 242);
            this.Ok_simpleButton.Name = "Ok_simpleButton";
            this.Ok_simpleButton.Size = new System.Drawing.Size(128, 27);
            this.Ok_simpleButton.TabIndex = 4;
            this.Ok_simpleButton.Text = "Ок";
            this.Ok_simpleButton.Click += new System.EventHandler(this.Ok_simpleButton_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl3.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl3.LineVisible = true;
            this.labelControl3.Location = new System.Drawing.Point(76, 177);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.ShowToolTips = false;
            this.labelControl3.Size = new System.Drawing.Size(531, 13);
            this.labelControl3.TabIndex = 14;
            // 
            // Base_labelControl
            // 
            this.Base_labelControl.Location = new System.Drawing.Point(76, 91);
            this.Base_labelControl.Name = "Base_labelControl";
            this.Base_labelControl.Size = new System.Drawing.Size(68, 13);
            this.Base_labelControl.TabIndex = 10;
            this.Base_labelControl.Text = "Код PDF-417:";
            // 
            // Barcode_textEdit
            // 
            this.Barcode_textEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Barcode_textEdit.EditValue = "";
            this.Barcode_textEdit.Location = new System.Drawing.Point(163, 88);
            this.Barcode_textEdit.Name = "Barcode_textEdit";
            this.Barcode_textEdit.Properties.MaxLength = 128;
            this.Barcode_textEdit.Size = new System.Drawing.Size(444, 20);
            this.Barcode_textEdit.TabIndex = 1;
            this.Barcode_textEdit.TextChanged += new System.EventHandler(this.Barcode_textEdit_TextChanged);
            // 
            // Note_labelControl
            // 
            this.Note_labelControl.Location = new System.Drawing.Point(76, 65);
            this.Note_labelControl.Name = "Note_labelControl";
            this.Note_labelControl.Size = new System.Drawing.Size(59, 13);
            this.Note_labelControl.TabIndex = 9;
            this.Note_labelControl.Text = "Код EAN13:";
            // 
            // Ean_textEdit
            // 
            this.Ean_textEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Ean_textEdit.Location = new System.Drawing.Point(163, 62);
            this.Ean_textEdit.Name = "Ean_textEdit";
            this.Ean_textEdit.Properties.MaxLength = 32;
            this.Ean_textEdit.Size = new System.Drawing.Size(140, 20);
            this.Ean_textEdit.TabIndex = 0;
            this.Ean_textEdit.TextChanged += new System.EventHandler(this.Ean_textEdit_TextChanged);
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
            this.labelControl1.Location = new System.Drawing.Point(76, 111);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.ShowToolTips = false;
            this.labelControl1.Size = new System.Drawing.Size(531, 13);
            this.labelControl1.TabIndex = 11;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(76, 156);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(63, 13);
            this.labelControl2.TabIndex = 13;
            this.labelControl2.Text = "Цена (руб.):";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(76, 130);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(58, 13);
            this.labelControl4.TabIndex = 12;
            this.labelControl4.Text = "Объём (л.):";
            // 
            // Volume_spinEdit
            // 
            this.Volume_spinEdit.EditValue = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.Volume_spinEdit.Location = new System.Drawing.Point(163, 127);
            this.Volume_spinEdit.Name = "Volume_spinEdit";
            this.Volume_spinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Volume_spinEdit.Properties.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.Volume_spinEdit.Properties.MaxValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.Volume_spinEdit.Size = new System.Drawing.Size(140, 20);
            this.Volume_spinEdit.TabIndex = 2;
            this.Volume_spinEdit.EditValueChanged += new System.EventHandler(this.Volume_spinEdit_EditValueChanged);
            // 
            // Price_spinEdit
            // 
            this.Price_spinEdit.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.Price_spinEdit.Location = new System.Drawing.Point(163, 153);
            this.Price_spinEdit.Name = "Price_spinEdit";
            this.Price_spinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.Price_spinEdit.Size = new System.Drawing.Size(140, 20);
            this.Price_spinEdit.TabIndex = 3;
            this.Price_spinEdit.EditValueChanged += new System.EventHandler(this.Price_spinEdit_EditValueChanged);
            // 
            // labelControl5
            // 
            this.labelControl5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl5.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl5.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl5.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl5.LineVisible = true;
            this.labelControl5.Location = new System.Drawing.Point(76, 45);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.ShowToolTips = false;
            this.labelControl5.Size = new System.Drawing.Size(531, 13);
            this.labelControl5.TabIndex = 8;
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
            this.Caption_labelControl.Location = new System.Drawing.Point(76, 12);
            this.Caption_labelControl.Name = "Caption_labelControl";
            this.Caption_labelControl.Size = new System.Drawing.Size(531, 27);
            this.Caption_labelControl.TabIndex = 6;
            this.Caption_labelControl.Text = "Укажите атрибуты алкогольной позиции в чеке: штрих-код PDF-417, код EAN13, объём " +
    "и розничную цену продукции.";
            // 
            // AlcoPositionForm
            // 
            this.AcceptButton = this.Ok_simpleButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_simpleButton;
            this.ClientSize = new System.Drawing.Size(624, 281);
            this.Controls.Add(this.Caption_labelControl);
            this.Controls.Add(this.Price_spinEdit);
            this.Controls.Add(this.Volume_spinEdit);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.Base_labelControl);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.Barcode_textEdit);
            this.Controls.Add(this.Note_labelControl);
            this.Controls.Add(this.Ean_textEdit);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.Ok_simpleButton);
            this.Controls.Add(this.Cancel_simpleButton);
            this.Controls.Add(this.Position_pictureEdit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 320);
            this.Name = "AlcoPositionForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Редактирование алкогольной позиции";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AlcoPositionForm_FormClosed);
            this.Load += new System.EventHandler(this.AlcoPositionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Editor16_imageCollection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Position_pictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Barcode_textEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Ean_textEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Volume_spinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Price_spinEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton Cancel_simpleButton;
        private DevExpress.XtraEditors.PictureEdit Position_pictureEdit;
        private DevExpress.XtraEditors.SimpleButton Ok_simpleButton;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.Utils.ImageCollection Editor16_imageCollection;
        private DevExpress.XtraEditors.LabelControl Base_labelControl;
        private DevExpress.XtraEditors.TextEdit Barcode_textEdit;
        private DevExpress.XtraEditors.LabelControl Note_labelControl;
        private DevExpress.XtraEditors.TextEdit Ean_textEdit;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.SpinEdit Volume_spinEdit;
        private DevExpress.XtraEditors.SpinEdit Price_spinEdit;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl Caption_labelControl;
    }
}