using System.Threading.Channels;
using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;

namespace CompanyName.MyProjectName.BuildingBlocks.Messaging.Channels
{
    internal class EventChannel : IEventChannel
    {
        private readonly Channel<IEvent> _channel = Channel.CreateUnbounded<IEvent>();

        public ChannelReader<IEvent> Reader => _channel.Reader;

        public ChannelWriter<IEvent> Writer => _channel.Writer;
    }
}
