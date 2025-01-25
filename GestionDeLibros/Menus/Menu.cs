using Servicies;
using Helper;
using Repositories;
using Data_Access.Models;

namespace GestionDeLibros.Menus
{
    public static class Menu
    {
        public static void ShowMenu()
        {
            var services = new InitiateService();
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
                        ShowBooks(services);
                        break;
                    case 2:
                        AddBook(services);
                        break;
                    case 3:
                        MenuModifyBook.OpenModifyMenu(services);
                        break;
                    case 4:
                        MenuDeleteBook.OpenDeleteMenu(services);
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

        private static void ShowBooks(InitiateService services)
        {
            var response = services.serviceGetBook.GetAllBooks();

            Console.Clear();
            Console.WriteLine("=== Lista de Libros ===");

            if (response.IsSuccess == false) { Console.WriteLine(response.Message); }

            else
            {
                if (response.IsSuccess == true && response.Result is List<Book> books && books.Count != 0)
                {
                    foreach (var book in books)
                    {
                        string status = book.Stock > 0 ? "Disponible" : "Agotado";

                        Console.WriteLine($"Título: {book.Title}, Autor: {book.Author}, Precio: ${book.Price:F2}, Stock: {book.Stock} ({status})");
                    }
                }
                else
                {
                    Console.WriteLine("\nNo hay libros registrados.");
                }
            }
        }

        private static void AddBook(InitiateService services)
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

            var success = services.serviceAddBook.AddBook(title, author, stock, price);

            if (success.IsSuccess)
                Console.WriteLine("\nLibro agregado correctamente.");
            else
                Console.WriteLine("\nHubo un error al agregar el libro.");
        }
        internal static Book GetBookByTitle(InitiateService services, string title)
        {
            var response = services.serviceGetBook.SearchBookByTitle(title);

            while (response.IsSuccess == false)
            {
                Console.WriteLine(response.Message);
                Console.Write("Ingrese el título nuevamente: ");
                title = InputHelper.GetNotNullString(Common.Exceptions.GlobalErrorMessages.errorMessageString);
                response = services.serviceGetBook.SearchBookByTitle(title);
            }

            Console.WriteLine($"\nHa seleccionado el libro {(response.Result as Book).Title}.");

            return response.Result as Book;
        }
    }
}
