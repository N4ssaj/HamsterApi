namespace HamsterApi.Api.Contracts.Request;

public record DepartmentRequest(string Title, IReadOnlyCollection<string> ChairsIds,IReadOnlyCollection<string> DirectionsIds);

