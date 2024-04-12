

using HamsterApi.Core.Common;
using System.Security.Cryptography.X509Certificates;

namespace HamsterApi.Core.Models;

public class ScheduledClassOfWeeks
{
    private ScheduledClassOfWeeks(string id, int weekNumber, DayOfWeek dayOfWee, List<ScheduledClass> scheduledClasses)
        => (Id, WeekNumber, DayOfWeek, _scheduledClasses) = (id,weekNumber , dayOfWee, scheduledClasses);

    public string Id { get; }=string.Empty;

    public int WeekNumber { get; }

    public DayOfWeek DayOfWeek { get; }

    public IReadOnlyCollection<ScheduledClass> ScheduledClasses
        => _scheduledClasses;

    private List<ScheduledClass> _scheduledClasses = [];

    public void Add(ScheduledClass scheduledClass)
        =>_scheduledClasses.Add(scheduledClass);

    public void Remove(ScheduledClass scheduledClass)
        =>_scheduledClasses.Remove(scheduledClass);

    public static Result<ScheduledClassOfWeeks> Create(string id, int weekNumber, DayOfWeek dayOfWee, List<ScheduledClass> scheduledClasses)
    {
        var scheduledClassOfWeeks = new ScheduledClassOfWeeks(id, weekNumber, dayOfWee, scheduledClasses);

        return scheduledClassOfWeeks;
    }

}
