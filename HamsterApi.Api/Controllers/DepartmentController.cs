using HamsterApi.Api.Contracts.Request;
using HamsterApi.Api.Contracts.Response;
using HamsterApi.Application.Service;
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace HamsterApi.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class DepartmentController:ControllerBase
{
    private readonly IChairService _chairService;

    private readonly IDirectionService _directionService;

    private readonly IDepartmentService _departmentService;

    public DepartmentController(IChairService chairService, IDirectionService directionService, IDepartmentService departmentService)
        =>(_chairService, _directionService, _departmentService)=(chairService, directionService,departmentService);

    [HttpGet("{id}")]
    public async Task<ActionResult<DepartmentResponse?>> ReadById(string id)
    {
        var department = await _departmentService.Read(id);
        return department is null ? NoContent() : Ok(new DepartmentResponse(department.Id, department.Title,department.ChairsIds,department.DirectionsIds));
    }
    [HttpGet("ids")]
    public async Task<ActionResult<List<DepartmentResponse>?>> ReadByIds([FromQuery] IEnumerable<string> ids)
    {
        var departments = await _departmentService.ReadByIds(ids);
        var responce = departments!.Select(department => new DepartmentResponse(department.Id, department.Title, department.ChairsIds, department.DirectionsIds));

        return Ok(responce);
    }
    [HttpGet("all")]
    public async Task<ActionResult<List<DepartmentResponse>?>> ReadAll()
    {
        var departments = await _departmentService.ReadAll();
        var responce = departments!.Select(department => new DepartmentResponse(department.Id, department.Title, department.ChairsIds, department.DirectionsIds));
        return Ok(responce);
    }
    [HttpPost]
    public async Task<ActionResult<string>> CreateDepartment([FromBody] DepartmentRequest request)
    {
        var item = Department.Create(Guid.NewGuid().ToString(), request.Title, [], []);
        if (item.Failure)
        {
            return BadRequest(item.Error);
        }
        await _departmentService.Create(item.Value);
        await AddChair(item.Value.Id,request.ChairsIds);
        await AddDirection(item.Value.Id, request.DirectionsIds);
        return Ok(item.Value.Id);
    }
    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteDepartment(string id)
    {
        var item = await _departmentService.Read(id);
        if (item is null) return BadRequest("item is null");
        await RemoveChair(id,item.ChairsIds);
        await RemoveDirection(id,item.DirectionsIds);
        var isSuccess = await _departmentService.Delete(id);

        return Ok(isSuccess);
    }
    [HttpPut]
    public async Task<ActionResult<bool>> UpdateDepartment(string id, [FromBody] DepartmentRequest request)
    {
        var item = await _departmentService.Read(id);
        if (item is null) return BadRequest("item is null");
        await RemoveChair(id, item.ChairsIds);
        await RemoveDirection(id,item.DirectionsIds);
        await AddChair(id,request.ChairsIds);
        await RemoveDirection(id, request.DirectionsIds);
        var isUpdatet = await _departmentService.Update(id, request.Title, request.ChairsIds,request.DirectionsIds);

        return Ok(isUpdatet);
    }
    [HttpPut("addchair")]
    public async Task<ActionResult<bool>> AddChair(string id, [FromBody] IEnumerable<string> chairIds)
    {
        bool b = false;
        b = await _departmentService.AddRangeChairById(id, chairIds);
        foreach (var i in chairIds)
            await _chairService.AddDepartment(i, id);
        return Ok(b);
    }
    [HttpPut("removechair")]
    public async Task<ActionResult<bool>> RemoveChair(string id, [FromBody] IEnumerable<string> chairIds)
    {
        bool b = false;
        b = await _departmentService.RemoveRangeChairById(id, chairIds);
        foreach (var i in chairIds)
            await _chairService.RemoveDepartment(i);
        return Ok(b);
    }
    [HttpPut("adddirection")]
    public async Task<ActionResult<bool>> AddDirection(string id, [FromBody] IEnumerable<string> directionIds)
    {
        bool b = false;
        b = await _departmentService.AddRangeDirectionById(id, directionIds);
        foreach (var i in directionIds)
            await _directionService.AddDepartment(i,id);
        return Ok(b);
    }
    [HttpPut("removedirection")]
    public async Task<ActionResult<bool>> RemoveDirection(string id, [FromBody] IEnumerable<string> directionIds)
    {
        bool b = false;
        b = await _departmentService.RemoveRangeDirectionById(id, directionIds);
        foreach (var i in directionIds)
            await _directionService.RemoveDepartment(i);
        return Ok(b);
    }
}
