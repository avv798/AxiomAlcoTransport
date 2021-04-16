namespace Axiom.AlcoTransport
{
    partial class WaybillRegIdRequestForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaybillRegIdRequestForm));
            this.Cancel_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Accept_pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.Ok_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.Number_textEdit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.Caption_labelControl = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.Accept_pictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Number_textEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // Cancel_simpleButton
            // 
            this.Cancel_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Cancel_simpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Cancel_simpleButton.Location = new System.Drawing.Point(259, 206);
            this.Cancel_simpleButton.Name = "Cancel_simpleButton";
            this.Cancel_simpleButton.Size = new System.Drawing.Size(128, 23);
            this.Cancel_simpleButton.TabIndex = 3;
            this.Cancel_simpleButton.Text = "Отмена";
            this.Cancel_simpleButton.Click += new System.EventHandler(this.Cancel_simpleButton_Click);
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
            // Ok_simpleButton
            // 
            this.Ok_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Ok_simpleButton.Location = new System.Drawing.Point(125, 206);
            this.Ok_simpleButton.Name = "Ok_simpleButton";
            this.Ok_simpleButton.Size = new System.Drawing.Size(128, 23);
            this.Ok_simpleButton.TabIndex = 2;
            this.Ok_simpleButton.Text = "Ок";
            this.Ok_simpleButton.Click += new System.EventHandler(this.Ok_simpleButton_Click);
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
            this.labelControl2.Location = new System.Drawing.Point(78, 187);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.ShowToolTips = false;
            this.labelControl2.Size = new System.Drawing.Size(309, 13);
            this.labelControl2.TabIndex = 6;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(78, 153);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(108, 13);
            this.labelControl1.TabIndex = 7;
            this.labelControl1.Text = "Идентификатор ТТН:";
            // 
            // Number_textEdit
            // 
            this.Number_textEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Number_textEdit.EditValue = "TTN-";
            this.Number_textEdit.Location = new System.Drawing.Point(210, 150);
            this.Number_textEdit.Name = "Number_textEdit";
            this.Number_textEdit.Properties.MaxLength = 50;
            this.Number_textEdit.Size = new System.Drawing.Size(177, 20);
            this.Number_textEdit.TabIndex = 0;
            this.Number_textEdit.EditValueChanged += new System.EventHandler(this.Number_textEdit_EditValueChanged);
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
            this.labelControl4.Location = new System.Drawing.Point(78, 131);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.ShowToolTips = false;
            this.labelControl4.Size = new System.Drawing.Size(309, 13);
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
            this.Caption_labelControl.Size = new System.Drawing.Size(309, 112);
            this.Caption_labelControl.TabIndex = 5;
            this.Caption_labelControl.Text = resources.GetString("Caption_labelControl.Text");
            // 
            // WaybillRegIdRequestForm
            // 
            this.AcceptButton = this.Ok_simpleButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Cancel_simpleButton;
            this.ClientSize = new System.Drawing.Size(404, 241);
            this.Controls.Add(this.Number_textEdit);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.Caption_labelControl);
            this.Controls.Add(this.Ok_simpleButton);
            this.Controls.Add(this.Cancel_simpleButton);
            this.Controls.Add(this.Accept_pictureEdit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(420, 280);
            this.Name = "WaybillRegIdRequestForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Укажите идентификатор накладной";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.WaybillRegIdRequestForm_FormClosed);
            this.Load += new System.EventHandler(this.WaybillRegIdRequestForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Accept_pictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Number_textEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton Cancel_simpleButton;
        private DevExpress.XtraEditors.PictureEdit Accept_pictureEdit;
        private DevExpress.XtraEditors.SimpleButton Ok_simpleButton;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit Number_textEdit;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl Caption_labelControl;
    }
}