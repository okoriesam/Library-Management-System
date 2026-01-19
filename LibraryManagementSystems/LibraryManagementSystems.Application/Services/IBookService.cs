using LibraryManagementSystems.Application.Dtos.Request.Book;
using LibraryManagementSystems.Application.Dtos.Response.Book;
using LibraryManagementSystems.Application.Queries.Book;
using LibraryManagementSystems.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystems.Application.Services
{
    public interface IBookService
    {
        Task<Result<string>> CreateBookAsync(CreateBookRequestModel request, string userId);
        Task<Result<IEnumerable<BookResponseModel>>> GetAllBookAsync(GetAllBooksQuery query);
        Task<Result<string>> UpdateBookAsync(int id, UpdateBookRequestModel request, string userId);
        Task<Result<string>> DeleteBookAsync(int id);
    }
}
