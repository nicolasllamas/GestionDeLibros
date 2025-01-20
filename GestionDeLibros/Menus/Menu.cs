using BLL;
using Repositories;

namespace GestionDeLibros.Menus
{
    public static class Menu
    {
        private static BookService _bookService;

        static Menu()
        {
            // Inicializa el servicio de libros
            _bookService = new BookService(new BookRepository());
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
                Console.WriteLine("3. Modificar un libro existente");
                Console.WriteLine("4. Eliminar un libro");
                Console.WriteLine("5. Modificar stock de un libro");
                Console.WriteLine("6. Modificar precio de un libro");
                Console.WriteLine("7. Salir");
                Console.Write("\nSeleccione una opción: ");

                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        ShowBooks();
                        break;
                    case "2":
                        AddBook();
                        break;
                    case "3":
                        UpdateBook();
                        break;
                    case "4":
                        DeleteBook();
                        break;
                    case "5":
                        UpdateStock();
                        break;
                    case "6":
                        UpdatePrice();
                        break;
                    case "7":
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
            var books = _bookService.GetAllBooks();

            Console.Clear();
            Console.WriteLine("=== Lista de Libros ===");

            if (books.Count == 0) { Console.WriteLine("\nNo hay libros registrados."); }
            else
            {
                foreach (var book in books)
                {
                    string status = book.Stock > 0 ? "Disponible" : "Agotado";

                    Console.WriteLine($"Título: {book.Title}, Autor: {book.Author}, Precio: ${book.Price:F2}, Stock: {book.Stock} ({status})");
                }
            }
        }

        private static void AddBook()
        {
            Console.Clear();
            Console.WriteLine("=== Agregar un nuevo libro ===");

            Console.Write("Ingrese el título: ");
            string title = Console.ReadLine();

            Console.Write("Ingrese el autor: ");
            string author = Console.ReadLine();

            Console.Write("Ingrese el precio: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Ingrese el stock: ");
            int stock = int.Parse(Console.ReadLine());

            var success = _bookService.AddBook(title, author, stock, price);

            if (success)
                Console.WriteLine("\nLibro agregado correctamente.");
            else
                Console.WriteLine("\nHubo un error al agregar el libro.");
        }

        private static void UpdateBook()
        {
            Console.Clear();
            Console.WriteLine("=== Modificar un libro existente ===");

            Console.Write("Ingrese el ID del libro a modificar: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Ingrese el nuevo título: ");
            string title = Console.ReadLine();

            Console.Write("Ingrese el nuevo autor: ");
            string author = Console.ReadLine();

            Console.Write("Ingrese el nuevo precio: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Ingrese el nuevo stock: ");
            int stock = int.Parse(Console.ReadLine());

            var success = _bookService.UpdateBook(id, title, author, stock, price);

            if (success)
                Console.WriteLine("\nLibro modificado correctamente.");
            else
                Console.WriteLine("\nHubo un error al modificar el libro.");
        }

        private static void DeleteBook()
        {
            Console.Clear();
            Console.WriteLine("=== Eliminar un libro ===");

            Console.Write("Ingrese el ID del libro a eliminar: ");
            int id = int.Parse(Console.ReadLine());

            var success = _bookService.DeleteBook(id);

            if (success)
                Console.WriteLine("\nLibro eliminado correctamente.");
            else
                Console.WriteLine("\nHubo un error al eliminar el libro.");
        }

        private static void UpdateStock()
        {
            Console.Clear();
            Console.WriteLine("=== Modificar el stock de un libro ===");

            Console.Write("Ingrese el ID del libro: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Ingrese el nuevo stock: ");
            int stock = int.Parse(Console.ReadLine());

            var success = _bookService.UpdateBookStock(id, stock);

            if (success)
                Console.WriteLine("\nStock modificado correctamente.");
            else
                Console.WriteLine("\nHubo un error al modificar el stock.");
        }

        private static void UpdatePrice()
        {
            Console.Clear();
            Console.WriteLine("=== Modificar el precio de un libro ===");

            Console.Write("Ingrese el ID del libro: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Ingrese el nuevo precio: ");
            decimal price = decimal.Parse(Console.ReadLine());

            var success = _bookService.UpdateBookPrice(id, price);

            if (success)
                Console.WriteLine("\nPrecio modificado correctamente.");
            else
                Console.WriteLine("\nHubo un error al modificar el precio.");
        }
    }
}
