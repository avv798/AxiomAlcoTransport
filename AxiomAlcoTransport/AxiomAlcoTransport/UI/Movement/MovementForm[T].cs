using System;
using System.Windows.Forms;
using Axiom.AlcoTransport.Document;
using Axiom.AlcoTransport.UI.Movement;
using Axiom.AlcoTransport.Watchtower.Configuration;
using DevExpress.XtraEditors;

namespace Axiom.AlcoTransport
{
    public class MovementForm<T> : MovementForm
        where T : AMovement
    {
        #region Защищённые объекты класса.
        /// <summary>
        /// Тип документа.
        /// Только чтение.
        /// </summary>
        protected readonly MovementType movementType;
        #endregion Защищённые объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="documents">Список документов.</param>
        public MovementForm(Documents documents) : base(documents)
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации с параметрами (generic class)... ");

                switch (typeof(T).Name)
                {
                    case "ActChargeOn": { movementType = MovementType.ActChargeOn; break; }
                    case "ActChargeOff": { movementType = MovementType.ActChargeOff; break; }
                    case "ActChargeOnShop": { movementType = MovementType.ActChargeOnShop; break; }
                    case "ActChargeOffShop": { movementType = MovementType.ActChargeOffShop; break; }
                    case "TransferToShop": { movementType = MovementType.TransferToShop; break; }
                    case "TransferFromShop": { movementType = MovementType.TransferFromShop; break; }
                    case "ActFixBarcode": { movementType = MovementType.ActFixBarcode; break; }
                    case "ActUnFixBarcode": { movementType = MovementType.ActUnFixBarcode; break; }

                    default: throw new Exception(string.Format("Работа с классом '{0}' не поддерживается в данной версии.", typeof(T).FullName));
                }

                Repeal_barButtonItem.Enabled = ((movementType == MovementType.ActChargeOn) || (movementType == MovementType.ActChargeOff));

                Program.Logger.Info(this, string.Format("... определён обобщённый класс формы: '{0}'...", movementType));

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
            get { return string.Format("{0}\\MovementForm\\{1}", Constants.RegistryPathVisualSettings, typeof(T).Name); }
        }
        #endregion Переопределение объектов базового класса.

