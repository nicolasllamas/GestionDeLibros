using Servicies;
using Helper;
using Servicies;

namespace GestionDeLibros.Menus
{
    internal static class MenuDeleteBook
    {
        public static void OpenDeleteMenu(ServiceDeleteBook _serviceDeleteBook)
        {
            Console.Clear();
            Console.WriteLine("=== Menu de Eliminación ===");
            Console.WriteLine("1. Ver lista de libros eliminados");
            Console.WriteLine("2. Restaurar un libro");
            Console.WriteLine("3. Eliminar un libro");
            Console.WriteLine("4. Eliminar un libro permanentemente");

            var input = InputHelper.GetValidInt(1, 3, Common.Exceptions.GlobalErrorMessages.errorMessageInt);
            switch (input)
            {
                case 1:
                    ShowDeletedBooks(_serviceDeleteBook);
                    break;
                case 2:
                    RestoreBook(_serviceDeleteBook);
                    break;
                case 3:
                    DeleteBook(_serviceDeleteBook);
                    break;
                case 4:
                    HardDeleteBook(_serviceDeleteBook);
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente nuevamente.");
                    break;
            }
        }

        private static void DeleteBook(ServiceDeleteBook _serviceDeleteBook)
        {
            Console.Clear();
            Console.WriteLine("=== Eliminar un libro ===");

            Console.Write("Ingrese el ID del libro a eliminar: ");
            int id = InputHelper.GetValidInt(0, Common.Exceptions.GlobalErrorMessages.errorMessageInt);

            if (_serviceDeleteBook.GetBookById(id) != null)
                Console.WriteLine($"Ha seleccionado el libro " + _serviceDeleteBook.GetBookById(id).Title);

            var success = _serviceDeleteBook.DeleteBook(id);

            if (success)
                Console.WriteLine("\nLibro eliminado correctamente.");
            else
                Console.WriteLine("\nHubo un error al eliminar el libro.");
        }

        private static void RestoreBook(ServiceDeleteBook _serviceDeleteBook)
        {
            Console.Clear();
            Console.WriteLine("=== Restaurar un libro ===");

            Console.Write("Ingrese el ID del libro a eliminar: ");
            int id = InputHelper.GetValidInt(0, Common.Exceptions.GlobalErrorMessages.errorMessageInt);

            if (_serviceDeleteBook.GetBookById(id) != null)
            {
                Console.WriteLine($"Ha seleccionado el libro " + _serviceDeleteBook.GetBookById(id).Title);

                var success = _serviceDeleteBook.RestoreBook(id);

                if (success)
                    Console.WriteLine("\nLibro restaurado correctamente.");
                else
                    Console.WriteLine("\nHubo un error al restaurar el libro.");
            }
            else
            {
                Console.WriteLine("El ID indicado no existe.");
            }
        }

        private static void ShowDeletedBooks(ServiceDeleteBook _serviceDeleteBook)
        {
            var books = _serviceDeleteBook.GetAllDeletedBooks();

            Console.Clear();
            Console.WriteLine("=== Lista de Libros Eliminados ===");

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

        private static void HardDeleteBook(ServiceDeleteBook _serviceDeleteBook)
        {
            Console.Clear();
            Console.WriteLine("=== Eliminar permanentemente un libro ===");

            Console.Write("Ingrese el ID del libro a eliminar: ");
            int id = InputHelper.GetValidInt(0, Common.Exceptions.GlobalErrorMessages.errorMessageInt);

            if (_serviceDeleteBook.GetBookById(id) != null)
            {
                Console.WriteLine($"Ha seleccionado el libro " + _serviceDeleteBook.GetBookById(id).Title);

                Console.WriteLine("¿Está seguro que desea eliminarlo permanentemente?");
                Console.WriteLine("1. Sí");
                Console.WriteLine("2. No");

                if (InputHelper.GetValidInt(1, 2, Common.Exceptions.GlobalErrorMessages.errorMessageInt) == 1)
                {
                    var success = _serviceDeleteBook.HardDeleteBook(id);
                    if (success)
                        Console.WriteLine("\nLibro eliminado permanentemente correctamente.");
                    else
                        Console.WriteLine("\nHubo un error al eliminar el libro permanentemente.");
                }
                else
                {
                    Console.WriteLine("Operación cancelada.");
                }

            }
            else
            {
                Console.WriteLine("El ID indicado no existe.");
            }
        }
    }
}
