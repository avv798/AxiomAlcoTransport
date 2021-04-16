using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Функционал позиции документов движения товара.
    /// </summary>
    [Serializable]
    public class MovePosition
    {
        #region Внешние константы класса.
        /// <summary>
        /// Длина кода PDF-417 по документации ЕГАИС.
        /// <remarks>
        /// Состав информации на марке определяется приказом Росалкогольрегулирования N33н от 12.05.2010.
        /// При осуществлении продажи требуется сканировать двумерный штриховой код.
        /// Штрих код имеет формат PDF-417. Так же требуется сканировать EAN-код.
        /// Пример набора символов, содержащихся в штрихкоде PDF-417 имеет вид (без кавычек):
        /// "19N00000XOPN13MM66T0HVF311210120003676539219152175585956302712947109".
        /// Символы представлены цифрами либо строчными латинскими буквами.
        /// Длина набора символов – 68 единиц.
        /// </remarks>
        /// </summary>
        public const int EGAISCodeLength = 68;
        #endregion Внешние константы класса.

        #region Защищенные объекты класса.
        /// <summary>
        /// Тип движения товара.
        /// Только чтение.
        /// </summary>
        protected readonly MovementType movementType;
        /// <summary>
        /// Тело документа алкогольной продукции.
        /// Только чтение.
        /// </summary>
        protected readonly string xmlProductionBody;
        /// <summary>
        /// Код продукта.
        /// </summary>
        [OptionalField]
        protected string productVCode;
        #endregion Защищенные объекты класса.

        #region Внешние объекты класса.

        [DisplayName("Идентификатор позиции"), ReadOnly(true), Browsable(true)]
        public int Identity { get; set; }

        [DisplayName("Наименование"), ReadOnly(true), Browsable(true)]
        public string Title { get; set; }

        [DisplayName("Код алкогольной продукции"), ReadOnly(true), Browsable(true)]
        public string AlcoCode { get; set; }

        [DisplayName("Код продукта"), ReadOnly(true), Browsable(true)]
        public string ProductVCode
        {
            get
            {
                if (string.IsNullOrWhiteSpace(productVCode))
                {
                    if (string.IsNullOrWhiteSpace(xmlProductionBody)) return string.Empty;

                    XmlDocument xml = new XmlDocument();
                    xml.LoadXml(xmlProductionBody);

                    if (xml["rap:Product"] == null) return string.Empty;

                    productVCode = ADocument.GetNodeValue("pref:ProductVCode", xml["rap:Product"]);
                }

                return productVCode;
            }
            set
            {
                productVCode = value;
            }
        }

        [DisplayName("Ёмкость"), ReadOnly(true), Browsable(true)]
        public string Capacity { get; set; }

        [DisplayName("Крепость"), ReadOnly(true), Browsable(true)]
        public string Volume { get; set; }

        [DisplayName("Количество"), ReadOnly(false), Browsable(true)]
        public decimal Quantity { get; set; }

        [DisplayName("Справка 'А' (идентификатор)"), ReadOnly(false), Browsable(true)]
        public string FormARegId { get; set; }

        [DisplayName("Справка 'Б' (идентификатор)"), ReadOnly(false), Browsable(true)]
        public string FormBRegId { get; set; }

        [DisplayName("Производитель"), ReadOnly(true), Browsable(true)]
        public string ProducerTitle { get; set; }

        [DisplayName("Статус"), ReadOnly(true), Browsable(true)]
        public int Status
        {
            get { return checkWithSuppress(); }
        }

        [DisplayName("Тело документа алкогольной продукции."), ReadOnly(true), Browsable(false)]
        public string XmlProductionBody
        {
            get { return xmlProductionBody; }
        }

        [DisplayName("Тип движения товара."), ReadOnly(true), Browsable(false)]
        public MovementType MovementType
        {
            get { return movementType; }
        }

        [DisplayName("Дата розлива / Дата справки к ГТД"), ReadOnly(false), Browsable(true)]
        public DateTime BottlingDate { get; set; }

        [DisplayName("Дата розлива / Дата справки к ГТД"), ReadOnly(true), Browsable(false)]
        public string BottlingDateShort
        {
            get { return BottlingDate.ToString("yyyy-MM-dd");  }
        }

        [DisplayName("Номер ТТН / Номер ГТД"), ReadOnly(false), Browsable(true)]
        public string TTNNumber { get; set; }

        [DisplayName("Дата ТТН / Дата справки к ГТД"), ReadOnly(false), Browsable(true)]
        public DateTime TTNDate { get; set; }

        [DisplayName("Дата ТТН / Дата справки к ГТД"), ReadOnly(true), Browsable(false)]
        public string TTNDateShort
        {
            get { return TTNDate.ToString("yyyy-MM-dd"); }
        }

        [DisplayName("Номер фиксации в ЕГАИС"), ReadOnly(false), Browsable(true)]
        public string EGAISNumber { get; set; }

        [DisplayName("Дата фиксации в ЕГАИС / Дата справки к ГТД"), ReadOnly(false), Browsable(true)]
        public DateTime EGAISDate { get; set; }

        [DisplayName("Дата фиксации в ЕГАИС / Дата справки к ГТД"), ReadOnly(true), Browsable(false)]
        public string EGAISDateShort
        {
            get { return EGAISDate.ToString("yyyy-MM-dd"); }
        }

        [DisplayName("Использовать помарочное сканирование"), ReadOnly(true), Browsable(false)]
        public bool UseScan { get; set; }

        [DisplayName("Код PDF-417"), ReadOnly(true), Browsable(true)]
        public List<string> PDF417Codes { get; protected set; }

        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected MovePosition()
        {
            movementType = MovementType.Unknown;
            
            xmlProductionBody = string.Empty;

            Identity = 0;
            Title = string.Empty;
            AlcoCode = string.Empty;
            ProductVCode = string.Empty;
            Capacity = string.Empty;
            Volume = string.Empty;
            Quantity = 1.0m;
            FormARegId = string.Empty;
            FormBRegId = string.Empty;
            ProducerTitle = string.Empty;
            BottlingDate = new DateTime(1990, 1, 1);
            TTNNumber = string.Empty;
            TTNDate = new DateTime(1990, 1, 1);
            EGAISNumber = string.Empty;
            EGAISDate = new DateTime(1990, 1, 1);

            UseScan = false;
            PDF417Codes = new List<string>();
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="movementType">Тип документа движения товара.</param>
        /// <param name="production">Алкогольная продукция.</param>
        public MovePosition(MovementType movementType, Production production) : this()
        {
            this.movementType = movementType;
            xmlProductionBody = production.XmlBody;

            Title = string.IsNullOrEmpty(production.ShortName) ? production.FullName : production.ShortName;
            AlcoCode = production.AlcCode;
            ProductVCode = production.ProductVCode;
            Capacity = production.Capacity;
            Volume = production.AlcVolume;
            ProducerTitle = production.ProducerName;
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="movementType">Тип документа движения товара.</param>
        /// <param name="rests">Остаток.</param>
        public MovePosition(MovementType movementType, RestsPosition rests) : this()
        {
            this.movementType = movementType;
            xmlProductionBody = rests.XmlProductionBody;

            Title = rests.Title;
            AlcoCode = rests.AlcoCode;
            ProductVCode = rests.ProductVCode;
            Capacity = rests.Capacity;
            Volume = rests.Volume;
            FormARegId = rests.FormARegId;
            FormBRegId = rests.FormBRegId;
            ProducerTitle = rests.Producer;
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="movementType">Тип документа движения товара.</param>
        /// <param name="rests">Остаток.</param>
        /// <param name="quantity">Явное указание количества товара.</param>
        public MovePosition(MovementType movementType, RestsPosition rests, decimal quantity) : this(movementType, rests)
        {
            Quantity = quantity;
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="position">Позиция.</param>
        public MovePosition(MovePosition position) : this()
        {
            movementType = position.movementType;
            xmlProductionBody = position.XmlProductionBody;

            Identity = position.Identity;
            Title = position.Title;
            AlcoCode = position.AlcoCode;
            ProductVCode = position.ProductVCode;
            Capacity = position.Capacity;
            Volume = position.Volume;
            Quantity = position.Quantity;
            FormARegId = position.FormARegId;
            FormBRegId = position.FormBRegId;
            ProducerTitle = position.ProducerTitle;
            BottlingDate = position.BottlingDate;
            TTNNumber = position.TTNNumber;
            TTNDate = position.TTNDate;
            EGAISNumber = position.EGAISNumber;
            EGAISDate = position.EGAISDate;

            UseScan = position.UseScan;

            PDF417Codes.Clear();
            if (position.PDF417Codes != null)
            {
                foreach (string code in position.PDF417Codes)
                {
                    PDF417Codes.Add(code);
                }
            }
        }
        #endregion Конструкторы класса.

        #region Внешние методы класса.
        /// <summary>
        /// Проверить позицию накладной.
        /// </summary>
        public void Check()
        {
            check();
        }
        /// <summary>
        /// Клонировать позицию.
        /// </summary>
        /// <returns>Новая позиция.</returns>
        public MovePosition Clone()
        {
            return clone();
        }
        #endregion Внешние методы класса.

        #region Защищенные методы класса.
        /// <summary>
        /// Проверка с подавлением исключений.
        /// </summary>
        /// <returns>Результат.</returns>
        protected virtual int checkWithSuppress()
        {
            try
            {
                check();

                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// Проверить позицию накладной.
        /// </summary>
        protected virtual void check()
        {
            if (movementType == MovementType.Unknown) throw new Exception("Позиция имеет неизвестный тип документа движения товара.");

            if (Identity < 1) throw new Exception("Идентификатор позиции не может быть быть нулевым или отрицательным.");

            if (Quantity <= 0.0m) throw new Exception(string.Format("Величина 'Количество' в позиции не может быть нулевой или отрицательной (позиция №{0}).", Identity));
            if (Quantity > 10000.0m) throw new Exception(string.Format("Величина 'Количество' в позиции превышает разумное значение (позиция №{0}).", Identity));

            if (string.IsNullOrWhiteSpace(AlcoCode)) throw new Exception(string.Format("Код алкогольной продукции не может быть пустым (позиция №{0}).", Identity));

            switch (MovementType)
            {
                case MovementType.ActChargeOn:
                {
                    if (string.IsNullOrWhiteSpace(xmlProductionBody)) throw new Exception(string.Format("Выбранный документ 'Алкогольная продукция' не может быть пустым (позиция №{0}).", Identity));

                    if (UseScan)
                    {
                        if (BottlingDate >= new DateTime(2012, 5, 22))
                        {
                            if (string.IsNullOrWhiteSpace(EGAISNumber)) throw new Exception(string.Format("Для продукции, выпущенной (импортированной) после 22-го мая 2012 г., " +
                                                                                                          "требуется указывать номер подтверждения фиксации в ЕГАИС.\r\n" +
                                                                                                          "В данном акте такое требование не выполнено (позиция №{0}).", Identity));
                        }

                        if (PDF417Codes == null) throw new Exception(string.Format("Не создан список отсканированных двумерных штрих-кодов (позиция №{0}).", Identity));
                        if (Quantity != PDF417Codes.Count) throw new Exception(string.Format("Величина 'Количество' не соответствует количеству отсканированных двумерных штрих-кодов (позиция №{0}).", Identity));
                        if (PDF417Codes.Distinct().ToList().Count < PDF417Codes.Count) throw new Exception(string.Format("Обнаружены повторяющиеся отсканированные двумерные штрих-коды (позиция №{0}).", Identity));
                        if (PDF417Codes.Any(code => !IsPDF417CodeValid(code))) throw new Exception(string.Format("Один из отсканированных двумерных штрих-кодов не соответствует требованиям ЕГАИС (позиция №{0}).", Identity));
                    }

                    break;
                }

                case MovementType.ActChargeOff:
                case MovementType.TransferToShop:
                case MovementType.TransferFromShop:
                {
                    if (FormBRegId.Length > 50) throw new Exception(string.Format("Длина идентификатора справки 'B' превышает максимально допустимый размер (позиция №{0}).", Identity));
                    if (string.IsNullOrWhiteSpace(FormBRegId)) throw new Exception(string.Format("Идентификатор справки 'Б' не может быть пустым.\r\n" +
                                                                                                 "В позиции документа необходимо указывать идентификатор записи предыдущей " +
                                                                                                 "отгрузки (по которой продукция поступила на склад) (позиция №{0}).", Identity));
                    break;
                }

                case MovementType.ActChargeOnShop:
                case MovementType.ActChargeOffShop:
                {
                    if (string.IsNullOrWhiteSpace(xmlProductionBody)) throw new Exception(string.Format("Выбранный документ 'Алкогольная продукция' не может быть пустым (позиция №{0}).", Identity));

                    break;
                }

                default: { throw new Exception(string.Format("Работа с типом '{0}' не поддерживается в данной версии.", movementType)); }
            }
        }
        /// <summary>
        /// Клонировать позицию.
        /// </summary>
        /// <returns>Новая позиция.</returns>
        protected virtual MovePosition clone()
        {
            return new MovePosition(this);
        }
        #endregion Защищенные методы класса.

        #region Внешние статические методы класса.
        /// <summary>
        /// Проверка кода PDF-417 на соответствие требований ФСРАР.
        /// </summary>
        /// <param name="code">Код.</param>
        /// <returns>Результат проверки.</returns>
        public static bool IsPDF417CodeValid(string code)
        {
            return ((!string.IsNullOrWhiteSpace(code))
                    && (code.Length == EGAISCodeLength));
        }
        #endregion Внешние статические методы класса.
    }
}
