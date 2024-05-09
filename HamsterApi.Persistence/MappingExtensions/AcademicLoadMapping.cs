

using HamsterApi.Domain.Models;
using HamsterApi.Persistence.Entites.Interfaces;

namespace HamsterApi.Persistence.MappingExtensions;

internal static class AcademicLoadMapping
{
    public static IAcademicLoadEntity ToEntity(this AcademicLoad academicLoad)
    {
        IAcademicLoadEntity academicLoadEntity = new AcademicLoadEntity
        {
            Id = academicLoad.Id,
            AcademicEvaluationType = academicLoad.AcademicEvaluationType,
            Credits = academicLoad.Credits,
            Laboratory = academicLoad.Laboratory,
            Lectures = academicLoad.Lectures,
            Practice = academicLoad.Practice,
            Total = academicLoad.Total
        };

        return academicLoadEntity;
    }

    public static AcademicLoad ToModel(this IAcademicLoadEntity academicLoadEntity)
    {
        AcademicLoad academicLoad = AcademicLoad.Create(academicLoadEntity.Id,
            academicLoadEntity.Lectures,
            academicLoadEntity.Laboratory,
            academicLoadEntity.Practice,
            academicLoadEntity.Credits,
            academicLoadEntity.AcademicEvaluationType).Value;

        return academicLoad;
    }
}
