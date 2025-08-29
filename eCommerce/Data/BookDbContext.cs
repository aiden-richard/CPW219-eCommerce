using eCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data;

public class BookDbContext : DbContext
{
    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Ensure unique constraints on Username and Email in Member entity
        modelBuilder.Entity<Member>()
            .HasIndex(m => m.Username)
            .IsUnique();

        modelBuilder.Entity<Member>()
            .HasIndex(m => m.Email)
            .IsUnique();
    }

    public DbSet<Book> Books { get; set; }

    public DbSet<Member> Members { get; set; }
}
