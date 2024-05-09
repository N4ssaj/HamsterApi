

using HamsterApi.Domain.Common;

namespace HamsterApi.Domain.Models;


public class Schedule
{

    private Schedule(string id, int semesterNumber, List<string> groupsScheduleIds)
        => (Id, SemesterNumber, _groupsIds) = (id, semesterNumber, groupsScheduleIds);

    public string Id { get; }

    public int SemesterNumber { get; }

    public IReadOnlyCollection<string> GroupsScheduleIds
        => _groupsIds;

    private List<string> _groupsIds;

    public void Add(string group)
        => _groupsIds.Add(group);

    public void Remove(string group)
        => _groupsIds.Remove(group);

    public static Result<Schedule> Create(string id, int semesterNumber, List<string> groupsScheduleIds)
    {
        var schedule = new Schedule(id, semesterNumber, groupsScheduleIds);
        return schedule;
    }
}

