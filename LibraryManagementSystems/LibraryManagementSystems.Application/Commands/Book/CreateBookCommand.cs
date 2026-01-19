using LibraryManagementSystems.Application.Dtos.Request.Book;
using LibraryManagementSystems.Application.Dtos.Response.Book;
using LibraryManagementSystems.Application.Responses;
using LibraryManagementSystems.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystems.Application.Commands.Book
{
    public class CreateBookCommand : CreateBookRequestModel, IRequest<Result<string>>
    {
        public string UserId { get; set; }
    }

    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, Result<string>>
    {
        private readonly IBookService _bookService;
        public CreateBookCommandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task<Result<string>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _bookService.CreateBookAsync(request, request.UserId);
                if (response == null)
                {
                    return Result<string>.Failure("Book creation failed");
                }

                return response;
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"An unexpected error occurred. {ex.Message}");
            }
        }
    }
}
