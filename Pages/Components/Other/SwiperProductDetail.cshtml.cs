using BAN_BANH.Method;
using BAN_BANH.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace BAN_BANH.Pages.Components.Other
{
    public class SwiperProductDetailViewComponent : ViewComponent
    {
        private readonly IConfiguration _configuration;
        public SwiperProductDetailViewComponent(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public IViewComponentResult Invoke(string pid)
        {

            var st = _configuration.GetSection("setting").Get<Setting>();
            string query = $"select * from dbo.AnhSanPham where pid like '{pid}'";
            var pd = new ParseDataOne();
            var listImage = new List<ImageGalleryProduct>();

            using (var conn = new SqlConnection(st.dbBanBanh))
            {
                conn.Open();
                var command = new SqlCommand(query, conn);

                var reader = command.ExecuteReader();

                listImage = pd.ParseImageProduct(reader);

            }


            return View("/Pages/Components/Other/SwiperProductDetail.cshtml", listImage);
        }
    }
}
