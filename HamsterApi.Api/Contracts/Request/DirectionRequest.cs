using HamsterApi.Core.Common.Enum;

namespace HamsterApi.Api.Contracts.Request;

public record DirectionRequest(string Title, IReadOnlyCollection<string> GroupsIds, FormOfEducation FormOfEducation, LevelOfEducation LevelOfEducation, string DepartmentId);
