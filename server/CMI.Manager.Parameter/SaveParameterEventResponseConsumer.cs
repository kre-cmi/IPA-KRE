using System;
using System.Threading.Tasks;
using CMI.Contract.Parameter.SaveParameter;
using MassTransit;

namespace CMI.Manager.Parameter
{
    public class SaveParameterEventResponseConsumer : IConsumer<SaveParameterEventResponse>
    {
        public async Task Consume(ConsumeContext<SaveParameterEventResponse> context)
        {
            //Put success here
            await Console.Out.WriteLineAsync("Saved Successfully");
        }
    }
}
