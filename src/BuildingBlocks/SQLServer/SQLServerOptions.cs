namespace CompanyName.MyProjectName.BuildingBlocks.SQLServer;

public sealed class SQLServerOptions
{
    public string ConnectionString { get; set; } = string.Empty;

    public Dictionary<string, string> ConnectionStrings { get; set; } = new();
}