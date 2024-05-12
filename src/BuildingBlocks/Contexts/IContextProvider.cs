namespace CompanyName.MyProjectName.BuildingBlocks.Contexts;

public interface IContextProvider
{
    IContext Current();
}