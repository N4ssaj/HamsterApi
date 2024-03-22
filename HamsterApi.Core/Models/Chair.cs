
using HamsterApi.Core.Common;

namespace HamsterApi.Core.Models;

public class Chair
{
    private Chair(string id, string title, IReadOnlyCollection<string> teachersIds, string departmentId)
        => (Id, Title, TeachersIds,DepartmentId) = (id, title, teachersIds,departmentId);

    public string Id { get; }

    public string Title { get; }

    public IReadOnlyCollection<string> TeachersIds { get; }

    public string DepartmentId { get; }

    public static Result<Chair> Create(string id, string title, IReadOnlyCollection<string> teachersIds, string departmentId)
    {
        var chair = new Chair(id, title, teachersIds,departmentId);
        return chair;
    }
}
