using BAN_BANH.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAN_BANH.Pages.Components.Other
{
    public class StarCountViewComponent : ViewComponent
    {
        public void OnGet()
        {
        }

        public IViewComponentResult Invoke(StarCountImage sci)
        {
            return View("/Pages/Components/Other/StarCount.cshtml", sci);
        }
    }
}
