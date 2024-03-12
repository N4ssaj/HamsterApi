using BrightstarDB.EntityFramework;


namespace HamsterApi.DataAccess.Entites.Interfaces;

[Entity]
internal interface IScheduledClassOfWeeksEntity
{
    public string Id { get; }

    public int WeekNumber { get; set; }

    public DayOfWeek DayOfWeek { get; set; }

    public ICollection<IScheduledClassEntity> ScheduledClasses { get; set; }
}
