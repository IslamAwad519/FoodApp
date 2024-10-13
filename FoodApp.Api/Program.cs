using AutoMapper;
using FoodApp.Api.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using Hangfire;
using FoodApp.Api.VerticalSlicing.Common;
using FoodApp.Api.VerticalSlicing.Data.Context;
using FoodApp.Api.VerticalSlicing.Features.Account.Common.Helper;
using FoodApp.Api.VerticalSlicing.Common.RabbitMQServices;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddApplicationService(builder.Configuration);

        builder.Logging.ClearProviders();


        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        Log.Logger = new LoggerConfiguration()
              .ReadFrom.Configuration(configuration)
              .Enrich.WithMachineName()
              .Enrich.WithThreadId()
              .WriteTo.Console()
              .WriteTo.Seq("http://localhost:5341/")
              .WriteTo.MSSqlServer(connectionString: configuration.GetConnectionString("DefaultConnection"),
              sinkOptions: new MSSqlServerSinkOptions
              {
                  TableName = "Logs",
                  AutoCreateSqlTable = true
              }).CreateLogger();


        builder.Host.UseSerilog();

        builder.Services.AddHangfire(cfg =>
                 cfg.UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddHangfireServer();
        builder.Services.AddHostedService<RabbitMQConsumerService>();
        var app = builder.Build();
        {
            #region Update-Database

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var LoggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbcontext = services.GetRequiredService<ApplicationDBContext>();
                await dbcontext.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "An error occured during updating database");
            }
            #endregion

            app.UseHangfireDashboard("/hangfire");

            MapperHandler.mapper = app.Services.GetService<IMapper>();
            TokenGenerator.options = app.Services.GetService<IOptions<JwtOptions>>()!.Value;

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}