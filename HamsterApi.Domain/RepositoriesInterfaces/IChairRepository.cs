using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces.Base;

namespace HamsterApi.Domain.RepositoriesInterfaces;

public interface IChairRepository : IBaseRepository<Chair>
{
    public Task<bool> Update(string id, string title, IReadOnlyCollection<string> teachersIds, string departmentId);
    public Task<bool> AddTeacherById(string id, string teacherId);
    public Task<bool> RemoveTeacherById(string id, string teacherId);
    public Task<bool> AddRangeTeacherById(string id, IEnumerable<string> teacherId);
    public Task<bool> RemoveRangeTeacherById(string id, IEnumerable<string> teacherId);
    public Task<bool> AddDepartment(string id, string departmentId);
    public Task<bool> RemoveDepartment(string id);
}
