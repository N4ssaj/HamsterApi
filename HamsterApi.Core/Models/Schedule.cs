

using HamsterApi.Core.Common;

namespace HamsterApi.Core.Models;


public class Schedule
{

    private Schedule(string id , int semesterNumber, List<ScheduleGroup> groupsSchedule)
        => (Id, SemesterNumber, _groups) = (id, semesterNumber, groupsSchedule);

    public string Id { get; }=string.Empty;

    public int SemesterNumber { get; }

    public IReadOnlyCollection<ScheduleGroup> GroupsSchedule
        => _groups;

    private List<ScheduleGroup> _groups=[];

    public void Add(ScheduleGroup group)
        =>_groups.Add(group);
    public void Remove(ScheduleGroup group)
        => _groups.Remove(group);

    public static Result<Schedule> Create(string id , int semesterNumber, List<ScheduleGroup> groupsSchedule)
    {
        var schedule = new Schedule(id, semesterNumber, groupsSchedule);
        return schedule;
    }
}

