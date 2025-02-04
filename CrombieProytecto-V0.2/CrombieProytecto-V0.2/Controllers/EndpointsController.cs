using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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
                    Path = api.RelativePath,
                    // Se agrupan los parámetros por nombre para evitar duplicados y se crea un único objeto JSON (diccionario)
                    Parameters = api.ParameterDescriptions
                        .SelectMany(p => GetParameterDetails(p))
                        .GroupBy(x => x.Name)
                        .ToDictionary(g => g.Key, g => g.First().Type)
                })
                .ToList();

            return Ok(endpoints);
        }

        private IEnumerable<(string Name, string Type)> GetParameterDetails(ApiParameterDescription p)
        {
            // Si el parámetro es complejo, usamos reflexión para obtener sus propiedades
            if (p.Type != null && !IsSimpleType(p.Type))
            {
                foreach (var prop in p.Type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    yield return (prop.Name, prop.PropertyType.Name);
                }
            }
            else
            {
                yield return (p.Name, p.Type?.Name);
            }
        }

        private bool IsSimpleType(System.Type type)
        {
            return type.IsPrimitive
                   || type.IsEnum
                   || type.Equals(typeof(string))
                   || type.Equals(typeof(decimal))
                   || type.Equals(typeof(System.DateTime))
                   || type.Equals(typeof(System.DateTimeOffset))
                   || type.Equals(typeof(System.TimeSpan))
                   || type.Equals(typeof(System.Guid));
        }
    }
}
