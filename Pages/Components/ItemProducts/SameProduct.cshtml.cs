using BAN_BANH.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using BAN_BANH.Method;
using Google.Cloud.Firestore;
using Microsoft.Extensions.Caching.Memory;

namespace BAN_BANH.Pages.Components.ItemProducts
{
    public class SameProductViewComponent : ViewComponent
    {

        private readonly IMemoryCache _memoryCache;

        public SameProductViewComponent(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public IViewComponentResult  Invoke(SameProduct sameProduct) {


            try
            {

                var list = _memoryCache.GetOrCreate(CACHEKEY.SAME_PRODUCT+sameProduct.msp, entrie =>
                {
                    USE_ENVIROMENT.ENVIROMENT_CODER_I();
                    var db = FirestoreDb.Create(VARIBLE.CODER_I);
                    var collection = new DB_DOCUMENT(db).CLIENT().Collection(FIREBASE_DB_COLLECTION.SANPHAM).WhereArrayContainsAny(FIREBASE_DB_FIELD.CM, sameProduct.cm);


               

                    var list = new ParseDataTwo().ListSanPham(collection.OrderBy(FIREBASE_DB_FIELD.SANPHAM__NGAY_NHAP_LIEU).Limit(5).GetSnapshotAsync()).Result;

                    entrie.SetValue(list);
                    entrie.SetSlidingExpiration(TimeSpan.FromHours(SETTING.DEFAULT_CACHE_TIME_HOUR));

                    return list;
                });
            

                return View("/Pages/Components/ItemProducts/SameProduct.cshtml", list);


            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
