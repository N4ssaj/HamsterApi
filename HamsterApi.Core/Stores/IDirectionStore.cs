using HamsterApi.Core.Common.Enum;
using HamsterApi.Core.Models;
using HamsterApi.Core.Stores.Base;

namespace HamsterApi.Core.Stores;

public interface IDirectionStore:IBaseStore<Direction>
{
    public Task<bool> Update(string id, string title, IReadOnlyCollection<string> groupsIds, FormOfEducation formOfEducation, LevelOfEducation levelOfEducation, string departmentId);
    public Task<bool> AddGroupById(string id, string groupId);
    public Task<bool> RemoveGroupById(string id, string groupId);
    public Task<bool> AddRangeGroupById(string id, IEnumerable<string> groupId);
    public Task<bool> RemoveRangeGroupById(string id, IEnumerable<string> groupId);
}
