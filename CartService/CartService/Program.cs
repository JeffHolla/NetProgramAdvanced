using Asp.Versioning;
using CartService.BLL.CartLogic;
using CartService.Common.Entities;
using CartService.DAL.Repositories;
using CartService.DAL.Repositories.Common;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.Json.Serialization;

namespace CartService
{
    internal class Program
    {
        private static IServiceProvider _serviceProvider;
        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            AddServices(builder);

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });

            // https://www.milanjovanovic.tech/blog/api-versioning-in-aspnetcore
            builder.Services.AddApiVersioning(opt =>
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

            //https://github.com/dotnet/aspnet-api-versioning/wiki/API-Documentation#aspnet-core
            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            builder.Services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";  
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            builder.Services.AddEndpointsApiExplorer();

            // -------------- Request Pipeline --------------
            var app = builder.Build();
            _serviceProvider = app.Services;

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    foreach (var description in app.DescribeApiVersions())
                    {
                        var url = $"/swagger/{description.GroupName}/swagger.json";
                        var name = description.GroupName.ToUpperInvariant();
                        options.SwaggerEndpoint(url, name);
                    }
                });

                app.UseDeveloperExceptionPage();
            }

            if (app.Environment.IsProduction())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }

        private static void AddServices(IHostApplicationBuilder builder)
        {
            builder.Services.AddScoped<IRepository<Cart>, CartRepository>();
            builder.Services.AddScoped<ICartLogicHandler, CartLogicHandler>();

            builder.Services.AddSingleton<IDbConnectionProvider, DbLiteConnectionProvider>(_ =>
                    new DbLiteConnectionProvider() { ConnectionString = "./MyData.db" }); // It's better to move Db path into a configuration file
        }

        private static IHostApplicationBuilder GetConsoleBuilder()
            => Host.CreateApplicationBuilder();

        private static IHostApplicationBuilder GetWebBuilder()
            => Host.CreateApplicationBuilder();
    }
}
