namespace CompanyName.MyProjectName.BuildingBlocks.Contexts.Accessors;
#nullable enable

public interface IContextAccessor
{
    IContext? Context { get; set; }
}
