using TaxCalculator.BL.Interfaces;
using TaxCalculator.BL.Services;
using TaxCalculator.BL.Validator;
using TaxCalculator.DbModels;
using TaxCalculator.Extensions;
using TaxCalculator.Host.Filters;
using NLog.Web;
using System.Diagnostics;

namespace TaxCalculator.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppContext.SetSwitch("Microsoft.AspNetCore.Mvc.NewtonJson.EnableSkipHandledError", true);

            var builder = WebApplication.CreateBuilder(args);
            RegisterDefaultServices(args, builder);
            RegisterSpecificServices(builder);
            AddCors(builder);

            var app = builder.Build();

            app.UseCors("ConfiguredCorsPolicy");
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            ConfigureLog(app);
            app.Run();
        }

        private static void RegisterDefaultServices(string[] args, WebApplicationBuilder builder)
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Configuration.AddCommandLine(args);
            builder.Configuration.AddEnvironmentVariables();

            builder.Logging.ClearProviders();
            builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            builder.Host.UseNLog();
        }

        private static void AddCors(WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
            {
                var corsSettings = builder.Configuration.GetSection("Cors");
                var allowedOrigins = corsSettings.GetSection("AllowedOrigins").Get<string[]>();
                var allowedMethods = corsSettings.GetSection("AllowedMethods").Get<string[]>();
                var allowedHeaders = corsSettings.GetSection("AllowedHeaders").Get<string[]>();

                options.AddPolicy("ConfiguredCorsPolicy", builder =>
                {
                    builder.WithOrigins(allowedOrigins)
                           .WithMethods(allowedMethods)
                           .WithHeaders(allowedHeaders);
                });
            });
        }

        private static void RegisterSpecificServices(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration["TaxCalculatorConnectionString"]!;

            builder.Services.AddTaxCalculatorDataAccess(connectionString);
            builder.Services.AddErrorManager<TaxBandErrorEnum>();
            builder.Services.AddCommonBlRegistrations<TaxBand>();
            builder.Services.AddScoped<ITaxBandCRUDService, TaxBandCRUDService>();
            builder.Services.AddScoped<ITaxService, TaxService>();
            builder.Services.AddScoped<ITaxBandValidator, TaxBandValidator>();

            builder.Services.AddLogging();

            builder.Services
                .AddControllers(
                    options => options.Filters.Add<CurtomExceptionFilter>())
                .AddNewtonsoftJson();
        }

        private static WebApplication ConfigureLog(WebApplication app)
        {
            var logger = app.Services.GetRequiredService<ILoggerFactory>().CreateLogger("CustomLogging");

            app.Use(async (context, next) =>
            {
                logger.LogInformation($"Handling request: {context.Request.Method} {context.Request.Path}");
                var watch = Stopwatch.StartNew();
                await next.Invoke();
                watch.Stop();
                logger.LogInformation($"Finished handling request. Response Status Code: {context.Response.StatusCode}. Time taken: {watch.ElapsedMilliseconds} ms.");
            });

            return app;
        }
    }
}
