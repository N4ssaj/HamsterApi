

using HamsterApi.Core.Common;

namespace HamsterApi.Core.Models;


public class ScheduleGroup
{

    public ScheduleGroup(string id ,string groupId, int semesterNumber, List<ScheduledClassOfWeeks> weeks)
        => (Id, GroupId, SemesterNumber, Weeks) = (id, groupId, semesterNumber, weeks);

    public string Id { get; } 

    public string GroupId { get; }

    public int SemesterNumber { get; }

    public List<ScheduledClassOfWeeks> Weeks { get; } 

    public static Result<ScheduleGroup> Create(string id , string group, int semesterNumber, List<ScheduledClassOfWeeks> weeks)
    {
        var scheduleGroup = new ScheduleGroup(id, group, semesterNumber, weeks);
        return scheduleGroup;
    }
}

