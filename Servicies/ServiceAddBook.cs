using Data_Access.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicies
{
    public class ServiceAddBook : AbstractService
    {
        public ServiceAddBook(IBookRepository bookRepository, ILogErrorRepository logErrorRepository)
            : base(bookRepository, logErrorRepository) { }

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
    }
}
