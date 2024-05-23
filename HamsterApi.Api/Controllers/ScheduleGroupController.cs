using HamsterApi.Api.Contracts.Request;
using HamsterApi.Api.Contracts.Response;
using HamsterApi.Api.Helpers;
using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace HamsterApi.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class ScheduleGroupController:ControllerBase
{
    private readonly IScheduleGroupService _scheduleService;

    public ScheduleGroupController(IScheduleGroupService scheduleService)
        => _scheduleService = scheduleService;

    [HttpGet("ids")]
    public async Task<ActionResult<List<ScheduleGroupResponse>?>> ReadByIds([FromQuery] IEnumerable<string> ids)
    {
        var schedules = await _scheduleService.ReadByIds(ids);
        var responce = schedules!.Select(schedule => new ScheduleGroupResponse(schedule.Id, schedule.GroupId, schedule.SemesterNumber, schedule.Weeks.ToList()));

        return Ok(responce);
    }
    [HttpGet("all")]
    public async Task<ActionResult<List<ScheduleGroupResponse>>> ReadAll()
    {

        var schedules = await _scheduleService.ReadAll();
        var responce = schedules!.Select(schedule => new ScheduleGroupResponse(schedule.Id,schedule.GroupId,schedule.SemesterNumber,schedule.Weeks.ToList()));
        return Ok(responce);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ScheduleGroupResponse?>> ReadById(string id)
    {
        var schedule = await _scheduleService.Read(id);
        return schedule is null ? NoContent() : Ok(new ScheduleGroupResponse(schedule.Id, schedule.GroupId, schedule.SemesterNumber, schedule.Weeks.ToList()));
    }
    [HttpPost]
    public async Task<ActionResult<string>> CreateScheduleGroup([FromBody] ScheduleGroupRequest request)
    {
        var item = ScheduleGroup.Create(Guid.NewGuid().ToString(),request.ScheduleId,request.GroupId,request.SemesterNumber,request.Weeks.Select(i=>i.Map()).ToList());
        if (item.Failure)
        {
            return BadRequest(item.Error);
        }
        await _scheduleService.Create(item.Value);
        return Ok(item.Value.Id);
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteScheduleGroup(string id)
    {
        var item = await _scheduleService.Read(id);
        if (item is null) return BadRequest("item is null");
        var isSuccess = await _scheduleService.Delete(id);
        return Ok(isSuccess);
    }
    [HttpPut]
    public async Task<ActionResult<bool>> UpdateScheduleGroup(string id, [FromBody] ScheduleGroupRequest request)
    {
        var item = await _scheduleService.Read(id);
        if (item is null) return BadRequest("item is null");
        var isUpdatet = await _scheduleService.Update(id, request.ScheduleId, request.GroupId, request.SemesterNumber, request.Weeks.Select(i => i.Map()).ToList());
        return Ok(isUpdatet);
    }
}
