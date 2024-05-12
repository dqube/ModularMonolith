using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;

namespace CompanyName.MyProjectName.BuildingBlocks.Contexts.Accessors;

public interface IMessageContextRegistry
{
    void Set(IMessage message, MessageContext context);
}