using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundooServiceLayer.TokenAuthentification
{
    public class TokenAuthenticationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var _tokenManager = (ITokenManager)context.HttpContext.RequestServices.GetService(typeof(ITokenManager));
            if (context.HttpContext.Request.Headers.ContainsKey("Authorization"))
            {
                var token = context.HttpContext.Request.Headers.First(cookie => cookie.Key == "Authorization").Value;
                var ClaimPrinciple = _tokenManager.GetPrincipal(token.ToString().Split(" ")[1]);
                var claimList = ClaimPrinciple.Claims.ToList();
                context.HttpContext.Items["userId"] = claimList[1].Value;
                context.HttpContext.Items["email"] = claimList[0].Value;
            }
            else
            {
                context.ModelState.AddModelError("unauthorized", "Missing token");
            }
        }
    }
}
