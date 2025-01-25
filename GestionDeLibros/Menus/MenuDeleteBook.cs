using Data_Access.Models;
using Helper;

namespace GestionDeLibros.Menus
{
    internal static class MenuDeleteBook
    {
        public static void OpenDeleteMenu(InitiateService services)
        {
            Console.Clear();
            Console.WriteLine("=== Menu de Eliminación ===");
            Console.WriteLine("1. Ver lista de libros eliminados");
            Console.WriteLine("2. Restaurar un libro");
            Console.WriteLine("3. Eliminar un libro");
            Console.WriteLine("4. Eliminar un libro permanentemente");

            var input = InputHelper.GetValidInt(1, 4, Common.Exceptions.GlobalErrorMessages.errorMessageInt);
            switch (input)
            {
                case 1:
                    ShowDeletedBooks(services);
                    break;
                case 2:
                    RestoreBook(services);
                    break;
                case 3:
                    DeleteBook(services);
                    break;
                case 4:
                    HardDeleteBook(services);
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente nuevamente.");
                    break;
            }
        }

        private static void DeleteBook(InitiateService services)
        {
            Console.Clear();
            Console.WriteLine("=== Eliminar un libro ===");

            Console.Write("Ingrese el título del libro a eliminar: ");
            string title = InputHelper.GetNotNullString(Common.Exceptions.GlobalErrorMessages.errorMessageString);
            var book = Menu.GetBookByTitle(services, title);

            var success = services.serviceDeleteBook.DeleteBook(book);

            if (success.IsSuccess == true)
                Console.WriteLine("\nLibro eliminado correctamente.");
            else
                Console.WriteLine("\nHubo un error al eliminar el libro.");
        }

        private static void RestoreBook(InitiateService services)
        {
            Console.Clear();
            Console.WriteLine("=== Restaurar un libro ===");

            Console.Write("Ingrese el título del libro a restaurar: ");
            string title = InputHelper.GetNotNullString(Common.Exceptions.GlobalErrorMessages.errorMessageString);
            var book = Menu.GetBookByTitle(services, title);

            var success = services.serviceDeleteBook.RestoreBook(book);

            if (success.IsSuccess == true)
                Console.WriteLine("\nLibro restaurado correctamente.");
            else
                Console.WriteLine("\nHubo un error al restaurar el libro.");
        }

        private static void ShowDeletedBooks(InitiateService services)
        {
            var response = services.serviceDeleteBook.GetAllDeletedBooks();
            var bookList = response.Result as List<Book>;

            Console.Clear();
            Console.WriteLine("=== Lista de Libros Eliminados ===");

            if (response.IsSuccess == true)
            {
                if (bookList.Count != 0)
                {
                    foreach (var book in bookList)
                    {
                        string status = book.Stock > 0 ? "Disponible" : "Agotado";

                        Console.WriteLine($"Título: {book.Title}, Autor: {book.Author}, Precio: ${book.Price:F2}, Stock: {book.Stock} ({status})");
                    }
                }
                else { Console.WriteLine("\nNo hay registros de libros eliminados."); }
            }
            else { Console.WriteLine(response.Message); }
        }

        private static void HardDeleteBook(InitiateService services)
        {
            Console.Clear();
            Console.WriteLine("=== Eliminar permanentemente un libro ===");

            Console.Write("Ingrese el título del libro a eliminar permanentemente: ");
            string title = InputHelper.GetNotNullString(Common.Exceptions.GlobalErrorMessages.errorMessageString);
            var book = Menu.GetBookByTitle(services, title);

            var success = services.serviceDeleteBook.HardDeleteBook(book);

            if (success.IsSuccess == true)
                Console.WriteLine("\nLibro eliminado permanentemente.");
            else
                Console.WriteLine("\nHubo un error al eliminar el libro.");
        }
    }
}
