using System;
using System.Linq;
using System.Xml;
using System.Collections.Generic;

namespace Axiom.AlcoTransport.Utility.Cheques
{
    /// <summary>
    /// Пивной чек.
    /// </summary>
    public class BeerCheque : ACheque
    {
        #region Защищённые объекты класса.
        /// <summary>
        /// Список позиций чека.
        /// Только чтение.
        /// </summary>
        protected readonly IList<BeerPosition> positions;
        #endregion Защищённые объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Список позиций чека.
        /// Только чтение.
        /// </summary>
        public IList<BeerPosition> Positions
        {
            get { return positions; }
        }
        #endregion Внешние объекты класса.

        #region Конструкторы класса.
        /// <summary>
        /// Конструктор класса "по умолчанию".
        /// </summary>
        public BeerCheque()
        {
            positions = new List<BeerPosition>();
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

            if (positions.Count == 0) throw new Exception("В заявленном пивном чеке не обнаружен пивной товар.");

            int count = 0;

            foreach (BeerPosition position in positions)
            {
                if (position == null) continue;

                XmlNode bottleNode = xmlCheque.CreateElement("nopdf");

                decimal direct = Direct;

                if (string.IsNullOrWhiteSpace(position.Title)) throw new Exception("Указан некорректное наименование для пивного товара.");
                if (string.IsNullOrWhiteSpace(position.Code)) throw new Exception("У пивного товара не указан обязательный 'Код вида продукции'.");
                if (string.IsNullOrWhiteSpace(position.Ean)) throw new Exception("Указан некорректный код EAN13 для пивного товара.");
                if (position.Price <= 0) throw new Exception("Указана некорректная цена товара. Цена не может быть нулевой или отрицательной.");
                if (position.Volume <= 0) throw new Exception("Указана некорректный объём для пивного товара. Объём не может быть нулевым или отрицательным.");
                if (position.AlcoStrength <= 0) throw new Exception("Указана некорректная крепость для пивного товара. Крепость не может быть нулевой или отрицательной.");
                if ((position.Count < 1) || (position.Count > 99)) throw new Exception("Некорректное значение поля 'количество' в товаре.");

                addAttribute(xmlCheque, bottleNode, "bname", position.Title);
                addAttribute(xmlCheque, bottleNode, "code", position.Code);
                addAttribute(xmlCheque, bottleNode, "ean", position.Ean);
                addAttribute(xmlCheque, bottleNode, "price", (direct * position.Price).ToString("F2").Replace(decimalSeparator, "."));
                addAttribute(xmlCheque, bottleNode, "volume", position.Volume.ToString("F4").Replace(decimalSeparator, "."));
                addAttribute(xmlCheque, bottleNode, "alc", position.AlcoStrength.ToString("F4").Replace(decimalSeparator, "."));
                addAttribute(xmlCheque, bottleNode, "count", position.Count.ToString("D"));

                chequeNode.AppendChild(bottleNode);

                ++count;
            }

            if (count == 0) throw new Exception("В заявленном пивном чеке не обнаружен пивной товар.");

            return xmlCheque;
        }
        /// <summary>
        /// Получить общую сумму чека.
        /// </summary>
        /// <returns>Общая сумма чека.</returns>
        protected override decimal getTotalSum()
        {
            return positions.Sum(position => position.TotalPrice);
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
