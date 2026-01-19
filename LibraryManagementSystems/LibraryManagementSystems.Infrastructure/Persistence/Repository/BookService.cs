using AutoMapper;
using LibraryManagementSystems.Application.Dtos.Request.Book;
using LibraryManagementSystems.Application.Dtos.Response.Book;
using LibraryManagementSystems.Application.Queries.Book;
using LibraryManagementSystems.Application.Responses;
using LibraryManagementSystems.Application.Services;
using LibraryManagementSystems.Domain.Entity;
using LibraryManagementSystems.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace LibraryManagementSystems.Infrastructure.Persistence.Repository
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public BookService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Result<string>> CreateBookAsync(CreateBookRequestModel request, string userId)
        {
            try
            {
                // Note that userId for Audit purpose
                var newBook = new Book
                {
                    Title = request.Title,
                    Author = request.Author,
                    ISBN = request.ISBN,
                    PublishDate = request.PublishDate,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = userId,
                    LastModifiedAt = DateTime.UtcNow,
                    LastModifiedBy = userId,
                };
                var addAsync = await _context.Books.AddAsync(newBook);
                var savedBooked = await _context.SaveChangesAsync();
                if(savedBooked == 0)
                {
                    return Result<string>.Failure("Book creation failed");
                }

                return Result<string>.Success(newBook.Title, "Book added successfully");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"An unexpected error occurred. {ex.Message}");
            }
        }

        public async Task<Result<string>> DeleteBookAsync(int id)
        {
            try
            {
                var findBookAsync = await _context.Books.FirstOrDefaultAsync(c => c.Id == id);
                if(findBookAsync == null)
                {
                    return Result<string>.Failure($"Book with id {id} not found");
                }

                _context.Books.Remove(findBookAsync);
               var saveditem = await _context.SaveChangesAsync();
                if (saveditem == 0) 
                {
                    return Result<string>.Failure("An unexpected error occurred...... while trying to delete book");
                }

                return Result<string>.Success(findBookAsync.Title, $"Book with title {findBookAsync.Title} Deleted successfully");
            }
            catch (Exception ex)
            {
                return Result<string>.Failure($"An unexpected error occurred. {ex.Message}");
            }
        }



        public async Task<Result<IEnumerable<BookResponseModel>>> GetAllBookAsync(GetAllBooksQuery query)
        {
            try
            {
                if (query.pageNumber <= 0) query.pageNumber = 1;
                if (query.pageSize <= 0) query.pageSize = 20;

                var booksQuery = _context.Books
                    .AsNoTracking()
                    .AsQueryable();

                //  Search by Title or Author
                if (!string.IsNullOrWhiteSpace(query.search))
                {
                    booksQuery = booksQuery.Where(b =>
                        b.Title.Contains(query.search) ||
                        b.Author.Contains(query.search));
                }

                var totalCount = await booksQuery.CountAsync();

                if (totalCount == 0)
                {
                    return Result<IEnumerable<BookResponseModel>>
                        .Success(Enumerable.Empty<BookResponseModel>(), "No books available");
                }

                var books = await booksQuery
                    .Skip((query.pageNumber - 1) * query.pageSize)
                    .Take(query.pageSize)
                    .ToListAsync();

                var response = _mapper.Map<IEnumerable<BookResponseModel>>(books);

                return Result<IEnumerable<BookResponseModel>>
                    .Success(response, "Books retrieved successfully");
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<BookResponseModel>>
                    .Failure($"An unexpected error occurred. {ex.Message}");
            }
        }


        public async Task<Result<string>> UpdateBookAsync(int id, UpdateBookRequestModel request, string userId)
        {
            // Note that userId for Audit purpose
            var findBookAsync = await _context.Books.FirstOrDefaultAsync(c => c.Id == id);
            if (findBookAsync == null)
            {
                return Result<string>.Failure($"Book with id {id} not found");
            }

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                findBookAsync.Title = request.Title;
            }

            if (!string.IsNullOrWhiteSpace(request.Author))
            {
                findBookAsync.Author = request.Author;
            }

            if (!string.IsNullOrWhiteSpace(request.ISBN))
            {
                findBookAsync.ISBN = request.ISBN;
            }
           

            findBookAsync.LastModifiedAt = DateTime.UtcNow;
            findBookAsync.LastModifiedBy = userId;


            var saveditem = await _context.SaveChangesAsync();
            if (saveditem == 0 && _context.Entry(findBookAsync).State != EntityState.Unchanged)
            {
                return Result<string>.Failure("Update failed at the database level.");
            }

            return Result<string>.Success(findBookAsync.Title, $"Book with title {findBookAsync.Title} updated successfully");
        }
    }
}
