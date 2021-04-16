using System;
using System.Drawing;
using System.Windows.Forms;

namespace Axiom.AlcoTransport
{
	/// <summary>
	/// Информационная форма "О программе".
	/// </summary>
    public partial class AboutForm : DevExpress.XtraEditors.XtraForm
	{
		#region Внутренние объекты класса.
		/// <summary>
		/// Цвет шрифта для восстановления.
		/// Только чтение.
		/// </summary>
    	private readonly Color prevColor;
		#endregion Внутренние объекты класса.

		#region Конструкторы класса.
		/// <summary>
		/// Конструктор класса по умолчанию.
		/// </summary>
		public AboutForm()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                prevColor = Name_LabelControl.ForeColor;

                Program.Logger.Info(this, "... инициализация успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации произошла ошибка.", exception);
            }
        }
		#endregion Конструкторы класса.

		#region Обработка событий пользовательского интерфейса.
		private void Close_SimpleButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void Name_LabelControl_MouseMove(object sender, MouseEventArgs e)
        {
            Name_LabelControl.ForeColor = Color.Blue;
        }
        private void Name_LabelControl_MouseLeave(object sender, EventArgs e)
        {
			Name_LabelControl.ForeColor = prevColor;
        }
        private void Law_LabelControl_MouseMove(object sender, MouseEventArgs e)
        {
            Law_LabelControl.ForeColor = Color.Red;
        }
        private void Law_LabelControl_MouseLeave(object sender, EventArgs e)
        {
            Law_LabelControl.ForeColor = prevColor;
        }
        private void gmd_About_Form_Load(object sender, EventArgs e)
        {
			// Name_LabelControl.Text += (" (ver. " + MainForm.GetNumbersOfVersion(3) + ")");
			Name_LabelControl.Text += (" " + MainForm.GetNumbersOfVersion(3));
			Name_LabelControl.ToolTip = Name_LabelControl.ToolTip += ("\r\nVersion " + MainForm.GetNumbersOfVersion(3) + " (" + Application.ProductVersion + ")");
		}
		private void Owner_labelControl_MouseLeave(object sender, EventArgs e)
		{
			Owner_labelControl.ForeColor = prevColor;
		}
		private void Owner_labelControl_MouseMove(object sender, MouseEventArgs e)
		{
			Owner_labelControl.ForeColor = Color.Blue;
		}
		private void ProjectName_labelControl_MouseLeave(object sender, EventArgs e)
		{
			ProjectName_labelControl.ForeColor = prevColor;
		}
		private void ProjectName_labelControl_MouseMove(object sender, MouseEventArgs e)
		{
			ProjectName_labelControl.ForeColor = Color.Blue;
		}
        private void AboutForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Logger.Info(this, "Форма закрывается.");
        }
        private void Owner_labelControl_Click(object sender, EventArgs e)
        {
            MainForm.OpenCompanySite();
        }
        #endregion Обработка событий пользовательского интерфейса.
	}
}
