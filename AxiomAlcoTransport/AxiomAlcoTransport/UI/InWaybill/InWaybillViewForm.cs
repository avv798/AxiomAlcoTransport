using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Axiom.AlcoTransport
{
    public partial class InWaybillViewForm : XtraForm
    {
        #region Внутренние объекты класса.
        /// <summary>
        /// Входящая накладная.
        /// Только чтение.
        /// </summary>
        private readonly InWaybill inWaybill;
        #endregion Внутренние объекты класса.

        #region Инициализация главной формы.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public InWaybillViewForm()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                Main_xtraTabControl.SelectedTabPageIndex = 0;

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
        /// <param name="inWaybill">Накладная.</param>
        public InWaybillViewForm(InWaybill inWaybill) : this()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации с параметрами... ");

                this.inWaybill = inWaybill;

                if (this.inWaybill != null)
                {
                    Detail_treeList.DataSource = inWaybill.TreeData;
                    MainForm.ExpandDetailView(Detail_treeList);
                }
                else
                {
                    throw new Exception("Накладная не может быть пустым объектом.");
                }

                Program.Logger.Info(this, "... инициализация с параметрами успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации произошла ошибка.", exception);
            }
        }
        #endregion Инициализация главной формы.

        #region Обработка событий пользовательского интерфейса.
        private void InWaybillViewForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузки формы... ");

                Cursor = Cursors.WaitCursor;

                if (inWaybill == null) throw new Exception("Накладная не может быть пустым объектом.");

                Status_textEdit.Text = inWaybill.StatusNote;
                WBRegId_textEdit.Text = inWaybill.WBRegId;
                Identity_textEdit.Text = inWaybill.Identity;
                CreateDate_textEdit.Text = inWaybill.CreateDateTime.ToString("dd MMMM yyyy (ddd) HH:mm:ss");
                Date_textEdit.Text = inWaybill.Date;
                Number_textEdit.Text = inWaybill.Number;
                ShipDate_textEdit.Text = inWaybill.ShippingDate;
                Shipper_textEdit.Text = inWaybill.ShipperName;
                Consignee_textEdit.Text = inWaybill.ConsigneeName;

                Program.Logger.Info(this, "... загрузка формы успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время загрузки произошла ошибка.", exception);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void Print_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (Detail_treeList.IsPrintingAvailable) Detail_treeList.ShowRibbonPrintPreview();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);

                MainForm.ShowErrorMessage(string.Format("Во время выполнения операции произошла ошибка:\r\n'{0}'", exception));
            }
        }
        private void CollapseTree_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Detail_treeList.CollapseAll();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void ExpandTree_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                MainForm.ExpandDetailView(Detail_treeList);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void Close_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время выполнения операции произошла ошибка.", exception);
            }
        }
        private void InWaybillViewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Форма закрывается.");
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
        #endregion Обработка событий пользовательского интерфейса.
    }
}
