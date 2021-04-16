using System;
using System.Globalization;
using System.IO;
using System.ComponentModel;
using System.Xml;

namespace Axiom.AlcoTransport.Utility.Cheques
{
    /// <summary>
    ///  Функционал представления чека, загруженного из файла.
    /// </summary>
    public class FileCheque
    {
        #region Защищённые объекты класса.
        /// <summary>
        /// Наименование файла.
        /// Только чтение.
        /// </summary>
        protected readonly string filename;
        /// <summary>
        /// Статус файла.
        /// </summary>
        protected string status;
        /// <summary>
        /// Тип файла.
        /// Только чтение.
        /// </summary>
        protected string typeCheque;
        /// <summary>
        /// Алкогольный чек.
        /// Только чтение.
        /// </summary>
        protected AlcoCheque alcoCheque;
        /// <summary>
        /// Пивной чек.
        /// Только чтение.
        /// </summary>
        protected BeerCheque beerCheque;
        #endregion Защищённые объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Наименование файла.
        /// Только чтение.
        /// </summary>
        [DisplayName("Наименование файла"), ReadOnly(true), Browsable(true)]
        public string Filename
        {
            get { return filename;  }
        }
        /// <summary>
        /// Статус файла.
        /// Только чтение.
        /// </summary>
        [DisplayName("Статус файла"), ReadOnly(true), Browsable(false)]
        public string Status
        {
            get { return status; }
        }
        /// <summary>
        /// Тип файла.
        /// Только чтение.
        /// </summary>
        [DisplayName("Тип файла"), ReadOnly(true), Browsable(false)]
        public string TypeCheque
        {
            get { return typeCheque; }
        }
        /// <summary>
        /// Статус файла.
        /// Только чтение.
        /// </summary>
        [DisplayName("Статус файла"), ReadOnly(true), Browsable(true)]
        public string StatusDescription
        {
            get
            {
                switch (status.ToLower())
                {
                    case "new": return "новый, готов к обработке";
                    case "empty": return "пустой, отсутствуют товарные позиции";
                    case "processed": return "файл обработан";
                    case "load_error": return "файл загружен с ошибкой";
                    case "processed_error": return "файл обработан с ошибкой";

                    default: return "неизвестный";
                }
            }
        }
        /// <summary>
        /// Тип файла.
        /// Только чтение.
        /// </summary>
        [DisplayName("Тип файла"), ReadOnly(true), Browsable(true)]
        public string TypeChequeDescription
        {
            get
            {
                switch (typeCheque.ToLower())
                {
                    case "alco": return "алкогольный чек";
                    case "beer": return "пивной чек";

                    default: return "неизвестный";
                }
            }
        }
        /// <summary>
        /// Комментарий к файлу.
        /// Только чтение.
        /// </summary>
        [DisplayName("Комментарий к файлу"), ReadOnly(true), Browsable(true)]
        public string Comment { get; set; }
        /// <summary>
        /// Пивной чек.
        /// Только чтение.
        /// </summary>
        [Browsable(false)]
        public BeerCheque BeerCheque
        {
            get { return beerCheque; }
        }
        /// <summary>
        /// Алкогольный чек.
        /// Только чтение.
        /// </summary>
        [Browsable(false)]
        public AlcoCheque AlcoCheque
        {
            get { return alcoCheque; }
        }
        /// <summary>
        /// Короткое имя файла для идентификации.
        /// </summary>
        [Browsable(false)]
        public string ShortFilename
        {
            get
            {
                string name = Path.GetFileNameWithoutExtension(Filename);

                if (string.IsNullOrWhiteSpace(name)) return string.Empty;

                return name.ToLower().Replace(".", "_");
            }
        }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        protected FileCheque()
        {
            filename = string.Empty;

            status = "unknown";
            typeCheque = "unknown";
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="filename">Наименование файла.</param>
        public FileCheque(string filename) : this()
        {
            this.filename = filename;

            loadFile();
        }
        #endregion Конструкторы класса.

        #region Защищённые методы класса.
        /// <summary>
        /// Загрузить файл.
        /// </summary>
        protected void loadFile()
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка загрузки файла-чека '{0}'...", filename));

                if (File.Exists(filename))
                {
                    XmlDocument xml = new XmlDocument();
                    xml.Load(filename);

                    if (xml.DocumentElement == null) throw new Exception("Неизвестный формат чека (отсутствует корневой элемент XML).");

                    int direct = 0;

                    foreach (XmlNode node in xml.DocumentElement.ChildNodes)
                    {
                        #region Alco...

                        if (node.Name.ToLower() == "bottle")
                        {
                            if (typeCheque == "beer") throw new Exception("Неизвестный формат чека (в алкогольном чеке присутствует пивная продукция).");

                            typeCheque = "alco";

                            if (alcoCheque == null) alcoCheque = new AlcoCheque();

                            decimal price = convertToDecimal(GetAttributeValue("price", node));

                            alcoCheque.Positions.Add(new AlcoPosition
                                                         {
                                                             Price = Math.Abs(price),
                                                             Volume = convertToDecimal(GetAttributeValue("volume", node)),
                                                             Barcode = GetAttributeValue("barcode", node),
                                                             Ean = GetAttributeValue("ean", node)
                                                         });

                            if ((price > 0) && (direct < 0)) throw new Exception("Ошибка формата товарной позиции (для возвратного чека указана положительная цена).");
                            if ((price < 0) && (direct > 0)) throw new Exception("Ошибка формата товарной позиции (для чека продажи указана отрицательная цена).");

                            direct = (price > 0) ? 1 : -1;

                            continue;
                        }

                        #endregion Alco...

                        #region Beer...

                        if (node.Name.ToLower() == "nopdf")
                        {
                            if (typeCheque == "alco") throw new Exception("Неизвестный формат чека (в пивном чеке присутствует алкогольная продукция).");

                            typeCheque = "beer";

                            if (beerCheque == null) beerCheque = new BeerCheque();

                            decimal price = convertToDecimal(GetAttributeValue("price", node));

                            beerCheque.Positions.Add(new BeerPosition
                                                         {
                                                             Price = Math.Abs(price),
                                                             Count = convertToInt(GetAttributeValue("count", node)),
                                                             AlcoStrength = convertToDecimal(GetAttributeValue("alc", node)),
                                                             Volume = convertToDecimal(GetAttributeValue("volume", node)),
                                                             Title = GetAttributeValue("bname", node),
                                                             Code = GetAttributeValue("code", node),
                                                             Ean = GetAttributeValue("ean", node)
                                                         });

                            if ((price > 0) && (direct < 0)) throw new Exception("Ошибка формата товарной позиции (для возвратного чека указана положительная цена).");
                            if ((price < 0) && (direct > 0)) throw new Exception("Ошибка формата товарной позиции (для чека продажи указана отрицательная цена).");

                            direct = (price > 0) ? 1 : -1;
                            
                            continue;
                        }

                        #endregion Beer...

                        throw new Exception("Неизвестный формат чека (неизвестная нода в составе чека).");
                    }

                    ACheque cheque = alcoCheque ?? (beerCheque as ACheque);

                    if (cheque == null) throw new Exception("Неизвестный формат чека (ошибка загрузки позиций чека).");

                    if (cheque.CountPositions == 0)
                    {
                        status = "empty";
                    }
                    else
                    {
                        cheque.Direct = direct;
                        cheque.Inn = GetAttributeValue("inn", xml.DocumentElement);
                        cheque.Kpp = GetAttributeValue("kpp", xml.DocumentElement);
                        cheque.Address = GetAttributeValue("address", xml.DocumentElement);
                        cheque.Title = GetAttributeValue("name", xml.DocumentElement);
                        cheque.Kassa = GetAttributeValue("kassa", xml.DocumentElement);
                        cheque.Shift = convertToInt(GetAttributeValue("shift", xml.DocumentElement));
                        cheque.Number = convertToInt(GetAttributeValue("number", xml.DocumentElement));
                        cheque.ProcessDateTime = DateTime.Now;

                        status = "new";
                    }
                }
                else
                {
                    status = "unknown";

                    Program.Logger.Warn(this, string.Format("... файл-чек '{0}' не найден.", filename));
                }

                Program.Logger.Info(this, string.Format("... файл-чек '{0}' с типом '{1}' добавлен в список.", filename, typeCheque));
            }
            catch (Exception exception)
            {
                status = "load_error";
                Comment = exception.Message;

                Program.Logger.Error(string.Format("Во время загрузки файла-чека '{0}' произошла ошибка.", filename), exception);
            }
        }
        #endregion Защищённые методы класса.
        
