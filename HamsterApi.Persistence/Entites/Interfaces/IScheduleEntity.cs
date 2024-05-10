
using BrightstarDB.EntityFramework;
using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;

namespace HamsterApi.Persistence.Entites.Interfaces;

[Entity]
internal interface IScheduleEntity
{
    public string Id { get; }

    public int Year { get; set; }

    public SpringOrAutumn SpringOrAutumn { get; set; }

    public ICollection<string> GroupsScheduleIds { get; set; }
}