        #region Переопределение методов базового класса.
        /// <summary>
        /// Загрузить данные.
        /// </summary>
        protected override void loadData()
        {
            switch (movementType)
            {
                case MovementType.ActChargeOn: { Document_bindingSource.DataSource = documents.ActChargeOnList; break; }
                case MovementType.ActChargeOff: { Document_bindingSource.DataSource = documents.ActChargeOffList; break; }
                case MovementType.ActChargeOnShop: { Document_bindingSource.DataSource = documents.ActChargeOnShopList; break; }
                case MovementType.ActChargeOffShop: { Document_bindingSource.DataSource = documents.ActChargeOffShopList; break; }
                case MovementType.TransferToShop: { Document_bindingSource.DataSource = documents.TransferToShopList; break; }
                case MovementType.TransferFromShop: { Document_bindingSource.DataSource = documents.TransferFromShopList; break; }
                case MovementType.ActFixBarcode: { Document_bindingSource.DataSource = documents.ActFixBarcodeList; break; }
                case MovementType.ActUnFixBarcode: { Document_bindingSource.DataSource = documents.ActUnFixBarcodeList; break; }

                default: throw new Exception(string.Format("Работа с типом '{0}' не поддерживается в данной версии.", movementType));
            }
        }
        /// <summary>
        /// Добавить документ.
        /// </summary>
        protected override void addDocument()
        {
            try
            {
                switch (movementType)
                {
                    case MovementType.ActChargeOn: { (new ActChargeOnEditorForm(documents, new ActChargeOn())).ShowDialog(this); break; }
                    case MovementType.ActChargeOff: { (new ActChargeOffEditorForm(documents, new ActChargeOff())).ShowDialog(this); break; }
                    case MovementType.ActChargeOnShop: { (new ActChargeOnShopEditorForm(documents, new ActChargeOnShop())).ShowDialog(this); break; }
                    case MovementType.ActChargeOffShop: { (new ActChargeOffShopEditorForm(documents, new ActChargeOffShop())).ShowDialog(this); break; }
                    case MovementType.TransferToShop: { (new TransferToShopEditorForm(documents, new TransferToShop())).ShowDialog(this); break; }
                    case MovementType.TransferFromShop: { (new TransferFromShopEditorForm(documents, new TransferFromShop())).ShowDialog(this); break; }
                    case MovementType.ActFixBarcode: { new ActFixBarcodeEditorForm<ActFixBarcode>(documents, new ActFixBarcode()).ShowDialog(this); break; }
                    case MovementType.ActUnFixBarcode: { new ActFixBarcodeEditorForm<ActUnFixBarcode>(documents, new ActUnFixBarcode()).ShowDialog(this); break; }

                    default: throw new Exception(string.Format("Работа с типом '{0}' не поддерживается в данной версии.", movementType));
                }

                Document_gridView.RefreshData();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception.Message));
            }
        }
        /// <summary>
        /// Изменить или просмотреть документ.
        /// </summary>
        protected override void editDocument()
        {
            try
            {
                if (Document_bindingSource.Current == null) return;

                T selected = (T)Document_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                switch (movementType)
                {
                    case MovementType.ActChargeOn: { (new ActChargeOnEditorForm(documents, selected as ActChargeOn)).ShowDialog(this); break; }
                    case MovementType.ActChargeOff: { (new ActChargeOffEditorForm(documents, selected as ActChargeOff)).ShowDialog(this); break; }
                    case MovementType.ActChargeOnShop: { (new ActChargeOnShopEditorForm(documents, selected as ActChargeOnShop)).ShowDialog(this); break; }
                    case MovementType.ActChargeOffShop: { (new ActChargeOffShopEditorForm(documents, selected as ActChargeOffShop)).ShowDialog(this); break; }
                    case MovementType.TransferToShop: { (new TransferToShopEditorForm(documents, selected as TransferToShop)).ShowDialog(this); break; }
                    case MovementType.TransferFromShop: { (new TransferFromShopEditorForm(documents, selected as TransferFromShop)).ShowDialog(this); break; }
                    case MovementType.ActFixBarcode: { new ActFixBarcodeEditorForm<ActFixBarcode>(documents, selected as ActFixBarcode).ShowDialog(this); break; }
                    case MovementType.ActUnFixBarcode: { new ActFixBarcodeEditorForm<ActUnFixBarcode>(documents, selected as ActUnFixBarcode).ShowDialog(this); break; }

                    default: throw new Exception(string.Format("Работа с типом '{0}' не поддерживается в данной версии.", movementType));
                }

                Document_gridView.RefreshData();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception.Message));
            }
        }
        /// <summary>
        /// Клонировать документ.
        /// </summary>
        protected override void cloneDocument()
        {
            try
            {
                if (Document_bindingSource.Current == null) return;

                T selected = (T)Document_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                switch (movementType)
                {
                    case MovementType.ActChargeOn: { (new ActChargeOnEditorForm(documents, new ActChargeOn(selected as ActChargeOn))).ShowDialog(this); break; }
                    case MovementType.ActChargeOff: { (new ActChargeOffEditorForm(documents, new ActChargeOff(selected as ActChargeOff))).ShowDialog(this); break; }
                    case MovementType.ActChargeOnShop: { (new ActChargeOnShopEditorForm(documents, new ActChargeOnShop(selected as ActChargeOnShop))).ShowDialog(this); break; }
                    case MovementType.ActChargeOffShop: { (new ActChargeOffShopEditorForm(documents, new ActChargeOffShop(selected as ActChargeOffShop))).ShowDialog(this); break; }
                    case MovementType.TransferToShop: { (new TransferToShopEditorForm(documents, new TransferToShop(selected as TransferToShop))).ShowDialog(this); break; }
                    case MovementType.TransferFromShop: { (new TransferFromShopEditorForm(documents, new TransferFromShop(selected as TransferFromShop))).ShowDialog(this); break; }
                    case MovementType.ActFixBarcode: { new ActFixBarcodeEditorForm<ActFixBarcode>(documents, new ActFixBarcode(selected as ActFixBarcode)).ShowDialog(this); break; }
                    case MovementType.ActUnFixBarcode: { new ActFixBarcodeEditorForm<ActUnFixBarcode>(documents, new ActUnFixBarcode(selected as ActUnFixBarcode)).ShowDialog(this); break; }
                    default: throw new Exception(string.Format("Работа с типом '{0}' не поддерживается в данной версии.", movementType));
                }

                Document_gridView.RefreshData();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception.Message));
            }
        }
        /// <summary>
        /// Удалить документ.
        /// </summary>
        protected override void removeDocument()
        {
            try
            {
                if (Document_bindingSource.Current == null) return;

                T selected = (T)Document_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                if ((selected.StatusEnum == MovementStatus.Partial) || (selected.StatusEnum == MovementStatus.New))
                {
                    if (DialogResult.Yes == XtraMessageBox.Show(string.Format("Удалить документ с номером '{0}' от '{1}'?\r\n" +
                                                                              "Восстановление удалённых документов невозможно."
                                                                              , selected.Number, selected.Date.ToString("dd MMMM yyyy (dddd), HH:mm:ss"))
                                                                  , "Подтверждение операции"
                                                                  , MessageBoxButtons.YesNo
                                                                  , MessageBoxIcon.Question))
                    {
                        documents.Delete(selected);
 
                        Document_gridView.RefreshData();

                        XtraMessageBox.Show(string.Format("Документ с номером '{0}' от '{1}' успешно удалён.",
                                            selected.Number, selected.Date.ToString("dd MMMM yyyy (dddd), HH:mm:ss")),
                                            "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    XtraMessageBox.Show("Операция удаления разрешена для документов, находящихся " +
                                        "в состоянии \"новый, заполнен частично\" или \"новый, готов к отправке\".",
                                        "Выполнение операции невозможно", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception.Message));
            }
        }
        /// <summary>
        /// Отправить документ.
        /// </summary>
        protected override void sendDocument()
        {
            try
            {
                if (Document_bindingSource.Current == null) return;

                T selected = (T)Document_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                if (selected.StatusEnum != MovementStatus.New)
                {
                    XtraMessageBox.Show("Отправить можно только такой документ, который находится в статусе 'новый, готов к отправке'.",
                                        "Выполнение операции невозможно", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                if (DialogResult.Yes == XtraMessageBox.Show(string.Format("Отправить документ с номером '{0}' от '{1}' на сервер ЕГАИС?",
                                                            selected.Number, selected.Date.ToString("dd MMMM yyyy (dddd), HH:mm:ss")),
                                                            "Подтверждение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    SplashAccessor.Show();
                    Cursor = Cursors.WaitCursor;
                    MainRibbonControl.Enabled = false;

                    documents.Send(selected);

                    Document_gridView.RefreshData();

                    MainRibbonControl.Enabled = true;
                    Cursor = Cursors.Default;
                    SplashAccessor.Close();

                    XtraMessageBox.Show(string.Format("Документ с номером '{0}' от '{1}' успешно отправлен.",
                                        selected.Number, selected.Date.ToString("dd MMMM yyyy (dddd), HH:mm:ss")),
                                        "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                MainRibbonControl.Enabled = true;
                SplashAccessor.Close();
                Cursor = Cursors.Default;

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка: '{0}'.", exception.Message));
            }
            finally
            {
                MainRibbonControl.Enabled = true;
                Cursor = Cursors.Default;

                SplashAccessor.Close();
            }
        }
        /// <summary>
        /// Проверить документ.
        /// </summary>
        protected override void checkDocument()
        {
            try
            {
                if (Document_bindingSource.Current == null) return;

                T selected = (T)Document_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                if ((selected.StatusEnum == MovementStatus.Partial)
                    || (selected.StatusEnum == MovementStatus.New))
                {
                    selected.Check(false);

                    XtraMessageBox.Show("Проверка успешно завершена. Акт движения товара заполнен корректно.",
                                        "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время проверки акта произошла ошибка.", exception);

                XtraMessageBox.Show(string.Format("Во время проверки акта найдена ошибка.\r\n{0}", exception.Message),
                                    "Обнаружена ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        /// <summary>
        /// Отменить проведение документа.
        /// </summary>
        protected override void repealDocument()
        {
            try
            {
                if (Document_bindingSource.Current == null) return;

                T selected = (T)Document_bindingSource.Current;

                if (selected == null) throw new Exception("Ошибка получения текущего документа.");

                if (selected.StatusEnum != MovementStatus.Confirmed)
                {
                    XtraMessageBox.Show("Отменить проведение акта можно для документа, который находится в статусе 'документ подтверждён ЕГАИС'.",
                                        "Выполнение операции невозможно", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                if (string.IsNullOrWhiteSpace(selected.EgaisRegId))
                {
                    XtraMessageBox.Show("К сожалению, данная версия документа не поддерживает отмену проведения акта движения товара.\r\n" +
                                        "Обратитесь в службу технической поддержки компании-разработчика.",
                                        "Выполнение операции невозможно", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                if (DialogResult.Yes == XtraMessageBox.Show(string.Format("Отменить проведения акта с номером '{0}' от '{1}'?",
                                                            selected.Number, selected.Date.ToString("dd MMMM yyyy (dddd), HH:mm:ss")),
                                                            "Подтверждение операции", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    SplashAccessor.Show();
                    Cursor = Cursors.WaitCursor;
                    MainRibbonControl.Enabled = false;

                    switch (movementType)
                    {
                        case MovementType.ActChargeOn:
                        case MovementType.ActChargeOff:
                            {
                                documents.Repeal(selected);
                                break;
                            }

                        default: throw new Exception(string.Format("Работа с типом '{0}' не поддерживается в данной версии.", movementType));
                    }

                    Document_gridView.RefreshData();

                    MainRibbonControl.Enabled = true;
                    Cursor = Cursors.Default;
                    SplashAccessor.Close();

                    XtraMessageBox.Show(string.Format("Проведение акта с номером '{0}' от '{1}' отменено.",
                                        selected.Number, selected.Date.ToString("dd MMMM yyyy (dddd), HH:mm:ss")),
                                        "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Document_gridView.RefreshData();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception.Message));
            }
        }
        #endregion Переопределение методов базового класса.
    }
}
