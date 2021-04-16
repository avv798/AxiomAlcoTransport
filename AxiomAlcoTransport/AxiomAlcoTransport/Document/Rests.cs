using System;
using System.ComponentModel;
using System.Xml;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Остатки на дату.
    /// </summary>
    [DisplayName("Справочник остатков на складе организации"), Serializable]
    public class Rests : BaseRests
    {
        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected Rests()
        {
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="node">XML-нода для создания.</param>
        /// <param name="version">Версия документа.</param>
        public Rests(XmlNode node, int version) : base(node)
        {
            VersionEgais = version;
        }
        #endregion Конструкторы класса.

        #region Внешние статические методы класса.
        /// <summary>
        /// Наименование документа.
        /// </summary>
        public static string NativeName
        {
            get { return "Справочник остатков на складе организации"; }
        }
        #endregion Внешние статические методы класса.

        #region Переопределение базовых методов.
        /// <summary>
        /// Получение описания объекта.
        /// </summary>
        /// <returns>Описание.</returns>
        protected override string GetDescription()
        {
            return string.Format("{0} {1}", base.GetDescription(), "(склад организации)");
        }
        #endregion Переопределение базовых методов.
    }
}
