using AutoMapper;
using LibraryManagementSystems.Application.Dtos.Request.Book;
using LibraryManagementSystems.Application.Dtos.Response.Book;
using LibraryManagementSystems.Application.Queries.Book;
using LibraryManagementSystems.Domain.Entity;
using LibraryManagementSystems.Infrastructure.Persistence.Data;
using LibraryManagementSystems.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystems.Test
{
    public class BookServiceTests
    {
        private readonly Mock<IMapper> _mapperMock;
        private readonly ApplicationDbContext _context;
        private readonly BookService _bookService;

        public BookServiceTests()
        {
           
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _mapperMock = new Mock<IMapper>();
            _bookService = new BookService(_context, _mapperMock.Object);
        }

        [Fact]
        public async Task CreateBookAsync_ShouldReturnSuccess_WhenBookIsCreated()
        {
            
            var request = new CreateBookRequestModel
            {
                Title = "Test Book",
                Author = "Author",
                ISBN = "123456",
                PublishDate = DateTime.Now
            };
            var userId = "user-123";

            // Act
            var result = await _bookService.CreateBookAsync(request, userId);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Equal("Test Book", result.Data);
            Assert.Single(_context.Books);
        }

        [Fact]
        public async Task GetAllBookAsync_ShouldReturnBooks_WhenBooksExist()
        {
            // Arrange
            _context.Books.Add(new Book { Id = 1, Title = "Book 1", Author = "A1", CreatedAt = DateTime.UtcNow });
            _context.Books.Add(new Book { Id = 2, Title = "Book 2", Author = "A2", CreatedAt = DateTime.UtcNow });
            await _context.SaveChangesAsync();

            var query = new GetAllBooksQuery { pageNumber = 1, pageSize = 10 };

            _mapperMock.Setup(m => m.Map<IEnumerable<BookResponseModel>>(It.IsAny<IEnumerable<Book>>()))
                       .Returns(new List<BookResponseModel> { new BookResponseModel { Title = "Book 1" } });

            // Act
            var result = await _bookService.GetAllBookAsync(query);

            // Assert
            Assert.True(result.Succeeded);
            Assert.NotNull(result.Data);
            Assert.NotEmpty(result.Data);
        }

        [Fact]
        public async Task DeleteBookAsync_ShouldReturnFailure_WhenBookDoesNotExist()
        {
            // Act
            var result = await _bookService.DeleteBookAsync(999); 

            // Assert
            Assert.False(result.Succeeded);
            Assert.Contains("not found", result.Message);
        }

        [Fact]
        public async Task UpdateBookAsync_ShouldUpdateWithOptionalFields()
        {
            // Arrange
            var existingBook = new Book { Id = 1, Title = "", Author = "Original", ISBN = "" };
            _context.Books.Add(existingBook);
            await _context.SaveChangesAsync();

            var updateRequest = new UpdateBookRequestModel { Title = "New Title" }; 

            // Act
            var result = await _bookService.UpdateBookAsync(1, updateRequest, "admin");

            // Assert
            var updatedBook = await _context.Books.FindAsync(1);
            Assert.Equal("New Title", updatedBook.Title);
            Assert.Equal("Original", updatedBook.Author); 
            Assert.Equal("admin", updatedBook.LastModifiedBy);
        }
    }
}
