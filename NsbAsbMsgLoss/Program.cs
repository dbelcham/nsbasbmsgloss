﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NsbAsbMsgLoss;
using NsbAsbMsgLoss.Messages;
using NServiceBus;
using Serilog;

await CreateHostBuilder(args).Build().RunAsync();

static IHostBuilder CreateHostBuilder(string[] args)
{
    var builder = Host.CreateDefaultBuilder(args);

    builder.ConfigureAppConfiguration((context, app) => 
        app.AddJsonFile("appsettings.json", false, true)
            .AddJsonFile("appsettings.dev.json", true, true)
    );

    builder.UseSerilog((ctx, svc, config) =>
    {
        config.ReadFrom.Configuration(ctx.Configuration)
            .WriteTo.Console()
            .WriteTo.File("log.txt");

    });

    builder.UseNServiceBus(ctx =>
    {
        var endpointConfiguration = new EndpointConfiguration("msglosstest");
        var transport = endpointConfiguration.UseTransport<AzureServiceBusTransport>();
        transport.ConnectionString(ctx.Configuration.GetConnectionString("asb"));
        transport.Transactions(TransportTransactionMode.SendsAtomicWithReceive);

        var routing = transport.Routing();
        routing.RouteToEndpoint(typeof(GoDoSomething), "msglosstest_godosomething");

        endpointConfiguration.EnableInstallers();
        endpointConfiguration.Conventions().Add(new MessageConvention());
        endpointConfiguration.PurgeOnStartup(false);
        endpointConfiguration.UseSerialization<XmlSerializer>();

        var recoverability = endpointConfiguration.Recoverability();
        recoverability.Immediate(x => x.NumberOfRetries(0));
        recoverability.Delayed(x => x.NumberOfRetries(0));

        var unitOfWorkSettings = endpointConfiguration.UnitOfWork();
        unitOfWorkSettings.WrapHandlersInATransactionScope();

        return endpointConfiguration;
    });

    return builder;
}