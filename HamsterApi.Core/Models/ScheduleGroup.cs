

using HamsterApi.Core.Common;

namespace HamsterApi.Core.Models;


public class ScheduleGroup
{

    private ScheduleGroup(string id ,string group, int semesterNumber, List<ScheduledClassOfWeeks> weeks)
        => (Id, GroupId, SemesterNumber, _weeks) = (id, group, semesterNumber, weeks);

    public string Id { get; } = string.Empty;

    public string GroupId { get; }= string.Empty;

    public int SemesterNumber { get; }

    public IReadOnlyCollection<ScheduledClassOfWeeks> Weeks
        => _weeks;

    private List<ScheduledClassOfWeeks> _weeks=[];

    public void Add(ScheduledClassOfWeeks week)
        =>_weeks.Add(week);

    public void Remove(ScheduledClassOfWeeks week)
        =>_weeks.Remove(week);

    public static Result<ScheduleGroup> Create(string id , string group, int semesterNumber, List<ScheduledClassOfWeeks> weeks)
    {
        var scheduleGroup = new ScheduleGroup(id, group, semesterNumber, weeks);
        return scheduleGroup;
    }
}

