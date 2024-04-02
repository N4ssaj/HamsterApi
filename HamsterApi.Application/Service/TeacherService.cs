
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface;
using HamsterApi.Core.Stores;

namespace HamsterApi.Application.Service;

public class TeacherService : ITeacherService
{
    private readonly ITeacherStore _teacherStore;

    public TeacherService(ITeacherStore teacherStore)
        => _teacherStore = teacherStore;

    public async Task<bool> AddRangeSubjectById(string id, IEnumerable<string> subjectId)
        =>await _teacherStore.AddRangeSubjectById(id, subjectId);

    public async Task<bool> AddSubjectById(string id, string subjectId)
        =>await _teacherStore.AddSubjectById(id, subjectId);

    public async Task<string> Create(Teacher item)
        =>await _teacherStore.Create(item);

    public async Task<bool> Delete(string id)
        =>await _teacherStore.Delete(id);

    public async Task<Teacher?> Read(string id)
        =>await Read(id);

    public async Task<List<Teacher>> ReadAll()
        =>await _teacherStore.ReadAll();

    public async Task<List<Teacher>> ReadByIds(IEnumerable<string> ids)
        =>await _teacherStore.ReadByIds(ids);

    public async Task<bool> RemoveRangeSubjectById(string id, IEnumerable<string> subjectId)
        =>await _teacherStore.RemoveRangeSubjectById(id, subjectId);

    public async Task<bool> RemoveSubjectById(string id, string subjectId)
        => await _teacherStore.RemoveSubjectById(id, subjectId);

    public async Task<bool> Update(string id, string name, string surname, string patronymic, IReadOnlyCollection<string> subjectsIds, string chairId, string teacherLoadId)
        =>await Update(id, name, surname, patronymic, subjectsIds, chairId, teacherLoadId);
    
}
