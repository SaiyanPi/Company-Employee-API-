using Contracts;
using LoggerService;
using Repository;
using Service.Contracts;
using Service;
using Microsoft.EntityFrameworkCore;

namespace CompanyEmployees.Extensions
{
    public static class ServiceExtensions
    {
        // 1) Configuring CORS(Cross Origin Resource Sharing)
        public static void ConfigureCors(this IServiceCollection services) =>
         services.AddCors(options =>
         {
             options.AddPolicy("CorsPolicy", builder =>
             builder.AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader());
         });

        // 1) Configuring IIS
        public static void ConfigureIISIntegration(this IServiceCollection services) =>
        services.Configure<IISOptions>(options =>
         {
         });

        // 3) Configuring logger service for logging messages
        public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();

        // 4) Configuring RepositoryManager
        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

        // 5) Configuring ServiceManager
        public static void ConfigureServiceManager(this IServiceCollection services) =>
        services.AddScoped<IServiceManager, ServiceManager>();

        //registering RepositoryContext class at run runtime
        public static void ConfigureSqlContext(this IServiceCollection services,
        IConfiguration configuration) =>
        services.AddDbContext<RepositoryContext>(opts =>
        opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        // custom formatter
        public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder) =>
        builder.AddMvcOptions(config => config.OutputFormatters.Add(new
        CsvOutputFormatter()));

    }
}
