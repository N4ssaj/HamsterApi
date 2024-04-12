
using HamsterApi.Core.Common;

namespace HamsterApi.Core.Models;

public class Curriculum
{
   
    private Curriculum(string id , string directionId,  List<SubjectWtihLoad> semestersSubjects,List<SubjectWtihLoad> semestersElectiveSubjects, int yearOfPreparation,string fGOSNumber)
        => (Id, DirectionId, _semestersSubject, _semestersElectiveSubject, YearOfPreparation,FGOSNumber) 
        = (id, directionId, semestersSubjects, semestersElectiveSubjects,yearOfPreparation,fGOSNumber);


    public string Id { get; } = string.Empty;

    public string DirectionId { get; } = string.Empty;

    public int YearOfPreparation { get; }

    public string FGOSNumber { get; } = string.Empty;

    public IReadOnlyCollection<SubjectWtihLoad> SemestersSubjects
        => _semestersSubject;

    private List<SubjectWtihLoad> _semestersSubject = [];

    public void Add(SubjectWtihLoad semester)
        =>_semestersSubject.Add(semester);

    public void Remove(SubjectWtihLoad semester)
        =>_semestersSubject.Remove(semester);

    public IReadOnlyCollection<SubjectWtihLoad> SemestersElectiveSubjects
        => _semestersElectiveSubject;

    private List<SubjectWtihLoad> _semestersElectiveSubject = [];

    public void AddElective(SubjectWtihLoad semester)
        =>_semestersElectiveSubject.Add(semester);

    public void RemoveElective(SubjectWtihLoad semester)
        =>_semestersElectiveSubject.Remove(semester);

    public static Result<Curriculum> Create(string id , string directionId,List<SubjectWtihLoad> semestersSubjects,List<SubjectWtihLoad> semestersElectiveSubjects, int yearOfPreparation, string fGOSNumber)
    {
        var curriculum = new Curriculum(id, directionId, semestersSubjects, semestersElectiveSubjects,yearOfPreparation,fGOSNumber);
        return curriculum;
    }
}

