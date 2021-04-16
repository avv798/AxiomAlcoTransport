namespace Axiom.AlcoTransport.Configuration
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
            this.Config_pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.Main_defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            this.Close_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Save_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Main_xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
            this.Common_xtraTabPage = new DevExpress.XtraTab.XtraTabPage();
            this.IntervalRequest_spinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.TimeoutLong_spinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.TimeoutShort_spinEdit = new DevExpress.XtraEditors.SpinEdit();
            this.IntervalRequest_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.Path_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.TimeoutLong_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.Inn_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.TimeoutShort_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.Port_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.Address_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.FsrarId_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.Inn_textEdit = new DevExpress.XtraEditors.TextEdit();
            this.Port_textEdit = new DevExpress.XtraEditors.TextEdit();
            this.Address_textEdit = new DevExpress.XtraEditors.TextEdit();
            this.FsrarId_textEdit = new DevExpress.XtraEditors.TextEdit();
            this.Path_buttonEdit = new DevExpress.XtraEditors.ButtonEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.Security_xtraTabPage = new DevExpress.XtraTab.XtraTabPage();
            this.Db_folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.Config_saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.Config_pictureEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Main_xtraTabControl)).BeginInit();
            this.Main_xtraTabControl.SuspendLayout();
            this.Common_xtraTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IntervalRequest_spinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeoutLong_spinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeoutShort_spinEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Inn_textEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Port_textEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Address_textEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FsrarId_textEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Path_buttonEdit.Properties)).BeginInit();
            this.SuspendLayout();
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
            // Close_simpleButton
            // 
            this.Close_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Close_simpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close_simpleButton.Location = new System.Drawing.Point(484, 406);
            this.Close_simpleButton.Name = "Close_simpleButton";
            this.Close_simpleButton.Size = new System.Drawing.Size(128, 23);
            this.Close_simpleButton.TabIndex = 0;
            this.Close_simpleButton.Text = "Закрыть";
            this.Close_simpleButton.Click += new System.EventHandler(this.Close_simpleButton_Click);
            // 
            // Save_simpleButton
            // 
            this.Save_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Save_simpleButton.Enabled = false;
            this.Save_simpleButton.Location = new System.Drawing.Point(77, 406);
            this.Save_simpleButton.Name = "Save_simpleButton";
            this.Save_simpleButton.Size = new System.Drawing.Size(128, 23);
            this.Save_simpleButton.TabIndex = 2;
            this.Save_simpleButton.Text = "Сохранить";
            this.Save_simpleButton.Click += new System.EventHandler(this.Save_simpleButton_Click);
            // 
            // Main_xtraTabControl
            // 
            this.Main_xtraTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Main_xtraTabControl.Location = new System.Drawing.Point(77, 12);
            this.Main_xtraTabControl.Name = "Main_xtraTabControl";
            this.Main_xtraTabControl.SelectedTabPage = this.Common_xtraTabPage;
            this.Main_xtraTabControl.Size = new System.Drawing.Size(539, 388);
            this.Main_xtraTabControl.TabIndex = 1;
            this.Main_xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.Common_xtraTabPage,
            this.Security_xtraTabPage});
            // 
            // Common_xtraTabPage
            // 
            this.Common_xtraTabPage.Controls.Add(this.IntervalRequest_spinEdit);
            this.Common_xtraTabPage.Controls.Add(this.TimeoutLong_spinEdit);
            this.Common_xtraTabPage.Controls.Add(this.TimeoutShort_spinEdit);
            this.Common_xtraTabPage.Controls.Add(this.IntervalRequest_labelControl);
            this.Common_xtraTabPage.Controls.Add(this.Path_labelControl);
            this.Common_xtraTabPage.Controls.Add(this.TimeoutLong_labelControl);
            this.Common_xtraTabPage.Controls.Add(this.Inn_labelControl);
            this.Common_xtraTabPage.Controls.Add(this.TimeoutShort_labelControl);
            this.Common_xtraTabPage.Controls.Add(this.Port_labelControl);
            this.Common_xtraTabPage.Controls.Add(this.Address_labelControl);
            this.Common_xtraTabPage.Controls.Add(this.FsrarId_labelControl);
            this.Common_xtraTabPage.Controls.Add(this.Inn_textEdit);
            this.Common_xtraTabPage.Controls.Add(this.Port_textEdit);
            this.Common_xtraTabPage.Controls.Add(this.Address_textEdit);
            this.Common_xtraTabPage.Controls.Add(this.FsrarId_textEdit);
            this.Common_xtraTabPage.Controls.Add(this.Path_buttonEdit);
            this.Common_xtraTabPage.Controls.Add(this.labelControl2);
            this.Common_xtraTabPage.Controls.Add(this.labelControl1);
            this.Common_xtraTabPage.Controls.Add(this.labelControl5);
            this.Common_xtraTabPage.Controls.Add(this.labelControl3);
            this.Common_xtraTabPage.Name = "Common_xtraTabPage";
            this.Common_xtraTabPage.Size = new System.Drawing.Size(533, 360);
            this.Common_xtraTabPage.Text = "Общие настройки";
            // 
            // IntervalRequest_spinEdit
            // 
            this.IntervalRequest_spinEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IntervalRequest_spinEdit.EditValue = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.IntervalRequest_spinEdit.Location = new System.Drawing.Point(376, 224);
            this.IntervalRequest_spinEdit.Name = "IntervalRequest_spinEdit";
            this.IntervalRequest_spinEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.IntervalRequest_spinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.IntervalRequest_spinEdit.Properties.MaxValue = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.IntervalRequest_spinEdit.Properties.MinValue = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.IntervalRequest_spinEdit.Size = new System.Drawing.Size(143, 20);
            this.IntervalRequest_spinEdit.TabIndex = 6;
            this.IntervalRequest_spinEdit.EditValueChanged += new System.EventHandler(this.IntervalRequest_spinEdit_EditValueChanged);
            // 
            // TimeoutLong_spinEdit
            // 
            this.TimeoutLong_spinEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeoutLong_spinEdit.EditValue = new decimal(new int[] {
            15000,
            0,
            0,
            0});
            this.TimeoutLong_spinEdit.Location = new System.Drawing.Point(406, 179);
            this.TimeoutLong_spinEdit.Name = "TimeoutLong_spinEdit";
            this.TimeoutLong_spinEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.TimeoutLong_spinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TimeoutLong_spinEdit.Properties.MaxValue = new decimal(new int[] {
            720000,
            0,
            0,
            0});
            this.TimeoutLong_spinEdit.Properties.MinValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.TimeoutLong_spinEdit.Size = new System.Drawing.Size(113, 20);
            this.TimeoutLong_spinEdit.TabIndex = 5;
            this.TimeoutLong_spinEdit.EditValueChanged += new System.EventHandler(this.TimeoutLong_spinEdit_EditValueChanged);
            // 
            // TimeoutShort_spinEdit
            // 
            this.TimeoutShort_spinEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TimeoutShort_spinEdit.EditValue = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.TimeoutShort_spinEdit.Location = new System.Drawing.Point(406, 153);
            this.TimeoutShort_spinEdit.Name = "TimeoutShort_spinEdit";
            this.TimeoutShort_spinEdit.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.TimeoutShort_spinEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.TimeoutShort_spinEdit.Properties.MaxValue = new decimal(new int[] {
            720000,
            0,
            0,
            0});
            this.TimeoutShort_spinEdit.Properties.MinValue = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.TimeoutShort_spinEdit.Size = new System.Drawing.Size(113, 20);
            this.TimeoutShort_spinEdit.TabIndex = 4;
            this.TimeoutShort_spinEdit.EditValueChanged += new System.EventHandler(this.TimeoutShort_spinEdit_EditValueChanged);
            // 
            // IntervalRequest_labelControl
            // 
            this.IntervalRequest_labelControl.Location = new System.Drawing.Point(12, 227);
            this.IntervalRequest_labelControl.Name = "IntervalRequest_labelControl";
            this.IntervalRequest_labelControl.Size = new System.Drawing.Size(216, 13);
            this.IntervalRequest_labelControl.TabIndex = 20;
            this.IntervalRequest_labelControl.Text = "Период опроса сервера УТМ (в секундах):";
            // 
            // Path_labelControl
            // 
            this.Path_labelControl.Location = new System.Drawing.Point(12, 272);
            this.Path_labelControl.Name = "Path_labelControl";
            this.Path_labelControl.Size = new System.Drawing.Size(173, 13);
            this.Path_labelControl.TabIndex = 18;
            this.Path_labelControl.Text = "Каталог локальной базы данных:";
            // 
            // TimeoutLong_labelControl
            // 
            this.TimeoutLong_labelControl.Location = new System.Drawing.Point(12, 182);
            this.TimeoutLong_labelControl.Name = "TimeoutLong_labelControl";
            this.TimeoutLong_labelControl.Size = new System.Drawing.Size(338, 13);
            this.TimeoutLong_labelControl.TabIndex = 20;
            this.TimeoutLong_labelControl.Text = "Тайм-аут (длинный) при обращении к серверу (в миллисекундах):";
            // 
            // Inn_labelControl
            // 
            this.Inn_labelControl.Location = new System.Drawing.Point(12, 48);
            this.Inn_labelControl.Name = "Inn_labelControl";
            this.Inn_labelControl.Size = new System.Drawing.Size(92, 13);
            this.Inn_labelControl.TabIndex = 19;
            this.Inn_labelControl.Text = "ИНН организации:";
            // 
            // TimeoutShort_labelControl
            // 
            this.TimeoutShort_labelControl.Location = new System.Drawing.Point(12, 156);
            this.TimeoutShort_labelControl.Name = "TimeoutShort_labelControl";
            this.TimeoutShort_labelControl.Size = new System.Drawing.Size(341, 13);
            this.TimeoutShort_labelControl.TabIndex = 20;
            this.TimeoutShort_labelControl.Text = "Тайм-аут (короткий) при обращении к серверу (в миллисекундах):";
            // 
            // Port_labelControl
            // 
            this.Port_labelControl.Location = new System.Drawing.Point(12, 115);
            this.Port_labelControl.Name = "Port_labelControl";
            this.Port_labelControl.Size = new System.Drawing.Size(97, 13);
            this.Port_labelControl.TabIndex = 21;
            this.Port_labelControl.Text = "Порт сервера УТМ:";
            // 
            // Address_labelControl
            // 
            this.Address_labelControl.Location = new System.Drawing.Point(12, 89);
            this.Address_labelControl.Name = "Address_labelControl";
            this.Address_labelControl.Size = new System.Drawing.Size(103, 13);
            this.Address_labelControl.TabIndex = 22;
            this.Address_labelControl.Text = "Адрес сервера УТМ:";
            // 
            // FsrarId_labelControl
            // 
            this.FsrarId_labelControl.Location = new System.Drawing.Point(12, 22);
            this.FsrarId_labelControl.Name = "FsrarId_labelControl";
            this.FsrarId_labelControl.Size = new System.Drawing.Size(202, 13);
            this.FsrarId_labelControl.TabIndex = 23;
            this.FsrarId_labelControl.Text = "Идентификатор организации в ФС РАР:";
            // 
            // Inn_textEdit
            // 
            this.Inn_textEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Inn_textEdit.Location = new System.Drawing.Point(232, 45);
            this.Inn_textEdit.Name = "Inn_textEdit";
            this.Inn_textEdit.Size = new System.Drawing.Size(287, 20);
            this.Inn_textEdit.TabIndex = 1;
            this.Inn_textEdit.EditValueChanged += new System.EventHandler(this.Inn_textEdit_EditValueChanged);
            // 
            // Port_textEdit
            // 
            this.Port_textEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Port_textEdit.EditValue = "8080";
            this.Port_textEdit.Location = new System.Drawing.Point(232, 112);
            this.Port_textEdit.Name = "Port_textEdit";
            this.Port_textEdit.Size = new System.Drawing.Size(287, 20);
            this.Port_textEdit.TabIndex = 3;
            this.Port_textEdit.EditValueChanged += new System.EventHandler(this.Port_textEdit_EditValueChanged);
            // 
            // Address_textEdit
            // 
            this.Address_textEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Address_textEdit.EditValue = "localhost";
            this.Address_textEdit.Location = new System.Drawing.Point(232, 86);
            this.Address_textEdit.Name = "Address_textEdit";
            this.Address_textEdit.Size = new System.Drawing.Size(287, 20);
            this.Address_textEdit.TabIndex = 2;
            this.Address_textEdit.EditValueChanged += new System.EventHandler(this.Address_textEdit_EditValueChanged);
            // 
            // FsrarId_textEdit
            // 
            this.FsrarId_textEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FsrarId_textEdit.Location = new System.Drawing.Point(232, 19);
            this.FsrarId_textEdit.Name = "FsrarId_textEdit";
            this.FsrarId_textEdit.Size = new System.Drawing.Size(287, 20);
            this.FsrarId_textEdit.TabIndex = 0;
            this.FsrarId_textEdit.EditValueChanged += new System.EventHandler(this.FsrarId_textEdit_EditValueChanged);
            // 
            // Path_buttonEdit
            // 
            this.Path_buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Path_buttonEdit.Location = new System.Drawing.Point(232, 269);
            this.Path_buttonEdit.Name = "Path_buttonEdit";
            this.Path_buttonEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.Path_buttonEdit.Size = new System.Drawing.Size(287, 20);
            this.Path_buttonEdit.TabIndex = 7;
            this.Path_buttonEdit.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.Path_buttonEdit_ButtonClick);
            this.Path_buttonEdit.EditValueChanged += new System.EventHandler(this.Path_buttonEdit_EditValueChanged);
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
            this.labelControl2.Size = new System.Drawing.Size(507, 13);
            this.labelControl2.TabIndex = 14;
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
            this.labelControl1.Location = new System.Drawing.Point(12, 205);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.ShowToolTips = false;
            this.labelControl1.Size = new System.Drawing.Size(507, 13);
            this.labelControl1.TabIndex = 15;
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
            this.labelControl5.Location = new System.Drawing.Point(12, 134);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.ShowToolTips = false;
            this.labelControl5.Size = new System.Drawing.Size(507, 13);
            this.labelControl5.TabIndex = 15;
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
            this.labelControl3.Location = new System.Drawing.Point(12, 250);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.ShowToolTips = false;
            this.labelControl3.Size = new System.Drawing.Size(507, 13);
            this.labelControl3.TabIndex = 16;
            // 
            // Security_xtraTabPage
            // 
            this.Security_xtraTabPage.Name = "Security_xtraTabPage";
            this.Security_xtraTabPage.PageEnabled = false;
            this.Security_xtraTabPage.Size = new System.Drawing.Size(533, 360);
            this.Security_xtraTabPage.Text = "Настройки безопасности";
            // 
            // Db_folderBrowserDialog
            // 
            this.Db_folderBrowserDialog.Description = "Выберите существующий или создайте новый каталог, в котором будет размещена локал" +
    "ьная база данных.";
            this.Db_folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // Config_saveFileDialog
            // 
            this.Config_saveFileDialog.DefaultExt = "cfg";
            this.Config_saveFileDialog.Filter = "Файлы конфигурации (*.cfg)|*.cfg|Все файлы (*.*)|*.*";
            this.Config_saveFileDialog.SupportMultiDottedExtensions = true;
            this.Config_saveFileDialog.Title = "Выберите файл для сохранения конфигурации приложения";
            // 
            // MainForm
            // 
            this.AcceptButton = this.Save_simpleButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Close_simpleButton;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.Main_xtraTabControl);
            this.Controls.Add(this.Save_simpleButton);
            this.Controls.Add(this.Close_simpleButton);
            this.Controls.Add(this.Config_pictureEdit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Informa. In Vino Veritas";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Config_pictureEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Main_xtraTabControl)).EndInit();
            this.Main_xtraTabControl.ResumeLayout(false);
            this.Common_xtraTabPage.ResumeLayout(false);
            this.Common_xtraTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.IntervalRequest_spinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeoutLong_spinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeoutShort_spinEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Inn_textEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Port_textEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Address_textEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FsrarId_textEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Path_buttonEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PictureEdit Config_pictureEdit;
        private DevExpress.LookAndFeel.DefaultLookAndFeel Main_defaultLookAndFeel;
        private DevExpress.XtraEditors.SimpleButton Close_simpleButton;
        private DevExpress.XtraEditors.SimpleButton Save_simpleButton;
        private DevExpress.XtraTab.XtraTabControl Main_xtraTabControl;
        private DevExpress.XtraTab.XtraTabPage Common_xtraTabPage;
        private DevExpress.XtraEditors.SpinEdit TimeoutShort_spinEdit;
        private DevExpress.XtraEditors.LabelControl Path_labelControl;
        private DevExpress.XtraEditors.LabelControl Inn_labelControl;
        private DevExpress.XtraEditors.LabelControl TimeoutShort_labelControl;
        private DevExpress.XtraEditors.LabelControl Port_labelControl;
        private DevExpress.XtraEditors.LabelControl Address_labelControl;
        private DevExpress.XtraEditors.LabelControl FsrarId_labelControl;
        private DevExpress.XtraEditors.TextEdit Inn_textEdit;
        private DevExpress.XtraEditors.TextEdit Port_textEdit;
        private DevExpress.XtraEditors.TextEdit Address_textEdit;
        private DevExpress.XtraEditors.TextEdit FsrarId_textEdit;
        private DevExpress.XtraEditors.ButtonEdit Path_buttonEdit;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraTab.XtraTabPage Security_xtraTabPage;
        private System.Windows.Forms.FolderBrowserDialog Db_folderBrowserDialog;
        private DevExpress.XtraEditors.SpinEdit TimeoutLong_spinEdit;
        private DevExpress.XtraEditors.LabelControl TimeoutLong_labelControl;
        private DevExpress.XtraEditors.SpinEdit IntervalRequest_spinEdit;
        private DevExpress.XtraEditors.LabelControl IntervalRequest_labelControl;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.SaveFileDialog Config_saveFileDialog;
    }
}