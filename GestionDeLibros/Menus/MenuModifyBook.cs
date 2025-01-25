using Servicies;
using Helper;
using Servicies;

namespace GestionDeLibros.Menus
{
    public static class MenuModifyBook
    {
        internal static void OpenModifyMenu(InitiateService services)
        {
            Console.Clear();
            Console.WriteLine("=== Menú de Modificación ===");
            Console.WriteLine("1. Modificar el stock");
            Console.WriteLine("2. Modificar el precio");
            Console.WriteLine("3. Modificar todo el registro");

            var input = InputHelper.GetValidInt(1, 3, Common.Exceptions.GlobalErrorMessages.errorMessageInt);
            switch (input)
            {
                case 1:
                    UpdateStock(services);
                    break;
                case 2:
                    UpdatePrice(services);
                    break;
                case 3:
                    UpdateBook(services);
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente nuevamente.");
                    break;
            }
        }

        private static void UpdateBook(InitiateService services)
        {
            Console.Clear();
            Console.WriteLine("=== Modificar un libro existente ===");

            Console.Write("Ingrese el título del libro a modificar: ");
            string oldTitle = InputHelper.GetNotNullString(Common.Exceptions.GlobalErrorMessages.errorMessageString);
            var book = Menu.GetBookByTitle(services, oldTitle);

            Console.Write("Ingrese el nuevo título: ");
            string newTitle = InputHelper.GetNotNullString(Common.Exceptions.GlobalErrorMessages.errorMessageString);

            Console.Write("Ingrese el nuevo autor: ");
            string newAuthor = InputHelper.GetNotNullString(Common.Exceptions.GlobalErrorMessages.errorMessageString);

            Console.Write("Ingrese el nuevo precio: ");
            decimal newPrice = InputHelper.GetPositiveDecimal(Common.Exceptions.GlobalErrorMessages.errorMessageDecimal);

            Console.Write("Ingrese el nuevo stock: ");
            int newStock = InputHelper.GetValidInt(0, Common.Exceptions.GlobalErrorMessages.errorMessageInt);

            var response = services.serviceUpdateBook.UpdateBook(book, newTitle, newAuthor, newStock, newPrice);

            if (response.IsSuccess)
                Console.WriteLine("\nLibro modificado correctamente.");
            else
                Console.WriteLine("\nHubo un error al modificar el libro.");
        }

        private static void UpdateStock(InitiateService services)
        {
            Console.Clear();
            Console.WriteLine("=== Modificar el stock de un libro ===");

            Console.Write("Ingrese el título del libro a modificar: ");
            string oldTitle = InputHelper.GetNotNullString(Common.Exceptions.GlobalErrorMessages.errorMessageString);
            var book = Menu.GetBookByTitle(services, oldTitle);

            Console.Write("Ingrese el nuevo stock: ");
            int stock = InputHelper.GetValidInt(0, Common.Exceptions.GlobalErrorMessages.errorMessageInt);

            var response = services.serviceUpdateBook.UpdateBookStock(book, stock);

            if (response.IsSuccess)
                Console.WriteLine("\nStock modificado correctamente.");
            else
                Console.WriteLine("\nHubo un error al modificar el stock.");
        }

        private static void UpdatePrice(InitiateService services)
        {
            Console.Clear();
            Console.WriteLine("=== Modificar el precio de un libro ===");

            Console.Write("Ingrese el título del libro a modificar: ");
            string oldTitle = InputHelper.GetNotNullString(Common.Exceptions.GlobalErrorMessages.errorMessageString);
            var book = Menu.GetBookByTitle(services, oldTitle);

            Console.Write("Ingrese el nuevo precio: ");
            decimal price = InputHelper.GetPositiveDecimal(Common.Exceptions.GlobalErrorMessages.errorMessageDecimal);

            var response = services.serviceUpdateBook.UpdateBookPrice(book, price);

            if (response.IsSuccess)
                Console.WriteLine("\nPrecio modificado correctamente.");
            else
                Console.WriteLine("\nHubo un error al modificar el precio.");
        }
    }
}
