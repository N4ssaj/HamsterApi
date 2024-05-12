using HamsterApi.Domain.Common.Enum;

namespace HamsterApi.Api.Contracts.Request;

public record AcademicLoadRequest(string Id,int Lectures, int Laboratory, int Practice, int Credits, int Total, AcademicEvaluationType AcademicEvaluationType);
