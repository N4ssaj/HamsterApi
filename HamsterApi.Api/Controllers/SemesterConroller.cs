using HamsterApi.Api.Contracts.Request;
using HamsterApi.Api.Contracts.Response;
using HamsterApi.Api.Helpers;
using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace HamsterApi.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SemesterController:ControllerBase
{
    private readonly ISemesterService _semesterService;

    public SemesterController(ISemesterService semesterService)
    {
        _semesterService = semesterService;
    }
    [HttpGet("ids")]
    public async Task<ActionResult<List<SemesterResponse>?>> ReadByIds([FromQuery] IEnumerable<string> ids)
    {
        var semester = await _semesterService.ReadByIds(ids);
        var responce = semester!.Select(semester => new SemesterResponse(semester.Id,semester.Number,semester.GroupId,semester.Subjects.Select(i=>i.Map()).ToList()));

        return Ok(responce);
    }
    [HttpGet("all")]
    public async Task<ActionResult<List<SemesterResponse>>> ReadAll()
    {

        var semester = await _semesterService.ReadAll();
        var responce = semester!.Select(semester => new SemesterResponse(semester.Id, semester.Number, semester.GroupId, semester.Subjects.Select(i => i.Map()).ToList()));
        return Ok(responce);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<SemesterResponse?>> ReadById(string id)
    {
        var semester = await _semesterService.Read(id);
        return semester is null ? NoContent() : Ok(new SemesterResponse(semester.Id, semester.Number, semester.GroupId, semester.Subjects.Select(i => i.Map()).ToList()));
    }
    [HttpPost]
    public async Task<ActionResult<string>> CreateSemester([FromBody] SemesterRequest request)
    {
        var item = Semester.Create(Guid.NewGuid().ToString(), request.Number, request.GroupId, request.Subjects.Select(i=>i.Map()).ToList());
        if (item.Failure)
        {
            return BadRequest(item.Error);
        }
        await _semesterService.Create(item.Value);
        return Ok(item.Value.Id);
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteSemester(string id)
    {
        var item = await _semesterService.Read(id);
        if (item is null) return BadRequest("item is null");
        var isSuccess = await _semesterService.Delete(id);
        return Ok(isSuccess);
    }
    [HttpPut]
    public async Task<ActionResult<bool>> UpdateSemester(string id, [FromBody] SemesterRequest request)
    {
        var item = await _semesterService.Read(id);
        if (item is null) return BadRequest("item is null");
        var isUpdatet = await _semesterService.Update(id, request.Number, request.GroupId, request.Subjects.Select(i => i.Map()).ToList());
        return Ok(isUpdatet);
    }
    [HttpPost("addsubject")]
    public async Task<ActionResult<string>> AddSubject(string id, IEnumerable<SubjectWtihLoadRequest> subjects)
    {
        var result = await _semesterService.AddSubjects(id,subjects.Select(i=>i.Map()).ToList());
        return Ok(result);
    }
    [HttpDelete("removesubject")]
    public async Task<ActionResult<bool>> RemoveSubject(string id, IEnumerable<string> subjectsIds)
    {
        var result = await _semesterService.RemoveSubjects(id,subjectsIds);
        return Ok(result);
    }
}
