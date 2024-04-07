
using AutoMapper;
using HamsterApi.Core.Models;
using HamsterApi.Core.Stores;
using HamsterApi.DataAccess.Entites.Interfaces;
using System;

namespace HamsterApi.DataAccess.Repositories;

public class ChairRepository : IChairStore
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;

    private readonly IMapper _mapper;

    public ChairRepository(HamsterApiDbContext hamsterApiDbContext, IMapper mapper)
            => (_hamsterApiDbContext, _mapper) = (hamsterApiDbContext, mapper);

    public async Task<bool> AddDepartment(string id, string departmentId)
    {
        var chair = await Read(id);
        if (chair is null) return false;
        return await Update(id,chair.Title,chair.TeachersIds,departmentId);
    }

    public async Task<bool> AddRangeTeacherById(string id, IEnumerable<string> teacherId)
    {
        var chair = await Read(id);
        if (chair is null) return false;
        foreach (var teacher in teacherId)
            if (!chair.TeachersIds.Contains(teacher))
                chair.AddTeacher(teacher);
        return await Update(chair.Id,chair.Title,chair.TeachersIds,chair.DepartmentId);
    }

    public async Task<bool> AddTeacherById(string id, string teacherId)
    {
        var chair = await Read(id);
        if (chair is null) return false;
        chair.AddTeacher(teacherId);

        return await Update(chair.Id, chair.Title, chair.TeachersIds, chair.DepartmentId);
    }

    public async Task<string> Create(Chair item)
    {
        var chairEntity = _mapper.Map<ChairEntity>(item);
        await Task.Run(() =>
        {
            _hamsterApiDbContext.ChairEntities.Add(chairEntity);
            _hamsterApiDbContext.SaveChanges();
        });
        return chairEntity.Id;
    }

    public async Task<bool> Delete(string id)
    {
        IChairEntity chairEntity = null;
        bool state = false;
        await Task.Run(() =>
        {
            chairEntity = _hamsterApiDbContext.ChairEntities.FirstOrDefault(a => a.Id == id)!;
            if (chairEntity is not null)
            {
                _hamsterApiDbContext.DeleteObject(chairEntity);
                _hamsterApiDbContext.SaveChanges();
                state = true;
            }
        });
        return state;
    }

    public async Task<Chair?> Read(string id)
    {
        IChairEntity chairEntity = null;
        await Task.Run(() =>
        {
            chairEntity = _hamsterApiDbContext.ChairEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (chairEntity is null) return null;

        var chair = _mapper.Map<Chair>(chairEntity);

        return chair;
    }

    public async Task<List<Chair>> ReadAll()
    {
        var chairEntityList = new List<IChairEntity>();
        await Task.Run(() =>
        {
            chairEntityList = _hamsterApiDbContext.ChairEntities.ToList();
        }
        );
        if (chairEntityList is null) return [];
        var chairList = chairEntityList.Select(a => _mapper.Map<Chair>(a)).ToList();

        return chairList;
    }

    public async Task<List<Chair>> ReadByIds(IEnumerable<string> ids)
    {
        var chairEntityList = new List<IChairEntity>();
        await Task.Run(() =>
        {
            chairEntityList = _hamsterApiDbContext.ChairEntities
            .Where(g => ids.Contains(g.Id))
            .ToList();
        }
        );
        if (chairEntityList is null) return [];
        var chairList = chairEntityList.Select(a => _mapper.Map<Chair>(a)).ToList();

        return chairList;
    }

    public async Task<bool> RemoveDepartment(string id)
    {
        var chair = await Read(id);
        if (chair is null) return false;
        return await Update(id, chair.Title, chair.TeachersIds,string.Empty);
    }

    public async Task<bool> RemoveRangeTeacherById(string id, IEnumerable<string> teacherId)
    {
        var chair = await Read(id);
        if (chair is null) return false;
        foreach (var teacher in teacherId)
            if (chair.TeachersIds.Contains(teacher))
                chair.RemoveTeacher(teacher);
        return await Update(chair.Id, chair.Title, chair.TeachersIds, chair.DepartmentId);
    }

    public async Task<bool> RemoveTeacherById(string id, string teacherId)
    {
        var chair = await Read(id);
        if (chair is null) return false;
        chair.RemoveTeacher(teacherId);
        return await Update(chair.Id, chair.Title, chair.TeachersIds, chair.DepartmentId);
    }

    public async Task<bool> Update(string id, string title, IReadOnlyCollection<string> teachersIds, string departmentId)
    {
        IChairEntity chairEntity = null;
        await Task.Run(() =>
        {
            chairEntity = _hamsterApiDbContext.ChairEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (chairEntity is null) return false;
        chairEntity.Title = title;
        chairEntity.TeachersIds = teachersIds.ToList();
        chairEntity.DepartmentId = departmentId;
        await Task.Run(() =>
        {
            _hamsterApiDbContext.SaveChanges();
        });
        return true;
    }
}
