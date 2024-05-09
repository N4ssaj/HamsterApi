using HamsterApi.Domain.Common;
using HamsterApi.Domain.Common.Enum;


namespace HamsterApi.Domain.Models;


public class AcademicLoad
{

    private AcademicLoad(string id, int lectures, int laboratory, int practice, int credits, int total, AcademicEvaluationType academicEvaluationType)
        => (Id, Lectures, Laboratory, Practice, Credits, Total, AcademicEvaluationType)
            = (id, lectures, laboratory, practice, credits, total, academicEvaluationType);

    public string Id { get; }

    public int Lectures { get; }

    public int Laboratory { get; }

    public int Practice { get; }

    public int Credits { get; }

    public int Total { get; }

    public AcademicEvaluationType AcademicEvaluationType { get; }

    public static Result<AcademicLoad> Create(string id, int lectures, int laboratory, int practice, int credits, AcademicEvaluationType academicEvaluationType)
    {
        // валидация
        var total = lectures + laboratory + practice + credits;
        var academicLoad = new AcademicLoad(id, lectures, laboratory, practice, credits, total, academicEvaluationType);
        return academicLoad;
    }
}
