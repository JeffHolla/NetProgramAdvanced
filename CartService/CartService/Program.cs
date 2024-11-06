﻿using CartService.BLL.CartLogic;
using CartService.BLL.CartLogic.Events;
using CartService.Common.Messaging;
using CatalogService.Infrastructure.Services.CartService;
using Microsoft.Extensions.Options;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace CartService
{
    internal class Program
    {
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var appSettingsEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false)
              .AddJsonFile($"appsettings.{appSettingsEnvironment}.json", optional: false);

            builder.Services.AddServices(builder.Configuration);

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });

            builder.Services.AddVersioning();
            builder.Services.AddSwagger();

            // -------------- Request Pipeline --------------
            var app = builder.Build();
            _serviceProvider = app.Services;

            ConfigureQueueListening(app);

            ConfigurePipeline(app);

            app.Run();
        }

        internal static void ConfigurePipeline(WebApplication app)
        {
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
        }

        private static IHostApplicationBuilder GetConsoleBuilder()
            => Host.CreateApplicationBuilder();

        private static IHostApplicationBuilder GetWebBuilder()
            => Host.CreateApplicationBuilder();

        private static void ConfigureQueueListening(WebApplication app)
        {
            var queueOptions = app.Services.GetService<IOptions<CartQueueOptions>>();
            var client = app.Services.GetService<IQueueClient>();
            client.QueueName = queueOptions.Value.QueueName;
            client.HostName = queueOptions.Value.HostName;

            client.ConfigureReceiveMessageAsync(Handler);
        }

        private static async Task Handler(object model, BasicDeliverEventArgs eventArgs)
        {
            using var scope = _serviceProvider.CreateAsyncScope();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            logger.LogInformation(message);

            var jsonObject = JsonObject.Parse(message);
            var messageType = jsonObject["Type"].GetValue<string>();

            switch(messageType)
            {
                // Surely there must be a better approach
                case nameof(UpdateProductEvent): 
                    var updateProductEvent = JsonSerializer.Deserialize<UpdateProductEvent>(message);
                    var eventHandler = scope.ServiceProvider.GetService<ICartEventHandler>();
                    await eventHandler.UpdateItemInAllCartsAsync(updateProductEvent);
                    break;
                default: 
                    throw new ArgumentException($"Unexpected event received: '{messageType}' / '{message}'.");
            };
        }
    }
}
