
using HamsterApi.Domain.Models;
using HamsterApi.Persistence.Entites.Interfaces;

namespace HamsterApi.Persistence.MappingExtensions;

internal static class ChairMapping
{
    public static IChairEntity ToEntity(this Chair chair)
    {
        IChairEntity chairEntity = new ChairEntity
        {
            Id = chair.Id,
            DepartmentId = chair.DepartmentId,
            TeachersIds = chair.TeachersIds.ToList(),
            Title = chair.Title
        };

        return chairEntity;
    }

    public static Chair ToModel(this IChairEntity chairEntity)
    {
        Chair chair = Chair.Create(chairEntity.Id,
            chairEntity.Title,
            chairEntity.TeachersIds.ToList(),
            chairEntity.DepartmentId).Value;

        return chair;
    }
}
