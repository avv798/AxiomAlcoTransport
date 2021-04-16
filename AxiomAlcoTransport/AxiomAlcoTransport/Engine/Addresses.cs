namespace Axiom.AlcoTransport
{
    /// <summary>
    /// Вспомогательный класс представления адресов сервера УТМ.
    /// </summary>
    public class Addresses
    {
        #region Защищенные объекты класса.
        /// <summary>
        /// Адрес сервера УТМ.
        /// Только чтение.
        /// </summary>
        protected readonly string address;
        /// <summary>
        /// Порт сервера УТМ.
        /// Только чтение.
        /// </summary>
        protected readonly string port;
        /// <summary>
        /// Пространство имён "xs".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix xs;
        /// <summary>
        /// Пространство имён "xsi".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix xsi;
        /// <summary>
        /// Пространство имён "ns".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix ns;
        /// <summary>
        /// Пространство имён "oref".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix oref;
        /// <summary>
        /// Пространство имён "oref2".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix oref2;
        /// <summary>
        /// Пространство имён "qp".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix qp;
        /// <summary>
        /// Пространство имён "qp".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix qpAco;
        /// <summary>
        /// Пространство имён "qp".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix qpAwo;
        /// <summary>
        /// Пространство имён "qf".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix qf;
        /// <summary>
        /// Пространство имён "qf" (v2).
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix qf2;
        /// <summary>
        /// Пространство имён "pref".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix pref;
        /// <summary>
        /// Пространство имён "pref2".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix pref2;
        /// <summary>
        /// Пространство имён "rfa".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix rfa;
        /// <summary>
        /// Пространство имён "wa".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix wa;
        /// <summary>
        /// Пространство имён "wa2".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix wa2; 
        /// <summary>
        /// Пространство имён "wa3".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix wa3;
        /// <summary>
        /// Пространство имён "wa3".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix wa4;
        /// <summary>
        /// Пространство имён "wb".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix wb;
        /// <summary>
        /// Пространство имён "wb2".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix wb2; 
        /// <summary>
        /// Пространство имён "wb3".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix wb3;
        /// <summary>
        /// Пространство имён "wb4".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix wb4;
        /// <summary>
        /// Пространство имён "ce3".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix ce3;
        /// <summary>
        /// Пространство имён "bk".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix bk;
        /// <summary>
        /// Пространство имён "ce".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix ce;
        /// <summary>
        /// Пространство имён "c".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix c;
        /// <summary>
        /// Пространство имён "wt".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix wt;
        /// <summary>
        /// Пространство имён "wtRepeal".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix wtRepeal;
        /// <summary>
        /// Пространство имён "ain".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix ain;
        /// <summary>
        /// Пространство имён "ain2".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix ain2;
        /// <summary>
        /// Пространство имён "aint".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix aint;
        /// <summary>
        /// Пространство имён "ainp".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix ainp;
        /// <summary>
        /// Пространство имён "iab".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix iab;
        /// <summary>
        /// Пространство имён "iab2".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix iab2;
        /// <summary>
        /// Пространство имён "awr".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix awr;
        /// <summary>
        /// Пространство имён "awr2".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix awr2;
        /// <summary>
        /// Пространство имён "awr3".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix awr3;
        /// <summary>
        /// Пространство имён "awr2".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix awrShop2;
        /// <summary>
        /// Пространство имён "awr".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix awrFixBc;  
        /// <summary>
        /// Пространство имён "awr".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix awrUnFixBc;
        /// <summary>
        /// Пространство имён "tts".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix tts;
        /// <summary>
        /// Пространство имён "tfs".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix tfs;
        /// <summary>
        /// Пространство имён "qp".
        /// Только чтение.
        /// </summary>
        protected readonly XmlPrefix qpRepealWB;

        public const string NsUrl = "http://fsrar.ru/WEGAIS/WB_DOC_SINGLE_01";
        public const string Wb2Url = "http://fsrar.ru/WEGAIS/TTNSingle_v2";
        public const string Oref2Url = "http://fsrar.ru/WEGAIS/ClientRef_v2";
        public const string Pref2Url = "http://fsrar.ru/WEGAIS/ProductRef_v2";
        public const string Ce3Url = "http://fsrar.ru/WEGAIS/CommonV3";
        public const string WbrUrl = "http://fsrar.ru/WEGAIS/TTNInformBReg";

        #endregion Защищенные объекты класса.

        #region Внешние объекты класса.
        /// <summary>
        /// Основной адрес сервера УТМ.
        /// Только чтение.
        /// </summary>
        public string Main
        {
            get
            {
                // Именно так: без слэша в конце адреса.
                return string.Format("http://{0}:{1}", address, port);
            }
        }
        /// <summary>
        /// Список запросов, отправленных на сервер УТМ.
        /// Только чтение.
        /// </summary>
        public string GetRequestOut
        {
            get
            {
                // Именно так: "/opt/in", а объект "...Out".
                // Ибо мы рассуждаем в терминах клиентского приложения, а не в терминах сервера.
                return string.Format("{0}/opt/in", Main);
            }
        }
        /// <summary>
        /// Запрос на получение справочника организаций.
        /// Только чтение.
        /// </summary>
        public string RequestPartners
        {
            get
            {
                return string.Format("{0}/QueryPartner", GetRequestOut);
            }
        }
        /// <summary>
        /// Запрос на получение справочника организаций (v2).
        /// Только чтение.
        /// </summary>
        public string RequestPartners_v2
        {
            get
            {
                return string.Format("{0}/QueryClients_v2", GetRequestOut);
            }
        }
        /// <summary>
        /// Отправка акта по ТТН.
        /// Только чтение.
        /// </summary>
        public string SendWaybillAct
        {
            get
            {
                return string.Format("{0}/WayBillAct", GetRequestOut);
            }
        }
         
       

        /// <summary>
        /// Отправка подтверждения акта.
        /// Только чтение.
        /// </summary>
        public string SendConfirm
        {
            get
            {
                return string.Format("{0}/WayBillTicket", GetRequestOut);
            }
        }
        /// <summary>
        /// Отправка накладной.
        /// Только чтение.
        /// </summary>
        public string SendWaybill
        {
            get
            {
                return string.Format("{0}/WayBill", GetRequestOut);
            }
        }
        /// <summary>
        /// Отправка накладной (v2).
        /// Только чтение.
        /// </summary>
        public string SendWaybill_v2
        {
            get
            {
                return string.Format("{0}/WayBill_v2", GetRequestOut);
            }
        }
        /// <summary>
        /// Отправка запроса на распроведение накладной.
        /// Только чтение.
        /// </summary>
        public string RequestRepealWaybill
        {
            get
            {
                return string.Format("{0}/RequestRepealWB", GetRequestOut);
            }
        }
        /// <summary>
        /// Отправка подтверждения на запроса распроведения накладной.
        /// Только чтение.
        /// </summary>
        public string SendConfirmRepealWaybill
        {
            get
            {
                return string.Format("{0}/ConfirmRepealWB", GetRequestOut);
            }
        }
        /// <summary>
        /// Отправка акта постановки товара на баланс.
        /// Только чтение.
        /// </summary>
        public string SendActChargeOn
        {
            get
            {
                return string.Format("{0}/ActChargeOn", GetRequestOut);
            }
        }
        /// <summary>
        /// Отправка акта постановки товара на баланс (v2).
        /// Только чтение.
        /// </summary>
        public string SendActChargeOn_v2
        {
            get
            {
                return string.Format("{0}/ActChargeOn_v2", GetRequestOut);
            }
        } 
        /// <summary>
        /// Отправка акта фиксации штрихкодов.
        /// Только чтение.
        /// </summary>
        public string SendActFixBarcode => $"{GetRequestOut}/ActFixBarCode"; 
        
        /// <summary>
        /// Отправка акта отмены фиксации штрихкодов.
        /// Только чтение.
        /// </summary>
        public string SendActUnFixBarcode => $"{GetRequestOut}/ActUnFixBarCode";

        /// <summary>
        /// Отмена акта постановки товара на баланс.
        /// Только чтение.
        /// </summary>
        public string RepealActChargeOn
        {
            get
            {
                return string.Format("{0}/RequestRepealACO", GetRequestOut);
            }
        }
        /// <summary>
        /// Отправка акта списания товара.
        /// Только чтение.
        /// </summary>
        public string SendActChargeOff
        {
            get
            {
                return string.Format("{0}/ActWriteOff", GetRequestOut);
            }
        }
        /// <summary>
        /// Отправка акта списания товара (v2-v3).
        /// Только чтение.
        /// </summary>
        public string SendActChargeOff_v2(int version) => $"{GetRequestOut}/ActWriteOff_v{version}";

        /// <summary>
        /// Отмена акта снятия товара с баланса.
        /// Только чтение.
        /// </summary>
        public string RepealActChargeOff
        {
            get
            {
                return string.Format("{0}/RequestRepealAWO", GetRequestOut);
            }
        }
        /// <summary>
        /// Отправка акта постановки товара на баланс в товарном зале.
        /// Только чтение.
        /// </summary>
        public string SendActChargeOnShop
        {
            get
            {
                return string.Format("{0}/ActChargeOnShop_v2", GetRequestOut);
            }
        }
        /// <summary>
        /// Отправка акта списания товара из товарного зала.
        /// Только чтение.
        /// </summary>
        public string SendActChargeOffShop
        {
            get
            {
                return string.Format("{0}/ActWriteOffShop_v2", GetRequestOut);
            }
        }
        /// <summary>
        /// Отправка документа передачи товара в торговый зал.
        /// Только чтение.
        /// </summary>
        public string SendTransferToShop
        {
            get
            {
                return string.Format("{0}/TransferToShop", GetRequestOut);
            }
        }
        /// <summary>
        /// Отправка документа возврата товара из торгового зала.
        /// Только чтение.
        /// </summary>
        public string SendTransferFromShop
        {
            get
            {
                return string.Format("{0}/TransferFromShop", GetRequestOut);
            }
        }
        /// <summary>
        /// Запрос на повторное получение накладной.
        /// Только чтение.
        /// </summary>
        public string QueryResendDoc
        {
            get
            {
                return string.Format("{0}/QueryResendDoc", GetRequestOut);
            }
        }
        /// <summary>
        /// Запрос на получение справочника алкогольной продукции.
        /// Только чтение.
        /// </summary>
        public string RequestAP
        {
            get
            {
                return string.Format("{0}/QueryAP", GetRequestOut);
            }
        }
        /// <summary>
        /// Запрос на получение справочника спирта.
        /// Только чтение.
        /// </summary>
        public string RequestSpirit
        {
            get
            {
                return string.Format("{0}/QuerySP_v2", GetRequestOut);
            }
        }
        /// <summary>
        /// Запрос на получение справочника спиртосодержащей продукции.
        /// Только чтение.
        /// </summary>
        public string RequestSpiritContainer
        {
            get
            {
                return string.Format("{0}/QuerySSP_v2", GetRequestOut);
            }
        }
        /// <summary>
        /// Запрос на получение справочника алкогольной продукции (v2).
        /// Только чтение.
        /// </summary>
        public string RequestAP_v2
        {
            get
            {
                return string.Format("{0}/QueryAP_v2", GetRequestOut);
            }
        }
        /// <summary>
        /// Запрос на получение остатков на складе организации.
        /// Только чтение.
        /// </summary>
        public string RequestRests
        {
            get
            {
                return string.Format("{0}/QueryRests", GetRequestOut);
            }
        }
        /// <summary>
        /// Запрос на получение остатков на складе организации (v2).
        /// Только чтение.
        /// </summary>
        public string RequestRests_v2
        {
            get
            {
                return string.Format("{0}/QueryRests_v2", GetRequestOut);
            }
        }
        /// <summary>
        /// Запрос на списка необработанных накладных.
        /// Только чтение.
        /// </summary>
        public string RequestNotAnswer
        {
            get
            {
                return string.Format("{0}/QueryNATTN", GetRequestOut);
            }
        }
        /// <summary>
        /// Запрос на получение остатков в торговом зале.
        /// Только чтение.
        /// </summary>
        public string RequestShopRests
        {
            get
            {
                return string.Format("{0}/QueryRestsShop_v2", GetRequestOut);
            }
        }
        /// <summary>
        /// Запрос на получение остатков по штрихкодам.
        /// Только чтение.
        /// </summary>
        public string RequestBCodeRests => $"{GetRequestOut}/QueryRestBCode";

        /// <summary>
        /// Запрос на получение формы 'А'.
        /// Только чтение.
        /// </summary>
        public string RequestFormA
        {
            get
            {
                return string.Format("{0}/QueryFormA", GetRequestOut);
            }
        }
        /// <summary>
        /// Запрос на получение формы 'Б'.
        /// Только чтение.
        /// </summary>
        public string RequestFormB
        {
            get
            {
                return string.Format("{0}/QueryFormB", GetRequestOut);
            }
        }
        /// <summary>
        /// Запрос на получение формы 'А' (v2).
        /// Только чтение.
        /// </summary>
        public string RequestFormF1
        {
            get
            {
                return string.Format("{0}/QueryFormF1", GetRequestOut);
            }
        }
        /// <summary>
        /// Запрос на получение формы 'Б' (v2).
        /// Только чтение.
        /// </summary>
        public string RequestFormF2
        {
            get
            {
                return string.Format("{0}/QueryFormF2", GetRequestOut);
            }
        }
        /// <summary>
        /// Запрос на получение движения по форме 'Б'.
        /// Только чтение.
        /// </summary>
        public string RequestHistoryFormB
        {
            get
            {
                return string.Format("{0}/QueryHistoryFormB", GetRequestOut);
            }
        }
        /// <summary>
        /// Запрос на получение движения по форме 'Б' (v2).
        /// Только чтение.
        /// </summary>
        public string RequestHistoryFormF2
        {
            get
            {
                return string.Format("{0}/QueryForm2History", GetRequestOut);
            }
        }
        /// <summary>
        /// Запрос на получение движения по форме 'Б'.
        /// Только чтение.
        /// </summary>
        public string RequestMarkBarcode
        {
            get
            {
                return string.Format("{0}/QueryBarcode", GetRequestOut);
            }
        }
        /// <summary>
        /// Список заголовков документов, полученных с сервера УТМ.
        /// Только чтение.
        /// </summary>
        public string GetRequestIn
        {
            get
            {
                // Именно так: "/opt/out", а объект "...In".
                // Ибо мы рассуждаем в терминах клиентского приложения, а не в терминах сервера.
                return string.Format("{0}/opt/out", Main);
            }
        }
        /// <summary>
        /// Пространство имён "xsi".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Xsi
        {
            get { return xsi; }
        }
        /// <summary>
        /// Пространство имён "xs".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Xs
        {
            get { return xs; }
        }
        /// <summary>
        /// Пространство имён "ns".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Ns
        {
            get { return ns; }
        }
        /// <summary>
        /// Пространство имён "oref".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Oref
        {
            get { return oref; }
        }
        /// <summary>
        /// Пространство имён "oref2".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Oref2
        {
            get { return oref2; }
        }
        /// <summary>
        /// Пространство имён "qp".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Qp
        {
            get { return qp; }
        }
        /// <summary>
        /// Пространство имён "qf".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Qf
        {
            get { return qf; }
        }
        /// <summary>
        /// Пространство имён "qf" (v2).
        /// Только чтение.
        /// </summary>
        public XmlPrefix Qf2
        {
            get { return qf2; }
        }
        /// <summary>
        /// Пространство имён "pref".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Pref
        {
            get { return pref; }
        }
        /// <summary>
        /// Пространство имён "pref2".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Pref2
        {
            get { return pref2; }
        }
        /// <summary>
        /// Пространство имён "rfa".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Rfa
        {
            get { return rfa; }
        }
        /// <summary>
        /// Пространство имён "bk".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Bk
        {
            get { return bk; }
        }
        /// <summary>
        /// Пространство имён "ce".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Ce
        {
            get { return ce; }
        }
        /// <summary>
        /// Пространство имён "wa".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Wa
        {
            get { return wa; }
        }
        /// <summary>
        /// Пространство имён "wa2".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Wa2
        {
            get { return wa2; }
        } 
        /// <summary>
        /// Пространство имён "wa3".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Wa3 => wa3;
        /// <summary>
        /// Пространство имён "wa4".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Wa4 => wa4;
        /// <summary>
        /// Пространство имён "wb".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Wb
        {
            get { return wb; }
        }
        /// <summary>
        /// Пространство имён "wb2".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Wb2
        {
            get { return wb2; }
        } 
        /// <summary>
        /// Пространство имён "wb3".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Wb3 => wb3;

        /// <summary>
        /// Пространство имён "c".
        /// Только чтение.
        /// </summary>
        public XmlPrefix C
        {
            get { return c; }
        }
        /// <summary>
        /// Пространство имён "wt".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Wt
        {
            get { return wt; }
        }
        /// <summary>
        /// Пространство имён "wtRepeal".
        /// Только чтение.
        /// </summary>
        public XmlPrefix WtRepeal
        {
            get { return wtRepeal; }
        }
        /// <summary>
        /// Пространство имён "ain".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Ain
        {
            get { return ain; }
        }
        /// <summary>
        /// Пространство имён "ain2".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Ain2
        {
            get { return ain2; }
        }
        /// <summary>
        /// Пространство имён "aint".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Aint
        {
            get { return aint; }
        }
        /// <summary>
        /// Пространство имён "ainp".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Ainp
        {
            get { return ainp; }
        }
        /// <summary>
        /// Пространство имён "iab".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Iab
        {
            get { return iab; }
        }
        /// <summary>
        /// Пространство имён "iab2".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Iab2
        {
            get { return iab2; }
        }
        /// <summary>
        /// Пространство имён "awr".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Awr
        {
            get { return awr; }
        }
        /// <summary>
        /// Пространство имён "awr2".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Awr2
        {
            get { return awr2; }
        } 
        /// <summary>
        /// Пространство имён "awr3".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Awr3 => awr3;

        /// <summary>
        /// Пространство имён "awr2".
        /// Только чтение.
        /// </summary>
        public XmlPrefix AwrShop2
        {
            get { return awrShop2; }
        } 
        /// <summary>
        /// Пространство имён "awr".
        /// Только чтение.
        /// </summary>
        public XmlPrefix AwrFixBc => awrFixBc;
        /// <summary>
        /// Пространство имён "awr".
        /// Только чтение.
        /// </summary>
        public XmlPrefix AwrUnFixBc => awrUnFixBc;

        /// <summary>
        /// Пространство имён "tts".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Tts
        {
            get { return tts; }
        }
        /// <summary>
        /// Пространство имён "tfs".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Tfs
        {
            get { return tfs; }
        }
        /// <summary>
        /// Пространство имён "qp".
        /// Только чтение.
        /// </summary>
        public XmlPrefix QpAco
        {
            get { return qpAco; }
        }
        /// <summary>
        /// Пространство имён "qp".
        /// Только чтение.
        /// </summary>
        public XmlPrefix QpAwo
        {
            get { return qpAwo; }
        }
        /// <summary>
        /// Пространство имён "qp".
        /// Только чтение.
        /// </summary>
        public XmlPrefix QpRepealWB
        {
            get { return qpRepealWB; }
        }
        /// <summary>
        /// Пространство имён "ce".
        /// Только чтение.
        /// </summary>
        public XmlPrefix Ce3 => ce3;

        public XmlPrefix Wb4 => wb4;

        #endregion Внешние объекты класса.
        
        #region Конструкторы класса.
        /// <summary>
        /// Защищённый конструктор класса "по умолчанию".
        /// </summary>
        protected Addresses()
        {
            address = string.Empty;
            port = string.Empty;

            ain = new XmlPrefix { Prefix = "ain", Uri = "http://fsrar.ru/WEGAIS/ActChargeOn" };
            ain2 = new XmlPrefix { Prefix = "ainp", Uri = "http://fsrar.ru/WEGAIS/ActChargeOn_v2" };
            ainp = new XmlPrefix { Prefix = "ainp", Uri = "http://fsrar.ru/WEGAIS/ActChargeOnShop_v2" };
            aint = new XmlPrefix { Prefix = "aint", Uri = "http://fsrar.ru/WEGAIS/ActInventoryInformBReg" };
            awr = new XmlPrefix { Prefix = "awr", Uri = "http://fsrar.ru/WEGAIS/ActWriteOff" };
            awr2 = new XmlPrefix { Prefix = "awr", Uri = "http://fsrar.ru/WEGAIS/ActWriteOff_v2" };
            awr3 = new XmlPrefix { Prefix = "awr", Uri = "http://fsrar.ru/WEGAIS/ActWriteOff_v3" };
            awrShop2 = new XmlPrefix { Prefix = "awr", Uri = "http://fsrar.ru/WEGAIS/ActWriteOffShop_v2" };
            awrFixBc = new XmlPrefix { Prefix = "awr", Uri = "http://fsrar.ru/WEGAIS/ActFixBarCode" };
            awrUnFixBc = new XmlPrefix { Prefix = "awr", Uri = "http://fsrar.ru/WEGAIS/ActUnFixBarCode" };
            bk = new XmlPrefix { Prefix = "bk", Uri = "http://fsrar.ru/WEGAIS/QueryBarcode" };
            c = new XmlPrefix { Prefix = "c", Uri = "http://fsrar.ru/WEGAIS/Common" };
            ce = new XmlPrefix { Prefix = "ce", Uri = "http://fsrar.ru/WEGAIS/CommonEnum" };
            iab = new XmlPrefix { Prefix = "iab", Uri = "http://fsrar.ru/WEGAIS/ActInventoryABInfo" };
            iab2 = new XmlPrefix { Prefix = "iab", Uri = "http://fsrar.ru/WEGAIS/ActInventoryF1F2Info" };
            ns = new XmlPrefix { Prefix = "ns", Uri = NsUrl };
            oref = new XmlPrefix { Prefix = "oref", Uri = "http://fsrar.ru/WEGAIS/ClientRef" };
            
            oref2 = new XmlPrefix { Prefix = "oref", Uri = Oref2Url };
            pref = new XmlPrefix { Prefix = "pref", Uri = "http://fsrar.ru/WEGAIS/ProductRef" };
            
            pref2 = new XmlPrefix { Prefix = "pref", Uri = Pref2Url };
            qf = new XmlPrefix { Prefix = "qf", Uri = "http://fsrar.ru/WEGAIS/QueryFormAB" };
            qf2 = new XmlPrefix { Prefix = "qf", Uri = "http://fsrar.ru/WEGAIS/QueryFormF1F2" };
            qp = new XmlPrefix { Prefix = "qp", Uri = "http://fsrar.ru/WEGAIS/QueryParameters" };
            qpAco = new XmlPrefix { Prefix = "qp", Uri = "http://fsrar.ru/WEGAIS/RequestRepealACO" };
            qpAwo = new XmlPrefix { Prefix = "qp", Uri = "http://fsrar.ru/WEGAIS/RequestRepealAWO" };
            qpRepealWB = new XmlPrefix { Prefix = "qp", Uri = "http://fsrar.ru/WEGAIS/RequestRepealWB" };
            rfa = new XmlPrefix { Prefix = "rfa", Uri = "http://fsrar.ru/WEGAIS/ReplyFormA" };
            tfs = new XmlPrefix { Prefix = "tfs", Uri = "http://fsrar.ru/WEGAIS/TransferFromShop" };
            tts = new XmlPrefix { Prefix = "tts", Uri = "http://fsrar.ru/WEGAIS/TransferToShop" };
            wa = new XmlPrefix { Prefix = "wa", Uri = "http://fsrar.ru/WEGAIS/ActTTNSingle" };
            wa2 = new XmlPrefix { Prefix = "wa", Uri = "http://fsrar.ru/WEGAIS/ActTTNSingle_v2" };
            wa3 = new XmlPrefix { Prefix = "wa", Uri = "http://fsrar.ru/WEGAIS/ActTTNSingle_v3" };
            wa4 = new XmlPrefix { Prefix = "wa", Uri = "http://fsrar.ru/WEGAIS/ActTTNSingle_v4" };
            
            wb = new XmlPrefix { Prefix = "wb", Uri = "http://fsrar.ru/WEGAIS/TTNSingle" };
            wb2 = new XmlPrefix { Prefix = "wb", Uri = Wb2Url };
            wb3 = new XmlPrefix { Prefix = "wb", Uri = "http://fsrar.ru/WEGAIS/TTNSingle_v3" };
            wb4 = new XmlPrefix { Prefix = "wb", Uri = "http://fsrar.ru/WEGAIS/TTNSingle_v4" };

            ce3 = new XmlPrefix { Prefix = "ce", Uri = Ce3Url };
            wt = new XmlPrefix { Prefix = "wt", Uri = "http://fsrar.ru/WEGAIS/ConfirmTicket" };
            wtRepeal = new XmlPrefix { Prefix = "wt", Uri = "http://fsrar.ru/WEGAIS/ConfirmRepealWB" };
            xs = new XmlPrefix { Prefix = "xs", Uri = "http://www.w3.org/2001/XMLSchema" };
            xsi = new XmlPrefix { Prefix = "xsi", Uri = "http://www.w3.org/2001/XMLSchema-instance" };
        }
        /// <summary>
        /// Конструктор класса с параметрами.
        /// </summary>
        /// <param name="address">Адрес сервера УТМ.</param>
        /// <param name="port">Порт сервера УТМ.</param>
        public Addresses(string address, string port) : this()
        {
            this.address = address;
            this.port = port;
        }
        #endregion Конструкторы класса.

        #region Внешние методы класса.

        public string SendWaybill_v3(int version) => $"{GetRequestOut}/WayBill_v{version}";
        /// <summary>
        /// Отправка акта по ТТН (v3).
        /// Только чтение.
        /// </summary>
        public string SendWaybillAct_v3(int version) => $"{GetRequestOut}/WayBillAct_v{version}";
     
        #endregion
    }
}
