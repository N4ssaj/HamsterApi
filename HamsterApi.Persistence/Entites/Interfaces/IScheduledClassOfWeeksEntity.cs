using BrightstarDB.EntityFramework;
using HamsterApi.Domain.Models;


namespace HamsterApi.Persistence.Entites.Interfaces;

[Entity]
internal interface IScheduledClassOfWeeksEntity
{
    public string Id { get; }

    public DayOfWeek DayOfWeek { get; set; }

    public string Date { get; set; }

    public ICollection<IScheduledClassEntity> ScheduledClasses { get; set; }
}
