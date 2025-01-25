using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Models;

namespace Repositories
{
    public interface IBookRepository
    {
        void AddBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(Book book);
        void RestoreBook(Book book);
        void HardDeleteBook(Book book);
        Book GetBookByTitle(string title);
        List<Book> GetAllBooks();
    }
}
