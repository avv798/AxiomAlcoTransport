using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Axiom.AlcoTransport.Utility
{
	public partial class MainForm
	{
		#region Внешние вспомогательные статические методы класса.
		/// <summary>
		/// Показать сообщение об ошибке.
		/// </summary>
		/// <param name="e">Возникшее исключение.</param>
		public static void ShowErrorMessage(Exception e)
		{
			XtraMessageBox.Show(e.Message + " (" + e.Source + ")"
								, "Ошибка"
								, MessageBoxButtons.OK
								, MessageBoxIcon.Error);
		}
		/// <summary>
		/// Показать сообщение об ошибке.
		/// </summary>
		/// <param name="errorMessage">Сообщение об ошибке.</param>
		public static void ShowErrorMessage(string errorMessage)
		{
			XtraMessageBox.Show(errorMessage
								, "Ошибка"
								, MessageBoxButtons.OK
								, MessageBoxIcon.Error);
		}
		/// <summary>
		/// Получить первые числа версии продукта (не более трёх).
		/// </summary>
		/// <param name="count">Количество первых чисел.</param>
		/// <returns>Результат.</returns>
		public static string GetNumbersOfVersion(int count)
		{
			if (count > 3) count = 3;

			string ver = Application.ProductVersion;

			int c = 0, inx = 0;

			for (int i = 0; i < ver.Length; ++i)
			{
				if (ver[i] == '.') ++c;

				if (c != count) continue;

				inx = i;
				break;
			}

            // ReSharper disable once RedundantAssignment
		    string debug = string.Empty;

			#if (DEBUG)
			debug = " (debug)";
			#endif 

			return  ver.Substring(0, inx) + debug;
		}
        /// <summary>
        /// Открыть сайт компании-разработчика.
        /// </summary>
        public static void OpenCompanySite()
        {
            try
            {
                System.Diagnostics.Process.Start("http://www.in-forma.pro/");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        #endregion Внешние вспомогательные статические методы класса.
	}
}