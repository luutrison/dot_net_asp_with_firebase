using BAN_BANH.Model;
using Google.Cloud.Firestore;
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

    public class MiddleWare {
        
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
