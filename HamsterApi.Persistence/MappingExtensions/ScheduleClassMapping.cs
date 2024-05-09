
using HamsterApi.Domain.Models;
using HamsterApi.Persistence.Entites.Interfaces;

namespace HamsterApi.Persistence.MappingExtensions;

internal static class ScheduleClassMapping
{
    public static IScheduledClassEntity ToEntity(this ScheduledClass @class)
    {
        IScheduledClassEntity classEntity = new ScheduledClassEntity 
        { 
          Id = @class.Id, 
          AuditoriumId = @class.AuditoriumId, 
          ClassNumber = @class.ClassNumber, 
          ClassType = @class.ClassType, 
          SubjectId = @class.SubjectId, 
          TeacherId = @class.TeacherId 
        };
        return classEntity;
    }

    public static ScheduledClass ToModel(this IScheduledClassEntity classEntity)
    {
        ScheduledClass scheduledClass = ScheduledClass.Create(classEntity.Id, 
            classEntity.ClassNumber, 
            classEntity.SubjectId, 
            classEntity.TeacherId, 
            classEntity.AuditoriumId, 
            classEntity.ClassType).Value;

        return scheduledClass;
    }
}
