namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
public class JobsAttribute : Attribute
{
    public string Name { get; set; }

    public string FullName { get; set; }

    public string Description { get; set; }

    public TimeSpan Interval { get; set; }

    public DateTimeOffset StartAt { get; set; } = DateTimeOffset.MinValue;

    public int RepeatCount { get; set; } = 5;

    public string TriggerName { get; set; }

    public string TriggerGroup { get; set; }

    public string TriggerDescription { get; set; }

    public int Priority { get; set; } = 0;

    public bool Manual { get; set; } = false;

    public JobsAttribute(
        string name = null, string fullName = null, string description = null, string triggerName = null, string triggerGroup = null, string module = null, bool enabled = true)
    {
        Name = name ?? string.Empty;
        FullName = fullName ?? string.Empty;
        TriggerName = triggerName ?? string.Empty;
        TriggerGroup = triggerGroup ?? string.Empty;
        Description = description;
    }
}