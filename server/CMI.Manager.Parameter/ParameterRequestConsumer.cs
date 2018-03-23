using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CMI.Contract.Parameter;
using MassTransit;

namespace CMI.Manager.Parameter
{
    public class ParameterRequestConsumer : IConsumer<ParameterRequest>
    {
        public Task Consume(ConsumeContext<ParameterRequest> context)
        {
            Console.Out.WriteLineAsync(context.Message.Message);
            ParameterHelper.Parameters = new List<Contract.Parameter.Parameter>();
            ParameterService.ParameterBus.Publish(new ParameterEvent { Type = "ParameterEvent" });
            Console.Out.WriteLineAsync("Event started");
            Thread.Sleep(400);
            context.RespondAsync(new ParameterResponse()
            {
                Parameters = ParameterHelper.Parameters.ToArray()
            });
            return Console.Out.WriteLineAsync("Event response sent");
        }
    }
}