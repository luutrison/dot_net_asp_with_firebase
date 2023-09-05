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

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IMemoryCache _memoryCache;

        public IndexModel(ILogger<IndexModel> logger, IMemoryCache memoryCache )
        {
            _logger = logger;
       
            _memoryCache = memoryCache;


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


        public async Task OnGet()
        {
           
            try
            {
                new SESSION_COOKIE(HttpContext, _memoryCache);
                ViewData[VIEWDATA.HOME_BLOCK] = Data();
            }
            catch (Exception err)
            {
                new MethodOne().LogsError(err.ToString());

                throw;
            }
        }
    }
}