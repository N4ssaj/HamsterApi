
using HamsterApi.Application.Service;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Domain.ServiceInterface;
using Microsoft.Extensions.DependencyInjection;

namespace HamsterApi.Application.DIExtensions;

public static  class DependencyInjectionApplicationExtensions
{
   public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IAcademicLoadService,AcademicLoadService>();
        services.AddScoped<IAuditoriumService,AuditoruimService>();
        services.AddScoped<IChairService, ChairService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IDirectionService, DirectionService>();
        services.AddScoped<IGroupService, GroupService>();
        services.AddScoped<IScheduleService, ScheduleService>();
        services.AddScoped<ISubjectService, SubjectService>();
        services.AddScoped<ITeacherService, TeacherService>();
        services.AddScoped<ITeacherLoadService, TeacherLoadService>();
        services.AddScoped<ISemesterService, SemesterService>();
        services.AddScoped<ICurriculumService, CurriculumService>();

        return services;
    }
}
