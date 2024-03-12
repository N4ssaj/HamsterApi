

using BrightstarDB.EntityFramework;

namespace HamsterApi.DataAccess.Entites.Interfaces;

[Entity]
internal interface ISubjectWtihLoadEntity
{
    public string Id { get;}

    public ISubjectEntity Subject { get; set; }

    public IAcademicLoadEntity AcademicLoad { get; set; }

    public int SemesterNumber { get; set; }
}
