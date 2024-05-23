using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;
using HamsterApi.Domain.RepositoriesInterfaces;
using HamsterApi.Persistence.Entites.Interfaces;
using HamsterApi.Persistence.MappingExtensions;
using static System.Runtime.InteropServices.JavaScript.JSType;



namespace HamsterApi.Persistence.Repositories;

internal class ScheduleGroupRepository:IScheduleGroupRepository
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;

    public ScheduleGroupRepository(HamsterApiDbContext hamsterApiDbContext)
        => (_hamsterApiDbContext) = (hamsterApiDbContext);

    public async Task<string> Create(ScheduleGroup item)
    {
        var groupEntity = item.ToEntity();
        await Task.Run(() =>
        {
            _hamsterApiDbContext.ScheduleGroupEntities.Add(groupEntity);
            _hamsterApiDbContext.SaveChanges();
        });
        return groupEntity.Id;
    }

    public async Task<bool> Delete(string id)
    {
        IScheduleGroupEntity scheduleGroupEntity = null;
        bool state = false;
        await Task.Run(() =>
        {
            scheduleGroupEntity = _hamsterApiDbContext.ScheduleGroupEntities.FirstOrDefault(a => a.Id == id)!;
            if (scheduleGroupEntity is not null)
            {
                _hamsterApiDbContext.DeleteObject(scheduleGroupEntity);
                _hamsterApiDbContext.SaveChanges();
                state = true;
            }
        });
        return state;
    }

    public async Task<ScheduleGroup?> Read(string id)
    {
        IScheduleGroupEntity scheduleGroupEntity = null;
        await Task.Run(() =>
        {
            scheduleGroupEntity = _hamsterApiDbContext.ScheduleGroupEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (scheduleGroupEntity is null) return null;

        var group = scheduleGroupEntity.ToModel();

        return group;
    }

    public async Task<List<ScheduleGroup>> ReadAll()
    {
        var scheduleGroupEntityList = new List<IScheduleGroupEntity>();
        await Task.Run(() =>
        {
            scheduleGroupEntityList = _hamsterApiDbContext.ScheduleGroupEntities.ToList();
        }
        );
        if (scheduleGroupEntityList is null) return [];
        var scheduleGroupList = scheduleGroupEntityList.Select(a => a.ToModel()).ToList();

        return scheduleGroupList;
    }

    public async Task<List<ScheduleGroup>> ReadByIds(IEnumerable<string> ids)
    {
        var scheduleGroupEntityList = new List<IScheduleGroupEntity>();
        await Task.Run(() =>
        {
            scheduleGroupEntityList = _hamsterApiDbContext.ScheduleGroupEntities
            .Where(g => ids.Contains(g.Id))
            .ToList();
        }
        );
        if (scheduleGroupEntityList is null) return [];
        var scheduleGroupList = scheduleGroupEntityList.Select(a => a.ToModel()).ToList();

        return scheduleGroupList;
    }

    public async Task<bool> Update(string id, string scheduleId, string groupId, int semesterNumber, List<ScheduledWeek> weeks)
    {
        IScheduleGroupEntity scheduleGroupEntity = null;
        await Task.Run(() =>
        {
            scheduleGroupEntity = _hamsterApiDbContext.ScheduleGroupEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (scheduleGroupEntity is null) return false;

        await Task.Run(() =>
        {
            _hamsterApiDbContext.DeleteObject(scheduleGroupEntity);
            var item = ScheduleGroup.Create(id,scheduleId,groupId,semesterNumber,weeks).Value;
            _hamsterApiDbContext.ScheduleGroupEntities.Add(item.ToEntity());
            _hamsterApiDbContext.SaveChanges();
        });
        return true;
    }
}
