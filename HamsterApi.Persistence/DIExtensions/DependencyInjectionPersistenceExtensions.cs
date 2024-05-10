using BrightstarDB.Client;
using HamsterApi.Persistence.Repositories;
using HamsterApi.Domain.RepositoriesInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HamsterApi.Persistence.DIExtension;

public static class DependencyInjectionPersistenceExtensions
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IAuditoriumRepository, AuditoriumRepository>();
        services.AddSingleton<IGroupRepository, GroupRepository>();
        services.AddSingleton<ISubjectRepository, SubjectRepository>();
        services.AddSingleton<ITeacherRepository, TeacherRepository>();
        services.AddSingleton<IDirectionRepository, DirectionRepository>();
        services.AddSingleton<IChairRepository, ChairRepository>();
        services.AddSingleton<IDepartmentRepository, DepartmentRepository>();
        services.AddSingleton<IScheduleRepository, ScheduleRepository>();
        services.AddSingleton<ITeachingLoadRepository, TeachingLoadRepository>();
        services.AddSingleton<IAcademicLoadRepository, AcademicLoadRepository>();
        services.AddSingleton<ISemesterRepository, SemesterRepository>();

        return services;
    }
    public static IServiceCollection RegisterDb(this IServiceCollection services,string connectionString,string dir)
    {
        var hamsterApiDbContext = new HamsterApiDbContext(connectionString);
        services.AddSingleton(hamsterApiDbContext);
        var client = BrightstarService.GetClient(dir);
        services.AddSingleton(client);

        return services;
    }
}
