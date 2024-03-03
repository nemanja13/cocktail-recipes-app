using Application.Commands.RecipeCommands;
using Application.DataTransfer;
using Application.Queries.CocktailQueries;
using Application.Searches;
using Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/cocktails")]
    [ApiController]
    public class CocktailsController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public CocktailsController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<CocktailsController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchCocktailDto search, [FromServices] IGetCocktailsQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET api/<CocktailsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetCocktailQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST api/<CocktailsController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] RecipeDto dto, [FromServices] ICreateRecipeCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<CocktailsController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RecipeDto dto, [FromServices] IUpdateRecipeCommand command)
        {
            dto.Id = id;
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<CocktailsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteRecipeCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
