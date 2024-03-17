

using BrightstarDB.EntityFramework;
using HamsterApi.Core.Models;

namespace HamsterApi.DataAccess.Entites.Interfaces;

[Entity]
internal interface IScheduleGroupEntity
{
    public string Id { get; }

    public IGroupEntity Group { get; set; }

    public int SemesterNumber { get; set; }

    public ICollection<IScheduledClassOfWeeksEntity> Weeks { get; set; }
}
