using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace AxiomAuxiliary.Network
{
    /// <summary>
    /// Функционал получения информации о локальной сети.
    /// Статический класс.
    /// </summary>
    public static class NetworkInformation
    {
        #region Внутренние константы класса.
        /// <summary>
        /// Неизвестное наименование.
        /// </summary>
        private const string unknownName = "unknown";
        #endregion Внутренние константы класса.

        #region Внешние статические методы класса.

        /// <summary>
        /// Получить имя хоста.
        /// </summary>
        /// <returns>Имя хоста.</returns>
        public static string GetHostName()
        {
            try
            {
                if (!IsNetworkAvailable()) return unknownName;

                return Dns.GetHostName();
            }
            catch (Exception exception)
            {
                Trace.TraceError("Exception. Method 'AxiomAuxiliary.Network.NetworkInformation.GetHostName'.", exception);

                return unknownName;
            }
        }

        /// <summary>
        /// Получить локальный ip-адрес.
        /// </summary>
        /// <returns>Ip-адрес.</returns>
        public static string GetLocalIPAddress()
        {
            try
            {
                if (!IsNetworkAvailable()) return unknownName;

                IPHostEntry entry = Dns.GetHostEntry(GetHostName());

                foreach (IPAddress address in entry.AddressList.Where(address => (address.AddressFamily == AddressFamily.InterNetwork)))
                {
                    return address.ToString();
                }

                return unknownName;
            }
            catch (Exception exception)
            {
                Trace.TraceError("Exception. Method 'AxiomAuxiliary.Network.NetworkInformation.GetLocalIPAddress'.", exception);

                return unknownName;
            }
        }

        /// <summary>
        /// Проверить, доступна ли сеть.
        /// </summary>
        /// <returns>Признак доступности.</returns>
        public static bool IsNetworkAvailable()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }

        #endregion Внешние статические методы класса.
    }
}
