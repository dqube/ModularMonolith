using Microsoft.Extensions.DependencyInjection;

namespace CompanyName.MyProjectName.BuildingBlocks.Saga
{
    public interface IChronicleBuilder
    {
        IServiceCollection Services { get; }

        IChronicleBuilder UseInMemoryPersistence();

        IChronicleBuilder UseSagaLog<TSagaLog>()
            where TSagaLog : ISagaLog;

        IChronicleBuilder UseSagaStateRepository<TRepository>()
            where TRepository : ISagaStateRepository;
    }
}
