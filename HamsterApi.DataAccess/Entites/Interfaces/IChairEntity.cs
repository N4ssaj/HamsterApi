

using BrightstarDB.EntityFramework;
using HamsterApi.Core.Models;

namespace HamsterApi.DataAccess.Entites.Interfaces;

[Entity]
internal interface IChairEntity
{
    public string Id { get; }

    public string Title { get; set; }

    public ICollection<string> TeachersIds { get; set; }

    public string DepartmentId { get; set; }
}
