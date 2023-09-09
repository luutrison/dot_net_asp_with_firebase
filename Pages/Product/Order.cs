using BAN_BANH.BAG.MOE;
using BAN_BANH.Method;
using BAN_BANH.Model.env;
using BAN_BANH.Model;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;

namespace BAN_BANH.Pages.Product
{
    public class Order : IMOE
    {
        public Order(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;

        }




        private IResponse DataSet(ProductOrder productOrder, I_CHECK_MET_PROP props)
        {

            var req = CHECK.CREATE_HTTP_REQUEST();

            var url_order_api = ENV_VARIBLE.GET_ENV_VARIBLE().URL_ORDER_API + URL.URL_POST_NEW_ORDER;

            var itemOrder = new NewOrder()
            {
                sessionCard = new SessionCard()
                {
                    addCardTimestamp = new MethodOne().TimeStamp(),
                    sessionId = props.userSession
                },
                sessionOrder = new SessionOrder()
                {
                    msp = productOrder.msp,
                    sessionId = props.userSession,
                    number = productOrder.numberOrder
                }
            };

            var order = JsonConvert.SerializeObject(itemOrder);

            var content = new StringContent(order, Encoding.UTF8, "application/json");

            var re = req.PostAsync(url_order_api, content).Result;
            var json = re.Content.ReadFromJsonAsync<IResponse>().Result;

            return json;
        }


        [HttpPost("/product/order")]
        public IActionResult OnPost(ProductOrder productOrder)
        {
            this._httpContext = HttpContext;

            return CHECK.OK(this).THEN(props =>
            {
                var res = DataSet(productOrder, props);

                return CHECK.RESPONSE(props, res)
                .OK(props =>
                {
                    return Redirect("/product/order");
                })
                .NOT(props =>
                {
                    return Redirect("/hi/order");
                })
                .RUN();
            });
        }


        [HttpGet("/product/delete")]
        public IActionResult OnDelete(string msp)
        {
            this._httpContext = HttpContext;

            return CHECK.OK(this).THEN(props =>
            {
                var od = new OrderDelete() { msp = msp, sessionId = props.userSession };

                var client = CHECK.CREATE_HTTP_REQUEST();

                var content = new StringContent(JsonConvert.SerializeObject(od), Encoding.Unicode, "application/json");

                var url_order_api = ENV_VARIBLE.GET_ENV_VARIBLE().URL_ORDER_API + URL.URL_DELETE_ORDER;

                var r = client.PostAsync(url_order_api, content).Result;

                return Redirect("/product/order");

            });
        }


        [HttpPost("/product/order-add")]
        public IActionResult OnPut(AddOrder addOrder)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    this._httpContext = HttpContext;

                    return CHECK.OK(this).THEN(props =>
                    {

                        var ords = JsonConvert.DeserializeObject<List<OrderLsAdd>>(addOrder.order);

                        if (ords != null)
                        {
                            addOrder.orderls = ords;
                        }
                        else
                        {
                            return Redirect("/product/order");
                        }

                        var request = CHECK.CREATE_HTTP_REQUEST();

                        var formContent = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>(VARIBLE.NAME_SECRET, VARIBLE.HUMAN_WEB_SEVER_CODER),
                        new KeyValuePair<string, string>(VARIBLE.NAME_RESPONSE, addOrder.human)
                    };

                        var form = new FormUrlEncodedContent(formContent);


                        var res = request.PostAsync(VARIBLE.HUMAN_VALIDATE_URL, form).Result;

                        var cont = res.Content.ReadAsStringAsync().Result;

                        var ver = JsonConvert.DeserializeObject<HumanVerify>(cont);

                        if (ver.success)
                        {
                            //post data to api
                            var url = ENV_VARIBLE.GET_ENV_VARIBLE().URL_ORDER_API + URL.URL_ADD_ORDER;
                            var request_add = request.PostAsync(url, new StringContent(JsonConvert.SerializeObject(addOrder), Encoding.UTF8, "application/json")).Result;

                            if (request_add != null)
                            {
                                var isGood = request_add.Content.ReadFromJsonAsync<IResponse>().Result;
                                if (isGood.check.isGood)
                                {

                                }
                                else
                                {

                                }

                            }
                            else
                            {

                            }
                        }
                        else
                        {

                        }

                        var noti = new NOTIFIER(_memoryCache, HttpContext);
                        noti.SETNOTIFY(new List<Notifier>() { NOTIFY.ORDER_SUCCESS });

                        return Redirect("/product/order");

                    });


                }
                else
                {
                    return Redirect("/");
                }

            }
            catch (Exception)
            {
                /***
                 * Cấu trúc có vấn đề, rõ ràng là cố ý thay đổi, khỏi cần message out
                 * Đưa luôn đến static page
                 * **/
                throw;
            }

        }




    }
}
