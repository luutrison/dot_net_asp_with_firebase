using BAN_BANH.Model;
using BAN_BANH.Pages.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAN_BANH.Pages.Components.ItemProducts
{
    public class ItemOrderViewComponent : ViewComponent
    {
        public void OnGet()
        {
        }

        public IViewComponentResult Invoke(OrderListGetter orderList)
        {


            return View("/Pages/Components/ItemProducts/ItemOrder.cshtml", orderList);
        }
    }
}
