using System;
using CMI.Contract.Parameter;
using MassTransit;

namespace CMI.Manager.ExampleServiceB
{
    public class ExampleServiceB
    {
        private IBusControl ParameterBus { get; set; }
        public void Start()
        {
            ParameterBus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint(host, "GetAllParametersB", ep =>
                {
                    ep.Handler<ParameterEvent>(context =>
                    {
                        //ParameterBus.Publish(new ParameterEventResponse { Parameters = "Example Service B Response" });
                        return Console.Out.WriteLineAsync("response sent from Parameter Service B!");
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
