
using HamsterApi.Domain.Common;

namespace HamsterApi.Domain.Models;

public class ScheduledWeek
{
    private ScheduledWeek(string id, int weekNumber, List<ScheduledClassOfWeeks> classOfWeeks)
       => (Id, WeekNumber, _classOfWeeks) = (id, weekNumber, classOfWeeks);

    public string Id { get; }

    public int WeekNumber { get; }

    private List<ScheduledClassOfWeeks> _classOfWeeks;

    public IReadOnlyCollection<ScheduledClassOfWeeks> ClassOfWeeks
        => _classOfWeeks;

    public void Add(ScheduledClassOfWeeks week)
        => _classOfWeeks.Add(week);

    public void Remove(ScheduledClassOfWeeks week)
        => _classOfWeeks.Remove(week);

    public static Result<ScheduledWeek> Create(string id, int weekNumber, List<ScheduledClassOfWeeks> classOfWeeks)
    {
        var item = new ScheduledWeek(id, weekNumber, classOfWeeks);
        return item;
    }
}
