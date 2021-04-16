using System;
using System.Xml;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Остатки на дату.
    /// <remarks>Промежуточный класс, ибо дизайнер форм DevExpress не очень корректно
    /// работает с абстрактными классами в качестве источника данных.</remarks>
    /// </summary>
    [DisplayName("Остатки на дату"), Serializable]
    public class BaseRests : ARests
    {
        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected BaseRests()
        {
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="node">XML-нода для создания.</param>
        protected BaseRests(XmlNode node) : base(node)
        {
        }
        #endregion Конструкторы класса.
    }
}
