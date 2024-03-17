

using BrightstarDB.EntityFramework;
using HamsterApi.Core.Common.Enum;

namespace HamsterApi.DataAccess.Entites.Interfaces;

[Entity]
internal interface IGroupEntity
{
    public string Number { get; set; }

    public string Id { get; }

    public LevelOfEducation LevelOfEducation { get; set; }
}
