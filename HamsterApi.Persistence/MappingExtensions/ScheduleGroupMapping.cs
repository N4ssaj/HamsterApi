using HamsterApi.Domain.Models;
using HamsterApi.Persistence.Entites.Interfaces;

namespace HamsterApi.Persistence.MappingExtensions;

internal static class ScheduleGroupMapping
{
    public static IScheduleGroupEntity ToEntity(this ScheduleGroup scheduleGroup)
    {
        IScheduleGroupEntity scheduleGroupEntity = new ScheduleGroupEntity
        {
            Id = scheduleGroup.Id,
            GroupId = scheduleGroup.GroupId,
            ScheduleId = scheduleGroup.ScheduleId,
            SemesterNumber = scheduleGroup.SemesterNumber,
            Weeks = scheduleGroup.Weeks.Select(item => item.ToEntity()).ToList()
        };
    
        return scheduleGroupEntity;
    }

    public static ScheduleGroup ToModel(this IScheduleGroupEntity scheduleGroupEntity)
    {
        ScheduleGroup scheduleGroup = ScheduleGroup.Create(scheduleGroupEntity.Id,
            scheduleGroupEntity.ScheduleId,
            scheduleGroupEntity.GroupId,
            scheduleGroupEntity.SemesterNumber,
            scheduleGroupEntity.Weeks.Select(item => item.ToModel()).ToList()).Value;

        return scheduleGroup;
    }
}
