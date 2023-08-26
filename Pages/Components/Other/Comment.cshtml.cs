using BAN_BANH.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BAN_BANH.Pages.Components.Other
{
    public class CommentViewComponent : ViewComponent
    {
        public void OnGet()
        {
        }

        public IViewComponentResult Invoke(Comment comment)
        {

            var date = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            date = date.AddSeconds(comment.Time).ToLocalTime();

            ViewData["date"] = date; 

            return View("/Pages/Components/Other/Comment.cshtml", comment);
        }
    }
}
