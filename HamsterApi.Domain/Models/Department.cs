
using HamsterApi.Domain.Common;

namespace HamsterApi.Domain.Models;

public class Department
{
    private Department(string id, string title, List<string> chairsIds, List<string> directionsIds)
        => (Id, Title, _chairsIds, _directionsIds) = (id, title, chairsIds, directionsIds);

    public string Title { get; }

    public string Id { get; }

    private List<string> _chairsIds;

    private List<string> _directionsIds;

    public IReadOnlyCollection<string> ChairsIds => _chairsIds;

    public IReadOnlyCollection<string> DirectionsIds => _directionsIds;

    public void AddChair(string chairId)
        => _chairsIds.Add(chairId);

    public void AddDirection(string directionId)
        => _directionsIds.Add(directionId);

    public void RemoveChair(string chairId)
        => _chairsIds.Remove(chairId);

    public void RemoveDirection(string directionId)
        => _directionsIds.Remove(directionId);

    public static Result<Department> Create(string id, string title, List<string> chairsIds, List<string> directionsIds)
    {
        var department = new Department(id, title, chairsIds, directionsIds);
        return department;
    }
}
