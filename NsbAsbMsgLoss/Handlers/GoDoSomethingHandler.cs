using Microsoft.Extensions.Logging;
using NsbAsbMsgLoss.Messages;
using NServiceBus;

namespace NsbAsbMsgLoss.Handlers;

public class GoDoSomethingHandler : IHandleMessages<GoDoSomething>
{
    private readonly ILogger<GoDoSomethingHandler> _logger;

    public GoDoSomethingHandler(ILogger<GoDoSomethingHandler> logger)
    {
        _logger = logger;
    }

    public async Task Handle(GoDoSomething message, IMessageHandlerContext context)
    {
        // simulate work
        Thread.Sleep(200);
        _logger.LogInformation("Processing message {id}", message.Id);
    }
}