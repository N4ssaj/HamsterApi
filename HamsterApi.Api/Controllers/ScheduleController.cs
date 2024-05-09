using HamsterApi.Api.Contracts.Request;
using HamsterApi.Api.Contracts.Response;
using HamsterApi.Application.Service;
using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface;
using HamsterApi.Domain.RepositoriesInterfaces;
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
        var responce = schedules!.Select(schedule => new ScheduleResponse(schedule.Id,schedule.SemesterNumber,schedule.GroupsScheduleIds));

        return Ok(responce);
    }
    [HttpGet("all")]
    public async Task<ActionResult<List<ScheduleResponse>>> ReadAll()
    {
        var schedules = await _scheduleService.ReadAll();
        var responce = schedules!.Select(schedule => new ScheduleResponse(schedule.Id, schedule.SemesterNumber, schedule.GroupsScheduleIds));
        return Ok(responce);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ScheduleResponse?>> ReadById(string id)
    {
        var Schedule = await _scheduleService.Read(id);
        return Schedule is null ? NoContent() : Ok(new ScheduleResponse(Schedule.Id,Schedule.SemesterNumber,Schedule.GroupsScheduleIds));
    }
    [HttpPost]
    public async Task<ActionResult<string>> CreateSchedule([FromBody] ScheduleRequest request)
    {
        var item = Schedule.Create(Guid.NewGuid().ToString(),request.SemesterNumber,request.GroupsScheduleIds);
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
        var isUpdatet = await _scheduleService.Update(id, request.SemesterNumber,request.GroupsScheduleIds);
        return Ok(isUpdatet);
    }
    [HttpPost("addgroup")]
    public async Task<ActionResult<string>> AddGroup(string id,IEnumerable<string> groupIds)
    {
        var result = await _scheduleService.AddGroup(id,groupIds);
        return Ok(result);
    }
    [HttpDelete("removegroup")]
    public async Task<ActionResult<bool>> RemoveGroup(string id,IEnumerable<string> groupIds)
    {
        var result=await _scheduleService.RemoveGroup(id, groupIds);
        return Ok(result);
    }
}
