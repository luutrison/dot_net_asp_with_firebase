using BAN_BANH.Method;
using BAN_BANH.Model;
using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAN_BANH.Pages.Components.ItemProducts
{
    public class ItemProductViewComponent : ViewComponent
    {

        private async Task<SanPhamLink> Data(SanPham sanPham) {

            USE_ENVIROMENT.ENVIROMENT_CODER_I();
            var db = FirestoreDb.Create(VARIBLE.CODER_I);


            var collection = new DB_DOCUMENT(db).DASH().Collection(FIREBASE_DB_COLLECTION.EVENT).Document(sanPham.sukien).GetSnapshotAsync();

            var ptwo = new Method.ParseDataTwo();

            var item = await ptwo.ListSanPhamLink(collection, sanPham.ngayNhapLieu);

            return item;

        }


        public  IViewComponentResult Invoke(SanPham sanpham)
        {
            ViewData["SanPhamLink"] =  Data(sanpham).Result;

            var view = View("/Pages/Components/ItemProducts/ItemProduct.cshtml", sanpham);

            return view;
        }


    }
}
