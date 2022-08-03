namespace Farmasi_Project_Web.Infrastructure.Extensions
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
      services.AddControllersWithViews();
    }
  }
}
