using BAN_BANH.Model;
using BAN_BANH.Model.env;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

namespace BAN_BANH.Method
{

    public static class USE_ENVIROMENT
    {
        public static void ENVIROMENT_CODER_I()
        {
            Environment.SetEnvironmentVariable(VARIBLE.GOOGLE_APPLICATION_CREDENTIALS, VARIBLE.API_FIRESTORE_CODER_READ);
        }
        public static void ENVIROMENT_WRITER_I()
        {
            Environment.SetEnvironmentVariable(VARIBLE.GOOGLE_APPLICATION_CREDENTIALS, VARIBLE.API_FIRESTORE_CODER_WRITER);
        }
    }



    /***
     * Lấy thông tin về sản phẩm theo mã sản phẩm, nếu sản phẩm đã tồn tại thì
     * nấy sản phẩm từ cache ra, nếu như chưa có thì nấy về từ database rồi lưu lại vào cache
     * **/

    public class PRODUCT
    {
        private readonly IMemoryCache _memoryCache;
        public PRODUCT(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /***
         * Nấy sản phẩm theo chuyên mục được liệt kê trong phần client
         * **/
        public List<DanhMuc> GetCategoryOfHomePage()
        {
            try
            {
                var listItem = _memoryCache.GetOrCreate(VARIBLE.CATEGORY_HOME, entie =>
                {
                    var db = FirestoreDb.Create(VARIBLE.CODER_I);
                    var collection = new DB_DOCUMENT(db).CLIENT().Collection(FIREBASE_DB_COLLECTION.DANHMUC).GetSnapshotAsync();

                    var listDanhMuc = new ParseDataTwo().ListDanhMuc(collection).Result;

                    entie.SetValue(listDanhMuc);

                    new CacheExpireNoRuntime(_memoryCache).addCacheItem(new()
                    {
                        name = VARIBLE.CATEGORY_HOME,
                        time = Convert.ToInt32(new MethodOne().TimeStamp() + TimeSpan.FromHours(SETTING.DEFAULT_CACHE_TIME_HOUR).TotalSeconds)
                    });

                    return listDanhMuc;

                });

                return listItem;
            }
            catch (Exception)
            {
                //return new List<DanhMuc> { };
                throw;
            }


        }


        /***
         * Tạo Danh sách sản phẩm mà người dùng đặt hàng
         * **/

        private SanPham SortSanPham(SessionOrder order, List<BlockCateOnHomePage> bl)
        {
            var spOut = new SanPham();
            foreach(var item in bl)
            {
                var sp = item.sanPham.Where(x => x.msp == order.msp).FirstOrDefault();
                if (sp != null) {
                    spOut = sp;
                }
                else
                {
                    /***
                     * Lấy thông tin sản phẩm từ database, nếu có thì add lại vào một list khác
                     * **/

                    FirestoreDb db = FirestoreDb.Create(VARIBLE.CODER_I);

                    var banh = new DB_DOCUMENT(db).CLIENT().Collection(FIREBASE_DB_COLLECTION.SANPHAM)
                        .WhereEqualTo(FIREBASE_DB_FIELD.MSP, order.msp);
                    var parseTwo = new ParseDataTwo();
                    var itb = parseTwo.ListSanPham(banh.GetSnapshotAsync()).Result;
                    if (itb.FirstOrDefault() != null) { 
                    
                        spOut = itb.FirstOrDefault();
                        var isp = bl.Where(x => spOut.cm.Contains(x.danhMuc.Id)).FirstOrDefault();

                        var msp = isp.sanPham.Where(x => x.msp == spOut.msp).FirstOrDefault();

                        if (msp == null)
                        {
                            isp.sanPham.Add(spOut);
                            _memoryCache.Set(CACHEKEY.CACHE_HOME_SPECIAL_PRODUCT, bl);
                        }
                    }
                }
            }

            return spOut;
        }

        public List<OrderLs> ListOrder(OrderListGetter ls)
        {
            try
            {
                var lsp = new PRODUCT(_memoryCache).BlockCateOnHome();

                var lso = new List<OrderLs>();


                foreach (var item in ls.pieObject.listOrder)
                {
                    var sap = SortSanPham(item, lsp);



                    if (sap != null)
                    {
                        var orl = new OrderLs()
                        {
                            sOrder = item,
                            sSanPham = sap
                        };

                        lso.Add(orl);
                    }
                    else
                    {
                        //Dữ liệu bị lệch
                    }
                }

                return lso;
            }
            catch (Exception)
            {
                return new List<OrderLs>();
            }


            
        }


        /***
         * Lọc lại thông tin, nấy ra những danh mục được phép hiển thị, 
         * các sản phẩm của danh mục đó và số lượng sản phẩm
         * mỗi chuyên mục
         * **/

        public List<BlockCateOnHomePage> BlockCateOnHome()
        {

            try
            {

                var listBlockCate = _memoryCache.GetOrCreate(CACHEKEY.CACHE_HOME_SPECIAL_PRODUCT, entrie =>
                {

                    var lsb = new List<BlockCateOnHomePage>();

                    var danhMuc = GetCategoryOfHomePage();

                    var dmsort = danhMuc.Where(x => x.isShow == true).OrderBy(x => x.orderIndexOnHomePage).ToList();
                    foreach (var item in dmsort)
                    {
                        var parseTwo = new ParseDataTwo();
                        FirestoreDb db = FirestoreDb.Create(VARIBLE.CODER_I);

                        var banh = new DB_DOCUMENT(db).CLIENT().Collection(FIREBASE_DB_COLLECTION.SANPHAM)
                            .WhereArrayContainsAny(FIREBASE_DB_FIELD.CM, new string[] { item.Id }).Limit(item.numberItem.Value | 0);
                        var ls = parseTwo.ListSanPham(banh.GetSnapshotAsync()).Result;


                        var blockCate = new BlockCateOnHomePage() { danhMuc = item, sanPham = ls };
                        lsb.Add(blockCate);
                    }

                    entrie.SetValue(lsb);

                    new CacheExpireNoRuntime(_memoryCache).addCacheItem(new()
                    {
                        name = CACHEKEY.CACHE_HOME_SPECIAL_PRODUCT,
                        time = Convert.ToInt32(new MethodOne().TimeStamp() + TimeSpan.FromHours(SETTING.DEFAULT_CACHE_TIME_HOUR).TotalSeconds)
                    });

                    return lsb;
                });

                return listBlockCate;


            }
            catch (Exception)
            {

                throw;
            }


        }

    }



