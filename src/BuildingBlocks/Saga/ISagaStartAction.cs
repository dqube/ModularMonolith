namespace CompanyName.MyProjectName.BuildingBlocks.Saga;

public interface ISagaStartAction<in TMessage> : ISagaAction<TMessage>
{
}
