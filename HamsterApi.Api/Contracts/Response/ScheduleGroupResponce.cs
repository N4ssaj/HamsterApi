using HamsterApi.Domain.Models;

namespace HamsterApi.Api.Contracts.Response;

public record ScheduleGroupResponce(string id, string GroupId, int SemesterNumber, IReadOnlyCollection<ScheduledClassOfWeeks> Weeks);
