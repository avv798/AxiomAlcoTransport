using System;
using System.ComponentModel;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Движение товара.
    /// <remarks>Промежуточный класс, ибо дизайнер форм DevExpress не очень корректно
    /// работает с абстрактными классами в качестве источника данных.</remarks>
    /// </summary>
    [DisplayName("Движение товара"), Serializable]
    public class Movement : AMovement
    {
    }
}
