namespace Axiom.AlcoTransport
{
    partial class SplashForm
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
            this.Splash_progressPanel = new DevExpress.XtraWaitForm.ProgressPanel();
            this.Splash_tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.Splash_tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // Splash_progressPanel
            // 
            this.Splash_progressPanel.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.Splash_progressPanel.Appearance.Options.UseBackColor = true;
            this.Splash_progressPanel.AppearanceCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Splash_progressPanel.AppearanceCaption.Options.UseFont = true;
            this.Splash_progressPanel.AppearanceDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Splash_progressPanel.AppearanceDescription.Options.UseFont = true;
            this.Splash_progressPanel.Caption = "Пожалуйста, подождите...";
            this.Splash_progressPanel.Description = "Выполняется загрузка всех необходимых компонентов и данных.";
            this.Splash_progressPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Splash_progressPanel.ImageHorzOffset = 32;
            this.Splash_progressPanel.Location = new System.Drawing.Point(0, 14);
            this.Splash_progressPanel.Margin = new System.Windows.Forms.Padding(0);
            this.Splash_progressPanel.Name = "Splash_progressPanel";
            this.Splash_progressPanel.Size = new System.Drawing.Size(570, 62);
            this.Splash_progressPanel.TabIndex = 0;
            // 
            // Splash_tableLayoutPanel
            // 
            this.Splash_tableLayoutPanel.AutoSize = true;
            this.Splash_tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Splash_tableLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.Splash_tableLayoutPanel.ColumnCount = 1;
            this.Splash_tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Splash_tableLayoutPanel.Controls.Add(this.Splash_progressPanel, 0, 0);
            this.Splash_tableLayoutPanel.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.Splash_tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Splash_tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.Splash_tableLayoutPanel.Name = "Splash_tableLayoutPanel";
            this.Splash_tableLayoutPanel.Padding = new System.Windows.Forms.Padding(0, 14, 0, 14);
            this.Splash_tableLayoutPanel.RowCount = 1;
            this.Splash_tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.Splash_tableLayoutPanel.Size = new System.Drawing.Size(570, 90);
            this.Splash_tableLayoutPanel.TabIndex = 1;
            // 
            // SplashForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(570, 90);
            this.Controls.Add(this.Splash_tableLayoutPanel);
            this.Name = "SplashForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Пожалуйста, подождите...";
            this.Splash_tableLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraWaitForm.ProgressPanel Splash_progressPanel;
        private System.Windows.Forms.TableLayoutPanel Splash_tableLayoutPanel;
    }
}
