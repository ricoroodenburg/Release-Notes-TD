using Newtonsoft.Json;

namespace TopdeskReleaseNotesV2.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Attentions
    {
        public bool highlight {  get; set; }   
    }

    public class Editions
    {
        public bool lite { get; set; }
        public bool professional { get; set; }
        public bool enterprise { get; set; }
    }

    public class Hosting
    {
        public bool saas { get; set; }
        [JsonProperty("on-premises virtual appliance")]
        public bool onpremisesvirtualappliance { get; set; }
    }

    public class Note
    {
        public string release { get; set; }
        public string releaseDate { get; set; }
        public string category { get; set; }
        public string subcategory { get; set; }
        public string description { get; set; }
        public string descriptionHtml { get; set; }
        public Editions editions { get; set; }
        public Attentions attentions { get; set; }
        public Hosting hosting { get; set; }
        public bool isTosNote { get; set; }
        public string SourceTag { get; set; }
    }

    public class Root
    {
        public List<Note> notes { get; set; }
    }


}
