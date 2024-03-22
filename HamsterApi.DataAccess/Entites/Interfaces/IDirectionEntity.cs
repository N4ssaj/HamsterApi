
using BrightstarDB.EntityFramework;
using HamsterApi.Core.Common.Enum;
using HamsterApi.Core.Models;

namespace HamsterApi.DataAccess.Entites.Interfaces;

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
