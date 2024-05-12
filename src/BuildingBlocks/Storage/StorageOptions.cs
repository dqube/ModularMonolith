namespace CompanyName.MyProjectName.BuildingBlocks.Storage;

public sealed class StorageOptions
{
    public string Name { get; set; } = string.Empty;

    public string Url { get; set; } = string.Empty;

    public int DefaultCacheTime { get; set; } = 60;
}