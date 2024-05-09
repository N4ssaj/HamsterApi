
using HamsterApi.Domain.Models;
using HamsterApi.Persistence.Entites.Interfaces;

namespace HamsterApi.Persistence.MappingExtensions;

internal static class TeachingLoadMapping
{
    public static ITeachingLoadEntity ToEntity(this TeachingLoad teachingLoad)
    {
        ITeachingLoadEntity teachingLoadEntity = new TeachingLoadEntity
        {
            Id = teachingLoad.Id,
            LaboratoryHours = teachingLoad.LaboratoryHours,
            LaboratoryHoursMax = teachingLoad.LaboratoryHoursMax,
            LecturesHours = teachingLoad.LecturesHours,
            LecturesHoursMax = teachingLoad.LecturesHoursMax,
            PracticeHours = teachingLoad.PracticeHours,
            PracticeHoursMax = teachingLoad.PracticeHoursMax
        };

        return teachingLoadEntity;
    }

    public static TeachingLoad ToModel(this ITeachingLoadEntity teachingLoadEntity)
    {
        TeachingLoad teachingLoad = TeachingLoad.Create(teachingLoadEntity.Id,
            teachingLoadEntity.LecturesHours,
            teachingLoadEntity.PracticeHours,
            teachingLoadEntity.LaboratoryHours,
            teachingLoadEntity.LecturesHoursMax,
            teachingLoadEntity.PracticeHoursMax,
            teachingLoadEntity.LaboratoryHoursMax).Value;

        return teachingLoad;
    }
}
