using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Caching
{
    public class ResponseCacheService:IResponseCacheService
    {
        private readonly IDistributedCache _distributedCache;
        public void CacheResponse(string cacheKey, object response, TimeSpan timeToLive)
        {
            

            if (response == null)
            {
                return;
            }
            var serializedResponse = JsonConvert.SerializeObject(response);
                _distributedCache.SetString(cacheKey, serializedResponse, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = timeToLive
            });
        }

        public string GetCachedResponse(string key)
        {
            var cachedResponse =  _distributedCache.GetString(key);
            return string.IsNullOrEmpty(cachedResponse) ? null : cachedResponse;
        }
    }
}
