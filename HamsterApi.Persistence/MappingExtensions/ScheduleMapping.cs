
using HamsterApi.Domain.Models;
using HamsterApi.Persistence.Entites.Interfaces;

namespace HamsterApi.Persistence.MappingExtensions;

internal static class ScheduleMapping
{
    public static IScheduleEntity ToEntity(this Schedule schedule)
    {
        IScheduleEntity scheduleEntity = new ScheduleEntity 
        { 
            Id = schedule.Id,
            Year = schedule.Year,
            SpringOrAutumn= schedule.SpringOrAutumn,
            GroupsScheduleIds = schedule.GroupsScheduleIds.ToList()
        };

        return scheduleEntity;
    }
    public static Schedule ToModel(this IScheduleEntity scheduleEntity)
    {
        Schedule schedule = Schedule.Create(scheduleEntity.Id, 
            scheduleEntity.Year,
            scheduleEntity.SpringOrAutumn,
            scheduleEntity.GroupsScheduleIds.ToList()).Value;

        return schedule;
    }
}
