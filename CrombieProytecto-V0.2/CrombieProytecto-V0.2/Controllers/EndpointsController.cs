using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Collections.Generic;
using System.Linq;

namespace CrombieProytecto_V0._2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EndpointsController : ControllerBase
    {
        private readonly IApiDescriptionGroupCollectionProvider _apiDescriptionProvider;

        public EndpointsController(IApiDescriptionGroupCollectionProvider apiDescriptionProvider)
        {
            _apiDescriptionProvider = apiDescriptionProvider;
        }

        [HttpGet("list")]
        public IActionResult GetAllEndpoints()
        {
            var endpoints = _apiDescriptionProvider.ApiDescriptionGroups.Items
                .SelectMany(group => group.Items)
                .Select(api => new
                {
                    Method = api.HttpMethod,
                    Path = api.RelativePath
                })
                .ToList();

            return Ok(endpoints);
        }
    }
}
