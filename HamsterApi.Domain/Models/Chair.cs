
using HamsterApi.Domain.Common;

namespace HamsterApi.Domain.Models;

public class Chair
{
    private Chair(string id, string title, List<string> teachersIds, string departmentId)
        => (Id, Title, _teachersIds, DepartmentId) = (id, title, teachersIds, departmentId);

    public string Id { get; }

    public string Title { get; }

    private List<string> _teachersIds;

    public IReadOnlyCollection<string> TeachersIds => _teachersIds;

    public string DepartmentId { get; }

    public void AddTeacher(string teacherId)
        => _teachersIds.Add(teacherId);

    public void RemoveTeacher(string teacherId)
        => _teachersIds.Remove(teacherId);

    public static Result<Chair> Create(string id, string title, List<string> teachersIds, string departmentId)
    {
        var chair = new Chair(id, title, teachersIds, departmentId);
        return chair;
    }
}
