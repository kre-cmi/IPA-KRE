using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CMI.Contract.Parameter.GetParameter;
using MassTransit;

namespace CMI.Manager.Parameter
{
    public class GetParameterRequestConsumer : IConsumer<GetParameterRequest>
    {
        public Task Consume(ConsumeContext<GetParameterRequest> context)
        {
            ParameterHelper.Parameters = new List<Contract.Parameter.Parameter>();
            ParameterService.ParameterBus.Publish(new GetParameterEvent());
            Console.Out.WriteLineAsync("Get Event started");
            Thread.Sleep(400);
            context.RespondAsync(new GetParameterResponse()
            {
                Parameters = ParameterHelper.Parameters.ToArray()
            });
            return Console.Out.WriteLineAsync("Get Event response sent");
        }
    }
}