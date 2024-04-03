using AutoMapper;
using HamsterApi.Core.Common.Enum;
using HamsterApi.Core.Models;
using HamsterApi.Core.Stores;
using HamsterApi.DataAccess.Entites.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HamsterApi.DataAccess.Repositories;

public class DirectionRepository : IDirectionStore
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;

    private readonly IMapper _mapper;

    public DirectionRepository(HamsterApiDbContext hamsterApiDbContext, IMapper mapper)
            => (_hamsterApiDbContext, _mapper) = (hamsterApiDbContext, mapper);

    public async Task<bool> AddGroupById(string id, string groupId)
    {
        var direction = await Read(id);
        if (direction is null) return false;
        direction.AddGroup(groupId);
        return await Update(direction.Id, direction.Title, direction.GroupsIds, direction.FormOfEducation, direction.LevelOfEducation, direction.DepartmentId);
    }

    public async Task<bool> AddRangeGroupById(string id, IEnumerable<string> groupId)
    {
        var direction = await Read(id);
        if (direction is null) return false;
        foreach (var group in groupId)
            if (!direction.GroupsIds.Contains(group))
                direction.AddGroup(group);
        return await Update(direction.Id, direction.Title, direction.GroupsIds, direction.FormOfEducation, direction.LevelOfEducation, direction.DepartmentId);
    }

    public async Task<string> Create(Direction item)
    {
        var directionEntity = _mapper.Map<DirectionEntity>(item);
        await Task.Run(() =>
        {
            _hamsterApiDbContext.DirectionEntities.Add(directionEntity);
            _hamsterApiDbContext.SaveChanges();
        });
        return directionEntity.Id;
    }

    public async Task<bool> Delete(string id)
    {
        IDirectionEntity directionEntity = null;
        bool state = false;
        await Task.Run(() =>
        {
            directionEntity = _hamsterApiDbContext.DirectionEntities.FirstOrDefault(a => a.Id == id)!;
            if (directionEntity is not null)
            {
                _hamsterApiDbContext.DeleteObject(directionEntity);
                _hamsterApiDbContext.SaveChanges();
                state = true;
            }
        });
        return state;
    }

    public async Task<Direction?> Read(string id)
    {
        IDirectionEntity directionEntity = null;
        await Task.Run(() =>
        {
            directionEntity = _hamsterApiDbContext.DirectionEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (directionEntity is null) return null;

        var direction = _mapper.Map<Direction>(directionEntity);

        return direction;
    }

    public async Task<List<Direction>> ReadAll()
    {
        var directionEntityList = new List<IDirectionEntity>();
        await Task.Run(() =>
        {
            directionEntityList = _hamsterApiDbContext.DirectionEntities.ToList();
        }
        );
        if (directionEntityList is null) return [];
        var directionList = directionEntityList.Select(a => _mapper.Map<Direction>(a)).ToList();

        return directionList;
    }

    public async Task<List<Direction>> ReadByIds(IEnumerable<string> ids)
    {
        var directionEntityList = new List<IDirectionEntity>();
        await Task.Run(() =>
        {
            directionEntityList = _hamsterApiDbContext.DirectionEntities
            .Where(g => ids.Contains(g.Id))
            .ToList();
        }
        );
        if (directionEntityList is null) return [];
        var directionList = directionEntityList.Select(a => _mapper.Map<Direction>(a)).ToList();

        return directionList;
    }

    public async Task<bool> RemoveGroupById(string id, string groupId)
    {
        var direction = await Read(id);
        if (direction is null) return false;
        direction.RemvoveGroup(groupId);
        return await Update(direction.Id, direction.Title, direction.GroupsIds, direction.FormOfEducation, direction.LevelOfEducation, direction.DepartmentId);
    }

    public async Task<bool> RemoveRangeGroupById(string id, IEnumerable<string> groupId)
    {
        var direction = await Read(id);
        if (direction is null) return false;
        foreach (var group in groupId)
            if (direction.GroupsIds.Contains(group))
                direction.RemvoveGroup(group);
        return await Update(direction.Id, direction.Title, direction.GroupsIds, direction.FormOfEducation, direction.LevelOfEducation, direction.DepartmentId);
    }

    public async Task<bool> Update(string id, string title, IReadOnlyCollection<string> groupsIds, FormOfEducation formOfEducation, LevelOfEducation levelOfEducation, string departmentId)
    {
        IDirectionEntity directionEntity = null;
        await Task.Run(() =>
        {
            directionEntity = _hamsterApiDbContext.DirectionEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (directionEntity is null) return false;

        directionEntity.Title = title;
        directionEntity.GroupsIds=groupsIds.ToList();
        directionEntity.FormOfEducation= formOfEducation;
        directionEntity.LevelOfEducation= levelOfEducation;
        directionEntity.DepartmentId= departmentId;

        await Task.Run(() =>
        {
            _hamsterApiDbContext.SaveChanges();
        });
        return true;
    }
}
