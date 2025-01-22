using Servicies;
using Helper;
using Repositories;

namespace GestionDeLibros.Menus
{
    public static class Menu
    {
        private static ServiceAddBook _serviceAddBook;
        private static ServiceUpdateBook _serviceUpdateBook;
        private static ServiceDeleteBook _serviceDeleteBook;
        private static ServiceGetBook _serviceGetBook;

        static Menu() // Initiating the servicies
        {
            _serviceAddBook = new ServiceAddBook(new BookRepository(), new LogErrorRepository());
            _serviceUpdateBook = new ServiceUpdateBook(new BookRepository(), new LogErrorRepository());
            _serviceDeleteBook = new ServiceDeleteBook(new BookRepository(), new LogErrorRepository());
            _serviceGetBook = new ServiceGetBook(new BookRepository(), new LogErrorRepository());
        }

        public static void ShowMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== Gestion de Librería ===");
                Console.WriteLine("1. Ver lista de libros");
                Console.WriteLine("2. Agregar un nuevo libro");
                Console.WriteLine("3. Menú de Modificación");
                Console.WriteLine("4. Menú de Eliminación");
                Console.WriteLine("5. Salir");
                Console.Write("\nSeleccione una opción: ");

                var input = InputHelper.GetValidInt(1, 5, Common.Exceptions.GlobalErrorMessages.errorMessageInt);
                switch (input)
                {
                    case 1:
                        ShowBooks();
                        break;
                    case 2:
                        AddBook();
                        break;
                    case 3:
                        MenuModifyBook.OpenModifyMenu(_serviceUpdateBook);
                        break;
                    case 4:
                        MenuDeleteBook.OpenDeleteMenu(_serviceDeleteBook);
                        break;
                    case 5:
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }

                if (!exit)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        private static void ShowBooks()
        {
            var books = _serviceGetBook.GetAllBooks();

            Console.Clear();
            Console.WriteLine("=== Lista de Libros ===");

            if (books.Count == 0) { Console.WriteLine("\nNo hay libros registrados."); }
            else
            {
                foreach (var book in books)
                {
                    string status = book.Stock > 0 ? "Disponible" : "Agotado";

                    Console.WriteLine($"Id: {book.Id}, Título: {book.Title}, Autor: {book.Author}, Precio: ${book.Price:F2}, Stock: {book.Stock} ({status})");
                }
            }
        }

        private static void AddBook()
        {
            Console.Clear();
            Console.WriteLine("=== Agregar un nuevo libro ===");

            Console.Write("Ingrese el título: ");
            string title = InputHelper.GetNotNullString(Common.Exceptions.GlobalErrorMessages.errorMessageString);

            Console.Write("Ingrese el autor: ");
            string author = InputHelper.GetNotNullString(Common.Exceptions.GlobalErrorMessages.errorMessageString);

            Console.Write("Ingrese el precio: ");
            decimal price = InputHelper.GetPositiveDecimal(Common.Exceptions.GlobalErrorMessages.errorMessageString);

            Console.Write("Ingrese el stock: ");
            int stock = InputHelper.GetValidInt(0, Common.Exceptions.GlobalErrorMessages.errorMessageInt);

            var success = _serviceAddBook.AddBook(title, author, stock, price);

            if (success)
                Console.WriteLine("\nLibro agregado correctamente.");
            else
                Console.WriteLine("\nHubo un error al agregar el libro.");
        }
    }
}
