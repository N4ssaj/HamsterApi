using HamsterApi.Core.Common.Enum;

namespace HamsterApi.Api.Contracts.Request;

public record ScheduledClassRequest(int ClassNumber, string Subject, string Teacher, string Auditorium, ClassType ClassType);
