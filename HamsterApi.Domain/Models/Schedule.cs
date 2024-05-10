using HamsterApi.Domain.Common;
using HamsterApi.Domain.Common.Enum;

namespace HamsterApi.Domain.Models;

public class Schedule
{

    private Schedule(string id, int year,SpringOrAutumn springOrAutumn,List<string> groupsScheduleIds)
        => (Id, Year,springOrAutumn, _groupsIds) = (id, year,springOrAutumn, groupsScheduleIds);

    public string Id { get; }

    public int Year { get; }

    public SpringOrAutumn SpringOrAutumn { get; }

    public IReadOnlyCollection<string> GroupsScheduleIds
        => _groupsIds;

    private List<string> _groupsIds;

    public void Add(string group)
        => _groupsIds.Add(group);

    public void Remove(string group)
        => _groupsIds.Remove(group);

    public static Result<Schedule> Create(string id, int year, SpringOrAutumn springOrAutumn, List<string> groupsScheduleIds)
    {
        var schedule = new Schedule(id,year,springOrAutumn,groupsScheduleIds);
        return schedule;
    }
}

