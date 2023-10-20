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


    public static class MOE
    {
        public static string MOE_CONTROLLER = "MOE_CONTROLLER";
        public static string MOE_MODEL = "MOE_MODEL";
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
        public static readonly string BAD_GUY_ZONE = "/a-zone-for-bad-guy";
        public static readonly string HI_HIGHT = "HI-HIGHT-HIDE";
        public static readonly string REALY_HI = "22-2-222-22222222-2-222-2222";
        public static readonly string HUMAN_WEB_SEVER_CODER = "0x4AAAAAAAJ2Gu3M5nJ0e82GeQLL1q85-Dg";
        public static readonly string HUMAN_WEB_CLIENT_CODER = "0x4AAAAAAAJ2GjePJjnFpWVv";
        public static readonly string COMP_COLORI = "ColorI";
        public static readonly string SECT_COLORI = "colori";
        public static readonly string NAME_HEADER = "Header";
        public static readonly string NAME_HEADER_DRUM = "drum";
        public static readonly string NAME_HEADER_NORMAL = "normal";
        public static readonly string COMP_FOOTER = "Footer";
        public static readonly string COMP_COLOR_II = "ColorII";
        public static readonly string SECT_COLORII = "colorii";
        public static readonly string HUMAN_VALIDATE_URL = "https://challenges.cloudflare.com/turnstile/v0/siteverify";
        public static readonly string NAME_SECRET = "secret";
        public static readonly string NAME_RESPONSE = "response";
        public static readonly string NAME_NOTIFIER = "Notifier";
        public static readonly string NAME_NOTIER = "NOTIER_";
        public static readonly dynamic DEFAULT_VIEW = null;
        public static readonly string STATIC_UPDATE_NAME = "1694268261";

        public static readonly bool IS_NEAD_PARKING = false;
        public static readonly bool PARKING_AND_CONTINUTE = false;
        public static readonly int PARKING_TIME_SECOND = 10000;



        public static readonly List<string> LIST_COLOR_I = new List<string>()
        {
            "/bootstrap/css/bootstrap.min.css",
            "/css/aos.min.css",
            "/css/animate.min.css",
            "/css/color.css",
            "/extends/notifier/notifier.css"
        };
        public static readonly List<string> LIST_COLOR_II = new List<string>()
        {
            "/js/jquery.min.js",
            "/bootstrap/js/bootstrap.min.js",
            "/js/aos.min.js",
            "/js/bs-init.js",
            "/js/color.js",
            "/extends/notifier/notifier.js"
        };
    }


    public static class NOTIFY
    {
        public static readonly Notifier ORDER_SUCCESS = new Notifier()
        {
            notifier = "Đơn đặt hàng của bạn đã được ghi lại thành công",
            autoDisable = false
        };
    }


    public static class ENV_KEY
    {
        public static readonly string ENV_API_ORDER_DEV = "https://localhost:5200/";
        public static readonly string ENV_API_ORDER_PRO = "http://www.content-file-banbanh.somee.com/";
        public static readonly string ENV_STATIC_DEV = "https://banbanh-static-load.pages.dev/" + VARIBLE.STATIC_UPDATE_NAME + "/";
        public static readonly string ENV_STATIC_PRO = "https://banbanh-static-load.pages.dev/" + VARIBLE.STATIC_UPDATE_NAME + "/";

    }

    public static class URL
    {
        public static readonly string URL_POST_NEW_ORDER = "i/order/order/postorder";
        public static readonly string URL_GET_ORDER = "i/order/order/getorder";
        public static readonly string URL_DELETE_ORDER = "i/order/order/deleteorder";
        public static readonly string URL_ADD_ORDER = "i/order/order/addOrder";

    }

    public static class REQUEST
    {
        public static readonly string GET_ORDER_HEADER_KEY_NAME = "Session-id";

    }


    public static class FIREBASE_DB_COLLECTION
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
