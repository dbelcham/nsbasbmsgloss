﻿using Microsoft.Extensions.Logging;
using NsbAsbMsgLoss.Messages;
using NServiceBus;

namespace NsbAsbMsgLoss.Handlers;

public class GettingStartedHandler : IHandleMessages<GettingStarted>
{
    private readonly ILogger<GettingStartedHandler> logger;

    public GettingStartedHandler(ILogger<GettingStartedHandler> logger)
    {
        this.logger = logger;
    }

     public async Task Handle(GettingStarted message, IMessageHandlerContext context)
    //public Task Handle(GettingStarted message, IMessageHandlerContext context)
    {
        logger.LogInformation("Got a kickoff message");

        for (var j = 0; j < 999; j++)
        {
            await context.Send(new GoDoSomething
            {
                Id = j,
                Value1 = Guid.NewGuid(),
                Value2 = DateTime.Now,
                Value3 = 123456789,
                Value4 = 123,
                Value5 = 123,
                Value6 = false,
                Value7 = 123123123,
                Value8 = 234234234,
                Value9 = false,
                Value10 = new List<GoDoSomethingSubType>
                {
                    new GoDoSomethingSubType
                    {
                        Value1 = 123123123123,
                        Value2 = 1234,
                        Value3 = 1,
                        Value4 = 123,
                        Value5 = 123,
                        Value6 = DateTime.Now,
                        Value7 = 123
                    }
                }
            });

            // var tasks = new List<Task>();
            // tasks.Add(context.SendLocal(new GoDoSomething
            // {
            //     Id = j,
            //     Value1 = Guid.NewGuid(),
            //     Value2 = DateTime.Now,
            //     Value3 = 123456789,
            //     Value4 = 123,
            //     Value5 = 123,
            //     Value6 = false,
            //     Value7 = 123123123,
            //     Value8 = 234234234,
            //     Value9 = false,
            //     Value10 = new List<GoDoSomethingSubType>
            //     {
            //         new GoDoSomethingSubType
            //         {
            //             Value1 = 123123123123,
            //             Value2 = 1234,
            //             Value3 = 1,
            //             Value4 = 123,
            //             Value5 = 123,
            //             Value6 = DateTime.Now,
            //             Value7 = 123
            //         }
            //     }
            // }));
        }

        // return Task.WhenAll(tasks);
    }
}