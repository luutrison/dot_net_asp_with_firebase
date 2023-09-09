using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using System.Runtime.CompilerServices;

namespace BAN_BANH.BAG.MOE
{

    public class IMOE : Controller
    {
        public IMemoryCache _memoryCache { get; set; }
        public string _sessionUser { get; set; }
        public bool _isPrepare { get; set; }

        public HttpContext _httpContext { get; set; }
    }

}
