using HamsterApi.Api.Contracts.Request;
using HamsterApi.Api.Contracts.Response;
using HamsterApi.Application.Service;
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface;
using HamsterApi.Core.Stores;
using Microsoft.AspNetCore.Mvc;

namespace HamsterApi.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ScheduleController:ControllerBase
{
    private readonly IScheduleService _scheduleService;

    public ScheduleController(IScheduleService scheduleService)
        =>_scheduleService = scheduleService;

    [HttpGet("ids")]
    public async Task<ActionResult<List<ScheduleResponse>?>> ReadByIds([FromQuery] IEnumerable<string> ids)
    {
        var schedules = await _scheduleService.ReadByIds(ids);
        var responce = schedules!.Select(schedule => new ScheduleResponse(schedule.Id,schedule.SemesterNumber,schedule.GroupsSchedule));

        return Ok(responce);
    }
    [HttpGet("all")]
    public async Task<ActionResult<List<ScheduleResponse>>> ReadAll()
    {
        var schedules = await _scheduleService.ReadAll();
        var responce = schedules!.Select(schedule => new ScheduleResponse(schedule.Id, schedule.SemesterNumber, schedule.GroupsSchedule));
        return Ok(responce);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ScheduleResponse?>> ReadById(string id)
    {
        var Schedule = await _scheduleService.Read(id);
        return Schedule is null ? NoContent() : Ok(new ScheduleResponse(Schedule.Id,Schedule.SemesterNumber,Schedule.GroupsSchedule));
    }
    [HttpPost]
    public async Task<ActionResult<string>> CreateSchedule([FromBody] ScheduleRequest request)
    {
        var item = Schedule.Create(Guid.NewGuid().ToString(),request.SemesterNumber,request.GroupsSchedule.ToList());
        if (item.Failure)
        {
            return BadRequest(item.Error);
        }
        await _scheduleService.Create(item.Value);
        return Ok(item.Value.Id);
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteSchedule(string id)
    {
        var item = await _scheduleService.Read(id);
        if (item is null) return BadRequest("item is null");
        var isSuccess = await _scheduleService.Delete(id);
        return Ok(isSuccess);
    }
    [HttpPut]
    public async Task<ActionResult<bool>> UpdateSchedule(string id, [FromBody] ScheduleRequest request)
    {
        var item = await _scheduleService.Read(id);
        if (item is null) return BadRequest("item is null");
        var isUpdatet = await _scheduleService.Update(id, request.SemesterNumber,request.GroupsSchedule);
        return Ok(isUpdatet);
    }
    [HttpPost("addgroup")]
    public async Task<ActionResult<string>> AddGroup(string id,[FromBody] ScheduleGroupRequest request)
    {
        var item = ScheduleGroup.Create(Guid.NewGuid().ToString(), request.GroupId,request.SemesterNumber, request.Weeks).Value;
        await _scheduleService.AddGroup(id,item);
        return Ok(item.Id);
    }
    [HttpDelete("removegroup")]
    public async Task<ActionResult<bool>> RemoveGroup(string id,string groupId)
    {
        var result=await _scheduleService.RemoveGroup(id, groupId);
        return Ok(result);
    }
    [HttpPost("addweek")]
    public async Task<ActionResult<string>> AddWeek(string id,string groupId,[FromBody] ScheduledClassOfWeeksRequest request)
    {
        var item = ScheduledClassOfWeeks.Create(Guid.NewGuid().ToString(), request.WeekNumber, request.DayOfWee, request.ScheduledClasses).Value;
        await _scheduleService.AddWeeksOfGroup(id,groupId,item);
        return Ok(item.Id);
    }
    [HttpDelete("removeweek")]
    public async Task<ActionResult<bool>> RemoveWeek(string id, string groupId, string weekId)
    {
        var result= await _scheduleService.RemoveWeeksOfGroup(id, groupId,weekId);
        return Ok(result);
    }
    [HttpPost("addclass")]
    public async Task<ActionResult<string>> AddClass(string id,string groupId,string weekId, [FromBody] ScheduledClassRequest request)
    {
        var item = ScheduledClass.Create(Guid.NewGuid().ToString(), request.ClassNumber, request.Subject, request.Teacher, request.Auditorium, request.ClassType).Value;
        await _scheduleService.AddClassOfWeek(id, groupId, weekId, item);
        return Ok(item.Id);
    }
    [HttpDelete("removeclass")]
    public async Task<ActionResult<bool>> RemoveClass(string id,string groupId,string weekId,string classId)
    {
        var result=await _scheduleService.RemoveClassOfWeek(id,groupId,weekId,classId);
        return Ok(result);
    }
}
