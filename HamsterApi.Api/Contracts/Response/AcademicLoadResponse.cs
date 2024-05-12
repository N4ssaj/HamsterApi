using HamsterApi.Domain.Common.Enum;

namespace HamsterApi.Api.Contracts.Response;

public record AcademicLoadResponse(string Id, int Lectures, int Laboratory, int Practice, int Credits, int Total, AcademicEvaluationType AcademicEvaluationType);
