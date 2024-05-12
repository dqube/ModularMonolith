namespace CompanyName.MyProjectName.BuildingBlocks.Observability.Logging;
#nullable enable

public sealed class SerilogOptions
{
    public string MinimumLevel { get; set; } = string.Empty;

    public ConsoleOptions Console { get; set; } = new();

    public FileOptions File { get; set; } = new();

    public SeqOptions Seq { get; set; } = new();

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public IEnumerable<string>? ExcludePaths { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

    public IEnumerable<string>? ExcludeProperties { get; set; }

    public Dictionary<string, string> Override { get; set; } = new();

    public Dictionary<string, object> Tags { get; set; } = new();

    public sealed class ConsoleOptions
    {
        public bool Enabled { get; set; }
    }

    public sealed class FileOptions
    {
        public bool Enabled { get; set; }

        public string Path { get; set; } = string.Empty;

        public string Interval { get; set; } = string.Empty;
    }

    public sealed class SeqOptions
    {
        public bool Enabled { get; set; }

        public string Url { get; set; } = string.Empty;

        public string ApiKey { get; set; } = string.Empty;
    }
}
