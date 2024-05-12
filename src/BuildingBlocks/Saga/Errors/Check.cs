namespace CompanyName.MyProjectName.BuildingBlocks.Saga.Errors;

internal static class Check
{
    internal static void Is<TExpected>(Type type, string message = null)
    {
        if (typeof(TExpected).IsAssignableFrom(type))
        {
            return;
        }

        message = message ?? CheckErrorMessages.InvalidArgumentType;
        throw new ChronicleException(message);
    }

    internal static void IsNull<TData>(TData data, string message = null)
        where TData : class
    {
        if (data is null)
        {
            message = message ?? CheckErrorMessages.ArgumentNull;
            throw new ChronicleException(message);
        }
    }

    private static class CheckErrorMessages
    {
#pragma warning disable SA1401 // Fields should be private
        public static string InvalidArgumentType = "Invalid argument type";
#pragma warning restore SA1401 // Fields should be private
#pragma warning disable SA1401 // Fields should be private
        public static string ArgumentNull = "Argument null";
#pragma warning restore SA1401 // Fields should be private
    }
}
