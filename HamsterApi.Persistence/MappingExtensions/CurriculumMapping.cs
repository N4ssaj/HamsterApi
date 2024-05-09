
using HamsterApi.Domain.Models;
using HamsterApi.Persistence.Entites.Interfaces;

namespace HamsterApi.Persistence.MappingExtensions;

internal static class CurriculumMapping
{
    public static ICurriculumEntity ToEntity(this Curriculum curriculum)
    {
        ICurriculumEntity curriculumEntity = new CurriculumEntity
        {
            Id = curriculum.Id,
            ChairId=curriculum.ChairId,
            DepartmentId=curriculum.DepartmentId,
            DirectionId = curriculum.DirectionId,
            FGOSNumber = curriculum.FGOSNumber,
            YearOfPreparation = curriculum.YearOfPreparation,
            SemestersElectiveSubjects = curriculum.SemestersElectiveSubjects.Select(item=>item.ToEntity()).ToList(),
            SemestersSubjects = curriculum.SemestersSubjects.Select(item => item.ToEntity()).ToList()
        };

        return curriculumEntity;
    }

    public static Curriculum ToModel(this ICurriculumEntity curriculumEntity)
    {
        Curriculum curriculum = Curriculum.Create(curriculumEntity.Id,
            curriculumEntity.ChairId,
            curriculumEntity.DepartmentId,
            curriculumEntity.DirectionId,
            curriculumEntity.SemestersSubjects.Select(item => item.ToModel()).ToList(),
            curriculumEntity.SemestersElectiveSubjects.Select(item => item.ToModel()).ToList(),
            curriculumEntity.YearOfPreparation,
            curriculumEntity.FGOSNumber).Value;

        return curriculum;
    }
}
