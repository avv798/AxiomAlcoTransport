using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Net;
using System.Text;
using System.Collections.Specialized;

namespace AxiomAuxiliary.Transports
{
    /// <summary>
    /// Класс, реализующий функционал простейшего HTTP-транспорта.
    /// Статический класс.
    /// <remarks>Класс узко специализирован. Предназначен для обмена данными с УТМ (универсальный транспортный модуль) ЕГАИС.</remarks>
    /// </summary>
    public static class HttpTransport
    {
        #region Внутренние статические объекты класса.
        /// <summary>
        /// Имя пользовательского агента для запросов.
        /// Только чтение.
        /// </summary>
        private static string userAgent
        {
            get
            {
                return string.Format("{0}: {1} ({2})", Application.ProductName, Process.GetCurrentProcess().ProcessName, Application.ProductVersion);
            }
        }
        #endregion Внутренние статические объекты класса.

        #region Внешние статические методы класса.
        /// <summary>
        /// Отправить запрос (метод "GET") к серверу и получить ответ.
        /// </summary>
        /// <param name="url">Адрес сервера.</param>
        /// <param name="timeout">Тайм-аут выполнения операции (в миллисекундах). Значение "по умолчанию" - 2500 миллисекунд.
        /// <remarks>При применении в кассовых приложениях не рекомендуется устанавливать тайм-аут слишком большим.</remarks></param>
        /// <returns>Ответ.</returns>
        public static string GetRequest(string url, int timeout = 2500)
        {
            return getRequest(url, timeout);
        }
        /// <summary>
        /// Отправить запрос с командой "DELETE" к серверу.
        /// </summary>
        /// <param name="url">Адрес сервера.</param>
        /// <param name="timeout">Тайм-аут выполнения операции (в миллисекундах). Значение "по умолчанию" - 2500 миллисекунд.</param>
        public static void DeleteRequest(string url, int timeout = 2500)
        {
            deleteRequest(url, timeout);
        }
        /// <summary>
        /// Загрузить файл на сервер методом "POST" и получить ответ.
        /// </summary>
        /// <param name="url">Адрес сервера.</param>
        /// <param name="fileBody">Тело файла (в кодировке UTF-8).
        /// <remarks>Если исходные данные содержатся в строке типа "string", 
        /// можно использовать метод "Encoding.UTF8.GetBytes(string)" для преобразования типов данных.</remarks></param>
        /// <param name="parameterName">Наименование параметра загрузки.
        /// <remarks>Например: "xml_file".</remarks></param>
        /// <param name="contentType">Тип контента.
        /// <remarks>Например: "text/xml; charset=utf-8".</remarks></param>
        /// <param name="nameValueCollection">Коллекция параметров запроса.
        /// <remarks>Например: "new NameValueCollection { { "Login", "Bill Gates" }, { "Password", "01234567890" } }".</remarks></param>
        /// <param name="timeout">Тайм-аут выполнения операции (в миллисекундах). Значение "по умолчанию" - 2500 миллисекунд.
        /// <remarks>При применении в кассовых приложениях не рекомендуется устанавливать тайм-аут слишком большим.</remarks></param>
        /// <returns>Ответ.</returns>
        public static string UploadFile(string url, byte[] fileBody, string parameterName, string contentType, NameValueCollection nameValueCollection, int timeout = 2500)
        {
            return uploadFile(url, fileBody, parameterName, contentType, nameValueCollection, timeout);
        }
        /// <summary>
        /// Загрузить файл на сервер методом "POST" и получить ответ.
        /// </summary>
        /// <param name="url">Адрес сервера.</param>
        /// <param name="fileBody">Тело файла (в кодировке UTF-8).
        /// <remarks>Если исходные данные содержатся в строке типа "string", 
        /// можно использовать метод "Encoding.UTF8.GetBytes(string)" для преобразования типов данных.</remarks></param>
        /// <param name="parameterName">Наименование параметра загрузки.
        /// <remarks>Например: "xml_file".</remarks></param>
        /// <param name="contentType">Тип контента.
        /// <remarks>Например: "text/xml; charset=utf-8".</remarks></param>
        /// <param name="timeout">Тайм-аут выполнения операции (в миллисекундах). Значение "по умолчанию" - 2500 миллисекунд.
        /// <remarks>При применении в кассовых приложениях не рекомендуется устанавливать тайм-аут слишком большим.</remarks></param>
        /// <returns>Ответ.</returns>
        public static string UploadFile(string url, byte[] fileBody, string parameterName, string contentType, int timeout = 2500)
        {
            return UploadFile(url, fileBody, parameterName, contentType, new NameValueCollection(), timeout);
        }
        /// <summary>
        /// Загрузить файл на сервер методом "POST" и получить ответ.
        /// </summary>
        /// <param name="url">Адрес сервера.</param>
        /// <param name="fileName">Полное имя файла (кодировка файла - UTF-8).
        /// <remarks>Если исходные данные содержатся в строке типа "string", 
        /// можно использовать метод "Encoding.UTF8.GetBytes(string)" для преобразования типов данных.</remarks></param>
        /// <param name="parameterName">Наименование параметра загрузки.
        /// <remarks>Например: "xml_file".</remarks></param>
        /// <param name="contentType">Тип контента.
        /// <remarks>Например: "text/xml; charset=utf-8".</remarks></param>
        /// <param name="nameValueCollection">Коллекция параметров запроса.
        /// <remarks>Например: "new NameValueCollection { { "Login", "Bill Gates" }, { "Password", "01234567890" } }".</remarks></param>
        /// <param name="timeout">Тайм-аут выполнения операции (в миллисекундах). Значение "по умолчанию" - 2500 миллисекунд.
        /// <remarks>При применении в кассовых приложениях не рекомендуется устанавливать тайм-аут слишком большим.</remarks></param>
        /// <returns>Ответ.</returns>
        public static string UploadFile(string url, string fileName, string parameterName, string contentType, NameValueCollection nameValueCollection, int timeout = 2500)
        {
            return UploadFile(url, File.ReadAllBytes(fileName), parameterName, contentType, nameValueCollection, timeout);
        }
        /// <summary>
        /// Загрузить файл на сервер методом "POST" и получить ответ.
        /// </summary>
        /// <param name="url">Адрес сервера.</param>
        /// <param name="fileName">Полное имя файла (кодировка файла - UTF-8).
        /// <remarks>Если исходные данные содержатся в строке типа "string", 
        /// можно использовать метод "Encoding.UTF8.GetBytes(string)" для преобразования типов данных.</remarks></param>
        /// <param name="parameterName">Наименование параметра загрузки.
        /// <remarks>Например: "xml_file".</remarks></param>
        /// <param name="contentType">Тип контента.
        /// <remarks>Например: "text/xml; charset=utf-8".</remarks></param>
        /// <param name="timeout">Тайм-аут выполнения операции (в миллисекундах). Значение "по умолчанию" - 2500 миллисекунд.
        /// <remarks>При применении в кассовых приложениях не рекомендуется устанавливать тайм-аут слишком большим.</remarks></param>
        /// <returns>Ответ.</returns>
        public static string UploadFile(string url, string fileName, string parameterName, string contentType, int timeout = 2500)
        {
            return UploadFile(url, fileName, parameterName, contentType, new NameValueCollection(), timeout);
        }
        /// <summary>
        /// Загрузить файл на сервер методом "POST" и получить ответ.
        /// </summary>
        /// <param name="url">Адрес сервера.</param>
        /// <param name="xmlDocument">XML-документ для загрузки.
        /// <remarks>Если исходные данные содержатся в строке типа "string", 
        /// можно использовать метод "Encoding.UTF8.GetBytes(string)" для преобразования типов данных.</remarks></param>
        /// <param name="parameterName">Наименование параметра загрузки.
        /// <remarks>Например: "xml_file".</remarks></param>
        /// <param name="contentType">Тип контента.
        /// <remarks>Например: "text/xml; charset=utf-8".</remarks></param>
        /// <param name="nameValueCollection">Коллекция параметров запроса.
        /// <remarks>Например: "new NameValueCollection { { "Login", "Bill Gates" }, { "Password", "01234567890" } }".</remarks></param>
        /// <param name="timeout">Тайм-аут выполнения операции (в миллисекундах). Значение "по умолчанию" - 2500 миллисекунд.
        /// <remarks>При применении в кассовых приложениях не рекомендуется устанавливать тайм-аут слишком большим.</remarks></param>
        /// <returns>Ответ.</returns>
        public static string UploadFile(string url, XmlDocument xmlDocument, string parameterName, string contentType, NameValueCollection nameValueCollection, int timeout = 2500)
        {
            return UploadFile(url, Encoding.UTF8.GetBytes(xmlDocument.OuterXml), parameterName, contentType, nameValueCollection, timeout);
        }
        /// <summary>
        /// Загрузить файл на сервер методом "POST" и получить ответ.
        /// </summary>
        /// <param name="url">Адрес сервера.</param>
        /// <param name="xmlDocument">XML-документ для загрузки.
        /// <remarks>Если исходные данные содержатся в строке типа "string", 
        /// можно использовать метод "Encoding.UTF8.GetBytes(string)" для преобразования типов данных.</remarks></param>
        /// <param name="parameterName">Наименование параметра загрузки.
        /// <remarks>Например: "xml_file".</remarks></param>
        /// <param name="contentType">Тип контента.
        /// <remarks>Например: "text/xml; charset=utf-8".</remarks></param>
        /// <param name="timeout">Тайм-аут выполнения операции (в миллисекундах). Значение "по умолчанию" - 2500 миллисекунд.
        /// <remarks>При применении в кассовых приложениях не рекомендуется устанавливать тайм-аут слишком большим.</remarks></param>
        /// <returns>Ответ.</returns>
        public static string UploadFile(string url, XmlDocument xmlDocument, string parameterName, string contentType, int timeout = 2500)
        {
            return UploadFile(url, xmlDocument, parameterName, contentType, new NameValueCollection(), timeout);
        }
        #endregion Внешние статические методы класса.

