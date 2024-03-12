
using BrightstarDB.EntityFramework;

namespace HamsterApi.DataAccess.Entites.Interfaces;

[Entity]
internal interface IScheduleEntity
{
    public string Id { get; }

    public int SemesterNumber { get; set; }

    public ICollection<IScheduleGroupEntity> GroupsSchedule { get; set; }
}
