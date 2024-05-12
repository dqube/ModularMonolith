namespace CompanyName.MyProjectName.BuildingBlocks.Saga;

public class ChronicleException : Exception
{
    public ChronicleException(string message)
        : base(message)
    {
    }

    public ChronicleException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
