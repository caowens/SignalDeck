namespace SignalDeck.Sdk.Models
{
    public class SignalEvent
    {
        public Guid Id { get; init; }
        public DateTime Timestamp { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public SignalSeverity Severity { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
        public Dictionary<string, string> Properties { get; set; } = new();

        public SignalEvent(string name, SignalSeverity severity = SignalSeverity.Info, string category = "General")
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
            Name = name;
            Category = category;
            Severity = severity;
        }
    }
}