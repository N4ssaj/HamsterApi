
using BrightstarDB.EntityFramework;


namespace HamsterApi.DataAccess.Entites.Interfaces;

[Entity]
internal interface ISemesterEntity
{
    public string Id { get; }

    public int Number { get; set; }

    public IGroupEntity Group { get; set; }

    public ICollection<ISubjectWtihLoadEntity> Subjects { get; set; }
}
