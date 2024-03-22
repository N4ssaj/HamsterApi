
using BrightstarDB.EntityFramework;
using HamsterApi.Core.Models;

namespace HamsterApi.DataAccess.Entites.Interfaces;

[Entity]
internal interface IDepartmentEntity
{
    public string Title { get; set; }

    public string Id { get; }

    public ICollection<string> ChairsIds { get; set; }

    public ICollection<string> DirectionsIds { get; set; }
}
