using System;
using System.Threading;
using CMI.Contract.Parameter;
using MassTransit;

namespace CMI.Manager.Parameter
{
    public class ParameterService
    {
        public static IBusControl ParameterBus { get; set; }
        public void Start()
        {
            ParameterBus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint("GetParameterQueue", ec =>
                {
                    ec.Consumer(() => new ParameterRequestConsumer());
                });
                cfg.ReceiveEndpoint("ResponseParameterEventQueue", ec =>
                {
                    ec.Consumer(() => new ParameterEventResponseConsumer());
                });
            });
            
            ParameterBus.Start();
            /*do
            {
                PullSettings();
            } while (true);
            */
        }

        public void PullSettings()
        {
            ParameterBus.Publish(new ParameterEvent {Type = "ParameterEvent"});
            Thread.Sleep(10000);
        }
        public void Stop()
        {
            ParameterBus.Stop();
        }
    }
}
