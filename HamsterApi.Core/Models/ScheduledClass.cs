using HamsterApi.Core.Common;
using HamsterApi.Core.Common.Enum;


namespace HamsterApi.Core.Models;


public class ScheduledClass
{
    private ScheduledClass(string id , int classNumber, string subject, string teacher, string auditorium,ClassType classType)
        => (Id, ClassNumber, SubjectId, TeacherId, AuditoriumId,ClassType) = (id, classNumber, subject, teacher, auditorium,classType);

    public string Id { get; }

    public ClassType ClassType { get; }

    public int ClassNumber { get; }

    public string SubjectId { get; }=string.Empty;

    public string TeacherId { get; } = string.Empty;

    public string AuditoriumId { get; } = string.Empty;

    public static Result<ScheduledClass> Create(string id , int classNumber,string subject, string teacher, string auditorium,ClassType classType)
    {

        var scheduledClass = new ScheduledClass(id, classNumber, subject, teacher, auditorium,classType);
        return scheduledClass;
    }
}

