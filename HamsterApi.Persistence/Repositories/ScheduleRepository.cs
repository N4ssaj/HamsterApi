using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Persistence.Entites.Interfaces;
using HamsterApi.Persistence.MappingExtensions;

namespace HamsterApi.Persistence.Repositories;

internal class ScheduleRepository : IScheduleRepository
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;

    public ScheduleRepository(HamsterApiDbContext hamsterApiDbContext)
        => (_hamsterApiDbContext) = (hamsterApiDbContext);

    public async Task<bool> AddGroup(string id, IEnumerable<string> groupId)
    {
        var item = await Read(id);
        if (item is null) return false;
        foreach (var i in groupId)
            if (!item.GroupsScheduleIds.Contains(i))
                item.Add(i);
        return await Update(id, item.Year, item.SpringOrAutumn, item.GroupsScheduleIds);
    }

    public async Task<string> Create(Schedule item)
    {
        var scheduleEntity = item.ToEntity();
        await Task.Run(() =>
        {
            _hamsterApiDbContext.ScheduleEntities.Add(scheduleEntity);
            _hamsterApiDbContext.SaveChanges();
        });
        return scheduleEntity.Id;
    }

    public async Task<bool> Delete(string id)
    {
        IScheduleEntity scheduleEntity = null;
        bool state = false;
        await Task.Run(() =>
        {
            scheduleEntity = _hamsterApiDbContext.ScheduleEntities.FirstOrDefault(a => a.Id == id)!;
            if (scheduleEntity is not null)
            {
                _hamsterApiDbContext.DeleteObject(scheduleEntity);
                _hamsterApiDbContext.SaveChanges();
                state = true;
            }
        });
        return state;
    }

    public async Task<Schedule?> Read(string id)
    {
        IScheduleEntity scheduleEntity = null;
        await Task.Run(() =>
        {
            scheduleEntity = _hamsterApiDbContext.ScheduleEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (scheduleEntity is null) return null;

        var schedule = scheduleEntity.ToModel();

        return schedule;
    }

    public async Task<List<Schedule>> ReadAll()
    {
        var scheduleEntityList = new List<IScheduleEntity>();
        await Task.Run(() =>
        {
            scheduleEntityList = _hamsterApiDbContext.ScheduleEntities.ToList();
        }
        );
        if (scheduleEntityList is null) return [];
        var scheduleList = scheduleEntityList.Select(a => a.ToModel()).ToList();

        return scheduleList;
    }

    public async Task<List<Schedule>> ReadByIds(IEnumerable<string> ids)
    {
        var scheduleEntityList = new List<IScheduleEntity>();
        await Task.Run(() =>
        {
            scheduleEntityList = _hamsterApiDbContext.ScheduleEntities
            .Where(g => ids.Contains(g.Id))
            .ToList();
        }
        );
        if (scheduleEntityList is null) return [];
        var scheduleList = scheduleEntityList.Select(a => a.ToModel()).ToList();

        return scheduleList;
    }

    public async Task<bool> RemoveGroup(string id, IEnumerable<string> groupId)
    {
        var item = await Read(id);
        if (item is null) return false;
        foreach (var i in groupId)
            if (item.GroupsScheduleIds.Contains(i))
                item.Remove(i);
        return await Update(id, item.Year,item.SpringOrAutumn,item.GroupsScheduleIds);
    }

    public async Task<bool> Update(string id, int year,SpringOrAutumn springOrAutumn,IReadOnlyCollection<string> groupsScheduleIds)
    {
        IScheduleEntity scheduleEntity = null;
        await Task.Run(() =>
        {
            scheduleEntity = _hamsterApiDbContext.ScheduleEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (scheduleEntity is null) return false;
        scheduleEntity.Year = year;
        scheduleEntity.SpringOrAutumn= springOrAutumn;
        scheduleEntity.GroupsScheduleIds = groupsScheduleIds.ToList();
        await Task.Run(() =>
        {
            _hamsterApiDbContext.SaveChanges();
        });
        return true;
    }
}
