﻿namespace CompanyName.MyProjectName.BuildingBlocks.Observability.Metrics;

public sealed class MetricsOptions
{
    public bool Enabled { get; set; }

    public string Endpoint { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public string Exporter { get; set; } = string.Empty;
}