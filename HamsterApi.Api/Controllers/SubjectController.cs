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

    public SubjectController(ISubjectService subjectService)
        =>_subjectService =subjectService;

    [HttpGet("{id}")]
    public async Task<ActionResult<Subject?>> ReadById(string id)
    {
        var subject = await _subjectService.Read(id);
        return subject is null ? NoContent() : Ok(new SubjectResponse(subject.Id,subject.Title,subject.Index,subject.TeachersIds));
    }

    [HttpGet("number/{number}")]
    public async Task<ActionResult<Subject?>> ReadByIndex(string number)
    {
        var subject = await _subjectService.ReadByIndex(number);
        return subject is null ? NoContent() : Ok(new SubjectResponse(subject.Id, subject.Title, subject.Index, subject.TeachersIds));
    }
    [HttpGet("ids")]
    public async Task<ActionResult<List<Subject>?>> ReadByIds([FromQuery] IEnumerable<string> ids)
    {
        var subjects = await _subjectService.ReadByIds(ids);
        var responce = subjects!.Select(a => new SubjectResponse(a.Id, a.Title, a.Index, a.TeachersIds));

        return Ok(responce);
    }
    [HttpGet("all")]
    public async Task<ActionResult<List<Subject>?>> ReadAll()
    {
        var subjects = await _subjectService.ReadAll();
        var responce = subjects!.Select(a => new SubjectResponse(a.Id, a.Title, a.Index, a.TeachersIds));
        return Ok(responce);
    }
    [HttpPost]
    public async Task<ActionResult<string>> CreateSubject([FromBody] SubjectRequest request)
    {
        var item = Subject.Create(Guid.NewGuid().ToString(), request.Title, request.Index, request.TeachersIds);
        if (item.Failure)
        {
            return BadRequest(item.Error);
        }
        await _subjectService.Create(item.Value);

        return Ok(item.Value.Id);
    }
    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteSubject(string id)
    {
        var isSuccess = await _subjectService.Delete(id);

        return Ok(isSuccess);
    }
    [HttpPut]
    public async Task<ActionResult<bool>> UpdateSubject(string id, [FromBody] SubjectRequest request)
    {
        var isUpdatet = await _subjectService.Update(id, request.Title, request.Index, request.TeachersIds);

        return Ok(isUpdatet);
    }
}
