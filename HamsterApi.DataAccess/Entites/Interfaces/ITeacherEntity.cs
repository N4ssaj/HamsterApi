using BrightstarDB.EntityFramework;

namespace HamsterApi.DataAccess.Entites.Interfaces;

[Entity]
internal interface ITeacherEntity
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public string FullName { get; set; }

    public ICollection<ISubjectEntity> Subjects { get; set; }

    public string Id { get; }
}