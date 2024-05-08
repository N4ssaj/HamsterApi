
using BrightstarDB.EntityFramework;
using HamsterApi.Core.Models;

namespace HamsterApi.DataAccess.Entites.Interfaces;
[Entity]
internal interface IScheduledWeekEntity
{
    public string Id { get; }

    public int WeekNumber { get; set; }

    public ICollection<IScheduledClassOfWeeksEntity> ClassOfWeeks { get; set; }
}
