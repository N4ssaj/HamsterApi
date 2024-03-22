
using HamsterApi.Core.Common;

namespace HamsterApi.Core.Models;

public class Department
{
    private Department(string id,string title,IReadOnlyCollection<string> chairsIds, IReadOnlyCollection<string> directionsIds)
        =>(Id,Title,ChairsIds,DirectionsIds)=(id,title,chairsIds,directionsIds);

    public string Title { get; }

    public string Id { get; }

    public IReadOnlyCollection<string> ChairsIds { get; }

    public IReadOnlyCollection<string> DirectionsIds { get; }

    public static Result<Department> Create(string id, string title, IReadOnlyCollection<string> chairsIds, IReadOnlyCollection<string> directionsIds)
    {
        var department = new Department(id, title, chairsIds,directionsIds);
        return department;
    }
}
