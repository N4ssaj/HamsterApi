using HamsterApi.Api.Contracts.Request;
using HamsterApi.Api.Contracts.Response;
using HamsterApi.Domain.Models;

namespace HamsterApi.Api.Helpers;

internal static class ScheduledGroupWeeksMap
{
    public static ScheduledWeek Map(this ScheduledWeekRequest request)
    {
        var itemClassOfWeek=request.ClassOfWeeks.Select(i=>ScheduledClassOfWeeks.Create(i.Id,i.DayOfWee,i.ScheduledClasses.Select(a=>ScheduledClass.Create(a.Id,a.ClassNumber,a.Subject,a.Teacher,a.Auditorium,a.ClassType).Value).ToList(),i.Date).Value).ToList();
        var item = ScheduledWeek.Create(request.Id, request.WeekNumber, itemClassOfWeek).Value;
        return item;
    }
}
