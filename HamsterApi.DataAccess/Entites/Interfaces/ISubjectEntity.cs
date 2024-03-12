using BrightstarDB.EntityFramework;


namespace HamsterApi.DataAccess.Entites.Interfaces;

[Entity]
internal interface ISubjectEntity
{
    public string Title { get; set; }

    public string Index { get; set; }

    [InverseProperty("Subjects")]
    public ICollection<ITeacherEntity> Teachers { get; set; }

    public string Id { get; }
}
