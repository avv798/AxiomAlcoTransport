using System;
using System.Windows.Forms;

namespace Axiom.AlcoTransport
{
	public partial class MainForm
	{
        #region Внешние методы класса.
        /// <summary>
        /// Создать и показать форму.
        /// </summary>
        public void ShowNotAnswerForm()
        {
            showNotAnswerForm();
        }
        #endregion Внешние методы класса.

        #region Защищенные методы класса.
		/// <summary>
		/// Создать и показать форму.
		/// </summary>
		protected void showNotAnswerForm()
		{
			try
			{
                SplashAccessor.Show();

                Cursor = Cursors.WaitCursor;

				Ribbon.Enabled = false;

				foreach (Form child in MdiChildren)
				{
					if (!(child is NotAnswerForm)) continue;

					#region Activate child-form.
                    ActivateMdiChild(child);
                    child.Hide();
                    child.Show();
					#endregion Activate child-form.

					return;
				}

				NotAnswerForm form = new NotAnswerForm (documents)
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