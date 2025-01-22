using Data_Access;
using Data_Access.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class LogErrorRepository : ILogErrorRepository
    {
        private readonly ApplicationDbContext _context;
        public LogErrorRepository()
        {
            _context = new ApplicationDbContext();
        }
        public void AddLogError(string mesaje)
        {
            _context.LogErrors.Add(new LogError { ErrorMessage = mesaje });
            _context.SaveChanges();
        }
    }
}
