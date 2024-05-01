namespace HamsterApi.Api.Contracts.Request;

public record DepartmentRequest(string Title, List<string> ChairsIds, List<string> DirectionsIds);

