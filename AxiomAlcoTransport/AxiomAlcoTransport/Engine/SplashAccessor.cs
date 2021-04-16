using System;
using DevExpress.XtraSplashScreen;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Управление окном ожидания.
    /// Статический класс.
    /// </summary>
    public static class SplashAccessor
    {
        #region Внешние статические методы класса.
        /// <summary>
        /// Показать окно ожидания.
        /// </summary>
        public static void Show()
        {
            Show(string.Empty, string.Empty);
        }
        /// <summary>
        /// Показать окно ожидания.
        /// </summary>
        /// <param name="caption">Заголовок.</param>
        /// <param name="description">Описание.</param>
        public static void Show(string caption, string description)
        {
            try
            {
                Program.Logger.Info("SplashAccessor: Попытка показать окно ожидания...");

                if (SplashScreenManager.Default != null)
                {
                    Program.Logger.Info("SplashAccessor: ... окно ожидания уже показано.");
                    return;
                }

                SplashScreenManager.ShowForm(typeof(SplashForm));

                if (SplashScreenManager.Default == null) return;

                if (!string.IsNullOrWhiteSpace(caption)) SplashScreenManager.Default.SetWaitFormCaption(caption);
                if (!string.IsNullOrWhiteSpace(description)) SplashScreenManager.Default.SetWaitFormDescription(description);

                Program.Logger.Info("SplashAccessor: ... окно ожидания успешно показано.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("SplashAccessor: Во время открытия окна ожидания произошла ошибка.", exception);
            }
        }
        /// <summary>
        /// Закрыть окно ожидания.
        /// </summary>
        public static void Close()
        {
            try
            {
                Program.Logger.Info("SplashAccessor: Попытка закрыть окно ожидания...");

                if (SplashScreenManager.Default == null)
                {
                    Program.Logger.Info("SplashAccessor: ... окно ожидания уже закрыто.");
                    return;
                }

                SplashScreenManager.CloseForm();

                Program.Logger.Info("SplashAccessor: ... окно ожидания успешно закрыто.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("SplashAccessor: Во время закрытия окна ожидания произошла ошибка.", exception);
            }
        }
        #endregion Внешние статические методы класса.
    }
}
