
using HamsterApi.Domain.Models;
using HamsterApi.Persistence.Entites.Interfaces;

namespace HamsterApi.Persistence.MappingExtensions;

internal static class SubjectWithLoadMapping
{
    public static ISubjectWtihLoadEntity ToEntity(this SubjectWtihLoad subjectWtihLoad)
    {
        ISubjectWtihLoadEntity subjectWtihLoadEntity = new SubjectWtihLoadEntity
        {
            Id = subjectWtihLoad.Id,
            AcademicLoad = subjectWtihLoad.AcademicLoad.ToEntity(),
            SemesterNumber = subjectWtihLoad.SemesterNumber,
            Subject = subjectWtihLoad.Subject.ToEntity()
        };

        return subjectWtihLoadEntity;
    }

    public static SubjectWtihLoad ToModel(this ISubjectWtihLoadEntity subjectWtihLoadEntity)
    {
        SubjectWtihLoad subjectWtihLoad = SubjectWtihLoad.Create(subjectWtihLoadEntity.Id,
            subjectWtihLoadEntity.Subject.ToModel(),
            subjectWtihLoadEntity.AcademicLoad.ToModel(),
            subjectWtihLoadEntity.SemesterNumber).Value;

        return subjectWtihLoad;
    }
}
