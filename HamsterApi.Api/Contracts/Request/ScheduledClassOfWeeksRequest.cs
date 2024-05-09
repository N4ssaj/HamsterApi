using HamsterApi.Domain.Models;

namespace HamsterApi.Api.Contracts.Request;

public record ScheduledClassOfWeeksRequest(int WeekNumber,DateOnly Date, DayOfWeek DayOfWee, List<ScheduledClassOfWeeksRequest> ScheduledClasses);
