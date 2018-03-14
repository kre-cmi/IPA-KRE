using System;
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
            ParameterHelper.Parameters = string.Empty;
            ParameterService.ParameterBus.Publish(new ParameterEvent { Type = "ParameterEvent" });
            Console.Out.WriteLineAsync("Event started");
            Thread.Sleep(1000);
            context.RespondAsync(new ParameterResponse()
            {
                Message = ParameterHelper.Parameters
            });
            return Console.Out.WriteLineAsync("Event response sent");
        }
    }
}