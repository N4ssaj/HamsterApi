

using HamsterApi.Domain.Models;
using HamsterApi.Persistence.Entites.Interfaces;

namespace HamsterApi.Persistence.MappingExtensions;

internal static class ScheduleWeekMapping
{
    public static IScheduledWeekEntity ToEntity(this ScheduledWeek scheduledWeek)
    {
        IScheduledWeekEntity scheduledWeekEntity = new ScheduledWeekEntity
        {
            Id = scheduledWeek.Id,
            ClassOfWeeks = scheduledWeek.ClassOfWeeks.Select(item=>item.ToEntity()).ToList(),
            WeekNumber = scheduledWeek.WeekNumber
        };

        return scheduledWeekEntity;
    }
    
    public static ScheduledWeek ToModel(this IScheduledWeekEntity scheduledWeekEntity)
    {
        ScheduledWeek scheduledWeek = ScheduledWeek.Create(scheduledWeekEntity.Id,
            scheduledWeekEntity.WeekNumber,
            scheduledWeekEntity.ClassOfWeeks.Select(item => item.ToModel()).ToList()).Value;

        return scheduledWeek;
    }
}
