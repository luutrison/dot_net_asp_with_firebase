using BAN_BANH.BAG.MOE;
using BAN_BANH.Method;
using BAN_BANH.Model;
using BAN_BANH.Model.env;
using Google.Api;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;

namespace BAN_BANH.Pages.Product
{



    public class OrderModel : MOE_PAGE_MODEL
    {
        public OrderModel(IMemoryCache _memoryCache)
        {
            this.memoryCache = _memoryCache;
        }

        public SanPham ToListItem(List<SanPham> sanPham, out SanPham spx, string msp)
        {

            spx = sanPham.Where(x => x.msp == msp).FirstOrDefault();

            return spx;
        }



        public void OnGet()
        {
            try
            {
                CHECK.OKPAGE(this).THEN(props =>
                {
                    var request = CHECK.CREATE_HTTP_REQUEST();

                    request.DefaultRequestHeaders.Add(REQUEST.GET_ORDER_HEADER_KEY_NAME, props.userSession);

                    var url = ENV_VARIBLE.GET_ENV_VARIBLE().URL_ORDER_API + URL.URL_GET_ORDER;

                    var lsi = request.GetAsync(url).Result;



                    var res = lsi.Content.ReadFromJsonAsync<IResponse>().Result;
                    var value = res.response;

                    var response = JsonConvert.DeserializeObject<OrderListGetter>(res.response);


                    var lod = new PRODUCT(memoryCache).ListOrder(response);
                    ViewData[VIEWDATA.ORDER_LIST] = lod;

                    return true;
                });


            }
            catch (Exception)
            {

                throw;
            }
        }


    }


    public class OrderController : MOE_CONTROLLER
    {


        public OrderController(IMemoryCache _memoryCache)
        {
            this.memoryCache = _memoryCache;
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
        public IActionResult Order(ProductOrder productOrder)
        {

            return CHECK.OKCON(this).THEN(props =>
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
        [HttpGet("/product/deleteOrder")]
        public IActionResult DeleteOrder(string msp)
        {

            return CHECK.OKCON(this).THEN(props =>
            {
                var od = new OrderDelete() { msp = msp, sessionId = props.userSession };

                var client = CHECK.CREATE_HTTP_REQUEST();

                var content = new StringContent(JsonConvert.SerializeObject(od), Encoding.Unicode, "application/json");

                var url_order_api = ENV_VARIBLE.GET_ENV_VARIBLE().URL_ORDER_API + URL.URL_DELETE_ORDER;

                var r = client.PostAsync(url_order_api, content).Result;



                return Redirect("/product/order");

            });

        }

        [HttpPost("/product/addOrder")]
        public IActionResult AddOrder(AddOrder addOrder)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var SESS = new SESSION_COOKIE(HttpContext, memoryCache);
                    SESS.CHECK();
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

                    return Redirect("/");
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


