

using HamsterApi.Domain.Common;


namespace HamsterApi.Domain.Models;

public class ScheduledClassOfWeeks
{
    private ScheduledClassOfWeeks(string id, DayOfWeek dayOfWeek, List<ScheduledClass> scheduledClasses, DateOnly date)
        => (Id, DayOfWeek, _scheduledClasses, Data) = (id, dayOfWeek, scheduledClasses, date);

    public string Id { get; }

    public DayOfWeek DayOfWeek { get; }

    private List<ScheduledClass> _scheduledClasses;

    public DateOnly Data { get; }

    public IReadOnlyCollection<ScheduledClass> ScheduledClasses
        => _scheduledClasses;

    public void Add(ScheduledClass scheduledClass)
        => _scheduledClasses.Add(scheduledClass);

    public void Remove(ScheduledClass scheduledClass)
        => _scheduledClasses.Remove(scheduledClass);

    public static Result<ScheduledClassOfWeeks> Create(string id, DayOfWeek dayOfWee, List<ScheduledClass> scheduledClasses, DateOnly date)
    {
        var scheduledClassOfWeeks = new ScheduledClassOfWeeks(id, dayOfWee, scheduledClasses, date);

        return scheduledClassOfWeeks;
    }

}
