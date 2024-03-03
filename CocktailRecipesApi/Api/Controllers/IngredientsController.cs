using Application.Commands.IngredientCommands;
using Application.DataTransfer;
using Application.Queries.IngredientQueries;
using Application.Searches;
using Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/ingredients")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public IngredientsController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/<IngredientsController>
        [HttpGet]
        public IActionResult Get([FromQuery] SearchIngredientDto search, [FromServices] IGetIngredientsQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET api/<IngredientsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetIngredientQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST api/<IngredientsController>
        [Authorize]
        [HttpPost]
        public IActionResult Post([FromBody] IngredientDto dto, [FromServices] ICreateIngredientCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT api/<IngredientsController>/5
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] IngredientDto dto, [FromServices] IUpdateIngredientCommand command)
        {
            dto.Id = id;
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        // DELETE api/<IngredientsController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteIngredientCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
