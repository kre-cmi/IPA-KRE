using System;
using System.Linq;
using CMI.Contract.Parameter.GetParameter;
using CMI.Contract.Parameter.SaveParameter;
using MassTransit;

namespace CMI.Contract.Parameter
{
    public static class ParameterBusHelper
    {
        public static void SubscribeGetEvent<T>(IBus parameterBus) where T:IParameter
        {
            var paramInstance = Activator.CreateInstance(typeof(T)) as IParameter;
            paramInstance = ParameterHelper.GetParameters(paramInstance);
            parameterBus.Publish(new GetParameterEventResponse { Parameters = ParameterHelper.GetParameterListFromParameter(paramInstance) });
        }

        public static void SubscribeSaveEvent<T>(IBus parameterBus, Parameter paramToSave) where T:IParameter
        {
            var paramInstance = Activator.CreateInstance(typeof(T)) as IParameter;
            paramInstance = ParameterHelper.GetParameters(paramInstance);
            var paramAsParamList = ParameterHelper.GetParameterListFromParameter(paramInstance);
            if(paramAsParamList.Any(p => p.Name == paramToSave.Name))
            {
                paramAsParamList.First(p => p.Name == paramToSave.Name).Value = paramToSave.Value;
                parameterBus.Publish(new SaveParameterEventResponse { Success = ParameterHelper.SaveParameter(paramInstance, paramAsParamList) });
            }
        }
    }
}
