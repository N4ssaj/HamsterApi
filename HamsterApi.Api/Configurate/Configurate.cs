using HamsterApi.Application.DIExtensions;
using HamsterApi.Persistence.DIExtension;

namespace HamsterApi.Api.Configurate;

public static class Configurate
{
    public static void Register(this IServiceCollection services, string connectionString,string dir)
    {
        services.RegisterDb(connectionString,dir);
        services.RegisterRepositories();
        services.RegisterServices();
    }
}