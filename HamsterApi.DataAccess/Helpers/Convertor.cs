using HamsterApi.Core.Models;
using HamsterApi.DataAccess.Entites.Interfaces;

namespace HamsterApi.DataAccess.Helpers;

internal static class Convertor
{
    public static IScheduledClassEntity ToEntity(ScheduledClass @class)
    {
        ScheduledClassEntity scheduledEntity = new ScheduledClassEntity {Id=@class.Id, ClassNumber=@class.ClassNumber, SubjectId=@class.SubjectId,TeacherId=@class.TeacherId, AuditoriumId=@class.AuditoriumId, ClassType=@class.ClassType };
        return scheduledEntity;
    }
    public static ScheduledClass ToModel(IScheduledClassEntity scheduledEntity)
    {
        ScheduledClass scheduledClass = ScheduledClass.Create(scheduledEntity.Id, scheduledEntity.ClassNumber, scheduledEntity.SubjectId, scheduledEntity.TeacherId, scheduledEntity.AuditoriumId, scheduledEntity.ClassType).Value;
        return scheduledClass;
    }
    public static IScheduledClassOfWeeksEntity ToEntity(ScheduledClassOfWeeks weeks)
    {
        ScheduledClassOfWeeksEntity schedule=new ScheduledClassOfWeeksEntity
        {
            Id=weeks.Id,
            DayOfWeek=weeks.DayOfWeek,
            ScheduledClasses=weeks.ScheduledClasses.Select(i=>ToEntity(i)).ToList(),
            WeekNumber=weeks.WeekNumber,
        };
        return schedule;
    }
    public static ScheduledClassOfWeeks ToModel(IScheduledClassOfWeeksEntity scheduledEntity)
    {
        ScheduledClassOfWeeks weeks=ScheduledClassOfWeeks.Create(scheduledEntity.Id,scheduledEntity.WeekNumber,scheduledEntity.DayOfWeek,scheduledEntity.ScheduledClasses.Select(i=>ToModel(i)).ToList()).Value;
        return weeks;
    }
    public static IScheduleGroupEntity ToEntity(ScheduleGroup group)
    {
        ScheduleGroupEntity entity = new ScheduleGroupEntity
        {
            Id = group.Id,
            GroupId=group.GroupId,
            SemesterNumber=group.SemesterNumber,
            Weeks=group.Weeks.Select(i=>ToEntity(i)).ToList(),
        };
        return entity;
    }
    public static ScheduleGroup ToModel(IScheduleGroupEntity group)
    {
        ScheduleGroup scheduleGroup=ScheduleGroup.Create(group.Id,group.GroupId,group.SemesterNumber,group.Weeks.Select(i=>ToModel(i)).ToList()).Value;
        return scheduleGroup;
    }
    public static IScheduleEntity ToEntity(Schedule schedule)
    {
        ScheduleEntity entity = new ScheduleEntity
        {
            Id=schedule.Id,
            GroupsSchedule=schedule.GroupsSchedule.Select(i=>ToEntity(i)).ToList(),
            SemesterNumber=schedule.SemesterNumber
        };
        return entity;
    }
    public static Schedule ToModel(IScheduleEntity schedule)
    {
        Schedule schedule1 = Schedule.Create(schedule.Id, schedule.SemesterNumber, schedule.GroupsSchedule.Select(i=>ToModel(i)).ToList()).Value;
        return schedule1;
    }
}
