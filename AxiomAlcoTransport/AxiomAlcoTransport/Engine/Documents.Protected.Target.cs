using System;
using System.Xml;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Collections.Generic;
using Axiom.AlcoTransport.Document;
using AxiomAuxiliary.Transports;
using DevExpress.Data.Helpers;
using DevExpress.Utils.Extensions;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Списки документов приложения.
    /// </summary>
    public partial class Documents
    {
        #region Защищённые методы класса.

        #region Partners...

        /// <summary>
        /// Получить справочник партнёров.
        /// </summary>
        /// <param name="url">Адрес для загрузки.</param>
        /// <param name="replyId">Идентификатор исходного запроса.</param>
        /// <param name="version">Версия документа.</param>
        /// <param name="xml">Ответ сервера.</param>
        protected virtual void getPartners(string url, string replyId, XmlDocument xml, int version = 1)
        {
            string nameReplyClient = (version == 1) ? "ns:ReplyClient" : "ns:ReplyClient_v2";

            if ((xml.DocumentElement == null)
                || (xml.DocumentElement["ns:Document"] == null)
                || (xml.DocumentElement["ns:Document"][nameReplyClient] == null)
                || (xml.DocumentElement["ns:Document"][nameReplyClient]["rc:Clients"] == null))
                throw new Exception("Необходимые XML-ноды в документе пустые.");

            XmlNode clients = xml.DocumentElement["ns:Document"][nameReplyClient]["rc:Clients"];

            if (clients.HasChildNodes)
            {
                foreach (XmlNode client in clients.ChildNodes)
                {
                    add(new Partner(client, version)
                    {
                        ReplyId = replyId,
                        Url = url
                    });
                }
            }
            else
            {
                string msg =
                    string.Format(
                        "Документ - справочник организаций, полученный с адреса '{0}', имеет корректный формат, однако не содержит в своём составе ни одной организации-партнёра.",
                        url);
                onlineEvents.Add(new OnlineEvent(OnlineEventStatus.Warning, this, msg));
                Program.Logger.Warn(msg);
            }
        }

        /// <summary>
        /// Добавить новый документ.
        /// </summary>
        /// <param name="obj">Новый документ.</param>
        protected virtual void add(Partner obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка добавить в хранилище полученный документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            Partner exist = partners.FirstOrDefault(x => x.ClientRegId == obj.ClientRegId);

            if (exist != null)
            {
                Program.Logger.Warn(this,
                    string.Format(
                        "Объект '{0}' уже присутствует в хранилище. Будет произведена попытка замены существующего объекта на новый.",
                        exist.Description));

                remove(exist);
            }

            partners.Add(obj);

            storage.Save(obj);

            Program.Logger.Info(this, "Новый документ успешно добавлен.");
        }

        /// <summary>
        /// Удалить документ из хранилища.
        /// </summary>
        /// <param name="obj">Документ для удаления.</param>
        protected virtual void remove(Partner obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка удалить из хранилища документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            partners.Remove(obj);

            storage.Delete(obj);

            Program.Logger.Info(this, "Документ успешно удалён из хранилища.");
        }

        /// <summary>
        /// Отправить запрос на получение справочника организаций.
        /// </summary>
        /// <param name="inn">ИНН</param>
        protected virtual void requestPartners(string inn)
        {
            XmlDocument client = getClientData(inn);

            #region Сохранение исходящего запроса.

            client.Save(string.Format("{0}\\{1}.requestclient.{2}.{3}", storage.PathOut, FileStorage.NativePrefix,
                DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));

            #endregion Сохранение исходящего запроса.

            Program.Logger.Info(this, string.Format("Попытка отправить данные '{0}' по адресу '{1}'...",
                client.OuterXml,
                (documentsVersion == 1) ? addresses.RequestPartners : addresses.RequestPartners_v2));

            HttpTransport.UploadFile((documentsVersion == 1) ? addresses.RequestPartners : addresses.RequestPartners_v2,
                client, "xml_file", "text/xml; charset=utf-8", configuration.UtmTimeoutLong);

            Program.Logger.Info(this, "... данные успешно отправлены.");
        }

        /// <summary>
        /// Сформировать xml-документ, представляющий клиента.
        /// </summary>
        /// <param name="inn">ИНН</param>
        /// <returns>XML-документа.</returns>
        protected virtual XmlDocument getClientData(string inn)
        {
            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            XmlNode docsNode = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docsNode, "Version", "1.0");
            addAttribute(xml, docsNode, "xmlns:" + addresses.Xsi.Prefix, addresses.Xsi.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Ns.Prefix, addresses.Ns.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Oref.Prefix, addresses.Oref.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Qp.Prefix, addresses.Qp.Uri);
            xml.AppendChild(docsNode);

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docsNode.AppendChild(owner);

            XmlNode FsrarId = xml.CreateElement(addresses.Ns.Prefix, "FSRAR_ID", addresses.Ns.Uri);
            FsrarId.InnerText = configuration.FsrarId;
            owner.AppendChild(FsrarId);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docsNode.AppendChild(doc);

            XmlNode queryClients = (documentsVersion == 1)
                ? xml.CreateElement(addresses.Ns.Prefix, "QueryClients", addresses.Ns.Uri)
                : xml.CreateElement(addresses.Ns.Prefix, "QueryClients_v2", addresses.Ns.Uri);
            doc.AppendChild(queryClients);

            XmlNode parameters = xml.CreateElement(addresses.Qp.Prefix, "Parameters", addresses.Qp.Uri);
            queryClients.AppendChild(parameters);

            XmlNode parameter = xml.CreateElement(addresses.Qp.Prefix, "Parameter", addresses.Qp.Uri);
            parameters.AppendChild(parameter);

            XmlNode name = xml.CreateElement(addresses.Qp.Prefix, "Name", addresses.Qp.Uri);
            name.InnerText = "ИНН";
            parameter.AppendChild(name);

            XmlNode value = xml.CreateElement(addresses.Qp.Prefix, "Value", addresses.Qp.Uri);
            value.InnerText = inn;
            parameter.AppendChild(value);

            return xml;
        }

        /// <summary>
        /// Найти партнёра по его идентификатору.
        /// </summary>
        /// <param name="clientRegId">Идентификатор.</param>
        /// <returns>Партнёр.</returns>
        protected virtual Partner findPartner(string clientRegId)
        {
            if (string.IsNullOrWhiteSpace(clientRegId))
                throw new Exception("Поиск организации по пустому идентификатору невозможен.");

            foreach (Partner partner in Partners)
            {
                if (partner.ClientRegId == clientRegId) return partner;
            }

            throw new Exception(string.Format("Организация с идентификатором '{0}' отсутствует в списке.",
                clientRegId));
        }

        /// <summary>
        /// Преобразовать строку партнёра в формат второй версии.
        /// <remarks>Грубый метод. Требует изменения.</remarks>
        /// </summary>
        /// <param name="source">Исходная строка.</param>
        /// <returns>Преобразованная строка.</returns>
        protected virtual string convertPartnerToV2(string source)
        {
            Program.Logger.Info(this,
                string.Format("Попытка преобразовать строку партнёра '{0}' в формат второй версии...", source));

            if (definePartnerVersion(source) == 2)
            {
                Program.Logger.Info(this,
                    "... преобразования не требуется; строка партнёра уже записана в формате второй версии.");

                return source;
            }

            // TODO: Грубый метод. Требует изменения.

            string target = source.Replace("ru/WEGAIS/ClientRef\"", "ru/WEGAIS/ClientRef_v2\"")
                .Replace("ru/WEGAIS/ProductRef\"", "ru/WEGAIS/ProductRef_v2\"")
                .Replace("<pref:Importer>", "<pref:Importer><oref:UL>")
                .Replace("</pref:Importer>", "</oref:UL></pref:Importer>");

            if (target.Contains("<pref:Importer>"))
            {
                target = target.Replace("<pref:Producer>",
                        "<pref:Producer><oref:FO xmlns:oref=\"http://fsrar.ru/WEGAIS/ClientRef_v2\">")
                    .Replace("<pref:Producer xmlns:pref=\"http://fsrar.ru/WEGAIS/ProductRef\">",
                        "<pref:Producer xmlns:pref=\"http://fsrar.ru/WEGAIS/ProductRef_v2\"><oref:FO xmlns:oref=\"http://fsrar.ru/WEGAIS/ClientRef_v2\">")
                    .Replace("<pref:Producer xmlns:pref=\"http://fsrar.ru/WEGAIS/ProductRef_v2\">",
                        "<pref:Producer xmlns:pref=\"http://fsrar.ru/WEGAIS/ProductRef_v2\"><oref:FO xmlns:oref=\"http://fsrar.ru/WEGAIS/ClientRef_v2\">")
                    .Replace("</pref:Producer>", "</oref:FO></pref:Producer>");
            }
            else
            {
                target = target.Replace("<pref:Producer>",
                        "<pref:Producer><oref:UL xmlns:oref=\"http://fsrar.ru/WEGAIS/ClientRef_v2\">")
                    .Replace("<pref:Producer xmlns:pref=\"http://fsrar.ru/WEGAIS/ProductRef\">",
                        "<pref:Producer xmlns:pref=\"http://fsrar.ru/WEGAIS/ProductRef_v2\"><oref:UL xmlns:oref=\"http://fsrar.ru/WEGAIS/ClientRef_v2\">")
                    .Replace("<pref:Producer xmlns:pref=\"http://fsrar.ru/WEGAIS/ProductRef_v2\">",
                        "<pref:Producer xmlns:pref=\"http://fsrar.ru/WEGAIS/ProductRef_v2\"><oref:UL xmlns:oref=\"http://fsrar.ru/WEGAIS/ClientRef_v2\">")
                    .Replace("</pref:Producer>", "</oref:UL></pref:Producer>");
            }

            Program.Logger.Info(this,
                string.Format("... преобразование строки партнёра в формат второй версии завершено: '{0}'.", target));

            return target;
        }

        /// <summary>
        /// Определить версию партнёра.
        /// <remarks>Грубый метод. Требует изменения.</remarks>
        /// </summary>
        /// <param name="xmlBody">Тело.</param>
        /// <returns>Преобразованная строка.</returns>
        protected virtual int definePartnerVersion(string xmlBody)
        {
            // TODO: Грубый метод. Требует изменения.

            string low = xmlBody.ToLower();

            if ((low.Contains("wegais/clientref_v2"))
                || (low.Contains("wegais/productref_v2"))
                || (low.Contains("wegais/replyap_v2"))
                || (low.Contains("oref:ul>"))
                || (low.Contains("oref:fl>"))
                || (low.Contains("oref:fo>"))
                || (low.Contains("oref:ts>")))
            {
                return 2;
            }

            return 1;
        }

        #endregion Partners...

        #region Rests...

        /// <summary>
        /// Получить остатки.
        /// </summary>
        /// <param name="url">Адрес для загрузки.</param>
        /// <param name="replyId">Идентификатор исходного запроса.</param>
        /// <param name="xml">Ответ сервера.</param>
        /// <param name="version">Версия документа.</param>
        protected virtual void getRests(string url, string replyId, XmlDocument xml, int version = 1)
        {
            string nameReplyRests = (version == 1) ? "ns:ReplyRests" : "ns:ReplyRests_v2";

            if ((xml.DocumentElement == null)
                || (xml.DocumentElement["ns:Document"] == null)
                || (xml.DocumentElement["ns:Document"][nameReplyRests] == null))
                throw new Exception("Необходимые XML-ноды в документе пустые.");

            Rests rests = new Rests(xml.DocumentElement["ns:Document"][nameReplyRests], version)
            {
                ReplyId = replyId,
                Url = url,
            };

            add(rests);
        }

        /// <summary>
        /// Добавить новый документ.
        /// </summary>
        /// <param name="obj">Новый документ.</param>
        protected virtual void add(Rests obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка добавить в хранилище полученный документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            Rests exist = restsList.FirstOrDefault(x => x.ReplyId == obj.ReplyId);

            if (exist != null)
            {
                Program.Logger.Warn(this,
                    string.Format(
                        "Объект '{0}' уже присутствует в хранилище. Будет произведена попытка замены существующего объекта на новый.",
                        exist.Description));

                remove(exist);
            }

            restsList.Add(obj);

            storage.Save(obj);

            Program.Logger.Info(this, "Новый документ успешно добавлен.");
        }

        /// <summary>
        /// Удалить документ из хранилища.
        /// </summary>
        /// <param name="obj">Документ для удаления.</param>
        protected virtual void remove(Rests obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка удалить из хранилища документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            restsList.Remove(obj);

            storage.Delete(obj);

            Program.Logger.Info(this, "Документ успешно удалён из хранилища.");
        }

        /// <summary>
        /// Отправить запрос на получение остатков.
        /// </summary>
        protected virtual void requestRests()
        {
            XmlDocument xmlDocument = getRestsData();

            #region Сохранение исходящего запроса.

            xmlDocument.Save(string.Format("{0}\\{1}.requestrests.{2}.{3}", storage.PathOut, FileStorage.NativePrefix,
                DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));

            #endregion Сохранение исходящего запроса.

            Program.Logger.Info(this, string.Format("Попытка отправить данные '{0}' по адресу '{1}'...",
                xmlDocument.OuterXml,
                (documentsVersion == 1) ? addresses.RequestRests : addresses.RequestRests_v2));

            HttpTransport.UploadFile((documentsVersion == 1) ? addresses.RequestRests : addresses.RequestRests_v2,
                xmlDocument, "xml_file", "text/xml; charset=utf-8", configuration.UtmTimeoutLong);

            Program.Logger.Info(this, "... данные успешно отправлены.");
        }

        /// <summary>
        /// Сформировать xml-документ, представляющий запрос остатков.
        /// </summary>
        /// <returns></returns>
        protected virtual XmlDocument getRestsData()
        {
            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            XmlNode docsNode = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docsNode, "Version", "1.0");
            addAttribute(xml, docsNode, "xmlns:" + addresses.Xsi.Prefix, addresses.Xsi.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Ns.Prefix, addresses.Ns.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Qp.Prefix, addresses.Qp.Uri);
            xml.AppendChild(docsNode);

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docsNode.AppendChild(owner);

            XmlNode FsrarId = xml.CreateElement(addresses.Ns.Prefix, "FSRAR_ID", addresses.Ns.Uri);
            FsrarId.InnerText = configuration.FsrarId;
            owner.AppendChild(FsrarId);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docsNode.AppendChild(doc);

            XmlNode queryRests = (documentsVersion == 1)
                ? xml.CreateElement(addresses.Ns.Prefix, "QueryRests", addresses.Ns.Uri)
                : xml.CreateElement(addresses.Ns.Prefix, "QueryRests_v2", addresses.Ns.Uri);
            doc.AppendChild(queryRests);

            return xml;
        }

        #endregion Rests...

        #region ShopRests...

        /// <summary>
        /// Получить остатки.
        /// </summary>
        /// <param name="url">Адрес для загрузки.</param>
        /// <param name="replyId">Идентификатор исходного запроса.</param>
        /// <param name="xml">Ответ сервера.</param>
        protected virtual void getShopRests(string url, string replyId, XmlDocument xml)
        {
            if ((xml.DocumentElement == null)
                || (xml.DocumentElement["ns:Document"] == null)
                || (xml.DocumentElement["ns:Document"]["ns:ReplyRestsShop_v2"] == null))
                throw new Exception("Необходимые XML-ноды в документе пустые.");

            ShopRests shopRests = new ShopRests(xml.DocumentElement["ns:Document"]["ns:ReplyRestsShop_v2"])
            {
                ReplyId = replyId,
                Url = url,
            };

            add(shopRests);
        }

        /// <summary>
        /// Добавить новый документ.
        /// </summary>
        /// <param name="obj">Новый документ.</param>
        protected virtual void add(ShopRests obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка добавить в хранилище полученный документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            ShopRests exist = shopRestsList.FirstOrDefault(x => x.ReplyId == obj.ReplyId);

            if (exist != null)
            {
                Program.Logger.Warn(this,
                    string.Format(
                        "Объект '{0}' уже присутствует в хранилище. Будет произведена попытка замены существующего объекта на новый.",
                        exist.Description));

                remove(exist);
            }

            shopRestsList.Add(obj);

            storage.Save(obj);

            Program.Logger.Info(this, "Новый документ успешно добавлен.");
        }

        /// <summary>
        /// Удалить документ из хранилища.
        /// </summary>
        /// <param name="obj">Документ для удаления.</param>
        protected virtual void remove(ShopRests obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка удалить из хранилища документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            shopRestsList.Remove(obj);

            storage.Delete(obj);

            Program.Logger.Info(this, "Документ успешно удалён из хранилища.");
        }

        /// <summary>
        /// Отправить запрос на получение остатков.
        /// </summary>
        protected virtual void requestShopRests()
        {
            XmlDocument xmlDocument = getShopRestsData();

            #region Сохранение исходящего запроса.

            xmlDocument.Save(string.Format("{0}\\{1}.requestshoprests.{2}.{3}", storage.PathOut,
                FileStorage.NativePrefix, DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));

            #endregion Сохранение исходящего запроса.

            Program.Logger.Info(this,
                string.Format("Попытка отправить данные '{0}' по адресу '{1}'...", xmlDocument.OuterXml,
                    addresses.RequestShopRests));

            HttpTransport.UploadFile(addresses.RequestShopRests, xmlDocument, "xml_file", "text/xml; charset=utf-8",
                configuration.UtmTimeoutLong);

            Program.Logger.Info(this, "... данные успешно отправлены.");
        }

        /// <summary>
        /// Сформировать xml-документ, представляющий запрос остатков.
        /// </summary>
        /// <returns></returns>
        protected virtual XmlDocument getShopRestsData()
        {
            XmlNode doc;
            var xml = GetCommonRestRequestXml(out doc);

            XmlNode queryRests = xml.CreateElement(addresses.Ns.Prefix, "QueryRestsShop_v2", addresses.Ns.Uri);
            doc.AppendChild(queryRests);

            return xml;
        }

        #endregion ShopRests...

        #region BCodeRests...

        /// <summary>
        /// Получить остатки.
        /// </summary>
        /// <param name="url">Адрес для загрузки.</param>
        /// <param name="replyId">Идентификатор исходного запроса.</param>
        /// <param name="xml">Ответ сервера.</param>
        protected virtual void getBcodeRests(string url, string replyId, XmlDocument xml)
        {
            if (xml.DocumentElement?["ns:Document"]?["ns:ReplyRestBCode"] == null)
                throw new Exception("Необходимые XML-ноды в документе пустые.");

            BCodeRests bCodeRests = new BCodeRests(xml.DocumentElement["ns:Document"]["ns:ReplyRestBCode"], 3)
            {
                ReplyId = replyId,
                Url = url,
            };

            add(bCodeRests);
        }

        /// <summary>
        /// Добавить новый документ.
        /// </summary>
        /// <param name="obj">Новый документ.</param>
        protected virtual void add(BCodeRests obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка добавить в хранилище полученный документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            BCodeRests exist = bCodeRestsList.FirstOrDefault(x => x.ReplyId == obj.ReplyId);

            if (exist != null)
            {
                Program.Logger.Warn(this,
                    string.Format(
                        "Объект '{0}' уже присутствует в хранилище. Будет произведена попытка замены существующего объекта на новый.",
                        exist.Description));

                remove(exist);
            }

            bCodeRestsList.Add(obj);

            storage.Save(obj);

            Program.Logger.Info(this, "Новый документ успешно добавлен.");
        }

        /// <summary>
        /// Удалить документ из хранилища.
        /// </summary>
        /// <param name="obj">Документ для удаления.</param>
        protected virtual void remove(BCodeRests obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка удалить из хранилища документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            bCodeRestsList.Remove(obj);

            storage.Delete(obj);

            Program.Logger.Info(this, "Документ успешно удалён из хранилища.");
        }

        /// <summary>
        /// Отправить запрос на получение остатков.
        /// </summary>
        protected virtual void requestBCodeRests(string form2Name)
        {
            XmlDocument xmlDocument = getBCodeRestsData(form2Name);

            #region Сохранение исходящего запроса.

            xmlDocument.Save(string.Format("{0}\\{1}.requestshoprests.{2}.{3}", storage.PathOut,
                FileStorage.NativePrefix, DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));

            #endregion Сохранение исходящего запроса.

            Program.Logger.Info(this,
                string.Format("Попытка отправить данные '{0}' по адресу '{1}'...", xmlDocument.OuterXml,
                    addresses.RequestBCodeRests));

            HttpTransport.UploadFile(addresses.RequestBCodeRests, xmlDocument, "xml_file", "text/xml; charset=utf-8",
                configuration.UtmTimeoutLong);

            Program.Logger.Info(this, "... данные успешно отправлены.");
        }

        /// <summary>
        /// Сформировать xml-документ, представляющий запрос остатков.
        /// </summary>
        /// <param name="form2Name"></param>
        /// <returns></returns>
        protected virtual XmlDocument getBCodeRestsData(string form2Name)
        {
            XmlNode doc;
            var xml = GetCommonRestRequestXml(out doc);

            XmlNode queryRests = xml.CreateElement(addresses.Ns.Prefix, "QueryRestBCode", addresses.Ns.Uri);
            doc.AppendChild(queryRests);

            XmlNode parameters = xml.CreateElement(addresses.Qp.Prefix, "Parameters", addresses.Qp.Uri);
            queryRests.AppendChild(parameters);

            XmlNode parameter = xml.CreateElement(addresses.Qp.Prefix, "Parameter", addresses.Qp.Uri);
            parameters.AppendChild(parameter);

            XmlNode name = xml.CreateElement(addresses.Qp.Prefix, "Name", addresses.Qp.Uri);
            name.InnerText = "ФОРМА2";
            parameter.AppendChild(name);

            XmlNode form2Node = xml.CreateElement(addresses.Qp.Prefix, "Value", addresses.Qp.Uri);
            form2Node.InnerText = form2Name;
            parameter.AppendChild(form2Node);

            return xml;
        }

        /// <summary>
        /// Общий xml у запросов QueryRestBCode и QueryShop_V2
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private XmlDocument GetCommonRestRequestXml(out XmlNode doc)
        {
            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            XmlNode docsNode = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docsNode, "Version", "1.0");
            addAttribute(xml, docsNode, "xmlns:" + addresses.Xsi.Prefix, addresses.Xsi.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Ns.Prefix, addresses.Ns.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Qp.Prefix, addresses.Qp.Uri);
            xml.AppendChild(docsNode);

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docsNode.AppendChild(owner);

            XmlNode FsrarId = xml.CreateElement(addresses.Ns.Prefix, "FSRAR_ID", addresses.Ns.Uri);
            FsrarId.InnerText = configuration.FsrarId;
            owner.AppendChild(FsrarId);

            doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docsNode.AppendChild(doc);
            return xml;
        }

        #endregion BCodeRests...

        #region AP...

        /// <summary>
        /// Получить справочник продукции.
        /// </summary>
        /// <param name="url">Адрес для загрузки.</param>
        /// <param name="replyId">Идентификатор исходного запроса.</param>
        /// <param name="xml">Ответ сервера.</param>
        /// <param name="productionType">Тип алкогольной продукции.</param>
        /// <param name="version">Версия документа.</param>
        protected virtual void getAP(string url, string replyId, XmlDocument xml, ProductionType productionType,
            int version = 1)
        {
            string nameReply;
            string nameProducts;

            switch (productionType)
            {
                case ProductionType.Alcohol:
                {
                    nameReply = (version == 1) ? "ns:ReplyAP" : "ns:ReplyAP_v2";
                    nameProducts = "rap:Products";
                    break;
                }

                case ProductionType.Spirit:
                {
                    nameReply = "ns:ReplySpirit_v2";
                    nameProducts = "rs:Products";
                    break;
                }

                case ProductionType.SpiritContainer:
                {
                    nameReply = "ns:ReplySSP_v2";
                    nameProducts = "rap:Products";
                    break;
                }

                default:
                    throw new Exception(string.Format("Неизвестный тип алкогольной продукции ('{0}').",
                        productionType));
            }

            if ((xml.DocumentElement == null)
                || (xml.DocumentElement["ns:Document"] == null)
                || (xml.DocumentElement["ns:Document"][nameReply] == null)
                || (xml.DocumentElement["ns:Document"][nameReply][nameProducts] == null))
                throw new Exception("Необходимые XML-ноды в документе пустые.");

            XmlNode products = xml.DocumentElement["ns:Document"][nameReply][nameProducts];

            if (products.HasChildNodes)
            {
                foreach (XmlNode product in products.ChildNodes)
                {
                    add(new Production(product, version, productionType)
                    {
                        ReplyId = replyId,
                        Url = url
                    });
                }
            }
            else
            {
                string msg =
                    string.Format(
                        "Документ - справочник алкогольной продукции, полученный с адреса '{0}', имеет корректный формат, однако не содержит в своём составе ни одной номенклатуры товара.",
                        url);
                onlineEvents.Add(new OnlineEvent(OnlineEventStatus.Warning, this, msg));
                Program.Logger.Warn(msg);
            }
        }

        /// <summary>
        /// Добавить новый документ.
        /// </summary>
        /// <param name="obj">Новый документ.</param>
        protected virtual void add(Production obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка добавить в хранилище полученный документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            Production exist = production.FirstOrDefault(x => x.AlcCode == obj.AlcCode);

            if (exist != null)
            {
                Program.Logger.Warn(this,
                    string.Format(
                        "Объект '{0}' уже присутствует в хранилище. Будет произведена попытка замены существующего объекта на новый.",
                        exist.Description));

                remove(exist);
            }

            production.Add(obj);

            storage.Save(obj);

            Program.Logger.Info(this, "Новый документ успешно добавлен.");
        }

        /// <summary>
        /// Удалить документ из хранилища.
        /// </summary>
        /// <param name="obj">Документ для удаления.</param>
        protected virtual void remove(Production obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка удалить из хранилища документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            production.Remove(obj);

            storage.Delete(obj);

            Program.Logger.Info(this, "Документ успешно удалён из хранилища.");
        }

        /// <summary>
        /// Отправить запрос на получение справочника алкогольной продукции.
        /// </summary>
        /// <param name="inn">ИНН</param>
        protected virtual void requestAP(string inn)
        {
            bool sendSpirit = Program.GetBooleanParameter("sendQueryForSpirit", false);
            bool sendSpiritContainer = Program.GetBooleanParameter("sendQueryForSpiritContainer", false);

            foreach (ProductionType type in Enum.GetValues(typeof(ProductionType)))
            {
                switch (type)
                {
                    case ProductionType.Spirit:
                    {
                        if (!sendSpirit) continue;
                        break;
                    }
                    case ProductionType.SpiritContainer:
                    {
                        if (!sendSpiritContainer) continue;
                        break;
                    }
                }

                XmlDocument xmlDocument = getAPData(inn, type);

                #region Сохранение исходящего запроса.

                xmlDocument.Save(string.Format("{0}\\{1}.request{2}.{3}.{4}",
                    storage.PathOut, FileStorage.NativePrefix, type.ToString().ToLower(),
                    DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));

                #endregion Сохранение исходящего запроса.

                string address;

                switch (type)
                {
                    case ProductionType.Alcohol:
                    {
                        address = (documentsVersion == 1) ? addresses.RequestAP : addresses.RequestAP_v2;
                        break;
                    }
                    case ProductionType.Spirit:
                    {
                        address = addresses.RequestSpirit;
                        break;
                    }
                    case ProductionType.SpiritContainer:
                    {
                        address = addresses.RequestSpiritContainer;
                        break;
                    }

                    default: throw new Exception(string.Format("Неизвестный тип алкогольной продукции ('{0}').", type));
                }

                Program.Logger.Info(this,
                    string.Format("Попытка отправить данные '{0}' по адресу '{1}'...", xmlDocument.OuterXml, address));

                HttpTransport.UploadFile(address, xmlDocument, "xml_file", "text/xml; charset=utf-8",
                    configuration.UtmTimeoutLong);

                Program.Logger.Info(this, "... данные успешно отправлены.");
            }
        }

        /// <summary>
        /// Сформировать xml-документ, представляющий запрос справочника алкогольной продукции.
        /// </summary>
        /// <param name="inn">ИНН</param>
        /// <param name="productionType">Тип алкогольной продукции.</param>
        /// <returns>XML-документа.</returns>
        protected virtual XmlDocument getAPData(string inn, ProductionType productionType)
        {
            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            XmlNode docsNode = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docsNode, "Version", "1.0");
            addAttribute(xml, docsNode, "xmlns:" + addresses.Xsi.Prefix, addresses.Xsi.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Ns.Prefix, addresses.Ns.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Qp.Prefix, addresses.Qp.Uri);

            if ((productionType == ProductionType.Spirit) || (productionType == ProductionType.SpiritContainer))
            {
                addAttribute(xml, docsNode, "xmlns:" + addresses.Oref.Prefix, addresses.Oref.Uri);
            }

            xml.AppendChild(docsNode);

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docsNode.AppendChild(owner);

            XmlNode FsrarId = xml.CreateElement(addresses.Ns.Prefix, "FSRAR_ID", addresses.Ns.Uri);
            FsrarId.InnerText = configuration.FsrarId;
            owner.AppendChild(FsrarId);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docsNode.AppendChild(doc);

            XmlNode queryAP;

            switch (productionType)
            {
                case ProductionType.Alcohol:
                {
                    queryAP = (documentsVersion == 1)
                        ? xml.CreateElement(addresses.Ns.Prefix, "QueryAP", addresses.Ns.Uri)
                        : xml.CreateElement(addresses.Ns.Prefix, "QueryAP_v2", addresses.Ns.Uri);
                    break;
                }

                case ProductionType.Spirit:
                {
                    queryAP = xml.CreateElement(addresses.Ns.Prefix, "QuerySP_v2", addresses.Ns.Uri);
                    break;
                }

                case ProductionType.SpiritContainer:
                {
                    queryAP = xml.CreateElement(addresses.Ns.Prefix, "QuerySSP_v2", addresses.Ns.Uri);
                    break;
                }

                default:
                    throw new Exception(string.Format("Неизвестный тип алкогольной продукции ('{0}').",
                        productionType));
            }

            doc.AppendChild(queryAP);

            XmlNode parameters = xml.CreateElement(addresses.Qp.Prefix, "Parameters", addresses.Qp.Uri);
            queryAP.AppendChild(parameters);

            XmlNode parameter = xml.CreateElement(addresses.Qp.Prefix, "Parameter", addresses.Qp.Uri);
            parameters.AppendChild(parameter);

            XmlNode name = xml.CreateElement(addresses.Qp.Prefix, "Name", addresses.Qp.Uri);
            name.InnerText = "ИНН";
            parameter.AppendChild(name);

            XmlNode value = xml.CreateElement(addresses.Qp.Prefix, "Value", addresses.Qp.Uri);
            value.InnerText = inn;
            parameter.AppendChild(value);

            return xml;
        }

        /// <summary>
        /// Найти продукцию по его идентификатору.
        /// </summary>
        /// <param name="alcCode">Идентификатор.</param>
        /// <returns>Продукция.</returns>
        protected virtual Production findProduction(string alcCode)
        {
            if (string.IsNullOrWhiteSpace(alcCode))
                throw new Exception("Поиск алкогольной продукции по пустому идентификатору невозможен.");

            foreach (Production ap in Production)
            {
                if (ap.AlcCode == alcCode) return ap;
            }

            throw new Exception(
                string.Format("Алкогольная продукция с идентификатором '{0}' отсутствует в справочнике.", alcCode));
        }

        #endregion AP...

        #region InWaybill...

        /// <summary>
        /// Получить накладную.
        /// </summary>
        /// <param name="url">Адрес для загрузки.</param>
        /// <param name="xml">Ответ сервера.</param>
        /// <param name="version">Версия документа.</param>
        protected virtual void getInWaybill(string url, XmlDocument xml, int version = 1)
        {
            if (xml.DocumentElement == null)
                throw new Exception("Необходимые XML-ноды в документе пустые.");
            var nsPrefix =
                xml.DocumentElement.Name.Split(new[] {":"}, StringSplitOptions.None)[0];
            var nameSpaces = new Dictionary<string, string>();
            foreach (XmlAttribute xmlAttribute in xml.DocumentElement.Attributes)
            {
                if (xmlAttribute.Name.Contains(":"))
                    nameSpaces.Add(xmlAttribute.Value.ToLower(),
                        xmlAttribute.Name.Split(new[] {":"}, StringSplitOptions.None)[1]);
            }

            string nameWayBill = (version == 1) ? $"{nsPrefix}:WayBill" : $"{nsPrefix}:WayBill_v{version}";

            if (xml.DocumentElement?[$"{nsPrefix}:Document"]?[nameWayBill] == null)
                throw new Exception("Необходимые XML-ноды в документе пустые.");

            InWaybill inWaybill =
                new InWaybill(xml.DocumentElement[$"{nsPrefix}:Document"]?[nameWayBill], version, nameSpaces)
                    {Url = url, VersionEgais = version};

            FormBRegInfo form = formBRegInfos.FirstOrDefault(item =>
                ((item.IsFreeDocument) && (item.AuxCompositeId == inWaybill.AuxCompositeId)));

            if ((form != null) && (inWaybill.StatusEnum == InWaybillStatus.Partial))
            {
                inWaybill.AddData(form);
                form.SetNonFreeStatus();
                storage.Save(form);
            }

            add(inWaybill);
        }

        /// <summary>
        /// Добавить новый документ.
        /// </summary>
        /// <param name="obj">Новый документ.</param>
        protected virtual void add(InWaybill obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка добавить в хранилище полученный документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            #region Удаление существующей накладной.

            if (deleteExistingInWaybill)
            {
                InWaybill exist = inWaybills.FirstOrDefault(x => x.AuxCompositeId == obj.AuxCompositeId);

                if (exist != null)
                {
                    Program.Logger.Warn(this, string.Format("Объект '{0}' уже присутствует в хранилище. " +
                                                            "Будет произведена попытка замены существующего объекта на новый.",
                        exist.Description));

                    onlineEvents.Add(new OnlineEvent(OnlineEventStatus.Warning, this,
                        string.Format("Накладная с номером '{0}' ('{1}') от '{2}' заменена новой накладной, " +
                                      "имеющей такие же идентификационные реквизиты.", exist.Number, exist.Date,
                            exist.ShipperName)));

                    remove(exist);
                }
            }

            #endregion Удаление существующей накладной.

            inWaybills.Add(obj);

            storage.Save(obj);

            Program.Logger.Info(this, "Новый документ успешно добавлен.");
        }

        /// <summary>
        /// Удалить документ из хранилища.
        /// </summary>
        /// <param name="obj">Документ для удаления.</param>
        protected virtual void remove(InWaybill obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка удалить из хранилища документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            inWaybills.Remove(obj);

            storage.Delete(obj);

            Program.Logger.Info(this, "Документ успешно удалён из хранилища.");
        }

        /// <summary>
        /// Отправить акт.
        /// </summary>
        /// <param name="inWaybill">Входящая накладная.</param>
        /// <param name="isAccepted">Признак акта - подтверждение или отказ.</param>
        /// <param name="actNumber">Номер акта.</param>
        /// <param name="actComment">Комментарий к акту.</param>
        protected virtual void sendSingleAct(InWaybill inWaybill, bool isAccepted, string actNumber, string actComment)
        {
            if (inWaybill.StatusEnum != InWaybillStatus.New)
                throw new Exception("Статус накладной не позволяет отправить акт.");
            if (string.IsNullOrWhiteSpace(inWaybill.WBRegId))
                throw new Exception("Отсутствует идентификатор накладной по документу регистрации движения.");

            XmlDocument act = buildSingleAct(inWaybill, isAccepted, actNumber, actComment);

            #region Сохранение исходящего запроса.

            act.Save(string.Format("{0}\\{1}.simpleact.{2}.{3}", storage.PathOut, FileStorage.NativePrefix,
                DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));

            #endregion Сохранение исходящего запроса.

            var url = (documentsVersion == 1)
                ? addresses.SendWaybillAct
                : addresses.SendWaybillAct_v3(documentsVersion);
            Program.Logger.Info(this, string.Format("Попытка отправить данные '{0}' по адресу '{1}'...",
                act.OuterXml, url));

            string answer = HttpTransport.UploadFile(url,
                act, "xml_file", "text/xml; charset=utf-8", configuration.UtmTimeoutLong);

            Program.Logger.Info(this, "... данные успешно отправлены.");

            // Увы, таковы реалии ЕГАИС: идентификатор запроса возвращается в ноде "url".
            inWaybill.SetActReplyId(getNodeValueFromXMLAnswer(answer, "url"));

            inWaybill.AddAct(act.OuterXml);

            inWaybill.ChangeStatus(isAccepted ? InWaybillStatus.Accepted : InWaybillStatus.Rejected);

            inWaybill.AlreadyReading = true;

            storage.Save(inWaybill);
        }

        /// <summary>
        /// Сформировать акт.
        /// </summary>
        /// <param name="inWaybill">Входящая накладная.</param>
        /// <param name="isAccepted">Признак акта - подтверждение или отказ.</param>    
        /// <param name="actNumber">Номер акта.</param>
        /// <param name="actComment">Комментарий к акту.</param>
        /// <returns>XML-документ.</returns>
        protected virtual XmlDocument buildSingleAct(InWaybill inWaybill, bool isAccepted, string actNumber,
            string actComment)
        {
            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            XmlNode docsNode = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docsNode, "Version", "1.0");
            addAttribute(xml, docsNode, "xmlns:" + addresses.Xsi.Prefix, addresses.Xsi.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Ns.Prefix, addresses.Ns.Uri);
            if (inWaybill.VersionEgais < 3)
            {
                addAttribute(xml, docsNode, "xmlns:" + addresses.Oref.Prefix, addresses.Oref.Uri);
                addAttribute(xml, docsNode, "xmlns:" + addresses.Pref.Prefix, addresses.Pref.Uri);
            }

            XmlPrefix wa;
            switch (inWaybill.VersionEgais)
            {
                case 1:
                    wa = addresses.Wa;
                    addAttribute(xml, docsNode, "xmlns:" + wa.Prefix, wa.Uri);
                    break;
                case 2:
                    wa = addresses.Wa2;
                    addAttribute(xml, docsNode, "xmlns:" + wa.Prefix, wa.Uri);
                    break;
                case 3:
                    wa = addresses.Wa3;
                    addAttribute(xml, docsNode, "xmlns:" + wa.Prefix, wa.Uri);
                    var ce3 = addresses.Ce3;
                    addAttribute(xml, docsNode, "xmlns:" + ce3.Prefix, ce3.Uri);
                    break;
                default:
                    wa = addresses.Wa4;
                    addAttribute(xml, docsNode, "xmlns:" + wa.Prefix, wa.Uri);
                    var ce = addresses.Ce3;
                    addAttribute(xml, docsNode, "xmlns:" + ce.Prefix, ce.Uri);
                    break;
            }


            xml.AppendChild(docsNode);

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docsNode.AppendChild(owner);

            XmlNode FsrarId = xml.CreateElement(addresses.Ns.Prefix, "FSRAR_ID", addresses.Ns.Uri);
            FsrarId.InnerText = configuration.FsrarId;
            owner.AppendChild(FsrarId);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docsNode.AppendChild(doc);

            XmlNode waybillact = (inWaybill.VersionEgais == 1)
                ? xml.CreateElement(addresses.Ns.Prefix, "WayBillAct", addresses.Ns.Uri)
                : xml.CreateElement(addresses.Ns.Prefix, $"WayBillAct_v{inWaybill.VersionEgais}", addresses.Ns.Uri);
            doc.AppendChild(waybillact);

            XmlNode header = xml.CreateElement(wa.Prefix, "Header", wa.Uri);
            waybillact.AppendChild(header);

            XmlNode isAccept = xml.CreateElement(wa.Prefix, "IsAccept", wa.Uri);
            isAccept.InnerText = isAccepted ? "Accepted" : "Rejected";
            header.AppendChild(isAccept);

            XmlNode actnumber = xml.CreateElement(wa.Prefix, "ACTNUMBER", wa.Uri);
            actnumber.InnerText = actNumber;
            header.AppendChild(actnumber);

            XmlNode actdate = xml.CreateElement(wa.Prefix, "ActDate", wa.Uri);
            actdate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
            header.AppendChild(actdate);

            XmlNode wbregid = xml.CreateElement(wa.Prefix, "WBRegId", wa.Uri);
            wbregid.InnerText = inWaybill.WBRegId;
            header.AppendChild(wbregid);

            XmlNode note = xml.CreateElement(wa.Prefix, "Note", wa.Uri);
            note.InnerText = actComment;
            header.AppendChild(note);

            XmlNode content = xml.CreateElement(wa.Prefix, "Content", wa.Uri);
            waybillact.AppendChild(content);

            XmlNode transport = xml.CreateElement(wa.Prefix, "Transport", wa.Uri);
            waybillact.AppendChild(transport);

            


            return xml;
        }

        /// <summary>
        /// Отправить акт.
        /// </summary>
        /// <param name="inWaybill">Входящая накладная.</param>
        /// <param name="positions">Список позиций.</param>
        /// <param name="actNumber">Номер акта.</param>
        /// <param name="actComment">Комментарий к акту.</param>
        protected virtual void sendDifferenceAct(InWaybill inWaybill, List<Position> positions, string actNumber,
            string actComment)
        {
            if (inWaybill.StatusEnum != InWaybillStatus.New)
                throw new Exception("Статус накладной не позволяет отправить акт.");
            if (string.IsNullOrWhiteSpace(inWaybill.WBRegId))
                throw new Exception("Отсутствует идентификатор накладной по документу регистрации движения.");
            if (positions.Count == 0) throw new Exception("Отсутствуют позиции для акта.");

            XmlDocument act = buildDifferenceAct(inWaybill, positions, actNumber, actComment);

            #region Сохранение исходящего запроса.

            act.Save(string.Format("{0}\\{1}.differenceact.{2}.{3}", storage.PathOut, FileStorage.NativePrefix,
                DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));

            #endregion Сохранение исходящего запроса.

            var url = (inWaybill.VersionEgais == 1)
                ? addresses.SendWaybillAct
                : addresses.SendWaybillAct_v3(inWaybill.VersionEgais);
            Program.Logger.Info(this, string.Format("Попытка отправить данные '{0}' по адресу '{1}'...",
                act.OuterXml, url));

            string answer = HttpTransport.UploadFile(url,
                act, "xml_file", "text/xml; charset=utf-8", configuration.UtmTimeoutLong);

            Program.Logger.Info(this, "... данные успешно отправлены.");

            // Увы, таковы реалии ЕГАИС: идентификатор запроса возвращается в ноде "url".
            inWaybill.SetActReplyId(getNodeValueFromXMLAnswer(answer, "url"));

            inWaybill.AddAct(act.OuterXml);

            inWaybill.ChangeStatus(InWaybillStatus.Difference);

            inWaybill.AlreadyReading = true;

            storage.Save(inWaybill);
        }

        /// <summary>
        /// Сформировать акт.
        /// </summary>
        /// <param name="inWaybill">Входящая накладная.</param>
        /// <param name="positions">Признак акта - подтверждение или отказ.</param>
        /// <param name="actNumber">Номер акта.</param>
        /// <param name="actComment">Комментарий к акту.</param>
        /// <returns>XML-документ.</returns>
        protected virtual XmlDocument buildDifferenceAct(InWaybill inWaybill, IEnumerable<Position> positions,
            string actNumber, string actComment)
        {
            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            XmlNode docsNode = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docsNode, "Version", "1.0");
            addAttribute(xml, docsNode, "xmlns:" + addresses.Xsi.Prefix, addresses.Xsi.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Ns.Prefix, addresses.Ns.Uri);
            if (inWaybill.VersionEgais < 3)
            {
                addAttribute(xml, docsNode, "xmlns:" + addresses.Oref.Prefix, addresses.Oref.Uri);
                addAttribute(xml, docsNode, "xmlns:" + addresses.Pref.Prefix, addresses.Pref.Uri);
            }

            XmlPrefix wa;
            XmlPrefix ce3 = addresses.Ce3;
            switch (inWaybill.VersionEgais)
            {
                case 1:
                    wa = addresses.Wa;
                    addAttribute(xml, docsNode, "xmlns:" + wa.Prefix, wa.Uri);
                    break;
                case 2:
                    wa = addresses.Wa2;
                    addAttribute(xml, docsNode, "xmlns:" + wa.Prefix, wa.Uri);
                    break;
                case 3:
                    wa = addresses.Wa3;
                    addAttribute(xml, docsNode, "xmlns:" + wa.Prefix, wa.Uri);
                    addAttribute(xml, docsNode, "xmlns:" + ce3.Prefix, ce3.Uri);
                    
                    break;

                default:
                    wa = addresses.Wa4;
                    addAttribute(xml, docsNode, "xmlns:" + wa.Prefix, wa.Uri);
                    addAttribute(xml, docsNode, "xmlns:" + ce3.Prefix, ce3.Uri);
                    break;
            }

            xml.AppendChild(docsNode);

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docsNode.AppendChild(owner);

            XmlNode FsrarId = xml.CreateElement(addresses.Ns.Prefix, "FSRAR_ID", addresses.Ns.Uri);
            FsrarId.InnerText = configuration.FsrarId;
            owner.AppendChild(FsrarId);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docsNode.AppendChild(doc);

            XmlNode waybillact = (inWaybill.VersionEgais == 1)
                ? xml.CreateElement(addresses.Ns.Prefix, "WayBillAct", addresses.Ns.Uri)
                : xml.CreateElement(addresses.Ns.Prefix, $"WayBillAct_v{inWaybill.VersionEgais}", addresses.Ns.Uri);
            doc.AppendChild(waybillact);

            XmlNode header = xml.CreateElement(wa.Prefix, "Header", wa.Uri);
            waybillact.AppendChild(header);

            XmlNode isAccept = xml.CreateElement(wa.Prefix, "IsAccept", wa.Uri);
            isAccept.InnerText = "Differences";
            header.AppendChild(isAccept);

            XmlNode actnumber = xml.CreateElement(wa.Prefix, "ACTNUMBER", wa.Uri);
            actnumber.InnerText = actNumber;
            header.AppendChild(actnumber);

            XmlNode actdate = xml.CreateElement(wa.Prefix, "ActDate", wa.Uri);
            actdate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
            header.AppendChild(actdate);

            XmlNode wbregid = xml.CreateElement(wa.Prefix, "WBRegId", wa.Uri);
            wbregid.InnerText = inWaybill.WBRegId;
            header.AppendChild(wbregid);

            XmlNode note = xml.CreateElement(wa.Prefix, "Note", wa.Uri);
            note.InnerText = actComment;
            header.AppendChild(note);

            XmlNode content = xml.CreateElement(wa.Prefix, "Content", wa.Uri);
            waybillact.AppendChild(content);

            foreach (Position position in positions)
            {
                // В акте должны быть все позиции накладной.
                // if (position.Quantity == position.RealQuantity) continue;

                if (position.Quantity < position.RealQuantity)
                {
                    throw new Exception(
                        "Элемент 'Реальное количество' у алкогольной позиции не может быть больше элемента 'Количество'.\r\n\r\n" +
                        "Примечание.\r\n" +
                        "Актом расхождения в ЕГАИС может оформляться только недостача продукции. " +
                        "Излишки поставленной продукции оформляются отдельной товарно-транспортной накладной.");
                }

                XmlNode item = xml.CreateElement(wa.Prefix, "Position", wa.Uri);
                content.AppendChild(item);

                XmlNode itemIdentity = xml.CreateElement(wa.Prefix, "Identity", wa.Uri);
                itemIdentity.InnerText = position.Identity;
                item.AppendChild(itemIdentity);

                XmlNode itemBRegId = (inWaybill.VersionEgais == 1)
                    ? xml.CreateElement(wa.Prefix, "InformBRegId", wa.Uri)
                    : xml.CreateElement(wa.Prefix, "InformF2RegId", wa.Uri);
                itemBRegId.InnerText = position.InformBRegId;
                item.AppendChild(itemBRegId);

                XmlNode itemRealQuantity = xml.CreateElement(wa.Prefix, "RealQuantity", wa.Uri);
                itemRealQuantity.InnerText = position.RealQuantity.ToString(CultureInfo.InvariantCulture);
                item.AppendChild(itemRealQuantity);
                if (position.BoxInfos.Count > 0)
                {
                    XmlNode markInfo = xml.CreateElement(wa.Prefix, "MarkInfo", wa.Uri);

                    foreach (var boxInfo in position.BoxInfos)
                    {
                        foreach (var amc in boxInfo.AmcList.Where(amc => string.IsNullOrEmpty(amc.RealBarcode)))
                        {
                            XmlNode amcNode = xml.CreateElement(ce3.Prefix, "amc", ce3.Uri);
                            amcNode.InnerText = amc.Barcode;
                            markInfo.AppendChild(amcNode);
                        }
                    }

                    if (markInfo.HasChildNodes)
                        item.AppendChild(markInfo);
                }
            }

            XmlNode transport = xml.CreateElement(wa.Prefix, "Transport", wa.Uri);
            waybillact.AppendChild(transport);

            XmlNode changeOwnership = xml.CreateElement(wa.Prefix, "ChangeOwnership", wa.Uri);
            changeOwnership.InnerText = "NotChange";
            transport.AppendChild(changeOwnership);




           

           


            return xml;
        }

        /// <summary>
        /// Переотправить акт по накладной.
        /// </summary>
        /// <param name="inWaybill">Накладная.</param>
        protected virtual void resendAct(InWaybill inWaybill)
        {
            if (string.IsNullOrWhiteSpace(inWaybill.XmlAct))
            {
                throw new Exception("Повторная отправка применима только для ранее отправлявшихся документов.");
            }

            if (!inWaybill.XmlAct.StartsWith("<?xml "))
            {
                throw new Exception("К сожалению, для данной версии документа повторная отправка невозможна." +
                                    "\r\nЕсли повторная отправка крайне необходима, обратитесь в службу технической поддержки.");
            }

            if ((inWaybill.StatusEnum == InWaybillStatus.New)
                || (inWaybill.StatusEnum == InWaybillStatus.Partial))
            {
                throw new Exception("Переотправить акт можно только по тем накладных, " +
                                    "для которых уже был сформирован акт подтверждения или отказа.");
            }

            var url = (inWaybill.VersionEgais == 1)
                ? addresses.SendWaybillAct
                : addresses.SendWaybillAct_v3(inWaybill.VersionEgais);
            Program.Logger.Info(this, string.Format("Попытка переотправить данные '{0}' по адресу '{1}'...",
                inWaybill.XmlAct, url));

            HttpTransport.UploadFile(url,
                Encoding.UTF8.GetBytes(inWaybill.XmlAct), "xml_file", "text/xml; charset=utf-8",
                configuration.UtmTimeoutLong);

            Program.Logger.Info(this, "... данные успешно переотправлены.");
        }

        /// <summary>
        /// Распроведение входящей накладной.
        /// </summary>
        /// <param name="inWaybill">Накладная.</param>
        protected virtual void repeal(InWaybill inWaybill)
        {
            if ((inWaybill.StatusEnum != InWaybillStatus.Accepted)
                && (inWaybill.StatusEnum != InWaybillStatus.Rejected)
                && (inWaybill.StatusEnum != InWaybillStatus.Difference))
            {
                throw new Exception("Отменить можно только ранее подтверждённую накладную.");
            }

            XmlDocument xmlOutDocument = buildRepealXmlDocument(inWaybill);

            #region Сохранение исходящего запроса.

            xmlOutDocument.Save(string.Format("{0}\\{1}.repealinwaybill.{2}.{3}", storage.PathOut,
                FileStorage.NativePrefix, DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));

            #endregion Сохранение исходящего запроса.

            Program.Logger.Info(this,
                string.Format("Попытка отправить данные '{0}' по адресу '{1}'...", xmlOutDocument.OuterXml,
                    addresses.RequestRepealWaybill));

            string answer = HttpTransport.UploadFile(addresses.RequestRepealWaybill, xmlOutDocument, "xml_file",
                "text/xml; charset=utf-8", configuration.UtmTimeoutLong);

            Program.Logger.Info(this, string.Format("... данные успешно отправлены; получен ответ '{0}'.", answer));

            // Увы, таковы реалии ЕГАИС: идентификатор запроса возвращается в ноде "url".
            inWaybill.SetRepealReplyId(getNodeValueFromXMLAnswer(answer, "url"));

            inWaybill.AddRepealData(xmlOutDocument);
            inWaybill.ChangeStatusOnRepeal(InWaybillStatus.Repealed);

            storage.Save(inWaybill);
        }

        /// <summary>
        /// Построить запрос на отмену проведения входящей накладной.
        /// </summary>
        /// <param name="inWaybill">Накладная.</param>
        protected virtual XmlDocument buildRepealXmlDocument(InWaybill inWaybill)
        {
            Program.Logger.Info(this,
                string.Format("Попытка построить исходящий xml-документ распроведения для накладной '{0}'...",
                    inWaybill.Description));

            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            #region Схемы...

            XmlNode docs = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docs, "Version", "1.0");
            addAttribute(xml, docs, "xmlns:" + addresses.Xsi.Prefix, addresses.Xsi.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.Ns.Prefix, addresses.Ns.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.QpRepealWB.Prefix, addresses.QpRepealWB.Uri);
            xml.AppendChild(docs);

            #endregion Схемы...

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docs.AppendChild(owner);

            addNodeToXmlDocument(xml, owner, addresses.Ns, "FSRAR_ID", configuration.FsrarId);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docs.AppendChild(doc);

            XmlNode requestRepealWB = xml.CreateElement(addresses.Ns.Prefix, "RequestRepealWB", addresses.Ns.Uri);
            doc.AppendChild(requestRepealWB);

            addNodeToXmlDocument(xml, requestRepealWB, addresses.QpRepealWB, "ClientId", configuration.FsrarId);
            addNodeToXmlDocument(xml, requestRepealWB, addresses.QpRepealWB, "RequestNumber",
                string.Format("RepealWB-{0}", DateTime.Now.ToString("yyMMddHHmmss")));
            addNodeToXmlDocument(xml, requestRepealWB, addresses.QpRepealWB, "RequestDate",
                DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));
            addNodeToXmlDocument(xml, requestRepealWB, addresses.QpRepealWB, "WBRegId", inWaybill.WBRegId);

            Program.Logger.Info(this,
                string.Format("... построение исходящего xml-документа длиной {0} символов успешно завершено.",
                    xml.OuterXml.Length));

            return xml;
        }

        /// <summary>
        /// Возврат накладной к предыдущему состоянию после попытки отправки.
        /// </summary>
        /// <param name="replyId">Идентификатор запроса.</param>
        /// <param name="conclusionComment">Комментарий к откату.</param>
        protected virtual void backoffInWaybill(string replyId, string conclusionComment)
        {
            try
            {
                Program.Logger.Info(this,
                    string.Format("Попытка отката входящих накладных по идентификатору '{0}'...", replyId));

                if (string.IsNullOrWhiteSpace(replyId))
                    throw new Exception("Идентификатор операции отката не может быть пустым.");

                InWaybill waybill = inWaybills.FirstOrDefault(item => (((item.StatusEnum == InWaybillStatus.Accepted)
                                                                        || (item.StatusEnum == InWaybillStatus.Rejected)
                                                                        || (item.StatusEnum ==
                                                                            InWaybillStatus.Difference))
                                                                       && (item.ActReplyId == replyId)));

                if (waybill != null)
                {
                    Program.Logger.Info(this,
                        string.Format("... найдена входящая накладная для отката: '{0}'...", waybill.Description));

                    waybill.ChangeStatus(InWaybillStatus.New);
                    waybill.SetActReplyId(string.Empty);
                    waybill.SetAdditionalComment(conclusionComment);

                    inWaybills.ForceChangeListEvent();

                    storage.Save(waybill);

                    Program.Logger.Info(this,
                        string.Format("... входящая накладная '{0}' откачена и сохранена...", waybill.Description));

                    onlineEvents.Add(new OnlineEvent(OnlineEventStatus.Warning, this,
                        string.Format(
                            "Отправление акта по входящей накладной с номером '{0}' ('{1}') от '{2}' было отвергнуто сервером ЕГАИС. Причина отказа предоставлена в соответствующей квитанции сервера (смотрите окно 'Список квитанций-ответов сервера').",
                            waybill.Number, waybill.Date, waybill.ShipperName)));
                }
                else
                {
                    Program.Logger.Info(this, "... документ для отката не найден...");
                }

                Program.Logger.Info(this,
                    string.Format("... попытка отката входящих накладных по идентификатору '{0}' успешно завершена.",
                        replyId));
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время проведения операции отката входящей накладной произошла ошибка.",
                    exception);

                throw;
            }
        }

        /// <summary>
        /// Указать, что накладная "распроведена".
        /// </summary>
        /// <param name="regId">Идентификатор ЕГАИС для накладной.</param>
        /// <param name="conclusion">Результат операции.</param>
        /// <param name="comment">Комментарий.</param>
        protected virtual void unconfirmInWaybill(string regId, string conclusion, string comment)
        {
            try
            {
                Program.Logger.Info(this,
                    string.Format("Попытка распроведения входящих накладных по идентификатору '{0}'...", regId));

                if (string.IsNullOrWhiteSpace(regId))
                    throw new Exception("Идентификатор операции распроведения не может быть пустым.");

                InWaybill waybill = inWaybills.FirstOrDefault(item =>
                    ((item.StatusEnum != InWaybillStatus.Partial) && (item.WBRegId == regId)));

                if (waybill != null)
                {
                    Program.Logger.Info(this,
                        string.Format("... найдена входящая накладная для распроведения: '{0}'...",
                            waybill.Description));

                    waybill.ChangeStatus((waybill.StatusEnum == InWaybillStatus.Repealed)
                        ? waybill.PrevRepealStatus
                        : InWaybillStatus.Repealed);

                    waybill.SetAdditionalComment(comment);

                    inWaybills.ForceChangeListEvent();

                    storage.Save(waybill);

                    Program.Logger.Info(this,
                        string.Format("... входящая накладная '{0}' распроведена и сохранена...", waybill.Description));

                    onlineEvents.Add(new OnlineEvent(OnlineEventStatus.Warning, this,
                        string.Format(
                            "Входящая накладная с номером '{0}' ('{1}') для '{2}' была распроведена. Причина отказа предоставлена в соответствующей квитанции сервера (смотрите окно 'Список квитанций-ответов сервера').",
                            waybill.Number, waybill.Date, waybill.ConsigneeName)));
                }
                else
                {
                    Program.Logger.Info(this, "... документ для распроведения не найден...");
                }

                Program.Logger.Info(this,
                    string.Format(
                        "... попытка распроведения входящих накладных по идентификатору '{0}' успешно завершена.",
                        regId));
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время проведения операции распроведения входящей накладной произошла ошибка.",
                    exception);

                throw;
            }
        }

        /// <summary>
        /// Добавить комментарий в накладную.
        /// </summary>
        /// <param name="regId">Идентификатор ЕГАИС для накладной.</param>
        /// <param name="comment">Комментарий.</param>
        protected virtual void addCommentToInWaybill(string regId, string comment)
        {
            try
            {
                Program.Logger.Info(this,
                    string.Format("Попытка добавить комментарий в накладную по идентификатору '{0}'...", regId));

                if (string.IsNullOrWhiteSpace(regId))
                    throw new Exception("Идентификатор операции не может быть пустым.");

                InWaybill waybill = inWaybills.FirstOrDefault(item =>
                    ((item.StatusEnum != InWaybillStatus.Partial) && (item.WBRegId == regId)));

                if (waybill != null)
                {
                    Program.Logger.Info(this,
                        string.Format("... найдена входящая накладная для добавления комментария: '{0}'...",
                            waybill.Description));

                    // if (waybill.StatusEnum == InWaybillStatus.Repealed)
                    // {
                    //     waybill.ChangeStatus(waybill.PrevRepealStatus);
                    // }

                    waybill.SetAdditionalComment(comment);

                    inWaybills.ForceChangeListEvent();

                    storage.Save(waybill);

                    Program.Logger.Info(this,
                        string.Format("... входящая накладная '{0}' откомментирована и сохранена...",
                            waybill.Description));

                    onlineEvents.Add(new OnlineEvent(OnlineEventStatus.Warning, this,
                        string.Format(
                            "Во входящую накладную с номером '{0}' ('{1}') для '{2}' был добавлен комментарий.",
                            waybill.Number, waybill.Date, waybill.ConsigneeName)));
                }
                else
                {
                    Program.Logger.Info(this, "... документ не найден...");
                }

                Program.Logger.Info(this,
                    string.Format(
                        "... попытка добавления комментария во входящую накладную по идентификатору '{0}' успешно завершена.",
                        regId));
            }
            catch (Exception exception)
            {
                Program.Logger.Error(
                    "Во время проведения добавления комментария во входящую накладную произошла ошибка.", exception);

                throw;
            }
        }

        /// <summary>
        /// Обработка распроведения накладной.
        /// </summary>
        /// <param name="replyId">Идентификатор запроса.</param>
        /// <param name="conclusion">Заключение.</param>
        /// <param name="conclusionComment">Комментарий.</param>
        protected virtual void repealInWaybill(string replyId, string conclusion, string conclusionComment)
        {
            try
            {
                Program.Logger.Info(this,
                    string.Format(
                        "Попытка обработки операции распроведения входящих накладных по идентификатору запроса '{0}'...",
                        replyId));

                if (string.IsNullOrWhiteSpace(replyId))
                    throw new Exception("Идентификатор операции отката не может быть пустым.");

                InWaybill waybill = inWaybills.FirstOrDefault(item => ((item.StatusEnum == InWaybillStatus.Repealed)
                                                                       && (item.RepealReplyId == replyId)));

                if (waybill != null)
                {
                    Program.Logger.Info(this,
                        string.Format("... найдена входящая накладная для обработки операции распроведения: '{0}'...",
                            waybill.Description));

                    if (conclusion.ToLower() == "rejected")
                    {
                        waybill.ChangeStatus(waybill.PrevRepealStatus);
                        waybill.SetAdditionalComment(conclusionComment);

                        Program.Logger.Info(this,
                            string.Format("\t... входящей накладной '{0}' возвращён предыдущий статус...",
                                waybill.Description));
                    }

                    inWaybills.ForceChangeListEvent();

                    storage.Save(waybill);

                    onlineEvents.Add(new OnlineEvent(OnlineEventStatus.Warning, this,
                        string.Format(
                            "Запрос на отмену проведения акта по входящей накладной с номером '{0}' ('{1}') от '{2}' был отвергнут сервером ЕГАИС. Причина отказа предоставлена в соответствующей квитанции сервера (смотрите окно 'Список квитанций-ответов сервера').",
                            waybill.Number, waybill.Date, waybill.ShipperName)));
                }
                else
                {
                    Program.Logger.Info(this, "... документ для отката не найден...");
                }

                Program.Logger.Info(this,
                    string.Format(
                        "... попытка обработки операции распроведения входящих накладных по идентификатору '{0}' успешно завершена.",
                        replyId));
            }
            catch (Exception exception)
            {
                Program.Logger.Error(
                    "Во время проведения операции обработки распроведения входящей накладной произошла ошибка.",
                    exception);

                throw;
            }
        }

        /// <summary>
        /// Откат распроведённой накладной.
        /// </summary>
        /// <param name="confirm">Подтверждение.</param>
        protected virtual void backoffRepealedInwaybill(WaybillRepealConfirm confirm)
        {
            try
            {
                Program.Logger.Info(this,
                    string.Format("Попытка отката распроведения входящих накладных по подтверждению '{0}'...",
                        confirm));

                if (confirm == null) throw new Exception("Подтверждение отката не может быть пустым.");

                InWaybill waybill = inWaybills.FirstOrDefault(item => ((item.StatusEnum == InWaybillStatus.Repealed)
                                                                       && (item.WBRegId == confirm.WBRegId)));

                if (waybill != null)
                {
                    Program.Logger.Info(this,
                        string.Format("... найдена входящая накладная для обработки операции распроведения: '{0}'...",
                            waybill.Description));

                    if (confirm.IsConfirm.ToLower() == "rejected")
                    {
                        waybill.ChangeStatus(waybill.PrevRepealStatus);
                        waybill.SetAdditionalComment(string.Format("Запрос на распроведение отвергнут. " +
                                                                   "Номер ответа: '{0}', " +
                                                                   "дата: '{1}', " +
                                                                   "комментарий: '{2}'.", confirm.ConfirmNumber,
                            confirm.ConfirmDate, confirm.Note));

                        Program.Logger.Info(this,
                            string.Format("\t... входящей накладной '{0}' возвращён предыдущий статус...",
                                waybill.Description));
                    }

                    inWaybills.ForceChangeListEvent();

                    storage.Save(waybill);

                    onlineEvents.Add(new OnlineEvent(OnlineEventStatus.Warning, this,
                        string.Format(
                            "Получен отказ на запрос распроведения входящей накладной с номером '{0}' ('{1}') от '{2}' был отвергнут сервером ЕГАИС. Причина отказа предоставлена в соответствующей квитанции сервера (смотрите окно 'Список квитанций-ответов сервера').",
                            waybill.Number, waybill.Date, waybill.ShipperName)));
                }
                else
                {
                    Program.Logger.Info(this, "... документ для отката не найден...");
                }

                Program.Logger.Info(this,
                    string.Format(
                        "... отката распроведения входящих накладных по подтверждению '{0}' успешно завершена.",
                        confirm));
            }
            catch (Exception exception)
            {
                Program.Logger.Error(
                    "Во время проведения операции обработки распроведения входящей накладной произошла ошибка.",
                    exception);

                throw;
            }
        }

        #endregion InWaybill...

        #region NotAnswer...

        /// <summary>
        /// Получить список необработанных накладных.
        /// </summary>
        /// <param name="url">Адрес для загрузки.</param>
        /// <param name="replyId">Идентификатор исходного запроса.</param>
        /// <param name="xml">Ответ сервера.</param>
        protected virtual void getNotAnswer(string url, string replyId, XmlDocument xml)
        {
            if ((xml.DocumentElement == null)
                || (xml.DocumentElement["ns:Document"] == null)
                || (xml.DocumentElement["ns:Document"]["ns:ReplyNoAnswerTTN"] == null))
                throw new Exception("Необходимые XML-ноды в документе пустые.");

            NotAnswer notAnswer = new NotAnswer(xml.DocumentElement["ns:Document"]["ns:ReplyNoAnswerTTN"])
            {
                ReplyId = replyId,
                Url = url,
            };

            add(notAnswer);
        }

        /// <summary>
        /// Добавить новый документ.
        /// </summary>
        /// <param name="obj">Новый документ.</param>
        protected virtual void add(NotAnswer obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка добавить в хранилище полученный документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            NotAnswer exist = notAnswerList.FirstOrDefault(x => x.ReplyId == obj.ReplyId);

            if (exist != null)
            {
                Program.Logger.Warn(this,
                    string.Format(
                        "Объект '{0}' уже присутствует в хранилище. Будет произведена попытка замены существующего объекта на новый.",
                        exist.Description));

                remove(exist);
            }

            notAnswerList.Add(obj);

            storage.Save(obj);

            Program.Logger.Info(this, "Новый документ успешно добавлен.");
        }

        /// <summary>
        /// Удалить документ из хранилища.
        /// </summary>
        /// <param name="obj">Документ для удаления.</param>
        protected virtual void remove(NotAnswer obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка удалить из хранилища документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            notAnswerList.Remove(obj);

            storage.Delete(obj);

            Program.Logger.Info(this, "Документ успешно удалён из хранилища.");
        }

        /// <summary>
        /// Отправить запрос на получение остатков.
        /// </summary>
        protected virtual void requestNotAnswer()
        {
            XmlDocument xmlDocument = getNotAnswerData();

            #region Сохранение исходящего запроса.

            xmlDocument.Save(string.Format("{0}\\{1}.requestnotanswer.{2}.{3}", storage.PathOut,
                FileStorage.NativePrefix, DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));

            #endregion Сохранение исходящего запроса.

            Program.Logger.Info(this,
                string.Format("Попытка отправить данные '{0}' по адресу '{1}'...", xmlDocument.OuterXml,
                    addresses.RequestNotAnswer));

            HttpTransport.UploadFile(addresses.RequestNotAnswer, xmlDocument, "xml_file", "text/xml; charset=utf-8",
                configuration.UtmTimeoutLong);

            Program.Logger.Info(this, "... данные успешно отправлены.");
        }

        /// <summary>
        /// Сформировать xml-документ, представляющий запрос остатков.
        /// </summary>
        /// <returns></returns>
        protected virtual XmlDocument getNotAnswerData()
        {
            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            XmlNode docsNode = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docsNode, "Version", "1.0");
            addAttribute(xml, docsNode, "xmlns:" + addresses.Xsi.Prefix, addresses.Xsi.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Ns.Prefix, addresses.Ns.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Qp.Prefix, addresses.Qp.Uri);
            xml.AppendChild(docsNode);

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docsNode.AppendChild(owner);

            XmlNode FsrarId = xml.CreateElement(addresses.Ns.Prefix, "FSRAR_ID", addresses.Ns.Uri);
            FsrarId.InnerText = configuration.FsrarId;
            owner.AppendChild(FsrarId);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docsNode.AppendChild(doc);

            XmlNode queryNA = xml.CreateElement(addresses.Ns.Prefix, "QueryNATTN", addresses.Ns.Uri);
            doc.AppendChild(queryNA);

            XmlNode parameters = xml.CreateElement(addresses.Qp.Prefix, "Parameters", addresses.Qp.Uri);
            queryNA.AppendChild(parameters);

            XmlNode parameter = xml.CreateElement(addresses.Qp.Prefix, "Parameter", addresses.Qp.Uri);
            parameters.AppendChild(parameter);

            XmlNode name = xml.CreateElement(addresses.Qp.Prefix, "Name", addresses.Qp.Uri);
            name.InnerText = "КОД";
            parameter.AppendChild(name);

            XmlNode value = xml.CreateElement(addresses.Qp.Prefix, "Value", addresses.Qp.Uri);
            value.InnerText = configuration.FsrarId;
            parameter.AppendChild(value);

            return xml;
        }

        #endregion NotAnswer...

        #region FormBRegInfo...

        /// <summary>
        /// Получить документ регистрации движения.
        /// </summary>
        /// <param name="url">Адрес для загрузки.</param>
        /// <param name="replyId">Идентификатор исходного запроса.</param>
        /// <param name="version">Версия документа.</param>
        /// <param name="xml">Ответ сервера.</param>
        protected virtual void getFormBRegId(string url, string replyId, XmlDocument xml, int version = 1)
        {
            string nameInformNode = (version == 1) ? "ns:TTNInformBReg" : "ns:TTNInformF2Reg";

            if ((xml.DocumentElement == null)
                || (xml.DocumentElement["ns:Document"] == null)
                || (xml.DocumentElement["ns:Document"][nameInformNode] == null))
                throw new Exception("Необходимые XML-ноды в документе пустые.");

            FormBRegInfo info = new FormBRegInfo(xml.DocumentElement["ns:Document"][nameInformNode], version)
            {
                ReplyId = replyId,
                Url = url
            };

            add(info);
        }

        /// <summary>
        /// Добавить новый документ.
        /// </summary>
        /// <param name="obj">Новый документ.</param>
        protected virtual void add(FormBRegInfo obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка добавить в хранилище полученный документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            // В реалиях ЕГАИС вполне возможен приход двух одинаковых документов регистрации движения.
            // Например, если накладная отправлена самому себе.
            // FormBRegInfo exist = formBRegInfos.FirstOrDefault(x => ((x.WBRegId == obj.WBRegId) && (x.AuxCompositeId == obj.AuxCompositeId)));
            // 
            // if (exist != null)
            // {
            //     Program.Logger.Warn(this, string.Format("Объект '{0}' уже присутствует в хранилище. Будет произведена попытка замены существующего объекта на новый.", exist.Description));
            // 
            //     remove(exist);
            // }

            associate(obj);

            formBRegInfos.Add(obj);

            storage.Save(obj);

            Program.Logger.Info(this, "Новый документ успешно добавлен.");
        }

        /// <summary>
        /// Ассоциировать (по возможности) документ о регистрации движения с накладной.
        /// </summary>
        /// <param name="info">Документ регистрации движения.</param>
        /// <param name="suppressException">Подавить исключение.</param>
        protected virtual void associate(FormBRegInfo info, bool suppressException = true)
        {
            try
            {
                Program.Logger.Info(this,
                    string.Format("Попытка ассоциировать документ регистрации движения '{0}' с накладной...",
                        info.Description));

                if (!info.IsFreeDocument)
                    throw new Exception(
                        "Нельзя повторно ассоциировать уже ассоциированный документ регистрации движения.");

                #region Входящие накладные...

                {
                    InWaybill inWaybill = inWaybills.FirstOrDefault(item =>
                        ((item.AuxCompositeId == info.AuxCompositeId) && (item.StatusEnum == InWaybillStatus.Partial)));

                    if (inWaybill != null)
                    {
                        inWaybill.AddData(info);
                        info.SetNonFreeStatus();

                        storage.Save(inWaybill);
                        storage.Save(info);

                        inWaybills.ForceChangeListEvent();

                        onlineEvents.Add(new OnlineEvent(this, string.Format(
                            "Накладная с номером '{0}' ('{1}') от '{2}' дополнена документом регистрации движения.",
                            inWaybill.Number, inWaybill.Date, inWaybill.ShipperName)));

                        Program.Logger.Info(this, string.Format(
                            "... документ регистрации движения успешно ассоциирован с накладной '{0}'...",
                            inWaybill.Description));
                    }
                    else
                    {
                        Program.Logger.Info(this, "\t... нужная входящая накладная не найдена...");
                    }
                }

                #endregion Входящие накладные...

                #region Исходящие накладные...

                {
                    OutWaybill outWaybill = outWaybills.FirstOrDefault(item =>
                        ((item.AuxCompositeId == info.AuxCompositeId) && (item.StatusEnum == OutWaybillStatus.Sent)));

                    if (outWaybill != null)
                    {
                        outWaybill.AddData(info);
                        info.SetNonFreeStatus();

                        storage.Save(outWaybill);
                        storage.Save(info);

                        outWaybills.ForceChangeListEvent();

                        onlineEvents.Add(new OnlineEvent(this, string.Format(
                            "Накладная с номером '{0}' ('{1}') для '{2}'  дополнена документом регистрации движения.",
                            outWaybill.Number, outWaybill.Date, outWaybill.ConsigneeName)));

                        Program.Logger.Info(this, string.Format(
                            "... документ регистрации движения успешно ассоциирован с накладной '{0}'...",
                            outWaybill.Description));
                    }
                    else
                    {
                        Program.Logger.Info(this, "\t... нужная исходящая накладная не найдена...");
                    }
                }

                #endregion Исходящие накладные...

                Program.Logger.Info(this,
                    "... попытка ассоциировать документ регистрации движения с накладной завершена.");
            }
            catch (Exception exception)
            {
                onlineEvents.Add(new OnlineEvent(OnlineEventStatus.Error, this,
                    string.Format(
                        "Во время ассоциирования документа регистрации движения '{0}' произошла ошибка: '{1}'.",
                        info.Description, exception.Message)));

                Program.Logger.Error("Во время ассоциирования документа регистрации движения произошла ошибка.",
                    exception);

                if (!suppressException) throw;
            }
        }

        /// <summary>
        /// Удалить документ из хранилища.
        /// </summary>
        /// <param name="obj">Документ для удаления.</param>
        protected virtual void remove(FormBRegInfo obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка удалить из хранилища документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            formBRegInfos.Remove(obj);

            storage.Delete(obj);

            Program.Logger.Info(this, "Документ успешно удалён из хранилища.");
        }

        #endregion FormBRegInfo...

        #region InventoryBRegInfo...

        /// <summary>
        /// Получить документ регистрации движения.
        /// </summary>
        /// <param name="url">Адрес для загрузки.</param>
        /// <param name="replyId">Идентификатор исходного запроса.</param>
        /// <param name="xml">Ответ сервера.</param>
        protected virtual void getInventoryBRegInfo(string url, string replyId, XmlDocument xml)
        {
            if ((xml.DocumentElement == null)
                || (xml.DocumentElement["ns:Document"] == null))
                throw new Exception("Необходимые XML-ноды в документе пустые.");

            if ((xml.DocumentElement["ns:Document"]["ns:ActInventoryInformBReg"] == null)
                && (xml.DocumentElement["ns:Document"]["ns:ActInventoryInformF2Reg"] == null))
                throw new Exception(
                    "Необходимые XML-ноды ('ns:ActInventoryInformBReg' и 'ns:ActInventoryInformF2Reg') в документе пустые.");

            string nodeName = (xml.DocumentElement["ns:Document"]["ns:ActInventoryInformBReg"] == null)
                ? "ns:ActInventoryInformF2Reg"
                : "ns:ActInventoryInformBReg";

            InventoryBRegInfo info = new InventoryBRegInfo(xml.DocumentElement["ns:Document"][nodeName])
                {Url = url, ReplyId = replyId};

            add(info);
        }

        /// <summary>
        /// Добавить новый документ.
        /// </summary>
        /// <param name="obj">Новый документ.</param>
        protected virtual void add(InventoryBRegInfo obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка добавить в хранилище полученный документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            InventoryBRegInfo exist = inventoryBRegInfoList.FirstOrDefault(x => x.ReplyId == obj.ReplyId);

            if (exist != null)
            {
                Program.Logger.Warn(this,
                    string.Format(
                        "Объект '{0}' уже присутствует в хранилище. Будет произведена попытка замены существующего объекта на новый.",
                        exist.Description));

                remove(exist);
            }

            associate(obj);

            inventoryBRegInfoList.Add(obj);

            storage.Save(obj);

            Program.Logger.Info(this, "Новый документ успешно добавлен.");
        }

        /// <summary>
        /// Ассоциировать (по возможности) уведомление о постановке на баланс продукции с актом.
        /// </summary>
        /// <param name="info">Уведомление о постановке на баланс продукции.</param>
        /// <param name="suppressException">Подавить исключение.</param>
        protected virtual void associate(InventoryBRegInfo info, bool suppressException = true)
        {
            try
            {
                Program.Logger.Info(this,
                    string.Format("Попытка ассоциировать уведомление о постановке на баланс продукции '{0}' с актом...",
                        info.Description));

                if (!info.IsFreeDocument)
                    throw new Exception(
                        "Нельзя повторно ассоциировать уже ассоциированное уведомление о постановке на баланс продукции.");

                ActChargeOn act = actChargeOnList.FirstOrDefault(item => ((item.SendReplyId == info.ReplyId)
                                                                          && ((item.StatusEnum ==
                                                                               MovementStatus.Confirmed)
                                                                              || (item.StatusEnum == MovementStatus.Sent
                                                                              ))));

                if (act != null)
                {
                    act.AddData(info);
                    info.SetNonFreeStatus();

                    storage.Save(act);
                    storage.Save(info);

                    actChargeOnList.ForceChangeListEvent();

                    onlineEvents.Add(new OnlineEvent(this,
                        string.Format(
                            "Акт постановки на баланс с номером '{0}' за '{1}' дополнен уведомлением от ЕГАИС.",
                            act.Number, act.Date)));

                    Program.Logger.Info(this,
                        string.Format("... документ регистрации движения успешно ассоциирован с актом '{0}'...",
                            act.Description));
                }
                else
                {
                    Program.Logger.Info(this, "... нужный акт постановки на баланс не найден...");
                }

                Program.Logger.Info(this,
                    "... попытка ассоциировать уведомление о постановке на баланс продукции завершена.");
            }
            catch (Exception exception)
            {
                onlineEvents.Add(new OnlineEvent(OnlineEventStatus.Error, this,
                    string.Format(
                        "Во время ассоциирования уведомления о постановке на баланс продукции '{0}' произошла ошибка: '{1}'.",
                        info.Description, exception.Message)));

                Program.Logger.Error(
                    "Во время ассоциирования уведомления о постановке на баланс продукции произошла ошибка.",
                    exception);

                if (!suppressException) throw;
            }
        }

        /// <summary>
        /// Удалить документ из хранилища.
        /// </summary>
        /// <param name="obj">Документ для удаления.</param>
        protected virtual void remove(InventoryBRegInfo obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка удалить из хранилища документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            inventoryBRegInfoList.Remove(obj);

            storage.Delete(obj);

            Program.Logger.Info(this, "Документ успешно удалён из хранилища.");
        }

        #endregion InventoryBRegInfo...

        #region FormA...

        /// <summary>
        /// Получить форму 'А'.
        /// </summary>
        /// <param name="url">Адрес для загрузки.</param>
        /// <param name="replyId">Идентификатор исходного запроса.</param>
        /// <param name="version">Версия документа.</param>
        /// <param name="xml">Ответ сервера.</param>
        protected virtual void getFormA(string url, string replyId, XmlDocument xml, int version = 1)
        {
            string nameReplyForm = (version == 1) ? "ns:ReplyFormA" : "ns:ReplyForm1";

            if ((xml.DocumentElement == null)
                || (xml.DocumentElement["ns:Document"] == null)
                || (xml.DocumentElement["ns:Document"][nameReplyForm] == null))
                throw new Exception("Необходимые XML-ноды в документе пустые.");

            FormA formA = new FormA(xml.DocumentElement["ns:Document"][nameReplyForm], version)
            {
                ReplyId = replyId,
                Url = url
            };

            add(formA);
        }

        /// <summary>
        /// Добавить новый документ.
        /// </summary>
        /// <param name="obj">Новый документ.</param>
        protected virtual void add(FormA obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка добавить в хранилище полученный документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            FormA exist = formAList.FirstOrDefault(x => x.ReplyId == obj.ReplyId);

            if (exist != null)
            {
                Program.Logger.Warn(this,
                    string.Format(
                        "Объект '{0}' уже присутствует в хранилище. Будет произведена попытка замены существующего объекта на новый.",
                        exist.Description));

                remove(exist);
            }

            formAList.Add(obj);

            storage.Save(obj);

            Program.Logger.Info(this, "Новый документ успешно добавлен.");
        }

        /// <summary>
        /// Удалить документ из хранилища.
        /// </summary>
        /// <param name="obj">Документ для удаления.</param>
        protected virtual void remove(FormA obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка удалить из хранилища документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            formAList.Remove(obj);

            storage.Delete(obj);

            Program.Logger.Info(this, "Документ успешно удалён из хранилища.");
        }

        #endregion FormA...

        #region FormB...

        /// <summary>
        /// Получить форму 'Б'.
        /// </summary>
        /// <param name="url">Адрес для загрузки.</param>
        /// <param name="replyId">Идентификатор исходного запроса.</param>
        /// <param name="version">Версия документа.</param>
        /// <param name="xml">Ответ сервера.</param>
        protected virtual void getFormB(string url, string replyId, XmlDocument xml, int version = 1)
        {
            string nameReplyForm = (version == 1) ? "ns:ReplyFormB" : "ns:ReplyForm2";

            if ((xml.DocumentElement == null)
                || (xml.DocumentElement["ns:Document"] == null)
                || (xml.DocumentElement["ns:Document"][nameReplyForm] == null))
                throw new Exception("Необходимые XML-ноды в документе пустые.");

            FormB formB = new FormB(xml.DocumentElement["ns:Document"][nameReplyForm], version)
            {
                ReplyId = replyId,
                Url = url
            };

            add(formB);
        }

        /// <summary>
        /// Добавить новый документ.
        /// </summary>
        /// <param name="obj">Новый документ.</param>
        protected virtual void add(FormB obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка добавить в хранилище полученный документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            FormB exist = formBList.FirstOrDefault(x => x.ReplyId == obj.ReplyId);

            if (exist != null)
            {
                Program.Logger.Warn(this,
                    string.Format(
                        "Объект '{0}' уже присутствует в хранилище. Будет произведена попытка замены существующего объекта на новый.",
                        exist.Description));

                remove(exist);
            }

            formBList.Add(obj);

            storage.Save(obj);

            Program.Logger.Info(this, "Новый документ успешно добавлен.");
        }

        /// <summary>
        /// Удалить документ из хранилища.
        /// </summary>
        /// <param name="obj">Документ для удаления.</param>
        protected virtual void remove(FormB obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка удалить из хранилища документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            formBList.Remove(obj);

            storage.Delete(obj);

            Program.Logger.Info(this, "Документ успешно удалён из хранилища.");
        }

        #endregion FormB...

        #region HistoryFormB...

        /// <summary>
        /// Получить движение по форме 'Б'.
        /// </summary>
        /// <param name="url">Адрес для загрузки.</param>
        /// <param name="replyId">Идентификатор исходного запроса.</param>
        /// <param name="xml">Ответ сервера.</param>
        /// <param name="version">Версия документа.</param>
        protected virtual void getHistoryFormB(string url, string replyId, XmlDocument xml, int version = 1)
        {
            string nameReplyHistory = (version == 1) ? "ns:ReplyHistFormB" : "ns:ReplyHistForm2";

            if ((xml.DocumentElement == null)
                || (xml.DocumentElement["ns:Document"] == null)
                || (xml.DocumentElement["ns:Document"][nameReplyHistory] == null))
                throw new Exception("Необходимые XML-ноды в документе пустые.");

            HistoryFormB historyFormB = new HistoryFormB(xml.DocumentElement["ns:Document"][nameReplyHistory], version)
            {
                ReplyId = replyId,
                Url = url,
            };

            add(historyFormB);
        }

        /// <summary>
        /// Добавить новый документ.
        /// </summary>
        /// <param name="obj">Новый документ.</param>
        protected virtual void add(HistoryFormB obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка добавить в хранилище полученный документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            HistoryFormB exist = historyFormBList.FirstOrDefault(x => x.ReplyId == obj.ReplyId);

            if (exist != null)
            {
                Program.Logger.Warn(this,
                    string.Format(
                        "Объект '{0}' уже присутствует в хранилище. Будет произведена попытка замены существующего объекта на новый.",
                        exist.Description));

                remove(exist);
            }

            historyFormBList.Add(obj);

            storage.Save(obj);

            Program.Logger.Info(this, "Новый документ успешно добавлен.");
        }

        /// <summary>
        /// Удалить документ из хранилища.
        /// </summary>
        /// <param name="obj">Документ для удаления.</param>
        protected virtual void remove(HistoryFormB obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка удалить из хранилища документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            historyFormBList.Remove(obj);

            storage.Delete(obj);

            Program.Logger.Info(this, "Документ успешно удалён из хранилища.");
        }

        /// <summary>
        /// Отправить запрос на получение движения по справке 'Б'.
        /// </summary>
        /// <param name="informBRegId">Номер справки 'Б'.</param>
        protected virtual void requestHistoryFormB(string informBRegId)
        {
            XmlDocument xmlDocument = getHistoryFormBData(informBRegId);

            #region Сохранение исходящего запроса.

            xmlDocument.Save(string.Format("{0}\\{1}.requesthistoryformb.{2}.{3}", storage.PathOut,
                FileStorage.NativePrefix, DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));

            #endregion Сохранение исходящего запроса.

            Program.Logger.Info(this, string.Format("Попытка отправить данные '{0}' по адресу '{1}'...",
                xmlDocument.OuterXml,
                (documentsVersion == 1) ? addresses.RequestHistoryFormB : addresses.RequestHistoryFormF2));

            HttpTransport.UploadFile(
                (documentsVersion == 1) ? addresses.RequestHistoryFormB : addresses.RequestHistoryFormF2,
                xmlDocument, "xml_file", "text/xml; charset=utf-8", configuration.UtmTimeoutLong);

            Program.Logger.Info(this, "... данные успешно отправлены.");
        }

        /// <summary>
        /// Сформировать xml-документ, представляющий запрос на получение движения по справке 'Б'.
        /// </summary>
        /// <param name="informBRegId">Номер справки 'Б'.</param>
        /// <returns></returns>
        protected virtual XmlDocument getHistoryFormBData(string informBRegId)
        {
            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            XmlNode docsNode = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docsNode, "Version", "1.0");
            addAttribute(xml, docsNode, "xmlns:" + addresses.Ns.Prefix, addresses.Ns.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Xsi.Prefix, addresses.Xsi.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Qp.Prefix, addresses.Qp.Uri);

            if (documentsVersion > 1)
            {
                addAttribute(xml, docsNode, "xmlns:" + addresses.Xs.Prefix, addresses.Xs.Uri);
                addAttribute(xml, docsNode, "xmlns:" + addresses.Oref.Prefix, addresses.Oref.Uri);
                addAttribute(xml, docsNode, "xmlns:" + addresses.Pref.Prefix, addresses.Pref.Uri);
            }

            xml.AppendChild(docsNode);

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docsNode.AppendChild(owner);

            XmlNode FsrarId = xml.CreateElement(addresses.Ns.Prefix, "FSRAR_ID", addresses.Ns.Uri);
            FsrarId.InnerText = configuration.FsrarId;
            owner.AppendChild(FsrarId);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docsNode.AppendChild(doc);

            XmlNode queryHist = (documentsVersion == 1)
                ? xml.CreateElement(addresses.Ns.Prefix, "QueryFormBHistory", addresses.Ns.Uri)
                : xml.CreateElement(addresses.Ns.Prefix, "QueryForm2History", addresses.Ns.Uri);
            doc.AppendChild(queryHist);

            XmlNode parameters = xml.CreateElement(addresses.Qp.Prefix, "Parameters", addresses.Qp.Uri);
            queryHist.AppendChild(parameters);

            XmlNode parameter = xml.CreateElement(addresses.Qp.Prefix, "Parameter", addresses.Qp.Uri);
            parameters.AppendChild(parameter);

            XmlNode name = xml.CreateElement(addresses.Qp.Prefix, "Name", addresses.Qp.Uri);
            name.InnerText = "RFB";
            parameter.AppendChild(name);

            XmlNode value = xml.CreateElement(addresses.Qp.Prefix, "Value", addresses.Qp.Uri);
            value.InnerText = informBRegId;
            parameter.AppendChild(value);

            return xml;
        }

        #endregion HistoryFormB...

        #region MarkBarcode...

        /// <summary>
        /// Получить движение по форме 'Б'.
        /// </summary>
        /// <param name="url">Адрес для загрузки.</param>
        /// <param name="replyId">Идентификатор исходного запроса.</param>
        /// <param name="xml">Ответ сервера.</param>
        protected virtual void getMarkBarcode(string url, string replyId, XmlDocument xml)
        {
            if ((xml.DocumentElement == null)
                || (xml.DocumentElement["ns:Document"] == null)
                || (xml.DocumentElement["ns:Document"]["ns:ReplyBarcode"] == null))
                throw new Exception("Необходимые XML-ноды в документе пустые.");

            MarkBarcode markBarcode = new MarkBarcode(xml.DocumentElement["ns:Document"]["ns:ReplyBarcode"])
            {
                ReplyId = replyId,
                Url = url,
            };

            add(markBarcode);
        }

        /// <summary>
        /// Добавить новый документ.
        /// </summary>
        /// <param name="obj">Новый документ.</param>
        protected virtual void add(MarkBarcode obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка добавить в хранилище полученный документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            MarkBarcode exist = markBarcodes.FirstOrDefault(x => x.ReplyId == obj.ReplyId);

            if (exist != null)
            {
                Program.Logger.Warn(this,
                    string.Format(
                        "Объект '{0}' уже присутствует в хранилище. Будет произведена попытка замены существующего объекта на новый.",
                        exist.Description));

                remove(exist);
            }

            markBarcodes.Add(obj);

            storage.Save(obj);

            Program.Logger.Info(this, "Новый документ успешно добавлен.");
        }

        /// <summary>
        /// Удалить документ из хранилища.
        /// </summary>
        /// <param name="obj">Документ для удаления.</param>
        protected virtual void remove(MarkBarcode obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка удалить из хранилища документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            markBarcodes.Remove(obj);

            storage.Delete(obj);

            Program.Logger.Info(this, "Документ успешно удалён из хранилища.");
        }

        /// <summary>
        /// Отправить запрос на получение штрих-кодов.
        /// </summary>
        /// <param name="list">Список марок.</param>
        protected virtual void requestMarkBarcodes(IList<StateMark> list)
        {
            XmlDocument xmlDocument = getMarkBarcodesData(list);

            #region Сохранение исходящего запроса.

            xmlDocument.Save(string.Format("{0}\\{1}.requestmarkbarcodes.{2}.{3}", storage.PathOut,
                FileStorage.NativePrefix, DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));

            #endregion Сохранение исходящего запроса.

            Program.Logger.Info(this,
                string.Format("Попытка отправить данные '{0}' по адресу '{1}'...", xmlDocument.OuterXml,
                    addresses.RequestMarkBarcode));

            HttpTransport.UploadFile(addresses.RequestMarkBarcode, xmlDocument, "xml_file", "text/xml; charset=utf-8",
                configuration.UtmTimeoutLong);

            Program.Logger.Info(this, "... данные успешно отправлены.");
        }

        /// <summary>
        /// Сформировать xml-документ, представляющий запрос на получение штрих-кодов.
        /// </summary>
        /// <param name="list">Список марок.</param>
        /// <returns></returns>
        protected virtual XmlDocument getMarkBarcodesData(IList<StateMark> list)
        {
            if (list.Count == 0)
                throw new Exception("Запрос не содержит ни одной марки, для которой необходимо получить штрих-код.");

            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            XmlNode docsNode = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docsNode, "Version", "1.0");
            addAttribute(xml, docsNode, "xmlns:" + addresses.Xsi.Prefix, addresses.Xsi.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Ns.Prefix, addresses.Ns.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Bk.Prefix, addresses.Bk.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Ce.Prefix, addresses.Ce.Uri);
            xml.AppendChild(docsNode);

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docsNode.AppendChild(owner);

            XmlNode FsrarId = xml.CreateElement(addresses.Ns.Prefix, "FSRAR_ID", addresses.Ns.Uri);
            FsrarId.InnerText = configuration.FsrarId;
            owner.AppendChild(FsrarId);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docsNode.AppendChild(doc);

            XmlNode queryBarcode = xml.CreateElement(addresses.Ns.Prefix, "QueryBarcode", addresses.Ns.Uri);
            doc.AppendChild(queryBarcode);

            addNodeToXmlDocument(xml, queryBarcode, addresses.Bk, "QueryNumber",
                string.Format("{0}", DateTime.Now.ToString("yyyyMMdd-HHmmssfff")));
            addNodeToXmlDocument(xml, queryBarcode, addresses.Bk, "Date", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));

            XmlNode marks = xml.CreateElement(addresses.Bk.Prefix, "Marks", addresses.Bk.Uri);
            queryBarcode.AppendChild(marks);

            int i = 1;

            foreach (StateMark stateMark in list)
            {
                XmlNode mark = xml.CreateElement(addresses.Bk.Prefix, "Mark", addresses.Bk.Uri);
                marks.AppendChild(mark);

                addNodeToXmlDocument(xml, mark, addresses.Bk, "Identity", i.ToString("D"));
                addNodeToXmlDocument(xml, mark, addresses.Bk, "Type", stateMark.MarkType.Code);
                addNodeToXmlDocument(xml, mark, addresses.Bk, "Rank", stateMark.Series);
                addNodeToXmlDocument(xml, mark, addresses.Bk, "Number", stateMark.Number);

                ++i;
            }

            return xml;
        }

        #endregion MarkBarcode...

        #region WaybillAct...

        /// <summary>
        /// Получить акт по накладной.
        /// </summary>
        /// <param name="url">Адрес для загрузки.</param>
        /// <param name="xml">Ответ сервера.</param>
        /// <param name="version">Версия документа.</param>
        protected virtual void getWaybillAct(string url, XmlDocument xml, int version = 1)
        {
            string nameWaybillAct = (version == 1) ? "ns:WayBillAct" : $"ns:WayBillAct_v{version}";

            if ((xml.DocumentElement == null)
                || (xml.DocumentElement["ns:Document"] == null)
                || (xml.DocumentElement["ns:Document"][nameWaybillAct] == null))
                throw new Exception("Необходимые XML-ноды в документе пустые.");

            WaybillAct waybillAct = new WaybillAct(xml.DocumentElement["ns:Document"][nameWaybillAct], version)
            {
                Url = url,
            };

            add(waybillAct);
        }

        /// <summary>
        /// Добавить новый документ.
        /// </summary>
        /// <param name="obj">Новый документ.</param>
        protected virtual void add(WaybillAct obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка добавить в хранилище полученный документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            WaybillAct exist =
                waybillActs.FirstOrDefault(x => (x.WBRegId == obj.WBRegId && x.ActNumber == obj.ActNumber));

            if (exist != null)
            {
                Program.Logger.Warn(this,
                    string.Format(
                        "Объект '{0}' уже присутствует в хранилище. Будет произведена попытка замены существующего объекта на новый.",
                        exist.Description));

                remove(exist);
            }

            waybillActs.Add(obj);

            storage.Save(obj);

            Program.Logger.Info(this, "Новый документ успешно добавлен.");
        }

        /// <summary>
        /// Удалить документ из хранилища.
        /// </summary>
        /// <param name="obj">Документ для удаления.</param>
        protected virtual void remove(WaybillAct obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка удалить из хранилища документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            waybillActs.Remove(obj);

            storage.Delete(obj);

            Program.Logger.Info(this, "Документ успешно удалён из хранилища.");
        }

        /// <summary>
        /// Подтвердить или отказать акт.
        /// </summary>
        /// <param name="waybillAct">Акт.</param>
        /// <param name="isAccepted">Признак - подтверждение или отказ.</param>
        /// <param name="actNumber">Номер документа.</param>
        /// <param name="actComment">Комментарий к акту.</param>
        protected virtual void sendConfirmByWaybillAct(WaybillAct waybillAct, bool isAccepted, string actNumber,
            string actComment)
        {
            if (waybillAct.StatusEnum != WaybillActStatus.New)
                throw new Exception("Статус акта не позволяет отправить акт.");
            if (!waybillAct.IsContentExist)
                throw new Exception("Подтвердить или отказать можно только акт расхождения.");
            if (string.IsNullOrWhiteSpace(waybillAct.WBRegId))
                throw new Exception("Отсутствует идентификатор накладной по документу регистрации движения.");

            XmlDocument confirm = buildConfirmAct(waybillAct, isAccepted, actNumber, actComment);

            #region Сохранение исходящего запроса.

            confirm.Save(string.Format("{0}\\{1}.confirmact.{2}.{3}", storage.PathOut, FileStorage.NativePrefix,
                DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));

            #endregion Сохранение исходящего запроса.

            Program.Logger.Info(this,
                string.Format("Попытка отправить данные '{0}' по адресу '{1}'...", confirm.OuterXml,
                    addresses.SendConfirm));

            string answer = HttpTransport.UploadFile(addresses.SendConfirm, confirm, "xml_file",
                "text/xml; charset=utf-8", configuration.UtmTimeoutLong);

            Program.Logger.Info(this, "... данные успешно отправлены.");

            // Увы, таковы реалии ЕГАИС: идентификатор запроса возвращается в ноде "url".
            waybillAct.SetConfirmActReplyId(getNodeValueFromXMLAnswer(answer, "url"));

            waybillAct.AddConfirmAct(confirm.OuterXml);

            waybillAct.ChangeStatus(isAccepted ? WaybillActStatus.Accepted : WaybillActStatus.Rejected);

            waybillAct.AlreadyReading = true;

            storage.Save(waybillAct);
        }

        /// <summary>
        /// Сформировать документ.
        /// </summary>
        /// <param name="waybillAct">Акт.</param>
        /// <param name="isAccepted">Признак - подтверждение или отказ.</param>    
        /// <param name="actNumber">Номер.</param>
        /// <param name="actComment">Комментарий.</param>
        /// <returns>XML-документ.</returns>
        protected virtual XmlDocument buildConfirmAct(WaybillAct waybillAct, bool isAccepted, string actNumber,
            string actComment)
        {
            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            XmlNode docsNode = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docsNode, "Version", "1.0");
            addAttribute(xml, docsNode, "xmlns:" + addresses.Xsi.Prefix, addresses.Xsi.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Ns.Prefix, addresses.Ns.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Wt.Prefix, addresses.Wt.Uri);
            xml.AppendChild(docsNode);

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docsNode.AppendChild(owner);

            XmlNode FsrarId = xml.CreateElement(addresses.Ns.Prefix, "FSRAR_ID", addresses.Ns.Uri);
            FsrarId.InnerText = configuration.FsrarId;
            owner.AppendChild(FsrarId);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docsNode.AppendChild(doc);

            XmlNode confirmTicket = xml.CreateElement(addresses.Ns.Prefix, "ConfirmTicket", addresses.Ns.Uri);
            doc.AppendChild(confirmTicket);

            XmlNode header = xml.CreateElement(addresses.Wt.Prefix, "Header", addresses.Wt.Uri);
            confirmTicket.AppendChild(header);

            XmlNode isConfirm = xml.CreateElement(addresses.Wt.Prefix, "IsConfirm", addresses.Wt.Uri);
            isConfirm.InnerText = isAccepted ? "Accepted" : "Rejected";
            header.AppendChild(isConfirm);

            XmlNode ticketnumber = xml.CreateElement(addresses.Wt.Prefix, "TicketNumber", addresses.Wt.Uri);
            ticketnumber.InnerText = actNumber;
            header.AppendChild(ticketnumber);

            XmlNode ticketdate = xml.CreateElement(addresses.Wt.Prefix, "TicketDate", addresses.Wt.Uri);
            ticketdate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
            header.AppendChild(ticketdate);

            XmlNode wbregid = xml.CreateElement(addresses.Wt.Prefix, "WBRegId", addresses.Wt.Uri);
            wbregid.InnerText = waybillAct.WBRegId;
            header.AppendChild(wbregid);

            XmlNode note = xml.CreateElement(addresses.Wt.Prefix, "Note", addresses.Wt.Uri);
            note.InnerText = actComment;
            header.AppendChild(note);

            return xml;
        }

        /// <summary>
        /// Переотправить подтверждение по акту.
        /// </summary>
        /// <param name="waybillAct">Акт.</param>
        protected virtual void resendConfirmAct(WaybillAct waybillAct)
        {
            if (string.IsNullOrWhiteSpace(waybillAct.XmlConfirmAct))
            {
                throw new Exception("Повторная отправка применима только для ранее отправлявшихся документов.");
            }

            if (!waybillAct.XmlConfirmAct.StartsWith("<?xml "))
            {
                throw new Exception("К сожалению, для данной версии документа повторная отправка невозможна." +
                                    "\r\nЕсли повторная отправка крайне необходима, обратитесь в службу технической поддержки.");
            }

            if (waybillAct.StatusEnum == WaybillActStatus.New)
            {
                throw new Exception("Переотправить подтверждение можно только по тем актам, " +
                                    "для которых уже было сформировано подтверждение или отказ.");
            }

            Program.Logger.Info(this,
                string.Format("Попытка переотправить данные '{0}' по адресу '{1}'...", waybillAct.XmlConfirmAct,
                    addresses.SendConfirm));

            HttpTransport.UploadFile(addresses.SendConfirm, Encoding.UTF8.GetBytes(waybillAct.XmlConfirmAct),
                "xml_file", "text/xml; charset=utf-8", configuration.UtmTimeoutLong);

            Program.Logger.Info(this, "... данные успешно переотправлены.");
        }

        /// <summary>
        /// Возврат акта по накладной к предыдущему состоянию после попытки отправки.
        /// </summary>
        /// <param name="replyId">Идентификатор запроса.</param>
        /// <param name="conclusionComment">Комментарий к откату.</param>
        protected void backoffActWaybill(string replyId, string conclusionComment)
        {
            try
            {
                Program.Logger.Info(this,
                    string.Format("Попытка отката актов по накладным по идентификатору '{0}'...", replyId));

                if (string.IsNullOrWhiteSpace(replyId))
                    throw new Exception("Идентификатор операции отката не может быть пустым.");

                WaybillAct act = waybillActs.FirstOrDefault(item => (((item.StatusEnum == WaybillActStatus.Accepted)
                                                                      || (item.StatusEnum == WaybillActStatus.Rejected))
                                                                     && (item.ConfirmActReplyId == replyId)));

                if (act != null)
                {
                    Program.Logger.Info(this,
                        string.Format("... найден акт по накладной для отката: '{0}'...", act.Description));

                    act.ChangeStatus(WaybillActStatus.New);
                    act.SetConfirmActReplyId(string.Empty);
                    act.SetAdditionalComment(conclusionComment);

                    waybillActs.ForceChangeListEvent();

                    storage.Save(act);

                    Program.Logger.Info(this,
                        string.Format("... акт по накладной '{0}' откачен и сохранен...", act.Description));

                    onlineEvents.Add(new OnlineEvent(OnlineEventStatus.Warning, this,
                        string.Format(
                            "Отправление подтверждения с номером '{0}' ('{1}') по акту накладной было отвергнуто сервером ЕГАИС. Причина отказа предоставлена в соответствующей квитанции сервера (смотрите окно 'Список квитанций-ответов сервера').",
                            act.ActNumber, act.ActDate)));
                }
                else
                {
                    Program.Logger.Info(this, "... документ для отката не найден...");
                }

                Program.Logger.Info(this,
                    string.Format("... попытка отката акта по накладным по идентификатору '{0}' успешно завершена.",
                        replyId));
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время проведения операции отката акта по накладной произошла ошибка.",
                    exception);

                throw;
            }
        }

        #endregion WaybillAct...

        #region WaybillRepeal...

        /// <summary>
        /// Получить акт по накладной.
        /// </summary>
        /// <param name="url">Адрес для загрузки.</param>
        /// <param name="xml">Ответ сервера.</param>
        protected virtual void getWaybillRepeal(string url, XmlDocument xml)
        {
            if ((xml.DocumentElement == null)
                || (xml.DocumentElement["ns:Document"] == null)
                || (xml.DocumentElement["ns:Document"]["ns:RequestRepealWB"] == null))
                throw new Exception("Необходимые XML-ноды в документе пустые.");

            WaybillRepeal waybillRepeal = new WaybillRepeal(xml.DocumentElement["ns:Document"]["ns:RequestRepealWB"])
            {
                Url = url,
            };

            add(waybillRepeal);
        }

        /// <summary>
        /// Добавить новый документ.
        /// </summary>
        /// <param name="obj">Новый документ.</param>
        protected virtual void add(WaybillRepeal obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка добавить в хранилище полученный документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            WaybillRepeal exist =
                waybillRepeals.FirstOrDefault(x => (x.WBRegId == obj.WBRegId && x.RequestNumber == obj.RequestNumber));

            if (exist != null)
            {
                Program.Logger.Warn(this,
                    string.Format(
                        "Объект '{0}' уже присутствует в хранилище. Будет произведена попытка замены существующего объекта на новый.",
                        exist.Description));

                remove(exist);
            }

            waybillRepeals.Add(obj);

            storage.Save(obj);

            Program.Logger.Info(this, "Новый документ успешно добавлен.");
        }

        /// <summary>
        /// Удалить документ из хранилища.
        /// </summary>
        /// <param name="obj">Документ для удаления.</param>
        protected virtual void remove(WaybillRepeal obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка удалить из хранилища документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            waybillRepeals.Remove(obj);

            storage.Delete(obj);

            Program.Logger.Info(this, "Документ успешно удалён из хранилища.");
        }

        /// <summary>
        /// Подтвердить или отказать акт.
        /// </summary>
        /// <param name="waybillRepeal">Акт.</param>
        /// <param name="isAccepted">Признак - подтверждение или отказ.</param>
        /// <param name="actNumber">Номер документа.</param>
        /// <param name="actComment">Комментарий к акту.</param>
        protected virtual void sendConfirmWaybillRepeal(WaybillRepeal waybillRepeal, bool isAccepted, string actNumber,
            string actComment)
        {
            if (waybillRepeal.StatusEnum != WaybillRepealStatus.New)
                throw new Exception("Статус запроса не позволяет отправить акт.");
            if (string.IsNullOrWhiteSpace(waybillRepeal.WBRegId))
                throw new Exception("Отсутствует идентификатор накладной по документу регистрации движения.");

            XmlDocument confirm = buildConfirmRepeal(waybillRepeal, isAccepted, actNumber, actComment);

            #region Сохранение исходящего запроса.

            confirm.Save(string.Format("{0}\\{1}.confirmrepealwaybill.{2}.{3}", storage.PathOut,
                FileStorage.NativePrefix, DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));

            #endregion Сохранение исходящего запроса.

            Program.Logger.Info(this,
                string.Format("Попытка отправить данные '{0}' по адресу '{1}'...", confirm.OuterXml,
                    addresses.SendConfirmRepealWaybill));

            string answer = HttpTransport.UploadFile(addresses.SendConfirmRepealWaybill, confirm, "xml_file",
                "text/xml; charset=utf-8", configuration.UtmTimeoutLong);

            Program.Logger.Info(this, "... данные успешно отправлены.");

            // Увы, таковы реалии ЕГАИС: идентификатор запроса возвращается в ноде "url".
            waybillRepeal.SetConfirmActReplyId(getNodeValueFromXMLAnswer(answer, "url"));

            waybillRepeal.AddConfirmAct(confirm.OuterXml);

            waybillRepeal.ChangeStatus(isAccepted ? WaybillRepealStatus.Accepted : WaybillRepealStatus.Rejected);

            waybillRepeal.AlreadyReading = true;

            storage.Save(waybillRepeal);
        }

        /// <summary>
        /// Сформировать документ.
        /// </summary>
        /// <param name="waybillRepeal">Акт.</param>
        /// <param name="isAccepted">Признак - подтверждение или отказ.</param>    
        /// <param name="actNumber">Номер.</param>
        /// <param name="actComment">Комментарий.</param>
        /// <returns>XML-документ.</returns>
        protected virtual XmlDocument buildConfirmRepeal(WaybillRepeal waybillRepeal, bool isAccepted, string actNumber,
            string actComment)
        {
            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            XmlNode docsNode = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docsNode, "Version", "1.0");
            addAttribute(xml, docsNode, "xmlns:" + addresses.Xsi.Prefix, addresses.Xsi.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Ns.Prefix, addresses.Ns.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.WtRepeal.Prefix, addresses.WtRepeal.Uri);
            xml.AppendChild(docsNode);

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docsNode.AppendChild(owner);

            XmlNode FsrarId = xml.CreateElement(addresses.Ns.Prefix, "FSRAR_ID", addresses.Ns.Uri);
            FsrarId.InnerText = configuration.FsrarId;
            owner.AppendChild(FsrarId);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docsNode.AppendChild(doc);

            XmlNode confirmRepeal = xml.CreateElement(addresses.Ns.Prefix, "ConfirmRepealWB", addresses.Ns.Uri);
            doc.AppendChild(confirmRepeal);

            XmlNode header = xml.CreateElement(addresses.WtRepeal.Prefix, "Header", addresses.WtRepeal.Uri);
            confirmRepeal.AppendChild(header);

            XmlNode isConfirm = xml.CreateElement(addresses.WtRepeal.Prefix, "IsConfirm", addresses.WtRepeal.Uri);
            isConfirm.InnerText = isAccepted ? "Accepted" : "Rejected";
            header.AppendChild(isConfirm);

            XmlNode confirmnumber =
                xml.CreateElement(addresses.WtRepeal.Prefix, "ConfirmNumber", addresses.WtRepeal.Uri);
            confirmnumber.InnerText = actNumber;
            header.AppendChild(confirmnumber);

            XmlNode confirmdate = xml.CreateElement(addresses.WtRepeal.Prefix, "ConfirmDate", addresses.WtRepeal.Uri);
            confirmdate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
            header.AppendChild(confirmdate);

            XmlNode wbregid = xml.CreateElement(addresses.WtRepeal.Prefix, "WBRegId", addresses.WtRepeal.Uri);
            wbregid.InnerText = waybillRepeal.WBRegId;
            header.AppendChild(wbregid);

            XmlNode note = xml.CreateElement(addresses.WtRepeal.Prefix, "Note", addresses.WtRepeal.Uri);
            note.InnerText = actComment;
            header.AppendChild(note);

            return xml;
        }

        /// <summary>
        /// Обработка подтверждения на распроведение накладной.
        /// </summary>
        /// <param name="replyId">Идентификатор запроса.</param>
        /// <param name="conclusion">Заключение.</param>
        /// <param name="conclusionComment">Комментарий.</param>
        protected virtual void backoffConfirmRepeal(string replyId, string conclusion, string conclusionComment)
        {
            try
            {
                Program.Logger.Info(this,
                    string.Format(
                        "Попытка обработки операции подтверждения или отказа распроведения входящих накладных по идентификатору запроса '{0}'...",
                        replyId));

                if (string.IsNullOrWhiteSpace(replyId))
                    throw new Exception("Идентификатор операции отката не может быть пустым.");

                WaybillRepeal waybillRepeal = waybillRepeals.FirstOrDefault(item =>
                    (((item.StatusEnum == WaybillRepealStatus.Accepted)
                      || (item.StatusEnum == WaybillRepealStatus.Rejected))
                     && (item.ConfirmActReplyId == replyId)));

                if (waybillRepeal != null)
                {
                    Program.Logger.Info(this,
                        string.Format("... найден запрос на распроведение накладной для обработки: '{0}'...",
                            waybillRepeal.Description));

                    if (conclusion.ToLower() == "rejected")
                    {
                        waybillRepeal.ChangeStatus(WaybillRepealStatus.New);
                        waybillRepeal.SetAdditionalComment(conclusionComment);

                        Program.Logger.Info(this,
                            string.Format("\t... запросу на распроведение '{0}' возвращён предыдущий статус...",
                                waybillRepeal.Description));
                    }

                    inWaybills.ForceChangeListEvent();

                    storage.Save(waybillRepeal);

                    onlineEvents.Add(new OnlineEvent(OnlineEventStatus.Warning, this,
                        string.Format(
                            "Подтверждение запрос на распроведение накладной с номером '{0}' от '{1}' был отвергнут сервером ЕГАИС. Причина отказа предоставлена в соответствующей квитанции сервера (смотрите окно 'Список квитанций-ответов сервера').",
                            waybillRepeal.RequestNumber, waybillRepeal.RequestNumber)));
                }
                else
                {
                    Program.Logger.Info(this, "... документ для отката не найден...");
                }

                Program.Logger.Info(this,
                    string.Format(
                        "... попытка обработки операции подтверждения или отказа распроведения входящих накладных по идентификатору '{0}' успешно завершена.",
                        replyId));
            }
            catch (Exception exception)
            {
                Program.Logger.Error(
                    "Во время проведения операции обработки распроведения входящей накладной произошла ошибка.",
                    exception);

                throw;
            }
        }

        #endregion WaybillRepeal...

        #region WaybillRepealConfirm...

        /// <summary>
        /// Получить акт по накладной.
        /// </summary>
        /// <param name="url">Адрес для загрузки.</param>
        /// <param name="xml">Ответ сервера.</param>
        protected virtual void getWaybillRepealConfirm(string url, XmlDocument xml)
        {
            if ((xml.DocumentElement == null)
                || (xml.DocumentElement["ns:Document"] == null)
                || (xml.DocumentElement["ns:Document"]["ns:ConfirmRepealWB"] == null))
                throw new Exception("Необходимые XML-ноды в документе пустые.");

            WaybillRepealConfirm waybillRepealConfirm =
                new WaybillRepealConfirm(xml.DocumentElement["ns:Document"]["ns:ConfirmRepealWB"])
                {
                    Url = url,
                };

            add(waybillRepealConfirm);
        }

        /// <summary>
        /// Добавить новый документ.
        /// </summary>
        /// <param name="obj">Новый документ.</param>
        protected virtual void add(WaybillRepealConfirm obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка добавить в хранилище полученный документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            WaybillRepealConfirm exist = waybillRepealConfirms.FirstOrDefault(x =>
                (x.WBRegId == obj.WBRegId && x.ConfirmNumber == obj.ConfirmNumber));

            if (exist != null)
            {
                Program.Logger.Warn(this,
                    string.Format(
                        "Объект '{0}' уже присутствует в хранилище. Будет произведена попытка замены существующего объекта на новый.",
                        exist.Description));

                remove(exist);
            }

            waybillRepealConfirms.Add(obj);

            storage.Save(obj);

            backoffRepealedInwaybill(obj);

            Program.Logger.Info(this, "Новый документ успешно добавлен.");
        }

        /// <summary>
        /// Удалить документ из хранилища.
        /// </summary>
        /// <param name="obj">Документ для удаления.</param>
        protected virtual void remove(WaybillRepealConfirm obj)
        {
            Program.Logger.Info(this,
                string.Format("Попытка удалить из хранилища документ '{0}': '{1}'.", obj.GetType().FullName,
                    obj.Description));

            waybillRepealConfirms.Remove(obj);

            storage.Delete(obj);

            Program.Logger.Info(this, "Документ успешно удалён из хранилища.");
        }

        #endregion WaybillRepealConfirm...

        #region OutWaybill...

        /// <summary>
        /// Добавить исходящую накладную в список и записать в хранилище.
        /// </summary>
        /// <param name="outWaybill">Исходящая накладная.</param>
        protected virtual void addAndSaveOutWaybill(OutWaybill outWaybill)
        {
            if (OutWaybills.All(waybill => waybill.SurrogateId != outWaybill.SurrogateId))
            {
                outWaybills.Add(outWaybill);
            }

            Storage.Save(outWaybill);
        }

        /// <summary>
        /// Удалить документ.
        /// </summary>
        /// <param name="document">Документ.</param>
        protected virtual void delete(OutWaybill document)
        {
            if (OutWaybills.Any(waybill => waybill.SurrogateId == document.SurrogateId))
            {
                outWaybills.Remove(document);
            }

            Storage.Delete(document);
        }

        /// <summary>
        /// Удалить документ.
        /// </summary>
        /// <param name="document">Документ.</param>
        protected virtual void delete(Production document)
        {
            if (Production.Any(x => x.SurrogateId == document.SurrogateId))
            {
                Production.Remove(document);
            }

            Storage.Delete(document, false);
        }

        /// <summary>
        /// Отправить исходящую накладную получателю.
        /// </summary>
        /// <param name="outWaybill">Накладная.</param>
        protected virtual void send(OutWaybill outWaybill)
        {
            if (outWaybill.StatusEnum != OutWaybillStatus.Ready)
            {
                throw new Exception(
                    "Отправить можно только такую накладную, которая находится в статусе 'новая, готова к отправке'.");
            }

            XmlDocument xmlDocument = buildOutWaybill(outWaybill);

            #region Сохранение исходящего запроса.

            xmlDocument.Save(string.Format("{0}\\{1}.outwaybill.{2}.{3}", storage.PathOut, FileStorage.NativePrefix,
                DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));

            #endregion Сохранение исходящего запроса.

            Program.Logger.Info(this, string.Format("Попытка отправить данные '{0}' по адресу '{1}'...",
                xmlDocument.OuterXml,
                (documentsVersion == 1) ? addresses.SendWaybill : addresses.SendWaybill_v3(documentsVersion)));

            string answer = HttpTransport.UploadFile(
                (documentsVersion == 1) ? addresses.SendWaybill : addresses.SendWaybill_v3(documentsVersion),
                xmlDocument, "xml_file", "text/xml; charset=utf-8", configuration.UtmTimeoutLong);

            Program.Logger.Info(this, string.Format("... данные успешно отправлены; получен ответ '{0}'.", answer));

            // Увы, таковы реалии ЕГАИС: идентификатор запроса возвращается в ноде "url".
            outWaybill.SetSendReplyId(getNodeValueFromXMLAnswer(answer, "url"));

            outWaybill.AddOutData(xmlDocument);
            outWaybill.ChangeStatus(OutWaybillStatus.Sent);

            storage.Save(outWaybill);
        }

        /// <summary>
        /// Построить исходящий xml-документ.
        /// </summary>
        /// <returns>Xml-документ</returns>
        protected virtual XmlDocument buildOutWaybill(OutWaybill outWaybill)
        {
            Program.Logger.Info(this,
                string.Format("Попытка построить исходящий xml-документ для накладной '{0}'...",
                    outWaybill.Description));

            outWaybill.Check(false);

            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            #region Схемы...

            XmlPrefix pref, oref, wb=null;
            XmlPrefix ce = addresses.Ce3;
            if (documentsVersion == 1)
            {
                oref = addresses.Oref;
                pref = addresses.Pref;
                wb = addresses.Wb;
            }
            else
            {
                oref = addresses.Oref2;
                pref = addresses.Pref2;
                switch (documentsVersion)
                {
                    case 2:
                    {
                        wb = addresses.Wb2;
                        break;
                    }
                    case 3:
                    {
                        wb = addresses.Wb3;
                        break;
                    }
                    case 4:
                    {
                        wb = addresses.Wb4;
                        break;
                    }
                    default:
                        //todo
                        break;
                }

                //wb = documentsVersion == 2 ? addresses.Wb2 : addresses.Wb3;
            }

            XmlNode docs = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docs, "Version", "1.0");
            addAttribute(xml, docs, "xmlns:" + addresses.Xsi.Prefix, addresses.Xsi.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.Ns.Prefix, addresses.Ns.Uri);
            addAttribute(xml, docs, "xmlns:" + oref.Prefix, oref.Uri);
            addAttribute(xml, docs, "xmlns:" + pref.Prefix, pref.Uri);
            addAttribute(xml, docs, "xmlns:" + wb.Prefix, wb.Uri);

            if (documentsVersion == 1) addAttribute(xml, docs, "xmlns:" + addresses.C.Prefix, addresses.C.Uri);
            if (documentsVersion >= 3) addAttribute(xml, docs, "xmlns:" + addresses.Ce3.Prefix, addresses.Ce3.Uri);

            xml.AppendChild(docs);

            #endregion Схемы...

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docs.AppendChild(owner);

            addNodeToXmlDocument(xml, owner, addresses.Ns, "FSRAR_ID", configuration.FsrarId);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docs.AppendChild(doc);

            XmlNode waybill = (documentsVersion == 1)
                ? xml.CreateElement(addresses.Ns.Prefix, "WayBill", addresses.Ns.Uri)
                : xml.CreateElement(addresses.Ns.Prefix, $"WayBill_v{documentsVersion}", addresses.Ns.Uri);
            doc.AppendChild(waybill);

            if (string.IsNullOrWhiteSpace(outWaybill.Identity))
                throw new Exception("В накладной отсутствует клиентский идентификатор ('Identity').");
            addNodeToXmlDocument(xml, waybill, wb, "Identity", outWaybill.Identity);

            XmlNode header = xml.CreateElement(wb.Prefix, "Header", wb.Uri);
            waybill.AppendChild(header);

            if (string.IsNullOrWhiteSpace(outWaybill.Number))
                throw new Exception("В накладной отсутствует номер ('Number').");
            addNodeToXmlDocument(xml, header, wb, "NUMBER", outWaybill.Number);

            addNodeToXmlDocument(xml, header, wb, "Date", outWaybill.OutDate.ToString("yyyy-MM-dd"));
            addNodeToXmlDocument(xml, header, wb, "ShippingDate", outWaybill.OutShippingDate.ToString("yyyy-MM-dd"));

            if (string.IsNullOrWhiteSpace(outWaybill.TypeWaybill))
                throw new Exception("Тип накладной не может быть пустым ('Type').");
            addNodeToXmlDocument(xml, header, wb, "Type", outWaybill.TypeWaybill);

            if (documentsVersion == 1)
            {
                if (string.IsNullOrWhiteSpace(outWaybill.UnitType))
                    throw new Exception("Тип продукции не может быть пустым ('UnitType').");
                addNodeToXmlDocument(xml, header, wb, "UnitType", outWaybill.UnitType);
            }

            #region Отправитель...

            {
                if (string.IsNullOrWhiteSpace(outWaybill.ShipperClientRegId))
                    throw new Exception("В накладной отсутствует идентификатор отправителя.");

                XmlNode shipper = xml.CreateElement(wb.Prefix, "Shipper", wb.Uri);
                header.AppendChild(shipper);

                XmlDocument xmlPartner = new XmlDocument();
                string bodyPartner = findPartner(outWaybill.ShipperClientRegId).XmlBody;
                if (documentsVersion < definePartnerVersion(bodyPartner))
                    throw new Exception(
                        "Ошибка составления: в накладную версии 'v1' добавляется информация об отправителе версии 'v2'.");
                xmlPartner.LoadXml(documentsVersion == 1 ? bodyPartner : convertPartnerToV2(bodyPartner));

                importToXmlDocument(xml, shipper, xmlPartner);
            }

            #endregion Отправитель...

            #region Получатель...

            {
                if (string.IsNullOrWhiteSpace(outWaybill.ConsigneeClientRegId))
                    throw new Exception("В накладной отсутствует идентификатор получателя.");

                XmlNode consignee = xml.CreateElement(wb.Prefix, "Consignee", wb.Uri);
                header.AppendChild(consignee);

                XmlDocument xmlPartner = new XmlDocument();
                string bodyPartner = findPartner(outWaybill.ConsigneeClientRegId).XmlBody;
                if (documentsVersion < definePartnerVersion(bodyPartner))
                    throw new Exception(
                        "Ошибка составления: в накладную версии 'v1' добавляется информация о получателе версии 'v2'.");
                xmlPartner.LoadXml(documentsVersion == 1 ? bodyPartner : convertPartnerToV2(bodyPartner));
                importToXmlDocument(xml, consignee, xmlPartner);
            }

            #endregion Получатель...

            #region Поставщик...

            // В документах второй версии поставщик не указывается.
            // Посему - удаляем этот функционал, в том числе - в UI.
            // if (documentsVersion == 1)
            // {
            //     if (outWaybill.IncludeSupplier)
            //     {
            //         if (string.IsNullOrWhiteSpace(outWaybill.SupplierClientRegId)) throw new Exception("В накладной отсутствует идентификатор поставщика.");
            // 
            //         XmlNode supplier = xml.CreateElement(wb.Prefix, "Supplier", wb.Uri);
            //         header.AppendChild(supplier);
            // 
            //         XmlDocument xmlPartner = new XmlDocument();
            //         string bodyPartner = findPartner(outWaybill.SupplierClientRegId).XmlBody;
            //         if (documentsVersion < definePartnerVersion(bodyPartner)) throw new Exception("Ошибка составления: в накладную версии 'v1' добавляется информация о поставщике версии 'v2'.");
            //         xmlPartner.LoadXml(documentsVersion == 1 ? bodyPartner : convertPartnerToV2(bodyPartner));
            //         importToXmlDocument(xml, supplier, xmlPartner);
            //     }
            // }

            #endregion Поставщик...

            #region Транспорт...

            {
                XmlNode transport = xml.CreateElement(wb.Prefix, "Transport", wb.Uri);
                header.AppendChild(transport);
                if (documentsVersion == 4)
                {
                    addNodeToXmlDocument(xml, transport, wb, "ChangeOwnership",
                        outWaybill.ChangeOwnership ? "IsChange" : "NotChange");
                }

                addNodeToXmlDocument(xml, transport, wb, "TRAN_TYPE", outWaybill.TranType);

                foreach (KeyValuePair<string, string> pair in outWaybill.Transport)
                                
                    
                    addNodeToXmlDocument(xml, transport, wb, pair.Key, pair.Value);
               
            }

            #endregion Транспорт...

            addNodeToXmlDocument(xml, header, wb, "Base", outWaybill.BaseWaybill);
            addNodeToXmlDocument(xml, header, wb, "Note", outWaybill.NoteWaybill);

            #region Состав...

            XmlNode content = xml.CreateElement(wb.Prefix, "Content", wb.Uri);
            waybill.AppendChild(content);
            if (outWaybill.OutPositions.Count == 0)
                throw new Exception("Список позиций товарно-транспортной накладной пуст. Отправка не имеет смысла.");

            foreach (OutPosition position in outWaybill.OutPositions)
            {
                position.Check();

                XmlNode positionNode = xml.CreateElement(wb.Prefix, "Position", wb.Uri);
                content.AppendChild(positionNode);

                addNodeToXmlDocument(xml, positionNode, wb, "Identity", position.Identity.ToString("D"));

                #region Продукт...

                XmlNode product = xml.CreateElement(wb.Prefix, "Product", wb.Uri);
                positionNode.AppendChild(product);

                string xmlProductionBody = string.IsNullOrWhiteSpace(position.XmlProductionBody)
                    ? findProduction(position.AlcoCode).XmlBody
                    : position.XmlProductionBody;

                if (string.IsNullOrWhiteSpace(xmlProductionBody))
                {
                    throw new Exception(
                        "В позиции накладной присутствует алкогольная продукция с незаполненным xml-телом.");
                }

                XmlDocument xmlProduct = new XmlDocument();
                if (documentsVersion < definePartnerVersion(xmlProductionBody))
                    throw new Exception(
                        "Ошибка составления: в накладную версии 'v1' добавляется информация о продукции версии 'v2'.");
                xmlProduct.LoadXml(documentsVersion == 1 ? xmlProductionBody : convertPartnerToV2(xmlProductionBody));
                importToXmlDocument(xml, product, xmlProduct);

                #region Fucking EGAIS...

                if (documentsVersion > 1)
                {
                    if (product["pref:Importer"] != null)
                    {
                        product.RemoveChild(product["pref:Importer"]);
                    }

                    if (product["pref:Producer"]?["oref:UL"]?["oref:address"] != null)
                    {
                        XmlNode addressNode = product["pref:Producer"]["oref:UL"]["oref:address"];

                        if (addressNode.ChildNodes.Cast<XmlNode>()
                            .All(child => child.Name.ToLower() != "oref:regioncode"))
                        {
                            addNodeToXmlDocument(xml, addressNode, oref, "RegionCode", "00");
                        }
                    }

                    if (product["pref:UnitType"] == null)
                        addNodeToXmlDocument(xml, product, pref, "UnitType", "Packed");
                    if (product["pref:Type"] == null) addNodeToXmlDocument(xml, product, pref, "Type", "АП");
                    if (product["pref:ShortName"] == null)
                        addNodeToXmlDocument(xml, product, pref, "ShortName", string.Empty, false);
                }

                #endregion Fucking EGAIS...

                #endregion Продукт...

                addNodeToXmlDocument(xml, positionNode, wb, "Quantity", convertToString(position.Quantity));
                addNodeToXmlDocument(xml, positionNode, wb, "Price", convertToString(position.Price));
                addNodeToXmlDocument(xml, positionNode, wb, "Party", position.Party);

                XmlNode informA;
                switch (documentsVersion)
                {
                    case 1:
                        informA = xml.CreateElement(wb.Prefix, "InformA", wb.Uri);
                        positionNode.AppendChild(informA);
                        addNodeToXmlDocument(xml, informA, pref, "RegId", position.FormARegId);
                        break;
                    case 2:
                        informA = xml.CreateElement(wb.Prefix, "InformF1", wb.Uri);
                        positionNode.AppendChild(informA);
                        addNodeToXmlDocument(xml, informA, pref, "RegId", position.FormARegId);
                        break;
                    default:
                        informA = xml.CreateElement(wb.Prefix, "FARegId", wb.Uri);
                        informA.InnerText = position.FormARegId;
                        positionNode.AppendChild(informA);
                        break;
                }


                XmlNode informB = (documentsVersion == 1)
                    ? xml.CreateElement(wb.Prefix, "InformB", wb.Uri)
                    : xml.CreateElement(wb.Prefix, "InformF2", wb.Uri);
                positionNode.AppendChild(informB);
                if (documentsVersion < 3)
                {
                    XmlNode informBItem = (documentsVersion == 1)
                        ? xml.CreateElement(pref.Prefix, "InformBItem", pref.Uri)
                        : xml.CreateElement(pref.Prefix, "InformF2Item", pref.Uri);
                    informB.AppendChild(informBItem);

                    addNodeToXmlDocument(xml, informBItem, pref, (documentsVersion == 1) ? "BRegId" : "F2RegId",
                        position.FormBRegId);
                }
                else
                {
                    XmlNode informBItem = xml.CreateElement(ce.Prefix, "F2RegId", ce.Uri);
                    informBItem.InnerText = position.FormBRegId;
                    informB.AppendChild(informBItem);

                    if (position.BoxInfos.Count > 0)
                    {
                        XmlNode markInfo = xml.CreateElement(ce.Prefix, "MarkInfo", ce.Uri);
                        informB.AppendChild(markInfo);

                        foreach (var boxInfo in position.BoxInfos)
                        {
                            XmlNode boxPos = xml.CreateElement(ce.Prefix, "boxpos", ce.Uri);
                            markInfo.AppendChild(boxPos);
                            addNodeToXmlDocument(xml, boxPos, ce, "boxnumber", boxInfo.BoxNumber);

                            XmlNode amcList = xml.CreateElement(ce.Prefix, "amclist", ce.Uri);
                            boxPos.AppendChild(amcList);

                            foreach (var amc in boxInfo.AmcList)
                            {
                              addNodeToXmlDocument(xml, amcList, ce, "amc", amc.Barcode);
                            }
                        }
                    }
                }
            }

            #endregion Состав...

            Program.Logger.Info(this,
                string.Format("... построение исходящего xml-документа длиной {0} символов успешно завершено.",
                    xml.OuterXml.Length));

            return xml;
        }

        /// <summary>
        /// Переотправить исходящую накладную получателю.
        /// </summary>
        /// <param name="outWaybill">Накладная.</param>
        protected virtual void resend(OutWaybill outWaybill)
        {
            if (string.IsNullOrWhiteSpace(outWaybill.XmlOutBody))
            {
                throw new Exception("Повторная отправка применима только для ранее отправлявшихся документов.");
            }

            if (!outWaybill.XmlOutBody.StartsWith("<?xml "))
            {
                throw new Exception("К сожалению, для данной версии документа повторная отправка невозможна." +
                                    "\r\nЕсли повторная отправка крайне необходима, обратитесь в службу технической поддержки.");
            }

            if ((outWaybill.StatusEnum == OutWaybillStatus.Partial)
                || (outWaybill.StatusEnum == OutWaybillStatus.Ready)
                || (outWaybill.StatusEnum == OutWaybillStatus.Revoked)
                || (outWaybill.StatusEnum == OutWaybillStatus.Rejected))
            {
                throw new Exception("Переотправить можно накладную, которая уже была ранее отправлена.");
            }

            var url = (documentsVersion == 1) ? addresses.SendWaybill : addresses.SendWaybill_v3(documentsVersion);
            Program.Logger.Info(this, string.Format("Попытка переотправить данные '{0}' по адресу '{1}'...",
                outWaybill.XmlOutBody,
                url));

            HttpTransport.UploadFile(url,
                Encoding.UTF8.GetBytes(outWaybill.XmlOutBody), "xml_file", "text/xml; charset=utf-8",
                configuration.UtmTimeoutLong);

            Program.Logger.Info(this, "... данные успешно переотправлены.");
        }

        /// <summary>
        /// Отозвать исходящую накладную.
        /// </summary>
        /// <param name="outWaybill">Накладная.</param>
        protected void revoke(OutWaybill outWaybill)
        {
            if (outWaybill.StatusEnum == OutWaybillStatus.Confirmed)
            {
                XmlDocument xmlRevokeAct = buildRevokeAct(outWaybill);

                #region Сохранение исходящего запроса.

                xmlRevokeAct.Save(string.Format("{0}\\{1}.revokeact.{2}.{3}", storage.PathOut, FileStorage.NativePrefix,
                    DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));

                #endregion Сохранение исходящего запроса.

                string url;
                url = documentsVersion == 1 ? addresses.SendWaybillAct : addresses.SendWaybillAct_v3(documentsVersion);
                Program.Logger.Info(this, string.Format("Попытка отправить данные '{0}' по адресу '{1}'...",
                    xmlRevokeAct.OuterXml,
                    url));

                string answer = HttpTransport.UploadFile(url,
                    xmlRevokeAct, "xml_file", "text/xml; charset=utf-8", configuration.UtmTimeoutLong);

                Program.Logger.Info(this, "... данные успешно отправлены.");

                // Увы, таковы реалии ЕГАИС: идентификатор запроса возвращается в ноде "url".
                outWaybill.SetRevokeReplyId(getNodeValueFromXMLAnswer(answer, "url"));

                outWaybill.AddRevokeAct(xmlRevokeAct);
                outWaybill.ChangeStatus(OutWaybillStatus.Revoked);

                storage.Save(outWaybill);
            }
            else
            {
                throw new Exception(
                    "Отозвать можно только накладную, находящуюся в состоянии \"отправлена, зарегистрирована в ЕГАИС\".");
            }
        }

        /// <summary>
        /// Создать акт отзыва.
        /// </summary>
        /// <param name="outWaybill">Исходящая накладная.</param>
        /// <returns>Акт отзыва.</returns>
        protected XmlDocument buildRevokeAct(OutWaybill outWaybill)
        {
            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            XmlNode docsNode = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docsNode, "Version", "1.0");
            addAttribute(xml, docsNode, "xmlns:" + addresses.Xsi.Prefix, addresses.Xsi.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Ns.Prefix, addresses.Ns.Uri);
            if (documentsVersion < 3)
            {
                addAttribute(xml, docsNode, "xmlns:" + addresses.Oref.Prefix, addresses.Oref.Uri);
                addAttribute(xml, docsNode, "xmlns:" + addresses.Pref.Prefix, addresses.Pref.Uri);
            }

            XmlPrefix wa;
            switch (documentsVersion)
            {
                case 1:
                    wa = addresses.Wa;
                    addAttribute(xml, docsNode, "xmlns:" + wa.Prefix, wa.Uri);
                    break;
                case 2:
                    wa = addresses.Wa2;
                    addAttribute(xml, docsNode, "xmlns:" + wa.Prefix, wa.Uri);
                    break;
                case 3:
                    wa = addresses.Wa3;
                    var ce = addresses.Ce3;
                    addAttribute(xml, docsNode, "xmlns:" + wa.Prefix, wa.Uri);
                    addAttribute(xml, docsNode, "xmlns:" + ce.Prefix, ce.Uri);
                    break;
                default :
                    wa = addresses.Wa4;
                    addAttribute(xml, docsNode, "xmlns:" + wa.Prefix, wa.Uri);
                    break;
                



            }


            xml.AppendChild(docsNode);

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docsNode.AppendChild(owner);

            XmlNode FsrarId = xml.CreateElement(addresses.Ns.Prefix, "FSRAR_ID", addresses.Ns.Uri);
            FsrarId.InnerText = configuration.FsrarId;
            owner.AppendChild(FsrarId);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docsNode.AppendChild(doc);

            XmlNode waybillact = (documentsVersion == 1)
                ? xml.CreateElement(addresses.Ns.Prefix, "WayBillAct", addresses.Ns.Uri)
                : xml.CreateElement(addresses.Ns.Prefix, $"WayBillAct_v{documentsVersion}", addresses.Ns.Uri);
            doc.AppendChild(waybillact);

            XmlNode header = xml.CreateElement(wa.Prefix, "Header", wa.Uri);
            waybillact.AppendChild(header);

            XmlNode isAccept = xml.CreateElement(wa.Prefix, "IsAccept", wa.Uri);
            isAccept.InnerText = "Rejected";
            header.AppendChild(isAccept);

            XmlNode actnumber = xml.CreateElement(wa.Prefix, "ACTNUMBER", wa.Uri);
            actnumber.InnerText =
                string.Format("{0}-{1}", outWaybill.Number, DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            header.AppendChild(actnumber);

            XmlNode actdate = xml.CreateElement(wa.Prefix, "ActDate", wa.Uri);
            actdate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
            header.AppendChild(actdate);

            if (string.IsNullOrWhiteSpace(outWaybill.WBRegId))
            {
                throw new Exception(
                    "Идентификатор накладной (по документу регистрации движения) пустой. Отзыв накладной невозможен.");
            }

            XmlNode wbregid = xml.CreateElement(wa.Prefix, "WBRegId", wa.Uri);
            wbregid.InnerText = outWaybill.WBRegId;
            header.AppendChild(wbregid);

            XmlNode note = xml.CreateElement(wa.Prefix, "Note", wa.Uri);
            note.InnerText = string.Format("Отзыв накладной от '{0}' для '{1}'.",
                outWaybill.OutDate.ToString("yyyy-MM-dd"), outWaybill.ConsigneeName);
            header.AppendChild(note);

            XmlNode content = xml.CreateElement(wa.Prefix, "Content", wa.Uri);
            waybillact.AppendChild(content);

            return xml;
        }

        /// <summary>
        /// Возврат накладной к предыдущему состоянию после попытки отправки.
        /// </summary>
        /// <param name="replyId">Идентификатор запроса.</param>
        /// <param name="conclusionComment">Комментарий к откату.</param>
        protected void backoffOutWaybill(string replyId, string conclusionComment)
        {
            try
            {
                Program.Logger.Info(this,
                    string.Format("Попытка отката исходящих накладных по идентификатору '{0}'...", replyId));

                if (string.IsNullOrWhiteSpace(replyId))
                    throw new Exception("Идентификатор операции отката не может быть пустым.");

                OutWaybill waybill = outWaybills.FirstOrDefault(item =>
                    ((item.StatusEnum == OutWaybillStatus.Sent) && (item.SendReplyId == replyId)));

                if (waybill != null)
                {
                    Program.Logger.Info(this,
                        string.Format("... найдена исходящая накладная для отката: '{0}'...", waybill.Description));

                    waybill.ChangeStatus(OutWaybillStatus.Rejected);
                    waybill.SetSendReplyId(string.Empty);
                    waybill.SetAdditionalComment(conclusionComment);

                    outWaybills.ForceChangeListEvent();

                    storage.Save(waybill);

                    Program.Logger.Info(this,
                        string.Format("... исходящая накладная '{0}' откачена и сохранена...", waybill.Description));

                    onlineEvents.Add(new OnlineEvent(OnlineEventStatus.Warning, this,
                        string.Format(
                            "Отправление исходящей накладной с номером '{0}' ('{1}') для '{2}' было отвергнуто сервером ЕГАИС. Причина отказа предоставлена в соответствующей квитанции сервера (смотрите окно 'Список квитанций-ответов сервера').",
                            waybill.Number, waybill.Date, waybill.ConsigneeName)));
                }
                else
                {
                    Program.Logger.Info(this, "... документ для отката не найден...");
                }

                Program.Logger.Info(this,
                    string.Format("... попытка отката исходящих накладных по идентификатору '{0}' успешно завершена.",
                        replyId));
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время проведения операции отката исходящей накладной произошла ошибка.",
                    exception);

                throw;
            }
        }

        /// <summary>
        /// Откатить отозванную накладную.
        /// </summary>
        /// <param name="replyId">Идентификатор запроса.</param>
        /// <param name="conclusionComment">Комментарий к откату.</param>
        protected void backoffRevokedOutWaybill(string replyId, string conclusionComment)
        {
            try
            {
                Program.Logger.Info(this,
                    string.Format("Попытка отката отозванных исходящих накладных по идентификатору '{0}'...", replyId));

                if (string.IsNullOrWhiteSpace(replyId))
                    throw new Exception("Идентификатор операции отката не может быть пустым.");

                OutWaybill waybill = outWaybills.FirstOrDefault(item =>
                    ((item.StatusEnum == OutWaybillStatus.Revoked) && (item.RevokeReplyId == replyId)));

                if (waybill != null)
                {
                    Program.Logger.Info(this,
                        string.Format("... найдена отозванная исходящая накладная для отката: '{0}'...",
                            waybill.Description));

                    waybill.ChangeStatus(OutWaybillStatus.Confirmed);
                    waybill.SetRevokeReplyId(string.Empty);
                    waybill.SetAdditionalComment(conclusionComment);

                    outWaybills.ForceChangeListEvent();

                    storage.Save(waybill);

                    Program.Logger.Info(this,
                        string.Format("... отозванная исходящая накладная '{0}' откачена и сохранена...",
                            waybill.Description));

                    onlineEvents.Add(new OnlineEvent(OnlineEventStatus.Warning, this,
                        string.Format(
                            "Отзыв исходящей накладной с номером '{0}' ('{1}') для '{2}' был отвергнут сервером ЕГАИС. Причина отказа предоставлена в соответствующей квитанции сервера (смотрите окно 'Список квитанций-ответов сервера').",
                            waybill.Number, waybill.Date, waybill.ConsigneeName)));
                }
                else
                {
                    Program.Logger.Info(this, "... документ для отката не найден...");
                }

                Program.Logger.Info(this,
                    string.Format(
                        "... попытка отката отозванных исходящих накладных по идентификатору '{0}' успешно завершена.",
                        replyId));
            }
            catch (Exception exception)
            {
                Program.Logger.Error(
                    "Во время проведения операции отката отозванных исходящей накладной произошла ошибка.", exception);

                throw;
            }
        }

        /// <summary>
        /// Указать, что накладная "распроведена".
        /// </summary>
        /// <param name="regId">Идентификатор ЕГАИС для накладной.</param>
        /// <param name="conclusion">Результат операции.</param>
        /// <param name="comment">Комментарий.</param>
        protected void unconfirmOutWaybill(string regId, string conclusion, string comment)
        {
            try
            {
                Program.Logger.Info(this,
                    string.Format("Попытка распроведения исходящих накладных по идентификатору '{0}'...", regId));

                if (string.IsNullOrWhiteSpace(regId))
                    throw new Exception("Идентификатор операции распроведения не может быть пустым.");

                OutWaybill waybill = outWaybills.FirstOrDefault(item =>
                    ((item.StatusEnum == OutWaybillStatus.Confirmed) && (item.WBRegId == regId)));

                if (waybill != null)
                {
                    Program.Logger.Info(this,
                        string.Format("\t... найдена исходящая накладная для распроведения: '{0}'...",
                            waybill.Description));
                    Program.Logger.Info(this,
                        string.Format("\t... команда для распроведения: '{0}'; комментарий: '{1}'...", conclusion,
                            comment));

                    waybill.ChangeStatus(OutWaybillStatus.Rejected);
                    waybill.SetAdditionalComment(comment);

                    outWaybills.ForceChangeListEvent();

                    storage.Save(waybill);

                    Program.Logger.Info(this,
                        string.Format("... исходящая накладная '{0}' распроведена и сохранена...",
                            waybill.Description));

                    onlineEvents.Add(new OnlineEvent(OnlineEventStatus.Warning, this,
                        string.Format(
                            "Исходящая накладная с номером '{0}' ('{1}') для '{2}' была распроведена. Причина отказа предоставлена в соответствующей квитанции сервера (смотрите окно 'Список квитанций-ответов сервера').",
                            waybill.Number, waybill.Date, waybill.ConsigneeName)));
                }
                else
                {
                    Program.Logger.Info(this, "... документ для распроведения не найден...");
                }

                Program.Logger.Info(this,
                    string.Format(
                        "... попытка распроведения исходящих накладных по идентификатору '{0}' успешно завершена.",
                        regId));
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время проведения операции распроведения исходящей накладной произошла ошибка.",
                    exception);

                throw;
            }
        }

        #endregion OutWaybill...

        #region ActChargeOn...

        /// <summary>
        /// Добавить акт в список (по необходимости) и записать в хранилище.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void addAndSave(ActChargeOn act)
        {
            if (actChargeOnList.All(item => item.SurrogateId != act.SurrogateId))
            {
                actChargeOnList.Add(act);
            }

            Storage.Save(act);
        }

        /// <summary>
        /// Удалить документ.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void delete(ActChargeOn act)
        {
            if (actChargeOnList.Any(item => item.SurrogateId == act.SurrogateId))
            {
                actChargeOnList.Remove(act);
            }

            Storage.Delete(act);
        }

        /// <summary>
        /// Отправить акт.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void send(ActChargeOn act)
        {
            if (act.StatusEnum != MovementStatus.New)
            {
                throw new Exception(
                    "Отправить можно только такой акт, который находится в статусе 'новый, готов к отправке'.");
            }

            XmlDocument xmlDocument = buildOutXmlDocument(act);

            #region Сохранение исходящего запроса.

            xmlDocument.Save(string.Format("{0}\\{1}.actchargeon.{2}.{3}", storage.PathOut, FileStorage.NativePrefix,
                DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));

            #endregion Сохранение исходящего запроса.

            Program.Logger.Info(this, string.Format("Попытка отправить данные '{0}' по адресу '{1}'...",
                xmlDocument.OuterXml,
                (documentsVersion == 1) ? addresses.SendActChargeOn : addresses.SendActChargeOn_v2));

            string answer = HttpTransport.UploadFile(
                (documentsVersion == 1) ? addresses.SendActChargeOn : addresses.SendActChargeOn_v2,
                xmlDocument, "xml_file", "text/xml; charset=utf-8", configuration.UtmTimeoutLong);

            Program.Logger.Info(this, string.Format("... данные успешно отправлены; получен ответ '{0}'.", answer));

            // Увы, таковы реалии ЕГАИС: идентификатор запроса возвращается в ноде "url".
            act.SetSendReplyId(getNodeValueFromXMLAnswer(answer, "url"));

            act.AddOutData(xmlDocument);
            act.ChangeStatus(MovementStatus.Sent);

            storage.Save(act);
        }

        /// <summary>
        /// Построить исходящий xml-документ.
        /// </summary>
        /// <returns>Xml-документ</returns>
        protected virtual XmlDocument buildOutXmlDocument(ActChargeOn act)
        {
            Program.Logger.Info(this,
                string.Format("Попытка построить исходящий xml-документ для акта '{0}'...", act.Description));

            act.Check(false);

            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            #region Схемы...

            XmlNode docs = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docs, "Version", "1.0");
            addAttribute(xml, docs, "xmlns:" + addresses.Xsi.Prefix, addresses.Xsi.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.Ns.Prefix, addresses.Ns.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.Ce.Prefix, addresses.Ce.Uri);

            XmlPrefix oref, pref, iab, ain;

            if (documentsVersion == 1)
            {
                oref = addresses.Oref;
                pref = addresses.Pref;
                ain = addresses.Ain;
                iab = addresses.Iab;
            }
            else
            {
                oref = addresses.Oref2;
                pref = addresses.Pref2;
                ain = addresses.Ain2;
                iab = addresses.Iab2;
            }

            addAttribute(xml, docs, "xmlns:" + oref.Prefix, oref.Uri);
            addAttribute(xml, docs, "xmlns:" + pref.Prefix, pref.Uri);
            addAttribute(xml, docs, "xmlns:" + ain.Prefix, ain.Uri);
            addAttribute(xml, docs, "xmlns:" + iab.Prefix, iab.Uri);
            xml.AppendChild(docs);

            #endregion Схемы...

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docs.AppendChild(owner);

            addNodeToXmlDocument(xml, owner, addresses.Ns, "FSRAR_ID", configuration.FsrarId);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docs.AppendChild(doc);

            XmlNode bodyAct = (documentsVersion == 1)
                ? xml.CreateElement(addresses.Ns.Prefix, "ActChargeOn", addresses.Ns.Uri)
                : xml.CreateElement(addresses.Ns.Prefix, "ActChargeOn_v2", addresses.Ns.Uri);
            doc.AppendChild(bodyAct);

            XmlNode header = xml.CreateElement(ain.Prefix, "Header", ain.Uri);
            bodyAct.AppendChild(header);

            if (string.IsNullOrWhiteSpace(act.Number)) throw new Exception("В акте отсутствует номер ('Number').");

            addNodeToXmlDocument(xml, header, ain, "Number", act.Number);
            addNodeToXmlDocument(xml, header, ain, "ActDate", act.DateShort);
            addNodeToXmlDocument(xml, header, ain, "Note", act.Note);
            if (documentsVersion != 1)
            {
                addNodeToXmlDocument(xml, header, ain, "TypeChargeOn", act.Reason);
            }

            XmlNode content = xml.CreateElement(ain.Prefix, "Content", ain.Uri);
            bodyAct.AppendChild(content);

            if (act.MovePositions.Count == 0)
                throw new Exception("Список позиций акта движения товара пуст. Отправка не имеет смысла.");

            foreach (MovePosition position in act.MovePositions)
            {
                position.Check();

                XmlNode positionNode = xml.CreateElement(ain.Prefix, "Position", ain.Uri);
                content.AppendChild(positionNode);

                addNodeToXmlDocument(xml, positionNode, ain, "Identity", position.Identity.ToString("D"));

                #region Продукт...

                XmlNode product = xml.CreateElement(ain.Prefix, "Product", ain.Uri);
                positionNode.AppendChild(product);

                if (string.IsNullOrWhiteSpace(position.XmlProductionBody))
                    throw new Exception("В позиции акта присутствует алкогольная продукция с незаполненным xml-телом.");

                XmlDocument xmlProduct = new XmlDocument();
                if (documentsVersion < definePartnerVersion(position.XmlProductionBody))
                    throw new Exception(
                        "Ошибка составления: в акт версии 'v1' добавляется информация о продукции версии 'v2'.");
                xmlProduct.LoadXml(documentsVersion == 1
                    ? position.XmlProductionBody
                    : convertPartnerToV2(position.XmlProductionBody));
                importToXmlDocument(xml, product, xmlProduct);

                #region Fucking EGAIS...

                if (documentsVersion > 1)
                {
                    if (product["pref:Importer"] != null)
                    {
                        product.RemoveChild(product["pref:Importer"]);
                    }

                    if ((product["pref:Producer"] != null)
                        && (product["pref:Producer"]["oref:UL"] != null)
                        && (product["pref:Producer"]["oref:UL"]["oref:address"] != null))
                    {
                        XmlNode addressNode = product["pref:Producer"]["oref:UL"]["oref:address"];

                        if (addressNode.ChildNodes.Cast<XmlNode>()
                            .All(child => child.Name.ToLower() != "oref:regioncode"))
                        {
                            addNodeToXmlDocument(xml, addressNode, oref, "RegionCode", "00");
                        }
                    }

                    if (product["pref:UnitType"] == null)
                        addNodeToXmlDocument(xml, product, pref, "UnitType", "Packed");
                    if (product["pref:Type"] == null) addNodeToXmlDocument(xml, product, pref, "Type", "АП");
                    if (product["pref:ShortName"] == null)
                        addNodeToXmlDocument(xml, product, pref, "ShortName", string.Empty, false);
                }

                #endregion Fucking EGAIS...

                #endregion Продукт...

                addNodeToXmlDocument(xml, positionNode, ain, "Quantity", convertToString(position.Quantity));

                #region Справки "А" и "Б"...

                XmlNode informAB = (documentsVersion == 1)
                    ? xml.CreateElement(ain.Prefix, "InformAB", ain.Uri)
                    : xml.CreateElement(ain.Prefix, "InformF1F2", ain.Uri);
                positionNode.AppendChild(informAB);

                XmlNode informABReg = (documentsVersion == 1)
                    ? xml.CreateElement(ain.Prefix, "InformABReg", ain.Uri)
                    : xml.CreateElement(ain.Prefix, "InformF1F2Reg", ain.Uri);
                informAB.AppendChild(informABReg);

                XmlNode informA = (documentsVersion == 1)
                    ? xml.CreateElement(ain.Prefix, "InformA", ain.Uri)
                    : xml.CreateElement(ain.Prefix, "InformF1", ain.Uri);
                informABReg.AppendChild(informA);

                addNodeToXmlDocument(xml, informA, iab, "Quantity", convertToString(position.Quantity));
                addNodeToXmlDocument(xml, informA, iab, "BottlingDate", position.BottlingDateShort);
                addNodeToXmlDocument(xml, informA, iab, "TTNNumber", position.TTNNumber);
                addNodeToXmlDocument(xml, informA, iab, "TTNDate", position.TTNDateShort);

                if ((position.UseScan) && (position.BottlingDate >= BorderBottlingDate))
                {
                    addNodeToXmlDocument(xml, informA, iab, "EGAISFixNumber", position.EGAISNumber);
                    addNodeToXmlDocument(xml, informA, iab, "EGAISFixDate", position.EGAISDateShort);
                }

                #endregion Справки "А" и "Б"...

                #region Штрих-коды...

                if (position.UseScan)
                {
                    if (position.Quantity != position.PDF417Codes.Count)
                    {
                        throw new Exception(
                            "Величина 'Количество' не соответствует количеству отсканированных двумерных штрих-кодов.");
                    }

                    XmlNode markCodeInfo = xml.CreateElement(ain.Prefix, "MarkCodeInfo", ain.Uri);
                    positionNode.AppendChild(markCodeInfo);

                    foreach (string pdf417Code in position.PDF417Codes)
                    {
                        addNodeToXmlDocument(xml, markCodeInfo, ain, "MarkCode", pdf417Code);
                    }
                }

                #endregion Штрих-коды...
            }

            Program.Logger.Info(this,
                string.Format("... построение исходящего xml-документа длиной {0} символов успешно завершено.",
                    xml.OuterXml.Length));

            return xml;
        }

        /// <summary>
        /// Обработать акт по квитанции.
        /// </summary>
        /// <param name="ticket">Квитанция.</param>
        protected virtual void conclusionActChargeOn(Ticket ticket)
        {
            try
            {
                Program.Logger.Info(this,
                    string.Format("Попытка обработки актов движения товара '{0}) по квитанции '{1}'...",
                        typeof(ActChargeOn).Name, ticket.Description));

                ActChargeOn act = actChargeOnList.FirstOrDefault(item => item.SendReplyId == ticket.ReplyId);

                if (act != null)
                {
                    conclusionMovementByTicket(ticket, act);

                    actChargeOnList.ForceChangeListEvent();
                }
                else
                {
                    Program.Logger.Info(this, "... документ для обработки не найден...");
                }

                Program.Logger.Info(this,
                    string.Format(
                        "... попытка обработки актов движения товара '{0}' по квитанции '{1}' успешно завершена",
                        typeof(ActChargeOn).Name, ticket.Description));
            }
            catch (Exception exception)
            {
                Program.Logger.Error(
                    "Во время проведения обработки актов движения товара по полученной квитанции произошла ошибка.",
                    exception);

                throw;
            }
        }

        /// <summary>
        /// Отменить проведение акта.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void repeal(ActChargeOn act)
        {
            if (act.StatusEnum != MovementStatus.Confirmed)
            {
                throw new Exception(
                    "Отменить можно только такой акт, который находится в статусе 'документ подтверждён ЕГАИС'.");
            }

            XmlDocument xmlOutDocument = buildRepealXmlDocument(act);

            #region Сохранение исходящего запроса.

            xmlOutDocument.Save(string.Format("{0}\\{1}.repealactchargeon.{2}.{3}", storage.PathOut,
                FileStorage.NativePrefix, DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));

            #endregion Сохранение исходящего запроса.

            Program.Logger.Info(this,
                string.Format("Попытка отправить данные '{0}' по адресу '{1}'...", xmlOutDocument.OuterXml,
                    addresses.RepealActChargeOn));

            string answer = HttpTransport.UploadFile(addresses.RepealActChargeOn, xmlOutDocument, "xml_file",
                "text/xml; charset=utf-8", configuration.UtmTimeoutLong);

            Program.Logger.Info(this, string.Format("... данные успешно отправлены; получен ответ '{0}'.", answer));

            // Увы, таковы реалии ЕГАИС: идентификатор запроса возвращается в ноде "url".
            act.SetRepealReplyId(getNodeValueFromXMLAnswer(answer, "url"));

            act.AddRepealData(xmlOutDocument);
            act.ChangeStatus(MovementStatus.Repealed);

            storage.Save(act);
        }

        /// <summary>
        /// Построить запрос на отмену проведение акта.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual XmlDocument buildRepealXmlDocument(ActChargeOn act)
        {
            Program.Logger.Info(this,
                string.Format("Попытка построить исходящий xml-документ для акта '{0}'...", act.Description));

            act.Check(false);

            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            #region Схемы...

            XmlNode docs = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docs, "Version", "1.0");
            addAttribute(xml, docs, "xmlns:" + addresses.Xsi.Prefix, addresses.Xsi.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.Ns.Prefix, addresses.Ns.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.QpAco.Prefix, addresses.QpAco.Uri);
            xml.AppendChild(docs);

            #endregion Схемы...

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docs.AppendChild(owner);

            addNodeToXmlDocument(xml, owner, addresses.Ns, "FSRAR_ID", configuration.FsrarId);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docs.AppendChild(doc);

            XmlNode requestRepealACO = xml.CreateElement(addresses.Ns.Prefix, "RequestRepealACO", addresses.Ns.Uri);
            doc.AppendChild(requestRepealACO);

            addNodeToXmlDocument(xml, requestRepealACO, addresses.QpAco, "ClientId", configuration.FsrarId);
            addNodeToXmlDocument(xml, requestRepealACO, addresses.QpAco, "RequestNumber",
                string.Format("RepealACO-{0}", DateTime.Now.ToString("yyMMddHHmmss")));
            addNodeToXmlDocument(xml, requestRepealACO, addresses.QpAco, "RequestDate",
                DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));
            addNodeToXmlDocument(xml, requestRepealACO, addresses.QpAco, "ACORegId", act.EgaisRegId);

            Program.Logger.Info(this,
                string.Format("... построение исходящего xml-документа длиной {0} символов успешно завершено.",
                    xml.OuterXml.Length));

            return xml;
        }

        #endregion ActChargeOn...

        #region ActFixBarcode...

        /// <summary>
        /// Добавить акт в список (по необходимости) и записать в хранилище.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void addAndSave(ActFixBarcode act)
        {
            if (actFixBarcodeList.All(item => item.SurrogateId != act.SurrogateId))
            {
                actFixBarcodeList.Add(act);
            }

            Storage.Save(act);
        }

        /// <summary>
        /// Удалить документ.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void delete(ActFixBarcode act)
        {
            if (actFixBarcodeList.Any(item => item.SurrogateId == act.SurrogateId))
            {
                actFixBarcodeList.Remove(act);
            }

            Storage.Delete(act);
        }

        /// <summary>
        /// Отправить акт.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void send(ActFixBarcode act)
        {
            if (act.StatusEnum != MovementStatus.New)
            {
                throw new Exception(
                    "Отправить можно только такой акт, который находится в статусе 'новый, готов к отправке'.");
            }

            XmlDocument xmlDocument = buildOutXmlDocument(act);

            #region Сохранение исходящего запроса.

            xmlDocument.Save(
                $"{storage.PathOut}\\{FileStorage.NativePrefix}.actfixbarcode.{DateTime.Now.ToString("yyyyMMddHHmmss")}.{FileStorage.NativeExtension}");

            #endregion Сохранение исходящего запроса.

            Program.Logger.Info(this,
                $"Попытка отправить данные '{xmlDocument.OuterXml}' по адресу '{addresses.SendActFixBarcode}'...");

            string answer = HttpTransport.UploadFile(addresses.SendActFixBarcode,
                xmlDocument, "xml_file", "text/xml; charset=utf-8", configuration.UtmTimeoutLong);

            Program.Logger.Info(this, $"... данные успешно отправлены; получен ответ '{answer}'.");

            // Увы, таковы реалии ЕГАИС: идентификатор запроса возвращается в ноде "url".
            act.SetSendReplyId(getNodeValueFromXMLAnswer(answer, "url"));

            act.AddOutData(xmlDocument);
            act.ChangeStatus(MovementStatus.Sent);

            storage.Save(act);
        }

        /// <summary>
        /// Построить исходящий xml-документ.
        /// </summary>
        /// <returns>Xml-документ</returns>
        protected virtual XmlDocument buildOutXmlDocument(ActFixBarcode act)
        {
            Program.Logger.Info(this,
                string.Format("Попытка построить исходящий xml-документ для акта '{0}'...", act.Description));

            act.Check(false);

            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            #region Схемы...

            XmlNode docs = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docs, "Version", "1.0");
            addAttribute(xml, docs, "xmlns:" + addresses.Xsi.Prefix, addresses.Xsi.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.Pref2.Prefix, addresses.Pref2.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.AwrFixBc.Prefix, addresses.AwrFixBc.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.Ce3.Prefix, addresses.Ce3.Uri);

            XmlPrefix oref, pref, awr;

            oref = addresses.Oref2;
            pref = addresses.Pref2;
            awr = addresses.AwrFixBc;

            xml.AppendChild(docs);

            #endregion Схемы...

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docs.AppendChild(owner);

            addNodeToXmlDocument(xml, owner, addresses.Ns, "FSRAR_ID", configuration.FsrarId);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docs.AppendChild(doc);

            XmlNode bodyAct = xml.CreateElement(addresses.Ns.Prefix, "ActFixBarCode", addresses.Ns.Uri);
            doc.AppendChild(bodyAct);

            if (string.IsNullOrWhiteSpace(act.Identity))
                throw new Exception("В акте отсутствует клиентский идентификатор ('Identity').");
            addNodeToXmlDocument(xml, bodyAct, awr, "Identity", act.Identity);

            XmlNode header = xml.CreateElement(awr.Prefix, "Header", awr.Uri);
            bodyAct.AppendChild(header);

            if (string.IsNullOrWhiteSpace(act.Number)) throw new Exception("В акте отсутствует номер ('Number').");

            addNodeToXmlDocument(xml, header, awr, "Number", act.Number);
            addNodeToXmlDocument(xml, header, awr, "ActDate", act.DateShort);
            addNodeToXmlDocument(xml, header, awr, "Note", act.Note);

            XmlNode content = xml.CreateElement(awr.Prefix, "Content", awr.Uri);
            bodyAct.AppendChild(content);

            if (act.MovePositions.Count == 0)
                throw new Exception("Список позиций акта движения товара пуст. Отправка не имеет смысла.");

            foreach (MovePosition position in act.MovePositions)
            {
                position.Check();

                XmlNode positionNode = xml.CreateElement(awr.Prefix, "Position", awr.Uri);
                content.AppendChild(positionNode);

                addNodeToXmlDocument(xml, positionNode, awr, "Identity", position.Identity.ToString("D"));


                XmlNode informBReg = xml.CreateElement(awr.Prefix, "Inform2RegId", awr.Uri);
                informBReg.InnerText = position.FormBRegId;
                positionNode.AppendChild(informBReg);


                #region Штрих-коды...

                XmlNode markInfo = xml.CreateElement(awr.Prefix, "MarkInfo", awr.Uri);
                positionNode.AppendChild(markInfo);

                foreach (string pdf417Code in position.PDF417Codes)
                {
                    addNodeToXmlDocument(xml, markInfo, addresses.Ce3, "amc", pdf417Code);
                }

                #endregion Штрих-коды...
            }

            Program.Logger.Info(this,
                string.Format("... построение исходящего xml-документа длиной {0} символов успешно завершено.",
                    xml.OuterXml.Length));

            return xml;
        }

        /// <summary>
        /// Обработать акт по квитанции.
        /// </summary>
        /// <param name="ticket">Квитанция.</param>
        protected virtual void conclusionActFixBarcode(Ticket ticket)
        {
            try
            {
                Program.Logger.Info(this,
                    string.Format("Попытка обработки актов движения товара '{0}) по квитанции '{1}'...",
                        typeof(ActFixBarcode).Name, ticket.Description));

                ActFixBarcode act = actFixBarcodeList.FirstOrDefault(item => item.SendReplyId == ticket.ReplyId);

                if (act != null)
                {
                    conclusionMovementByTicket(ticket, act);

                    actFixBarcodeList.ForceChangeListEvent();
                }
                else
                {
                    Program.Logger.Info(this, "... документ для обработки не найден...");
                }

                Program.Logger.Info(this,
                    string.Format(
                        "... попытка обработки актов движения товара '{0}' по квитанции '{1}' успешно завершена",
                        typeof(ActChargeOn).Name, ticket.Description));
            }
            catch (Exception exception)
            {
                Program.Logger.Error(
                    "Во время проведения обработки актов движения товара по полученной квитанции произошла ошибка.",
                    exception);

                throw;
            }
        }

        #endregion ActFixBarcode...

        #region ActUnFixBarcode...

        /// <summary>
        /// Добавить акт в список (по необходимости) и записать в хранилище.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void addAndSave(ActUnFixBarcode act)
        {
            if (actUnFixBarcodeList.All(item => item.SurrogateId != act.SurrogateId))
            {
                actUnFixBarcodeList.Add(act);
            }

            Storage.Save(act);
        }

        /// <summary>
        /// Удалить документ.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void delete(ActUnFixBarcode act)
        {
            if (actUnFixBarcodeList.Any(item => item.SurrogateId == act.SurrogateId))
            {
                actUnFixBarcodeList.Remove(act);
            }

            Storage.Delete(act);
        }

        /// <summary>
        /// Отправить акт.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void send(ActUnFixBarcode act)
        {
            if (act.StatusEnum != MovementStatus.New)
            {
                throw new Exception(
                    "Отправить можно только такой акт, который находится в статусе 'новый, готов к отправке'.");
            }

            XmlDocument xmlDocument = buildOutXmlDocument(act);

            #region Сохранение исходящего запроса.

            xmlDocument.Save(
                $"{storage.PathOut}\\{FileStorage.NativePrefix}.actunfixbarcode.{DateTime.Now.ToString("yyyyMMddHHmmss")}.{FileStorage.NativeExtension}");

            #endregion Сохранение исходящего запроса.

            Program.Logger.Info(this,
                $"Попытка отправить данные '{xmlDocument.OuterXml}' по адресу '{addresses.SendActUnFixBarcode}'...");

            string answer = HttpTransport.UploadFile(addresses.SendActUnFixBarcode,
                xmlDocument, "xml_file", "text/xml; charset=utf-8", configuration.UtmTimeoutLong);

            Program.Logger.Info(this, $"... данные успешно отправлены; получен ответ '{answer}'.");

            // Увы, таковы реалии ЕГАИС: идентификатор запроса возвращается в ноде "url".
            act.SetSendReplyId(getNodeValueFromXMLAnswer(answer, "url"));

            act.AddOutData(xmlDocument);
            act.ChangeStatus(MovementStatus.Sent);

            storage.Save(act);
        }

        /// <summary>
        /// Построить исходящий xml-документ.
        /// </summary>
        /// <returns>Xml-документ</returns>
        protected virtual XmlDocument buildOutXmlDocument(ActUnFixBarcode act)
        {
            Program.Logger.Info(this,
                string.Format("Попытка построить исходящий xml-документ для акта '{0}'...", act.Description));

            act.Check(false);

            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            #region Схемы...

            XmlNode docs = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docs, "Version", "1.0");
            addAttribute(xml, docs, "xmlns:" + addresses.Xsi.Prefix, addresses.Xsi.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.Pref2.Prefix, addresses.Pref2.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.AwrUnFixBc.Prefix, addresses.AwrUnFixBc.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.Ce3.Prefix, addresses.Ce3.Uri);

            XmlPrefix oref, pref, awr;

            oref = addresses.Oref2;
            pref = addresses.Pref2;
            awr = addresses.AwrFixBc;

            xml.AppendChild(docs);

            #endregion Схемы...

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docs.AppendChild(owner);

            addNodeToXmlDocument(xml, owner, addresses.Ns, "FSRAR_ID", configuration.FsrarId);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docs.AppendChild(doc);

            XmlNode bodyAct = xml.CreateElement(addresses.Ns.Prefix, "ActUnFixBarCode", addresses.Ns.Uri);
            doc.AppendChild(bodyAct);

            if (string.IsNullOrWhiteSpace(act.Identity))
                throw new Exception("В акте отсутствует клиентский идентификатор ('Identity').");
            addNodeToXmlDocument(xml, bodyAct, awr, "Identity", act.Identity);

            XmlNode header = xml.CreateElement(awr.Prefix, "Header", awr.Uri);
            bodyAct.AppendChild(header);

            if (string.IsNullOrWhiteSpace(act.Number)) throw new Exception("В акте отсутствует номер ('Number').");

            addNodeToXmlDocument(xml, header, awr, "Number", act.Number);
            addNodeToXmlDocument(xml, header, awr, "ActDate", act.DateShort);
            addNodeToXmlDocument(xml, header, awr, "Note", act.Note);

            XmlNode content = xml.CreateElement(awr.Prefix, "Content", awr.Uri);
            bodyAct.AppendChild(content);

            if (act.MovePositions.Count == 0)
                throw new Exception("Список позиций акта движения товара пуст. Отправка не имеет смысла.");

            foreach (MovePosition position in act.MovePositions)
            {
                position.Check();

                XmlNode positionNode = xml.CreateElement(awr.Prefix, "Position", awr.Uri);
                content.AppendChild(positionNode);

                addNodeToXmlDocument(xml, positionNode, awr, "Identity", position.Identity.ToString("D"));


                XmlNode informBReg = xml.CreateElement(awr.Prefix, "Inform2RegId", awr.Uri);
                informBReg.InnerText = position.FormBRegId;
                positionNode.AppendChild(informBReg);


                #region Штрих-коды...

                XmlNode markInfo = xml.CreateElement(awr.Prefix, "MarkInfo", awr.Uri);
                positionNode.AppendChild(markInfo);

                foreach (string pdf417Code in position.PDF417Codes)
                {
                    addNodeToXmlDocument(xml, markInfo, addresses.Ce3, "amc", pdf417Code);
                }

                #endregion Штрих-коды...
            }

            Program.Logger.Info(this,
                string.Format("... построение исходящего xml-документа длиной {0} символов успешно завершено.",
                    xml.OuterXml.Length));

            return xml;
        }

        /// <summary>
        /// Обработать акт по квитанции.
        /// </summary>
        /// <param name="ticket">Квитанция.</param>
        protected virtual void conclusionActUnFixBarcode(Ticket ticket)
        {
            try
            {
                Program.Logger.Info(this,
                    string.Format("Попытка обработки актов движения товара '{0}) по квитанции '{1}'...",
                        typeof(ActUnFixBarcode).Name, ticket.Description));

                ActUnFixBarcode act = actUnFixBarcodeList.FirstOrDefault(item => item.SendReplyId == ticket.ReplyId);

                if (act != null)
                {
                    conclusionMovementByTicket(ticket, act);

                    actUnFixBarcodeList.ForceChangeListEvent();
                }
                else
                {
                    Program.Logger.Info(this, "... документ для обработки не найден...");
                }

                Program.Logger.Info(this,
                    string.Format(
                        "... попытка обработки актов движения товара '{0}' по квитанции '{1}' успешно завершена",
                        typeof(ActChargeOn).Name, ticket.Description));
            }
            catch (Exception exception)
            {
                Program.Logger.Error(
                    "Во время проведения обработки актов движения товара по полученной квитанции произошла ошибка.",
                    exception);

                throw;
            }
        }

        #endregion ActFixBarcode...

        #region ActChargeOff...

        /// <summary>
        /// Добавить акт в список (по необходимости) и записать в хранилище.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void addAndSave(ActChargeOff act)
        {
            if (actChargeOffList.All(item => item.SurrogateId != act.SurrogateId))
            {
                actChargeOffList.Add(act);
            }

            Storage.Save(act);
        }

        /// <summary>
        /// Удалить документ.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void delete(ActChargeOff act)
        {
            if (actChargeOffList.Any(item => item.SurrogateId == act.SurrogateId))
            {
                actChargeOffList.Remove(act);
            }

            Storage.Delete(act);
        }

        /// <summary>
        /// Отправить акт.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void send(ActChargeOff act)
        {
            if (act.StatusEnum != MovementStatus.New)
            {
                throw new Exception(
                    "Отправить можно только такой акт, который находится в статусе 'новый, готов к отправке'.");
            }

            XmlDocument xmlDocument = buildOutXmlDocument(act);

            #region Сохранение исходящего запроса.

            xmlDocument.Save(string.Format("{0}\\{1}.actchargeoff.{2}.{3}", storage.PathOut, FileStorage.NativePrefix,
                DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));

            #endregion Сохранение исходящего запроса.

            Program.Logger.Info(this, string.Format("Попытка отправить данные '{0}' по адресу '{1}'...",
                xmlDocument.OuterXml,
                (documentsVersion == 1)
                    ? addresses.SendActChargeOff
                    : addresses.SendActChargeOff_v2(documentsVersion)));

            string answer = HttpTransport.UploadFile(
                (documentsVersion == 1) ? addresses.SendActChargeOff : addresses.SendActChargeOff_v2(documentsVersion),
                xmlDocument, "xml_file", "text/xml; charset=utf-8", configuration.UtmTimeoutLong);

            Program.Logger.Info(this, string.Format("... данные успешно отправлены; получен ответ '{0}'.", answer));

            // Увы, таковы реалии ЕГАИС: идентификатор запроса возвращается в ноде "url".
            act.SetSendReplyId(getNodeValueFromXMLAnswer(answer, "url"));

            act.AddOutData(xmlDocument);
            act.ChangeStatus(MovementStatus.Sent);

            storage.Save(act);
        }

        /// <summary>
        /// Построить исходящий xml-документ.
        /// </summary>
        /// <returns>Xml-документ</returns>
        protected virtual XmlDocument buildOutXmlDocument(ActChargeOff act)
        {
            Program.Logger.Info(this,
                string.Format("Попытка построить исходящий xml-документ для акта '{0}'...", act.Description));

            act.Check(false);

            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            #region Схемы...

            XmlPrefix awr;
            XmlPrefix pref;
            XmlNode docs = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);

            addAttribute(xml, docs, "Version", "1.0");
            addAttribute(xml, docs, "xmlns:" + addresses.Xsi.Prefix, addresses.Xsi.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.Ns.Prefix, addresses.Ns.Uri);
            switch (documentsVersion)
            {
                case 1:
                    awr = addresses.Awr;
                    pref = addresses.Pref;
                    break;
                case 2:
                    awr = addresses.Awr2;
                    pref = addresses.Pref2;

                    addAttribute(xml, docs, "xmlns:" + addresses.Ce.Prefix, addresses.Ce.Uri);
                    break;
                default:
                    awr = addresses.Awr3;
                    pref = addresses.Pref2;

                    addAttribute(xml, docs, "xmlns:" + addresses.Ce3.Prefix, addresses.Ce3.Uri);
                    break;
            }

            addAttribute(xml, docs, "xmlns:" + pref.Prefix, pref.Uri);
            addAttribute(xml, docs, "xmlns:" + awr.Prefix, awr.Uri);

            xml.AppendChild(docs);

            #endregion Схемы...

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docs.AppendChild(owner);

            addNodeToXmlDocument(xml, owner, addresses.Ns, "FSRAR_ID", configuration.FsrarId);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docs.AppendChild(doc);

            XmlNode bodyAct = (documentsVersion == 1)
                ? xml.CreateElement(addresses.Ns.Prefix, "ActWriteOff", addresses.Ns.Uri)
                : xml.CreateElement(addresses.Ns.Prefix, $"ActWriteOff_v{documentsVersion}", addresses.Ns.Uri);
            doc.AppendChild(bodyAct);

            if (string.IsNullOrWhiteSpace(act.Identity))
                throw new Exception("В акте отсутствует клиентский идентификатор ('Identity').");
            addNodeToXmlDocument(xml, bodyAct, awr, "Identity", act.Identity);

            XmlNode header = xml.CreateElement(awr.Prefix, "Header", awr.Uri);
            bodyAct.AppendChild(header);

            if (string.IsNullOrWhiteSpace(act.Number)) throw new Exception("В акте отсутствует номер ('Number').");
            if (string.IsNullOrWhiteSpace(act.Reason)) throw new Exception("В акте отсутствует поле 'Основание'.");

            addNodeToXmlDocument(xml, header, awr, "ActNumber", act.Number);
            addNodeToXmlDocument(xml, header, awr, "ActDate", act.DateShort);
            addNodeToXmlDocument(xml, header, awr, "TypeWriteOff", act.Reason);
            addNodeToXmlDocument(xml, header, awr, "Note", act.Note);

            XmlNode content = xml.CreateElement(awr.Prefix, "Content", awr.Uri);
            bodyAct.AppendChild(content);

            if (act.MovePositions.Count == 0)
                throw new Exception("Список позиций акта движения товара пуст. Отправка не имеет смысла.");

            foreach (MovePosition position in act.MovePositions)
            {
                position.Check();

                XmlNode positionNode = xml.CreateElement(awr.Prefix, "Position", awr.Uri);
                content.AppendChild(positionNode);

                addNodeToXmlDocument(xml, positionNode, awr, "Identity", position.Identity.ToString("D"));
                addNodeToXmlDocument(xml, positionNode, awr, "Quantity", convertToString(position.Quantity));

                switch (documentsVersion)
                {
                    case 1:
                        XmlNode informB = xml.CreateElement(awr.Prefix, "InformB", awr.Uri);
                        positionNode.AppendChild(informB);

                        addNodeToXmlDocument(xml, informB, pref, "BRegId", position.FormBRegId);
                        break;
                    case 2:
                        AddInformF1F2(xml, awr, positionNode, pref, position);
                        break;
                    default:
                        AddInformF1F2(xml, awr, positionNode, pref, position);
                        if (position.PDF417Codes.Count > 0)
                        {
                            XmlPrefix ce = addresses.Ce3;
                            XmlNode markInfo = xml.CreateElement(awr.Prefix, "MarkCodeInfo", awr.Uri);
                            positionNode.AppendChild(markInfo);

                            foreach (var amc in position.PDF417Codes)
                            {
                                addNodeToXmlDocument(xml, markInfo, ce, "amc", amc);
                            }
                        }

                        break;
                }
            }

            Program.Logger.Info(this,
                string.Format("... построение исходящего xml-документа длиной {0} символов успешно завершено.",
                    xml.OuterXml.Length));

            return xml;
        }

        private void AddInformF1F2(XmlDocument xml, XmlPrefix awr, XmlNode positionNode, XmlPrefix pref,
            MovePosition position)
        {
            XmlNode informF1F2 = xml.CreateElement(awr.Prefix, "InformF1F2", awr.Uri);
            positionNode.AppendChild(informF1F2);

            XmlNode informF2 = xml.CreateElement(awr.Prefix, "InformF2", awr.Uri);
            informF1F2.AppendChild(informF2);

            addNodeToXmlDocument(xml, informF2, pref, "F2RegId", position.FormBRegId);
        }

        /// <summary>
        /// Обработать акт по квитанции.
        /// </summary>
        /// <param name="ticket">Квитанция.</param>
        protected virtual void conclusionActChargeOff(Ticket ticket)
        {
            try
            {
                Program.Logger.Info(this,
                    string.Format("Попытка обработки актов движения товара '{0}) по квитанции '{1}'...",
                        typeof(ActChargeOff).Name, ticket.Description));

                ActChargeOff act = actChargeOffList.FirstOrDefault(item => item.SendReplyId == ticket.ReplyId);

                if (act != null)
                {
                    conclusionMovementByTicket(ticket, act);

                    actChargeOffList.ForceChangeListEvent();
                }
                else
                {
                    Program.Logger.Info(this, "... документ для обработки не найден...");
                }

                Program.Logger.Info(this,
                    string.Format(
                        "... попытка обработки актов движения товара '{0}' по квитанции '{1}' успешно завершена",
                        typeof(ActChargeOff).Name, ticket.Description));
            }
            catch (Exception exception)
            {
                Program.Logger.Error(
                    "Во время проведения обработки актов движения товара по полученной квитанции произошла ошибка.",
                    exception);

                throw;
            }
        }

        /// <summary>
        /// Отменить проведение акта.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void repeal(ActChargeOff act)
        {
            if (act.StatusEnum != MovementStatus.Confirmed)
            {
                throw new Exception(
                    "Отменить можно только такой акт, который находится в статусе 'документ подтверждён ЕГАИС'.");
            }

            XmlDocument xmlOutDocument = buildRepealXmlDocument(act);

            #region Сохранение исходящего запроса.

            xmlOutDocument.Save(string.Format("{0}\\{1}.repealactchargeoff.{2}.{3}", storage.PathOut,
                FileStorage.NativePrefix, DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));

            #endregion Сохранение исходящего запроса.

            Program.Logger.Info(this,
                string.Format("Попытка отправить данные '{0}' по адресу '{1}'...", xmlOutDocument.OuterXml,
                    addresses.RepealActChargeOff));

            string answer = HttpTransport.UploadFile(addresses.RepealActChargeOff, xmlOutDocument, "xml_file",
                "text/xml; charset=utf-8", configuration.UtmTimeoutLong);

            Program.Logger.Info(this, string.Format("... данные успешно отправлены; получен ответ '{0}'.", answer));

            // Увы, таковы реалии ЕГАИС: идентификатор запроса возвращается в ноде "url".
            act.SetRepealReplyId(getNodeValueFromXMLAnswer(answer, "url"));

            act.AddRepealData(xmlOutDocument);
            act.ChangeStatus(MovementStatus.Repealed);

            storage.Save(act);
        }

        /// <summary>
        /// Построить запрос на отмену проведение акта.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual XmlDocument buildRepealXmlDocument(ActChargeOff act)
        {
            Program.Logger.Info(this,
                string.Format("Попытка построить исходящий xml-документ для акта '{0}'...", act.Description));

            act.Check(false);

            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            #region Схемы...

            XmlNode docs = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docs, "Version", "1.0");
            addAttribute(xml, docs, "xmlns:" + addresses.Xsi.Prefix, addresses.Xsi.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.Ns.Prefix, addresses.Ns.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.QpAwo.Prefix, addresses.QpAwo.Uri);
            xml.AppendChild(docs);

            #endregion Схемы...

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docs.AppendChild(owner);

            addNodeToXmlDocument(xml, owner, addresses.Ns, "FSRAR_ID", configuration.FsrarId);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docs.AppendChild(doc);

            XmlNode requestRepealAWO = xml.CreateElement(addresses.Ns.Prefix, "RequestRepealAWO", addresses.Ns.Uri);
            doc.AppendChild(requestRepealAWO);

            addNodeToXmlDocument(xml, requestRepealAWO, addresses.QpAwo, "ClientId", configuration.FsrarId);
            addNodeToXmlDocument(xml, requestRepealAWO, addresses.QpAwo, "RequestNumber",
                string.Format("RepealAWO-{0}", DateTime.Now.ToString("yyMMddHHmmss")));
            addNodeToXmlDocument(xml, requestRepealAWO, addresses.QpAwo, "RequestDate",
                DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss"));
            addNodeToXmlDocument(xml, requestRepealAWO, addresses.QpAwo, "AWORegId", act.EgaisRegId);

            Program.Logger.Info(this,
                string.Format("... построение исходящего xml-документа длиной {0} символов успешно завершено.",
                    xml.OuterXml.Length));

            return xml;
        }

        #endregion ActChargeOff..

        #region ActChargeOnShop...

        /// <summary>
        /// Добавить акт в список (по необходимости) и записать в хранилище.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void addAndSave(ActChargeOnShop act)
        {
            if (actChargeOnShopList.All(item => item.SurrogateId != act.SurrogateId))
            {
                actChargeOnShopList.Add(act);
            }

            Storage.Save(act);
        }

        /// <summary>
        /// Удалить документ.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void delete(ActChargeOnShop act)
        {
            if (actChargeOnShopList.Any(item => item.SurrogateId == act.SurrogateId))
            {
                actChargeOnShopList.Remove(act);
            }

            Storage.Delete(act);
        }

        /// <summary>
        /// Отправить акт.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void send(ActChargeOnShop act)
        {
            if (act.StatusEnum != MovementStatus.New)
            {
                throw new Exception(
                    "Отправить можно только такой акт, который находится в статусе 'новый, готов к отправке'.");
            }

            XmlDocument xmlOutDocument = buildOutXmlDocument(act);

            #region Сохранение исходящего запроса.

            xmlOutDocument.Save(string.Format("{0}\\{1}.actchargeonshop.{2}.{3}", storage.PathOut,
                FileStorage.NativePrefix, DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));

            #endregion Сохранение исходящего запроса.

            Program.Logger.Info(this,
                string.Format("Попытка отправить данные '{0}' по адресу '{1}'...", xmlOutDocument.OuterXml,
                    addresses.SendActChargeOnShop));

            string answer = HttpTransport.UploadFile(addresses.SendActChargeOnShop, xmlOutDocument, "xml_file",
                "text/xml; charset=utf-8", configuration.UtmTimeoutLong);

            Program.Logger.Info(this, string.Format("... данные успешно отправлены; получен ответ '{0}'.", answer));

            // Увы, таковы реалии ЕГАИС: идентификатор запроса возвращается в ноде "url".
            act.SetSendReplyId(getNodeValueFromXMLAnswer(answer, "url"));

            act.AddOutData(xmlOutDocument);
            act.ChangeStatus(MovementStatus.Sent);

            storage.Save(act);
        }

        /// <summary>
        /// Построить исходящий xml-документ.
        /// </summary>
        /// <returns>Xml-документ</returns>
        protected virtual XmlDocument buildOutXmlDocument(ActChargeOnShop act)
        {
            Program.Logger.Info(this,
                string.Format("Попытка построить исходящий xml-документ для акта '{0}'...", act.Description));

            act.Check(false);

            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            #region Схемы...

            XmlNode docs = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docs, "Version", "1.0");
            addAttribute(xml, docs, "xmlns:" + addresses.Xsi.Prefix, addresses.Xsi.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.Ns.Prefix, addresses.Ns.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.Oref2.Prefix, addresses.Oref2.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.Pref2.Prefix, addresses.Pref2.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.Ainp.Prefix, addresses.Ainp.Uri);
            xml.AppendChild(docs);

            #endregion Схемы...

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docs.AppendChild(owner);

            addNodeToXmlDocument(xml, owner, addresses.Ns, "FSRAR_ID", configuration.FsrarId);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docs.AppendChild(doc);

            XmlNode bodyAct = xml.CreateElement(addresses.Ns.Prefix, "ActChargeOnShop_v2", addresses.Ns.Uri);
            doc.AppendChild(bodyAct);

            if (string.IsNullOrWhiteSpace(act.Identity))
                throw new Exception("В акте отсутствует клиентский идентификатор ('Identity').");
            addNodeToXmlDocument(xml, bodyAct, addresses.Ainp, "Identity", act.Identity);

            XmlNode header = xml.CreateElement(addresses.Ainp.Prefix, "Header", addresses.Ainp.Uri);
            bodyAct.AppendChild(header);

            if (string.IsNullOrWhiteSpace(act.Number)) throw new Exception("В акте отсутствует номер ('Number').");
            if (string.IsNullOrWhiteSpace(act.Reason)) throw new Exception("В акте отсутствует поле 'Основание'.");

            addNodeToXmlDocument(xml, header, addresses.Ainp, "ActDate", act.DateShort);
            addNodeToXmlDocument(xml, header, addresses.Ainp, "Number", act.Number);
            addNodeToXmlDocument(xml, header, addresses.Ainp, "TypeChargeOn", act.Reason);
            addNodeToXmlDocument(xml, header, addresses.Ainp, "Note", act.Note);

            XmlNode content = xml.CreateElement(addresses.Ainp.Prefix, "Content", addresses.Ainp.Uri);
            bodyAct.AppendChild(content);

            foreach (MovePosition position in act.MovePositions)
            {
                position.Check();

                XmlNode positionNode = xml.CreateElement(addresses.Ainp.Prefix, "Position", addresses.Ainp.Uri);
                content.AppendChild(positionNode);

                addNodeToXmlDocument(xml, positionNode, addresses.Ainp, "Identity", position.Identity.ToString("D"));

                #region Продукт...

                XmlNode product = xml.CreateElement(addresses.Ainp.Prefix, "Product", addresses.Ainp.Uri);
                positionNode.AppendChild(product);

                if (string.IsNullOrWhiteSpace(position.XmlProductionBody))
                    throw new Exception("В позиции акта присутствует алкогольная продукция с незаполненным xml-телом.");

                XmlDocument xmlProduct = new XmlDocument();
                xmlProduct.LoadXml(convertPartnerToV2(position.XmlProductionBody));
                importToXmlDocument(xml, product, xmlProduct);

                #region Fucking EGAIS...

                if (product["pref:Importer"] != null)
                {
                    product.RemoveChild(product["pref:Importer"]);
                }

                if ((product["pref:Producer"] != null)
                    && (product["pref:Producer"]["oref:UL"] != null)
                    && (product["pref:Producer"]["oref:UL"]["oref:address"] != null))
                {
                    XmlNode addressNode = product["pref:Producer"]["oref:UL"]["oref:address"];

                    if (addressNode.ChildNodes.Cast<XmlNode>().All(child => child.Name.ToLower() != "oref:regioncode"))
                    {
                        addNodeToXmlDocument(xml, addressNode, addresses.Oref2, "RegionCode", "00");
                    }
                }

                if (product["pref:UnitType"] == null)
                    addNodeToXmlDocument(xml, product, addresses.Pref2, "UnitType", "Packed");
                if (product["pref:Type"] == null) addNodeToXmlDocument(xml, product, addresses.Pref2, "Type", "АП");
                if (product["pref:ShortName"] == null)
                    addNodeToXmlDocument(xml, product, addresses.Pref2, "ShortName", string.Empty, false);

                #endregion Fucking EGAIS...

                #endregion Продукт...

                addNodeToXmlDocument(xml, positionNode, addresses.Ainp, "Quantity", convertToString(position.Quantity));
            }

            Program.Logger.Info(this,
                string.Format("... построение исходящего xml-документа длиной {0} символов успешно завершено.",
                    xml.OuterXml.Length));

            return xml;
        }

        /// <summary>
        /// Обработать акт по квитанции.
        /// </summary>
        /// <param name="ticket">Квитанция.</param>
        protected virtual void conclusionActChargeOnShop(Ticket ticket)
        {
            try
            {
                Program.Logger.Info(this,
                    string.Format("Попытка обработки актов движения товара '{0}) по квитанции '{1}'...",
                        typeof(ActChargeOnShop).Name, ticket.Description));

                ActChargeOnShop act = actChargeOnShopList.FirstOrDefault(item => item.SendReplyId == ticket.ReplyId);

                if (act != null)
                {
                    conclusionMovementByTicket(ticket, act);

                    actChargeOnShopList.ForceChangeListEvent();
                }
                else
                {
                    Program.Logger.Info(this, "... документ для обработки не найден...");
                }

                Program.Logger.Info(this,
                    string.Format(
                        "... попытка обработки актов движения товара '{0}' по квитанции '{1}' успешно завершена",
                        typeof(ActChargeOnShop).Name, ticket.Description));
            }
            catch (Exception exception)
            {
                Program.Logger.Error(
                    "Во время проведения обработки актов движения товара по полученной квитанции произошла ошибка.",
                    exception);

                throw;
            }
        }

        #endregion ActChargeOnShop...

        #region ActChargeOffShop...

        /// <summary>
        /// Добавить акт в список (по необходимости) и записать в хранилище.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void addAndSave(ActChargeOffShop act)
        {
            if (actChargeOffShopList.All(item => item.SurrogateId != act.SurrogateId))
            {
                actChargeOffShopList.Add(act);
            }

            Storage.Save(act);
        }

        /// <summary>
        /// Удалить документ.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void delete(ActChargeOffShop act)
        {
            if (actChargeOffShopList.Any(item => item.SurrogateId == act.SurrogateId))
            {
                actChargeOffShopList.Remove(act);
            }

            Storage.Delete(act);
        }

        /// <summary>
        /// Отправить акт.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void send(ActChargeOffShop act)
        {
            if (act.StatusEnum != MovementStatus.New)
            {
                throw new Exception(
                    "Отправить можно только такой акт, который находится в статусе 'новый, готов к отправке'.");
            }

            XmlDocument xmlOutDocument = buildOutXmlDocument(act);

            #region Сохранение исходящего запроса.

            xmlOutDocument.Save(string.Format("{0}\\{1}.actchargeoffshop.{2}.{3}", storage.PathOut,
                FileStorage.NativePrefix, DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));

            #endregion Сохранение исходящего запроса.

            Program.Logger.Info(this,
                string.Format("Попытка отправить данные '{0}' по адресу '{1}'...", xmlOutDocument.OuterXml,
                    addresses.SendActChargeOffShop));

            string answer = HttpTransport.UploadFile(addresses.SendActChargeOffShop, xmlOutDocument, "xml_file",
                "text/xml; charset=utf-8", configuration.UtmTimeoutLong);

            Program.Logger.Info(this, string.Format("... данные успешно отправлены; получен ответ '{0}'.", answer));

            // Увы, таковы реалии ЕГАИС: идентификатор запроса возвращается в ноде "url".
            act.SetSendReplyId(getNodeValueFromXMLAnswer(answer, "url"));

            act.AddOutData(xmlOutDocument);
            act.ChangeStatus(MovementStatus.Sent);

            storage.Save(act);
        }

        /// <summary>
        /// Построить исходящий xml-документ.
        /// </summary>
        /// <returns>Xml-документ</returns>
        protected virtual XmlDocument buildOutXmlDocument(ActChargeOffShop act)
        {
            Program.Logger.Info(this,
                string.Format("Попытка построить исходящий xml-документ для акта '{0}'...", act.Description));

            act.Check(false);

            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            #region Схемы...

            XmlNode docs = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docs, "Version", "1.0");
            addAttribute(xml, docs, "xmlns:" + addresses.Xsi.Prefix, addresses.Xsi.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.Ns.Prefix, addresses.Ns.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.Pref.Prefix, addresses.Pref2.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.Oref.Prefix, addresses.Oref2.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.AwrShop2.Prefix, addresses.AwrShop2.Uri);
            xml.AppendChild(docs);

            #endregion Схемы...

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docs.AppendChild(owner);

            addNodeToXmlDocument(xml, owner, addresses.Ns, "FSRAR_ID", configuration.FsrarId);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docs.AppendChild(doc);

            XmlNode bodyAct = xml.CreateElement(addresses.Ns.Prefix, "ActWriteOffShop_v2", addresses.Ns.Uri);
            doc.AppendChild(bodyAct);

            if (string.IsNullOrWhiteSpace(act.Identity))
                throw new Exception("В акте отсутствует клиентский идентификатор ('Identity').");
            addNodeToXmlDocument(xml, bodyAct, addresses.AwrShop2, "Identity", act.Identity);

            XmlNode header = xml.CreateElement(addresses.AwrShop2.Prefix, "Header", addresses.AwrShop2.Uri);
            bodyAct.AppendChild(header);

            if (string.IsNullOrWhiteSpace(act.Number)) throw new Exception("В акте отсутствует номер ('Number').");
            if (string.IsNullOrWhiteSpace(act.Reason)) throw new Exception("В акте отсутствует поле 'Основание'.");

            addNodeToXmlDocument(xml, header, addresses.AwrShop2, "ActNumber", act.Number);
            addNodeToXmlDocument(xml, header, addresses.AwrShop2, "ActDate", act.DateShort);
            addNodeToXmlDocument(xml, header, addresses.AwrShop2, "TypeWriteOff", act.Reason);
            addNodeToXmlDocument(xml, header, addresses.AwrShop2, "Note", act.Note);

            XmlNode content = xml.CreateElement(addresses.AwrShop2.Prefix, "Content", addresses.AwrShop2.Uri);
            bodyAct.AppendChild(content);

            foreach (MovePosition position in act.MovePositions)
            {
                position.Check();

                XmlNode positionNode = xml.CreateElement(addresses.AwrShop2.Prefix, "Position", addresses.AwrShop2.Uri);
                content.AppendChild(positionNode);

                addNodeToXmlDocument(xml, positionNode, addresses.AwrShop2, "Identity",
                    position.Identity.ToString("D"));

                #region Продукт...

                XmlNode product = xml.CreateElement(addresses.AwrShop2.Prefix, "Product", addresses.AwrShop2.Uri);
                positionNode.AppendChild(product);

                if (string.IsNullOrWhiteSpace(position.XmlProductionBody))
                    throw new Exception("В позиции акта присутствует алкогольная продукция с незаполненным xml-телом.");

                XmlDocument xmlProduct = new XmlDocument();
                xmlProduct.LoadXml(position.XmlProductionBody);
                importToXmlDocument(xml, product, xmlProduct);

                if (product["pref:UnitType"] == null)
                    addNodeToXmlDocument(xml, product, addresses.Pref2, "UnitType", "Packed");
                if (product["pref:Type"] == null) addNodeToXmlDocument(xml, product, addresses.Pref2, "Type", "АП");
                if (product["pref:ShortName"] == null)
                    addNodeToXmlDocument(xml, product, addresses.Pref2, "ShortName", string.Empty, false);

                #endregion Продукт...

                addNodeToXmlDocument(xml, positionNode, addresses.AwrShop2, "Quantity",
                    convertToString(position.Quantity));
            }

            Program.Logger.Info(this,
                string.Format("... построение исходящего xml-документа длиной {0} символов успешно завершено.",
                    xml.OuterXml.Length));

            return xml;
        }

        /// <summary>
        /// Обработать акт по квитанции.
        /// </summary>
        /// <param name="ticket">Квитанция.</param>
        protected virtual void conclusionActChargeOffShop(Ticket ticket)
        {
            try
            {
                Program.Logger.Info(this,
                    string.Format("Попытка обработки актов движения товара '{0}) по квитанции '{1}'...",
                        typeof(ActChargeOffShop).Name, ticket.Description));

                ActChargeOffShop act = actChargeOffShopList.FirstOrDefault(item => item.SendReplyId == ticket.ReplyId);

                if (act != null)
                {
                    conclusionMovementByTicket(ticket, act);

                    actChargeOffShopList.ForceChangeListEvent();
                }
                else
                {
                    Program.Logger.Info(this, "... документ для обработки не найден...");
                }

                Program.Logger.Info(this,
                    string.Format(
                        "... попытка обработки актов движения товара '{0}' по квитанции '{1}' успешно завершена",
                        typeof(ActChargeOffShop).Name, ticket.Description));
            }
            catch (Exception exception)
            {
                Program.Logger.Error(
                    "Во время проведения обработки актов движения товара по полученной квитанции произошла ошибка.",
                    exception);

                throw;
            }
        }

        #endregion ActChargeOffShop...

        #region TransferToShop...

        /// <summary>
        /// Добавить акт в список (по необходимости) и записать в хранилище.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void addAndSave(TransferToShop act)
        {
            if (transferToShopList.All(item => item.SurrogateId != act.SurrogateId))
            {
                transferToShopList.Add(act);
            }

            Storage.Save(act);
        }

        /// <summary>
        /// Удалить документ.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void delete(TransferToShop act)
        {
            if (transferToShopList.Any(item => item.SurrogateId == act.SurrogateId))
            {
                transferToShopList.Remove(act);
            }

            Storage.Delete(act);
        }

        /// <summary>
        /// Отправить акт.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void send(TransferToShop act)
        {
            if (act.StatusEnum != MovementStatus.New)
            {
                throw new Exception(
                    "Отправить можно только такой акт, который находится в статусе 'новый, готов к отправке'.");
            }

            XmlDocument xmlOutDocument = buildOutXmlDocument(act);

            #region Сохранение исходящего запроса.

            xmlOutDocument.Save(string.Format("{0}\\{1}.transfertoshop.{2}.{3}", storage.PathOut,
                FileStorage.NativePrefix, DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));

            #endregion Сохранение исходящего запроса.

            Program.Logger.Info(this,
                string.Format("Попытка отправить данные '{0}' по адресу '{1}'...", xmlOutDocument.OuterXml,
                    addresses.SendTransferToShop));

            string answer = HttpTransport.UploadFile(addresses.SendTransferToShop, xmlOutDocument, "xml_file",
                "text/xml; charset=utf-8", configuration.UtmTimeoutLong);

            Program.Logger.Info(this, string.Format("... данные успешно отправлены; получен ответ '{0}'.", answer));

            // Увы, таковы реалии ЕГАИС: идентификатор запроса возвращается в ноде "url".
            act.SetSendReplyId(getNodeValueFromXMLAnswer(answer, "url"));

            act.AddOutData(xmlOutDocument);
            act.ChangeStatus(MovementStatus.Sent);

            storage.Save(act);
        }

        /// <summary>
        /// Построить исходящий xml-документ.
        /// </summary>
        /// <returns>Xml-документ</returns>
        protected virtual XmlDocument buildOutXmlDocument(TransferToShop act)
        {
            Program.Logger.Info(this,
                string.Format("Попытка построить исходящий xml-документ для акта '{0}'...", act.Description));

            act.Check(false);

            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            #region Схемы...

            XmlNode docs = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docs, "Version", "1.0");
            addAttribute(xml, docs, "xmlns:" + addresses.Ns.Prefix, addresses.Ns.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.Xs.Prefix, addresses.Xs.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.C.Prefix, addresses.C.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.Pref2.Prefix, addresses.Pref2.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.Tts.Prefix, addresses.Tts.Uri);
            xml.AppendChild(docs);

            #endregion Схемы...

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docs.AppendChild(owner);

            addNodeToXmlDocument(xml, owner, addresses.Ns, "FSRAR_ID", configuration.FsrarId);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docs.AppendChild(doc);

            XmlNode bodyAct = xml.CreateElement(addresses.Ns.Prefix, "TransferToShop", addresses.Ns.Uri);
            doc.AppendChild(bodyAct);

            if (string.IsNullOrWhiteSpace(act.Identity))
                throw new Exception("В акте отсутствует клиентский идентификатор ('Identity').");
            addNodeToXmlDocument(xml, bodyAct, addresses.Tts, "Identity", act.Identity);

            XmlNode header = xml.CreateElement(addresses.Tts.Prefix, "Header", addresses.Tts.Uri);
            bodyAct.AppendChild(header);

            if (string.IsNullOrWhiteSpace(act.Number)) throw new Exception("В акте отсутствует номер ('Number').");

            addNodeToXmlDocument(xml, header, addresses.Tts, "TransferNumber", act.Number);
            addNodeToXmlDocument(xml, header, addresses.Tts, "TransferDate", act.DateShort);

            XmlNode content = xml.CreateElement(addresses.Tts.Prefix, "Content", addresses.Tts.Uri);
            bodyAct.AppendChild(content);

            if (act.MovePositions.Count == 0)
                throw new Exception("Список позиций акта движения товара пуст. Отправка не имеет смысла.");

            foreach (MovePosition position in act.MovePositions)
            {
                position.Check();

                XmlNode positionNode = xml.CreateElement(addresses.Tts.Prefix, "Position", addresses.Tts.Uri);
                content.AppendChild(positionNode);

                addNodeToXmlDocument(xml, positionNode, addresses.Tts, "Identity", position.Identity.ToString("D"));
                addNodeToXmlDocument(xml, positionNode, addresses.Tts, "ProductCode", position.AlcoCode);
                addNodeToXmlDocument(xml, positionNode, addresses.Tts, "Quantity", convertToString(position.Quantity));

                XmlNode informF2 = xml.CreateElement(addresses.Tts.Prefix, "InformF2", addresses.Tts.Uri);
                positionNode.AppendChild(informF2);
                addNodeToXmlDocument(xml, informF2, addresses.Pref2, "F2RegId", position.FormBRegId);
            }

            Program.Logger.Info(this,
                string.Format("... построение исходящего xml-документа длиной {0} символов успешно завершено.",
                    xml.OuterXml.Length));

            return xml;
        }

        /// <summary>
        /// Обработать акт по квитанции.
        /// </summary>
        /// <param name="ticket">Квитанция.</param>
        protected virtual void conclusionTransferToShop(Ticket ticket)
        {
            try
            {
                Program.Logger.Info(this,
                    string.Format("Попытка обработки актов движения товара '{0}) по квитанции '{1}'...",
                        typeof(TransferToShop).Name, ticket.Description));

                TransferToShop act = transferToShopList.FirstOrDefault(item => item.SendReplyId == ticket.ReplyId);

                if (act != null)
                {
                    conclusionMovementByTicket(ticket, act);

                    transferToShopList.ForceChangeListEvent();
                }
                else
                {
                    Program.Logger.Info(this, "... документ для обработки не найден...");
                }

                Program.Logger.Info(this,
                    string.Format(
                        "... попытка обработки актов движения товара '{0}' по квитанции '{1}' успешно завершена",
                        typeof(TransferToShop).Name, ticket.Description));
            }
            catch (Exception exception)
            {
                Program.Logger.Error(
                    "Во время проведения обработки актов движения товара по полученной квитанции произошла ошибка.",
                    exception);

                throw;
            }
        }

        #endregion TransferToShop...

        #region TransferFromShop...

        /// <summary>
        /// Добавить акт в список (по необходимости) и записать в хранилище.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void addAndSave(TransferFromShop act)
        {
            if (transferFromShopList.All(item => item.SurrogateId != act.SurrogateId))
            {
                transferFromShopList.Add(act);
            }

            Storage.Save(act);
        }

        /// <summary>
        /// Удалить документ.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void delete(TransferFromShop act)
        {
            if (transferFromShopList.Any(item => item.SurrogateId == act.SurrogateId))
            {
                transferFromShopList.Remove(act);
            }

            Storage.Delete(act);
        }

        /// <summary>
        /// Отправить акт.
        /// </summary>
        /// <param name="act">Акт.</param>
        protected virtual void send(TransferFromShop act)
        {
            if (act.StatusEnum != MovementStatus.New)
            {
                throw new Exception(
                    "Отправить можно только такой акт, который находится в статусе 'новый, готов к отправке'.");
            }

            XmlDocument xmlOutDocument = buildOutXmlDocument(act);

            #region Сохранение исходящего запроса.

            xmlOutDocument.Save(string.Format("{0}\\{1}.transferFromShop.{2}.{3}", storage.PathOut,
                FileStorage.NativePrefix, DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));

            #endregion Сохранение исходящего запроса.

            Program.Logger.Info(this,
                string.Format("Попытка отправить данные '{0}' по адресу '{1}'...", xmlOutDocument.OuterXml,
                    addresses.SendTransferFromShop));

            string answer = HttpTransport.UploadFile(addresses.SendTransferFromShop, xmlOutDocument, "xml_file",
                "text/xml; charset=utf-8", configuration.UtmTimeoutLong);

            Program.Logger.Info(this, string.Format("... данные успешно отправлены; получен ответ '{0}'.", answer));

            // Увы, таковы реалии ЕГАИС: идентификатор запроса возвращается в ноде "url".
            act.SetSendReplyId(getNodeValueFromXMLAnswer(answer, "url"));

            act.AddOutData(xmlOutDocument);
            act.ChangeStatus(MovementStatus.Sent);

            storage.Save(act);
        }

        /// <summary>
        /// Построить исходящий xml-документ.
        /// </summary>
        /// <returns>Xml-документ</returns>
        protected virtual XmlDocument buildOutXmlDocument(TransferFromShop act)
        {
            Program.Logger.Info(this,
                string.Format("Попытка построить исходящий xml-документ для акта '{0}'...", act.Description));

            act.Check(false);

            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            #region Схемы...

            XmlNode docs = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docs, "Version", "1.0");
            addAttribute(xml, docs, "xmlns:" + addresses.Ns.Prefix, addresses.Ns.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.Xs.Prefix, addresses.Xs.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.C.Prefix, addresses.C.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.Pref2.Prefix, addresses.Pref2.Uri);
            addAttribute(xml, docs, "xmlns:" + addresses.Tfs.Prefix, addresses.Tfs.Uri);
            xml.AppendChild(docs);

            #endregion Схемы...

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docs.AppendChild(owner);

            addNodeToXmlDocument(xml, owner, addresses.Ns, "FSRAR_ID", configuration.FsrarId);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docs.AppendChild(doc);

            XmlNode bodyAct = xml.CreateElement(addresses.Ns.Prefix, "TransferFromShop", addresses.Ns.Uri);
            doc.AppendChild(bodyAct);

            if (string.IsNullOrWhiteSpace(act.Identity))
                throw new Exception("В акте отсутствует клиентский идентификатор ('Identity').");
            addNodeToXmlDocument(xml, bodyAct, addresses.Tfs, "Identity", act.Identity);

            XmlNode header = xml.CreateElement(addresses.Tfs.Prefix, "Header", addresses.Tfs.Uri);
            bodyAct.AppendChild(header);

            if (string.IsNullOrWhiteSpace(act.Number)) throw new Exception("В акте отсутствует номер ('Number').");

            addNodeToXmlDocument(xml, header, addresses.Tfs, "TransferNumber", act.Number);
            addNodeToXmlDocument(xml, header, addresses.Tfs, "TransferDate", act.DateShort);

            XmlNode content = xml.CreateElement(addresses.Tfs.Prefix, "Content", addresses.Tfs.Uri);
            bodyAct.AppendChild(content);

            if (act.MovePositions.Count == 0)
                throw new Exception("Список позиций акта движения товара пуст. Отправка не имеет смысла.");

            foreach (MovePosition position in act.MovePositions)
            {
                position.Check();

                XmlNode positionNode = xml.CreateElement(addresses.Tfs.Prefix, "Position", addresses.Tfs.Uri);
                content.AppendChild(positionNode);

                addNodeToXmlDocument(xml, positionNode, addresses.Tfs, "Identity", position.Identity.ToString("D"));
                addNodeToXmlDocument(xml, positionNode, addresses.Tfs, "ProductCode", position.AlcoCode);
                addNodeToXmlDocument(xml, positionNode, addresses.Tfs, "Quantity", convertToString(position.Quantity));

                XmlNode informF2 = xml.CreateElement(addresses.Tfs.Prefix, "InformF2", addresses.Tfs.Uri);
                positionNode.AppendChild(informF2);
                addNodeToXmlDocument(xml, informF2, addresses.Pref2, "F2RegId", position.FormBRegId);
            }

            Program.Logger.Info(this,
                string.Format("... построение исходящего xml-документа длиной {0} символов успешно завершено.",
                    xml.OuterXml.Length));

            return xml;
        }

        /// <summary>
        /// Обработать акт по квитанции.
        /// </summary>
        /// <param name="ticket">Квитанция.</param>
        protected virtual void conclusionTransferFromShop(Ticket ticket)
        {
            try
            {
                Program.Logger.Info(this,
                    string.Format("Попытка обработки актов движения товара '{0}) по квитанции '{1}'...",
                        typeof(TransferFromShop).Name, ticket.Description));

                TransferFromShop act = transferFromShopList.FirstOrDefault(item => item.SendReplyId == ticket.ReplyId);

                if (act != null)
                {
                    conclusionMovementByTicket(ticket, act);

                    transferFromShopList.ForceChangeListEvent();
                }
                else
                {
                    Program.Logger.Info(this, "... документ для обработки не найден...");
                }

                Program.Logger.Info(this,
                    string.Format(
                        "... попытка обработки актов движения товара '{0}' по квитанции '{1}' успешно завершена",
                        typeof(TransferFromShop).Name, ticket.Description));
            }
            catch (Exception exception)
            {
                Program.Logger.Error(
                    "Во время проведения обработки актов движения товара по полученной квитанции произошла ошибка.",
                    exception);

                throw;
            }
        }

        #endregion TransferFromShop...

        #endregion Защищённые методы класса.
    }
}