using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access;

namespace Repositories
{
    public interface IBookRepository
    {
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(int id);
        void HardDeleteBook(int id);
        Book GetBookById(int id);
        List<Book> GetAllBooks();
        //void DetachBook(Book book);
    }
}
