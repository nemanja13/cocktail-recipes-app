using Application.Queries.RecipeQueries;
using Application.Searches;
using Application;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/recipes")]
    [ApiController]
    public class RecipesController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public RecipesController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<RecipesController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchRecipeDto search, [FromServices] IGetRecipesQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET api/<RecipesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetRecipeQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

    }
}
