
using HamsterApi.Domain.Models;
using HamsterApi.Persistence.Entites.Interfaces;

namespace HamsterApi.Persistence.MappingExtensions;

internal static class SemesterMapping
{
    public static ISemesterEntity ToEntity(this Semester semester)
    {
        ISemesterEntity semesterEntity = new SemesterEntity
        {
            Id = semester.Id,
            GroupId = semester.GroupId,
            Number = semester.Number,
            Subjects = semester.Subjects.Select(item=>item.ToEntity()).ToList()
        };

        return semesterEntity;
    }

    public static Semester ToModel(this ISemesterEntity semesterEntity)
    {
        Semester semester = Semester.Create(semesterEntity.Id,
            semesterEntity.Number,
            semesterEntity.GroupId,
            semesterEntity.Subjects.Select(item => item.ToModel()).ToList()).Value;

        return semester;
    }
}
