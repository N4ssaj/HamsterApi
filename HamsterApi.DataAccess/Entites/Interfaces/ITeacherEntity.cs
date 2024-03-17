using BrightstarDB.EntityFramework;
using HamsterApi.Core.Models;

namespace HamsterApi.DataAccess.Entites.Interfaces;

[Entity]
internal interface ITeacherEntity
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public string FullName { get; set;}

    [InverseProperty("Teachers")]
    public ICollection<ISubjectEntity> Subjects { get; set; }

    public string Id { get; }

    [InverseProperty("Teachers")]
    public IDepartmentEntity Department { get;set; }

    public ITeacherLoadEntity TeacherLoad { get; set; }
}