

using BrightstarDB.EntityFramework;
using HamsterApi.Domain.Models;

namespace HamsterApi.Persistence.Entites.Interfaces;

[Entity]
internal interface IScheduleGroupEntity
{
    public string Id { get; }

    public string ScheduleId { get; set; }

    public string GroupId { get; set; }

    public int SemesterNumber { get; set; }

    public ICollection<IScheduledWeekEntity> Weeks { get; set; }
}
