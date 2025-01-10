using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Text;

namespace CartService.PL.WebAPI.Middlewares
{
    public class IdentityLoggingMiddleware(RequestDelegate next)
    {
        public Task Invoke(HttpContext httpContext, ILogger<IdentityLoggingMiddleware> logger)
        {
            var user = httpContext.User;
            var userInfo = GetIdentityInfo(user);

            logger.LogInformation(userInfo);
            
            return next(httpContext);
        }

        private static string GetIdentityInfo(ClaimsPrincipal user)
        {
            var stringBuilder = new StringBuilder(100);

            AddClaimsInfo(stringBuilder, user.Claims);
            AddIdentitiesInfo(stringBuilder, user.Identities);

            return stringBuilder.ToString();
        }

        private static void AddClaimsInfo(StringBuilder stringBuilder, IEnumerable<Claim> claims)
        {
            stringBuilder.AppendLine("Claims: [");
            foreach (var claim in claims)
            {
                stringBuilder.AppendLine("{ ");
                stringBuilder.AppendLine($" {{ Subject: '{claim.Subject}' }}, ");
                stringBuilder.AppendLine($" {{ Issuer: '{claim.Issuer}' }}, ");
                stringBuilder.AppendLine($" {{ Value: '{claim.Value}' }}, ");
                stringBuilder.AppendLine($" {{ ValueType: '{claim.ValueType}' }}");
                stringBuilder.AppendLine("}, ");
            }
            stringBuilder.AppendLine("]");
        }

        private static void AddIdentitiesInfo(StringBuilder stringBuilder, IEnumerable<ClaimsIdentity> identitites)
        {
            stringBuilder.AppendLine("Identities: [");
            foreach (var identity in identitites)
            {
                stringBuilder.AppendLine("{ ");
                stringBuilder.AppendLine($" {{ ActorName: '{identity.Actor?.Name}' }}, ");
                stringBuilder.AppendLine($" {{ IdentityName: '{identity.Name}' }}, ");
                stringBuilder.AppendLine($" {{ AuthenticationType: '{identity.AuthenticationType}' }}, ");
                stringBuilder.AppendLine($" {{ IsAuthenticated: '{identity.IsAuthenticated}' }}");
                stringBuilder.AppendLine($" {{ NameClaimType: '{identity.NameClaimType}' }}");
                stringBuilder.AppendLine($" {{ RoleClaimType: '{identity.RoleClaimType}' }}");
                stringBuilder.AppendLine($" {{ Label: '{identity.Label}' }}");
                stringBuilder.AppendLine("}, ");
            }
            stringBuilder.AppendLine("]");
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LogIdentityMiddlewareExtensions
    {
        public static IApplicationBuilder UseIdentityLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IdentityLoggingMiddleware>();
        }
    }
}
