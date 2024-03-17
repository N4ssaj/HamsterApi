
using BrightstarDB.EntityFramework;
using HamsterApi.Core.Models;

namespace HamsterApi.DataAccess.Entites.Interfaces;

[Entity]
internal interface IFacultyEntity
{
    public string Title { get; set; }

    public string Id { get; }

    [InverseProperty("Faculty")]
    public ICollection<IDepartmentEntity> Departments { get; set; }

    [InverseProperty("Faculty")]
    public ICollection<IDirectionEntity> Directions { get; set;}
}
