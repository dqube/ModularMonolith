using CompanyName.MyProjectName.BuildingBlocks.Contexts;

namespace CompanyName.MyProjectName.BuildingBlocks.Contexts.Accessors;

#nullable enable
public interface IMessageContextAccessor
{
    MessageContext? MessageContext { get; set; }
}