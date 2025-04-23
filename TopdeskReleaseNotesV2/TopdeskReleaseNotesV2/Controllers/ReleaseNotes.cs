using Microsoft.AspNetCore.Mvc;
using TopdeskReleaseNotesV2.Models;
using TopdeskReleaseNotesV2.Functions;
using System.Collections.Generic;


[ApiController]
[Route("api/[controller]")]
public class ReleaseNotesController : ControllerBase
{
    [HttpGet]
    [HttpHead]
    public async Task<ActionResult<List<Note>>> Get(/* string? searchTerm */)
    {

        Root apiNotesv2 = await ReleaseNotes.GetNotesFromUrlv2("https://releasenotes.topdesk.com/api/v1/releasenotes/releases", "API");
        Root notesv2 = await ReleaseNotes.GetNotesFromUrlv2("https://releasenotes.topdesk.com/v1/releasenotes/releases", "Feature");
        List<Note> combined = apiNotesv2.notes.Concat(notesv2.notes).OrderByDescending(note => note.releaseDate).ToList();
        Root result = new Root
        {
            notes = combined,
            lastCheckedApi = apiNotesv2.lastCheckedApi,
            lastCheckedFeature = notesv2.lastCheckedFeature
        };
        


        //List<Note> apiNotes = await ReleaseNotes.GetNotesFromUrl("https://releasenotes.topdesk.com/api/v1/releasenotes/releases", "API");
        //List<Note> featureNotes = await ReleaseNotes.GetNotesFromUrl("https://releasenotes.topdesk.com/v1/releasenotes/releases", "Feature");
        //List<Note> combined = apiNotes.Concat(featureNotes).OrderByDescending(note => note.releaseDate).ToList();
        
        

        /* var result = string.IsNullOrEmpty(searchTerm) ? combined : combined.Where(x => x.description.ToLowerInvariant().Contains(searchTerm.ToLowerInvariant())
            || x.release.ToLowerInvariant().Contains(searchTerm.ToLowerInvariant())); */

        return Ok(result);
    }
}

