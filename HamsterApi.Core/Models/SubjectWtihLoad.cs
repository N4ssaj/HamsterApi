

using HamsterApi.Core.Common;

namespace HamsterApi.Core.Models;

public class SubjectWtihLoad
{
    private SubjectWtihLoad(string id, Subject subject, AcademicLoad academicLoad, int semesterNumber)
        => (Id,SemesterNumber,Subject, AcademicLoad) = (id,semesterNumber, subject, academicLoad);

    public string Id { get; }

    public int SemesterNumber { get; }

    public Subject Subject { get; }
    
    public AcademicLoad AcademicLoad { get; }

    public static Result<SubjectWtihLoad> Create(string id, Subject subject, AcademicLoad academicLoad, int semesterNumber=0)
    {
        var subjectWithLoad = new SubjectWtihLoad(id, subject, academicLoad, semesterNumber);
        return subjectWithLoad;
    }
}
