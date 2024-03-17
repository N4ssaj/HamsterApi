using BrightstarDB.EntityFramework;
using HamsterApi.Core.Models;


namespace HamsterApi.DataAccess.Entites.Interfaces;

[Entity]
internal interface ISubjectEntity
{
    public string Title { get; set; }

    public string Index { get; set; }

    public ICollection<ITeacherEntity> Teachers { get; set; }

    public string Id { get; }
}
