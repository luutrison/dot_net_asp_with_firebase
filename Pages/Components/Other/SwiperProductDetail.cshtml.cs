using BAN_BANH.Method;
using BAN_BANH.Model;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using System.Data.SqlClient;

namespace BAN_BANH.Pages.Components.Other
{
    public class SwiperProductDetailViewComponent : ViewComponent
    {
        private readonly IMemoryCache _memoryCache;
        public SwiperProductDetailViewComponent(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }


        public IViewComponentResult Invoke(string msp)
        {

            try
            {

                var listImage = _memoryCache.GetOrCreate(CACHEKEY.GALLERY_IMAGE + msp, enchy =>
                {
                    USE_ENVIROMENT.ENVIROMENT_CODER_I();
                    var db = FirestoreDb.Create(VARIBLE.CODER_I);
                    var collection = new DB_DOCUMENT(db).CLIENT().Collection(FIREBASE_DB_COLLECTION.ANHSANPHAM).WhereEqualTo(FIREBASE_DB_FIELD.MSP, msp).GetSnapshotAsync();
                    var listImage = new ParseDataTwo().ListImageGallery(collection).Result;
                    return listImage;
                });

                return View("/Pages/Components/Other/SwiperProductDetail.cshtml", listImage);

            }
            catch (Exception)
            {

                throw;
            }

          


        }
    }
}
