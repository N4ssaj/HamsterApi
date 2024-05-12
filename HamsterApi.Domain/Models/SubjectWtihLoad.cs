using HamsterApi.Domain.Common;

namespace HamsterApi.Domain.Models;

public class SubjectWtihLoad
{
    private SubjectWtihLoad(string id, Subject subject, AcademicLoad academicLoad)
        => (Id, Subject, AcademicLoad) = (id, subject, academicLoad);

    public string Id { get; }

    public Subject Subject { get; }

    public AcademicLoad AcademicLoad { get; }

    public static Result<SubjectWtihLoad> Create(string id, Subject subject, AcademicLoad academicLoad)
    {
        var subjectWithLoad = new SubjectWtihLoad(id, subject, academicLoad);
        return subjectWithLoad;
    }
}
