

using BrightstarDB.EntityFramework;
using HamsterApi.Domain.Models;

namespace HamsterApi.Persistence.Entites.Interfaces;

[Entity]
internal interface ISubjectWtihLoadEntity
{
    public string Id { get; }

    public int SemesterNumber { get; set; }

    public ISubjectEntity Subject { get; set; }

    public IAcademicLoadEntity AcademicLoad { get; set;}
}
