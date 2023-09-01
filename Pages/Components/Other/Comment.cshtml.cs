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
            return View("/Pages/Components/Other/Comment.cshtml", comment);
        }
    }
}
