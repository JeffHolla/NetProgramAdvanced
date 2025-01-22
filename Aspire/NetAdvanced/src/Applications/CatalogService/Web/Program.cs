using System.Text.Json.Serialization;
using CatalogService.Infrastructure;
using CatalogService.Infrastructure.Data;

namespace CatalogService.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var appSettingsEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", optional: false)
                      .AddJsonFile($"appsettings.{appSettingsEnvironment}.json", optional: false)
                      .AddEnvironmentVariables();

        // Aspire
        builder.AddServiceDefaults();
        builder.AddNpgsqlDbContext<ApplicationDbContext>("postgresdb");

        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(builder.Configuration);

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

        // -------------- Request Pipeline --------------
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            await app.InitialiseDatabaseAsync();
            app.UseDeveloperExceptionPage();
        }

        if (app.Environment.IsProduction())
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        //app.UseHttpsRedirection();

        app.UseRouting();

        // https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-8.0&tabs=visual-studio#configure-identity-services
        app.UseAuthentication();

        app.UseAuthorization();

        app.MapControllers()
            .RequireAuthorization();

        app.MapControllers();
        app.Run();
    }
}
