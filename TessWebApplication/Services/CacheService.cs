using System;
using System.Web;
using System.Web.Caching;

namespace Greenspoon.Tess.Services
{
    public class CacheService<T>
    {
        private readonly string _cacheKey;
      
        public CacheService(string key)
        {
            _cacheKey = key;
        } 

        public T Grab()
        {
            return (T)HttpContext.Current.Cache.Get(_cacheKey);
        }

        public void Insert(T obj, DateTime duration, CacheItemPriority priority = CacheItemPriority.Default)
        {
            HttpContext.Current.Cache.Insert(_cacheKey, obj, null, duration, TimeSpan.Zero);
        }

        public void Clear()
        {
            HttpContext.Current.Cache.Remove(_cacheKey);
        }

    }
}