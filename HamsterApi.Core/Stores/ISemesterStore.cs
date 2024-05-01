
using HamsterApi.Core.Models;
using HamsterApi.Core.Stores.Base;

namespace HamsterApi.Core.Stores;

public interface ISemesterStore:IBaseStore<Semester>
{
    public Task<bool> Update(string id, int number, string groupId, List<SubjectWtihLoad> subjects);
    public Task<bool> AddSubjects(string id,IEnumerable<SubjectWtihLoad> subjects);
    public Task<bool> RemoveSubjects(string id,IEnumerable<SubjectWtihLoad> subjects);
}
