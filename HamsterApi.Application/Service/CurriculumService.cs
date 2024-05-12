using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Domain.ServiceInterface;

namespace HamsterApi.Application.Service;

internal class CurriculumService : ICurriculumService
{
    private readonly ICurriculumRepository _curriculumRepository;

    public CurriculumService(ICurriculumRepository curriculumRepository)
        => _curriculumRepository = curriculumRepository;


    public async Task<bool> AddElectives(string id, IEnumerable<SubjectWtihLoad> subjectWtihLoads)
        =>await _curriculumRepository.AddElectives(id, subjectWtihLoads);

    public async Task<bool> AddSubject(string id, IEnumerable<SubjectWtihLoad> subjectWtihLoads)
        =>await _curriculumRepository.AddSubject(id, subjectWtihLoads); 

    public async Task<string> Create(Curriculum item)
        =>await _curriculumRepository.Create(item);

    public async Task<bool> Delete(string id)
        =>await _curriculumRepository.Delete(id);

    public async Task<Curriculum?> Read(string id)
        =>await _curriculumRepository.Read(id);

    public Task<List<Curriculum>> ReadAll()
        => _curriculumRepository.ReadAll();

    public async Task<List<Curriculum>> ReadByIds(IEnumerable<string> ids)
        =>await _curriculumRepository.ReadByIds(ids);

    public async Task<bool> RemoveElectives(string id, IEnumerable<string> subjectIds)
        =>await _curriculumRepository.RemoveElectives(id, subjectIds);

    public async Task<bool> RemoveSubject(string id, IEnumerable<string> subjectIds)
        =>await _curriculumRepository.RemoveSubject(id, subjectIds);

    public async Task<bool> Update(string id, string chairId, string departmentId, string directionId, List<SubjectWtihLoad> curriculumsSubjects, List<SubjectWtihLoad> curriculumsElectiveSubjects, int yearOfPreparation, string fGOSNumber)
        =>await _curriculumRepository.Update(id, chairId, departmentId, directionId, curriculumsSubjects, curriculumsElectiveSubjects, yearOfPreparation, fGOSNumber);
}
