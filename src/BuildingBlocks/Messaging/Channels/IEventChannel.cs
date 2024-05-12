using System.Threading.Channels;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;

namespace CompanyName.MyProjectName.BuildingBlocks.Messaging.Channels
{
    internal interface IEventChannel
    {
        ChannelReader<IEvent> Reader { get; }

        ChannelWriter<IEvent> Writer { get; }
    }
}