    public static class METHOD
    {

        public static void ENDPOINT(IEndpointRouteBuilder endpoint)
        {
            endpoint.MapControllerRoute(
                 name: "api",
                 pattern: "{controller}/{action}"
                );
        }

        public static List<string> COLORI()
        {
            var ls = VARIBLE.LIST_COLOR_I;
            var lsi = new List<string>();
            foreach (var item in ls)
            {
                lsi.Add(ENV_VARIBLE.GET_ENV_VARIBLE().URL_STATIC_FILES + item);
            }

            return lsi;
        }

        public static List<string> COLORII()
        {
            var ls = VARIBLE.LIST_COLOR_II;
            var lsi = new List<string>();
            foreach (var item in ls)
            {
                lsi.Add(ENV_VARIBLE.GET_ENV_VARIBLE().URL_STATIC_FILES + item);
            }

            return lsi;
        }

        public static string URI_STATIC(string uri)
        {
            return ENV_VARIBLE.GET_ENV_VARIBLE().URL_STATIC_FILES + uri;
        }

        public static void SESSION_CONFIG(SessionOptions options)
        {
            options.IdleTimeout = TimeSpan.FromHours(10);
            options.Cookie.IsEssential = true;
            options.Cookie.Name = "banbanh";
        }

        public static void ENVIROMENT_WEB(WebApplication app)
        {

            if (!app.Environment.IsDevelopment())
            {

                Environment.SetEnvironmentVariable(VARIBLE.CURRENT_ENV, VARIBLE.ENV_PRODUCT);

                app.UseExceptionHandler("/error/fix");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                Environment.SetEnvironmentVariable(VARIBLE.CURRENT_ENV, VARIBLE.ENV_DEV);
                app.UseExceptionHandler("/error/fix");
            }
        }

    }




