using BAN_BANH.Method;
using BAN_BANH.Model;
using BAN_BANH.Model.env;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Text;

namespace BAN_BANH.Pages.Product
{
    public class OrderModel : PageModel
    {

        private readonly IMemoryCache _memoryCache;
        public OrderModel(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }



        public void OnGet()
        {
            try
            {
                new SESSION_COOKIE(HttpContext, _memoryCache);

            }
            catch (Exception)
            {

                throw;
            }
        }


    }


    public class OrderController : Controller
    {

        private readonly IMemoryCache _memoryCache;
        public OrderController(IMemoryCache memoryCache)
        {

            _memoryCache = memoryCache;
        }

        [HttpPost]
        [Route("/product/order")]
        public IActionResult Order(ProductOrder productOrder)
        {

            try
            {

                var req = new HttpClient();
                var context = HttpContext;
                var sessionId = HttpContext.Session.Id;

                var url_order_api = ENV_VARIBLE.GET_ENV_VARIBLE().URL_ORDER_API;

                var itemOrder = new NewOrder()
                {
                    sessionCard = new SessionCard()
                    {
                        addCardTimestamp = new MethodOne().TimeStamp(),
                        sessionId = sessionId
                    },
                    sessionOrder = new SessionOrder()
                    {
                        msp = productOrder.msp,
                        sessionId = sessionId,
                        number = productOrder.numberOrder
                    }
                };

                var examp = "Exu�e����mxlm" as dynamic;

                //JsonConvert.SerializeObject(itemOrder)

                var content = new StringContent(examp, Encoding.UTF8, "application/json");

                var reqe = req.PostAsync(url_order_api, content).Result;

                return Redirect("/product/order");
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
