using HamsterApi.Domain.Models;

namespace HamsterApi.Api.Contracts.Request;

public record ScheduledWeekRequest(int WeekNumber, List<ScheduledClassOfWeeksRequest> ClassOfWeeks);

