using Microsoft.EntityFrameworkCore;

using var db = new LibraryContext();

var books = db.Books
    .Include(b => b.Author)
    .ToList();

foreach (var book in books)
{
    Console.WriteLine(
        $"Book: {book.Title}, Author: {book.Author.Name}");
}