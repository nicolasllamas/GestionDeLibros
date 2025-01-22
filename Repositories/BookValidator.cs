using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public static class BookValidator
    {
        public static bool IsValidPrice(decimal price)
        {
            return price > 0;
        }

        public static bool IsValidStock(int stock)
        {
            return stock >= 0;
        }

        public static bool IsValidBookData(string title, string author, int stock, decimal price)
        {
            return !string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(author) && IsValidStock(stock) && IsValidPrice(price);
        }

    }
}
