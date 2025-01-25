using Data_Access;
using Data_Access.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;
        public BookRepository()
        {
            _context = new ApplicationDbContext();
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }
        public void DeleteBook(Book book) // soft delete
        {
            book.IsDeleted = true;
            _context.Books.Update(book);
            _context.SaveChanges();
        }
        public void RestoreBook(Book book) // soft delete
        {
            book.IsDeleted = false;
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public void HardDeleteBook(Book book) // hard delete, in case it is needed in the future
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
        }
        public Book GetBookByTitle(string title)
        {
            return _context.Books.FirstOrDefault(x => EF.Functions.Like(x.Title, $"%{title}%"));
        }
        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }
    }
}
