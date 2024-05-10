
using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Domain.ServiceInterface;

namespace HamsterApi.Application.Service;

internal class SemesterService : ISemesterService
{
    private readonly ISemesterRepository _semesterRepository;



    public Task<bool> AddSubjects(string id, IEnumerable<SubjectWtihLoad> subjects)
    {
        throw new NotImplementedException();
    }

    public Task<string> Create(Semester item)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Semester?> Read(string id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Semester>> ReadAll()
    {
        throw new NotImplementedException();
    }

    public Task<List<Semester>> ReadByIds(IEnumerable<string> ids)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveSubjects(string id, IEnumerable<SubjectWtihLoad> subjects)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(string id, int number, string groupId, List<SubjectWtihLoad> subjects)
    {
        throw new NotImplementedException();
    }
}
