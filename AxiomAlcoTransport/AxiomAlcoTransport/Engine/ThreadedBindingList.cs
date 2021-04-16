using System.Threading;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Модифицированный потокобезопасный класс "BindingList".
    /// </summary>
    public class ThreadedBindingList<T> : BindingList<T>
    {
        #region Внутренние объекты класса.
        /// <summary>
        /// Контекст выполнения.
        /// </summary>
        private readonly SynchronizationContext context;
        #endregion Внутренние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public ThreadedBindingList()
        {
            context = SynchronizationContext.Current;
        }
        #endregion Конструкторы класса.

        #region Внешние методы класса.
        /// <summary>
        /// Выбросить событие изменения состава списка.
        /// </summary>
        public void ForceChangeListEvent()
        {
            forceChangeListEvent();
        }
        #endregion Внешние методы класса.

        #region Защищённые методы класса.
        /// <summary>
        /// Выбросить событие изменения состава списка.
        /// <remarks>Грубый метод. Требует изменения.</remarks>
        /// </summary>
        protected virtual void forceChangeListEvent()
        {
            // TODO: переделать с использованием интерфейса "INotifyPropertyChanged"...

            lock (this)
            {
                if (Count > 0)
                {
                    T last = base[Count - 1];

                    Remove(last);
                    Add(last);
                }
            }
        }
        #endregion Защищённые методы класса.

        #region Переопределение методов базового класса.
        /// <summary>
        /// Переопределённый метод добавления.
        /// </summary>
        /// <param name="e">Данные события.</param>
        protected override void OnAddingNew(AddingNewEventArgs e)
        {
            SynchronizationContext current = SynchronizationContext.Current;

            if (current == null)
            {
                base.OnAddingNew(e);
            }
            else
            {
                current.Send(delegate { base.OnAddingNew(e); }, null);
            }
        }
        /// <summary>
        /// Переопределённый метод изменения.
        /// </summary>
        /// <param name="e">Данные события.</param>
        protected override void OnListChanged(ListChangedEventArgs e)
        {
            if (context == null)
            {
                base.OnListChanged(e);
            }
            else
            {
                context.Send(delegate { base.OnListChanged(e); }, null);
            }
        }
        #endregion Переопределение методов базового класса.
    }
}
