using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Axiom.AlcoTransport.Watchtower
{
    /// <summary>
    /// Функционал простейшего шифрования.
    /// Статический класс.
    /// </summary>
    public static class AesCrypto
    {
        #region Внутренние статические объекты класса.
        /// <summary>
        /// Разделяемая строка.
        /// </summary>
        private const string shared = "Axiom.AlcoTransport.Watchtower";
        /// <summary>
        /// Соль.
        /// </summary>
        private static readonly byte[] salt = Encoding.ASCII.GetBytes("Axioma. In Vino Veritas");
        #endregion Внутренние статические объекты класса.

        #region Внутренние статические методы класса.
        /// <summary>
        /// Чтение потока.
        /// </summary>
        /// <param name="stream">Поток.</param>
        /// <returns>Массив.</returns>
        private static byte[] ReadByteArray(Stream stream)
        {
            byte[] rawLength = new byte[sizeof(int)];

            if (stream.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
            {
                throw new SystemException("Crypto. Stream did not contain properly formatted byte array.");
            }

            byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];

            if (stream.Read(buffer, 0, buffer.Length) != buffer.Length)
            {
                throw new SystemException("Crypto. Did not read byte array properly.");
            }

            return buffer;
        }
        #endregion Внутренние статические методы класса.

        #region Внешние статические методы класса.
        /// <summary>
        /// Шифрование строки с использованием AES.
        /// </summary>
        /// <param name="plainText">Строка для шифрования.</param>
        /// <returns>Результат.</returns>
        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) throw new ArgumentNullException("plainText");

            string outString;
            RijndaelManaged aesAlg = null;

            try
            {
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(shared, salt);
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);

                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }

                    outString = Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
            finally
            {
                if (aesAlg != null) aesAlg.Clear();
            }

            return outString;
        }
        /// <summary>
        /// Дешифрование строки с использованием AES.
        /// </summary>
        /// <param name="cipherText">Строка для шифрования.</param>
        /// <returns>Результат.</returns>
        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) throw new ArgumentNullException("cipherText");

            string plainText;
            RijndaelManaged aesAlg = null;

            try
            {
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(shared, salt);
                byte[] bytes = Convert.FromBase64String(cipherText);

                using (MemoryStream msDecrypt = new MemoryStream(bytes))
                {
                    aesAlg = new RijndaelManaged();
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                    aesAlg.IV = ReadByteArray(msDecrypt);
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            plainText = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            finally
            {
                if (aesAlg != null) aesAlg.Clear();
            }

            return plainText;
        }
        #endregion Внешние статические методы класса.
    }
}
