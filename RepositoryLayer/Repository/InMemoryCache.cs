using Microsoft.Extensions.Caching.Memory;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class InMemoryCache : IInMemoryCache
    {
        private IMemoryCache _memoryCache;

        public InMemoryCache(IMemoryCache memoryCache)
        {
            this._memoryCache = memoryCache;
        }

        public List<T> GetCache<T>(string key)
        {
            if (!_memoryCache.TryGetValue(key, out List<T> lstCache))
            {
                return null;
            }
            return lstCache;
        }

        //setting up cache options
        public void SetCache<T>(string key,List<T> value)
        {
             var cacheExpiryOptions = new MemoryCacheEntryOptions
             {
                 AbsoluteExpiration = DateTime.Now.AddDays(1),
                 SlidingExpiration = TimeSpan.FromMinutes(1)
             };
             //setting cache entries
             _memoryCache.Set(key, value, cacheExpiryOptions);
            
        }
    }
}
