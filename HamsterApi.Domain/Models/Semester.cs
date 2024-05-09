
using HamsterApi.Domain.Common;

namespace HamsterApi.Domain.Models;


public class Semester
{

    private Semester(string id, int number, string groupId, List<SubjectWtihLoad> subjects)
        => (Id, Number, GroupId, _subjects) = (id, number, groupId, subjects);

    public string Id { get; } = string.Empty;

    public int Number { get; }

    public string GroupId { get; } = string.Empty;


    public IReadOnlyCollection<SubjectWtihLoad> Subjects
        => _subjects;

    private List<SubjectWtihLoad> _subjects = [];

    public void Add(SubjectWtihLoad subject)
        => _subjects.Add(subject);

    public void Remove(SubjectWtihLoad subject)
        => _subjects.Remove(subject);


    public static Result<Semester> Create(string id, int number, string groupId, List<SubjectWtihLoad> subjects)
    {

        var semester = new Semester(id, number, groupId, subjects);
        return semester;
    }
}

