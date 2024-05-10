using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface.Base;

namespace HamsterApi.Domain.ServiceInterface;

public interface ISemesterService:IBaseService<Semester>
{
    public Task<bool> Update(string id, int number, string groupId, List<SubjectWtihLoad> subjects);
    public Task<bool> AddSubjects(string id, IEnumerable<SubjectWtihLoad> subjects);
    public Task<bool> RemoveSubjects(string id, IEnumerable<SubjectWtihLoad> subjects);
}
