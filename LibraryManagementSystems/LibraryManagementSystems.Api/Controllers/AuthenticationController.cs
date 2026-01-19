using LibraryManagementSystems.Application.Commands.User;
using LibraryManagementSystems.Application.Dtos.Request.User;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystems.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegistrationRequestModel requestModel)
        {
            var register = new UserRegistrationCommand
            {
                Email = requestModel.Email,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                PhoneNumber = requestModel.PhoneNumber,
                Password = requestModel.Password,
            };
            var result = await _mediator.Send(register);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestModel requestModel)
        {
            var login = new UserLoginCommand
            {
                Email = requestModel.Email,
                Password = requestModel.Password,
            };

            var result = await _mediator.Send(login);
            return result.Succeeded ? Ok(result) : BadRequest(result);
           
        }
    }
}
