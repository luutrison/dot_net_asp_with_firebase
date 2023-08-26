using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAN_BANH.Pages.Shared.Components
{
    public class HeaderViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke(string mode)
        {
            return View("/Pages/Shared/Components/Header.cshtml", mode);
        }
    }
}
