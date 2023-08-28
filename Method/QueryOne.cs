using BAN_BANH.Model;

namespace BAN_BANH.Method
{
    public class QueryOne
    {
        public async Task<string> QuerySameProduct(SameProduct  sameProduct)
        {
            var listTag = sameProduct.sanpham.tag.ToString().Replace(" ", "").Split(",");

            string starCmd = "select top 5 * from sanpham where";
            string cmdCenter = "";

            string endCmd = "order by ngaytao asc";

            

            for (int i = 0; i < listTag.Length; i++)
            {
                if (i > 0)
                {
                    cmdCenter += $" or tag like '%{listTag[i]}%'";
                }
                else
                {
                    cmdCenter += $" tag like '%{listTag[i]}%'";
                }
            }

            string cmd = $"{starCmd} {cmdCenter} {endCmd}";


            return cmd;
        }
    }
}
