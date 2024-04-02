using HamsterApi.Api.Contracts.Request;
using HamsterApi.Api.Contracts.Response;
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace HamsterApi.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TeacherController:ControllerBase
{
    private readonly ISubjectService _subjectService;

    private readonly ITeacherService _teacherService;

    public TeacherController(ISubjectService subjectService, ITeacherService teachreService)
        => (_subjectService, _teacherService) = (subjectService, teachreService);

    [HttpGet("ids")]
    public async Task<ActionResult<List<Teacher>?>> ReadByIds([FromQuery] IEnumerable<string> ids)
    {
        var teachers = await _teacherService.ReadByIds(ids);
        var responce = teachers!.Select(teacher => new TeacherResponse(teacher.Id, teacher.Name, teacher.Surname, teacher.Patronymic, teacher.SubjectsIds, teacher.ChairId, teacher.TeacherLoadId));

        return Ok(responce);
    }
    [HttpGet("all")]
    public async Task<ActionResult<List<Teacher>>> ReadAll()
    {
        var teachers = await _teacherService.ReadAll();
        var responce = teachers!.Select(teacher => new TeacherResponse(teacher.Id, teacher.Name, teacher.Surname, teacher.Patronymic, teacher.SubjectsIds, teacher.ChairId, teacher.TeacherLoadId));
        return Ok(responce);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Teacher?>> ReadById(string id)
    {
        var teacher = await _teacherService.ReadByIds([id]);
        return teacher[0] is null ? NoContent() : Ok(new TeacherResponse(teacher[0].Id,teacher[0].Name,teacher[0].Surname,teacher[0].Patronymic,teacher[0].SubjectsIds,teacher[0].ChairId,teacher[0].TeacherLoadId));
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateTeacher([FromBody] TeacherRequest request)
    {
        var item = Teacher.Create(Guid.NewGuid().ToString(), request.Name, request.Surname, request.Patronymic,[],request.ChairId,request.TeacherLoadId);
        if (item.Failure)
        {
            return BadRequest(item.Error);
        }
        await _teacherService.Create(item.Value);
        await AddSubject(item.Value.Id, request.SubjectsIds);
        return Ok(item.Value.Id);
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteTeacher(string id)
    {
        var item = await _teacherService.ReadByIds([id]);
        //if (item.Count > 0) return BadRequest("item is not found");
        await RemoveSubject(id, item[0].SubjectsIds);
        
        var isSuccess = await _teacherService.Delete(id);

        return Ok(isSuccess);
    }

    [HttpPut]
    public async Task<ActionResult<bool>> UpdateSubject(string id, [FromBody] TeacherRequest request)
    {
        var item = await _teacherService.ReadByIds([id]);
        if (item.Count > 0 ) return BadRequest("item is null");
        await RemoveSubject(id, item[0].SubjectsIds);
        await AddSubject(id, request.SubjectsIds);
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
}
