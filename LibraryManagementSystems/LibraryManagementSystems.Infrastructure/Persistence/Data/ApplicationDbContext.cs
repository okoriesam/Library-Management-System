using LibraryManagementSystems.Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystems.Infrastructure.Persistence.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt) 
        {
              
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Book>().HasKey(b => b.Id);

            builder.Entity<Book>().Property(b => b.RowVersion).IsRowVersion();

            builder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", ISBN = "9780743273565", PublishDate = new DateTime(1925, 4, 10), CreatedAt = DateTime.UtcNow },
                new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", ISBN = "9780061120084", PublishDate = new DateTime(1960, 7, 11), CreatedAt = DateTime.UtcNow },
                new Book { Id = 3, Title = "1984", Author = "George Orwell", ISBN = "9780451524935", PublishDate = new DateTime(1949, 6, 8), CreatedAt = DateTime.UtcNow },
                new Book { Id = 4, Title = "The Catcher in the Rye", Author = "J.D. Salinger", ISBN = "9780316769488", PublishDate = new DateTime(1951, 7, 16), CreatedAt = DateTime.UtcNow },
                new Book { Id = 5, Title = "The Hobbit", Author = "J.R.R. Tolkien", ISBN = "9780547928227", PublishDate = new DateTime(1937, 9, 21), CreatedAt = DateTime.UtcNow },
                new Book { Id = 6, Title = "Fahrenheit 451", Author = "Ray Bradbury", ISBN = "9781451673319", PublishDate = new DateTime(1953, 10, 19), CreatedAt = DateTime.UtcNow },
                new Book { Id = 7, Title = "Pride and Prejudice", Author = "Jane Austen", ISBN = "9780141439518", PublishDate = new DateTime(1813, 1, 28), CreatedAt = DateTime.UtcNow },
                new Book { Id = 8, Title = "The Book Thief", Author = "Markus Zusak", ISBN = "9780375842207", PublishDate = new DateTime(2005, 3, 14), CreatedAt = DateTime.UtcNow },
                new Book { Id = 9, Title = "Animal Farm", Author = "George Orwell", ISBN = "9780451526342", PublishDate = new DateTime(1945, 8, 17), CreatedAt = DateTime.UtcNow },
                new Book { Id = 10, Title = "Brave New World", Author = "Aldous Huxley", ISBN = "9780060850524", PublishDate = new DateTime(1932, 1, 1), CreatedAt = DateTime.UtcNow }
            );
        }
    }
}
