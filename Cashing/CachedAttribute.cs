using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashing
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {

        private int _timeToLiveSeconds;
        private string key;
        public CachedAttribute(int timeToLiveSeconds)
        {
           _timeToLiveSeconds = timeToLiveSeconds;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
             var cacheSettings = context.HttpContext.RequestServices.GetRequiredService<CacheSettings>();
            if (!cacheSettings.IsEnabled)
            {
                await next();
                return;
            }
            var cacheService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();
            string key = (string)context.HttpContext.Items["userId"];
            string cachedResponse = await cacheService.GetCachedResponseAsync(key);

            if (!string.IsNullOrEmpty(cachedResponse) && context.HttpContext.Request.Method == "GET")
            {
                var contentResult = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentResult;
                return;

            }
            var executedContextResult = await next();

            if (executedContextResult.Result is OkObjectResult okObjectResult)
            {
                await cacheService.CacheResponseAsync(key, okObjectResult.Value, TimeSpan.FromSeconds(_timeToLiveSeconds));
            }
        }

       

        //public string GenerateCacheKeyFromRequest(HttpRequest request)
        //{
        //    var KeyBuilder = new StringBuilder();
        //    KeyBuilder.Append($"{request.Path}");
        //    foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
        //    {
        //        KeyBuilder.Append($"|{key }-{value}");
        //    }
        //    return KeyBuilder.ToString();
        //}

       
    }
}



