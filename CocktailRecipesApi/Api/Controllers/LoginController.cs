using Api.Core.Jwt;
using Application.DataTransfer;
using FluentValidation;
using Implementation.Validators;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly JwtManager manager;

        public LoginController(JwtManager manager)
        {
            this.manager = manager;
        }

        // POST api/<LoginController>
        [HttpPost]
        public IActionResult Post([FromBody] UserLoginRequest request, [FromServices] UserLoginRequestValidator validator)
        {
            validator.ValidateAndThrow(request);

            var token = manager.MakeToken(request.Username, request.Password);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { token });
        }
    }
}
