using BAN_BANH.Method;
using BAN_BANH.Model;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace BAN_BANH.Pages.Product
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;

        public IndexModel(IConfiguration configuration, IMemoryCache memoryCache)
        {
            _configuration = configuration;
            _memoryCache = memoryCache;
        }


        private Task<AggregateQuerySnapshot> SaoNumber(CollectionReference soSao, int num, string msp)
        {
            return soSao.WhereEqualTo(FIREBASE_DB_FIELD.SAO, num).WhereEqualTo(FIREBASE_DB_FIELD.MSP, msp).Count().GetSnapshotAsync();
        }


        private async Task<SoSao> SoSaoTask(CollectionReference soSao, Task<AggregateQuerySnapshot> total, string msp)
        {

            try
            {
                var lsTask = new List<Task<AggregateQuerySnapshot>>();

                lsTask.Add(total);
                lsTask.Add(SaoNumber(soSao, 1, msp));
                lsTask.Add(SaoNumber(soSao, 2, msp));
                lsTask.Add(SaoNumber(soSao, 3, msp));
                lsTask.Add(SaoNumber(soSao, 4, msp));
                lsTask.Add(SaoNumber(soSao, 5, msp));

                Task.WaitAll(lsTask.ToArray());


                return new SoSao()
                {
                    Tong = lsTask[0].Result.Count as dynamic,
                    Sao1 = lsTask[1].Result.Count as dynamic,
                    Sao2 = lsTask[2].Result.Count as dynamic,
                    Sao3 = lsTask[3].Result.Count as dynamic,
                    Sao4 = lsTask[4].Result.Count as dynamic,
                    Sao5 = lsTask[5].Result.Count as dynamic,
                };
            }
            catch (Exception err)
            {
                new MethodOne().LogsError(err.ToString());
                throw;
            }
      
        }


        public async Task<IActionResult> OnGet()
        {


            try
            {

                new SESSION_COOKIE(HttpContext, _memoryCache);

                var nmsp = FIREBASE_DB_FIELD.MSP;
                USE_ENVIROMENT.ENVIROMENT_CODER_I();
                string msp = RouteData.Values[nmsp].ToString();
                var db = FirestoreDb.Create(VARIBLE.CODER_I);
                var soSao = new DB_DOCUMENT(db).CLIENT().Collection(FIREBASE_DB_COLLECTION.SOSAO);


                var sanpham = _memoryCache.GetOrCreate(VIEWDATA.SAN_PHAM + msp, entry =>
                {
                    var document = new DB_DOCUMENT(db).CLIENT().Collection(FIREBASE_DB_COLLECTION.SANPHAM).WhereEqualTo(nmsp, msp).GetSnapshotAsync();

                    var sanPham = new ParseDataTwo().ListSanPham(document);

                    var item = sanPham.Result.FirstOrDefault();

                    entry.SetValue(item);
                    entry.SetSlidingExpiration(TimeSpan.FromHours(SETTING.DEFAULT_CACHE_TIME_HOUR));
                    return item;
                });



                var comment = _memoryCache.GetOrCreate(VIEWDATA.COMMENT + msp, entry =>
                {
                    var comment = new ParseDataTwo().ListComment(soSao.WhereEqualTo(nmsp, msp).Limit(8).GetSnapshotAsync());
                    entry.SetValue(comment.Result);
                    entry.SetSlidingExpiration(TimeSpan.FromHours(SETTING.DEFAULT_CACHE_TIME_HOUR));

                    return comment.Result;
                });


                var star = _memoryCache.GetOrCreate(VIEWDATA.STAR + msp, entry =>
                {
                    var sao = SoSaoTask(soSao, soSao.WhereEqualTo(nmsp, msp).Count().GetSnapshotAsync(), msp);

                    var star = new CalStar()
                    {
                        AvgStar = new MethodOne().AvgRateStar(sao.Result),
                        Total = sao.Result.Tong
                    };
                    entry.SetValue(star);
                    entry.SetSlidingExpiration(TimeSpan.FromHours(SETTING.DEFAULT_CACHE_TIME_HOUR));
                    return star;
                });

                if (sanpham == null)
                {
                   return new RedirectResult("/error/404");
                }
                else
                {
                    ViewData[VIEWDATA.SAN_PHAM] = sanpham;
                    ViewData[VIEWDATA.COMMENT] = comment;
                    ViewData[VIEWDATA.STAR] = star;
                    return new PageResult();
                }

                
            }
            catch (Exception err)
            {
                new MethodOne().LogsError(err.ToString());
                throw;
            }

        }
    }
}
