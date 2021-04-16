using System;
using System.ComponentModel;
using System.Xml;

namespace Axiom.AlcoTransport.Document
{
    /// <summary>
    /// Остатки на дату в разрезе штрихкодов
    /// </summary>
    [DisplayName(@"Справочник остатков по штрихкодам"), Serializable]
    public class BCodeRests : BaseRests
    {
        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected BCodeRests()
        {
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="node">XML-нода для создания.</param>
        /// <param name="version">Версия документа.</param>
        public BCodeRests(XmlNode node, int version) : base(node)
        {
            VersionEgais = version;
        }
        #endregion Конструкторы класса.

        #region Внешние статические методы класса.
        /// <summary>
        /// Наименование документа.
        /// </summary>
        public static string NativeName => "Справочник остатков в разрезе штрихкодов";

        #endregion Внешние статические методы класса.

        #region Переопределение базовых методов.
        /// <summary>
        /// Получение описания объекта.
        /// </summary>
        /// <returns>Описание.</returns>
        protected override string GetDescription()
        {
            return $"{base.GetDescription()} (разрез штрихкодов)";
        }
        #endregion Переопределение базовых методов.
    }
}