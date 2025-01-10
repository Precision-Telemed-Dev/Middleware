using Precision.API;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Precision.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AuthorizationFilterAttribute : Attribute, IAuthorizationFilter
    {
        readonly IConfiguration _config;
        readonly string? PrecisionUsername;
        readonly string? PrecisionPassword;
        public AuthorizationFilterAttribute(IConfiguration configuration)
        {
            _config = configuration;

            PrecisionUsername = _config.GetValue<string>("PrecisionUsername");
            PrecisionPassword = _config.GetValue<string>("PrecisionPassword");
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string? username = context.HttpContext.Request.Headers["username"];
            string? password = context.HttpContext.Request.Headers["password"];

            if (username != PrecisionUsername || password != PrecisionPassword)
                context.Result = new UnauthorizedObjectResult("Unauthorized");
        }    
    }
}