
using HamsterApi.Core.Common.Enum;
using HamsterApi.Core.Models;
using HamsterApi.Core.Stores;
using HamsterApi.DataAccess.Entites.Interfaces;

namespace HamsterApi.DataAccess.Repositories;

public class GroupRepository : IGroupStore
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;

    public GroupRepository(HamsterApiDbContext hamsterApiDbContext)
        => _hamsterApiDbContext = hamsterApiDbContext;

    public async Task<string> Create(Group item)
    {
        var groupEntity = new GroupEntity()
        { Id = item.Id, Number = item.Number, LevelOfEducation=item.LevelOfEducation };
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
            groupEntity = _hamsterApiDbContext.GroupEntities.FirstOrDefault(a => a.Id == id);
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
            groupEntity = _hamsterApiDbContext.GroupEntities.FirstOrDefault(a => a.Id == id);
        });
        if (groupEntity is null) return null;

        var group = Group.Create(groupEntity.Id, groupEntity.Number,groupEntity.LevelOfEducation);

        return group.Value;
    }

    public async Task<List<Group>?> ReadAll()
    {
        var groupEntityList = new List<IGroupEntity>();
        await Task.Run(() =>
        {
            groupEntityList = _hamsterApiDbContext.GroupEntities.ToList();
        }
        );
        var groupList = groupEntityList.Select(a => Group.Create(a.Id, a.Number, a.LevelOfEducation).Value).ToList();

        return groupList;
    }

    public async Task<Group?> ReadByNumber(string number)
    {
        IGroupEntity groupEntity = null;
        await Task.Run(() =>
        {
            groupEntity = _hamsterApiDbContext.GroupEntities.FirstOrDefault(a => a.Number == number);
        });
        if (groupEntity is null) return null;

        var group = Group.Create(groupEntity.Id, groupEntity.Number, groupEntity.LevelOfEducation);

        return group.Value;
    }

    public async Task<bool> Update(string id, string number, LevelOfEducation levelOfEducation)
    {
        IGroupEntity groupEntity = null;
        await Task.Run(() =>
        {
            groupEntity = _hamsterApiDbContext.GroupEntities.FirstOrDefault(a => a.Id == id);
        });
        if (groupEntity is null) return false;

        groupEntity.Number = number;
        groupEntity.LevelOfEducation= levelOfEducation;

        await Task.Run(() =>
        {
            _hamsterApiDbContext.SaveChanges();
        });
        return true;
    }
}
