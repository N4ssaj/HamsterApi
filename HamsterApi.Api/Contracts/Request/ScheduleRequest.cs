using HamsterApi.Core.Common.Enum;
using HamsterApi.Core.Models;

namespace HamsterApi.Api.Contracts.Request;

public record ScheduleRequest(int SemesterNumber, List<ScheduleGroup> GroupsSchedule);
