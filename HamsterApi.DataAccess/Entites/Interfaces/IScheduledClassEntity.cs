

using BrightstarDB.EntityFramework;
using HamsterApi.Core.Common.Enum;
using HamsterApi.Core.Models;


namespace HamsterApi.DataAccess.Entites.Interfaces;

[Entity]
internal interface IScheduledClassEntity
{

    public string Id { get; }

    public ClassType ClassType { get; set; }

    public int ClassNumber { get; set; }

    public string SubjectId { get; set; }

    public string TeacherId { get; set; }

    public string AuditoriumId { get; set; }
}
