using HamsterApi.Api.Contracts.Request;
using HamsterApi.Api.Contracts.Response;
using HamsterApi.Api.Helpers;
using HamsterApi.Domain.Models;
using HamsterApi.Domain.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace HamsterApi.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CurriculumController:ControllerBase
{
    private readonly ICurriculumService _curriculumService;

    public CurriculumController(ICurriculumService curriculumService)
    {
        _curriculumService = curriculumService;
    }
    [HttpGet("ids")]
    public async Task<ActionResult<List<CurriculumResponse>?>> ReadByIds([FromQuery] IEnumerable<string> ids)
    {
        var curriculum = await _curriculumService.ReadByIds(ids);
        var responce = curriculum!.Select(curriculum => new CurriculumResponse(curriculum.Id,
            curriculum.ChairId,
            curriculum.DepartmentId,
            curriculum.DirectionId,
            curriculum.SemestersSubjects.Select(i=>i.Map()).ToList(),
            curriculum.SemestersElectiveSubjects.Select(i => i.Map()).ToList(),
            curriculum.YearOfPreparation,
            curriculum.FGOSNumber));

        return Ok(responce);
    }
    [HttpGet("all")]
    public async Task<ActionResult<List<CurriculumResponse>>> ReadAll()
    {

        var curriculum = await _curriculumService.ReadAll();
        var responce = curriculum!.Select(curriculum => new CurriculumResponse(curriculum.Id,
            curriculum.ChairId,
            curriculum.DepartmentId,
            curriculum.DirectionId,
            curriculum.SemestersSubjects.Select(i => i.Map()).ToList(),
            curriculum.SemestersElectiveSubjects.Select(i => i.Map()).ToList(),
            curriculum.YearOfPreparation,
            curriculum.FGOSNumber));
        return Ok(responce);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<CurriculumResponse?>> ReadById(string id)
    {
        var curriculum = await _curriculumService.Read(id);
        return curriculum is null ? NoContent() : Ok(new CurriculumResponse(curriculum.Id,
            curriculum.ChairId,
            curriculum.DepartmentId,
            curriculum.DirectionId,
            curriculum.SemestersSubjects.Select(i => i.Map()).ToList(),
            curriculum.SemestersElectiveSubjects.Select(i => i.Map()).ToList(),
            curriculum.YearOfPreparation,
            curriculum.FGOSNumber));
    }
    [HttpPost]
    public async Task<ActionResult<string>> CreateCurriculum([FromBody] CurriculumRequest request)
    {
        var item = Curriculum.Create(Guid.NewGuid().ToString(),
            request.ChairId,
            request.DepartmentId,
            request.DirectionId,
            request.SemestersSubjects.Select(i => i.Map()).ToList(),
            request.SemestersElectiveSubjects.Select(i => i.Map()).ToList(),
            request.YarOfPreparation,
            request.FGOSNumber);

        if (item.Failure)
        {
            return BadRequest(item.Error);
        }
        await _curriculumService.Create(item.Value);
        return Ok(item.Value.Id);
    }

    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteCurriculum(string id)
    {
        var item = await _curriculumService.Read(id);
        if (item is null) return BadRequest("item is null");
        var isSuccess = await _curriculumService.Delete(id);
        return Ok(isSuccess);
    }
    [HttpPut]
    public async Task<ActionResult<bool>> UpdateCurriculum(string id, [FromBody] CurriculumRequest request)
    {
        var isUpdatet = await _curriculumService.Update(id, request.ChairId,
            request.DepartmentId,
            request.DirectionId,
            request.SemestersSubjects.Select(i => i.Map()).ToList(),
            request.SemestersElectiveSubjects.Select(i => i.Map()).ToList(),
            request.YarOfPreparation,
            request.FGOSNumber);

        return Ok(isUpdatet);
    }
    [HttpPost("addelectivesubject")]
    public async Task<ActionResult<string>> AddElectiveSubject(string id, IEnumerable<SubjectWtihLoadRequest> subjects)
    {
        var result = await _curriculumService.AddElectives(id, subjects.Select(i => i.Map()).ToList());
        return Ok(result);
    }
    [HttpDelete("removeelectivesubject")]
    public async Task<ActionResult<bool>> RemoveElectiveSubject(string id, IEnumerable<string> subjectsIds)
    {
        var result = await _curriculumService.RemoveElectives(id, subjectsIds);
        return Ok(result);
    }
    [HttpPost("addsubject")]
    public async Task<ActionResult<string>> AddSubject(string id, IEnumerable<SubjectWtihLoadRequest> subjects)
    {
        var result = await _curriculumService.AddSubject(id, subjects.Select(i => i.Map()).ToList());
        return Ok(result);
    }
    [HttpDelete("removesubject")]
    public async Task<ActionResult<bool>> RemoveSubject(string id, IEnumerable<string> subjectsIds)
    {
        var result = await _curriculumService.RemoveSubject(id, subjectsIds);
        return Ok(result);
    }
}
