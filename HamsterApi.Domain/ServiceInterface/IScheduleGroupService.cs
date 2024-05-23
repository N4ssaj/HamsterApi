
using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface.Base;

namespace HamsterApi.Domain.ServiceInterface;

public interface IScheduleGroupService:IBaseService<ScheduleGroup>
{
    public Task<bool> Update(string id, string scheduleId, string groupId, int semesterNumber, List<ScheduledWeek> weeks);
}
