using BAN_BANH.Method;
using BAN_BANH.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BAN_BANH.Pages.Product
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnGet()
        {

            var st = _configuration.GetSection("setting").Get<Setting>();
            var pid = RouteData.Values["pid"];

            ViewData["config"] = _configuration;

            try
            {
                using (SqlConnection connect = new SqlConnection(st.dbBanBanh))
                {
                    connect.Open();

                    string query = $"select * from dbo.sanpham where pid like '{pid}' ";
                    string queryComment = $"select top 8 * from dbo.sosao where pid like '{pid}' order by Time desc";
                    string querySoSao = $"select Count(case when pid like '{pid}' then 1 end) as 'Tong', " +
                        $"\r\nCount(case when Rate = 5 and pid like '{pid}' then 1 end) as 'Sao5', " +
                        $"\r\nCount(case when Rate = 4 and pid like '{pid}' then 1 end) as 'Sao4'," +
                        $"\r\nCount(case when Rate = 3 and pid like '{pid}' then 1 end) as 'Sao3'," +
                        $"\r\nCount(case when Rate = 2 and pid like '{pid}' then 1 end) as 'Sao2'," +
                        $"\r\nCount(case when Rate = 1 and pid like '{pid}' then 1 end) as 'Sao1'" +
                        $"\r\nFrom SoSao";
                    var pdata = new ParseDataOne();
                    var mthod = new MethodOne();

                    SqlCommand cmd = new SqlCommand(query, connect);
                    SqlCommand cmdBinhLuan = new SqlCommand(queryComment, connect);
                    SqlCommand cmdSoSao = new SqlCommand(querySoSao, connect);

                    var readerSanPham = cmd.ExecuteReader();
                    var listSanPham = pdata.ListSanPham(readerSanPham);
                    readerSanPham.Close();

                    var readerBinhLuan = cmdBinhLuan.ExecuteReader();
                    var listComment = pdata.ListComment(readerBinhLuan);
                    readerBinhLuan.Close();

                    var readerSoSao = cmdSoSao.ExecuteReader();
                    var soSao = pdata.DemSao(readerSoSao);
                    readerSoSao.Close();

                    var avgStar = mthod.AvgRateStar(soSao);
                    var calStar = new CalStar()
                    {
                        AvgStar = avgStar,
                        Total = soSao.Tong
                    };

                    ViewData["sanpham"] = listSanPham[0];
                    ViewData["comment"] = listComment;
                    ViewData["star"] = calStar;

                }
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
