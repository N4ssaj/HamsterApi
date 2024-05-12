namespace HamsterApi.Api.Contracts.Request;

public record SubjectLoadRequest(string Id,string Title, string Index, List<string> TeachersIds);