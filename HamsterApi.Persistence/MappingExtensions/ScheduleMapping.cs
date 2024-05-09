
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
            SemesterNumber = schedule.SemesterNumber, 
            GroupsScheduleIds = schedule.GroupsScheduleIds.ToList()
        };

        return scheduleEntity;
    }
    public static Schedule ToModel(this IScheduleEntity scheduleEntity)
    {
        Schedule schedule = Schedule.Create(scheduleEntity.Id, 
            scheduleEntity.SemesterNumber, 
            scheduleEntity.GroupsScheduleIds.ToList()).Value;

        return schedule;
    }
}
