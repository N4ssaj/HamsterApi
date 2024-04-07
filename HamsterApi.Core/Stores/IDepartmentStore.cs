

using HamsterApi.Core.Models;
using HamsterApi.Core.Stores.Base;

public interface IDepartmentStore : IBaseStore<Department>
{
    public Task<bool> Update(string id, string title, IReadOnlyCollection<string> chairsIds, IReadOnlyCollection<string> directionsIds);
    public Task<bool> AddRangeChairById(string id, IEnumerable<string> chairId);
    public Task<bool> RemoveRangeChairById(string id, IEnumerable<string> chairId);
    public Task<bool> AddRangeDirectionById(string id, IEnumerable<string> directionId);
    public Task<bool> RemoveRangeDirectionById(string id, IEnumerable<string> directionId);
}
