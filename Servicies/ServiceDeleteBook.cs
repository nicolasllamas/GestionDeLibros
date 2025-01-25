using Common.Exceptions;
using Common;
using Data_Access.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Servicies
{
    public class ServiceDeleteBook : AbstractService
    {
        public ServiceDeleteBook(IBookRepository bookRepository, ILogErrorRepository logErrorRepository, IChangeLogRepository changeLogRepository)
            : base(bookRepository, logErrorRepository, changeLogRepository) { }

        public ResponseDTO DeleteBook(Book book) // Soft delete
            => PerformBookOperation(book, _bookRepository.DeleteBook, "Delete");

        public ResponseDTO RestoreBook(Book book) // Restoring book that was soft deleted
            => PerformBookOperation(book, _bookRepository.RestoreBook, "Restore");

        public ResponseDTO HardDeleteBook(Book book) // Hard delete (permanent)
        {
            return PerformBookOperation(book, bookToDelete =>
            {
                // Unlink related ChangeLogs before deleting the book
                _changeLogRepository.UnlinkBookFromChangeLogs(bookToDelete.BookId);
                _bookRepository.HardDeleteBook(bookToDelete);
            }, "Hard Delete");
        }

        private ResponseDTO PerformBookOperation(Book book, Action<Book> operation, string operationType)
        {
            var response = new ResponseDTO { IsSuccess = false };
                response = TryExecute<InvalidInputException>(() =>
                {
                    // Log the change
                    string oldValue = $"Title: {book.Title}, Author: {book.Author}, Stock: {book.Stock}, Price: {book.Price}";
                    string newValue = $"Status: {operationType}"; // Change to status depending on the operation

                    // The log is added first, so that if the operation fails, the log is still added
                    _changeLogRepository.AddChangeLog(book.BookId, operationType, oldValue, newValue);

                    // Perform the operation (soft delete, restore, or hard delete)
                    operation(book);
                }, response);

            return response;
        }

    }
}
