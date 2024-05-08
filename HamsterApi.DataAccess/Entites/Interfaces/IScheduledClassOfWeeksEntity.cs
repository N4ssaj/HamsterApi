using BrightstarDB.EntityFramework;
using HamsterApi.Core.Models;


namespace HamsterApi.DataAccess.Entites.Interfaces;

[Entity]
internal interface IScheduledClassOfWeeksEntity
{
    public string Id { get; }

    public DayOfWeek DayOfWeek { get; set; }

    public string Data { get; set; }

    public ICollection<IScheduledClassEntity> ScheduledClasses { get; set; }
}
