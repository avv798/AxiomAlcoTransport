namespace Axiom.AlcoTransport.Licence
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.Main_defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            this.Close_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Config_pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.Main_xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
            this.Generate_xtraTabPage = new DevExpress.XtraTab.XtraTabPage();
            this.DT_dateEdit = new DevExpress.XtraEditors.DateEdit();
            this.Generate_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.DT_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.FsrarId_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.FsrarId_textEdit = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.Security_xtraTabPage = new DevExpress.XtraTab.XtraTabPage();
            this.Licence_saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.Config_pictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Main_xtraTabControl)).BeginInit();
            this.Main_xtraTabControl.SuspendLayout();
            this.Generate_xtraTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DT_dateEdit.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DT_dateEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FsrarId_textEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // Close_simpleButton
            // 
            this.Close_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Close_simpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close_simpleButton.Location = new System.Drawing.Point(409, 286);
            this.Close_simpleButton.Name = "Close_simpleButton";
            this.Close_simpleButton.Size = new System.Drawing.Size(128, 23);
            this.Close_simpleButton.TabIndex = 0;
            this.Close_simpleButton.Text = "Закрыть";
            this.Close_simpleButton.Click += new System.EventHandler(this.Close_simpleButton_Click);
            // 
            // Config_pictureEdit
            // 
            this.Config_pictureEdit.EditValue = ((object)(resources.GetObject("Config_pictureEdit.EditValue")));
            this.Config_pictureEdit.Location = new System.Drawing.Point(12, 12);
            this.Config_pictureEdit.Name = "Config_pictureEdit";
            this.Config_pictureEdit.Properties.AllowFocused = false;
            this.Config_pictureEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Config_pictureEdit.Properties.Appearance.Options.UseBackColor = true;
            this.Config_pictureEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Config_pictureEdit.Properties.ReadOnly = true;
            this.Config_pictureEdit.Properties.ShowMenu = false;
            this.Config_pictureEdit.Size = new System.Drawing.Size(59, 62);
            this.Config_pictureEdit.TabIndex = 2;
            // 
            // Main_xtraTabControl
            // 
            this.Main_xtraTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Main_xtraTabControl.Location = new System.Drawing.Point(77, 12);
            this.Main_xtraTabControl.Name = "Main_xtraTabControl";
            this.Main_xtraTabControl.SelectedTabPage = this.Generate_xtraTabPage;
            this.Main_xtraTabControl.Size = new System.Drawing.Size(465, 268);
            this.Main_xtraTabControl.TabIndex = 1;
            this.Main_xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.Generate_xtraTabPage,
            this.Security_xtraTabPage});
            // 
            // Generate_xtraTabPage
            // 
            this.Generate_xtraTabPage.Controls.Add(this.DT_dateEdit);
            this.Generate_xtraTabPage.Controls.Add(this.Generate_simpleButton);
            this.Generate_xtraTabPage.Controls.Add(this.DT_labelControl);
            this.Generate_xtraTabPage.Controls.Add(this.FsrarId_labelControl);
            this.Generate_xtraTabPage.Controls.Add(this.FsrarId_textEdit);
            this.Generate_xtraTabPage.Controls.Add(this.labelControl1);
            this.Generate_xtraTabPage.Controls.Add(this.labelControl2);
            this.Generate_xtraTabPage.Name = "Generate_xtraTabPage";
            this.Generate_xtraTabPage.Size = new System.Drawing.Size(459, 240);
            this.Generate_xtraTabPage.Text = "Генератор лицензий";
            // 
            // DT_dateEdit
            // 
            this.DT_dateEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DT_dateEdit.EditValue = new System.DateTime(2015, 12, 1, 13, 3, 9, 0);
            this.DT_dateEdit.Location = new System.Drawing.Point(262, 45);
            this.DT_dateEdit.Name = "DT_dateEdit";
            this.DT_dateEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.DT_dateEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DT_dateEdit.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.DT_dateEdit.Properties.DisplayFormat.FormatString = "dd MMMM yyyy (dddd)";
            this.DT_dateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DT_dateEdit.Properties.EditFormat.FormatString = "dd MMMM yyyy (dddd)";
            this.DT_dateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.DT_dateEdit.Properties.Mask.EditMask = "dd MMMM yyyy (dddd)";
            this.DT_dateEdit.Size = new System.Drawing.Size(183, 20);
            this.DT_dateEdit.TabIndex = 1;
            this.DT_dateEdit.EditValueChanged += new System.EventHandler(this.DT_dateEdit_EditValueChanged);
            // 
            // Generate_simpleButton
            // 
            this.Generate_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Generate_simpleButton.Location = new System.Drawing.Point(262, 87);
            this.Generate_simpleButton.Name = "Generate_simpleButton";
            this.Generate_simpleButton.Size = new System.Drawing.Size(183, 23);
            this.Generate_simpleButton.TabIndex = 2;
            this.Generate_simpleButton.Text = "Сгенерировать файл лицензии";
            this.Generate_simpleButton.Click += new System.EventHandler(this.Generate_simpleButton_Click);
            // 
            // DT_labelControl
            // 
            this.DT_labelControl.Location = new System.Drawing.Point(12, 48);
            this.DT_labelControl.Name = "DT_labelControl";
            this.DT_labelControl.Size = new System.Drawing.Size(223, 13);
            this.DT_labelControl.TabIndex = 4;
            this.DT_labelControl.Text = "Дата окончания лицензии (включительно):";
            // 
            // FsrarId_labelControl
            // 
            this.FsrarId_labelControl.Location = new System.Drawing.Point(12, 22);
            this.FsrarId_labelControl.Name = "FsrarId_labelControl";
            this.FsrarId_labelControl.Size = new System.Drawing.Size(202, 13);
            this.FsrarId_labelControl.TabIndex = 3;
            this.FsrarId_labelControl.Text = "Идентификатор организации в ФС РАР:";
            // 
            // FsrarId_textEdit
            // 
            this.FsrarId_textEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FsrarId_textEdit.Location = new System.Drawing.Point(262, 19);
            this.FsrarId_textEdit.Name = "FsrarId_textEdit";
            this.FsrarId_textEdit.Size = new System.Drawing.Size(183, 20);
            this.FsrarId_textEdit.TabIndex = 0;
            this.FsrarId_textEdit.EditValueChanged += new System.EventHandler(this.FsrarId_textEdit_EditValueChanged);
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
            this.labelControl1.Location = new System.Drawing.Point(12, 116);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.ShowToolTips = false;
            this.labelControl1.Size = new System.Drawing.Size(433, 13);
            this.labelControl1.TabIndex = 6;
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControl2.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.labelControl2.LineVisible = true;
            this.labelControl2.Location = new System.Drawing.Point(12, 67);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.ShowToolTips = false;
            this.labelControl2.Size = new System.Drawing.Size(433, 13);
            this.labelControl2.TabIndex = 5;
            // 
            // Security_xtraTabPage
            // 
            this.Security_xtraTabPage.Name = "Security_xtraTabPage";
            this.Security_xtraTabPage.PageEnabled = false;
            this.Security_xtraTabPage.Size = new System.Drawing.Size(459, 240);
            this.Security_xtraTabPage.Text = "Проверка лицензий";
            // 
            // Licence_saveFileDialog
            // 
            this.Licence_saveFileDialog.DefaultExt = "lic";
            this.Licence_saveFileDialog.Filter = "Файлы лицензии (*.lic)|*.lic|Все файлы (*.*)|*.*";
            this.Licence_saveFileDialog.SupportMultiDottedExtensions = true;
            this.Licence_saveFileDialog.Title = "Выберит файл для генерации лицензии";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Close_simpleButton;
            this.ClientSize = new System.Drawing.Size(554, 321);
            this.Controls.Add(this.Main_xtraTabControl);
            this.Controls.Add(this.Close_simpleButton);
            this.Controls.Add(this.Config_pictureEdit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Informa. In Vino Veritas";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Config_pictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Main_xtraTabControl)).EndInit();
            this.Main_xtraTabControl.ResumeLayout(false);
            this.Generate_xtraTabPage.ResumeLayout(false);
            this.Generate_xtraTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DT_dateEdit.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DT_dateEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FsrarId_textEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.LookAndFeel.DefaultLookAndFeel Main_defaultLookAndFeel;
        private DevExpress.XtraEditors.SimpleButton Close_simpleButton;
        private DevExpress.XtraEditors.PictureEdit Config_pictureEdit;
        private DevExpress.XtraTab.XtraTabControl Main_xtraTabControl;
        private DevExpress.XtraTab.XtraTabPage Generate_xtraTabPage;
        private DevExpress.XtraEditors.DateEdit DT_dateEdit;
        private DevExpress.XtraEditors.SimpleButton Generate_simpleButton;
        private DevExpress.XtraEditors.LabelControl DT_labelControl;
        private DevExpress.XtraEditors.LabelControl FsrarId_labelControl;
        private DevExpress.XtraEditors.TextEdit FsrarId_textEdit;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraTab.XtraTabPage Security_xtraTabPage;
        private System.Windows.Forms.SaveFileDialog Licence_saveFileDialog;


    }
}