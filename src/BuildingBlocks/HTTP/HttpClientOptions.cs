namespace CompanyName.MyProjectName.BuildingBlocks.HTTP;

public sealed class HttpClientOptions
{
    public string Name { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    public CertificateOptions? Certificate { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

    public ResiliencyOptions Resiliency { get; set; } = new();

    public RequestMaskingOptions RequestMasking { get; set; } = new();

    public Dictionary<string, string> Services { get; set; } = new();

    public sealed class CertificateOptions
    {
        public string Location { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }

    public sealed class ResiliencyOptions
    {
        public int Retries { get; set; } = 3;

        public TimeSpan? RetryInterval { get; set; }

        public bool Exponential { get; set; }
    }

    public class RequestMaskingOptions
    {
        public bool Enabled { get; set; }

        public List<string> UrlParts { get; set; } = new();

        public string MaskTemplate { get; set; } = "***";
    }
}
