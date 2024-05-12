using HamsterApi.Api.Contracts.Response;

namespace HamsterApi.Api.Contracts.Request;

public record SubjectWtihLoadRequest(SubjectLoadRequest Subject, AcademicLoadRequest AcademicLoad);
