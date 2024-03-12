

using BrightstarDB.EntityFramework;
using HamsterApi.Core.Common.Enum;


namespace HamsterApi.DataAccess.Entites.Interfaces;

[Entity]
internal interface IScheduledClassEntity
{
    public string Id { get; }

    public ClassType ClassType { get; set; }

    public int ClassNumber { get; set; }

    public ISubjectEntity Subject { get; set; }

    public ITeacherEntity Teacher { get; set; }

    public IAuditoriumEntity Auditorium { get; set; }
}
