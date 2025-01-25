using Repositories;
using Servicies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionDeLibros
{
    internal class InitiateService
    {
        internal ServiceUpdateBook serviceUpdateBook;
        internal ServiceDeleteBook serviceDeleteBook;
        internal ServiceGetBook serviceGetBook;
        internal ServiceAddBook serviceAddBook;
        internal InitiateService() // Initiating the servicies
        {
            var bookRepository = new BookRepository();
            var logErrorRepository = new LogErrorRepository();
            var changeLogRepository = new ChangeLogRepository();
            serviceUpdateBook = new ServiceUpdateBook(bookRepository, logErrorRepository, changeLogRepository);
            serviceDeleteBook = new ServiceDeleteBook(bookRepository, logErrorRepository, changeLogRepository);
            serviceGetBook = new ServiceGetBook(bookRepository, logErrorRepository, changeLogRepository);
            serviceAddBook = new ServiceAddBook(bookRepository, logErrorRepository, changeLogRepository);
        }
    }
}
