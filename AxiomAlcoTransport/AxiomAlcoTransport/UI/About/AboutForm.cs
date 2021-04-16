using System;
using System.Drawing;
using System.Windows.Forms;

namespace Axiom.AlcoTransport
{
	/// <summary>
	/// �������������� ����� "� ���������".
	/// </summary>
    public partial class AboutForm : DevExpress.XtraEditors.XtraForm
	{
		#region ���������� ������� ������.
		/// <summary>
		/// ���� ������ ��� ��������������.
		/// ������ ������.
		/// </summary>
    	private readonly Color prevColor;
		#endregion ���������� ������� ������.

		#region ������������ ������.
		/// <summary>
		/// ����������� ������ �� ���������.
		/// </summary>
		public AboutForm()
        {
            try
            {
                Program.Logger.Info(this, "������� �������������... ");

                InitializeComponent();

                prevColor = Name_LabelControl.ForeColor;

                Program.Logger.Info(this, "... ������������� ������� ���������.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("�� ����� ������������� ��������� ������.", exception);
            }
        }
		#endregion ������������ ������.

		#region ��������� ������� ����������������� ����������.
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
            Program.Logger.Info(this, "����� �����������.");
        }
        private void Owner_labelControl_Click(object sender, EventArgs e)
        {
            MainForm.OpenCompanySite();
        }
        #endregion ��������� ������� ����������������� ����������.
	}
}
