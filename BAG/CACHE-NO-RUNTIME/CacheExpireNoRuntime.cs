using BAN_BANH.Model;
using Microsoft.Extensions.Caching.Memory;

namespace BAN_BANH.Method
{

    public class ICacheItemExpire
    {
        public string name { get; set; }
        public int time { get; set; }
    }

    public class IListKeyCache
    {
        public int time { get; set; }
        public List<ICacheItemExpire> list { get; set; }
    }
    public  class CacheExpireNoRuntime
    {
        private  readonly IMemoryCache _memoryCache;
        public  CacheExpireNoRuntime(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void addCacheItem(ICacheItemExpire cacheItemExpire)
        {
            try
            {
                var checke = _memoryCache.GetOrCreate(CACHEKEY.CAKE_LIST_CHECK, entrie => {
                    var item = new IListKeyCache() {
                        time = new MethodOne().TimeStamp(),
                        list = new List<ICacheItemExpire>() {  cacheItemExpire }
                    };

                    entrie.SetValue(item);
                    return item;
                });

                checke.list.Add(cacheItemExpire);

                _memoryCache.Set(CACHEKEY.CAKE_LIST_CHECK, checke);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void updateExpire()
        {
            try
            {

                var med = new MethodOne();
                var isTime = _memoryCache.GetOrCreate(CACHEKEY.CAKE_TIME, entrie => {

                    var time = med.TimeStamp();
                    entrie.SetValue(time);
                    return time;
                });

                var curTime = med.TimeStamp();

                if (curTime - isTime > TimeSpan.FromMinutes(SETTING.MINIMUM_MINITUTE_CHECK_EXPRITE).TotalSeconds)
                {
                    var checke = _memoryCache.GetOrCreate(CACHEKEY.CAKE_LIST_CHECK, entrie => {
                        var item = new IListKeyCache()
                        {
                            time = med.TimeStamp(),
                            list = new List<ICacheItemExpire>() { }
                        };

                        entrie.SetValue(item);
                        return item;
                    });

                    var items = checke.list.Where(x => x.time < curTime).ToList();

                    foreach (var item in items)
                    {
                        _memoryCache.Remove(item.name);
                    }
                    _memoryCache.Set(CACHEKEY.CAKE_TIME, curTime);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
