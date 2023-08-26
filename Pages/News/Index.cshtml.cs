using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAN_BANH.Pages.News
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            var title = RouteData.Values["title"];
            var id = RouteData.Values["id"];
        }

        public IActionResult OnGetPartial() =>
    new PartialViewResult
    {
        ViewName = "_component",
        ViewData = ViewData,
    };
    }
}
