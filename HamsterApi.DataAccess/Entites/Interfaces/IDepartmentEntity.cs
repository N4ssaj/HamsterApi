

using BrightstarDB.EntityFramework;
using HamsterApi.Core.Models;

namespace HamsterApi.DataAccess.Entites.Interfaces;

[Entity]
internal interface IDepartmentEntity
{
    public string Id { get; }

    public string Title { get; set; }

    public ICollection<ITeacherEntity> Teachers { get; set; }

    public IFacultyEntity Faculty { get; set;}
}
