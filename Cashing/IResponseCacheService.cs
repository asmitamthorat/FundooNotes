using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cashing
{
    public interface IResponseCacheService
    {
         Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);
        Task<string> GetCachedResponseAsync(string key);
    }
}
