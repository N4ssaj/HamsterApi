using HamsterApi.Domain.Models;

namespace HamsterApi.Api.Contracts.Request;

public record ScheduledClassOfWeeksRequest(string Id,int WeekNumber,DateOnly Date, DayOfWeek DayOfWee, List<ScheduledClassRequest> ScheduledClasses);
