

using BrightstarDB.EntityFramework;
using HamsterApi.Domain.Models;

namespace HamsterApi.Persistence.Entites.Interfaces;

[Entity]
internal interface IChairEntity
{
    public string Id { get; }

    public string Title { get; set; }

    public ICollection<string> TeachersIds { get; set; }

    public string DepartmentId { get; set; }
}
