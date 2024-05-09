

using HamsterApi.Domain.Models;
using HamsterApi.Persistence.Entites.Interfaces;


namespace HamsterApi.Persistence.MappingExtensions;

internal static class ScheduleClassOfWeeksMapping
{
    public static IScheduledClassOfWeeksEntity ToEntity(this ScheduledClassOfWeeks scheduledClassOfWeeks)
    {
        IScheduledClassOfWeeksEntity scheduledClassOfWeeksEntity = new ScheduledClassOfWeeksEntity
        {
            Id = scheduledClassOfWeeks.Id,
            Date = scheduledClassOfWeeks.Data.ToString(),
            DayOfWeek = scheduledClassOfWeeks.DayOfWeek,
            ScheduledClasses = scheduledClassOfWeeks.ScheduledClasses.Select(item => item.ToEntity()).ToList()
        };

        return scheduledClassOfWeeksEntity;
    }

    public static ScheduledClassOfWeeks ToModel(this IScheduledClassOfWeeksEntity scheduledClassOfWeeksEntity)
    {
        ScheduledClassOfWeeks scheduledClassOfWeeks = ScheduledClassOfWeeks.Create(scheduledClassOfWeeksEntity.Id,
            scheduledClassOfWeeksEntity.DayOfWeek,
            scheduledClassOfWeeksEntity.ScheduledClasses.Select(item=>item.ToModel()).ToList(),
            DateOnly.Parse(scheduledClassOfWeeksEntity.Date)).Value;

        return scheduledClassOfWeeks;
    }
}
