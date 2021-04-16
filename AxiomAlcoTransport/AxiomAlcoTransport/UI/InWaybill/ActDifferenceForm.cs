using Axiom.AlcoTransport.Engine.BarcodeCheck;
using Axiom.AlcoTransport.Watchtower.Configuration;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Axiom.AlcoTransport
{
    public partial class ActDifferenceForm : XtraForm
    {
        #region Внутренние объекты класса.

        /// <summary>
        /// Входящая накладная.
        /// Только чтение.
        /// </summary>
        private readonly InWaybill inWaybill;

        /// <summary>
        /// Управление документами.
        /// Только чтение.
        /// </summary>
        private readonly Documents documents;

        /// <summary>
        /// Список позиций.
        /// Только чтение.
        /// </summary>
        private readonly List<Position> positions;

        #endregion Внутренние объекты класса.

        #region Инициализация главной формы.

        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public ActDifferenceForm()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                documents = null;
                inWaybill = null;
                positions = null;

                Program.Logger.Info(this, "... инициализация успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации произошла ошибка.", exception);
            }
        }

        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="documents">Управление документами.</param>
        /// <param name="inWaybill">Входящая накладная.</param>
        /// <param name="barCodePositions"></param>
        public ActDifferenceForm(Documents documents, InWaybill inWaybill,
            List<BarcodePosition> barCodePositions = null) : this()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации с параметрами... ");

                this.documents = documents;
                this.inWaybill = inWaybill;
                positions = this.inWaybill.GetPositions();

                //Пытаемся установить штрихкоды в соотвествии со сканированными
                if (barCodePositions != null)
                    positions.ForEach(position =>
                    {
                        foreach (var positionBoxInfo in position.BoxInfos)
                        {
                            foreach (var barcode in positionBoxInfo.AmcList)
                            {
                                if (
                                    barCodePositions.Any(
                                        barcodePosition => barcodePosition.BarcodeInput == barcode.Barcode))
                                    barcode.RealBarcode = barcode.Barcode;
                                else
                                    position.RealQuantity--;
                            }
                        }
                    });
                else
                    positions.ForEach(position =>
                    {
                        foreach (var positionBoxInfo in position.BoxInfos)
                        {
                            foreach (var barcode in positionBoxInfo.AmcList)
                            {
                                barcode.RealBarcode = barcode.Barcode;
                            }
                        }
                    });

                Document_BindingSource.DataSource = positions;
                positionBindingSource.DataSource = positions;

                Program.Logger.Info(this, "... инициализация с параметрами успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации произошла ошибка.", exception);
            }
        }

        #endregion Инициализация главной формы.

        #region Обработка событий пользовательского интерфейса.

        private void Close_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.",
                    exception);
            }
        }

        private void ActDifferenceForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Форма закрывается.");

                saveGridLayot();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время закрытия произошла ошибка.", exception);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void ActDifferenceForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузки формы... ");

                loadGridLayot();

                Caption_labelControl.Text = string.Format("Создать и отправить акт расхождения по входящей накладной " +
                                                          "за номером '{0}' " +
                                                          "(номер по документу регистрации движения - '{1}'; " +
                                                          "отправитель - '{2}', " +
                                                          "дата составления - '{3}', " +
                                                          "дата отгрузки продукции - '{4}'), " +
                                                          "полученной '{5}'.",
                    inWaybill.Identity,
                    inWaybill.WBRegId,
                    inWaybill.ShipperName,
                    inWaybill.Date,
                    inWaybill.ShippingDate,
                    inWaybill.CreateDateTime);

                Number_textEdit.Text = string.Format("{0}-difference-{1}", inWaybill.WBRegId,
                    DateTime.Now.ToString("yyMMddHHmmss"));
                Comment_textEdit.Text = string.Empty;

                validateData();

                Program.Logger.Info(this, "... загрузка формы успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время загрузки произошла ошибка.", exception);
            }
        }

        private void Send_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (!validatePositions())
                {
                    XtraMessageBox.Show("Скорректированный список позиций не отличается от оригинального. " +
                                        "Формирование акта расхождения не имеет смысла.\r\n\r\n" +
                                        "Актом расхождения в ЕГАИС может оформляться только недостача продукции.\r\n" +
                                        "Излишки поставленной продукции оформляются отдельной товарно-транспортной накладной.",
                        "Предупреждение об ошибке", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                Cursor = Cursors.WaitCursor;
                SplashAccessor.Show();

                documents.SendDifferenceAct(inWaybill, positions, Number_textEdit.Text, Comment_textEdit.Text);

                SplashAccessor.Close();
                Cursor = Cursors.Default;

                XtraMessageBox.Show("Акт расхождения успешно отправлен.", "Сообщение", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                Close();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время отправки акта произошла ошибка.", exception);

                SplashAccessor.Close();
                Cursor = Cursors.Default;

                MainForm.ShowErrorMessage(string.Format("Во время отправки акта произошла ошибка: '{0}'.",
                    exception.Message));
            }
        }

        private void Number_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            validateData();
        }

        private void Comment_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            validateData();
        }

        private void Print_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Document_gridControl.IsPrintingAvailable) Document_gridControl.ShowRibbonPrintPreview();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'",
                    exception));
            }
        }

        private void Find_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Document_gridView.OptionsFind.AlwaysVisible = !Document_gridView.OptionsFind.AlwaysVisible;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }

        private void Group_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Document_gridView.OptionsView.ShowGroupPanel = !Document_gridView.OptionsView.ShowGroupPanel;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }

        private void Filter_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Document_gridView.Columns.Count > 1)
                    Document_gridView.ShowFilterEditor(Document_gridView.Columns[1]);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }

        private void Columns_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Document_gridView.ShowCustomization();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }

        private void Refresh_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Document_gridView.RefreshData();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }

        private void Document_gridView_CellValueChanged(object sender,
            DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                Document_gridView.RefreshData();
                validateData();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }

        #endregion Обработка событий пользовательского интерфейса.

        #region Внутренние методы класса.

        /// <summary>
        /// Проверить данные.
        /// </summary>
        private void validateData()
        {
            try
            {
                Send_simpleButton.Enabled = ((!string.IsNullOrWhiteSpace(Number_textEdit.Text))
                                             && (!string.IsNullOrWhiteSpace(Comment_textEdit.Text))
                                             && (validatePositions())
                                             && (positions.Count > 0));
            }
            catch (Exception exception)
            {
                Send_simpleButton.Enabled = false;

                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.",
                    exception);
            }
        }

        /// <summary>
        /// Проверить позиции.
        /// </summary>
        /// <returns>Признак корректности.</returns>
        private bool validatePositions()
        {
            return positions.Any(position => (position.RealQuantity < position.Quantity)) || !positions.TrueForAll(position=>position.BoxInfos.TrueForAll(box=>box.AmcList.TrueForAll(amc=>amc.Barcode==amc.RealBarcode)));
        }

        #endregion Внутренние методы класса.

        #region Раскладка грида.

        /// <summary>
        /// Путь в реестре для сохранения раскладки грида.
        /// Только чтение.
        /// </summary>
        private string registryPathVisualSettings
        {
            get { return string.Format("{0}\\{1}", Constants.RegistryPathVisualSettings, GetType().Name); }
        }

        /// <summary>
        /// Загрузка оформления редактора.
        /// </summary>
        private void loadGridLayot()
        {
            try
            {
                Document_gridView.RestoreLayoutFromRegistry(registryPathVisualSettings);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции загрузки раскладки грида произошла ошибка.",
                    exception);
            }
        }

        /// <summary>
        /// Сохранение оформления редактора.
        /// </summary>
        private void saveGridLayot()
        {
            try
            {
                Document_gridView.SaveLayoutToRegistry(registryPathVisualSettings);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции сохранения раскладки грида произошла ошибка.",
                    exception);
            }
        }

        #endregion Раскладка грида.

        private void barcodes_gridView_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            validateData();
        }
    }
}