using BAN_BANH.Method;
using BAN_BANH.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace BAN_BANH.Pages.Components.Other
{
    public class BlockHomeItemViewComponent : ViewComponent
    {

   

        public IViewComponentResult Invoke(List<BlockCateOnHomePage> ls)
        {

            return View("/Pages/Components/Other/BlockHomeItem.cshtml", ls);
        }
    }
}
