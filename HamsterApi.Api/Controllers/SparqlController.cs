using BrightstarDB.Client;
using Microsoft.AspNetCore.Mvc;

namespace HamsterApi.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SparqlController: ControllerBase
{
    [HttpPost]
    public ActionResult Index(string query)
    {
        try
        {
            var client = BrightstarService.GetClient("Type=embedded;StoresDirectory=c:\\brightstardb;StoreName=Test4");
            var results = client.ExecuteQuery("Test4", query);
            return new FileStreamResult(results, "application/xml;charset=utf-8");
        }
        catch (Exception ex) { }
        return NoContent();
    }
}
