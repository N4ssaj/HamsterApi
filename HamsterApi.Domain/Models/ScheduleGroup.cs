

using HamsterApi.Domain.Common;

namespace HamsterApi.Domain.Models;


public class ScheduleGroup
{

    private ScheduleGroup(string id, string scheduleId, string groupId, int semesterNumber, List<ScheduledWeek> weeks)
        => (Id, ScheduleId, GroupId, SemesterNumber, Weeks) = (id, scheduleId, groupId, semesterNumber, weeks);

    public string Id { get; }

    public string ScheduleId { get; }

    public string GroupId { get; }

    public int SemesterNumber { get; }

    public List<ScheduledWeek> Weeks { get; }

    public static Result<ScheduleGroup> Create(string id, string scheduleId, string group, int semesterNumber, List<ScheduledWeek> weeks)
    {
        var scheduleGroup = new ScheduleGroup(id, scheduleId, group, semesterNumber, weeks);
        return scheduleGroup;
    }
}

