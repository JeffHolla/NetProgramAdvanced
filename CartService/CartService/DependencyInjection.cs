﻿using Asp.Versioning;
using CartService.BLL.CartLogic;
using CartService.BLL.CartLogic.Events;
using CartService.Common.Entities;
using CartService.Common.Messaging;
using CartService.DAL.Repositories;
using CartService.DAL.Repositories.Common;
using CatalogService.Infrastructure.Services.CartService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.Json.Nodes;
using System.Text;
using CartService.PL.WebAPI;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace CartService
{
    internal static class DependencyInjection
    {
        internal static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRepository<Cart>, CartRepository>();
            services.AddScoped<ICartLogicHandler, CartLogicHandler>();
            services.AddScoped<ICartEventHandler, CartEventHandler>();

            services.AddSingleton<IDbConnectionProvider, DbLiteConnectionProvider>(_ =>
                    new DbLiteConnectionProvider() { ConnectionString = configuration.GetConnectionString("DbLite") });

            // Since we don't have a "scope" to retrieve messages, maybe we should use a Singleton?
            // Another option is to split the receiving and sending of messages into two separate entities.
            // And another option is to split Queue on Top Level Handlers and subscribers with IQueueClient
            services.AddSingleton<IQueueClient, RabbitQueue>();
            services.Configure<CartQueueOptions>(configuration.GetSection("CartQueue"));
        }

        internal static void AddVersioning(this IServiceCollection services)
        {
            // https://www.milanjovanovic.tech/blog/api-versioning-in-aspnetcore
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc() // ← bring in MVC (Core); not required for Minimal APIs, but required for controllers
            .AddApiExplorer(opt =>
            {
                // format the version as "'v'major[.minor][-status]"
                opt.GroupNameFormat = "'v'V";
                opt.SubstituteApiVersionInUrl = true;
            });
        }

        internal static void AddSwagger(this IServiceCollection services)
        {
            //https://github.com/dotnet/aspnet-api-versioning/wiki/API-Documentation#aspnet-core
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            services.AddEndpointsApiExplorer();
        }

        internal static void ConfigureAuthentication(this IServiceCollection services)
        {
            const string oidcScheme = OpenIdConnectDefaults.AuthenticationScheme;
            services.AddAuthentication(oidcScheme)
                    .AddKeycloakOpenIdConnect("keycloak", realm: "master", oidcScheme, options =>
                    {
                        options.ClientId = "cartservice";
                        options.ResponseType = OpenIdConnectResponseType.Code;
                        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                        options.Scope.Clear();
                        options.Scope.Add("openid");
                        options.Scope.Add("profile");
                        options.GetClaimsFromUserInfoEndpoint = true;

                        options.MapInboundClaims = false; // Don't rename claim types (to Microsoft type names)

                        options.ClaimActions.MapJsonKey("role", "role", "role");    //rename the role claim to get this claim from IdP
                        options.TokenValidationParameters.NameClaimType = "name";   //rename the name claim to get this claim from IdP
                        options.TokenValidationParameters.RoleClaimType = "role";   //rename the role claim to get this claim from IdP

                        options.SaveTokens = true;

                        options.RequireHttpsMetadata = false; // Disable HTTPS, need to be enabled in the production
                    })
                    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

            services.AddCascadingAuthenticationState();
        }

        internal static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization();
        }
    }
}
