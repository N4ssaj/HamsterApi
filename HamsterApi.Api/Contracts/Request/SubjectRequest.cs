namespace HamsterApi.Api.Contracts.Request;

public record SubjectRequest(string Title, string Index, List<string> TeachersIds);
