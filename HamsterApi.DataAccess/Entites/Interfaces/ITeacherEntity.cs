using BrightstarDB.EntityFramework;
using HamsterApi.Core.Models;

namespace HamsterApi.DataAccess.Entites.Interfaces;

[Entity]
internal interface ITeacherEntity
{

    public string Name { get; set; }

    public string Surname { get; set; }

    public string Patronymic { get; set; }

    public string FullName { get; set; }

    public ICollection<string> SubjectsIds { get; set; }

    public string Id { get; }

    public string ChairId { get; set; }

    public string TeacherLoadId { get; set; }
}