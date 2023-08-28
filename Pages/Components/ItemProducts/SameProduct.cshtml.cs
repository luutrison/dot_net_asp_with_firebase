using BAN_BANH.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using BAN_BANH.Method;

namespace BAN_BANH.Pages.Components.ItemProducts
{
    public class SameProductViewComponent : ViewComponent
    {

        public IViewComponentResult  Invoke(SameProduct sameProduct) {

            var config = sameProduct.config.GetSection("setting").Get<Setting>();
            var cns = config.DatabaseConnectionString;

            var lsSanPham = new List<SanPham>();

            using (SqlConnection conn = new SqlConnection(cns))
            {
                conn.Open();

                List<Task> listTask = new List<Task>();

                var qro = new QueryOne();
                var prs = new ParseDataOne();

                var query = qro.QuerySameProduct(sameProduct).Result;


                SqlCommand cmd = new SqlCommand(query, conn);

                var reader = cmd.ExecuteReader();

                lsSanPham = prs.ListSanPham(reader);

            }


            return View("/Pages/Components/ItemProducts/SameProduct.cshtml", lsSanPham);
        }
    }
}
