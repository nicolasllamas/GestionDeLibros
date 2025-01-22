using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IChangeLogRepository
    {
        void AddChangeLog(int bookId, string fieldName, string oldValue, string newValue);
    }
}
