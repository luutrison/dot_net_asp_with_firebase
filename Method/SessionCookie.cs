using BAN_BANH.Model;
using Microsoft.Extensions.Caching.Memory;

namespace BAN_BANH.Method
{
    public class SESSION_COOKIE
    {


        private readonly HttpContext _httpContext;
        private readonly IMemoryCache _memoryCache;
        private readonly CacheExpireNoRuntime _expireNoRuntime;
        public SESSION_COOKIE(HttpContext context, IMemoryCache memoryCache)
        {
            _httpContext = context;
            _memoryCache = memoryCache;
            _expireNoRuntime = new CacheExpireNoRuntime(_memoryCache);

            _expireNoRuntime.updateExpire();
            LIMIT_CONNECTION_TIME();
            CHECK_SESSION();
        }

        private string newSession()
        {
            var nowDate = DateTime.UtcNow;

            var method = new MethodOne();
            var timeStampNow = method.TimeStamp();

            var timeStampOut = timeStampNow + TimeSpan.FromHours(SETTING.DEFAULT_CACHE_TIME_HOUR).TotalSeconds;
            var newId = VARIBLE.COOKIE_SESSION_NAME_SPLIT + timeStampNow + "." + timeStampOut;


            _memoryCache.GetOrCreate(newId, enchie =>
            {
                enchie.SetValue(newId);
                enchie.SetAbsoluteExpiration(DateTimeOffset.UtcNow.AddHours(SETTING.DEFAULT_CACHE_TIME_HOUR));
                setCookie(newId);


                return newId;
            });

            return newId;

        }

        private void haveSession(string session)
        {
            var cacheItem = _memoryCache.Get(session);
            if (cacheItem == null)
            {
                newSession();
            }
        }

        private void setCookie(string cookie)
        {
            _httpContext.Response.Cookies
                .Append(VARIBLE.COOKIE_SESSION_NAME, cookie,
                new CookieOptions
                {
                    Expires = DateTime.UtcNow.AddHours(SETTING.DEFAULT_CARD_TIME_HOUR)
                });
        }

        public void CHECK_SESSION()
        {
            try
            {
                var currentUser = _httpContext.Request.Cookies[VARIBLE.COOKIE_SESSION_NAME];
                if (currentUser == null)
                {
                    newSession();
                }
                else
                {
                    haveSession(currentUser);
                }
            }
            catch (Exception err)
            {
                new MethodOne().LogsError(err as dynamic);
                throw;
            }

        }
        private void AUTO_CALCULATION_HARDWARE()
        {

        }

        private int getTime()
        {
            var timeStamp = new MethodOne().TimeStamp();
            var outTime = timeStamp + Convert.ToInt32(TimeSpan.FromMinutes(SETTING.TIME_RESET_COUNT_LOAD).TotalSeconds);
            return outTime;
        }

        private void LIMIT_CONNECTION_TIME()
        {
            try
            {
                if (SETTING.ALLOW_LIMIT_VIEW)
                {
                    var ip = _httpContext.Request.HttpContext.Connection.RemoteIpAddress;

                    var nameIp = VARIBLE.VIEW_LIMIT_IP_NAME + ip;
                    var id = _memoryCache.GetOrCreate(nameIp, entrie =>
                    {
                        var uuid = Guid.NewGuid();
                        entrie.SetValue(uuid);
                        _expireNoRuntime.addCacheItem(new ICacheItemExpire()
                        {
                            name = nameIp,
                            time = getTime()
                        });
                        return uuid;
                    });

                    var nameCount = VARIBLE.COUNT_VIEW_LIMIT_IP_NAME + ip;
                    var count = _memoryCache.GetOrCreate(nameCount, entrie =>
                    {
                        entrie.SetValue(0);
                        _expireNoRuntime.addCacheItem(new ICacheItemExpire()
                        {
                            name = nameCount,
                            time = getTime()
                        }); ;

                        return 0;
                    });
                    if (count > SETTING.MAX_LOAD_IPS)
                    {
                        _httpContext.Response.Redirect(VARIBLE.PATH_RAY_WAIT_PAGE + id.ToString());
                    }
                    else
                    {
                        _memoryCache.Set(VARIBLE.COUNT_VIEW_LIMIT_IP_NAME + ip, ++count);
                    }


                }
            }
            catch (Exception)
            {

                throw;
            }


        }
    }
}
