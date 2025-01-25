using Data_Access.Models;
using Data_Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ChangeLogRepository : IChangeLogRepository
    {
        private readonly ApplicationDbContext _context;
        public ChangeLogRepository()
        {
            _context = new ApplicationDbContext();
        }
        public void AddChangeLog(Guid bookId, string fieldName, string oldValue, string newValue)
        {
            _context.ChangeLog.Add(new ChangeLog { BookId = bookId, FieldName = fieldName, OldValue = oldValue, NewValue = newValue, ChangeDate = DateTime.Now});
            _context.SaveChanges();
        }
        public void UnlinkBookFromChangeLogs(Guid bookId) // For permanent deletion
        {
            var changeLogs = _context.ChangeLog.Where(cl => cl.BookId == bookId).ToList();

            foreach (var changeLog in changeLogs)
            {
                changeLog.BookId = null; 
            }

            _context.SaveChanges();
        }
    }
}
