using BAN_BANH.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAN_BANH.Pages.Components.ItemProducts
{
    public class ItemProductViewComponent : ViewComponent
    {
        
        public IViewComponentResult Invoke(SanPham sanpham)
        {
            return View("/Pages/Components/ItemProducts/ItemProduct.cshtml", sanpham);
        }


    }
}
