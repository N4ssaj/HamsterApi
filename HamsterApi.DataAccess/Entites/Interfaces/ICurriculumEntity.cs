
using BrightstarDB.EntityFramework;


namespace HamsterApi.DataAccess.Entites.Interfaces;

[Entity]
internal interface ICurriculumEntity
{
    public string Id { get; }

    public IGroupEntity Group { get; set; }

    public ICollection<ISubjectWtihLoadEntity> SemestersSubjects { get; set; }

    public ICollection<ISubjectWtihLoadEntity> SemestersElectiveSubjects { get; set; }
}
