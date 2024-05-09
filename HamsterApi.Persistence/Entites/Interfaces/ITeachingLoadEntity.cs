
using BrightstarDB.EntityFramework;

namespace HamsterApi.Persistence.Entites.Interfaces;

[Entity]
internal interface ITeachingLoadEntity
{
    public string Id { get; }

    public int LecturesHours { get; set; }

    public int PracticeHours { get; set; }

    public int LaboratoryHours { get; set; }

    public int LecturesHoursMax { get; set; }

    public int PracticeHoursMax { get; set; }

    public int LaboratoryHoursMax { get; set; }
}
