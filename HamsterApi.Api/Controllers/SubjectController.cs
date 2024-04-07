using HamsterApi.Api.Contracts.Request;
using HamsterApi.Api.Contracts.Response;
using HamsterApi.Application.Service;
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace HamsterApi.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class SubjectController:ControllerBase
{
    private readonly ISubjectService _subjectService;

    private readonly ITeacherService _teacherService;

    public SubjectController(ISubjectService subjectService, ITeacherService teachreService)
        =>(_subjectService, _teacherService) =(subjectService, teachreService);

    [HttpGet("{id}")]
    public async Task<ActionResult<SubjectResponse?>> ReadById(string id)
    {
        var subject = await _subjectService.Read(id);
        return subject is null ? NoContent() : Ok(new SubjectResponse(subject.Id,subject.Title,subject.Index,subject.TeachersIds));
    }

    [HttpGet("index/{index}")]
    public async Task<ActionResult<SubjectResponse?>> ReadByIndex(string index)
    {
        var subject = await _subjectService.ReadByIndex(index);
        return subject is null ? NoContent() : Ok(new SubjectResponse(subject.Id, subject.Title, subject.Index, subject.TeachersIds));
    }
    [HttpGet("ids")]
    public async Task<ActionResult<List<SubjectResponse>?>> ReadByIds([FromQuery] IEnumerable<string> ids)
    {
        var subjects = await _subjectService.ReadByIds(ids);
        var responce = subjects!.Select(a => new SubjectResponse(a.Id, a.Title, a.Index, a.TeachersIds));

        return Ok(responce);
    }
    [HttpGet("all")]
    public async Task<ActionResult<List<SubjectResponse>?>> ReadAll()
    {
        var subjects = await _subjectService.ReadAll();
        var responce = subjects!.Select(a => new SubjectResponse(a.Id, a.Title, a.Index, a.TeachersIds));
        return Ok(responce);
    }
    [HttpPost]
    public async Task<ActionResult<string>> CreateSubject([FromBody] SubjectRequest request)
    {
        var item = Subject.Create(Guid.NewGuid().ToString(), request.Title, request.Index, []);
        if (item.Failure)
        {
            return BadRequest(item.Error);
        }
        await _subjectService.Create(item.Value);
        await AddTeacher(item.Value.Id, request.TeachersIds);
        return Ok(item.Value.Id);
    }
    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteSubject(string id)
    {
        var item = await _subjectService.Read(id);
        if (item is null) return BadRequest("item is null");
        await RemoveTeacher(id, item.TeachersIds);
        var isSuccess = await _subjectService.Delete(id);

        return Ok(isSuccess);
    }
    [HttpPut]
    public async Task<ActionResult<bool>> UpdateSubject(string id, [FromBody] SubjectRequest request)
    {
        var item = await _subjectService.Read(id);
        if (item is null) return BadRequest("item is null");
        await RemoveTeacher(id, item.TeachersIds);
        await AddTeacher(id,request.TeachersIds);
        var isUpdatet = await _subjectService.Update(id, request.Title, request.Index, request.TeachersIds);

        return Ok(isUpdatet);
    }
    [HttpPut("addteacher")]
    public async Task<ActionResult<bool>> AddTeacher(string id, [FromBody] IEnumerable<string> teacherIds)
    {
        bool b = false;
        b=await _subjectService.AddRangeTeacherById(id, teacherIds);
        foreach (var i in teacherIds)
            await _teacherService.AddSubjectById(i, id);
       return Ok(b);
    }
    [HttpPut("removeteacher")]
    public async Task<ActionResult<bool>> RemoveTeacher(string id, [FromBody] IEnumerable<string> teacherIds)
    {
        bool b = false;
        b = await _subjectService.RemoveRangeTeacherById(id, teacherIds);
        foreach (var i in teacherIds)
            await _teacherService.RemoveSubjectById(i, id);
        return Ok(b);
    }

}
