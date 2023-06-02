using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NsbAsbMsgLoss.Messages;
using NsbAsbMsgLoss.Starter;
using NServiceBus;

await CreateHostBuilder(args).RunConsoleAsync();

static IHostBuilder CreateHostBuilder(string[] args)
{
    var builder = Host.CreateDefaultBuilder(args);

    builder.ConfigureAppConfiguration((context, app) =>
        app.AddJsonFile("appsettings.json", false, true)
            .AddJsonFile("appsettings.dev.json", true, true)
    );

    builder.UseNServiceBus(ctx =>
    {
        var endpointConfiguration = new EndpointConfiguration("msglosstest_starter");
        endpointConfiguration.EnableInstallers();
        // endpointConfiguration.SendOnly();
        endpointConfiguration.Conventions().Add(new MessageConvention());
        endpointConfiguration.PurgeOnStartup(false);
        endpointConfiguration.UseSerialization<XmlSerializer>();

        var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
        transport.ConnectionString(ctx.Configuration.GetConnectionString("asb"));
        transport.Transactions(TransportTransactionMode.SendsAtomicWithReceive);

        var routing = transport.Routing();
        routing.RouteToEndpoint(typeof(GettingStarted), "msglosstest");

        var unitOfWorkSettings = endpointConfiguration.UnitOfWork();
        unitOfWorkSettings.WrapHandlersInATransactionScope();

        return endpointConfiguration;
    });

    return builder.ConfigureServices((context, services) =>
    {
        services.AddHostedService<ConsoleHostedService>();
    });
}