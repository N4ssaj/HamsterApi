using HamsterApi.Core.Common.Enum;

namespace HamsterApi.Api.Contracts.Response;

public record DirectionResponse(string Id, string Title, IReadOnlyCollection<string> GroupsIds, FormOfEducation FormOfEducation, LevelOfEducation LevelOfEducation, string DepartmentId);
