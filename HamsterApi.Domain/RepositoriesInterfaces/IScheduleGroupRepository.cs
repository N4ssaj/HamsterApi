using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces.Base;

namespace HamsterApi.Domain.RepositoriesInterfaces;

public interface IScheduleGroupRepository : IBaseRepository<ScheduleGroup>
{
    public Task<bool> Update(string id, string scheduleId, string groupId, int semesterNumber, List<ScheduledWeek> weeks);
}
