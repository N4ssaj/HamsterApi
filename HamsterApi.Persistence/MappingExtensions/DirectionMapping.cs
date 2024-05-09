
using HamsterApi.Domain.Models;
using HamsterApi.Persistence.Entites.Interfaces;

namespace HamsterApi.Persistence.MappingExtensions;

internal static class DirectionMapping
{
    public static IDirectionEntity ToEntity(this Direction direction)
    {
        IDirectionEntity directionEntity = new DirectionEntity
        {
            Id = direction.Id,
            DepartmentId = direction.DepartmentId,
            FormOfEducation = direction.FormOfEducation,
            GroupsIds = direction.GroupsIds.ToList(),
            LevelOfEducation = direction.LevelOfEducation,
            Title = direction.Title
        };

        return directionEntity;
    }

    public static Direction ToModel(this IDirectionEntity directionEntity)
    {
        Direction direction = Direction.Create(directionEntity.Id,
            directionEntity.Title,
            directionEntity.GroupsIds.ToList(),
            directionEntity.FormOfEducation,
            directionEntity.LevelOfEducation,
            directionEntity.DepartmentId).Value;

        return direction;
    }
}
