using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface;
using HamsterApi.Domain.RepositoriesInterfaces;

namespace HamsterApi.Application.Service;

internal class TeacherService : ITeacherService
{
    private readonly ITeacherRepository _teacherRepository;

    public TeacherService(ITeacherRepository teacherRepository)
        => _teacherRepository = teacherRepository;

    public async Task<bool> AddChair(string id, string chairId)
        =>await _teacherRepository.AddChair(id, chairId);

    public async Task<bool> AddRangeSubjectById(string id, IEnumerable<string> subjectId)
        =>await _teacherRepository.AddRangeSubjectById(id, subjectId);

    public async Task<bool> AddSubjectById(string id, string subjectId)
        =>await _teacherRepository.AddSubjectById(id, subjectId);

    public async Task<string> Create(Teacher item)
        =>await _teacherRepository.Create(item);

    public async Task<bool> Delete(string id)
        =>await _teacherRepository.Delete(id);

    public async Task<Teacher?> Read(string id)
        =>await Read(id);

    public async Task<List<Teacher>> ReadAll()
        =>await _teacherRepository.ReadAll();

    public async Task<List<Teacher>> ReadByIds(IEnumerable<string> ids)
        =>await _teacherRepository.ReadByIds(ids);

    public async Task<bool> RemoveChair(string id)
        =>await _teacherRepository.RemoveChair(id);

    public async Task<bool> RemoveRangeSubjectById(string id, IEnumerable<string> subjectId)
        =>await _teacherRepository.RemoveRangeSubjectById(id, subjectId);

    public async Task<bool> RemoveSubjectById(string id, string subjectId)
        => await _teacherRepository.RemoveSubjectById(id, subjectId);

    public async Task<bool> Update(string id, string name, string surname, string patronymic, IReadOnlyCollection<string> subjectsIds, string chairId, string teacherLoadId)
        =>await Update(id, name, surname, patronymic, subjectsIds, chairId, teacherLoadId);
    
}
