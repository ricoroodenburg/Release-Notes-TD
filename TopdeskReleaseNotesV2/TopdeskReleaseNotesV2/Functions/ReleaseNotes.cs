using Newtonsoft.Json;
using TopdeskReleaseNotesV2.Models;

namespace TopdeskReleaseNotesV2.Functions
{
    public class ReleaseNotes
    {
        public static async Task<List<Note>> GetNotesFromUrl(string url, string sourceTag)
        {
            using HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(url);
            var root = JsonConvert.DeserializeObject<Root>(response);

            if (root?.notes != null)
            {
                foreach (Note note in root.notes)
                {
                    note.SourceTag = sourceTag;
                }
                return root.notes;
            }

            return new List<Note>();
        }
    }
}
