using System;
using System.Globalization;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Axiom.AlcoTransport
{
    public partial class ActChargeOnDetailForm : XtraForm
    {
        #region Внутренние объекты класса.
        /// <summary>
        /// Дата розлива / Дата справки к ГТД.
        /// </summary>
        private DateTime bottlingDate;
        /// <summary>
        /// Дата ТТН / Дата справки к ГТД.
        /// </summary>
        private DateTime ttnDate;
        /// <summary>
        /// Номер ТТН от производителя / Номер ГТД.
        /// </summary>
        private string ttnNumber;
        /// <summary>
        /// Номер подтверждения фиксации в ЕГАИС.
        /// </summary>
        private string egaisNumber;
        /// <summary>
        /// Дата подтверждения фиксации в ЕГАИС.
        /// </summary>
        private DateTime egaisDate;
        /// <summary>
        /// Использовать помарочное сканирование.
        /// </summary>
        private bool useScan;
        /// <summary>
        /// Количество товара.
        /// </summary>
        private int quantity;
        /// <summary>
        /// Алкогольная продукция.
        /// Только чтение.
        /// </summary>
        private readonly Production production;
        #endregion Внутренние объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Дата розлива / Дата справки к ГТД.
        /// </summary>
        public DateTime BottlingDate
        {
            get { return bottlingDate; }
        }
        /// <summary>
        /// Дата ТТН / Дата справки к ГТД.
        /// </summary>
        public DateTime TtnDate
        {
            get { return ttnDate; }
        }
        /// <summary>
        /// Номер ТТН от производителя / Номер ГТД.
        /// </summary>
        public string TtnNumber
        {
            get { return ttnNumber; }
        }
        /// <summary>
        /// Номер подтверждения фиксации в ЕГАИС.
        /// </summary>
        public string EgaisNumber
        {
            get { return egaisNumber; }
        }
        /// <summary>
        /// Дата подтверждения фиксации в ЕГАИС.
        /// </summary>
        public DateTime EgaisDate
        {
            get { return egaisDate; }
        }
        /// <summary>
        /// Использовать помарочное сканирование.
        /// </summary>
        public bool UseScan
        {
            get { return useScan; }
        }
        /// <summary>
        /// Количество товара.
        /// </summary>
        public int Quantity
        {
            get { return quantity; }
        }
        #endregion Внешние объекты класса.

        #region Инициализация главной формы.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public ActChargeOnDetailForm()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации... ");

                InitializeComponent();

                useScan = true;
                bottlingDate = DateTime.Now;
                ttnDate = DateTime.Now;
                ttnNumber = string.Empty;
                egaisNumber = string.Empty;
                egaisDate = DateTime.Now;
                quantity = 1;

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
        /// <param name="production">Алкогольная продукция.</param>
        public ActChargeOnDetailForm(Production production) : this()
        {
            try
            {
                Program.Logger.Info(this, "Попытка инициализации с параметрами... ");

                this.production = production;

                Program.Logger.Info(this, "... инициализация с параметрами успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время инициализации произошла ошибка.", exception);
            }
        }
        #endregion Инициализация главной формы.

        #region Обработка событий пользовательского интерфейса.
        private void ActChargeOnDetailForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Logger.Info(this, "Форма закрывается.");
        }
        private void ActChargeOnDetailForm_Load(object sender, EventArgs e)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузки формы... ");

                useScan = attemptParseProduction();

                UseScan_checkEdit.Checked = UseScan;
                BottlingDate_dateEdit.EditValue = DateTime.Now;
                TTNNumber_textEdit.Text = string.Empty;
                TTNDate_dateEdit.EditValue = DateTime.Now;
                EGAISNumber_textEdit.Text = string.Empty;
                EGAISDate_dateEdit.EditValue = DateTime.Now;
                Quantity_spinEdit.Value = Quantity;

                AP_labelControl.Text = string.Format("Алкогольная продукция: '{0}', крепость: '{1}', ёмкость: '{2}', производитель: '{3}'.",
                                                     string.IsNullOrEmpty(production.ShortName) ? production.FullName : production.ShortName,
                                                     production.AlcVolume, production.Capacity, production.ProducerName);

                validateData();

                Program.Logger.Info(this, "... загрузка формы успешно завершена.");
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время загрузки произошла ошибка.", exception);
            }
        }
        private void Cancel_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        private void Ok_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                useScan = UseScan_checkEdit.Checked;
                bottlingDate = (DateTime)BottlingDate_dateEdit.EditValue;
                ttnNumber = TTNNumber_textEdit.Text;
                ttnDate = (DateTime)TTNDate_dateEdit.EditValue;
                egaisNumber = EGAISNumber_textEdit.Text;
                egaisDate = (DateTime)EGAISDate_dateEdit.EditValue;
                quantity = (int)Quantity_spinEdit.Value;

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        private void Help_simpleButton_Click(object sender, EventArgs e)
        {
            try
            {
                (new ActChargeOnHelpForm()).ShowDialog(this);
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        private void TTNNumber_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void EGAISNumber_textEdit_EditValueChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void BottlingDate_dateEdit_EditValueChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void TTNDate_dateEdit_EditValueChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void EGAISDate_dateEdit_EditValueChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void UseScan_checkEdit_CheckedChanged(object sender, EventArgs e)
        {
            validateData();
        }
        private void Quantity_spinEdit_EditValueChanged(object sender, EventArgs e)
        {
            validateData();
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
                EGAISNumber_textEdit.Enabled = EGAISDate_dateEdit.Enabled = UseScan_checkEdit.Checked;

                if (UseScan_checkEdit.Checked)
                {
                    EGAISNumber_textEdit.Enabled = EGAISDate_dateEdit.Enabled = ((DateTime) BottlingDate_dateEdit.EditValue >= Documents.BorderBottlingDate);
                }

                bool valid = ((!string.IsNullOrWhiteSpace(TTNNumber_textEdit.Text))
                                && ((DateTime)BottlingDate_dateEdit.EditValue <= DateTime.Now)
                                && ((DateTime)TTNDate_dateEdit.EditValue <= DateTime.Now)
                                && ((int)Quantity_spinEdit.Value > 0));

                if ((UseScan_checkEdit.Checked) && ((DateTime)BottlingDate_dateEdit.EditValue >= Documents.BorderBottlingDate))
                {
                    if (string.IsNullOrWhiteSpace(EGAISNumber_textEdit.Text)) valid = false;
                }

                Ok_simpleButton.Enabled = valid;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время обработки события пользовательского интерфейса произошла ошибка.", exception);
            }
        }
        /// <summary>
        /// Попытаться понять, нужно ли для данной алкогольной продукции помарочное сканирование.
        /// </summary>
        /// <returns>Признак необходимости помарочного сканирования.</returns>
        private bool attemptParseProduction()
        {
            try
            {
                if (convertToDecimal(production.AlcVolume) >= 9.0m) return true;

                string name = (string.IsNullOrWhiteSpace(production.FullName)) ? production.ShortName.ToLower() : production.FullName.ToLower();
                if (name.Contains("пиво ")) return false;
                if (name.Contains("сидр ")) return false;
                if (name.Contains("пуаре ")) return false;
                if (name.Contains("медовуха ")) return false;
                if (name.Contains("на основе пива")) return false;
                if (name.Contains("пивной напиток")) return false;
                if (name.Contains("напиток пивной")) return false;

                return true;
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время синтаксического анализа алкогольной продукции произошла ошибка.", exception);

                return true;
            }
        }
        /// <summary>
        /// Преобразовать строку в число.
        /// </summary>
        /// <param name="str">Строка.</param>
        /// <returns>Результат.</returns>
        private decimal convertToDecimal(string str)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(str)) throw new Exception("Строка для преобразования пустая.");

                return Convert.ToDecimal(str.Trim().Replace(" ", "").Replace(",", "."), new NumberFormatInfo { NumberDecimalSeparator = ".", NumberGroupSeparator = string.Empty });
            }
            catch (Exception exception)
            {
                Program.Logger.Error(this, string.Format("При преобразовании строки '{0}' в число произошла ошибка '{1}'.", str, exception));

                throw;
            }
        }
        #endregion Внутренние методы класса.
    }
}