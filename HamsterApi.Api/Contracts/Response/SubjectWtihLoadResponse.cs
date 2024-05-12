using HamsterApi.Domain.Models;

namespace HamsterApi.Api.Contracts.Response;

public record SubjectWtihLoadResponse(string id, SubjectResponse subject, AcademicLoadResponse academicLoad);
