namespace HamsterApi.Api.Contracts.Response;

public record ChairResponse(string Id, string Title, IReadOnlyCollection<string> TeachersIds, string DepartmentId);

