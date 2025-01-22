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
        public ServiceDeleteBook(IBookRepository bookRepository, ILogErrorRepository logErrorRepository)
            : base(bookRepository, logErrorRepository) { }

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

        public bool RestoreBook(int id) // Restoring book that was soft deleted
        {
            bool bookDeleted = false;
            if (id >= 0)
            {
                bookDeleted = TryExecute(() =>
                {
                    _bookRepository.RestoreBook(id);
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
    }
}
