namespace HamsterApi.Api.Contracts.Response;

public record DepartmentResponse(string Id, string Title, IReadOnlyCollection<string> ChairsIds, IReadOnlyCollection<string> DirectionsIds);