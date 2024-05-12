namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Time;

public sealed class UtcClock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}