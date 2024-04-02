using AutoMapper;
using HamsterApi.Application.Service;
using HamsterApi.Core.ServiceInterface;
using HamsterApi.Core.Stores;
using HamsterApi.DataAccess;
using HamsterApi.DataAccess.MappingProfile;
using HamsterApi.DataAccess.Repositories;
using System.Runtime.CompilerServices;
using VDS.RDF.Query.Algebra;

namespace HamsterApi.Api.Configurate;

public static class Configurate
{

    public static void RegisterMapper(this IServiceCollection _services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile<GeneralProfile>();
        });
        var mapper = mapperConfig.CreateMapper();
        _services.AddSingleton(mapper);
    }
    private static void RegisterStores(this IServiceCollection _services)
    {
        _services.AddSingleton<IAuditoriumStore, AuditoriumRepository>();
        _services.AddSingleton<IGroupStore, GroupRepository>();
        _services.AddSingleton<ISubjectStore, SubjectRepository>();
        _services.AddSingleton<ITeacherStore, TeacherRepository>();
        _services.AddSingleton<IDirectionStore,DirectionRepository>();
    }

    private static void RegisterService(this IServiceCollection _services)
    {
        _services.AddSingleton<IAuditoriumService, AuditoruimService>();
        _services.AddSingleton<IGroupService, GroupService>();
        _services.AddSingleton<ISubjectService, SubjectService>();
        _services.AddSingleton<ITeacherService,TeacherService>();
        _services.AddSingleton<IDirectionService, DirectionService>();
    }
    private static void RegisterDb(this IServiceCollection _services,string connectionString)
    {
        var hamsterApiDbContext = new HamsterApiDbContext(connectionString);
        _services.AddSingleton(hamsterApiDbContext);
    }
    public static void Register(this IServiceCollection _services, string connectionString)
    {
        _services.RegisterDb(connectionString);
        _services.RegisterMapper();
        _services.RegisterStores();
        _services.RegisterService();
    }
}