namespace HamsterApi.Api.Contracts.Response;

public record SubjectResponse(string Id,string Title, string Index, IReadOnlyCollection<string> TeachersIds);
