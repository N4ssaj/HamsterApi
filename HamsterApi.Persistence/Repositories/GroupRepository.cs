using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Persistence.Entites.Interfaces;
using HamsterApi.Persistence.MappingExtensions;


namespace HamsterApi.Persistence.Repositories;

internal class GroupRepository : IGroupRepository
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;


    public GroupRepository(HamsterApiDbContext hamsterApiDbContext)
        => _hamsterApiDbContext = hamsterApiDbContext;

    public async Task<bool> AddDirection(string id, string directionId)
    {
        var group = await Read(id);
        if (group is null) return false;
        return await Update(id, group.Number, group.LevelOfEducation, directionId);
    }

    public async Task<string> Create(Group item)
    {
        var groupEntity = item.ToEntity();
        await Task.Run(() =>
        {
            _hamsterApiDbContext.GroupEntities.Add(groupEntity);
            _hamsterApiDbContext.SaveChanges();
        });
        return groupEntity.Id;
    }

    public async Task<bool> Delete(string id)
    {
        IGroupEntity groupEntity = null;
        bool state = false;
        await Task.Run(() =>
        {
            groupEntity = _hamsterApiDbContext.GroupEntities.FirstOrDefault(a => a.Id == id)!;
            if (groupEntity is not null)
            {
                _hamsterApiDbContext.DeleteObject(groupEntity);
                _hamsterApiDbContext.SaveChanges();
                state = true;
            }
        });
        return state;
    }

    public async Task<Group?> Read(string id)
    {
        IGroupEntity groupEntity = null;
        await Task.Run(() =>
        {
            groupEntity = _hamsterApiDbContext.GroupEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (groupEntity is null) return null;

        var group = groupEntity.ToModel();

        return group;
    }

    public async Task<List<Group>> ReadAll()
    {
        var groupEntityList = new List<IGroupEntity>();
        await Task.Run(() =>
        {
            groupEntityList = _hamsterApiDbContext.GroupEntities.ToList();
        }
        );
        if (groupEntityList is null) return [];
        var groupList = groupEntityList.Select(a => a.ToModel()).ToList();

        return groupList;
    }

    public async Task<List<Group>> ReadByIds(IEnumerable<string> ids)
    {
        var groupEntityList = new List<IGroupEntity>();
        await Task.Run(() =>
        {
            groupEntityList = _hamsterApiDbContext.GroupEntities
            .Where(g=>ids.Contains(g.Id))
            .ToList();
        }
        );
        if (groupEntityList is null) return [];
        var groupList = groupEntityList.Select(a => a.ToModel()).ToList();

        return groupList;
    }

    public async Task<Group?> ReadByNumber(string number)
    {
        IGroupEntity groupEntity = null;
        await Task.Run(() =>
        {
            groupEntity = _hamsterApiDbContext.GroupEntities.FirstOrDefault(a => a.Number == number)!;
        });
        if (groupEntity is null) return null;

        var group = groupEntity.ToModel();

        return group;
    }

    public async Task<bool> RemoveDirection(string id)
    {
        var group = await Read(id);
        if (group is null) return false;
        return await Update(id, group.Number, group.LevelOfEducation, string.Empty);
    }

    public async Task<bool> Update(string id, string number, LevelOfEducation levelOfEducation,string directionId)
    {
        IGroupEntity groupEntity = null;
        await Task.Run(() =>
        {
            groupEntity = _hamsterApiDbContext.GroupEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (groupEntity is null) return false;

        groupEntity.Number = number;
        groupEntity.LevelOfEducation= levelOfEducation;
        groupEntity.DirectionId = directionId;
        await Task.Run(() =>
        {
            _hamsterApiDbContext.SaveChanges();
        });
        return true;
    }
}
