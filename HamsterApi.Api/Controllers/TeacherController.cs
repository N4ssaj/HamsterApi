using HamsterApi.Api.Contracts.Request;
using HamsterApi.Api.Contracts.Response;
using HamsterApi.Application.Service;
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.ComponentModel;

namespace HamsterApi.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TeacherController:ControllerBase
{
    private readonly ISubjectService _subjectService;

    private readonly ITeacherService _teacherService;

    private readonly IChairService _chairService;

    public TeacherController(ISubjectService subjectService, ITeacherService teachreService, IChairService chairService)
        => (_subjectService, _teacherService,_chairService) = (subjectService, teachreService,chairService);

    [HttpGet("ids")]
    public async Task<ActionResult<List<TeacherResponse>?>> ReadByIds([FromQuery] IEnumerable<string> ids)
    {
        var teachers = await _teacherService.ReadByIds(ids);
        var responce = teachers!.Select(teacher => new TeacherResponse(teacher.Id, teacher.Name, teacher.Surname, teacher.Patronymic, teacher.SubjectsIds, teacher.ChairId, teacher.TeacherLoadId));

        return Ok(responce);
    }
    [HttpGet("all")]
    public async Task<ActionResult<List<TeacherResponse>>> ReadAll()
    {
        var teachers = await _teacherService.ReadAll();
        var responce = teachers!.Select(teacher => new TeacherResponse(teacher.Id, teacher.Name, teacher.Surname, teacher.Patronymic, teacher.SubjectsIds, teacher.ChairId, teacher.TeacherLoadId));
        return Ok(responce);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<TeacherResponse?>> ReadById(string id)
    {
        var teacher = await _teacherService.ReadByIds([id]);
        return teacher[0] is null ? NoContent() : Ok(new TeacherResponse(teacher[0].Id,teacher[0].Name,teacher[0].Surname,teacher[0].Patronymic,teacher[0].SubjectsIds,teacher[0].ChairId,teacher[0].TeacherLoadId));
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateTeacher([FromBody] TeacherRequest request)
    {
        var item = Teacher.Create(Guid.NewGuid().ToString(), request.Name, request.Surname, request.Patronymic,[],string.Empty,request.TeacherLoadId);
        if (item.Failure)
        {
            return BadRequest(item.Error);
        }
        await _teacherService.Create(item.Value);
        await AddChair(item.Value.Id, request.ChairId);
        await AddSubject(item.Value.Id, request.SubjectsIds);
        return Ok(item.Value.Id);
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteTeacher(string id)
    {
        var item = await _teacherService.ReadByIds([id]);
        await RemoveSubject(id, item[0].SubjectsIds);
        await RemoveChair(id, item[0].ChairId);
        var isSuccess = await _teacherService.Delete(id);

        return Ok(isSuccess);
    }

    [HttpPut]
    public async Task<ActionResult<bool>> UpdateTeacher(string id, [FromBody] TeacherRequest request)
    {
        var item = await _teacherService.ReadByIds([id]);
        await RemoveChair(id, item[0].ChairId);
        await RemoveSubject(id, item[0].SubjectsIds);
        await AddSubject(id, request.SubjectsIds);
        await AddChair(id, request.ChairId);
        var isUpdatet = await _teacherService.Update(id, request.Name,request.Surname,request.Patronymic,request.SubjectsIds,request.ChairId,request.TeacherLoadId);

        return Ok(isUpdatet);
    }
    [HttpPut("addsubject")]
    public async Task<ActionResult<bool>> AddSubject(string id, [FromBody] IEnumerable<string> subjectIds)
    {
        bool b = false;
        b = await _teacherService.AddRangeSubjectById(id, subjectIds);
        foreach (var i in subjectIds)
            await _subjectService.AddTeacherById(i, id);
        return Ok(b);
    }
    [HttpPut("removesubject")]
    public async Task<ActionResult<bool>> RemoveSubject(string id, [FromBody] IEnumerable<string> subjectIds)
    {
        bool b = false;
        b = await _teacherService.RemoveRangeSubjectById(id, subjectIds);
        foreach (var i in subjectIds)
            await _subjectService.RemoveTeacherById(i, id);
        return Ok(b);
    }
    [HttpPut("setchair")]
    public async Task<ActionResult<bool>> AddChair(string id, string chairId)
    {
        bool b = await _teacherService.AddChair(id, chairId);
        await _chairService.AddRangeTeacherById(chairId, [id]);
        return Ok(b);
    }
    [HttpPut("removechair")]
    public async Task<ActionResult<bool>> RemoveChair(string id, string chairId)
    {
        bool b = await _teacherService.RemoveChair(id);
        await _chairService.RemoveRangeTeacherById(chairId, [id]);
        return Ok(b);
    }
}
