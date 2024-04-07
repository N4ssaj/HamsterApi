using HamsterApi.Api.Contracts.Request;
using HamsterApi.Api.Contracts.Response;
using HamsterApi.Application.Service;
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace HamsterApi.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class ChairController:ControllerBase
{
    private readonly IChairService _chairService;

    private readonly ITeacherService _teacherService;

    private readonly IDepartmentService _departmentService;

    public ChairController(IChairService chairService,ITeacherService teacherService,IDepartmentService departmentService)
        =>(_chairService,_teacherService, _departmentService) =(chairService, teacherService,departmentService);

    [HttpGet("ids")]
    public async Task<ActionResult<List<ChairResponse>?>> ReadByIds([FromQuery] IEnumerable<string> ids)
    {
        var chairs = await _chairService.ReadByIds(ids);
        var responce = chairs!.Select(chair => new ChairResponse(chair.Id,chair.Title,chair.TeachersIds,chair.DepartmentId));

        return Ok(responce);
    }
    [HttpGet("all")]
    public async Task<ActionResult<List<ChairResponse>>> ReadAll()
    {
        var chairs = await _chairService.ReadAll();
        var responce = chairs!.Select(chair => new ChairResponse(chair.Id, chair.Title, chair.TeachersIds, chair.DepartmentId));
        return Ok(responce);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<ChairResponse?>> ReadById(string id)
    {
        var chair = await _chairService.Read(id);
        return chair is null ? NoContent() : Ok(new ChairResponse(chair.Id, chair.Title, chair.TeachersIds, chair.DepartmentId));
    }
    [HttpPost]
    public async Task<ActionResult<string>> CreateChair([FromBody] ChairRequest request)
    {
        var item = Chair.Create(Guid.NewGuid().ToString(), request.Title, [],request.DepartmentId);
        if (item.Failure)
        {
            return BadRequest(item.Error);
        }
        await _chairService.Create(item.Value);
        await AddTeacher(item.Value.Id, request.TeachersIds);
        await SetDepartment(item.Value.Id, request.DepartmentId);
        return Ok(item.Value.Id);
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteChair(string id)
    {
        var item = await _chairService.Read(id);
        if (item is null) return BadRequest("item is null");
        await RemoveTeacher(id, item.TeachersIds);
        await RemoveDepartment(id, item.DepartmentId);
        var isSuccess = await _chairService.Delete(id);

        return Ok(isSuccess);
    }

    [HttpPut]
    public async Task<ActionResult<bool>> UpdateChair(string id, [FromBody] ChairRequest request)
    {
        var item = await _chairService.Read(id);
        if (item is null) return BadRequest("item is null");
        await RemoveTeacher(id, item.TeachersIds);
        await AddTeacher(id, request.TeachersIds);
        await RemoveDepartment(id, item.DepartmentId);
        await SetDepartment(id, request.DepartmentId);
        var isUpdatet = await _chairService.Update(id,request.Title,request.TeachersIds,request.DepartmentId);

        return Ok(isUpdatet);
    }
    [HttpPut("addteacher")]
    public async Task<ActionResult<bool>> AddTeacher(string id, [FromBody] IEnumerable<string> teachersIds)
    {
        bool b = false;
        b = await _chairService.AddRangeTeacherById(id, teachersIds);
        foreach (var i in teachersIds)
            await _teacherService.AddChair(i, id);
        return Ok(b);
    }
    [HttpPut("removeteacher")]
    public async Task<ActionResult<bool>> RemoveTeacher(string id, [FromBody] IEnumerable<string> teachersIds)
    {
        bool b = false;
        b = await _chairService.RemoveRangeTeacherById(id, teachersIds);
        foreach (var i in teachersIds)
            await _teacherService.RemoveChair(i);
        return Ok(b);
    }
    [HttpPut("setdepartment")]
    public async Task<ActionResult<bool>> SetDepartment(string id, string departmentId)
    {
        bool b = await _chairService.AddDepartment(id, departmentId);
        await _departmentService.AddRangeChairById(departmentId, [id]);
        return Ok(b);
    }
    [HttpPut("removedepartment")]
    public async Task<ActionResult<bool>> RemoveDepartment(string id, string departmentId)
    {
        bool b = await _chairService.RemoveDepartment(id);
        await _departmentService.RemoveRangeChairById(departmentId, [id]);
        return Ok(b);
    }
}
