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

        private async Task<List<SanPham>> Data() {

            try
            {
        

                USE_ENVIROMENT.ENVIROMENT_CODER_I();
                

                var parseTwo = new ParseDataTwo();
                FirestoreDb db = FirestoreDb.Create(VARIBLE.CODER_I);

                var banh = db.Collection(FIREBASE_DB_COLLECTION.BANBANH).Document(FIREBASE_DB_DOCUMENT.CLIENT).Collection(FIREBASE_DB_COLLECTION.SANPHAM);
                var list = await parseTwo.ListSanPham(banh.GetSnapshotAsync());


                return list;
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
                var context = HttpContext;
                var list = await Data();
                ViewData["List"] = list;
            }
            catch (Exception err)
            {
                new MethodOne().LogsError(err.ToString());
                throw;
            }
        }
    }
}