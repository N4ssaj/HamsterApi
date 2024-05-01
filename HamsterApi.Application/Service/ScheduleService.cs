
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface;
using HamsterApi.Core.Stores;

namespace HamsterApi.Application.Service;

public class ScheduleService:IScheduleService
{
    private readonly IScheduleStore _scheduleStore;

    public ScheduleService(IScheduleStore scheduleStore)
        => _scheduleStore = scheduleStore;

    public async Task<bool> AddClassOfWeek(string id, string groupId, string weekId, ScheduledClass scheduledClass)
        =>await _scheduleStore.AddClassOfWeek(id, groupId, weekId, scheduledClass);

    public async Task<bool> AddGroup(string id, ScheduleGroup group)
        =>await _scheduleStore.AddGroup(id, group);

    public async Task<bool> AddWeeksOfGroup(string id, string groupId, ScheduledClassOfWeeks scheduleClassOfWeeks)
        => await _scheduleStore.AddWeeksOfGroup(id, groupId, scheduleClassOfWeeks);

    public async Task<string> Create(Schedule item)
        =>await _scheduleStore.Create(item);

    public async Task<bool> Delete(string id)
        =>await _scheduleStore.Delete(id);

    public async Task<Schedule?> Read(string id)
        =>await _scheduleStore.Read(id);

    public async Task<List<Schedule>> ReadAll()
        =>await _scheduleStore.ReadAll();

    public async Task<List<Schedule>> ReadByIds(IEnumerable<string> ids)
        =>await _scheduleStore.ReadByIds(ids);

    public async Task<bool> RemoveClassOfWeek(string id, string groupId, string weekId, string classId)
        =>await _scheduleStore.RemoveClassOfWeek(id, groupId, weekId, classId);

    public async Task<bool> RemoveGroup(string id, string groupId)
        =>await _scheduleStore.RemoveGroup(id,groupId);

    public async Task<bool> RemoveWeeksOfGroup(string id, string groupId, string weekId)
        =>await _scheduleStore.RemoveWeeksOfGroup(id, groupId, weekId);

    public async Task<bool> Update(string id, int semesterNumber, IReadOnlyCollection<ScheduleGroup> groupsSchedule)
        =>await _scheduleStore.Update(id, semesterNumber, groupsSchedule);
}
