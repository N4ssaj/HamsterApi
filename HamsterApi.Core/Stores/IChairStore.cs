using HamsterApi.Core.Models;
using HamsterApi.Core.Stores.Base;

namespace HamsterApi.Core.Stores;

public interface IChairStore:IBaseStore<Chair>
{
    public Task<bool> Update(string id, string title, IReadOnlyCollection<string> teachersIds, string departmentId);
    public Task<bool> AddTeacherById(string id, string teacherId);
    public Task<bool> RemoveTeacherById(string id, string teacherId);
    public Task<bool> AddRangeTeacherById(string id, IEnumerable<string> teacherId);
    public Task<bool> RemoveRangeTeacherById(string id, IEnumerable<string> teacherId);
}
