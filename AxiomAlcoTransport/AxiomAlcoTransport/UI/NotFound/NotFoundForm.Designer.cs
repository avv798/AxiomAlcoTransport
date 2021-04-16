namespace Axiom.AlcoTransport
{
    partial class NotFoundForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NotFoundForm));
            this.Close_simpleButton = new DevExpress.XtraEditors.SimpleButton();
            this.Error_pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            this.Error_labelControl = new DevExpress.XtraEditors.LabelControl();
            this.NotFound_defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
            ((System.ComponentModel.ISupportInitialize)(this.Error_pictureEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // Close_simpleButton
            // 
            this.Close_simpleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Close_simpleButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close_simpleButton.Location = new System.Drawing.Point(302, 286);
            this.Close_simpleButton.Name = "Close_simpleButton";
            this.Close_simpleButton.Size = new System.Drawing.Size(150, 23);
            this.Close_simpleButton.TabIndex = 0;
            this.Close_simpleButton.Text = "Закрыть";
            this.Close_simpleButton.Click += new System.EventHandler(this.Close_simpleButton_Click);
            // 
            // Error_pictureEdit
            // 
            this.Error_pictureEdit.EditValue = ((object)(resources.GetObject("Error_pictureEdit.EditValue")));
            this.Error_pictureEdit.Location = new System.Drawing.Point(12, 12);
            this.Error_pictureEdit.Name = "Error_pictureEdit";
            this.Error_pictureEdit.Properties.AllowFocused = false;
            this.Error_pictureEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Error_pictureEdit.Properties.Appearance.Options.UseBackColor = true;
            this.Error_pictureEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.Error_pictureEdit.Properties.ReadOnly = true;
            this.Error_pictureEdit.Properties.ShowMenu = false;
            this.Error_pictureEdit.Size = new System.Drawing.Size(59, 62);
            this.Error_pictureEdit.TabIndex = 1;
            // 
            // Error_labelControl
            // 
            this.Error_labelControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Error_labelControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.Error_labelControl.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.Error_labelControl.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.Error_labelControl.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.Error_labelControl.Location = new System.Drawing.Point(88, 12);
            this.Error_labelControl.Name = "Error_labelControl";
            this.Error_labelControl.Size = new System.Drawing.Size(364, 259);
            this.Error_labelControl.TabIndex = 2;
            this.Error_labelControl.Text = "Приложение \'Informa.AlcoTransport\'  не сконфигурировано. Воспользуйтесь приложени" +
    "ем \'Informa.AlcoTransport.Configuration\' для инициализации конфигурации.";
            // 
            // NotFoundForm
            // 
            this.AcceptButton = this.Close_simpleButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.Close_simpleButton;
            this.ClientSize = new System.Drawing.Size(464, 321);
            this.Controls.Add(this.Error_labelControl);
            this.Controls.Add(this.Error_pictureEdit);
            this.Controls.Add(this.Close_simpleButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NotFoundForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Informa. In Vino Veritas";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfigurationNotFoundForm_FormClosed);
            this.Load += new System.EventHandler(this.ConfigurationNotFoundForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Error_pictureEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton Close_simpleButton;
        private DevExpress.XtraEditors.PictureEdit Error_pictureEdit;
        private DevExpress.XtraEditors.LabelControl Error_labelControl;
        private DevExpress.LookAndFeel.DefaultLookAndFeel NotFound_defaultLookAndFeel;
    }
}