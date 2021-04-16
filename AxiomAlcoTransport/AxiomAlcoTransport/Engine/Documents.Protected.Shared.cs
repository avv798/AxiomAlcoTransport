using System;
using System.Xml;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using Axiom.AlcoTransport.Document;
using AxiomAuxiliary.Transports;

namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Списки документов приложения.
    /// </summary>
    public partial class Documents
    {
        #region Защищённые методы класса.

        #region Documents...
        /// <summary>
        /// Получить все входящие документы.
        /// </summary>
        protected virtual void getDocuments()
        {
            Program.Logger.Info(this, string.Format("Попытка отправить запрос на сервер по адресу '{0}'...", addresses.GetRequestIn));

            lock (locker)
            {
                #region Licence...
                if (!Program.ValidateLicence())
                {
                    throw new Exception("Обнаружено нарушение лицензионного соглашения. Обратитесь в службу технической поддержки.");
                }
                #endregion Licence...

                string answer = HttpTransport.GetRequest(addresses.GetRequestIn, configuration.UtmTimeoutLong);

                Program.Logger.Info(this, string.Format("... получен ответ от сервера; длина ответа {0} символов.", answer.Length));

                Program.Logger.Info(this, "Попытка синтаксического анализа ответа сервера...");

                XmlDocument xml = new XmlDocument();
                xml.LoadXml(answer);

                if (xml.DocumentElement == null) throw new Exception("Основная XML-нода в документе пустая.");

                List<string> receivedDocuments = new List<string>();

                foreach (XmlNode node in xml.DocumentElement.ChildNodes)
                {
                    try
                    {
                        if (node.Name.ToLower() != "url") continue;

                        string url = node.InnerText;
                        string replyId = ADocument.GetAttributeValue("replyId", node);

                        if (string.IsNullOrWhiteSpace(url)) continue;

                        #region Получение документа...

                        Program.Logger.Info(this, string.Format("Попытка получить документ по ссылке '{0}'...", url));

                        string answerDocument = HttpTransport.GetRequest(url, configuration.UtmTimeoutLong);

                        Program.Logger.Info(this, string.Format("... получен ответ от сервера; длина ответа {0} символов.", answerDocument.Length));

                        #endregion Получение документа...

                        Program.Logger.Info(this, "Попытка синтаксического анализа (для документа) ответа сервера...");

                        XmlDocument xmlDocument = new XmlDocument();
                        xmlDocument.LoadXml(answerDocument);

                        #region Перебор всех типов...

                        bool know = true;
                        string urlLower = url.ToLower(); 
                        string addressIn = addresses.GetRequestIn.ToLower();

                        // TODO: переделать на более элегантный вариант.
                        if (urlLower.StartsWith(string.Format("{0}/replypartner/", addressIn))) { getPartners(url, replyId, xmlDocument); receivedDocuments.Add(Partner.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/replyclient_v2/", addressIn))) { getPartners(url, replyId, xmlDocument, 2); receivedDocuments.Add(Partner.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/replyap/", addressIn))) { getAP(url, replyId, xmlDocument, ProductionType.Alcohol); receivedDocuments.Add(AlcoTransport.Production.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/replyap_v2/", addressIn))) { getAP(url, replyId, xmlDocument, ProductionType.Alcohol, 2); receivedDocuments.Add(AlcoTransport.Production.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/replyspirit_v2/", addressIn))) { getAP(url, replyId, xmlDocument, ProductionType.Spirit, 2); receivedDocuments.Add(AlcoTransport.Production.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/replyssp_v2/", addressIn))) { getAP(url, replyId, xmlDocument, ProductionType.SpiritContainer, 2); receivedDocuments.Add(AlcoTransport.Production.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/waybill/", addressIn))) { getInWaybill(url, xmlDocument); receivedDocuments.Add(InWaybill.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/waybill_v2/", addressIn))) { getInWaybill(url, xmlDocument, 2); receivedDocuments.Add(InWaybill.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/waybill_v3/", addressIn))) { getInWaybill(url, xmlDocument, 3); receivedDocuments.Add(InWaybill.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/waybill_v4/", addressIn))) { getInWaybill(url, xmlDocument, 4); receivedDocuments.Add(InWaybill.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/formbreginfo/", addressIn))) { getFormBRegId(url, replyId, xmlDocument); receivedDocuments.Add(FormBRegInfo.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/form2reginfo/", addressIn))) { getFormBRegId(url, replyId, xmlDocument, 2); receivedDocuments.Add(FormBRegInfo.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/inventoryreginfo/", addressIn))) { getInventoryBRegInfo(url, replyId, xmlDocument); receivedDocuments.Add(InventoryBRegInfo.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/replyrests/", addressIn))) { getRests(url, replyId, xmlDocument); receivedDocuments.Add(Rests.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/replyrests_v2/", addressIn))) { getRests(url, replyId, xmlDocument, 2); receivedDocuments.Add(Rests.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/replyrestbcode/", addressIn))) { getBcodeRests(url, replyId, xmlDocument); receivedDocuments.Add(BCodeRests.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/replyrestsshop_v2/", addressIn))) { getShopRests(url, replyId, xmlDocument); receivedDocuments.Add(ShopRests.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/replyhistoryformb/", addressIn))) { getHistoryFormB(url, replyId, xmlDocument); receivedDocuments.Add(HistoryFormB.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/replyhistform2/", addressIn))) { getHistoryFormB(url, replyId, xmlDocument, 2); receivedDocuments.Add(HistoryFormB.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/replybarcode/", addressIn))) { getMarkBarcode(url, replyId, xmlDocument); receivedDocuments.Add(MarkBarcode.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/waybillact/", addressIn))) { getWaybillAct(url, xmlDocument); receivedDocuments.Add(WaybillAct.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/waybillact_v2/", addressIn))) { getWaybillAct(url, xmlDocument, 2); receivedDocuments.Add(WaybillAct.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/waybillact_v3/", addressIn))) { getWaybillAct(url, xmlDocument, 3); receivedDocuments.Add(WaybillAct.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/requestrepealwb/", addressIn))) { getWaybillRepeal(url, xmlDocument); receivedDocuments.Add(WaybillRepeal.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/confirmrepealwb/", addressIn))) { getWaybillRepealConfirm(url, xmlDocument); receivedDocuments.Add(WaybillRepealConfirm.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/replynattn/", addressIn))) { getNotAnswer(url, replyId, xmlDocument); receivedDocuments.Add(NotAnswer.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/replyforma/", addressIn))) { getFormA(url, replyId, xmlDocument); receivedDocuments.Add(FormA.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/replyform1/", addressIn))) { getFormA(url, replyId, xmlDocument, 2); receivedDocuments.Add(FormA.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/replyformb/", addressIn))) { getFormB(url, replyId, xmlDocument); receivedDocuments.Add(FormB.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/replyform2/", addressIn))) { getFormB(url, replyId, xmlDocument, 2); receivedDocuments.Add(FormB.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/waybillticket/", addressIn))) { getWaybillTicket(url, xmlDocument); receivedDocuments.Add(WaybillTicket.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/cryptoticket/", addressIn))) { getTickets(url, replyId, xmlDocument); receivedDocuments.Add(Ticket.NativeName); }
                        else if (urlLower.StartsWith(string.Format("{0}/ticket/", addressIn))) { getTickets(url, replyId, xmlDocument); receivedDocuments.Add(Ticket.NativeName); }
                        else
                        {
                            know = false;

                            Program.Logger.Warn(string.Format("Неизвестная ссылка '{0}' на входящий документ в буфере.", url));

                            onlineEvents.Add(new OnlineEvent(OnlineEventStatus.Warning, this, string.Format("Неизвестная ссылка '{0}' на входящий документ в буфере.", url)));
                            onlineEvents.Add(new OnlineEvent(OnlineEventStatus.Warning, this, string.Format("Входящий документ по адресу '{0}' остаётся во входящем буфере УТМ.", url)));
                        }
                        #endregion Перебор всех типов...

                        if (know)
                        {
                            deleteRequest(url);

                            onlineEvents.Add(new OnlineEvent(this, string.Format("Входящий документ по адресу '{0}' успешно получен и сохранён в локальной базе данных.", url)));
                        }

                        Program.Logger.Info(this, "... синтаксический анализ (для документа) ответа сервера успешно завершён.");
                    }
                    catch (Exception exception)
                    {
                        Program.Logger.Error(string.Format("Во время получения входящего документа по xml-объекту '{0}' произошла ошибка.", node.OuterXml), exception);
                        
                        onlineEvents.Add(new OnlineEvent(this, "Во время синтаксического анализа входящих документов произошла ошибка.", exception));
                        onlineEvents.Add(new OnlineEvent(OnlineEventStatus.Error, this, string.Format("Ошибка синтаксического анализа входящего документа, полученного по адресу '{0}'.", node.InnerText)));
                        onlineEvents.Add(new OnlineEvent(OnlineEventStatus.Warning, this, string.Format("Входящий документ по адресу '{0}' остаётся во входящем буфере УТМ.", node.InnerText)));
                    }
                }

                if (receivedDocuments.Count > 0)
                {
                    if (DocumentReceivedEvent != null)
                    {
                        DocumentReceivedEvent(this, new DocumentReceivedEventArgs(receivedDocuments));
                    }
                }
            }

            Program.Logger.Info(this, "... синтаксический анализ ответа успешно завершён.");
        }
        /// <summary>
        /// Удаление запроса из базы данных УТМ.
        /// </summary>
        /// <param name="url">Адрес.</param>
        protected virtual void deleteRequest(string url)
        {
            Program.Logger.Info(this, string.Format("Попытка удаления запроса из базы данных УТМ по ссылке '{0}'...", url));

            HttpTransport.DeleteRequest(url, configuration.UtmTimeoutShort);

            Program.Logger.Info(this, "... удаление запроса из базы данных УТМ успешно завершено.");
        }
        /// <summary>
        /// Загрузить базу данных в память приложения.
        /// </summary>
        /// <param name="verbose">Включить протоколирование загрузки.</param>
        protected virtual void loadDatabase(bool verbose = false)
        {
            #region Загрузка файлов...
            foreach (string fullname in storage.GetFileList())
            {
                try
                {
                    string filename = System.IO.Path.GetFileName(fullname);

                    if (string.IsNullOrWhiteSpace(filename)) continue;

                    #region Перебор всех типов...

                    // TODO: переделать на более элегантный вариант.
                    if (filename.StartsWith(string.Format("{0}.", typeof(Partner).FullName))) partners.Add(storage.Load<Partner>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(Production).FullName))) production.Add(storage.Load<Production>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(Rests).FullName))) restsList.Add(storage.Load<Rests>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(BCodeRests).FullName))) bCodeRestsList.Add(storage.Load<BCodeRests>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(ShopRests).FullName))) shopRestsList.Add(storage.Load<ShopRests>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(InWaybill).FullName))) inWaybills.Add(storage.Load<InWaybill>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(OutWaybill).FullName))) outWaybills.Add(storage.Load<OutWaybill>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(FormA).FullName))) formAList.Add(storage.Load<FormA>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(FormB).FullName))) formBList.Add(storage.Load<FormB>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(HistoryFormB).FullName))) historyFormBList.Add(storage.Load<HistoryFormB>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(MarkBarcode).FullName))) markBarcodes.Add(storage.Load<MarkBarcode>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(WaybillAct).FullName))) waybillActs.Add(storage.Load<WaybillAct>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(WaybillTicket).FullName))) waybillTickets.Add(storage.Load<WaybillTicket>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(WaybillRepeal).FullName))) waybillRepeals.Add(storage.Load<WaybillRepeal>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(NotAnswer).FullName))) notAnswerList.Add(storage.Load<NotAnswer>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(ActChargeOn).FullName))) actChargeOnList.Add(storage.Load<ActChargeOn>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(ActChargeOff).FullName))) actChargeOffList.Add(storage.Load<ActChargeOff>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(ActChargeOnShop).FullName))) actChargeOnShopList.Add(storage.Load<ActChargeOnShop>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(ActChargeOffShop).FullName))) actChargeOffShopList.Add(storage.Load<ActChargeOffShop>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(TransferToShop).FullName))) transferToShopList.Add(storage.Load<TransferToShop>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(TransferFromShop).FullName))) transferFromShopList.Add(storage.Load<TransferFromShop>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(ActFixBarcode).FullName))) actFixBarcodeList.Add(storage.Load<ActFixBarcode>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(ActUnFixBarcode).FullName))) actUnFixBarcodeList.Add(storage.Load<ActUnFixBarcode>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(Ticket).FullName))) tickets.Add(storage.Load<Ticket>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(FormBRegInfo).FullName))) formBRegInfos.Add(storage.Load<FormBRegInfo>(fullname));
                    else if (filename.StartsWith(string.Format("{0}.", typeof(InventoryBRegInfo).FullName))) inventoryBRegInfoList.Add(storage.Load<InventoryBRegInfo>(fullname));

                    #endregion Перебор всех типов...

                    if (verbose) Program.Logger.Info(this, string.Format("Документ из файла '{0}' успешно загружен в память приложения из хранилища.", filename));
                }
                catch (Exception exception)
                {
                    string msg = string.Format("Во время загрузки файла '{0}' произошла ошибка: '{1}'.", fullname, exception.Message);
                    Program.Logger.Error(msg, exception);
                    onlineEvents.Add(new OnlineEvent(OnlineEventStatus.Error, this, msg));
                }
            }
            #endregion Загрузка файлов...

            #region Ассоциация неполных накладных с документом регистрации движения...
            if (inWaybills.Count(waybill => waybill.StatusEnum == InWaybillStatus.Partial) > 0)
            {
                foreach (FormBRegInfo info in formBRegInfos)
                {
                    if (info.IsFreeDocument) associate(info);
                }
            }
            #endregion Ассоциация неполных накладных с документом регистрации движения...

            #region Ассоциация актов постановки на баланс с уведомлениями от ЕГАИС...
            if (actChargeOnList.Count(item => ((item.StatusEnum == MovementStatus.Confirmed) || (item.StatusEnum == MovementStatus.Sent))) > 0)
            {
                foreach (InventoryBRegInfo info in inventoryBRegInfoList)
                {
                    if (info.IsFreeDocument) associate(info);
                }
            }
            #endregion Ассоциация актов постановки на баланс с уведомлениями от ЕГАИС...
        }
        /// <summary>
        /// Очистить исходящий буфер.
        /// </summary>
        protected virtual void clearOutBuffer()
        {
            Program.Logger.Info(this, string.Format("Попытка отправить запрос на сервер по адресу '{0}'...", addresses.GetRequestOut));

            string answer = HttpTransport.GetRequest(addresses.GetRequestOut, configuration.UtmTimeoutLong);

            Program.Logger.Info(this, string.Format("... получен ответ от сервера; длина ответа {0} символов.", answer.Length));

            Program.Logger.Info(this, "Попытка синтаксического анализа ответа сервера...");

            XmlDocument xml = new XmlDocument();

            xml.LoadXml(answer);

            if (xml.DocumentElement == null) throw new Exception("Основная XML-нода в документе пустая.");

            foreach (XmlNode node in xml.DocumentElement.ChildNodes)
            {
                if (node.Name.ToLower() != "url") continue;

                string url = node.InnerText;

                if (string.IsNullOrWhiteSpace(url)) continue;

                deleteRequest(url);
            }

            Program.Logger.Info(this, "... синтаксический анализ ответа успешно завершён.");
        }
        /// <summary>
        /// Очистить исходящий буфер во время старта приложения.
        /// </summary>
        protected virtual void clearOutBufferOnStartup()
        {
            Program.Logger.Info(this, string.Format("Попытка отправить запрос на сервер по адресу '{0}'...", addresses.GetRequestOut));

            string answer = HttpTransport.GetRequest(addresses.GetRequestOut, configuration.UtmTimeoutLong);

            Program.Logger.Info(this, string.Format("... получен ответ от сервера; длина ответа {0} символов.", answer.Length));

            Program.Logger.Info(this, "Попытка синтаксического анализа ответа сервера...");

            XmlDocument xml = new XmlDocument();

            xml.LoadXml(answer);

            if (xml.DocumentElement == null) throw new Exception("Основная XML-нода в документе пустая.");

            // Оставим в буфере несколько последних документов. По умолчанию - пять штук.
            // Следует помнить, что в XML-буфере количество нод равно количеству исходящих документов, плюс одна служебная (версионная) нода.
            const int maxNodes = 5;

            if ((xml.DocumentElement.ChildNodes.Count - 1)> maxNodes)
            {
                for (int i = 0; i < (xml.DocumentElement.ChildNodes.Count - maxNodes - 1); ++i)
                {
                    XmlNode node = xml.DocumentElement.ChildNodes[i];

                    if (node.Name.ToLower() != "url") continue;

                    string url = node.InnerText;

                    if (string.IsNullOrWhiteSpace(url)) continue;

                    deleteRequest(url);
                }
            }
            else
            {
                Program.Logger.Info(this, string.Format("В исходящем буфере менее {0} документов. Очистка не требуется.", maxNodes));
            }

            Program.Logger.Info(this, "... синтаксический анализ ответа успешно завершён.");

        }
        /// <summary>
        /// Добавить атрибут к указанной XML-ноде.
        /// </summary>
        /// <param name="xmlDocument">XML-документ.</param>
        /// <param name="node">XML-нода.</param>
        /// <param name="name">Наименование атрибута.</param>
        /// <param name="value">Значение атрибута.</param>
        protected virtual void addAttribute(XmlDocument xmlDocument, XmlNode node, string name, string value)
        {
            if (xmlDocument == null) throw new Exception("Object 'xmlDocument' is null.");

            XmlAttribute attribute = xmlDocument.CreateAttribute(name);
            attribute.Value = value;

            if (node.Attributes == null) throw new Exception("Object 'Node.Attributes' is null.");

            node.Attributes.Append(attribute);
        }
        /// <summary>
        /// Получить значение ноды из xml-ответа сервера.
        /// </summary>
        /// <param name="xmlAnswer">Xml-ответ сервера.</param>
        /// <param name="nodeName">Наименование ноды.</param>
        /// <returns>Значение секции 'url'.</returns>
        protected virtual string getNodeValueFromXMLAnswer(string xmlAnswer, string nodeName)
        {
            try
            {
                Program.Logger.Info(this, string.Format("Попытка получить секцию '{0}' из ответа сервера '{1}'...", nodeName, xmlAnswer));

                XmlDocument xmlDocument = new XmlDocument();

                xmlDocument.LoadXml(xmlAnswer);

                if (xmlDocument.DocumentElement == null) throw new Exception("XML-object 'xmlAnswer.DocumentElement' is null.");

                bool exist = false;
                string url = string.Empty;

                foreach (XmlNode node in xmlDocument.DocumentElement.ChildNodes)
                {
                    if (node.Name.ToLower() == nodeName.ToLower())
                    {
                        url = node.InnerText;
                        exist = true;
                        break;
                    }
                }

                if (!exist)
                {
                    throw new Exception(string.Format("Нода '{0}' в xml-ответе сервера не найдена.", nodeName));
                }

                Program.Logger.Info(this, string.Format("... значение секции 'url' успешно получено: '{0}'.", url));

                return url;
            }
            catch(Exception exception)
            {
                Program.Logger.Error("Во время синтаксического анализа ответа сервера произошла ошибка.", exception);

                throw;
            }
        }
        /// <summary>
        /// Отправить запрос на повторное получение накладной по её идентификатору.
        /// </summary>
        /// <param name="wBRegId">Идентификатор накладной.</param>
        protected virtual void queryResendWaybill(string wBRegId)
        {
            XmlDocument client = getQueryResendWaybillData(wBRegId);

            #region Сохранение исходящего запроса.
            client.Save(string.Format("{0}\\{1}.queryresenddoc.{2}.{3}", storage.PathOut, FileStorage.NativePrefix, DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));
            #endregion Сохранение исходящего запроса.

            Program.Logger.Info(this, string.Format("Попытка отправить данные '{0}' по адресу '{1}'...", client.OuterXml, addresses.QueryResendDoc));

            HttpTransport.UploadFile(addresses.QueryResendDoc, client, "xml_file", "text/xml; charset=utf-8", configuration.UtmTimeoutLong);

            Program.Logger.Info(this, "... данные успешно отправлены.");
        }
        /// <summary>
        /// Составить xml-запрос на повторное получение накладной по её идентификатору.
        /// </summary>
        /// <param name="wBRegId">Идентификатор накладной.</param>
        /// <returns>Xml-документ.</returns>
        protected virtual XmlDocument getQueryResendWaybillData(string wBRegId)
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

            XmlNode queryClients = xml.CreateElement(addresses.Ns.Prefix, "QueryResendDoc", addresses.Ns.Uri);
            doc.AppendChild(queryClients);

            XmlNode parameters = xml.CreateElement(addresses.Qp.Prefix, "Parameters", addresses.Qp.Uri);
            queryClients.AppendChild(parameters);

            XmlNode parameter = xml.CreateElement(addresses.Qp.Prefix, "Parameter", addresses.Qp.Uri);
            parameters.AppendChild(parameter);

            XmlNode name = xml.CreateElement(addresses.Qp.Prefix, "Name", addresses.Qp.Uri);
            name.InnerText = "WBREGID";
            parameter.AppendChild(name);

            XmlNode value = xml.CreateElement(addresses.Qp.Prefix, "Value", addresses.Qp.Uri);
            value.InnerText = wBRegId;
            parameter.AppendChild(value);

            return xml;
        }
        /// <summary>
        /// Добавить ноду в заголовок накладной.
        /// </summary>
        /// <param name="xml">XML-документ.</param>
        /// <param name="node">Нода.</param>
        /// <param name="prefix">Префикс.</param>
        /// <param name="name">Имя добавляемой ноды.</param>
        /// <param name="value">Значение.</param>
        /// <param name="checkValue">Проверять значение перед добавлением.</param>
        protected virtual void addNodeToXmlDocument(XmlDocument xml, XmlNode node, XmlPrefix prefix, string name, string value, bool checkValue = true)
        {
            if (checkValue)
            {
                if (string.IsNullOrWhiteSpace(value)) return;
            }

            XmlNode xmlNode = xml.CreateElement(prefix.Prefix, name, prefix.Uri);
            xmlNode.InnerText = value;
            node.AppendChild(xmlNode);
        }
        /// <summary>
        /// Вставить ноду перед указанной.
        /// </summary>
        /// <param name="xml">XML-документ.</param>
        /// <param name="node">Нода.</param>
        /// <param name="prefix">Префикс.</param>
        /// <param name="name">Имя добавляемой ноды.</param>
        /// <param name="value">Значение.</param>
        /// <param name="beforeNode">Нода, перед которой надо вставить.</param>
        protected virtual void addNodeToXmlDocument(XmlDocument xml, XmlNode node, XmlPrefix prefix, string name, string value, XmlNode beforeNode)
        {
            // if (string.IsNullOrWhiteSpace(value)) return;
            if (value == null) return;

            XmlNode xmlNode = xml.CreateElement(prefix.Prefix, name, prefix.Uri);
            xmlNode.InnerText = value;
            node.InsertBefore(xmlNode, beforeNode);
        }
        /// <summary>
        /// Импортировать документ в ноду.
        /// </summary>
        /// <param name="xml">XML-документ.</param>
        /// <param name="node">Нода.</param>
        /// <param name="source">Источник.</param>
        protected virtual void importToXmlDocument(XmlDocument xml, XmlNode node, XmlDocument source)
        {
            if (source == null) throw new Exception("Импортируемый xml-документ пустой.");
            if (source.DocumentElement == null) throw new Exception("Импортируемый xml-документ не имеет состава.");

            foreach (XmlNode child in source.DocumentElement.ChildNodes)
            {
                #region Fucking EGAIS...
                if ((source.DocumentElement.Name.ToLower() == "rc:client")
                    && (child.Name.ToLower() == "oref:state")) continue;
                #endregion Fucking EGAIS...

                XmlNode imported = xml.ImportNode(child, true);
                node.AppendChild(imported);
            }
        }
        /// <summary>
        /// Преобразовать число в строку.
        /// </summary>
        /// <param name="number">Число.</param>
        /// <returns>Результат.</returns>
        protected virtual string convertToString(decimal number)
        {
            try
            {
                return Convert.ToString(number, new NumberFormatInfo { NumberDecimalSeparator = ".", NumberGroupSeparator = string.Empty });
            }
            catch (Exception exception)
            {
                Program.Logger.Error(this, string.Format("При преобразовании числа '{0}' в число произошла ошибка '{1}'.", number, exception));

                throw;
            }
        }
        /// <summary>
        /// Найти входящую накладную по идентификатору справки 'Б' одной из её позиций.
        /// Если накладная не найдена, то будет возвращено значение 'null'.
        /// </summary>
        /// <param name="formBRegId">Идентификатор справки 'Б'.</param>
        /// <returns>Накладная.</returns>
        protected virtual InWaybill findInWaybill(string formBRegId)
        {
            return inWaybills.FirstOrDefault(inWaybill => inWaybill.GetPositions().Any(position => (position.InformBRegId == formBRegId)));
        }
        /// <summary>
        /// Осуществить поиск в файлах базы данных.
        /// </summary>
        /// <param name="findtext">Строка для поиска.</param>
        /// <returns>Документы, содержащие указанную строку.</returns>
        protected virtual ThreadedBindingList<ADocument> fullTextSearch(string findtext)
        {
            ThreadedBindingList<ADocument> list = new ThreadedBindingList<ADocument>();

            foreach (string fullname in storage.FullTextSearch(findtext))
            {
                if (string.IsNullOrWhiteSpace(fullname)) continue;

                string filename = System.IO.Path.GetFileName(fullname);

                if (string.IsNullOrWhiteSpace(filename)) continue;

                #region Перебор всех типов...

                // TODO: переделать на более элегантный вариант.
                if (filename.StartsWith(string.Format("{0}.", typeof(Rests).FullName))) list.Add(storage.Load<Rests>(fullname));
                else if (filename.StartsWith(string.Format("{0}.", typeof(ShopRests).FullName))) list.Add(storage.Load<ShopRests>(fullname));
                else if (filename.StartsWith(string.Format("{0}.", typeof(InWaybill).FullName))) list.Add(storage.Load<InWaybill>(fullname));
                else if (filename.StartsWith(string.Format("{0}.", typeof(OutWaybill).FullName))) list.Add(storage.Load<OutWaybill>(fullname));
                else if (filename.StartsWith(string.Format("{0}.", typeof(FormA).FullName))) list.Add(storage.Load<FormA>(fullname));
                else if (filename.StartsWith(string.Format("{0}.", typeof(FormB).FullName))) list.Add(storage.Load<FormB>(fullname));
                else if (filename.StartsWith(string.Format("{0}.", typeof(HistoryFormB).FullName))) list.Add(storage.Load<HistoryFormB>(fullname));
                else if (filename.StartsWith(string.Format("{0}.", typeof(MarkBarcode).FullName))) list.Add(storage.Load<MarkBarcode>(fullname));
                else if (filename.StartsWith(string.Format("{0}.", typeof(WaybillAct).FullName))) list.Add(storage.Load<WaybillAct>(fullname));
                else if (filename.StartsWith(string.Format("{0}.", typeof(WaybillTicket).FullName))) list.Add(storage.Load<WaybillTicket>(fullname));
                else if (filename.StartsWith(string.Format("{0}.", typeof(NotAnswer).FullName))) list.Add(storage.Load<NotAnswer>(fullname));
                else if (filename.StartsWith(string.Format("{0}.", typeof(ActChargeOn).FullName))) list.Add(storage.Load<ActChargeOn>(fullname));
                else if (filename.StartsWith(string.Format("{0}.", typeof(ActChargeOff).FullName))) list.Add(storage.Load<ActChargeOff>(fullname));
                else if (filename.StartsWith(string.Format("{0}.", typeof(ActChargeOnShop).FullName))) list.Add(storage.Load<ActChargeOnShop>(fullname));
                else if (filename.StartsWith(string.Format("{0}.", typeof(ActChargeOffShop).FullName))) list.Add(storage.Load<ActChargeOffShop>(fullname));
                else if (filename.StartsWith(string.Format("{0}.", typeof(TransferToShop).FullName))) list.Add(storage.Load<TransferToShop>(fullname));
                else if (filename.StartsWith(string.Format("{0}.", typeof(TransferFromShop).FullName))) list.Add(storage.Load<TransferFromShop>(fullname));
                else if (filename.StartsWith(string.Format("{0}.", typeof(Ticket).FullName))) list.Add(storage.Load<Ticket>(fullname));

                #endregion Перебор всех типов...
            }

            return list;
        }
        /// <summary>
        /// Экспорт документа в систему "AxiTrade".
        /// </summary>
        /// <param name="waybill">Документ.</param>
        /// <param name="filename">Имя файла для экспорта.</param>
        protected virtual void exportToAxiTrade(InWaybill waybill, string filename)
        {
            Program.Logger.Info(this, string.Format("Попытка построить xml-документ формата 'AxiTrade' для накладной '{0}'...", waybill.Description));

            List<Position> positions = waybill.GetPositions();
            if (positions.Count == 0) throw new Exception("Список позиций товарно-транспортной накладной пуст. Экспорт не имеет смысла.");

            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            XmlNode doc = xml.CreateElement("InVinoVeritas.ExportDocument");
            xml.AppendChild(doc);

            addAttribute(xml, doc, "ExportDateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            addAttribute(xml, doc, "ExportFSRARID", Configuration.FsrarId);
            addAttribute(xml, doc, "ExportINN", Configuration.Inn);

            XmlNode income = xml.CreateElement("IncomeWaybill");
            doc.AppendChild(income);

            addAttribute(xml, income, "AuxCompositeId", waybill.AuxCompositeId);
            addAttribute(xml, income, "ConsigneeName", waybill.ConsigneeName);
            addAttribute(xml, income, "CreateDateTime", waybill.CreateDateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            addAttribute(xml, income, "Date", waybill.Date);
            addAttribute(xml, income, "DocumentType", waybill.DocumentType);
            addAttribute(xml, income, "Identity", waybill.Identity);
            addAttribute(xml, income, "LastChange", waybill.LastChange);
            addAttribute(xml, income, "Number", waybill.Number);
            addAttribute(xml, income, "ShipperName", waybill.ShipperName);
            addAttribute(xml, income, "ShippingDate", waybill.ShippingDate);
            addAttribute(xml, income, "StatusNote", waybill.StatusNote);

            XmlNode content = xml.CreateElement("Content");
            income.AppendChild(content);

            foreach (Position item in positions)
            {
                XmlNode position = xml.CreateElement("Item");
                content.AppendChild(position);

                addAttribute(xml, position, "Identity", item.Identity);
                addAttribute(xml, position, "AlcoCode", item.AlcoCode);
                addAttribute(xml, position, "AlcoStrength", string.IsNullOrWhiteSpace(item.Volume) ? "0" : item.Volume);
                addAttribute(xml, position, "Capacity", string.IsNullOrWhiteSpace(item.Capacity) ? "0" : item.Capacity);
                addAttribute(xml, position, "FormARegId", item.FormARegId);
                addAttribute(xml, position, "FormBRegId", item.FormBRegId);
                addAttribute(xml, position, "InformBRegId", item.InformBRegId);
                addAttribute(xml, position, "Producer", item.Producer);
                addAttribute(xml, position, "ProductVCode", item.ProductVCode);
                addAttribute(xml, position, "FullName", item.FullName);
                addAttribute(xml, position, "ShortName", item.ShortName);
                addAttribute(xml, position, "Price", convertToString(item.Price));
                addAttribute(xml, position, "Quantity", convertToString(item.Quantity));
                addAttribute(xml, position, "TotalPrice", convertToString(item.TotalPrice));
            }

            Program.Logger.Info(this, string.Format("... построение xml-документа формата 'AxiTrade' длиной {0} символов успешно завершено.", xml.OuterXml.Length));

            Program.Logger.Info(this, string.Format("Попытка сохранения xml-документа формата 'AxiTrade' в файл '{0}'...", filename));

            xml.Save(filename);

            Program.Logger.Info(this, string.Format("... сохранение xml-документа формата 'AxiTrade' в файле {0} успешно завершено.", filename));
        }
        #endregion Documents...

        #region OnlineEvents...
        /// <summary>
        /// Загрузить список событий.
        /// </summary>
        protected virtual void loadOnlineEvents()
        {
            List<OnlineEvent> list = storage.LoadOnlineEvents();

            if (list.Count > 0)
            {
                foreach (OnlineEvent onlineEvent in list)
                {
                    onlineEvents.Add(onlineEvent);
                }
            }
        }
        /// <summary>
        /// Сохранить список событий.
        /// </summary>
        protected virtual void saveOnlineEvents()
        {
            List<OnlineEvent> list = onlineEvents.OrderByDescending(x => x.Timestamp).Take(CountToSaveOnlineEvents).ToList();

            storage.SaveOnlineEvents(list);
        }
        #endregion OnlineEvents...

        #region Garbage...
        /// <summary>
        /// Загрузить корзину.
        /// </summary>
        /// <param name="verbose">Включить протоколирование загрузки.</param>
        protected virtual void loadGarbage(bool verbose = false)
        {
            try
            {
                Program.Logger.Info(this, "Попытка загрузки файлов корзины в память приложения...");

                garbage = new ThreadedBindingList<ADocument>();

                foreach (string fullname in storage.GetGarbageFileList())
                {
                    try
                    {
                        string filename = System.IO.Path.GetFileName(fullname);

                        if (string.IsNullOrWhiteSpace(filename)) continue;

                        #region Перебор всех типов...

                        // TODO: переделать на более элегантный вариант.
                        if (filename.StartsWith(typeof(Partner).FullName)) garbage.Add(storage.Load<Partner>(fullname));
                        else if (filename.StartsWith(typeof(Production).FullName)) garbage.Add(storage.Load<Production>(fullname));
                        else if (filename.StartsWith(typeof(InWaybill).FullName)) garbage.Add(storage.Load<InWaybill>(fullname));
                        else if (filename.StartsWith(typeof(FormA).FullName)) garbage.Add(storage.Load<FormA>(fullname));
                        else if (filename.StartsWith(typeof(FormB).FullName)) garbage.Add(storage.Load<FormB>(fullname));
                        else if (filename.StartsWith(typeof(Rests).FullName)) garbage.Add(storage.Load<Rests>(fullname));
                        else if (filename.StartsWith(typeof(ShopRests).FullName)) garbage.Add(storage.Load<ShopRests>(fullname));
                        else
                        {
                            Program.Logger.Warn(this, string.Format("Файл с наименованием '{0}' не поддерживается в данной версии корзины.", fullname));
                        }

                        #endregion Перебор всех типов...

                        if (verbose) Program.Logger.Info(this, string.Format("Документ из файла '{0}' успешно загружен в память приложения из корзины.", filename));
                    }
                    catch (Exception exception)
                    {
                        string msg = string.Format("Во время загрузки файла '{0}' в корзину произошла ошибка: '{1}'.", fullname, exception.Message);
                        Program.Logger.Error(msg, exception);
                        onlineEvents.Add(new OnlineEvent(OnlineEventStatus.Error, this, msg));
                    }
                }

                onlineEvents.Add(new OnlineEvent(this, string.Format("Выполнена загрузка файлов корзины (в количестве {0} шт.).", garbage.Count)));

                Program.Logger.Info(this, string.Format("... загрузка файлов корзины (в количестве {0} шт.) в память приложения успешно завершена.", garbage.Count));
            }
            catch (Exception exception)
            {
                const string msg = "Во время загрузки файлов корзины произошла ошибка.";

                Program.Logger.Error(msg, exception);
                onlineEvents.Add(new OnlineEvent(this, msg, exception));

                throw;
            }
        }
        /// <summary>
        /// Перенести документ в корзину.
        /// </summary>
        /// <param name="document">Документ.</param>
        protected virtual void moveToGarbage(ADocument document)
        {
            if (document == null) throw new Exception("Документ для переноса в корзину пустой ('null').");

            Type type = document.GetType();

            // TODO: переделать на более элегантный вариант.
            if (type == typeof(Partner)) partners.Remove((Partner)document);
            else if (type == typeof(Production)) production.Remove((Production)document);
            else if (type == typeof(InWaybill)) inWaybills.Remove((InWaybill)document);
            else if (type == typeof(FormA)) formAList.Remove((FormA)document);
            else if (type == typeof(FormB)) formBList.Remove((FormB)document);
            else if (type == typeof(Rests)) restsList.Remove((Rests)document);
            else if (type == typeof(BCodeRests)) bCodeRestsList.Remove((BCodeRests)document);
            else if (type == typeof(ShopRests)) shopRestsList.Remove((ShopRests)document);
            else throw new Exception(string.Format("Перенос в корзину для указанного типа документа ('{0}') не поддерживается в данной версии приложения.", type.FullName));

            if (garbage != null) garbage.Add(document);

            storage.MoveToGarbage(document);
        }
        /// <summary>
        /// Восстановить документ из корзины.
        /// </summary>
        /// <param name="document">Документ.</param>
        protected virtual void restoreFromGarbage(ADocument document)
        {
            if (document == null) throw new Exception("Документ для восстановления пустой ('null').");

            Type type = document.GetType();

            // TODO: переделать на более элегантный вариант.
            if (type == typeof(Partner)) partners.Add((Partner)document);
            else if (type == typeof(Production)) production.Add((Production)document);
            else if (type == typeof(InWaybill)) inWaybills.Add((InWaybill)document);
            else if (type == typeof(FormA)) formAList.Add((FormA)document);
            else if (type == typeof(FormB)) formBList.Add((FormB)document);
            else if (type == typeof(Rests)) restsList.Add((Rests)document);
            else if (type == typeof(BCodeRests)) bCodeRestsList.Add((BCodeRests)document);
            else if (type == typeof(ShopRests)) shopRestsList.Add((ShopRests)document);
            else throw new Exception(string.Format("Восстановление для указанного типа документа ('{0}') не поддерживается в данной версии приложения.", type.FullName));

            garbage.Remove(document);

            storage.RestoreFromGarbage(document);
        }
        #endregion Garbage...

        #region Forms...
        /// <summary>
        /// Отправить запрос на получение формы 'А'.
        /// </summary>
        /// <param name="format">Формат формы.</param>
        /// <param name="formRegId">Идентификатор.</param>
        protected virtual void requestForm(FormsFormat format, string formRegId)
        {
            XmlDocument xml = buildForm(format, formRegId);

            #region Сохранение исходящего запроса.
            xml.Save(string.Format("{0}\\{1}.requestform.{2}.{3}", storage.PathOut, FileStorage.NativePrefix, DateTime.Now.ToString("yyyyMMddHHmmss"), FileStorage.NativeExtension));
            #endregion Сохранение исходящего запроса.

            string address;

            if (documentsVersion == 1)
            {
                address = (format == FormsFormat.A) ? addresses.RequestFormA : addresses.RequestFormB;
            }
            else
            {
                address = (format == FormsFormat.A) ? addresses.RequestFormF1 : addresses.RequestFormF2;
            }

            Program.Logger.Info(this, string.Format("Попытка отправить данные '{0}' по адресу '{1}'...", xml.OuterXml, address));

            HttpTransport.UploadFile(address, xml, "xml_file", "text/xml; charset=utf-8", configuration.UtmTimeoutLong);

            Program.Logger.Info(this, "... данные успешно отправлены.");
        }
        /// <summary>
        /// Сформировать документ на получение формы 'А'.
        /// </summary>
        /// <param name="format">Формат формы.</param>
        /// <param name="formRegId">Идентификатор.</param>
        protected virtual XmlDocument buildForm(FormsFormat format, string formRegId)
        {
            XmlDocument xml = new XmlDocument();

            XmlNode declaration = xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            xml.AppendChild(declaration);

            XmlNode docsNode = xml.CreateElement(addresses.Ns.Prefix, "Documents", addresses.Ns.Uri);
            addAttribute(xml, docsNode, "Version", "1.0");
            addAttribute(xml, docsNode, "xmlns:" + addresses.Xsi.Prefix, addresses.Xsi.Uri);
            addAttribute(xml, docsNode, "xmlns:" + addresses.Ns.Prefix, addresses.Ns.Uri);

            if (documentsVersion == 1)
            {
                addAttribute(xml, docsNode, "xmlns:" + addresses.Qf.Prefix, addresses.Qf.Uri);
            }
            else
            {
                addAttribute(xml, docsNode, "xmlns:" + addresses.Qf2.Prefix, addresses.Qf2.Uri);
            }
            xml.AppendChild(docsNode);

            XmlNode owner = xml.CreateElement(addresses.Ns.Prefix, "Owner", addresses.Ns.Uri);
            docsNode.AppendChild(owner);

            XmlNode FsrarId = xml.CreateElement(addresses.Ns.Prefix, "FSRAR_ID", addresses.Ns.Uri);
            FsrarId.InnerText = configuration.FsrarId;
            owner.AppendChild(FsrarId);

            XmlNode doc = xml.CreateElement(addresses.Ns.Prefix, "Document", addresses.Ns.Uri);
            docsNode.AppendChild(doc);

            string nodeName;

            if (documentsVersion == 1)
            {
                nodeName = (format == FormsFormat.A) ? "QueryFormA" : "QueryFormB";
            }
            else
            {
                nodeName = (format == FormsFormat.A) ? "QueryFormF1" : "QueryFormF2";
            }
            XmlNode queryForm = xml.CreateElement(addresses.Ns.Prefix, nodeName, addresses.Ns.Uri);
            doc.AppendChild(queryForm);

            if (string.IsNullOrWhiteSpace(formRegId)) throw new Exception("Идентификатор ('FormRegId') раздела справки не может быть пустым.");

            XmlNode nodeFormRegId = (documentsVersion == 1) ? xml.CreateElement(addresses.Qf.Prefix, "FormRegId", addresses.Qf.Uri)
                                                            : xml.CreateElement(addresses.Qf2.Prefix, "FormRegId", addresses.Qf2.Uri);
            nodeFormRegId.InnerText = formRegId;
            queryForm.AppendChild(nodeFormRegId);

            return xml;
        }
        #endregion Forms...

        #region Tickets...
        /// <summary>
        /// Получить квиток.
        /// </summary>
        /// <param name="url">Адрес для загрузки.</param>
        /// <param name="replyId">Идентификатор исходного запроса.</param>
        /// <param name="xml">Ответ сервера.</param>
        protected virtual void getTickets(string url, string replyId, XmlDocument xml)
        {
            if (!string.IsNullOrWhiteSpace(replyId))
            {
                if ((xml.DocumentElement == null)
                    || (xml.DocumentElement["ns:Document"] == null)
                    || (xml.DocumentElement["ns:Document"]["ns:Ticket"] == null))
                    throw new Exception("Необходимые XML-ноды в документе пустые.");

                Ticket ticket = new Ticket(xml.DocumentElement["ns:Document"]["ns:Ticket"])
                {
                    ReplyId = replyId,
                    Url = url,
                    TicketType = (url.ToLower().Contains("cryptoticket") ? "CryptoTicket" : "Ticket")
                };

                add(ticket);

                #region "Откат" документов.
                if (ticket.Conclusion.ToLower() == "rejected")
                {
                    backoffInWaybill(replyId, ticket.ConclusionComment);
                    backoffOutWaybill(replyId, ticket.ConclusionComment);
                    backoffRevokedOutWaybill(replyId, ticket.ConclusionComment);
                    backoffActWaybill(replyId, ticket.ConclusionComment);
                }
                #endregion "Откат" документов.

                #region "Распроведение" накладных.
                if ((ticket.GetOperationName().ToLower() == "unconfirm") && (ticket.DocType.ToLower() == "waybill"))
                {
                    unconfirmInWaybill(ticket.GetRegId(), ticket.Conclusion, ticket.ConclusionComment);
                    unconfirmOutWaybill(ticket.GetRegId(), ticket.Conclusion, ticket.ConclusionComment);
                }
                #endregion "Распроведение" накладных.

                #region Ещё одно fucking распроведение.

                if (ticket.DocType.ToLower() == "requestrepealwb") repealInWaybill(ticket.ReplyId, ticket.Conclusion, ticket.ConclusionComment);
                if (ticket.DocType.ToLower() == "confirmrepealwb") repealInWaybill(ticket.ReplyId, ticket.Conclusion, ticket.ConclusionComment);

                #endregion Ещё одно fucking распроведение.

                #region Подтверждение или отказ актов движения товара.
                conclusionActChargeOn(ticket);
                conclusionActChargeOff(ticket);
                conclusionActChargeOnShop(ticket);
                conclusionActChargeOffShop(ticket);
                conclusionTransferToShop(ticket);
                conclusionTransferFromShop(ticket);
                #endregion Подтверждение или отказ актов движения товара.
            }
            else
            {
                Program.Logger.Warn(this, string.Format("Квитанция ответа сервера по ссылке '{0}' с пустым полем 'replyId' не будет сохранёна в базе данных.", url));
            }
        }
        /// <summary>
        /// Добавить новый документ.
        /// </summary>
        /// <param name="obj">Новый документ.</param>
        protected virtual void add(Ticket obj)
        {
            Program.Logger.Info(this, string.Format("Попытка добавить в хранилище полученный документ '{0}': '{1}'.", obj.GetType().FullName, obj.Description));

            // Увы, реалии ЕГАИС таковы, что на один запрос может быть прислано несколько ответов.
            // Ticket exist = tickets.FirstOrDefault(x => x.ReplyId == obj.ReplyId);
            // if (exist != null)
            // {
            //     Program.Logger.Warn(this, string.Format("Объект '{0}' уже присутствует в хранилище. " +
            //                                             "Будет произведена попытка замены существующего объекта на новый.", exist.Description));
            //     remove(exist);
            // }

            tickets.Add(obj);

            storage.Save(obj);

            Program.Logger.Info(this, "Новый документ успешно добавлен.");
        }
        /// <summary>
        /// Удалить документ из хранилища.
        /// </summary>
        /// <param name="obj">Документ для удаления.</param>
        protected virtual void remove(Ticket obj)
        {
            Program.Logger.Info(this, string.Format("Попытка удалить из хранилища документ '{0}': '{1}'.", obj.GetType().FullName, obj.Description));

            tickets.Remove(obj);

            storage.Delete(obj);

            Program.Logger.Info(this, "Документ успешно удалён из хранилища.");
        }
        #endregion Tickets...

        #region WaybillTicket...
        /// <summary>
        /// Получить список необработанных накладных.
        /// </summary>
        /// <param name="url">Адрес для загрузки.</param>
        /// <param name="xml">Ответ сервера.</param>
        protected virtual void getWaybillTicket(string url, XmlDocument xml)
        {
            if ((xml.DocumentElement == null)
                || (xml.DocumentElement["ns:Document"] == null)
                || (xml.DocumentElement["ns:Document"]["ns:ConfirmTicket"] == null))
                throw new Exception("Необходимые XML-ноды в документе пустые.");

            WaybillTicket waybillTicket = new WaybillTicket(xml.DocumentElement["ns:Document"]["ns:ConfirmTicket"]) { Url = url };

            add(waybillTicket);

            #region Дополнение входящих накладных.
            if ((waybillTicket.IsConfirmOriginal.ToLower() == "accepted") || ((waybillTicket.IsConfirmOriginal.ToLower() == "rejected")))
            {
                addCommentToInWaybill(waybillTicket.WBRegId, string.Format("Состояние акта: '{0}', комментарий: '{1}'.", waybillTicket.IsConfirm, waybillTicket.Note));
            }
            #endregion Дополнение входящих накладных.
        }
        /// <summary>
        /// Добавить новый документ.
        /// </summary>
        /// <param name="obj">Новый документ.</param>
        protected virtual void add(WaybillTicket obj)
        {
            Program.Logger.Info(this, string.Format("Попытка добавить в хранилище полученный документ '{0}': '{1}'.", obj.GetType().FullName, obj.Description));

            waybillTickets.Add(obj);

            storage.Save(obj);

            Program.Logger.Info(this, "Новый документ успешно добавлен.");
        }
        #endregion WaybillTicket...

        #region Movement...
        /// <summary>
        /// Обработка актов движения товара в соответствии с полученной квитанцией.
        /// </summary>
        /// <param name="ticket">Квитанция.</param>
        /// <param name="act">Акт.</param>
        protected virtual void conclusionMovementByTicket(Ticket ticket, AMovement act)
        {
            try
            {
                #region Подтверждение или отказ...
                if ((act.StatusEnum == MovementStatus.Sent) || (act.StatusEnum == MovementStatus.Confirmed))
                {
                    Program.Logger.Info(this, string.Format("Попытка обработки акта движения '{0}' по квитанции '{1}'...", act.Description, ticket.Description));

                    if (string.IsNullOrWhiteSpace(ticket.ReplyId)) throw new Exception("Идентификатор операции не может быть пустым.");
                    if (act.SendReplyId != ticket.ReplyId) throw new Exception("Акт движения товара не соответствует квитанции сервера.");

                    if (ticket.Conclusion.ToLower() == "rejected")
                    {
                        act.ChangeStatus(MovementStatus.Rejected);
                    }
                    else if (ticket.Conclusion.ToLower() == "accepted")
                    {
                        act.ChangeStatus(MovementStatus.Confirmed);
                    }

                    act.SetAdditionalComment(ticket.ConclusionComment);

                    string regId = ticket.GetRegId();
                    if (!string.IsNullOrWhiteSpace(regId)) act.SetEgaisRegId(regId);

                    storage.Save(act);

                    Program.Logger.Info(this, string.Format("... акт '{0}' обработан и сохранён...", act.Description));

                    onlineEvents.Add(new OnlineEvent(OnlineEventStatus.Warning, this, string.Format("По акту движения товара ('{0}') номер '{1}' от '{2}' получена квитанция сервера с результатом '{3}' ('{4}').",
                                                                                                    act.GetType().Name, act.Number, act.Date, ticket.Conclusion, ticket.ConclusionComment)));

                    Program.Logger.Info(this, string.Format("... попытка обработки акта движения '{0}' по квитанции '{1}' успешно завершена", act.Description, ticket.Description));
                }
                #endregion Подтверждение или отказ...

                if ((act.GetType() == typeof(ActChargeOn)) || (act.GetType() == typeof(ActChargeOff)))
                {
                    if (ticket.GetOperationName().ToLower() == "unconfirm")
                    {
                        #region Распроведение (только для склада организации)...

                            if (act.StatusEnum == MovementStatus.Repealed)
                            {
                                Program.Logger.Info(this, string.Format("Попытка распроведения акта движения '{0}' по квитанции '{1}'...", act.Description, ticket.Description));

                                if (string.IsNullOrWhiteSpace(ticket.ReplyId)) throw new Exception("Идентификатор операции не может быть пустым.");
                                if (act.SendReplyId != ticket.ReplyId) throw new Exception("Акт движения товара не соответствует квитанции сервера.");
                                if (ticket.GetRegId() != act.EgaisRegId) throw new Exception("Идентификатор акта движения товара не соответствует идентификатору документа в квитанции сервера.");

                                if (ticket.Conclusion.ToLower() == "rejected") act.ChangeStatus(MovementStatus.Confirmed);

                                act.SetAdditionalComment(ticket.ConclusionComment);

                                storage.Save(act);

                                Program.Logger.Info(this, string.Format("... акт '{0}' обработан и сохранён...", act.Description));

                                onlineEvents.Add(new OnlineEvent(OnlineEventStatus.Warning, this, string.Format("По акту движения товара ('{0}') номер '{1}' от '{2}' получена квитанция сервера с результатом '{3}' ('{4}').",
                                                                                                                act.GetType().Name, act.Number, act.Date, ticket.Conclusion, ticket.ConclusionComment)));

                                Program.Logger.Info(this, string.Format("... попытка распроведения акта движения '{0}' по квитанции '{1}' успешно завершена", act.Description, ticket.Description));
                            }

                        #endregion Распроведение (только для склада организации)...
                    }
                }
            }
            catch (Exception exception)
            {
                Program.Logger.Error("Во время проведения обработки акта движения товара по полученной квитанции произошла ошибка.", exception);

                throw;
            }
        }
        #endregion Movement...

        #endregion Защищённые методы класса.
    }
}
