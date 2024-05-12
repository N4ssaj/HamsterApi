using BrightstarDB.Client;
using HamsterApi.Persistence.Repositories;
using HamsterApi.Domain.RepositoriesInterfaces;
using Microsoft.Extensions.DependencyInjection;

namespace HamsterApi.Persistence.DIExtension;

public static class DependencyInjectionPersistenceExtensions
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAuditoriumRepository, AuditoriumRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<ITeacherRepository, TeacherRepository>();
        services.AddScoped<IDirectionRepository, DirectionRepository>();
        services.AddScoped<IChairRepository, ChairRepository>();
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IScheduleRepository, ScheduleRepository>();
        services.AddScoped<ITeachingLoadRepository, TeachingLoadRepository>();
        services.AddScoped<IAcademicLoadRepository, AcademicLoadRepository>();
        services.AddScoped<ISemesterRepository, SemesterRepository>();
        services.AddScoped<ICurriculumRepository, CurriculumRepository>();

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