        #region Внутренние статические методы класса.
        /// <summary>
        /// Отправить запрос (метод "GET") к серверу и получить ответ.
        /// </summary>
        /// <param name="url">Адрес сервера.</param>
        /// <param name="timeout">Тайм-аут выполнения операции (в миллисекундах). Значение "по умолчанию" - 2500 миллисекунд.
        /// <remarks>При применении в кассовых приложениях не рекомендуется устанавливать тайм-аут слишком большим.</remarks></param>
        /// <returns>Ответ.</returns>
        private static string getRequest(string url, int timeout = 2500)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";
            request.Timeout = timeout;
            request.UserAgent = userAgent;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream stream = response.GetResponseStream();

            if (stream == null) throw new Exception("Object 'HttpWebResponse.GetResponseStream' is null.");

            string result;

            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            response.Close();

            return result;
        }
        /// <summary>
        /// Отправить запрос с командой "DELETE" к серверу.
        /// </summary>
        /// <param name="url">Адрес сервера.</param>
        /// <param name="timeout">Тайм-аут выполнения операции (в миллисекундах). Значение "по умолчанию" - 2500 миллисекунд.</param>
        private static void deleteRequest(string url, int timeout = 2500)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "DELETE";
            request.Timeout = timeout;
            request.UserAgent = userAgent;

