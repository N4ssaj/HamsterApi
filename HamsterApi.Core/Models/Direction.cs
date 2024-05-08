

using HamsterApi.Core.Common;
using HamsterApi.Core.Common.Enum;

namespace HamsterApi.Core.Models;

public class Direction
{
    private Direction(string id, string title, List<string> groupsIds,FormOfEducation formOfEducation,LevelOfEducation levelOfEducation,string departmentId)
        => (Id, Title, _groupsIds, FormOfEducation,LevelOfEducation,DepartmentId) = (id, title, groupsIds,formOfEducation,levelOfEducation,departmentId);

    public string Title { get; }

    public string Id { get; }

    private List<string> _groupsIds;

    public IReadOnlyCollection<string> GroupsIds=> _groupsIds;

    public LevelOfEducation  LevelOfEducation { get; }

    public FormOfEducation FormOfEducation { get; }

    public string DepartmentId { get; } 

    public void AddGroup(string groupName)
        =>_groupsIds.Add(groupName);

    public void RemvoveGroup(string groupName)
        =>_groupsIds.Remove(groupName);

    public static Result<Direction> Create(string id, string title, List<string> groupsIds, FormOfEducation formOfEducation,LevelOfEducation levelOfEducation,string departmentId)
    {
        var direction=new Direction(id, title, groupsIds,formOfEducation,levelOfEducation,departmentId);
        return direction;
    }
}
