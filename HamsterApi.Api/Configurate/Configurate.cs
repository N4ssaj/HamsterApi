using HamsterApi.Application.DIExtensions;
using HamsterApi.Persistence.DIExtension;
using Microsoft.OpenApi.Validations;
using Serilog;
using Serilog.Events;

namespace HamsterApi.Api.Configurate;

public static class Configurate
{
    public static void Register(this IServiceCollection services, string connectionString,string dir)
    {
        services.RegisterDb(connectionString,dir);
        services.RegisterRepositories();
        services.RegisterServices();
        services.AddCors();
        Log.Logger=new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Seq("http://hamster-seq:5340")
            .WriteTo.Console()
            .CreateLogger();

        services.AddSingleton<Serilog.ILogger>(Log.Logger);
    }
}