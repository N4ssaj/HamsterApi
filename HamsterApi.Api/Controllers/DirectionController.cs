using HamsterApi.Api.Contracts.Request;
using HamsterApi.Api.Contracts.Response;
using HamsterApi.Application.Service;
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace HamsterApi.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class DirectionController:ControllerBase
{
    private readonly IDirectionService _directionService;

    private readonly IGroupService _groupService;

    public DirectionController(IDirectionService directionService, IGroupService groupService)
        =>(_directionService, _groupService)=(directionService, groupService);


    [HttpGet("{id}")]
    public async Task<ActionResult<Direction?>> ReadById(string id)
    {
        var direction = await _directionService.Read(id);
        return direction is null ? NoContent() : Ok(new DirectionResponse(direction.Id,direction.Title,direction.GroupsIds,direction.FormOfEducation,direction.LevelOfEducation,direction.DepartmentId));
    }
    [HttpGet("ids")]
    public async Task<ActionResult<List<Direction>?>> ReadByIds([FromQuery] IEnumerable<string> ids)
    {
        var directions = await _directionService.ReadByIds(ids);
        var responce = directions!.Select(direction => new DirectionResponse(direction.Id, direction.Title, direction.GroupsIds, direction.FormOfEducation, direction.LevelOfEducation, direction.DepartmentId));

        return Ok(responce);
    }
    [HttpGet("all")]
    public async Task<ActionResult<List<Direction>?>> ReadAll()
    {
        var directions = await _directionService.ReadAll();
        var responce = directions!.Select(direction => new DirectionResponse(direction.Id, direction.Title, direction.GroupsIds, direction.FormOfEducation, direction.LevelOfEducation, direction.DepartmentId));
        return Ok(responce);
    }
    [HttpPost]
    public async Task<ActionResult<string>> CreateDirection([FromBody] DirectionRequest request)
    {
        var item = Direction.Create(Guid.NewGuid().ToString(), request.Title, [],request.FormOfEducation,request.LevelOfEducation,request.DepartmentId);
        if (item.Failure)
        {
            return BadRequest(item.Error);
        }
        await _directionService.Create(item.Value);
        await AddGroup(item.Value.Id, request.GroupsIds);
        return Ok(item.Value.Id);
    }
    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteDirection(string id)
    {
        var item = await _directionService.Read(id);
        if (item is null) return BadRequest("item is null");
        await RemoveGroup(id, item.GroupsIds);
        var isSuccess = await _directionService.Delete(id);

        return Ok(isSuccess);
    }
    [HttpPut]
    public async Task<ActionResult<bool>> UpdateDirection(string id, [FromBody] DirectionRequest request)
    {
        var item = await _directionService.Read(id);
        if (item is null) return BadRequest("item is null");
        await RemoveGroup(id, item.GroupsIds);
        await AddGroup(id, request.GroupsIds);
        var isUpdatet = await _directionService.Update(id,request.Title,request.GroupsIds,request.FormOfEducation,request.LevelOfEducation,request.DepartmentId);

        return Ok(isUpdatet);
    }
    [HttpPut("addgroup")]
    public async Task<ActionResult<bool>> AddGroup(string id, [FromBody] IEnumerable<string> groupsIds)
    {
        bool b = false;
        b = await _directionService.AddRangeGroupById(id, groupsIds);
        foreach (var i in groupsIds)
            await _groupService.AddDirection(i,id);
        return Ok(b);
    }
    [HttpPut("removegroup")]
    public async Task<ActionResult<bool>> RemoveGroup(string id, [FromBody] IEnumerable<string> groupsIds)
    {
        bool b = false;
        b = await _directionService.RemoveRangeGroupById(id, groupsIds);
        foreach (var i in groupsIds)
            await _groupService.RemoveDirection(i);
        return Ok(b);
    }
}
