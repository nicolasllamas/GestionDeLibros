using Common;
using Data_Access.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Exceptions;

namespace Servicies
{
    public class ServiceAddBook : AbstractService
    {
        public ServiceAddBook(IBookRepository bookRepository, ILogErrorRepository logErrorRepository, IChangeLogRepository changeLogRepository)
            : base(bookRepository, logErrorRepository, changeLogRepository) { }

        public ResponseDTO AddBook(string title, string author, int stock, decimal price)
        {
            var response = new Common.ResponseDTO();

            if (!BookValidator.IsValidBookData(title, author, stock, price))
            {
                response.Message = new InvalidInputException().Message;
            }

            else
            {
                response = TryExecute<InvalidInputException>(() =>
                {
                    var book = new Book { Title = title, Author = author, Stock = stock, Price = price };
                    _bookRepository.AddBook(book);
                    response.Result = book;
                }, response);
            }
            return response;
        }
    }
}
