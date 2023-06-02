using NsbAsbMsgLoss.Messages;
using NServiceBus;

namespace NsbAsbMsgLoss;

public class MessageConvention : IMessageConvention
{
    public bool IsMessageType(Type type)
    {
        return false;
    }

    public bool IsCommandType(Type type)
    {
        return type.Namespace != null && type.Namespace == typeof(GoDoSomething).Namespace;
    }

    public bool IsEventType(Type type)
    {
        return false;
    }

    public string Name { get; } = "Message conventions";
}