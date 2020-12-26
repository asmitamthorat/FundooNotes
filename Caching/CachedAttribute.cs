using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caching
{
    public class CachedAttribute:Attribute,IActionFilter
    {

        private readonly int _timeToLiveSeconds;
        private string key;
        public CachedAttribute(int timeToLiveSeconds)
        {
            _timeToLiveSeconds = timeToLiveSeconds;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var cacheSettings = context.HttpContext.RequestServices.GetRequiredService<CacheSettings>();

            if (!cacheSettings.IsEnabled)
            {
                next();

            }
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            var cachekey = GenerateCacheKeyFromRequest(context.HttpContext.Request);
            string cachedResponse = cacheService.GetCachedResponse(cachekey);

            if (!string.IsNullOrEmpty(cachedResponse) && context.HttpContext.Request.Method == "GET")
            {
                var contentResult = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult;

            }
            var executedContextResult = next();

            if (executedContextResult.Result is OkObjectResult okObjectResult)
            {
                cacheService.CacheResponse(cachekey, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveSeconds));
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            next();
        }

        public void OnActionExecution(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           
           

        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var KeyBuilder = new StringBuilder();
            KeyBuilder.Append($"{request.Path}");
            foreach (var (key,value) in request.Query.OrderBy(x=>x.Key)) 
            {
                KeyBuilder.Append($"|{key }-{value}");
            }
            return KeyBuilder.ToString();     
        }
    }
}
