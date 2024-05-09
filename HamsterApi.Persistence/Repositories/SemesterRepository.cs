using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Persistence.Entites.Interfaces;
using HamsterApi.Persistence.MappingExtensions;


namespace HamsterApi.Persistence.Repositories;

internal class SemesterRepository : ISemesterRepository
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;

    public SemesterRepository(HamsterApiDbContext hamsterApiDbContext)
            => _hamsterApiDbContext = hamsterApiDbContext;

    public async Task<bool> AddSubjects(string id, IEnumerable<SubjectWtihLoad> subjects)
    {
        var item=await Read(id);
        if (item != null) return false;
        foreach (var subject in subjects)
            item.Add(subject);
        return await Update(id, item.Number,item.GroupId, item.Subjects.ToList());
    }

    public async Task<string> Create(Semester item)
    {
        var semesterEntity = item.ToEntity();
        await Task.Run(() =>
        {
            _hamsterApiDbContext.SemesterEntities.Add(semesterEntity);
            _hamsterApiDbContext.SaveChanges();
        });
        return semesterEntity.Id;
    }

    public async Task<bool> Delete(string id)
    {
        ISemesterEntity semesterEntity = null;
        bool state = false;
        await Task.Run(() =>
        {
            semesterEntity = _hamsterApiDbContext.SemesterEntities.FirstOrDefault(a => a.Id == id)!;
            if (semesterEntity is not null)
            {
                _hamsterApiDbContext.DeleteObject(semesterEntity);
                _hamsterApiDbContext.SaveChanges();
                state = true;
            }
        });
        return state;
    }

    public async Task<Semester?> Read(string id)
    {
        ISemesterEntity semesterEntity = null;
        await Task.Run(() =>
        {
            semesterEntity = _hamsterApiDbContext.SemesterEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (semesterEntity is null) return null;

        var semester = semesterEntity.ToModel();

        return semester;
    }

    public async Task<List<Semester>> ReadAll()
    {
        var semesterEntityList = new List<ISemesterEntity>();
        await Task.Run(() =>
        {
            semesterEntityList = _hamsterApiDbContext.SemesterEntities.ToList();
        }
        );
        if (semesterEntityList is null) return [];
        var semesterList = semesterEntityList.Select(a => a.ToModel()).ToList();

        return semesterList;
    }

    public async Task<List<Semester>> ReadByIds(IEnumerable<string> ids)
    {
        var semesterEntityList = new List<ISemesterEntity>();
        await Task.Run(() =>
        {
            semesterEntityList = _hamsterApiDbContext.SemesterEntities
            .Where(g => ids.Contains(g.Id))
            .ToList();
        }
        );
        if (semesterEntityList is null) return [];
        var semesterList = semesterEntityList.Select(a => a.ToModel()).ToList();

        return semesterList;
    }

    public async Task<bool> RemoveSubjects(string id, IEnumerable<SubjectWtihLoad> subjects)
    {
        var item = await Read(id);
        if (item != null) return false;
        foreach (var subject in subjects)
            item.Remove(subject);
        return await Update(id, item.Number, item.GroupId, item.Subjects.ToList());
    }

    public async Task<bool> Update(string id, int number, string groupId, List<SubjectWtihLoad> subjects)
    {
        ISemesterEntity semesterEntity = null;
        await Task.Run(() =>
        {
            semesterEntity = _hamsterApiDbContext.SemesterEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (semesterEntity is null) return false;

        semesterEntity.Number = number;
        semesterEntity.GroupId = groupId;
        semesterEntity.Subjects = subjects.Select(i => i.ToEntity()).ToList();
        await Task.Run(() =>
        {
            _hamsterApiDbContext.SaveChanges();
        });
        return true;
    }
}
