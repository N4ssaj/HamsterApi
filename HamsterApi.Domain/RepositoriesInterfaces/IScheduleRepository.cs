using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces.Base;

namespace HamsterApi.Domain.RepositoriesInterfaces;

public interface IScheduleRepository : IBaseRepository<Schedule>
{
    public Task<bool> Update(string id, int year,SpringOrAutumn springOrAutumn,IReadOnlyCollection<string> groupsScheduleIds);
    public Task<bool> AddGroup(string id, IEnumerable<string> groupIds);
    public Task<bool> RemoveGroup(string id, IEnumerable<string> groupIds);
}
