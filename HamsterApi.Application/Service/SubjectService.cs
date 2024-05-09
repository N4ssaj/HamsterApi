using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface;
using HamsterApi.Domain.RepositoriesInterfaces;

namespace HamsterApi.Application.Service;

internal class SubjectService : ISubjectService
{
    private readonly ISubjectRepository _subjectRepository;

    public SubjectService(ISubjectRepository subjectRepository)
        =>_subjectRepository =subjectRepository;

    public async Task<bool> AddRangeTeacherById(string id, IEnumerable<string> teacherId)
        =>await _subjectRepository.AddRangeTeacherById(id, teacherId);

    public async Task<bool> AddTeacherById(string id, string teacherId)
        =>await _subjectRepository.AddTeacherById(id,teacherId);

    public async Task<string> Create(Subject item)
        =>await _subjectRepository.Create(item);

    public async Task<bool> Delete(string id)
        =>await (_subjectRepository.Delete(id));

    public async Task<Subject?> Read(string id)
        =>await _subjectRepository.Read(id);

    public async Task<List<Subject>> ReadAll()
        =>await _subjectRepository.ReadAll();

    public async Task<List<Subject>> ReadByIds(IEnumerable<string> ids)
        =>await _subjectRepository.ReadByIds(ids);

    public async Task<Subject?> ReadByIndex(string index)
        =>await _subjectRepository.ReadByIndex(index);

    public async Task<bool> RemoveRangeTeacherById(string id, IEnumerable<string> teacherId)
        => await _subjectRepository.RemoveRangeTeacherById(id, teacherId);

    public async Task<bool> RemoveTeacherById(string id, string teacherId)
        => await _subjectRepository.RemoveTeacherById(id, teacherId);

    public async Task<bool> Update(string id, string title, string index, IReadOnlyCollection<string> teachersIds)
        =>await _subjectRepository.Update(id,title,index, teachersIds);
}
