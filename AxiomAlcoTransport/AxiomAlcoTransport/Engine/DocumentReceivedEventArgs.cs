using System.Collections.Generic;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Аргументы события.
    /// </summary>
    public class DocumentReceivedEventArgs
    {
        #region Внешние объекты класса.
        /// <summary>
        /// Текст сообщения.
        /// </summary>
        public List<string> List { get; private set; }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected DocumentReceivedEventArgs()
        {
            List = new List<string>();
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="list">Список полученных документов.</param>
        public DocumentReceivedEventArgs(IEnumerable<string> list) : this()
        {
            foreach (string s in list)
            {
                if (!List.Contains(s))
                {
                    List.Add(s);
                }
            }
        }
        #endregion Конструкторы класса.
    }
}
