namespace HamsterApi.Api.Contracts.Request;

public record ChairRequest(string Title, IReadOnlyCollection<string> TeachersIds, string DepartmentId);
