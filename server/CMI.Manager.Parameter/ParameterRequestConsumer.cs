using System;
using System.Threading.Tasks;
using CMI.Contract.Parameter;
using MassTransit;

namespace CMI.Manager.Parameter
{
    public class ParameterRequestConsumer : IConsumer<ParameterRequest>
    {
        public async Task Consume(ConsumeContext<ParameterRequest> context)
        {
            await Console.Out.WriteLineAsync(context.Message.Message);
            await context.RespondAsync(new ParameterResponse()
            {
                Message = "The Parameter Request Consumed the message Successfully!"
            });
        }
    }
}