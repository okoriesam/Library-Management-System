using LibraryManagementSystems.Application.Dtos.Response.Book;
using LibraryManagementSystems.Application.Responses;
using LibraryManagementSystems.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystems.Application.Queries.Book
{
    public class GetAllBooksQuery : IRequest<Result<IEnumerable<BookResponseModel>>>
    {
        public string? search {  get; set; }
        public int pageSize { get; set; } = 20;
        public int pageNumber { get; set; } = 1;
    }

    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, Result<IEnumerable<BookResponseModel>>>
    {
        private readonly IBookService _bookService;
        public GetAllBooksQueryHandler(IBookService bookService)
        {
            _bookService = bookService;   
        }
        public async Task<Result<IEnumerable<BookResponseModel>>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _bookService.GetAllBookAsync(request);
                if (response == null)
                {
                    return Result<IEnumerable<BookResponseModel>>.Success(Enumerable.Empty<BookResponseModel>(), "No records found.");
                }

                return response;
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<BookResponseModel>>.Failure($"An unexpected error occurred. {ex.Message}");
            }
        }
    }
}
