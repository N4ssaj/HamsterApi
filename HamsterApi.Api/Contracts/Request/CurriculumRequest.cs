using HamsterApi.Api.Contracts.Request;
using HamsterApi.Domain.Models;

public record CurriculumRequest(string ChairId, string DepartmentId, string DirectionId, List<SubjectWtihLoadRequest> SemestersSubjects, List<SubjectWtihLoadRequest> SemestersElectiveSubjects, int YarOfPreparation, string FGOSNumber);