            request.GetResponse();
        }
        /// <summary>
        /// Загрузить файл на сервер методом "POST" и получить ответ.
        /// </summary>
        /// <param name="url">Адрес сервера.</param>
        /// <param name="fileBody">Тело файла (в кодировке UTF-8).</param>
        /// <param name="parameterName">Наименование параметра загрузки.
        /// <remarks>Например: "xml_file".</remarks></param>
        /// <param name="contentType">Тип контента.
        /// <remarks>Например: "text/xml; charset=utf-8".</remarks></param>
        /// <param name="nameValueCollection">Коллекция параметров запроса.
        /// <remarks>Например: "new NameValueCollection { { "Login", "Bill Gates" }, { "Password", "01234567890" } }".</remarks></param>
        /// <param name="timeout">Тайм-аут выполнения операции (в миллисекундах). Значение "по умолчанию" - 2500 миллисекунд.
        /// <remarks>При применении в кассовых приложениях не рекомендуется устанавливать тайм-аут слишком большим.</remarks></param>
        /// <returns>Ответ.</returns>
        private static string uploadFile(string url, byte[] fileBody, string parameterName, string contentType, NameValueCollection nameValueCollection, int timeout = 2500)
        {
            const string crlf = "\r\n";

            string boundary = "--------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundaryBegin = Encoding.UTF8.GetBytes("--" + boundary + crlf);
            byte[] boundaryMiddle = Encoding.UTF8.GetBytes(crlf + "--" + boundary + crlf);
            byte[] boundaryFinal = Encoding.UTF8.GetBytes(crlf + "--" + boundary + "--" + crlf);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "POST";
            request.KeepAlive = true;
            request.Timeout = timeout;
            request.Headers.Add("Pragma: no-cache");
            request.UserAgent = userAgent;
            request.Accept = "*/*";
            request.Credentials = CredentialCache.DefaultCredentials;
            request.ContentType = "multipart/form-data; boundary=" + boundary;

            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.Write(boundaryBegin, 0, boundaryBegin.Length);

                foreach (string key in nameValueCollection.Keys)
                {
                    string formitem = string.Format("Content-Disposition: form-data; name=\"xml_file\"; filename=\"{0}\"{2}{2}{1}", key, nameValueCollection[key], crlf);
                    byte[] formitembytes = Encoding.UTF8.GetBytes(formitem);
                    requestStream.Write(formitembytes, 0, formitembytes.Length);
                    requestStream.Write(boundaryMiddle, 0, boundaryMiddle.Length);
                }

                string header = string.Format("Content-Disposition: form-data; name=\"xml_file\"; filename=\"{0}\";{2}Content-Type: {1}{2}{2}", parameterName, contentType, crlf);
                byte[] headerBytes = Encoding.UTF8.GetBytes(header);

                requestStream.Write(headerBytes, 0, headerBytes.Length);
                requestStream.Write(fileBody, 0, fileBody.Length);
                requestStream.Write(boundaryFinal, 0, boundaryFinal.Length);
            }

            string result;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                Stream stream = response.GetResponseStream();

                if (stream == null) throw new Exception("Object 'HttpWebResponse.GetResponseStream' is null.");

                using (StreamReader reader = new StreamReader(stream))
                {
                    result = reader.ReadToEnd();
                }
            }

            return result;
        }
        #endregion Внутренние статические методы класса.
    }
}
