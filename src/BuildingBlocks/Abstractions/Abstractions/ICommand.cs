namespace CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;

// Marker interface
public interface ICommand : IMessage
{
}

public interface ICommand<T> : ICommand
{
}