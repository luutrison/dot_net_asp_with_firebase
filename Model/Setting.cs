namespace BAN_BANH.Model
{
    public class Setting
    {
        public string dbBanBanh { get; set; }
        public string dbBanBanhOrder { get; set; }

        public string API_FIRESTORE { get; set; }
        public string FIREBASE_READER_ID { get; set; }
    }

    public class SanPhamLink
    {
        public string ten { get; set; }
        public string suKien { get; set; }
        public int timeStamp { get; set; }
    }


    public static class VARIBLE
    {
        public static readonly string CODER_I = "coder-5d2de";
        public static readonly string WRITER_I = "coder-writer";
        public static readonly string API_FIRESTORE_CODER_READ = "./Configs/API/coder-5d2de-577aa17b3d6f.json";
        public static readonly string API_FIRESTORE_CODER_WRITER = "./Configs/API/coder-writer-be0810473956.json";
        public static readonly string TIMESTAMPSPLIT = "Timestamp:";
        public static readonly string DATE_TIME_FORMAT = "DATE_TIME_FORMAT";
        public static readonly string GOOGLE_APPLICATION_CREDENTIALS = "GOOGLE_APPLICATION_CREDENTIALS";
        public static readonly string URL_BANBANH_ORDER_API = "URL_BANBANH_API";
        public static readonly string CURRENT_ENV = "CURRENT_ENV";
        public static readonly string ENV_DEV = "ENV_DEV";
        public static readonly string ENV_PRODUCT = "ENV_PRODUCT";
        public static readonly string COOKIE_SESSION_NAME = "_GAP_";
        public static readonly string COOKIE_SESSION_NAME_SPLIT = "_GAP_";
        public static readonly string VIEW_LIMIT_IP_NAME = "IP_";
        public static readonly string COUNT_VIEW_LIMIT_IP_NAME = "COUNT_IP_";
        public static readonly string PATH_RAY_WAIT_PAGE = "/wait/";
        public static readonly string PATH_RAY_BUSY_PAGE = "/too-busy/";
        public static readonly string COUNT_LIMIT_IPS = "IPS_";
        public static readonly string COUNT_MAX_SESSION_NAME = "SESSION_";
        public static readonly string CATEGORY_HOME = "CATEGORY_HOME";



    }


    public static class ENV_KEY
    {
        public static readonly string ENV_API_ORDER_DEV = "https://localhost:7152/";
        public static readonly string ENV_API_ORDER_PRO = "http://ip/order";

    }

    public static class URL
    {
        public static readonly string URL_POST_NEW_ORDER = "i/order/order/postorder";
        public static readonly string URL_GET_ORDER = "i/order/order/getorder";

    }

    public static class REQUEST
    {
        public static readonly string GET_ORDER_HEADER_KEY_NAME = "Session-id";

    }


    public static class  FIREBASE_DB_COLLECTION
    {
        public static readonly string BANBANH = "BANBANH";
        public static readonly string SANPHAM = "SANPHAM";
        public static readonly string SOSAO = "SOSAO";
        public static readonly string DANHMUC = "DANHMUC";
        public static readonly string EVENT = "EVENT";
        public static readonly string ANHSANPHAM = "ANHSANPHAM";
        public static readonly string BB_ORDER_NC = "BANBANH_ORDER_NOT_CASH";
   
    }

    public static class FIREBASE_DB_FIELD
    {
        public static readonly string MSP = "msp";
        public static readonly string SAO = "sao";

        public static readonly string CM = "cm";
        public static readonly string SANPHAM__NGAY_NHAP_LIEU = "ngayNhapLieu";
        public static readonly string BB_SESSION_ID = "sessionId";
        public static readonly string BB_ADD_CARD_TIMESTAMP = "addCardTimestamp";

    }


    public static class FIREBASE_DB_DOCUMENT
    {
        public static readonly string CLIENT = "CLIENT";
        public static readonly string DASH = "DASH";
        public static readonly string BB_ORDER = "ORDER";
        public static readonly string BB_SESSION = "SESSION_USER_ORDER";
    }

    public static class SETTING
    {
        public static readonly string ERROR_LOGS_PATH = "./Error_Logs.txt";
        public static readonly int MAX_ERROR_LOGS_SIZE = 3 * 1024 * 1024;
        public static readonly int DEFAULT_CACHE_TIME_HOUR = 1;
        public static readonly int DEFAULT_CARD_TIME_HOUR = 5;
        public static readonly int MAX_LOAD_IPS = 5000;
        public static readonly int MAX_IPS = 1000;
        public static readonly int TIME_RESET_COUNT_LOAD = 5;
        public static readonly bool ALLOW_LIMIT_VIEW = true;

        public static readonly bool ALLOW_AUTO_CALCULATION_HARDWARE = true;
        public static readonly bool ALLOW_AUTO_SCALE = false;
        public static readonly int MINIMUM_MINITUTE_CHECK_EXPRITE = 5;


        public static readonly int RANGE_RESPONSE = 20 * 1024;
        public static readonly int SINGLE_BANWIDTH = 20 * 1024 * 1024;

    }
}
