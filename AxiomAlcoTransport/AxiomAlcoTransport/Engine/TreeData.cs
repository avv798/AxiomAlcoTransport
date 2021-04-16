using System.Collections;
using DevExpress.XtraTreeList;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Функционал представления иерархических данных.
    /// </summary>
    public class TreeData : TreeList.IVirtualTreeListData
    {
        #region Защищённые объекты класса.
        /// <summary>
        /// Список объектов элемента.
        /// </summary>
        protected readonly object[] cellsCore;
        /// <summary>
        /// Родительский элемент.
        /// </summary>
        protected readonly TreeData parentCore;
        /// <summary>
        /// Список подчинённых объектов.
        /// </summary>
        protected readonly ArrayList childrenCore = new ArrayList();
        #endregion Защищённые объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected TreeData()
        {
            cellsCore = null;
            parentCore = null;
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="parent">Родительский элемент.</param>
        /// <param name="cells">Список объектов.</param>
        public TreeData(TreeData parent, object[] cells) : this()
        {
            cellsCore = cells;

            parentCore = parent;

            if (parentCore != null)
            {
                parentCore.childrenCore.Add(this);
            }
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="parent">Родительский элемент.</param>
        /// <param name="title">Данные - заголовок.</param>
        /// <param name="value">Данные - значение.</param>
        public TreeData(TreeData parent, string title, string value)
        {
            cellsCore = new object[]
                            {
                                Program.Language.Translate(title),
                                Program.Language.TranslateReference(ADocument.TryParseDateTime(value))
                            };

            parentCore = parent;

            if (parentCore != null)
            {
                parentCore.childrenCore.Add(this);
            }
        }
        #endregion Конструкторы класса.

        #region Реализация интерфейса 'TreeList.IVirtualTreeListData'.
        void TreeList.IVirtualTreeListData.VirtualTreeGetChildNodes(VirtualTreeGetChildNodesInfo info)
        {
            info.Children = childrenCore;
        }
        void TreeList.IVirtualTreeListData.VirtualTreeGetCellValue(VirtualTreeGetCellValueInfo info)
        {
            info.CellData = cellsCore[info.Column.AbsoluteIndex];
        }
        void TreeList.IVirtualTreeListData.VirtualTreeSetCellValue(VirtualTreeSetCellValueInfo info)
        {
            cellsCore[info.Column.AbsoluteIndex] = info.NewCellData;
        }
        #endregion Реализация интерфейса 'TreeList.IVirtualTreeListData'.
    }
}
