using System;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Списки документов приложения.
    /// </summary>
    public partial class Documents
    {
        #region Периодический опрос сервера.
        private void onRequestTimedEvent(object source, EventArgs e)
        {
            Program.Logger.Info(this, "Background: Попытка получить входящие документы в фоновом режиме...");

            BackgroundWorker requestBackgroundWorker = new BackgroundWorker
            {
                WorkerSupportsCancellation = true
            };

            requestBackgroundWorker.DoWork += requestBackground;
            requestBackgroundWorker.RunWorkerCompleted += requestBackgroundCompleted;
            requestBackgroundWorker.RunWorkerAsync();
        }
        private void requestBackground(object sender, DoWorkEventArgs e)
        {
            try
            {
                getDocuments();

                onlineEvents.Add(new OnlineEvent(this, "Получение документов в фоновом режиме завершено."));
            }
            catch (Exception exception)
            {
                onlineEvents.Add(new OnlineEvent(this, "Во время получения входящих документов в фоновом режиме произошла ошибка.", exception));

                Program.Logger.Error("Background: Во время получения входящих документов в фоновом режиме произошла ошибка.", exception);
            }
        }
        private void requestBackgroundCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Program.Logger.Info(this, "Background: ... попытка получения входящие документов в фоновом режиме завершена.");
        }
        #endregion Периодический опрос сервера.

        #region Background...
        /// <summary>
        /// Запустить фоновый опрос сервера.
        /// </summary>
        protected virtual void startBackgroundRequest()
        {
            if (!requestTimer.IsEnabled)
            {
                requestTimer.Start();
            }
        }
        /// <summary>
        /// Остановить фоновый опрос сервера.
        /// </summary>
        protected virtual void stopBackgroundRequest()
        {
            if (requestTimer.IsEnabled)
            {
                requestTimer.Stop();
            }
        }
        #endregion Background...
    }
}
