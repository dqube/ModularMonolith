using CompanyName.MyProjectName.BuildingBlocks.Abstractions.Abstractions;
using CompanyName.MyProjectName.BuildingBlocks.Contexts;

namespace CompanyName.MyProjectName.BuildingBlocks.Messaging;

public record MessageEnvelope<T>(T Message, MessageContext Context)
    where T : IMessage;
