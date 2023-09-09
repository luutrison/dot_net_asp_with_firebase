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

    public class OrderModel : IMOE
    {
        public OrderModel(IMemoryCache _memoryCache)
        {
            this._memoryCache = _memoryCache;
           
        }

        public SanPham ToListItem(List<SanPham> sanPham, out SanPham spx, string msp)
        {

            spx = sanPham.Where(x => x.msp == msp).FirstOrDefault();

            return spx;
        }


        [HttpGet("/product/order")]
        public IActionResult OnGet()
        {
            try
            {
                this._httpContext = HttpContext;

                return CHECK.OK(this).THEN(props =>
                {
                    var request = CHECK.CREATE_HTTP_REQUEST();

                    request.DefaultRequestHeaders.Add(REQUEST.GET_ORDER_HEADER_KEY_NAME, props.userSession);

                    var url = ENV_VARIBLE.GET_ENV_VARIBLE().URL_ORDER_API + URL.URL_GET_ORDER;

                    var lsi = request.GetAsync(url).Result;

                    var res = lsi.Content.ReadFromJsonAsync<IResponse>().Result;
                    var value = res.response;

                    var response = JsonConvert.DeserializeObject<OrderListGetter>(res.response);

                    var lod = new PRODUCT(_memoryCache).ListOrder(response);
                    ViewData[VIEWDATA.ORDER_LIST] = lod;

                    return View("/Pages/Product/Order.cshtml");
                });


            }
            catch (Exception)
            {

                throw;
            }
        }

    }

}


