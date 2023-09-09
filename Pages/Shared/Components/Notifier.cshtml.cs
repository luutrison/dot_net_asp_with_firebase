using BAN_BANH.Method;
using BAN_BANH.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace BAN_BANH.Pages.Shared.Components
{
    public class NotifierViewComponent : ViewComponent
    {

        private readonly IMemoryCache _memoryCache;

        public NotifierViewComponent(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
     
        public IViewComponentResult Invoke()
        {
            try
            {

                return View("/Pages/Shared/Components/Notifier.cshtml", _memoryCache);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
