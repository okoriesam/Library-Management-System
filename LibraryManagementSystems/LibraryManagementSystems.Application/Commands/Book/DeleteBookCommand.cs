using LibraryManagementSystems.Application.Dtos.Request.Book;
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
    public class DeleteBookCommand : IRequest<Result<string>>
    {
        public int id { get; set; }
    }

    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Result<string>>
    {
        private readonly IBookService _bookService;
        public DeleteBookCommandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task<Result<string>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _bookService.DeleteBookAsync(request.id);
                if (response == null)
                {
                    return Result<string>.Failure("Book delection failed");
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
