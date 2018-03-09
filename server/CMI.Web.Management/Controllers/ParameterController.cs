using System;
using System.Web.Http;
using System.Web.Routing;
using CMI.Contract.Parameter;
using CMI.Web.Management.Helpers;
using MassTransit;

namespace CMI.Web.Management.Controllers
{
    public class ParameterController : ApiController
    {
        [Route(@"~/Controllers/GetAllParameters")]
        [HttpGet]
        public IHttpActionResult GetAllParameters()
        {
            var requestClient = BusHelper.ParameterBus.CreateRequestClient<ParameterRequest, ParameterResponse>(new Uri(BusHelper.ParameterBus.Address, "GetParameterQueue"), TimeSpan.FromSeconds(10));
            var result = requestClient.Request(new ParameterRequest {Message = "API call!"}).GetAwaiter().GetResult();
            
            return Ok("API Task result: " + result.Message);
        }
    }
}
