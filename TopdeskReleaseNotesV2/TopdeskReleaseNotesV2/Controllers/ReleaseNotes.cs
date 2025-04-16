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
        List<Note> apiNotes = await ReleaseNotes.GetNotesFromUrl("https://releasenotes.topdesk.com/api/v1/releasenotes/releases", "API");
        List<Note> featureNotes = await ReleaseNotes.GetNotesFromUrl("https://releasenotes.topdesk.com/v1/releasenotes/releases", "Feature");

        List<Note> combined = apiNotes.Concat(featureNotes).ToList();
        return Ok(combined);
    }
}
