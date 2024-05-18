using Serilog;


namespace HamsterApi.Api.Configurate;

public static class Configurate
{
    public static IServiceCollection RegisterService(this IServiceCollection services)
    {
        services.AddCors();

        return services;
    }

    public static IServiceCollection AddLogger(this IServiceCollection services)
    {
        var serilogLogger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.File("log.txt",
                rollingInterval: RollingInterval.Day,
                rollOnFileSizeLimit: true)
            .WriteTo.Seq("http://hamster-seq:5341")
            .WriteTo.Console()
            .CreateLogger();

        services.AddLogging(configure=>configure.AddSerilog(serilogLogger,true));

        return services;
    }
    public static IServiceCollection AddRedis(this IServiceCollection services)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "hamster-redis:6379,abortConnect=False";
            options.InstanceName = "hamster-redis";
        });

        return services;
    }
}