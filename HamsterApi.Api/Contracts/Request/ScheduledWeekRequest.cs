using HamsterApi.Domain.Models;

namespace HamsterApi.Api.Contracts.Request;

public record ScheduledWeekRequest(string Id,int WeekNumber, List<ScheduledClassOfWeeksRequest> ClassOfWeeks);

