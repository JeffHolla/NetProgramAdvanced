using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            [
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            ];

        public static IEnumerable<ApiScope> ApiScopes =>
            [
                new ApiScope("catalog_api"),
                new ApiScope("cart_api")
            ];

        public static IEnumerable<Client> Clients =>
            [
                // web client using code flow + pkce
                //new Client
                //{
                //    ClientId = "catalog_service",
                //    ClientSecrets = { new Secret("secret".Sha256()) },

                //    AllowedGrantTypes = GrantTypes.Code,

                //    // where to redirect to after login
                //    RedirectUris = { "https://localhost:54283/signin-oidc" },
                //    // where to redirect to after logout
                //    PostLogoutRedirectUris = { "https://localhost:54283/signout-callback-oidc" },
                    
                //    AllowedScopes =
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //        "catalog_api",
                //        "cart_api"
                //    }
                //},
                // web client using code flow + pkce
                new Client
                {
                    ClientId = "cart_service",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,

                    // where to redirect to after login
                    //RedirectUris = { "https://localhost:57931/signin-oidc" },
                    //RedirectUris = { "https://localhost:4430/signin-oidc" },
                    //RedirectUris = { "https://cartservice:443/signin-oidc" },
                    //RedirectUris = { "http://localhost:30500/signin-oidc" },
                    RedirectUris = { "http://cartservice:80/signin-oidc" },
                    // where to redirect to after logout
                    //PostLogoutRedirectUris = { "https://localhost:57931/signout-callback-oidc" },
                    //PostLogoutRedirectUris = { "https://localhost:4430/signout-callback-oidc" },
                    //PostLogoutRedirectUris = { "https://cartservice:443/signout-callback-oidc" },
                    //PostLogoutRedirectUris = { "http://localhost:30500/signout-callback-oidc" },
                    PostLogoutRedirectUris = { "http://cartservice:80/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "catalog_api",
                        "cart_api"
                    }
                }
            ];
    }
}
