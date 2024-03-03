using Application.Queries.TypeQueries;
using Application.Searches;
using Application;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/types")]
    [ApiController]
    public class TypesController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public TypesController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<TypesController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchTypeDto search, [FromServices] IGetTypesQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }
    }
}
