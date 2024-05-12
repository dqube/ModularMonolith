namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions;

public sealed class AppOptions
{
    public string Name { get; set; } = string.Empty;

    public string Version { get; set; } = string.Empty;

    public string Project { get; set; } = string.Empty;

    public string ModulePart { get; set; } = string.Empty;
}