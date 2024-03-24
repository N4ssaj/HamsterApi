using HamsterApi.Core.Common.Enum;

namespace HamsterApi.Api.Contracts.Response;

public record GroupResponse(string Id,string Number, LevelOfEducation LevelOfEducation, string DirectionId);
