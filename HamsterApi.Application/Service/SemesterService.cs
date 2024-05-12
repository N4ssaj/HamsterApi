
using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Domain.ServiceInterface;

namespace HamsterApi.Application.Service;

internal class SemesterService : ISemesterService
{
    private readonly ISemesterRepository _semesterRepository;

    public SemesterService(ISemesterRepository semesterRepository)
        =>_semesterRepository = semesterRepository;


    public async Task<bool> AddSubjects(string id, IEnumerable<SubjectWtihLoad> subjects)
        =>await _semesterRepository.AddSubjects(id, subjects);

    public async Task<string> Create(Semester item)
        =>await _semesterRepository.Create(item);

    public async Task<bool> Delete(string id)
        =>await _semesterRepository.Delete(id);

    public async Task<Semester?> Read(string id)
        =>await _semesterRepository.Read(id);

    public async Task<List<Semester>> ReadAll()
        =>await _semesterRepository.ReadAll();

    public async Task<List<Semester>> ReadByIds(IEnumerable<string> ids)
        =>await _semesterRepository.ReadByIds(ids);

    public async Task<bool> RemoveSubjects(string id, IEnumerable<string> subjectsIds)
        =>await _semesterRepository.RemoveSubjects(id, subjectsIds);

    public async Task<bool> Update(string id, int number, string groupId, List<SubjectWtihLoad> subjects)
        =>await _semesterRepository.Update(id, number, groupId, subjects);
}
