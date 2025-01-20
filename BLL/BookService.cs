using Data_Access;
using Repositories;
using System.Collections.Generic;

namespace BLL
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public bool AddBook(string title, string author, int stock, decimal price)
        {
            bool bookAdded;

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author) || stock < 0 || price <= 0) bookAdded = false;

            else
            {
                bookAdded = TryExecute(() =>
                {
                    var book = new Book { Title = title, Author = author, Stock = stock, Price = price };
                    _bookRepository.AddBook(book);
                }, "An error occurred while adding the book.");
            }
            return bookAdded;
        }

        public bool UpdateBook(int id, string title, string author, int stock, decimal price)
        {
            bool bookUpdated;
            if (id <= 0 || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author) || stock < 0 || price <= 0) bookUpdated = false;
            else
            {
                bookUpdated = TryExecute(() =>
                {
                    var book = new Book { Id = id, Title = title, Author = author, Stock = stock, Price = price };
                    _bookRepository.UpdateBook(book);
                }, "An error occurred while updating the book.");
            }
            return bookUpdated;
        }

        public bool DeleteBook(int id) // Soft delete
        {
            bool bookDeleted = false;
            if (id >= 0)
            {
                bookDeleted = TryExecute(() =>
                {
                    _bookRepository.DeleteBook(id);
                }, "An error occurred while deleting the book.");
            }
            return bookDeleted;
        }

        public bool HardDeleteBook(int id) // Hard delete, in case it is needed in the future
        {
            bool bookDeleted = false;
            if (id <= 0)
            {
                bookDeleted = TryExecute(() =>
                {
                    _bookRepository.HardDeleteBook(id);
                }, "An error occurred while permanently deleting the book.");
            }
            return bookDeleted;
        }

        private bool TryExecute(Action action, string errorMessage)
        {
            try
            {
                action();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(errorMessage, ex);
            }
        }
        public Book GetBookById(int id)
        {
            return _bookRepository.GetBookById(id);
        }
        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        public bool UpdateBookStock(int id, int stock)
        {
            var bookToUpdate = GetBookById(id);
            return UpdateBook(id, bookToUpdate.Title, bookToUpdate.Author, stock, bookToUpdate.Price);
        }
        public bool UpdateBookPrice(int id, decimal price)
        {
            var bookToUpdate = GetBookById(id);
            return UpdateBook(id, bookToUpdate.Title, bookToUpdate.Author, bookToUpdate.Stock, price);
        }
    }
}
