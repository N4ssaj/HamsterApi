
using BrightstarDB.EntityFramework;
using HamsterApi.Domain.Models;

namespace HamsterApi.Persistence.Entites.Interfaces;

[Entity]
internal interface IScheduleEntity
{
    public string Id { get; }

    public int SemesterNumber { get; set; }

    public ICollection<string> GroupsScheduleIds { get; set; }
}
