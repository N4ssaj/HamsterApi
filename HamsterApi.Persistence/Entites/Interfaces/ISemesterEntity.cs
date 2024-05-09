
using BrightstarDB.EntityFramework;
using HamsterApi.Domain.Models;


namespace HamsterApi.Persistence.Entites.Interfaces;

[Entity]
internal interface ISemesterEntity
{

    public string Id { get; }

    public int Number { get; set; }

    public string GroupId { get; set; }

    public ICollection<ISubjectWtihLoadEntity> Subjects { get; set; }
}
