
using HamsterApi.Domain.Models;
using HamsterApi.Persistence.Entites.Interfaces;

namespace HamsterApi.Persistence.MappingExtensions;

internal static class SubjectMapping
{
    public static ISubjectEntity ToEntity(this Subject subject)
    {
        ISubjectEntity subjectEntity = new SubjectEntity
        {
            Id = subject.Id,
            Index = subject.Index,
            TeachersIds = subject.TeachersIds.ToList(),
            Title = subject.Title
        };

        return subjectEntity;
    }

    public static Subject ToModel(this ISubjectEntity subjectEntity)
    {
        Subject subject = Subject.Create(subjectEntity.Id,
            subjectEntity.Title,
            subjectEntity.Index,
            subjectEntity.TeachersIds.ToList()).Value;

        return subject;
    }
}
