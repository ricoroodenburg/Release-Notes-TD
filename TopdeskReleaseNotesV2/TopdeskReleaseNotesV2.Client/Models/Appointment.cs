namespace TopdeskReleaseNotesV2.Models
{
    public class Appointment
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Text { get; set; }
        public bool highlight { get; set; }
        public string description { get; set; }
        public string descriptionHtml { get; set; }
    }
}
