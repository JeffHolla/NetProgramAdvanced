using CartService.BLL.CartLogic;
using CartService.Common.Entities;
using CartService.DAL.Repositories;
using CartService.DAL.Repositories.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CartService
{
    // This whole file is not in scope of the task. The purpose of this file is only for tests and DI.
    internal class Program
    {
        private static IServiceProvider _serviceProvider;
        static void Main(string[] args)
        {
            AddServices();
        }

        public static void AddServices()
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder();

            builder.Services.AddScoped<IRepository<Cart>, CartRepository>();
            builder.Services.AddScoped<ICartLogicHandler, CartLogicHandler>();

            builder.Services.AddScoped<IDbConnectionProvider, DbLiteConnectionProvider>(_ =>
                    new DbLiteConnectionProvider() { ConnectionString = "./MyData.db" }); // It's better to move Db path into a configuration file

            using var host = builder.Build();
            _serviceProvider = host.Services;
        }
    }
}
