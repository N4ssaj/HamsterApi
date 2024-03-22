
using BrightstarDB.EntityFramework;
using HamsterApi.Core.Models;


namespace HamsterApi.DataAccess.Entites.Interfaces;

[Entity]
internal interface ICurriculumEntity
{

    public string Id { get; }

    public string DirectionId { get; set; }

    public int YearOfPreparation { get; set; }

    public string FGOSNumber { get; set; }

    public ICollection<ISubjectWtihLoadEntity> SemestersSubjects { get; set; }

    public ICollection<ISubjectWtihLoadEntity> SemestersElectiveSubjects { get; set; }
}
