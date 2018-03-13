using System;
using System.Threading;
using CMI.Contract.Parameter;
using MassTransit;

namespace CMI.Manager.Parameter
{
    public class ParameterService
    {
        private IBusControl ParameterBus { get; set; }
        public void Start()
        {
            ParameterBus = BusConfigurator.ConfigureBus((cfg, host) =>
            {
                cfg.ReceiveEndpoint("GetParameterQueue", ec =>
                {
                    ec.Consumer(() => new ParameterRequestConsumer());
                });
            });
            ParameterBus.Start();
            do
            {
                PullSettings();
            } while (true);
        }

        public void PullSettings()
        {
            ParameterBus.Publish(new ParameterEvent {Type = "ParameterEvent"});
            Thread.Sleep(5000);
        }
        public void Stop()
        {
            ParameterBus.Stop();
        }
    }
}
