using BookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {   public DbSet<Category>Categories { get; set; }
        public DbSet<Author>Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> bookCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<BookCategory>().HasKey(e => new
            {
                e.CatrgoryId,
                e.BookId,
            });
            base.OnModelCreating(builder);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}