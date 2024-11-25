using CatalogService.Application.Common.Interfaces.Database;
using CatalogService.Application.Common.Interfaces.Messaging;
using CatalogService.Application.Common.Interfaces.Services;
using CatalogService.Infrastructure.Data;
using CatalogService.Infrastructure.Messaging;
using CatalogService.Infrastructure.Security.Identity;
using CatalogService.Infrastructure.Services.CartService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddScoped<IReadOnlyApplicationDbContext, ReadOnlyApplicationDbContext>();

        services.AddSingleton(TimeProvider.System);

        services.AddScoped<IQueueClient, RabbitQueue>();

        services.AddScoped<ICartClientService, CartClientService>();
        services.Configure<CartQueueOptions>(configuration.GetSection("CartQueue"));

        // Authentication, Authorization
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme);

        services.AddDatabaseDeveloperPageExceptionFilter();
        
        services.AddIdentityApiEndpoints<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddApiEndpoints();

        services.AddAuthorization();

        return services;
    }
}
