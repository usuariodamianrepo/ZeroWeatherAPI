using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using ZeroWeatherAPI.Core.Dtos;
using ZeroWeatherAPI.Core.Interfaces;
using ZeroWeatherAPI.Core.Interfaces.Repositories;
using ZeroWeatherAPI.Core.Interfaces.Services;
using ZeroWeatherAPI.Core.Interfaces.Shared;
using ZeroWeatherAPI.Infrastructure.Data;
using ZeroWeatherAPI.Infrastructure.Repositories;
using ZeroWeatherAPI.Infrastructure.Services;
using ZeroWeatherAPI.Services;

namespace ZeroWeatherAPI.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddContextInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Default")));
        }

        public static void AddRepositoryCore(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped(typeof(ICityRepository), typeof(CityRepository));
            services.AddScoped(typeof(IWeatherRepository), typeof(WeatherRepository));

            services.AddScoped(typeof(ICityService), typeof(CityService));
            services.AddScoped(typeof(IWeatherService), typeof(WeatherService));
            services.AddScoped(typeof(IStoredProcedureService), typeof(StoredProcedureService));
        }

        public static void AddSharedInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration.GetSection("UrlSettings");
            services.Configure<UrlSettings>(url);

            // Clientes con tipo IHttpClientFactory
            services.AddHttpClient<IOpenWeatherService, OpenWeatherService>(httpClient =>
                    {
                        httpClient.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/weather");
                    })
                    .ConfigurePrimaryHttpMessageHandler(sp =>
                    {
                        return new HttpClientHandler()
                        {
                            UseProxy = true,
                            DefaultProxyCredentials = CredentialCache.DefaultCredentials
                        };
                    });
        }
    }
}
