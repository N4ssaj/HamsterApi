using BrightstarDB.Client;
using Microsoft.AspNetCore.Mvc;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;

namespace HamsterApi.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SparqlController: ControllerBase
{
    private readonly IBrightstarService _brightstarService;

    public SparqlController(IBrightstarService brightstarService)
        => _brightstarService = brightstarService;
    [HttpPost("updateqraphql")]
    public ActionResult<string> UpdateGraphql(string querty,string storyName)
    {
        IJobInfo jobInfo = _brightstarService.ExecuteUpdate(storyName, querty, false);
        return new JsonResult(jobInfo);
    }
    [HttpPost("createstore")]
    public ActionResult<string> CreateDB()
    {
        try
        {
            var storyName = "store" + Guid.NewGuid();
            _brightstarService.CreateStore(storyName);
            return storyName;
        }
        catch (Exception ex)
        {
            return NoContent();
        }
    }
    [HttpDelete("deletestore")]
    public ActionResult<string> DeleteDB(string storyName)
    {
        try
        { 
            _brightstarService.DeleteStore(storyName);
            return storyName;
        }
        catch (Exception ex)
        {
            return NoContent();
        }
    }
    [HttpDelete]
    public ActionResult<string> Delete(string deletePatterns,string storyName)
    {
        try
        {
            var transactionData = new UpdateTransactionData { DeletePatterns=deletePatterns };
            var job = _brightstarService.ExecuteTransaction(storyName, transactionData);
            return new JsonResult(job);
        }
        catch (Exception ex)
        {
            return NoContent();
        }
    }
    [HttpPost]
    public ActionResult<string> Update(string addTriples,string storyName)
    {
        try
        {
            var transactionData = new UpdateTransactionData { InsertData = addTriples };
            var job=_brightstarService.ExecuteTransaction(storyName, transactionData);
            return new JsonResult(job);
        }
        catch (Exception ex)
        {
            return NoContent();
        }
    }
    [HttpGet]
    public ActionResult Index(string query, string storyName)
    {
        try
        {
            var results = _brightstarService.ExecuteQuery(storyName, query);
            return new FileStreamResult(results, "application/xml;charset=utf-8");
        }
        catch (Exception ex) { }
        return NoContent();
    }

}
