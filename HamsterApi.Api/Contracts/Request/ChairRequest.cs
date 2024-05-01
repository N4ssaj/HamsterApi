namespace HamsterApi.Api.Contracts.Request;

public record ChairRequest(string Title, List<string> TeachersIds, string DepartmentId);
