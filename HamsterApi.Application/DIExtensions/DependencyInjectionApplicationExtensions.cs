
using HamsterApi.Application.Service;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Domain.ServiceInterface;
using Microsoft.Extensions.DependencyInjection;

namespace HamsterApi.Application.DIExtensions;

public static  class DependencyInjectionApplicationExtensions
{
   public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddSingleton<IAcademicLoadService,AcademicLoadService>();
        services.AddSingleton<IAuditoriumService,AuditoruimService>();
        services.AddSingleton<IChairService, ChairService>();
        services.AddSingleton<IDepartmentService, DepartmentService>();
        services.AddSingleton<IDirectionService, DirectionService>();
        services.AddSingleton<IGroupService, GroupService>();
        services.AddSingleton<IScheduleService, ScheduleService>();
        services.AddSingleton<ISubjectService, SubjectService>();
        services.AddSingleton<ITeacherService, TeacherService>();
        services.AddSingleton<ITeacherLoadService, TeacherLoadService>();
        services.AddSingleton<ISemesterService, SemesterService>();
        return services;
    }
}
