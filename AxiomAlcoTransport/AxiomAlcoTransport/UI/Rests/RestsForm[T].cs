using System;
using System.Linq;
using System.Windows.Forms;
using Axiom.AlcoTransport.Watchtower.Configuration;
using DevExpress.XtraEditors;

namespace Axiom.AlcoTransport
{
    public class RestsForm<T> : RestsForm
        where T : ARests
    {
        #region Защищённые объекты класса.
        /// <summary>
        /// Тип документа.
        /// Только чтение.
        /// </summary>
        protected readonly RestsType restsType;
        #endregion Защищённые объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="documents">Список документов.</param>
        public RestsForm(Documents documents) : base(documents)
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации с параметрами (generic class)... ");

                switch (typeof(T).Name)
                {
                    case "Rests": { restsType = RestsType.Main; break; }
                    case "ShopRests": { restsType = RestsType.Shop; break; }
                    case "BCodeRests": { restsType = RestsType.BCode; break; }

                    default: throw new Exception(string.Format("Работа с классом '{0}' не поддерживается в данной версии.", typeof(T).FullName));
                }

                Program.Logger.Info(this, string.Format("... определён обобщённый класс формы: '{0}'...", restsType));

                Program.Logger.Info(this, "... инициализация (generic class) с параметрами успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации (generic class) произошла ошибка.", exception);
            }
        }
        #endregion Конструкторы класса.

        #region Переопределение объектов базового класса.
        /// <summary>
        /// Путь в реестре для сохранения раскладки грида.
        /// Только чтение.
        /// </summary>
        protected override string registryPathVisualSettings
        {
            get { return string.Format("{0}\\RestsForm\\{1}", Constants.RegistryPathVisualSettings, typeof(T).Name); }
        }
        #endregion Переопределение объектов базового класса.

        #region Переопределение методов базового класса.
        /// <summary>
        /// Загрузить данные.
        /// </summary>
        protected override void loadData()
        {
            switch (restsType)
            {
                case RestsType.Main: { Document_bindingSource.DataSource = documents.RestsList; break; }
                case RestsType.Shop: { Document_bindingSource.DataSource = documents.ShopRestsList; break; }
                case RestsType.BCode: { Document_bindingSource.DataSource = documents.BCodeRestsList; break; }

                default: throw new Exception(string.Format("Работа с типом '{0}' не поддерживается в данной версии.", restsType));
            }
        }
        /// <summary>
        /// Отправить запрос.
        /// </summary>
        protected override void sendRequest()
        {
            if (restsType == RestsType.BCode)
            {
                MainForm.InLogErrorContext(() => {
                    ARests selected = documents.RestsList.OrderByDescending(rests => rests.CreateDateTime).FirstOrDefault();

                    if (selected == null) throw new Exception("Для формирования запроса остатков в разрезе штрихкодов необходимо наличие остатков!");

                    new RestsPositionViewForm(selected, documents, true).ShowDialog();
                }, true);
                
            }
            else
            try
            {
                if (DialogResult.Yes == XtraMessageBox.Show("Отправить на сервер запрос на получение документа-остатков?"
                                                          , "Подтверждение операции"
                                                          , MessageBoxButtons.YesNo
                                                          , MessageBoxIcon.Question))
                {
                    SplashAccessor.Show();
                    Cursor = Cursors.WaitCursor;
                    MainRibbonControl.Enabled = false;

                    switch (restsType)
                    {
                        case RestsType.Main: { documents.RequestRests(); break; }
                        case RestsType.Shop: { documents.RequestShopRests(); break; }

                        default: throw new Exception(string.Format("Работа с типом '{0}' не поддерживается в данной версии.", restsType));
                    }
                }
            }
            catch (Exception exception)
            {
                const string msg = "Во время отправки запроса на получение документа-остатков организаций произошла ошибка.";
                Program.Logger.Error(msg, exception);

                MainRibbonControl.Enabled = true;
                SplashAccessor.Close();
                Cursor = Cursors.Default;

                MainForm.ShowErrorMessage(string.Format("{0}\r\n{1}", msg, exception.Message));
            }
            finally
            {
                MainRibbonControl.Enabled = true;
                Cursor = Cursors.Default;

                SplashAccessor.Close();
            }
        }
        #endregion Переопределение методов базового класса.
    }
}
