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
        public void DeleteBook(int id) // soft delete
        {
            var book = _context.Books.Find(id);
            book.IsDeleted = true;
            _context.Books.Update(book);
            _context.SaveChanges();
        }
        public void RestoreBook(int id) // soft delete
        {
            var book = _context.Books.Find(id);
            book.IsDeleted = false;
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public void HardDeleteBook(int id) // hard delete, in case it is needed in the future
        {
            var book = _context.Books.Find(id);
            _context.Books.Remove(book);
            _context.SaveChanges();
        }
        public Book GetBookById(int id)
        {
            return _context.Books.Find(id);
        }
        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }
    }
}
