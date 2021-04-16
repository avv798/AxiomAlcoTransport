using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;

namespace Axiom.AlcoTransport
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
		/// Выполнить в контексте логгирования ошибки.
		/// </summary>
		public static void InLogErrorContext(Action action, bool showMessage=false)
		{
            try
            {
               action.Invoke();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                if (showMessage)
                    ShowErrorMessage($"Во время выполнения операции произошла ошибка: '{exception.Message}'.");
            }
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
		/// Получить версию продукта.
		/// </summary>
		/// <returns>Результат.</returns>
		public static string GetProductVersion()
		{
			string ver = Application.ProductVersion;

            // ReSharper disable once RedundantAssignment
		    string debug = string.Empty;

			#if (DEBUG)
			debug = "(debug)";
			#endif 

			return  string.Format("{0} {1}", ver, debug);
		}
        /// <summary>
        /// Развернуть детальный вид в правильное состояние.
        /// </summary>
        /// <param name="treeList">Дерево.</param>
	    public static void ExpandDetailView(TreeList treeList)
        {
            try
            {
                if (treeList == null) return;

                treeList.ExpandAll();

                if ((treeList.Nodes != null) && (treeList.Nodes.Count > 0))
                {
                    treeList.Nodes[0].Expanded = false;
                }
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время работы с детальным видом произошла ошибка.", exception);
            }
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
        /// <summary>
        /// Открыть руководство пользователя.
        /// </summary>
	    public static void OpenUserGuide()
	    {
            try
            {
                string filename = Program.GetAppSetting("userguideFilename");

                if (File.Exists(filename))
                {
                    System.Diagnostics.Process.Start(filename);
                }
                else throw new Exception(string.Format("Не найден файл руководства пользователя '{0}'.", filename));
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка.\r\n{0}", exception.Message));
            }
        }
        /// <summary>
        /// Получить значение атрибута "DisplayName".
        /// </summary>
        /// <param name="type">Тип данных.</param>
        /// <returns>Значение атрибута "DisplayName".</returns>
        public static string GetDisplayName(Type type)
        {
            DisplayNameAttribute attribute = (DisplayNameAttribute)Attribute.GetCustomAttribute(type, typeof(DisplayNameAttribute));

            return (attribute == null) ? string.Empty : attribute.DisplayName;
        }
		#endregion Внешние вспомогательные статические методы класса.
	}
}