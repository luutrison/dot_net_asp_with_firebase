using BAN_BANH.Model;
using System;
using System.Text;
using System.Text.RegularExpressions;

namespace BAN_BANH.Method
{
    public class MethodOne
    {
        private readonly string subLinkName = "product";
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

        public string URLSanPham(string Ten, int Id)
        {
            try
            {
                var title = KhongDau(Ten);
                return "/" + subLinkName + "/" + title + "-p-" + Id;
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



      
    }
}
