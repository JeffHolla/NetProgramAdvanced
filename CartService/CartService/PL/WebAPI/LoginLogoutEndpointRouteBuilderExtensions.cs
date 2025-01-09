using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CartService.PL.WebAPI
{
    public static class LoginLogoutEndpointRouteBuilderExtensions
    {
        public static IEndpointConventionBuilder MapLoginAndLogout(
            this IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("authentication");

            group.MapGet(pattern: "/login", OnLogin).AllowAnonymous();
            group.MapPost(pattern: "/logout", OnLogout);

            return group;
        }

        public static ChallengeHttpResult OnLogin() =>
            TypedResults.Challenge(properties: new AuthenticationProperties
            {
                RedirectUri = "/"
            });

        public static SignOutHttpResult OnLogout() =>
            TypedResults.SignOut(properties: new AuthenticationProperties
            {
                RedirectUri = "/"
            },
            [
                CookieAuthenticationDefaults.AuthenticationScheme,
                OpenIdConnectDefaults.AuthenticationScheme
            ]);
    }
}
