using System;
using CMI.Contract.Parameter;
using MassTransit;

namespace CMI.Manager.ExampleServiceA
{
    public class ExampleServiceA
    {
        private IBusControl ParameterBus { get; set; }
        public void Start()
        {
            ParameterBus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host, "GetAllParametersA", ep =>
                {
                    ep.Handler<ParameterEvent>(context =>
                    {
                        var exampleParameterA = ParameterHelper.GetParameters(new ExampleParameterA());
                        var exampleParameterAList = ParameterHelper.GetParameterListFromParameter(exampleParameterA);
                        ParameterBus.Publish(new ParameterEventResponse { Parameters = exampleParameterAList });
                        return Console.Out.WriteLineAsync("response sent from Parameter Service A!");
                    });
                });
            });
            ParameterBus.Start();

        }

        public void Stop()
        {
        }
    }
}