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
    public class UpdateBookCommand : UpdateBookRequestModel, IRequest<Result<string>>
    {
        public int id { get; set; }
        public string UserId { get; set; }
    }

    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, Result<string>>
    {
        private readonly IBookService _bookService;
        public UpdateBookCommandHandler(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task<Result<string>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _bookService.UpdateBookAsync(request.id, request, request.UserId);
                if (response == null)
                {
                    return Result<string>.Failure("failed to update Book");
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
