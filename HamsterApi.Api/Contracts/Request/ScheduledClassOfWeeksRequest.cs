using HamsterApi.Core.Models;

namespace HamsterApi.Api.Contracts.Request;

public record ScheduledClassOfWeeksRequest(int WeekNumber, DayOfWeek DayOfWee, List<ScheduledClass> ScheduledClasses);
