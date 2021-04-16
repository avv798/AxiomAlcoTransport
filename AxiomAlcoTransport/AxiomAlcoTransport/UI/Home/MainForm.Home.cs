using System;
using System.Windows.Forms;

namespace Axiom.AlcoTransport
{
	public partial class MainForm
	{
		#region Защищенные методы класса.
		/// <summary>
		/// Создать и показать форму домашней страницы.
		/// </summary>
		protected void showHomeForm()
		{
			try
			{
                Cursor = Cursors.WaitCursor;

				Ribbon.Enabled = false;

				foreach (Form child in MdiChildren)
				{
					if (!(child is HomeForm)) continue;

					#region Activate child-form.
                    ActivateMdiChild(child);
                    child.Hide();
                    child.Show();
					#endregion Activate child-form.

					return;
				}

				HomeForm homeForm = new HomeForm (this, documents) { MdiParent = this };
				homeForm.Show();
			}
			catch(Exception exception)
			{
				Ribbon.Enabled = true;
                Program.Logger.Error(this, exception.Message);
				Cursor = Cursors.Default;
			}
			finally
			{
				Ribbon.Enabled = true;
				Cursor = Cursors.Default;				
			}
		}
		#endregion Защищенные методы класса.
	}
}