using BAN_BANH.BAG.MOE;
using BAN_BANH.Method;
using BAN_BANH.Model;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using System.Data.SqlClient;
using System.Reflection;

namespace BAN_BANH.Pages
{


    public class IndexController : IMOE
    {


        public IndexController( IMemoryCache memoryCache )
        {
            this._memoryCache = memoryCache;
        }

        private List<BlockCateOnHomePage> Data()
        {

            try
            {
                USE_ENVIROMENT.ENVIROMENT_CODER_I();

                var ls = new PRODUCT(_memoryCache).BlockCateOnHome();

                return ls;
            }
            catch (Exception err)
            {
                new MethodOne().LogsError(err.ToString());
                throw;
            }

        }

        [HttpGet("/")]
        public IActionResult OnGet()
        {
           
            try
            {
                this._httpContext = HttpContext;
                return CHECK.OK(this).THEN(props =>
                {
                    ViewData[VIEWDATA.HOME_BLOCK] = Data();

                    return View("/Pages/Index.cshtml");
                });
            }
            catch (Exception err)
            {
                new MethodOne().LogsError(err.ToString());

                throw;
            }
        }
    }
}