using HamsterApi.Domain.Models;

namespace HamsterApi.Api.Contracts.Response;

public record ScheduleGroupResponse(string id, string GroupId, int SemesterNumber, IReadOnlyCollection<ScheduledWeek> Weeks);
