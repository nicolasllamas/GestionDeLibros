using Data_Access.Models;
using Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ChangeLogRepository
    {
        private readonly ApplicationDbContext _context;
        public ChangeLogRepository()
        {
            _context = new ApplicationDbContext();
        }
        public void AddChangeLog(int bookId, string fieldName, string oldValue, string newValue)
        {
            _context.ChangeLog.Add(new ChangeLog { BookId = bookId, FieldName = fieldName, OldValue = oldValue, NewValue = newValue, ChangeDate = DateTime.Now});
            _context.SaveChanges();
        }   
    }
}
