using BrightstarDB.EntityFramework;
using HamsterApi.Domain.Models;


namespace HamsterApi.Persistence.Entites.Interfaces;

[Entity]
internal interface ISubjectEntity
{
    public string Title { get; set; }

    public string Index { get; set; }

    public ICollection<string> TeachersIds { get; set; }

    public string Id { get; }
}
