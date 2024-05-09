

using BrightstarDB.EntityFramework;
using HamsterApi.Domain.Common.Enum;

namespace HamsterApi.Persistence.Entites.Interfaces;

[Entity]
internal interface IGroupEntity
{

    public string Number { get; set; }

    public string Id { get; }

    public string DirectionId { get; set; }

    public LevelOfEducation LevelOfEducation { get; set; }
}
