

using HamsterApi.Core.Common;
using HamsterApi.Core.Common.Enum;

namespace HamsterApi.Core.Models;

public class Direction
{
    private Direction(string id, string title, IReadOnlyCollection<string> groupsIds,FormOfEducation formOfEducation,LevelOfEducation levelOfEducation,string departmentId)
        => (Id, Title, GroupsIds,FormOfEducation,LevelOfEducation,DepartmentId) = (id, title, groupsIds,formOfEducation,levelOfEducation,departmentId);

    public string Title { get; }

    public string Id { get; }

    public IReadOnlyCollection<string> GroupsIds { get; }

    public LevelOfEducation  LevelOfEducation { get; }

    public FormOfEducation FormOfEducation { get; }

    public string DepartmentId { get; }

    public static Result<Direction> Create(string id, string title, IReadOnlyCollection<string> groupsIds, FormOfEducation formOfEducation,LevelOfEducation levelOfEducation,string departmentId)
    {
        var direction=new Direction(id, title, groupsIds,formOfEducation,levelOfEducation,departmentId);
        return direction;
    }
}
