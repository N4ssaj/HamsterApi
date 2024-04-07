using HamsterApi.Api.Contracts.Request;
using HamsterApi.Api.Contracts.Responce;
using HamsterApi.Application.Service;
using HamsterApi.Core.Models;
using HamsterApi.Core.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace HamsterApi.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuditoriumController : ControllerBase
{
    private readonly IAuditoriumService _auditoruimService;

    public AuditoriumController(IAuditoriumService auditoruimService)
        => _auditoruimService = auditoruimService;

    [HttpGet("{id}")]
    public async Task<ActionResult<AuditoriumResponse?>> ReadById(string id)
    {
        var auditorium = await _auditoruimService.Read(id);
        return auditorium is null ? NoContent() : Ok(new AuditoriumResponse(auditorium.Id, auditorium.Number));
    }

    [HttpGet("number/{number}")]
    public async Task<ActionResult<AuditoriumResponse?>> ReadByNumber(string number)
    {
        var auditorium = await _auditoruimService.ReadByNumber(number);
        return auditorium is null ? NoContent() : Ok(new AuditoriumResponse(auditorium.Id, auditorium.Number));
    }
    [HttpGet("ids")]
    public async Task<ActionResult<List<AuditoriumResponse>?>> ReadByIds([FromQuery]IEnumerable<string> ids)
    {
        var auditoriums = await _auditoruimService.ReadByIds(ids);
        var responce=auditoriums!.Select(a=>new AuditoriumResponse(a.Id, a.Number));

        return Ok(responce);
    }
    [HttpGet("all")]
    public async Task<ActionResult<List<AuditoriumResponse>?>> ReadAll()
    {
        var auditoriums = await _auditoruimService.ReadAll();
        var responce = auditoriums!.Select(a => new AuditoriumResponse(a.Id, a.Number));
        return Ok(responce);
    }
    [HttpPost]
    public async Task<ActionResult<string>> CreateAuditorium([FromBody] AuditoriumRequest request)
    {
        var item = Auditorium.Create(Guid.NewGuid().ToString(), request.Number);
        if(item.Failure)
        {
            return BadRequest(item.Error);
        }
        await _auditoruimService.Create(item.Value);

        return Ok(item.Value.Id);
    }
    [HttpDelete]
    public async Task<ActionResult<bool>> DeleteAuditorium(string id)
    {
        var isSuccess=await _auditoruimService.Delete(id);

        return Ok(isSuccess);
    }
    [HttpPut]
    public async Task<ActionResult<bool>> UpdateAuditorium(string id, [FromBody] AuditoriumRequest request)
    {
        var isUpdatet=await _auditoruimService.Update(id, request.Number);

        return Ok(isUpdatet);
    }

}
