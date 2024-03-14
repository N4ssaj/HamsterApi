

using HamsterApi.Core.Common;

namespace HamsterApi.Core.Models;

public class Direction
{
    private Direction(string id, string title, IReadOnlyCollection<Group> groups)
        => (Id, Title, Groups) = (id, title, groups);
    public string Title { get; }

    public string Id { get; }

    public IReadOnlyCollection<Group> Groups { get; }

    public static Result<Direction> Create(string id, string title, IReadOnlyCollection<Group> groups)
    {
        var direction=new Direction(id, title, groups);
        return direction;
    }
}
