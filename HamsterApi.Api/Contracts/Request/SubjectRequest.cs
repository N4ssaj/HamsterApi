namespace HamsterApi.Api.Contracts.Request;

public record SubjectRequest(string Title, string Index, IReadOnlyCollection<string> TeachersIds);
