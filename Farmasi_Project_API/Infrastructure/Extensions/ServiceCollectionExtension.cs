using Farmasi_Project.Application.Common.Interfaces;
using Farmasi_Project.Application.Models;
using Farmasi_Project.Infrastructure.Persistance;
using Farmasi_Project.Infrastructure.Services;
using Farmasi_Project_API.Helper;
using MediatR;
using StackExchange.Redis;

namespace Farmasi_Project_API.Infrastructure.Extensions
{
  public static class ServiceCollectionExtension
  {
    private static TConfig ConfigureStartupConfig<TConfig>(IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
    {
      if (services == null)
        throw new ArgumentNullException(nameof(services));

      if (configuration == null)
        throw new ArgumentNullException(nameof(configuration));


      var config = new TConfig();

      configuration.Bind(config);

      services.AddSingleton(config);

      return config;
    }


    public static void ConfigureServices(this IServiceCollection services, ConfigureHostBuilder host, ConfigurationManager configuration)
    {
      var appSettings = ConfigureStartupConfig<AppSettings>(services, configuration.GetSection("AppSettings"));
      services.AddControllers();
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen();
      services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
      #region Dependency Injection
      services.AddScoped<IRedisCacheManager, RedisCacheManager>();
      services.AddScoped<ICacheHelper, CacheHelper>();
      services.AddScoped<IApplicationDbContext,ApplicationDbContext>();
      #endregion
 

    }
  }
}
