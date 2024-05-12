namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;

// Marker interface
public interface IMessage
{
}

public interface IMessage<out TResult>
{
}