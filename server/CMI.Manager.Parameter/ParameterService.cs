using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        public void Stop()
        {
            ParameterBus.Stop();
        }
    }
}
