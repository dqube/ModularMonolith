namespace CompanyName.MyProjectName.BuildingBlocks.Saga.Managers;

internal interface ISagaInitializer
{
    Task<(bool IsInitialized, ISagaState State)> TryInitializeAsync<TMessage>(
        ISaga saga,
        SagaId id,
#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
        TMessage _);
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
}
