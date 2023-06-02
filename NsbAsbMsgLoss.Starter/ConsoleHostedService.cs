using Microsoft.Extensions.Hosting;
using NsbAsbMsgLoss.Messages;
using NServiceBus;

namespace NsbAsbMsgLoss.Starter;

public class ConsoleHostedService : IHostedService
{
    private readonly IHostApplicationLifetime _appLifetime;
    private readonly IMessageSession _messageSession;

    public ConsoleHostedService(IHostApplicationLifetime appLifetime, IMessageSession messageSession)
    {
        _appLifetime = appLifetime;
        _messageSession = messageSession;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _appLifetime.ApplicationStarted.Register(() => 
            Task.Run(async () =>
            {
                while (true)
                {
                    Console.WriteLine("Send starter message? (y/n)");

                    var starter = Console.ReadLine();

                    if (string.Compare(starter, "y", StringComparison.InvariantCultureIgnoreCase) == 0)
                    {
                        await _messageSession.Send(new GettingStarted());
                    }
                }
            })
        );

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}