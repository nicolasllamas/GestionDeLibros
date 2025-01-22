using Data_Access.Models;
using Repositories;
using System.Collections.Generic;

namespace Servicies
{
    public class ServiceGetBook : AbstractService
    {
        public ServiceGetBook(IBookRepository bookRepository, ILogErrorRepository logErrorRepository, IChangeLogRepository changeLogRepository)
            : base(bookRepository, logErrorRepository, changeLogRepository) { }

        // The methods are defined in the abstract class since they are needed in the other services
        // This class is only for the implementation of those methods
    }
}
