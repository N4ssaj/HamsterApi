
using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface.Base;

namespace HamsterApi.Domain.ServiceInterface;

public interface IDirectionService : IBaseService<Direction>
{
    public Task<bool> Update(string id, string title, IReadOnlyCollection<string> groupsIds, FormOfEducation formOfEducation, LevelOfEducation levelOfEducation, string departmentId);
    public Task<bool> AddGroupById(string id, string groupId);
    public Task<bool> RemoveGroupById(string id, string groupId);
    public Task<bool> AddRangeGroupById(string id, IEnumerable<string> groupId);
    public Task<bool> RemoveRangeGroupById(string id, IEnumerable<string> groupId);
    public Task<bool> AddDepartment(string id, string departmentId);
    public Task<bool> RemoveDepartment(string id);
}
