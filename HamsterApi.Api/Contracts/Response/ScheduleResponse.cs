using HamsterApi.Core.Models;

namespace HamsterApi.Api.Controllers;

public record ScheduleResponse(string Id, int SemesterNumber, IReadOnlyCollection<ScheduleGroup> GroupsSchedule);