        #region Внешние методы класса.
        /// <summary>
        /// Установить статус файла.
        /// </summary>
        /// <param name="newStatus">Новый статус.</param>
        public void SetStatus(string newStatus)
        {
            status = newStatus;
        }
        #endregion Внешние методы класса.

        #region Внешние статические методы класса.
        /// <summary>
        /// Получить значение атрибута.
        /// Если атрибут не будет найден, будет возвращено значение "string.Empty".
        /// </summary>
        /// <param name="attributeName">Наименование атрибута.</param>
        /// <param name="node">Нода.</param>
        /// <returns>Значение атрибута.</returns>
        public static string GetAttributeValue(string attributeName, XmlNode node)
        {
            if ((node == null)
                || (node.Attributes == null)
                || (node.Attributes.Count == 0))
            {
                return string.Empty;
            }

            try
            {
                return node.Attributes[attributeName].InnerText;
            }
            catch (Exception exception)
            {
                Program.Logger.Error(string.Format("Во время поиска атрибута '{0}' произошла ошибка.", attributeName), exception);

                return string.Empty;
            }
        }
        /// <summary>
        /// Преобразовать строку в число.
        /// </summary>
        /// <param name="str">Строка.</param>
        /// <returns>Результат.</returns>
        protected static decimal convertToDecimal(string str)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(str)) throw new Exception("Строка для преобразования пустая.");

                return Convert.ToDecimal(str.Trim().Replace(" ", "").Replace(",", "."), new NumberFormatInfo { NumberDecimalSeparator = ".", NumberGroupSeparator = string.Empty });
            }
            catch (Exception exception)
            {
                Program.Logger.Error(string.Format("При преобразовании строки '{0}' в число произошла ошибка '{1}'.", str, exception));

                throw;
            }
        }
        /// <summary>
        /// Преобразовать строку в число.
        /// </summary>
        /// <param name="str">Строка.</param>
        /// <returns>Результат.</returns>
        protected static int convertToInt(string str)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(str)) throw new Exception("Строка для преобразования пустая.");

                return Convert.ToInt32(str.Trim().Replace(" ", ""));
            }
            catch (Exception exception)
            {
                Program.Logger.Error(string.Format("При преобразовании строки '{0}' в целое число произошла ошибка '{1}'.", str, exception));

                throw;
            }
        }
        #endregion Внешние статические методы класса.
    }
}
