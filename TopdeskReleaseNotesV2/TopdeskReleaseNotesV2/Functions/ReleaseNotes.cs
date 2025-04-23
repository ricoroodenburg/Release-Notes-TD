using Newtonsoft.Json;
using TopdeskReleaseNotesV2.Models;

namespace TopdeskReleaseNotesV2.Functions
{
    public class ReleaseNotes
    {

        public static DateTime UnixMillisecondsToDateTime(long timestamp, bool local = false)
        {
            var offset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
            //return DateTime.Now;
            return local ? offset.LocalDateTime : offset.UtcDateTime;
        }

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
        

        public static async Task<Root> GetNotesFromUrlv2(string url, string sourceTag)
        {
            using HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(url);
            var root = JsonConvert.DeserializeObject<Root>(response);

            if (root?.notes != null)
            {

                switch (sourceTag)
                {
                    case "Feature":
                        Console.WriteLine("Ik ben hier");
                        root.lastCheckedFeature = UnixMillisecondsToDateTime(root.lastChecked, false);
                        break;

                    case "API":
                        Console.WriteLine("Ik ben ook hier");
                        root.lastCheckedApi = UnixMillisecondsToDateTime(root.lastChecked, false);
                        break;
                }

                foreach (Note note in root.notes)
                {
                    note.SourceTag = sourceTag;
                }
                return root;
            }

            return null;
        }
    }
}
