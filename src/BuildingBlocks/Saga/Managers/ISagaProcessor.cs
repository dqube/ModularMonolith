namespace CompanyName.MyProjectName.BuildingBlocks.Saga.Managers;

internal interface ISagaProcessor
{
    Task ProcessAsync<TMessage>(
        ISaga saga,
        TMessage message,
        ISagaState state,
        ISagaContext context)
        where TMessage : class;
}