﻿using Duende.IdentityServer;
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
                    RedirectUris = { "http://cartservice/signin-oidc" },
                    // where to redirect to after logout
                    PostLogoutRedirectUris = { "http://cartservice/signout-callback-oidc" },

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
