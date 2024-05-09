using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;

namespace HamsterApi.Api.Contracts.Request;

public record ScheduleRequest(int SemesterNumber, List<string> GroupsScheduleIds);
