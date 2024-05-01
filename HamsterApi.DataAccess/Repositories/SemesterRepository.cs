
using AutoMapper;
using HamsterApi.Core.Models;
using HamsterApi.Core.Stores;
using HamsterApi.DataAccess.Entites.Interfaces;
using System;

namespace HamsterApi.DataAccess.Repositories;

public class SemesterRepository : ISemesterStore
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;
    private readonly IMapper _mapper;

    public SemesterRepository(HamsterApiDbContext hamsterApiDbContext, IMapper mapper)
            => (_hamsterApiDbContext, _mapper) = (hamsterApiDbContext, mapper);

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
        var semesterEntity = _mapper.Map<SemesterEntity>(item);
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

        var semester = _mapper.Map<Semester>(semesterEntity);

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
        var semesterList = semesterEntityList.Select(a => _mapper.Map<Semester>(a)).ToList();

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
        var semesterList = semesterEntityList.Select(a => _mapper.Map<Semester>(a)).ToList();

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
        semesterEntity.Subjects = subjects.Select(i => _mapper.Map<ISubjectWtihLoadEntity>(i)).ToList();
        await Task.Run(() =>
        {
            _hamsterApiDbContext.SaveChanges();
        });
        return true;
    }
}
