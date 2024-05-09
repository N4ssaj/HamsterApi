
using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface.Base;

namespace HamsterApi.Domain.ServiceInterface;

public interface IScheduleService : IBaseService<Schedule>
{
    public Task<bool> Update(string id, int semesterNumber, IReadOnlyCollection<string> groupsScheduleIds);
    public Task<bool> AddGroup(string id, IEnumerable<string> groupIds);
    public Task<bool> RemoveGroup(string id, IEnumerable<string> groupIds);
}
