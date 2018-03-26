using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using CMI.Contract.Parameter.SaveParameter;

namespace CMI.Manager.Parameter
{
    public class SaveParameterRequestConsumer : IConsumer<SaveParameterRequest>
    {
        public Task Consume(ConsumeContext<SaveParameterRequest> context)
        {
            ParameterHelper.Parameters = new List<Contract.Parameter.Parameter>();
            ParameterService.ParameterBus.Publish(new SaveParameterEvent(context.Message.Parameter));
            Console.Out.WriteLineAsync("Save Event started");
            Thread.Sleep(400);
            context.RespondAsync(new SaveParameterResponse()
            {
                Success = true
            });
            return Console.Out.WriteLineAsync("Save Event response sent");
        }
    }
}