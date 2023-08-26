using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAN_BANH.Pages.Shared.Components
{
    public class FooterViewComponent : ViewComponent
    {
        public void OnGet()
        {
        }

        public IViewComponentResult Invoke() { 
            return View("/Pages/Shared/Components/Footer.cshtml");
        }
    }
}
