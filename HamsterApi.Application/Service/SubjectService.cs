
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface;
using HamsterApi.Core.Stores;

namespace HamsterApi.Application.Service;

public class SubjectService : ISubjectService
{
    private readonly ISubjectStore _subjectStore;

    public SubjectService(ISubjectStore subjectStore)
        =>_subjectStore =subjectStore;

    public async Task<bool> AddRangeTeacherById(string id, IEnumerable<string> teacherId)
        =>await _subjectStore.AddRangeTeacherById(id, teacherId);

    public async Task<bool> AddTeacherById(string id, string teacherId)
        =>await _subjectStore.AddTeacherById(id,teacherId);

    public async Task<string> Create(Subject item)
        =>await _subjectStore.Create(item);

    public async Task<bool> Delete(string id)
        =>await (_subjectStore.Delete(id));

    public async Task<Subject?> Read(string id)
        =>await _subjectStore.Read(id);

    public async Task<List<Subject>> ReadAll()
        =>await _subjectStore.ReadAll();

    public async Task<List<Subject>> ReadByIds(IEnumerable<string> ids)
        =>await _subjectStore.ReadByIds(ids);

    public async Task<Subject?> ReadByIndex(string index)
        =>await _subjectStore.ReadByIndex(index);

    public async Task<bool> RemoveRangeTeacherById(string id, IEnumerable<string> teacherId)
        => await _subjectStore.RemoveRangeTeacherById(id, teacherId);

    public async Task<bool> RemoveTeacherById(string id, string teacherId)
        => await _subjectStore.RemoveTeacherById(id, teacherId);

    public async Task<bool> Update(string id, string title, string index, IReadOnlyCollection<string> teachersIds)
        =>await _subjectStore.Update(id,title,index, teachersIds);
}
