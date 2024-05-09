using HamsterApi.Domain.Models;

namespace HamsterApi.Api.Controllers;

public record ScheduleResponse(string Id, int SemesterNumber, IReadOnlyCollection<string> GroupsScheduleIds);
