
using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces.Base;

namespace HamsterApi.Domain.RepositoriesInterfaces;

public interface ISemesterRepository : IBaseRepository<Semester>
{
    public Task<bool> Update(string id, int number, string groupId, List<SubjectWtihLoad> subjects);
    public Task<bool> AddSubjects(string id, IEnumerable<SubjectWtihLoad> subjects);
    public Task<bool> RemoveSubjects(string id, IEnumerable<string> subjectsIds);
}