    public class MethodOne
    {
        private readonly string subLinkName = "";
        public string KhongDau(string vanBan)
        {
            try
            {
                string stFormD = vanBan.Normalize(NormalizationForm.FormD);
                StringBuilder sBuilder = new StringBuilder();
                for (int ich = 0; ich < stFormD.Length; ich++)
                {
                    System.Globalization.UnicodeCategory uc = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                    if (uc != System.Globalization.UnicodeCategory.NonSpacingMark)
                    {
                        sBuilder.Append(stFormD[ich]);
                    }
                }

                sBuilder = sBuilder.Replace('Đ', 'D');
                sBuilder = sBuilder.Replace('đ', 'd');
                char[] specialCase = { '(', ')', ' ', '+', '&', '`', '~', '!', '@', '#', '$', '%', '^'
                        , '*', '_', '-', '=', '+', '[', ']', ';', ':', '\''
                        , '"', ',', '<', '.', '>', '?', '/', '\\', '|' };
                var sN = sBuilder.ToString();
                foreach (var item in specialCase)
                {
                    sN = sN.Replace(item, '-');
                }
                sN = LooperCheckCase(sN);
                return (sN.Normalize(NormalizationForm.FormD).ToLower());
            }
            catch (Exception)
            {

                throw;
            }


        }

        private string LooperCheckCase(string text)
        {
            string[] noCase = { "-----", "----", "---", "-–-", "--" };
            for (int i = 0; i < noCase.Length; i++)
            {
                var index = text.IndexOf(noCase[i]);

                if (index >= 0)
                {
                    var arr = text.Split(noCase[i]);
                    text = "";
                    for (int ii = 0; ii < arr.Length; ii++)
                    {
                        if (ii < arr.Length - 1)
                        {
                            text += LooperCheckCase(arr[ii]) + "-";
                        }
                        else
                        {
                            text += LooperCheckCase(arr[ii]);
                        }
                    }
                    return text;
                }

            }
            return text;
        }

        public string URLSanPham(string ten, string msp)
        {
            try
            {
                var title = KhongDau(ten);
                return "/" + title + "-ms-" + msp;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public double AvgRateStar(SoSao soSao)
        {
            float totalScore = 5 * soSao.Sao5 + 4 * soSao.Sao4 + 3 * soSao.Sao3 + 2 * soSao.Sao2 + 1 * soSao.Sao1;

            float avg = 0;

            if (totalScore > 0 && soSao.Tong > 0)
            {
                avg = totalScore / soSao.Tong;
            }

            return Math.Round(avg, 1);
        }

        public int TimeStamp()
        {
            var currentSecond = Math.Round(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
            return Convert.ToInt32(currentSecond);
        }


        public DateTime DateTimeFromTimeStamp(int timeStamp)
        {

            var date = new DateTime(1970, 1, 1, 0, 0, 0);
            date = date.AddSeconds(timeStamp);

            var timezone = TimeZoneInfo.ConvertTimeToUtc(date, TimeZoneInfo.Utc);

            return timezone;

        }

        public void LogsError(string error)
        {
            try
            {

                var fileSize = new FileInfo(SETTING.ERROR_LOGS_PATH);

                string errorFormat = $"\n\nError At - {DateTime.Now} \n" +
                    $"-----------------------------------------\n" +
                    $"{error}";

                if (fileSize.Exists && fileSize.Length < SETTING.MAX_ERROR_LOGS_SIZE)
                {


                    File.AppendAllText(Path.GetFullPath(SETTING.ERROR_LOGS_PATH), errorFormat as dynamic);
                }
                else
                {
                    File.Create(Path.GetFullPath(SETTING.ERROR_LOGS_PATH)).Close();
                    File.WriteAllText(Path.GetFullPath(SETTING.ERROR_LOGS_PATH), errorFormat as dynamic);
                }



            }
            catch (Exception)
            {

                throw;
            }

        }


    }
}
