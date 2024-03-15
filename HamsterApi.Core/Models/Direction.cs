

using HamsterApi.Core.Common;
using HamsterApi.Core.Common.Enum;

namespace HamsterApi.Core.Models;

public class Direction
{
    private Direction(string id, string title, IReadOnlyCollection<Group> groups,FormOfEducation formOfEducation,LevelOfEducation levelOfEducation)
        => (Id, Title, Groups,FormOfEducation,LevelOfEducation) = (id, title, groups,formOfEducation,levelOfEducation);

    public string Title { get; }

    public string Id { get; }

    public IReadOnlyCollection<Group> Groups { get; }

    public LevelOfEducation  LevelOfEducation { get; }

    public FormOfEducation FormOfEducation { get; }

    public static Result<Direction> Create(string id, string title, IReadOnlyCollection<Group> groups, FormOfEducation formOfEducation,LevelOfEducation levelOfEducation)
    {
        var direction=new Direction(id, title, groups,formOfEducation,levelOfEducation);
        return direction;
    }
}
