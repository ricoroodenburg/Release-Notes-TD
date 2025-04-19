using Microsoft.AspNetCore.Mvc;
using TopdeskReleaseNotesV2.Models;
using TopdeskReleaseNotesV2.Functions;


[ApiController]
[Route("api/[controller]")]
public class ReleaseNotesController : ControllerBase
{
    [HttpGet]
    [HttpHead]
    public async Task<ActionResult<List<Note>>> Get(string? searchTerm)
    {
        Console.Write("ReleaseNotesController");
        List<Note> apiNotes = await ReleaseNotes.GetNotesFromUrl("https://releasenotes.topdesk.com/api/v1/releasenotes/releases", "API");
        List<Note> featureNotes = await ReleaseNotes.GetNotesFromUrl("https://releasenotes.topdesk.com/v1/releasenotes/releases", "Feature");
        List<Note> combined = apiNotes.Concat(featureNotes).OrderByDescending(note => note.releaseDate).ToList();
        var result = string.IsNullOrEmpty(searchTerm) ? combined : combined.Where(x => x.description.ToLowerInvariant().Contains(searchTerm)
            || x.release.ToLowerInvariant().Contains(searchTerm)); 

        return Ok(result);
    }
}

