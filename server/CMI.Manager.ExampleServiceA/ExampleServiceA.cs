using System;
using System.Linq;
using CMI.Contract.Parameter;
using CMI.Contract.Parameter.GetParameter;
using CMI.Contract.Parameter.SaveParameter;
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
                    ep.Handler<GetParameterEvent>(context =>
                    {
                        var exampleParameterA = ParameterHelper.GetParameters(new ExampleParameterA());
                        var exampleParameterAList = ParameterHelper.GetParameterListFromParameter(exampleParameterA);
                        ParameterBus.Publish(new GetParameterEventResponse { Parameters = exampleParameterAList });
                        return Console.Out.WriteLineAsync("Get Parameters");
                    });
                });
                cfg.ReceiveEndpoint(host, "SaveAllParametersA", ep =>
                {
                    ep.Handler<SaveParameterEvent>(context =>
                    {
                        var exampleParameterA = ParameterHelper.GetParameters(new ExampleParameterA());
                        var exampleParameterAList = ParameterHelper.GetParameterListFromParameter(exampleParameterA);
                        exampleParameterAList.First(p => p.Name == context.Message.Parameter.Name).Value = context.Message.Parameter.Value;
                        ParameterBus.Publish(new SaveParameterEventResponse { Success = ParameterHelper.SaveParameter(exampleParameterA, exampleParameterAList) });
                        return Console.Out.WriteLineAsync("Saved Parameter");
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