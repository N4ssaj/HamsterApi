
using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces.Base;

namespace HamsterApi.Domain.RepositoriesInterfaces;

public interface ISubjectRepository : IBaseRepository<Subject>
{
    public Task<bool> Update(string id, string title, string index, IReadOnlyCollection<string> teachersIds);
    public Task<Subject?> ReadByIndex(string index);
    public Task<bool> AddTeacherById(string id, string teacherId);
    public Task<bool> RemoveTeacherById(string id, string teacherId);
    public Task<bool> AddRangeTeacherById(string id, IEnumerable<string> teacherId);
    public Task<bool> RemoveRangeTeacherById(string id, IEnumerable<string> teacherId);

}
