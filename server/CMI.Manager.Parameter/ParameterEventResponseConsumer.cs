using System;
using System.Threading.Tasks;
using CMI.Contract.Parameter;
using MassTransit;

namespace CMI.Manager.Parameter
{
    public class ParameterEventResponseConsumer : IConsumer<ParameterEventResponse>
    {
        public async Task Consume(ConsumeContext<ParameterEventResponse> context)
        {
            ParameterHelper.Parameters.AddRange(context.Message.Parameters);
            await Console.Out.WriteLineAsync("Parameter Recived");
        }
    }
}
