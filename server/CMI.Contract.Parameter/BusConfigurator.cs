using MassTransit;
using System;
using GreenPipes;
using GreenPipes.Configurators;
using MassTransit.RabbitMqTransport;

namespace CMI.Contract.Parameter
{
    public static class BusConfigurator
    {
        public static string UserName => "guest";
        public static string Password => "guest";
        public static string Uri => "rabbitmq://localhost";

        public static IBusControl ConfigureBus(Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost> registrationAction = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri(Uri), hst =>
                {
                    hst.Username(UserName);
                    hst.Password(Password);
                });

                registrationAction?.Invoke(cfg, host);
            });
        }

        public static void ConfigureDefaultRetryPolicy(IRetryConfigurator retryPolicy)
        {
            retryPolicy.Exponential(10, TimeSpan.FromSeconds(1), TimeSpan.FromMinutes(5), TimeSpan.FromSeconds(5));
        }
    }
}
