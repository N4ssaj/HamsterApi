using AutoMapper;
using HamsterApi.Core.Models;
using HamsterApi.Core.Stores;
using HamsterApi.DataAccess.Entites.Interfaces;
using HamsterApi.DataAccess.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HamsterApi.DataAccess.Repositories;

public class ScheduleRepository : IScheduleStore
{
    private readonly HamsterApiDbContext _hamsterApiDbContext;


    public ScheduleRepository(HamsterApiDbContext hamsterApiDbContext)
        => (_hamsterApiDbContext) = (hamsterApiDbContext);

    public async Task<bool> AddClassOfWeek(string id, string groupId, string weekId, ScheduledClass scheduledClass)
    {
        var item = await Read(id);
        if (item is null) return false;
        item.GroupsSchedule.FirstOrDefault(i=>i.Id==groupId)?.Weeks.FirstOrDefault(i=>i.Id==weekId)?.ScheduledClasses?.FirstOrDefault(s=>s.Id==id);
        return await Update(id,item.SemesterNumber,item.GroupsSchedule);
    }

    public async Task<bool> AddGroup(string id, ScheduleGroup group)
    {
        var item = await Read(id);
        if (item is null) return false;
        item.Add(group);
        return await Update(id, item.SemesterNumber, item.GroupsSchedule);
    }

    public async Task<bool> AddWeeksOfGroup(string id, string groupId, ScheduledClassOfWeeks scheduleClassOfWeeks)
    {
        var item = await Read(id);
        if (item is null) return false;
        item.GroupsSchedule.FirstOrDefault(i => i.Id == groupId)?.Weeks.Add(scheduleClassOfWeeks);
        Console.WriteLine($"{item.GroupsSchedule.FirstOrDefault(i => i.Id == groupId)?.Weeks.Count}");
        return await Update(id, item.SemesterNumber, item.GroupsSchedule);
    }

    public async Task<string> Create(Schedule item)
    {
        var scheduleEntity = Convertor.ToEntity(item);
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

        var schedule = Convertor.ToModel(scheduleEntity);

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
        var scheduleList = scheduleEntityList.Select(a => Convertor.ToModel(a)).ToList();

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
        var scheduleList = scheduleEntityList.Select(a => Convertor.ToModel(a)).ToList();

        return scheduleList;
    }

    public async Task<bool> RemoveClassOfWeek(string id, string groupId, string weekId, string classId)
    {
        var item = await Read(id);
        if (item is null) return false;
        var it=item.GroupsSchedule.FirstOrDefault(i => i.Id == groupId)?.Weeks.FirstOrDefault(i => i.Id == weekId)?.ScheduledClasses.FirstOrDefault(i=>i.Id == classId);
        if(it is null) return false;
        item.GroupsSchedule.FirstOrDefault(i => i.Id == groupId)?.Weeks.FirstOrDefault(i => i.Id == weekId)?.ScheduledClasses.Remove(it);
        return await Update(id, item.SemesterNumber, item.GroupsSchedule);
    }

    public async Task<bool> RemoveGroup(string id, string groupId)
    {
        var item = await Read(id);
        if (item is null) return false;
        var it=item.GroupsSchedule.FirstOrDefault(i => i.Id == groupId);
        if(it is null) return false;    
        item.Remove(it);
        return await Update(id, item.SemesterNumber, item.GroupsSchedule);
    }

    public async Task<bool> RemoveWeeksOfGroup(string id, string groupId, string weekId)
    {
        var item = await Read(id);
        if (item is null) return false;
        var it = item.GroupsSchedule.FirstOrDefault(i => i.Id == groupId)?.Weeks.FirstOrDefault(i => i.Id == weekId);
        if (it is null) return false;
        item.GroupsSchedule.FirstOrDefault(i => i.Id == groupId)?.Weeks.Remove(it);
        return await Update(id, item.SemesterNumber, item.GroupsSchedule);
    }

    public async Task<bool> Update(string id, int semesterNumber, IReadOnlyCollection<ScheduleGroup> groupsSchedule)
    {
        IScheduleEntity scheduleEntity = null;
        await Task.Run(() =>
        {
            scheduleEntity = _hamsterApiDbContext.ScheduleEntities.FirstOrDefault(a => a.Id == id)!;
        });
        if (scheduleEntity is null) return false;
        await Delete(id);
        await Create(Schedule.Create(id,semesterNumber,groupsSchedule.ToList()).Value);
        
        await Task.Run(() =>
        {
            _hamsterApiDbContext.SaveChanges();
        });
        return true;
    }
}
