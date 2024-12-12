using CatalogService.Application.Common.Interfaces.Database;
using CatalogService.Application.Common.Interfaces.Messaging;
using CatalogService.Application.Common.Interfaces.Services;
using CatalogService.Infrastructure.Data;
using CatalogService.Infrastructure.Messaging;
using CatalogService.Infrastructure.Security.Identity;
using CatalogService.Infrastructure.Services.CartService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddScoped<IReadOnlyApplicationDbContext, ReadOnlyApplicationDbContext>();

        services.AddSingleton(TimeProvider.System);

        services.AddScoped<IQueueClient, RabbitQueue>();

        services.AddScoped<ICartClientService, CartClientService>();
        services.Configure<CartQueueOptions>(configuration.GetSection("CartQueue"));

        // Authentication, Authorization
        //services
        //    .AddAuthentication(options =>
        //    {
        //        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        //        options.DefaultChallengeScheme = "oidc";
        //    })
        //    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
        //    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        //    {
        //        options.Authority = "https://localhost:5001";
        //        options.TokenValidationParameters.ValidateAudience = false;
        //    })
        //    .AddOpenIdConnect("oidc", options =>
        //    {
        //        options.Authority = "https://localhost:5001";

        //        options.ClientId = "catalog_service";
        //        options.ClientSecret = "secret";
        //        options.ResponseType = "code";

        //        options.Scope.Clear();
        //        options.Scope.Add("openid");
        //        options.Scope.Add("profile");
        //        options.GetClaimsFromUserInfoEndpoint = true;

        //        options.MapInboundClaims = false; // Don't rename claim types (to Microsoft type names)

        //        options.ClaimActions.MapJsonKey("role", "role", "role");    //rename the role claim to get this claim from IdP
        //        options.TokenValidationParameters.NameClaimType = "name";   //rename the name claim to get this claim from IdP
        //        options.TokenValidationParameters.RoleClaimType = "role";   //rename the role claim to get this claim from IdP

        //        // Issue was in the following default name and type definitions
        //        // ClaimsIdentity.DefaultRoleClaimType - "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        //        // ClaimsIdentity.DefaultNameClaimType - "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"

        //        options.SaveTokens = true;
        //    });

        //services.AddAuthorization();

        return services;
    }
}
