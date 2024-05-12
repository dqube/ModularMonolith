namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Serialization;

public interface IJsonSerializer
{
    string Serialize<T>(T value);

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
    T? Deserialize<T>(string value);
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.

    object Deserialize(string value, Type type);
}