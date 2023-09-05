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
                var SS = new SESSION_COOKIE(HttpContext, _memoryCache);
                var SSS = SS.GET_SESSION();

                var ohyeah = new HttpClient();

                var url = ENV_VARIBLE.GET_ENV_VARIBLE().URL_ORDER_API + URL.URL_GET_ORDER;

                ohyeah.DefaultRequestHeaders.Add(REQUEST.GET_ORDER_HEADER_KEY_NAME, SSS);
                var listItem = ohyeah.GetAsync(url).Result;

                var ls = listItem.Content.ReadFromJsonAsync<OrderListGetter>().Result;

                ViewData[VIEWDATA.ORDER_LIST] = ls;
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
                var SESS = new SESSION_COOKIE(HttpContext, _memoryCache);
                SESS.CHECK_SESSION();

                var req = new HttpClient();
                var context = HttpContext;

                var sessionId =  SESS.GET_SESSION();

                var url_order_api = ENV_VARIBLE.GET_ENV_VARIBLE().URL_ORDER_API + URL.URL_POST_NEW_ORDER;

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

                var order = JsonConvert.SerializeObject(itemOrder);

                var content = new StringContent(order, Encoding.UTF8, "application/json");

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
