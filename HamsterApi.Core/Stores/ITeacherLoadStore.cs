
using HamsterApi.Core.Models;
using HamsterApi.Core.Stores.Base;

namespace HamsterApi.Core.Stores;

public interface ITeacherLoadStore:IBaseStore<TeachingLoad>
{
    public Task<bool> Update(string id, int lecturesHours, int practiceHours, int laboratoryHours, int lecturesHoursMax, int practiceHoursMax, int laboratoryHoursMax);
}
