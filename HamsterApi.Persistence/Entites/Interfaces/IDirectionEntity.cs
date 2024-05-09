
using BrightstarDB.EntityFramework;
using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;

namespace HamsterApi.Persistence.Entites.Interfaces;

[Entity]
internal interface IDirectionEntity
{
    public string Title { get; set; }

    public string Id { get; }

    public ICollection<string> GroupsIds { get; set; }

    public LevelOfEducation LevelOfEducation { get; set; }

    public FormOfEducation FormOfEducation { get; set; }

    public string DepartmentId { get; set; }
}
