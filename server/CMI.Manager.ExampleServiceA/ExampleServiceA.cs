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
                        return Console.Out.WriteLineAsync($"Recived GetAllParameters: " + "I am an ExampleServiceAParameter!");
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