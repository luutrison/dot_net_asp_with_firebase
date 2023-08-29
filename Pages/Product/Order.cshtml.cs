using BAN_BANH.Method;
using BAN_BANH.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace BAN_BANH.Pages.Product
{
    public class OrderModel : PageModel
    {

        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;
        public OrderModel(IMemoryCache memoryCache, IConfiguration configuration) {
            _memoryCache = memoryCache;
            _configuration = configuration;
        }



        public void OnGet() {
            var st = _configuration.GetSection("setting").Get<Setting>();
            var mtd = new MethodOne();
            mtd.UpdateTimeOutCard(_memoryCache, st.dbBanBanhOrder);


        }


    }

    public class OrderController: Controller
    {
        [HttpPost]
        [Route("/product/order")]
        public IActionResult Order(ProductOrder productOrder)
        {
            var number = productOrder.numberOrder;

            var mt = new MethodOne();

            var user = mt.CheckUserSession(HttpContext);





            return Redirect("/product/order");
        }
    }
}
