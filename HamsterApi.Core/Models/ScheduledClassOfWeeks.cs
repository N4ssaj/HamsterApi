

using HamsterApi.Core.Common;


namespace HamsterApi.Core.Models;

public class ScheduledClassOfWeeks
{
    public ScheduledClassOfWeeks(string id, int weekNumber, DayOfWeek dayOfWeek, List<ScheduledClass> scheduledClasses)
        => (Id, WeekNumber, DayOfWeek, ScheduledClasses) = (id,weekNumber , dayOfWeek, scheduledClasses);

    public string Id { get; }

    public int WeekNumber { get; }

    public DayOfWeek DayOfWeek { get; }

    public List<ScheduledClass> ScheduledClasses { get; }

    public static Result<ScheduledClassOfWeeks> Create(string id, int weekNumber, DayOfWeek dayOfWee, List<ScheduledClass> scheduledClasses)
    {
        var scheduledClassOfWeeks = new ScheduledClassOfWeeks(id, weekNumber, dayOfWee, scheduledClasses);

        return scheduledClassOfWeeks;
    }

}
