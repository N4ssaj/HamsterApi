using HamsterApi.Domain.Common.Enum;

namespace HamsterApi.Api.Contracts.Request;

public record ScheduledClassRequest(string Id,int ClassNumber, string Subject, string Teacher, string Auditorium, ClassType ClassType);
