
using HamsterApi.Core.Models;
using HamsterApi.Core.Stores.Base;

namespace HamsterApi.Core.Stores;

public interface ISubjectStore:IBaseStore<Subject>
{
    public Task<bool> Update(string id, string title, string index, IReadOnlyCollection<string> teachersIds);
    public Task<Subject?> ReadByIndex(string index);
    public Task<bool> AddTeacherById(string id, string teacherId);
    public Task<bool> RemoveTeacherById(string id, string teacherId);
    public Task<bool> AddRangeTeacherById(string id, IEnumerable<string> teacherId);
    public Task<bool> RemoveRangeTeacherById(string id, IEnumerable<string> teacherId);
}
