
using HamsterApi.Core.Models;
using HamsterApi.Core.Stores;
using HamsterApi.DataAccess.Entites.Interfaces;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HamsterApi.DataAccess.Repositories;

public class SubjectRepository : ISubjectStore
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;

    public SubjectRepository(HamsterApiDbContext hamsterApiDbContext)
            => _hamsterApiDbContext = hamsterApiDbContext;

    public async Task<string> Create(Subject item)
    {
        var teacherListId=item.Teachers.Select(t => t.Id).ToList();

        var teacher = _hamsterApiDbContext.TeacherEntities.Where(t => teacherListId.Contains(t.Id)).ToList();

        var subjectEntity = new SubjectEntity()
        { Id = item.Id,
         Index=item.Index,
         Teachers=teacher,
         Title=item.Title};
        await Task.Run(() =>
        {
            _hamsterApiDbContext.SubjectEntities.Add(subjectEntity);
            _hamsterApiDbContext.SaveChanges();
        });
        return subjectEntity.Id;
    }

    public async Task<bool> Delete(string id)
    {
        ISubjectEntity subjectEntity = null;
        bool state = false;
        await Task.Run(() =>
        {
            subjectEntity = _hamsterApiDbContext.SubjectEntities.FirstOrDefault(a => a.Id == id);
            if (subjectEntity is not null)
            {
                _hamsterApiDbContext.DeleteObject(subjectEntity);
                _hamsterApiDbContext.SaveChanges();
                state = true;
            }
        });
        return state;
    }

    public async Task<Subject?> Read(string id)
    {
        ISubjectEntity subjectEntity = null;
        await Task.Run(() =>
        {
            subjectEntity = _hamsterApiDbContext.SubjectEntities.FirstOrDefault(a => a.Id == id);
        });
        if (subjectEntity is null) return null;

        var subject = Subject.Create(subjectEntity.Id, subjectEntity.Number);

        return subject.Value;
    }

    public async Task<List<Subject>?> ReadAll()
    {
        var subjectEntityList = new List<ISubjectEntity>();
        await Task.Run(() =>
        {
            subjectEntityList = _hamsterApiDbContext.SubjectEntities.ToList();
        }
        );
        var subjectList = subjectEntityList.Select(a => Subject.Create(a.Id, a.Number).Value).ToList();

        return subjectList;
    }

    public async Task<Group?> ReadByIndex(string index)
    {
        ISubjectEntity subjectEntity = null;
        await Task.Run(() =>
        {
            subjectEntity = _hamsterApiDbContext.SubjectEntities.FirstOrDefault(a => a.Index == index);
        });
        if (subjectEntity is null) return null;

        var subject = Subject.Create(subjectEntity.Id, subjectEntity.Number);

        return subject.Value;
    }

    public async Task<bool> Update(string id, string title, string index, ICollection<Teacher> teachers)
    {
        ISubjectEntity subjectEntity = null;
        await Task.Run(() =>
        {
            subjectEntity = _hamsterApiDbContext.SubjectEntities.FirstOrDefault(a => a.Id == id);
        });
        if (subjectEntity is null) return false;

        var teacherListId = teachers.Select(t => t.Id).ToList();

        var teacher = _hamsterApiDbContext.TeacherEntities.Where(t => teacherListId.Contains(t.Id)).ToList();

        subjectEntity.Title = title;
        subjectEntity.Index = index;
        subjectEntity.Teachers = teacher ;
        await Task.Run(() =>
        {
            _hamsterApiDbContext.SaveChanges();
        });
        return true;
    }
}
