using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.CompilerServices;

namespace BAN_BANH.BAG.MOE
{
    public class MOE_CONTROLLER : Controller
    {
        public IMemoryCache memoryCache { get; set; }
    }
    public class MOE_PAGE_MODEL : PageModel
    {
        public IMemoryCache memoryCache { get; set; }

    }

}
