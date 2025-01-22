using Common;
using Common.Exceptions;
using Data_Access.Models;
using Repositories;
using System.Collections.Generic;

namespace Servicies
{
    public class ServiceUpdateBook : AbstractService
    {
        public ServiceUpdateBook(IBookRepository bookRepository, ILogErrorRepository logErrorRepository, IChangeLogRepository changeLogRepository)
            : base(bookRepository, logErrorRepository, changeLogRepository) { }

        public ResponseDTO UpdateBook(int id, string title, string author, int stock, decimal price)
        {
            var response = new ResponseDTO { IsSuccess = false };

            // Check if the input is valid
            if (!BookValidator.IsValidBookData(title, author, stock, price))
            {
                response.Message = new InvalidInputException().Message;
            }
            else
            {
                response = UpdateBookDetails(id, title, author, stock, price, response);
            }
            return response;
        }

        private ResponseDTO UpdateBookDetails(int id, string title, string author, int stock, decimal price, ResponseDTO response)
        {
            var bookResponse = GetBookById(id);

            // Check if the book exists
            if (bookResponse.IsSuccess == true && bookResponse.Result is Book oldBook)
            {
                response = UpdateValidBookDetails(id, title, author, stock, price, oldBook);
            }
            // If the book does not exist, return the error message
            else
            {
                response.Message = bookResponse.Message;
            }

            return response;
        }

        private ResponseDTO UpdateValidBookDetails(int id, string title, string author, int stock, decimal price, Book oldBook)
        {
            ResponseDTO response;
            // If the book exists, save the old values
            string oldValue = $"{oldBook.Title}, {oldBook.Author}, {oldBook.Stock}, {oldBook.Price}";

            // Only then, update the book
            response = ApplyBookUpdates(oldBook, book =>
            {
                book.Title = title;
                book.Author = author;
                book.Stock = stock;
                book.Price = price;
            });

            // If the update was successful, save changes to the ChangeLog
            if (response.IsSuccess == true)
            {
                string fieldName = "All fields";
                string newValue = $"{title}, {author}, {stock}, {price}";
                _changeLogRepository.AddChangeLog(id, fieldName, oldValue, newValue);
            }

            return response;
        }

        public ResponseDTO UpdateBookStock(int id, int stock)
        {
            var response = new ResponseDTO { IsSuccess = false };

            // First check that the input is valid
            if (!BookValidator.IsValidStock(stock))
            {
                response.Message = new InvalidInputException().Message;
            }
            else
            {
                // If the input is valid, check if the book exists
                response = UpdateBookStockDetails(id, stock, response);
            }

            return response;
        }

        private ResponseDTO UpdateBookStockDetails(int id, int stock, ResponseDTO response)
        {
            var bookResponse = GetBookById(id);

            if (bookResponse.IsSuccess == true && bookResponse.Result is Book oldBook)
            {
                // Since the book exists, save the old value of the "Stock" property
                string fieldName = "Stock";
                string oldValue = oldBook.Stock.ToString();
                string newValue = stock.ToString();

                // Then update the book
                response = ApplyBookUpdates(oldBook, book => book.Stock = stock);

                // If the update was successful, save the changes to the ChangeLog
                if (response.IsSuccess == true)
                {
                    _changeLogRepository.AddChangeLog(id, fieldName, oldValue, newValue);
                }
            }
            else
            {
                response.Message = bookResponse.Message;
            }
            return response;
        }

        public ResponseDTO UpdateBookPrice(int id, decimal price)
        {
            var response = new ResponseDTO { IsSuccess = false };

            if (!BookValidator.IsValidPrice(price))
            {
                response.Message = new InvalidInputException().Message;
            }
            else
            {
                // If the input is valid, check if the book exists
                response = UpdateBookPriceDetails(id, price, response);
            }

            return response;
        }

        private ResponseDTO UpdateBookPriceDetails(int id, decimal price, ResponseDTO response)
        {
            var bookResponse = GetBookById(id);

            if (bookResponse.IsSuccess == true && bookResponse.Result is Book oldBook)
            {
                // Since the book exists, save the old value of the "Price" property
                string fieldName = "Price";
                string oldValue = oldBook.Price.ToString();
                string newValue = price.ToString();

                // Then update the book
                response = ApplyBookUpdates(oldBook, book => book.Price = price);

                // If the update was successful, save the changes to the ChangeLog
                if (response.IsSuccess == true)
                {
                    _changeLogRepository.AddChangeLog(id, fieldName, oldValue, newValue);
                }
            }
            else
            {
                response.Message = bookResponse.Message;
            }

            return response;
        }

        private ResponseDTO ApplyBookUpdates(Book book, Action<Book> updateAction)
        {
            var response = new ResponseDTO { IsSuccess = false };

            response = TryExecute<InvalidInputException>(() =>
            {
                updateAction(book);
                _bookRepository.UpdateBook(book);
                response.Result = book; // Return the updated book
            });

            return response;
        }
    }
}
