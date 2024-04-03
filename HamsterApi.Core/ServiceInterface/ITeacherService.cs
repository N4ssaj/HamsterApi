
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface.Base;

namespace HamsterApi.Core.ServiceInterface;

public interface ITeacherService:IBaseService<Teacher>
{
    public Task<bool> Update(string id, string name, string surname, string patronymic, IReadOnlyCollection<string> subjectsIds, string chairId, string teacherLoadId);
    public Task<bool> AddSubjectById(string id, string subjectId);
    public Task<bool> RemoveSubjectById(string id, string subjectId);
    public Task<bool> AddRangeSubjectById(string id, IEnumerable<string> subjectId);
    public Task<bool> RemoveRangeSubjectById(string id, IEnumerable<string> subjectId);
    public Task<bool> AddChair(string id, string chairId);
    public Task<bool> RemoveChair(string id);
}
