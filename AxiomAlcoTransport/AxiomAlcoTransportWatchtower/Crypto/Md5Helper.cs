using System.Text;
using System.Security.Cryptography;

namespace Axiom.AlcoTransport.Watchtower.Crypto
{
    /// <summary>
    /// Вспомогательный функционал для "MD5".
    /// </summary>
    public static class Md5Helper
    {
        /// <summary>
        /// Получить MD5-хэш.
        /// </summary>
        /// <param name="input">Строка.</param>
        /// <returns>Хэш.</returns>
        public static string GetHash(string input)
        {
            MD5 md5 = MD5.Create();

            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder builder = new StringBuilder();

            foreach (byte b in data)
            {
                builder.Append(b.ToString("x2"));
            }

            return builder.ToString();
        }

    }
}
