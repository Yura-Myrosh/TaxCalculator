using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using TaxCalculator.Common.Implementation;
using TaxCalculator.Common.Interfaces;
using TaxCalculator.Common.Models;
using TaxCalculator.DAL.Context;
using TaxCalculator.DAL.Implementation;
using TaxCalculator.DAL.Interfaces;
using TaxCalculator.DbModels;

namespace TaxCalculator.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTaxCalculatorDataAccess(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TaxCalculatorDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<IRepository<TaxBand>, Repository<TaxBand, TaxCalculatorDbContext>>();
            services.AddScoped<ITaxBandRepository, TaxBandRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddErrorManager<TEnum>(this IServiceCollection services)
            where TEnum : Enum
        {
            services.AddScoped<IErrorManager<CommonErrorEnum>, ErrorManager<CommonErrorEnum>>();
            services.AddScoped<IErrorManager<TEnum>, ErrorManager<TEnum>>();
            return services;
        }

        public static IServiceCollection AddCommonBlRegistrations<T>(this IServiceCollection services)
        {
            services.AddScoped<ICommonValidator<T>, CommonValidator<T>>();
            services.AddScoped<ICRUDService<T>, CRUDService<T>>();
            return services;
        }

        public static IServiceCollection AddAutomapperRegistration(this IServiceCollection services, Profile[] mappingProfiles)
        {
            var mapper = new MapperConfiguration(mc =>
            {
                mc.AddProfiles(mappingProfiles);
            }).CreateMapper();

            services.AddSingleton(mapper);

            return services;
        }
    }
}
