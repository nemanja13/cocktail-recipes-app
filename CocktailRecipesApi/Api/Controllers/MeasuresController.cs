using Application.Queries.MeasureQueries;
using Application.Searches;
using Application;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/measures")]
    [ApiController]
    public class MeasuresController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public MeasuresController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<MeasuresController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchMeasureDto search, [FromServices] IGetMeasuresQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }
    }
}

