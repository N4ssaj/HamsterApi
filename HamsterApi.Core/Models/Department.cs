
using HamsterApi.Core.Common;

namespace HamsterApi.Core.Models;

public class Department
{
    private Department(string id, string title, IReadOnlyCollection<Teacher> teachers, Faculty faculty)
        => (Id, Title, Teachers,Faculty) = (id, title, teachers,faculty);

    public string Id { get; }

    public string Title { get; }

    public IReadOnlyCollection<Teacher> Teachers { get; }

    public Faculty Faculty { get; }

    public static Result<Department> Create(string id, string title, IReadOnlyCollection<Teacher> teachers, Faculty faculty)
    {
        var department = new Department(id, title, teachers,faculty);
        return department;
    }
}
