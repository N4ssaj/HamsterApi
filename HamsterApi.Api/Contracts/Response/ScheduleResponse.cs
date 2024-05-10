using HamsterApi.Domain.Common.Enum;
using HamsterApi.Domain.Models;

namespace HamsterApi.Api.Controllers;

public record ScheduleResponse(string Id, int Year, SpringOrAutumn SpringOrAutumn, IReadOnlyCollection<string> GroupsScheduleIds);
