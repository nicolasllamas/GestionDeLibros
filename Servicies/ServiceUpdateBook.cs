using Common;
using Common.Exceptions;
using Data_Access.Models;
using Repositories;

namespace Servicies
{
    public class ServiceUpdateBook : AbstractService
    {
        public ServiceUpdateBook(IBookRepository bookRepository, ILogErrorRepository logErrorRepository, IChangeLogRepository changeLogRepository)
            : base(bookRepository, logErrorRepository, changeLogRepository) { }

        public ResponseDTO UpdateBook(Book oldBook, string newTitle, string newAuthor, int newStock, decimal newPrice)
        {
            var response = new ResponseDTO { IsSuccess = false };

            // Checking the inputs are valid
            if (!BookValidator.IsValidBookData(newTitle, newAuthor, newStock, newPrice)) { response.Message = new InvalidInputException().Message; }
            else { response = UpdateBookDetails(oldBook, newTitle, newAuthor, newStock, newPrice, response); }

            return response;
        }

        private ResponseDTO UpdateBookDetails(Book oldBook, string newTitle, string newAuthor, int newStock, decimal newPrice, ResponseDTO response)
        {
            // Save old values for logs
            string oldValue = $"{oldBook.Title}, {oldBook.Author}, {oldBook.Stock}, {oldBook.Price}";

            //Update the book
            response = ApplyBookUpdates(oldBook, book =>
            {
                book.Title = newTitle;
                book.Author = newAuthor;
                book.Stock = newStock;
                book.Price = newPrice;
            }, response);

            // If the update was successful, save changes to the ChangeLog
            if (response.IsSuccess == true)
            {
                string fieldName = "All fields";
                string newValue = $"{newTitle}, {newAuthor}, {newStock}, {newPrice}";
                _changeLogRepository.AddChangeLog(oldBook.BookId, fieldName, oldValue, newValue);
            }

            return response;
        }

        public ResponseDTO UpdateBookStock(Book book, int stock)
        {
            var response = new ResponseDTO { IsSuccess = false };

            // Checking the input is valid

            if (!BookValidator.IsValidStock(stock)) { response.Message = new InvalidInputException().Message; }
            else { response = UpdateBookStockDetails(book, stock, response); }

            return response;
        }

        private ResponseDTO UpdateBookStockDetails(Book book, int stock, ResponseDTO response)
        {

            string fieldName = "Stock";
            string oldValue = book.Stock.ToString();
            string newValue = stock.ToString();

            // Update the stock
            response = ApplyBookUpdates(book, book => book.Stock = stock, response);

            // If the update was successful, save the changes to the ChangeLog
            if (response.IsSuccess == true)
            {
                _changeLogRepository.AddChangeLog(book.BookId, fieldName, oldValue, newValue);
            }

            return response;
        }

        public ResponseDTO UpdateBookPrice(Book book, decimal price)
        {
            var response = new ResponseDTO { IsSuccess = false };

            // Checking the input is valid

            if (!BookValidator.IsValidPrice(price)) { response.Message = new InvalidInputException().Message; }
            else { response = UpdateBookPriceDetails(book, price, response); }

            return response;
        }

        private ResponseDTO UpdateBookPriceDetails(Book book, decimal price, ResponseDTO response)
        {

            string fieldName = "Price";
            string oldValue = book.Stock.ToString();
            string newValue = price.ToString();

            // Update the stock
            response = ApplyBookUpdates(book, book => book.Price = price, response);

            // If the update was successful, save the changes to the ChangeLog
            if (response.IsSuccess == true)
            {
                _changeLogRepository.AddChangeLog(book.BookId, fieldName, oldValue, newValue);
            }

            return response;
        }

        private ResponseDTO ApplyBookUpdates(Book book, Action<Book> updateAction, ResponseDTO response)
        {
            response = TryExecute<InvalidInputException>(() =>
            {
                updateAction(book);
                _bookRepository.UpdateBook(book);
                response.Result = book; // Return the updated book
            }, response);

            return response;
        }
    }
}
