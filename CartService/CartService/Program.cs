using Asp.Versioning;
using CartService.BLL.CartLogic;
using CartService.Common.Entities;
using CartService.DAL.Repositories;
using CartService.DAL.Repositories.Common;
using Microsoft.OpenApi.Models;
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

            builder.Services.AddApiVersioning(opt => 
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Cart API",
                    Description = "Cart API"
                });

                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "Cart API",
                    Description = "Cart API"
                });
            });

            // -------------- Request Pipeline --------------
            var app = builder.Build();
            _serviceProvider = app.Services;

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseDeveloperExceptionPage();
            }

            if (app.Environment.IsProduction())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

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
