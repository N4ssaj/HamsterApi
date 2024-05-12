using HamsterApi.Domain.Models;

namespace HamsterApi.Api.Contracts.Response;

public record SemesterResponse(string Id, int Number, string GroupId, List<SubjectWtihLoadResponse> Subjects);
