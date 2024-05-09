
using BrightstarDB.EntityFramework;
using HamsterApi.Domain.Models;


namespace HamsterApi.Persistence.Entites.Interfaces;

[Entity]
internal interface ICurriculumEntity
{

    public string Id { get; }

    public string ChairId { get; set; }

    public string DepartmentId { get; set; }

    public string DirectionId { get; set; }

    public int YearOfPreparation { get; set; }

    public string FGOSNumber { get; set; }

    public ICollection<ISubjectWtihLoadEntity> SemestersSubjects { get; set; }

    public ICollection<ISubjectWtihLoadEntity> SemestersElectiveSubjects { get; set; }
}
