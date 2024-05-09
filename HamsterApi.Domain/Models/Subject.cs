

using HamsterApi.Domain.Common;

namespace HamsterApi.Domain.Models;


public class Subject
{
    private Subject(string id, string title, string index, List<string> teachersIds)
        => (Id, Title, Index, _teachersIds) = (id, title, index, teachersIds);

    public string Title { get; } = string.Empty;

    public string Index { get; } = string.Empty;

    private List<string> _teachersIds = [];

    public IReadOnlyCollection<string> TeachersIds => _teachersIds;

    public void AddTeacher(string id)
        => _teachersIds.Add(id);


    public void RemoveTeacher(string id)
        => _teachersIds.Remove(id);


    public string Id { get; } = string.Empty;

    public static Result<Subject> Create(string id, string title, string index, List<string> teachersIds)
    {

        var subject = new Subject(id, title, index, teachersIds);

        return subject;
    }
}
