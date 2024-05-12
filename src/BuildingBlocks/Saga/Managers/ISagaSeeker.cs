namespace CompanyName.MyProjectName.BuildingBlocks.Saga.Managers;

internal interface ISagaSeeker
{
    IEnumerable<ISagaAction<TMessage>> Seek<TMessage>();
}
