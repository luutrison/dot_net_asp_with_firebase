using BAN_BANH.Model;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;

namespace BAN_BANH.Method
{
    public class MethodOne
    {
        private readonly string subLinkName = "P";
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
                return (sN.Normalize(NormalizationForm.FormD));
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

        public string URLSanPham(string Ten, string pid)
        {
            try
            {
                var title = KhongDau(Ten);
                return "/" + subLinkName + "/" + pid + "/" + title ;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public double AvgRateStar(SoSao soSao)
        {
            float totalScore = 5 * soSao.Sao5 + 4 * soSao.Sao4 + 3 * soSao.Sao3 + 2 * soSao.Sao2 + 1 * soSao.Sao1;

            float avg = totalScore / soSao.Tong;

            return Math.Round(avg, 1);
        }

        public double TimeStamp()
        {
            return Math.Round(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        }

        public string CheckUserSession(HttpContext context)
        {
            var user = context.Session.Get("user").ToString();
            if (user == null)
            {
                var uuid = Guid.NewGuid();
                var uid = uuid + "-" + TimeStamp();
                var sessId = "user-" + uid.ToString();
                user = sessId.ToString();
                context.Session.SetString("user", sessId);
            }

            return user;    
        }

        public void UpdateTimeOutCard(IMemoryCache memoryCache, string connectionString) {

            try
            {
                var isUpdate = memoryCache.Get("banhbanh-updateCard");
                if (isUpdate == null)
                {

                    int after5h = 5 * 60 * 60;

                    var pd = new ParseDataOne();
                    var qr = new QueryOne();

                    var timeCheck = TimeStamp() - after5h;

                    using (var conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        var queryUserTimeOut = $"use BANBANH_ORDER select * from dbo.UserCard where timestamp <= {timeCheck}";
                        var sqlCommand = new SqlCommand(queryUserTimeOut, conn);
                        var reader = sqlCommand.ExecuteReader();

                        var listUser = pd.ParseUserCard(reader);
                        var cmd = qr.TimeOutOrder(listUser);
                        reader.Close();

                        var deleteCmd = new SqlCommand(cmd, conn);
                        var rdl = deleteCmd.ExecuteReader();
                        rdl.Close();
                    }

                   

                    MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(5),
                        
                    };
                    memoryCache.Set("banhbanh-updateCard", "ok", cacheOptions);
                }
            }
            catch (Exception)
            {

                throw;
            }


            
        }



      
    }
}
