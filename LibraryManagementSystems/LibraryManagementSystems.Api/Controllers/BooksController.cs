using LibraryManagementSystems.Application.Commands.Book;
using LibraryManagementSystems.Application.Dtos.Request.Book;
using LibraryManagementSystems.Application.Queries.Book;
using LibraryManagementSystems.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagementSystems.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;
        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookAsync([FromBody] CreateBookRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.Email);
            var createBook = new CreateBookCommand
            {
                Title = request.Title,
                Author = request.Author,
                ISBN = request.ISBN,
                PublishDate = request.PublishDate,
                UserId = userId
            };
            var result = await _mediator.Send(createBook);
            return result.Succeeded ? StatusCode(201, result) : BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooksAsync([FromQuery] GetAllBooksQuery query)
        {
            var result = await _mediator.Send(query);
            if (!result.Succeeded)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBookAsync(int id, [FromBody] UpdateBookRequestModel request)
        {
            var userId = User.FindFirstValue(ClaimTypes.Email);
            var command = new UpdateBookCommand
            {
                id = id,
                Title = request.Title,
                Author = request.Author,
                ISBN = request.ISBN,
                UserId = userId
            };

            var result = await _mediator.Send(command);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            var command = new DeleteBookCommand { id = id };
            var result = await _mediator.Send(command);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }
    }
}

