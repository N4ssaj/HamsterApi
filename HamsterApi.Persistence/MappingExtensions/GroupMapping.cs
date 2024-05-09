
using HamsterApi.Domain.Models;
using HamsterApi.Persistence.Entites.Interfaces;

namespace HamsterApi.Persistence.MappingExtensions;

internal static class GroupMapping
{
    public static IGroupEntity ToEntity(this Group group)
    {
        IGroupEntity groupEntity = new GroupEntity
        {
            Id = group.Id,
            DirectionId = group.DirectionId,
            LevelOfEducation = group.LevelOfEducation,
            Number = group.Number
        };

        return groupEntity;
    }

    public static Group ToModel(this IGroupEntity groupEntity)
    {
        Group group = Group.Create(groupEntity.Id,
            groupEntity.Number,
            groupEntity.LevelOfEducation,
            groupEntity.DirectionId).Value;

        return group;
    }
}
