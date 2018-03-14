using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CMI.Contract.Parameter;
using MassTransit;

namespace CMI.Manager.Parameter
{
    public class ParameterEventResponseConsumer : IConsumer<ParameterEventResponse>
    {
        public async Task Consume(ConsumeContext<ParameterEventResponse> context)
        {
            ParameterHelper.Parameters += context.Message.Message + " / ";
            await Console.Out.WriteLineAsync("Parameter string is now " + ParameterHelper.Parameters);
        }
    }
}
