using System;
using System.Web.Http;
using CMI.Contract.Parameter;
using CMI.Contract.Parameter.GetParameter;
using CMI.Contract.Parameter.SaveParameter;
using CMI.Web.Management.Attributes;
using CMI.Web.Management.Helpers;
using MassTransit;

namespace CMI.Web.Management.Controllers
{
    [CamelCaseJson]
    public class ParameterController : ApiController
    {
        [Route(@"~/Controllers/GetAllParameters")]
        [HttpGet]
        public IHttpActionResult GetAllParameters()
        {
            var requestClient = BusHelper.ParameterBus.CreateRequestClient<GetParameterRequest, GetParameterResponse>(new Uri(BusHelper.ParameterBus.Address, "GetParameterQueue"), TimeSpan.FromSeconds(15));
            var result = requestClient.Request(new GetParameterRequest()).GetAwaiter().GetResult();
            
            return Ok(result.Parameters);
        }

        [Route(@"~/Controllers/SaveParameter")]
        [HttpPost]
        public IHttpActionResult SaveParameter(Parameter parameter)
        {
            var requestClient = BusHelper.ParameterBus.CreateRequestClient<SaveParameterRequest, SaveParameterResponse>(new Uri(BusHelper.ParameterBus.Address, "SaveParameterQueue"), TimeSpan.FromSeconds(700));
            var result = requestClient.Request(new SaveParameterRequest(parameter)).GetAwaiter().GetResult();
            return Ok(result.Success);
        }
    }
}
