using HamsterApi.Domain.Common.Enum;

namespace HamsterApi.Api.Contracts.Request;

public record DirectionRequest(string Title, List<string> GroupsIds, FormOfEducation FormOfEducation, LevelOfEducation LevelOfEducation, string DepartmentId);
