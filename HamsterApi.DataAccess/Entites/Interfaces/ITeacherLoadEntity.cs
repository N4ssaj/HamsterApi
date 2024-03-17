
using BrightstarDB.EntityFramework;

namespace HamsterApi.DataAccess.Entites.Interfaces;

[Entity]
internal interface ITeacherLoadEntity
{
    public string Id { get; }

    public int LecturesHours { get; set; }

    public int PracticeHours { get; set; }

    public int LaboratoryHours { get; set; }

    public int LecturesHoursMax { get; set; }

    public int PracticeHoursMax { get; set; }

    public int LaboratoryHoursMax { get; set; }
}
