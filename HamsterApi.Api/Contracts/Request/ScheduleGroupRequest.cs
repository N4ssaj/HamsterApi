using HamsterApi.Domain.Models;

namespace HamsterApi.Api.Contracts.Request;

public record ScheduleGroupRequest(string ScheduleId,string GroupId, int SemesterNumber, List<ScheduledWeekRequest> Weeks);
