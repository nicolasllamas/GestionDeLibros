using Common.Exceptions;
using Common;
using Data_Access.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicies
{
    public class ServiceDeleteBook : AbstractService
    {
        public ServiceDeleteBook(IBookRepository bookRepository, ILogErrorRepository logErrorRepository, IChangeLogRepository changeLogRepository)
            : base(bookRepository, logErrorRepository, changeLogRepository) { }

        public ResponseDTO DeleteBook(int id) // Soft delete
            => PerformBookOperation(id, _bookRepository.DeleteBook, "Delete");

        public ResponseDTO RestoreBook(int id) // Restoring book that was soft deleted
            => PerformBookOperation(id, _bookRepository.RestoreBook, "Restore");

        public ResponseDTO HardDeleteBook(int id) // Hard delete (permanent)
            => PerformBookOperation(id, _bookRepository.HardDeleteBook, "Hard Delete");

        private ResponseDTO PerformBookOperation(int id, Action<int> operation, string operationType)
        {
            var response = new ResponseDTO { IsSuccess = false };

            var bookResponse = GetBookById(id);
            if (bookResponse.IsSuccess && bookResponse.Result is Book oldBook)
            {
                response = TryExecute<InvalidInputException>(() =>
                {
                    // Perform the operation (soft delete, restore, or hard delete)
                    operation(id);

                    // Log the change
                    string oldValue = $"Title: {oldBook.Title}, Author: {oldBook.Author}, Stock: {oldBook.Stock}, Price: {oldBook.Price}";
                    string newValue = $"Status: {operationType}"; // Change to status depending on the operation

                    // Add to change log
                    _changeLogRepository.AddChangeLog(id, operationType, oldValue, newValue);
                });
            }
            else
            {
                response.Message = bookResponse.Message; // Return error if book not found
            }

            return response;
        }
    }
}
