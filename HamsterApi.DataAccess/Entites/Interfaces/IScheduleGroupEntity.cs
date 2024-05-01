

using BrightstarDB.EntityFramework;
using HamsterApi.Core.Models;

namespace HamsterApi.DataAccess.Entites.Interfaces;

[Entity]
internal interface IScheduleGroupEntity
{
    public string Id { get; }

    public string GroupId { get; set; }

    public int SemesterNumber { get; set; }

    public ICollection<IScheduledClassOfWeeksEntity> Weeks { get; set; }
}
