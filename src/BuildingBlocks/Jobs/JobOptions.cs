namespace CompanyName.MyProjectName.BuildingBlocks.Jobs;

public sealed class JobOptions
{
    public Dictionary<string, QuartzJobs> Jobs { get; set; } = new();

    public sealed class QuartzJobs
    {
        public int Interval { get; set; } = 3;

        public string Name { get; set; } = string.Empty;
    }
}