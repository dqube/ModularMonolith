namespace CompanyName.MyProjectName.BuildingBlocks.Observability.Tracing;

public sealed class TracingOptions
{
    public bool Enabled { get; set; }

    public string Exporter { get; set; } = string.Empty;

    public JaegerOptions Jaeger { get; set; } = new();

    public string Url { get; set; } = string.Empty;

    public OltpOptions Oltp { get; set; } = new();

    public sealed class JaegerOptions
    {
        public string AgentHost { get; set; } = "localhost";

        public int AgentPort { get; set; } = 6831;

        public int? MaxPayloadSizeInBytes { get; set; }

        public string ExportProcessorType { get; set; } = "batch";
    }

    public sealed class OltpOptions
    {
        public string EndPoint { get; set; } = string.Empty;
    }
}