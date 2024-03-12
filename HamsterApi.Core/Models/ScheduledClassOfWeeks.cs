

using HamsterApi.Core.Common;
using System.Security.Cryptography.X509Certificates;

namespace HamsterApi.Core.Models;

public class ScheduledClassOfWeeks
{
    private ScheduledClassOfWeeks(string id, int weekNumber, DayOfWeek dayOfWee, IReadOnlyCollection<ScheduledClass> scheduledClasses)
        => (Id, WeekNumber, DayOfWeek, ScheduledClasses) = (id,weekNumber , dayOfWee, scheduledClasses);

    public string Id { get; }

    public int WeekNumber { get; }

    public DayOfWeek DayOfWeek { get; }

    public IReadOnlyCollection<ScheduledClass> ScheduledClasses { get; }

    public static Result<ScheduledClassOfWeeks> Create(string id, int weekNumber, DayOfWeek dayOfWee, IReadOnlyCollection<ScheduledClass> scheduledClasses)
    {
        var scheduledClassOfWeeks = new ScheduledClassOfWeeks(id, weekNumber, dayOfWee, scheduledClasses);

        return scheduledClassOfWeeks;
    }

}
