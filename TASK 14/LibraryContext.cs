
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

public class LibraryContext : DbContext
{
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Borrower> Borrowers => Set<Borrower>();
    public DbSet<Loan> Loans => Set<Loan>();

    protected override void OnConfiguring(
        DbContextOptionsBuilder options)
    {
        options.UseSqlServer(
    @"Server=.\SQLEXPRESS;Database=LibraryDB;Trusted_Connection=True;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        // Author 1 to Many Books
        modelBuilder.Entity<Author>()
            .HasMany(a => a.Books)
            .WithOne(b => b.Author)
            .HasForeignKey(b => b.AuthorId);

        // Composite Primary Key for Loan
        modelBuilder.Entity<Loan>()
            .HasKey(l => new
            {
                l.BookId,
                l.BorrowerId,
                l.LoanDate
            });

        // Book 1 to Many Loans
        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Book)
            .WithMany(b => b.Loans)
            .HasForeignKey(l => l.BookId);

        // Borrower 1 to Many Loans
        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Borrower)
            .WithMany(b => b.Loans)
            .HasForeignKey(l => l.BorrowerId);
    }
}
