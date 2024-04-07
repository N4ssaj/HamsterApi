
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface.Base;

namespace HamsterApi.Core.ServiceInterface;

public interface IDepartmentService:IBaseService<Department>
{
    public Task<bool> Update(string id, string title, IReadOnlyCollection<string> chairsIds, IReadOnlyCollection<string> directionsIds);
    public Task<bool> AddRangeChairById(string id, IEnumerable<string> chairId);
    public Task<bool> RemoveRangeChairById(string id, IEnumerable<string> chairId);
    public Task<bool> AddRangeDirectionById(string id, IEnumerable<string> directionId);
    public Task<bool> RemoveRangeDirectionById(string id, IEnumerable<string> directionId);
}
