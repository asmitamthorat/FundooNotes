using System;
using System.Collections.Generic;
using System.Text;

namespace Caching
{
    public interface IResponseCacheService
    {
        string GetCachedResponse(string key);
        void CacheResponse(string cacheKey, object response, TimeSpan timeToLive);
    }
}
