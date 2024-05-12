namespace CompanyName.MyProjectName.BuildingBlocks.Saga.Utils;

internal static class DateTimeExtensions
{
    internal static long GetTimeStamp(this DateTimeOffset dateTime)
        => dateTime.ToUnixTimeMilliseconds();
}
