using BAN_BANH.Model;

namespace BAN_BANH.Method
{
    public class QueryOne
    {
        //public async Task<string> QuerySameProduct(SameProduct  sameProduct)
        //{
        //    var listTag = sameProduct.sanpham.tag.ToString().Replace(" ", "").Split(",");

        //    string starCmd = "select top 5 * from sanpham where";
        //    string cmdCenter = "";

        //    string endCmd = "order by timestamp asc";

            

        //    for (int i = 0; i < listTag.Length; i++)
        //    {
        //        if (i > 0)
        //        {
        //            cmdCenter += $" or tag like '%{listTag[i]}%'";
        //        }
        //        else
        //        {
        //            cmdCenter += $" tag like '%{listTag[i]}%'";
        //        }
        //    }

        //    string cmd = $"{starCmd} {cmdCenter} {endCmd}";


        //    return cmd;
        //}

        public string TimeOutOrder(List<UserCard> list)
        {
            try
            {
                var dbName = "BANBANH_ORDER";

                string outTimeCmd = $"use {dbName}";

                foreach (var item in list)
                {
                    outTimeCmd += $" delete  from dbo.UserCard where id like '{item.id}' delete  from dbo.OrderCard where userId like '{item.id}'";

                }

                return outTimeCmd;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }


   
}
