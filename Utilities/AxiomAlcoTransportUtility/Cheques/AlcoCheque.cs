using System;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace Axiom.AlcoTransport.Utility.Cheques
{
    /// <summary>
    /// Алкогольный чек.
    /// </summary>
    public class AlcoCheque : ACheque
    {
        #region Защищённые объекты класса.
        /// <summary>
        /// Список позиций чека.
        /// Только чтение.
        /// </summary>
        protected readonly IList<AlcoPosition> positions;
        #endregion Защищённые объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Список позиций чека.
        /// Только чтение.
        /// </summary>
        public IList<AlcoPosition> Positions
        {
            get { return positions; }
        }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public AlcoCheque()
        {
            positions = new List<AlcoPosition>();
        }
        #endregion Конструкторы класса.

        #region Переопределение методов базового класса.
        /// <summary>
        /// Сформировать xml-документ по чеку.
        /// </summary>
        /// <returns>Xml-документ.</returns>
        protected override XmlDocument buildXmlDocument()
        {
            XmlDocument xmlCheque = new XmlDocument();

            XmlNode declaration = xmlCheque.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlCheque.AppendChild(declaration);

            XmlNode chequeNode = xmlCheque.CreateElement("Cheque");
            xmlCheque.AppendChild(chequeNode);

            validateChequeData();

            addAttribute(xmlCheque, chequeNode, "name", Title);
            addAttribute(xmlCheque, chequeNode, "address", Address);
            addAttribute(xmlCheque, chequeNode, "inn", Inn);
            addAttribute(xmlCheque, chequeNode, "kpp", Kpp);
            addAttribute(xmlCheque, chequeNode, "kassa", Kassa);
            addAttribute(xmlCheque, chequeNode, "shift", Shift.ToString("D"));
            addAttribute(xmlCheque, chequeNode, "number", Number.ToString("D"));
            addAttribute(xmlCheque, chequeNode, "datetime", ProcessDateTimeFormatted);

            if (positions.Count == 0) throw new Exception("В заявленном алкогольном чеке не обнаружен алкогольный товар.");

            int count = 0;

            foreach (AlcoPosition position in positions)
            {
                if (position == null) continue;

                XmlNode bottleNode = xmlCheque.CreateElement("Bottle");

                decimal direct = Direct;

                if (position.Price <= 0m) throw new Exception("Указана некорректная цена товара. Цена не может быть нулевой или отрицательной.");
                if (position.Volume <= 0m) throw new Exception("Указана некорректный объём для алкогольного товара. Объём не может быть нулевым или отрицательным.");
                if (string.IsNullOrWhiteSpace(position.Ean)) throw new Exception("Указан некорректный код EAN13 для алкогольного товара.");
                if ((string.IsNullOrWhiteSpace(position.Barcode)) || (position.Barcode.Length != EGAISCodeLength)) throw new Exception("Указан некорректный код PDF-417 для алкогольного товара.");

                addAttribute(xmlCheque, bottleNode, "price", (direct * position.Price).ToString("F2").Replace(decimalSeparator, "."));
                addAttribute(xmlCheque, bottleNode, "volume", position.Volume.ToString("F4").Replace(decimalSeparator, "."));
                addAttribute(xmlCheque, bottleNode, "barcode", position.Barcode);
                addAttribute(xmlCheque, bottleNode, "ean", position.Ean);

                chequeNode.AppendChild(bottleNode);

                ++count;
            }

            if (count == 0) throw new Exception("В заявленном алкогольном чеке не обнаружен алкогольный товар.");

            return xmlCheque;
        }
        /// <summary>
        /// Получить общую сумму чека.
        /// </summary>
        /// <returns>Общая сумма чека.</returns>
        protected override decimal getTotalSum()
        {
            return positions.Sum(position => position.Price);
        }
        /// <summary>
        /// Получить количество позиций в чеке.
        /// </summary>
        /// <returns>Количество позиций в чеке</returns>
        protected override int getCountPositions()
        {
            return positions.Count;
        }
        #endregion Переопределение методов базового класса.
    }
}
