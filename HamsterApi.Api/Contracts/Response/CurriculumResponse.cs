using HamsterApi.Domain.Models;

namespace HamsterApi.Api.Contracts.Response;

public record CurriculumResponse(string Id, string ChairId, string DepartmentId, string DirectionId, List<SubjectWtihLoadResponse> SemestersSubjects, List<SubjectWtihLoadResponse> SemestersElectiveSubjects, int YarOfPreparation, string FGOSNumber);
