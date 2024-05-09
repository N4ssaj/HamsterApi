
using HamsterApi.Domain.Common;

namespace HamsterApi.Domain.Models;

public class Curriculum
{

    private Curriculum(string id, string directionId, string chairId, string departmentId, List<SubjectWtihLoad> semestersSubjects, List<SubjectWtihLoad> semestersElectiveSubjects, int yearOfPreparation, string fGOSNumber)
        => (Id, DirectionId, ChairId, DepartmentId, _semestersSubject, _semestersElectiveSubject, YearOfPreparation, FGOSNumber)
        = (id, directionId, chairId, departmentId, semestersSubjects, semestersElectiveSubjects, yearOfPreparation, fGOSNumber);


    public string Id { get; }

    public string ChairId { get; }

    public string DepartmentId { get; }

    public string DirectionId { get; }

    public int YearOfPreparation { get; }

    public string FGOSNumber { get; }

    public IReadOnlyCollection<SubjectWtihLoad> SemestersSubjects
        => _semestersSubject;

    private List<SubjectWtihLoad> _semestersSubject;

    public void Add(SubjectWtihLoad semester)
        => _semestersSubject.Add(semester);

    public void Remove(SubjectWtihLoad semester)
        => _semestersSubject.Remove(semester);

    public IReadOnlyCollection<SubjectWtihLoad> SemestersElectiveSubjects
        => _semestersElectiveSubject;

    private List<SubjectWtihLoad> _semestersElectiveSubject;

    public void AddElective(SubjectWtihLoad semester)
        => _semestersElectiveSubject.Add(semester);

    public void RemoveElective(SubjectWtihLoad semester)
        => _semestersElectiveSubject.Remove(semester);

    public static Result<Curriculum> Create(string id, string chairId, string departmentId, string directionId, List<SubjectWtihLoad> semestersSubjects, List<SubjectWtihLoad> semestersElectiveSubjects, int yearOfPreparation, string fGOSNumber)
    {
        var curriculum = new Curriculum(id, directionId, chairId, departmentId, semestersSubjects, semestersElectiveSubjects, yearOfPreparation, fGOSNumber);
        return curriculum;
    }
}

