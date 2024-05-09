
using BrightstarDB.EntityFramework;

namespace HamsterApi.Persistence.Entites.Interfaces;
[Entity]
internal interface IScheduledWeekEntity
{
    public string Id { get; }

    public int WeekNumber { get; set; }

    public ICollection<IScheduledClassOfWeeksEntity> ClassOfWeeks { get; set; }
}
