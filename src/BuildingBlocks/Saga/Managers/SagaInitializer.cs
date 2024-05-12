using CompanyName.MyProjectName.BuildingBlocks.Saga.Persistence;
using CompanyName.MyProjectName.BuildingBlocks.Saga.Utils;

namespace CompanyName.MyProjectName.BuildingBlocks.Saga.Managers
{
    internal sealed class SagaInitializer : ISagaInitializer
    {
        private readonly ISagaStateRepository _repository;

        public SagaInitializer(ISagaStateRepository repository)
        {
            _repository = repository;
        }

        public async Task<(bool IsInitialized, ISagaState State)> TryInitializeAsync<TMessage>(ISaga saga, SagaId id, TMessage _)
        {
            var action = (ISagaAction<TMessage>)saga;
            var sagaType = saga.GetType();
            var dataType = saga.GetSagaDataType();

            var state = await _repository.ReadAsync(id, sagaType).ConfigureAwait(false);

            if (state is null)
            {
                if (!(action is ISagaStartAction<TMessage>))
                {
                    return (false, default);
                }

                state = CreateSagaState(id, sagaType, dataType);
            }
            else if (state.State is SagaStates.Rejected)
            {
                return (false, default);
            }

            InitializeSaga(saga, id, state);

            return (true, state);
        }

        private static ISagaState CreateSagaState(SagaId id, Type sagaType, Type dataType)
        {
            var sagaData = dataType != null ? Activator.CreateInstance(dataType) : null;
            return SagaState.Create(id, sagaType, SagaStates.Pending, sagaData);
        }

        private void InitializeSaga(ISaga saga, SagaId id, ISagaState state)
        {
            if (state.Data is null)
            {
                saga.Initialize(id, state.State);
            }
            else
            {
                saga.InvokeGeneric(nameof(ISaga<object>.Initialize), id, state.State, state.Data);
            }
        }
    }
}
