using HamsterApi.Api.Contracts.Response;

namespace HamsterApi.Api.Contracts.Request;

public record SemesterRequest(int Number, string GroupId, List<SubjectWtihLoadRequest> Subjects);
