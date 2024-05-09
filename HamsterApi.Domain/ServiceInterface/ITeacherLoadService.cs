
using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface.Base;

namespace HamsterApi.Domain.ServiceInterface;

public interface ITeacherLoadService : IBaseService<TeachingLoad>
{
    public Task<bool> Update(string id, int lecturesHours, int practiceHours, int laboratoryHours, int lecturesHoursMax, int practiceHoursMax, int laboratoryHoursMax);
}
