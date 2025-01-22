using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicies
{
    public class ServiceUpdateBook : AbstractService
    {
        public ServiceUpdateBook(IBookRepository bookRepository, ILogErrorRepository logErrorRepository)
            : base(bookRepository, logErrorRepository) { }

        public bool UpdateBook(int id, string title, string author, int stock, decimal price)
        {
            bool bookUpdated;
            if (id <= 0 || string.IsNullOrEmpty(title) || string.IsNullOrEmpty(author) || stock < 0 || price <= 0)
            {
                bookUpdated = false;
            }
            else
            {
                bookUpdated = TryExecute(() =>
                {
                    var book = _bookRepository.GetBookById(id);
                    if (book != null)
                    {
                        book.Title = title;
                        book.Author = author;
                        book.Stock = stock;
                        book.Price = price;
                        _bookRepository.UpdateBook(book);
                    }
                }, "An error occurred while updating the book.");
            }
            return bookUpdated;
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
