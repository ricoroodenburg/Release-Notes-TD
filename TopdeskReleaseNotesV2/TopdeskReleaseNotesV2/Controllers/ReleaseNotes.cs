using Microsoft.AspNetCore.Mvc;
using TopdeskReleaseNotesV2.Models;
using TopdeskReleaseNotesV2.Functions;


[ApiController]
[Route("api/[controller]")]
public class ReleaseNotesController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Note>>> Get()
    {
        var apiNotes = await ReleaseNotes.GetNotesFromUrl("https://releasenotes.topdesk.com/api/v1/releasenotes/releases", "API");
        var featureNotes = await ReleaseNotes.GetNotesFromUrl("https://releasenotes.topdesk.com/v1/releasenotes/releases", "Feature");

        var combined = apiNotes.Concat(featureNotes).ToList();
        return Ok(combined);
    }
}
