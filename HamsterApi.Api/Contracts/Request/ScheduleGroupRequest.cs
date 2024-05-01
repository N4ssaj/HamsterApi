using HamsterApi.Core.Models;

namespace HamsterApi.Api.Contracts.Request;

public record ScheduleGroupRequest(string GroupId, int SemesterNumber, List<ScheduledClassOfWeeks> Weeks);
