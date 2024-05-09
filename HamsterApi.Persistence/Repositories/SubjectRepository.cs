using HamsterApi.Domain.Models;
using HamsterApi.Persistence.Entites.Interfaces;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Persistence.MappingExtensions;


namespace HamsterApi.Persistence.Repositories;

internal class SubjectRepository : ISubjectRepository
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;

    public SubjectRepository(HamsterApiDbContext hamsterApiDbContext)
            => _hamsterApiDbContext = hamsterApiDbContext;

    public async Task<bool> AddRangeTeacherById(string id, IEnumerable<string> teacherId)
    {
        var subjectCol = await ReadByIds([id]);
        var subject = subjectCol[0];
        foreach (var teacher in teacherId)
            if(!subject.TeachersIds.Contains(teacher))
                subject.AddTeacher(teacher);
        return await Update(subject.Id, subject.Title, subject.Index, subject.TeachersIds);
    }

    public async Task<bool> AddTeacherById(string id, string teacherId)
    {
        var subjectCol = await ReadByIds([id]);
        var subject = subjectCol[0];
        subject.AddTeacher(teacherId);
        return await Update(subject.Id, subject.Title, subject.Index, subject.TeachersIds);
    }

    public async Task<string> Create(Subject item)
    {
        var subjectEntity = item.ToEntity();
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
            subjectEntity = _hamsterApiDbContext.SubjectEntities.FirstOrDefault(a => a.Id == id)!;
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
            subjectEntity = _hamsterApiDbContext.SubjectEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (subjectEntity is null) return null;

        var subject = subjectEntity.ToModel();

        return subject;
    }

    public async Task<List<Subject>> ReadAll()
    {
        var subjectEntityList = new List<ISubjectEntity>();
        await Task.Run(() =>
        {
            subjectEntityList = _hamsterApiDbContext.SubjectEntities.ToList();
        }
        );
        if (subjectEntityList is null) return [];
        var subjectList = subjectEntityList.Select(a =>a.ToModel()).ToList();

        return subjectList;
    }

    public async Task<List<Subject>> ReadByIds(IEnumerable<string> ids)
    {
        var subjectEntityList = new List<ISubjectEntity>();
        await Task.Run(() =>
        {
            subjectEntityList = _hamsterApiDbContext.SubjectEntities
            .Where(g => ids.Contains(g.Id))
            .ToList();
        }
        );
        if (subjectEntityList is null) return [];
        var subjectList = subjectEntityList.Select(a => a.ToModel()).ToList();

        return subjectList;
    }

    public async Task<Subject?> ReadByIndex(string index)
    {
        ISubjectEntity subjectEntity = null;
        await Task.Run(() =>
        {
            subjectEntity = _hamsterApiDbContext.SubjectEntities.FirstOrDefault(a => a.Index == index)!;
        });
        if (subjectEntity is null) return null;

        var subject = subjectEntity.ToModel();

        return subject;
    }

    public async Task<bool> RemoveRangeTeacherById(string id, IEnumerable<string> teacherId)
    {
        var subjectCol = await ReadByIds([id]);
        var subject = subjectCol[0];
        foreach (var teacher in teacherId)
            if (subject.TeachersIds.Contains(teacher))
                subject.RemoveTeacher(teacher);
        return await Update(subject.Id, subject.Title, subject.Index, subject.TeachersIds);
    }

    public async Task<bool> RemoveTeacherById(string id, string teacherId)
    {
        var subjectCol = await ReadByIds([id]);
        var subject = subjectCol[0];
        subject.RemoveTeacher(teacherId);
        return await Update(subject.Id, subject.Title, subject.Index, subject.TeachersIds);
    }

    public async Task<bool> Update(string id, string title, string index, IReadOnlyCollection<string> teachersIds)
    {
        ISubjectEntity subjectEntity = null;
        await Task.Run(() =>
        {
            subjectEntity = _hamsterApiDbContext.SubjectEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (subjectEntity is null) return false;

        subjectEntity.Title = title;
        subjectEntity.Index = index;
        subjectEntity.TeachersIds = teachersIds.ToList();
        await Task.Run(() =>
        {
            _hamsterApiDbContext.SaveChanges();
        });
        return true;
    }
}
