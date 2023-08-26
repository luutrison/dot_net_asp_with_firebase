using BAN_BANH.Method;
using BAN_BANH.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BAN_BANH.Pages
{


    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IConfiguration _configuration;

        public IndexModel(ILogger<IndexModel> logger, IConfiguration config)
        {
            _logger = logger;
            _configuration = config;
        }


        public void OnGet()
        {

            var st = _configuration.GetSection("setting").Get<Setting>();
            var list = new List<SanPham>();

            var pdata = new ParseDataOne();

            try
            {
                using (SqlConnection conn = new SqlConnection(st.DatabaseConnectionString))
                {
                    conn.Open();
                    string commandtext = "select top 8 * from dbo.SanPham where hot = 1 order by ngaytao desc";

                    SqlCommand cmd = new SqlCommand(commandtext, conn);

                    var reader = cmd.ExecuteReader();

                    list = pdata.ListSanPham(reader);

                    conn.Close();

                }

                ViewData["List"] = list;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}