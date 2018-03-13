﻿using System;
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
                        return Console.Out.WriteLineAsync($"Recived GetAllParameters: " + "I am an ExampleServiceBParameter!");
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
