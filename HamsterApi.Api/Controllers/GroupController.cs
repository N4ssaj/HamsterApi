using HamsterApi.Api.Contracts.Request;
using HamsterApi.Api.Contracts.Responce;
using HamsterApi.Api.Contracts.Response;
using HamsterApi.Application.Service;
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace HamsterApi.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class GroupController:ControllerBase
{
    private readonly IGroupService _groupService;

    private readonly IDirectionService _directionService;

    public GroupController(IGroupService groupService, IDirectionService directionService)
    => (_groupService, _directionService) = (groupService, directionService);

    [HttpGet("{id}")]
    public async Task<ActionResult<GroupResponse?>> ReadById(string id)
    {
        var group = await _groupService.Read(id);
        return group is null ? NoContent() : Ok(new GroupResponse(group.Id,group.Number,group.LevelOfEducation,group.DirectionId));
    }

    [HttpGet("number/{number}")]
    public async Task<ActionResult<GroupResponse?>> ReadByNumber(string number)
    {
        var group = await _groupService.ReadByNumber(number);
        return group is null ? NoContent() : Ok(new GroupResponse(group.Id, group.Number, group.LevelOfEducation, group.DirectionId));
    }
    [HttpGet("ids")]
    public async Task<ActionResult<List<GroupResponse>?>> ReadByIds([FromQuery] IEnumerable<string> ids)
    {
        var groups = await _groupService.ReadByIds(ids);
        var responce = groups!.Select(a => new GroupResponse(a.Id, a.Number, a.LevelOfEducation, a.DirectionId));

        return Ok(responce);
    }
    [HttpGet("all")]
    public async Task<ActionResult<List<GroupResponse>?>> ReadAll()
    {
        var groups = await _groupService.ReadAll();
        var responce = groups!.Select(a => new GroupResponse(a.Id, a.Number, a.LevelOfEducation, a.DirectionId));
        return Ok(responce);
    }
    [HttpPost]
    public async Task<ActionResult<string>> CreateGroup([FromBody] GroupRequest request)
    {
        var item = Group.Create(Guid.NewGuid().ToString(), request.Number,request.LevelOfEducation,string.Empty);
        if (item.Failure)
        {
            return BadRequest(item.Error);
        }
        await _groupService.Create(item.Value);
        await AddDirection(item.Value.Id, request.DirectionId);
        return Ok(item.Value.Id);
    }
    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteGroup(string id)
    {
        var item = await _groupService.Read(id);
        if (item is null) return BadRequest("item is null");
        await RemoveDirection(id,item.DirectionId);
        var isSuccess = await _groupService.Delete(id);

        return Ok(isSuccess);
    }
    [HttpPut]
    public async Task<ActionResult<bool>> UpdateGroup(string id, [FromBody] GroupRequest request)
    {
        var item = await _groupService.Read(id);
        if (item is null) return BadRequest("item is null");
        await RemoveDirection(id, item.DirectionId);
        await AddDirection(id, request.DirectionId);
        var isUpdatet = await _groupService.Update(id, request.Number,request.LevelOfEducation,request.DirectionId);

        return Ok(isUpdatet);
    }
    [HttpPut("setdirection")]
    public async Task<ActionResult<bool>> AddDirection(string id,string directionId)
    {
        bool b = false;
        b= await _groupService.AddDirection(id, directionId);
        await _directionService.AddGroupById(directionId, id);
        return Ok(b);
    }
    [HttpPut("removedirection")]
    public async Task<ActionResult<bool>> RemoveDirection(string id,string directionId)
    {
        bool b = false;
        b = await _groupService.RemoveDirection(id);

        await _directionService.RemoveGroupById(directionId, id);

        return Ok(b);
    }
}
