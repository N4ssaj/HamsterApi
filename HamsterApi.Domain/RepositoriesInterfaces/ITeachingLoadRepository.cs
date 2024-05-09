
using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces.Base;

namespace HamsterApi.Domain.RepositoriesInterfaces;

public interface ITeachingLoadRepository : IBaseRepository<TeachingLoad>
{
    public Task<bool> Update(string id, int lecturesHours, int practiceHours, int laboratoryHours, int lecturesHoursMax, int practiceHoursMax, int laboratoryHoursMax);
}
