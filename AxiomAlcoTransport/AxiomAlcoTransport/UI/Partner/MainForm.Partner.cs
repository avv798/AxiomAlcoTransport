using System;
using System.Windows.Forms;

namespace Axiom.AlcoTransport
{
	public partial class MainForm
	{
		#region Защищенные методы класса.
		/// <summary>
		/// Создать и показать форму.
		/// </summary>
		protected void showPartnerForm()
		{
			try
			{
                SplashAccessor.Show();

                Cursor = Cursors.WaitCursor;

				Ribbon.Enabled = false;

				foreach (Form child in MdiChildren)
				{
					if (!(child is PartnerForm)) continue;

					#region Activate child-form.
                    ActivateMdiChild(child);
                    child.Hide();
                    child.Show();
					#endregion Activate child-form.

					return;
				}

				PartnerForm form = new PartnerForm (documents)
				                        {
				                            MdiParent = this,
                                            MainRibbonControl = Main_ribbon
				                        };
				form.Show();
			}
			catch(Exception exception)
			{
				Ribbon.Enabled = true;
                Program.Logger.Error(this, exception.Message);

                SplashAccessor.Close();

                Cursor = Cursors.Default;

			    ShowErrorMessage(string.Format("Во время открытия формы произошла ошибка: '{0}'.", exception.Message));
			}
			finally
			{
				Ribbon.Enabled = true;
				Cursor = Cursors.Default;

                SplashAccessor.Close();
			}
		}
		#endregion Защищенные методы класса.
	}
}