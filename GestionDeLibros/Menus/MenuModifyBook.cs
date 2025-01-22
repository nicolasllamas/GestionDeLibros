using Servicies;
using Helper;
using Servicies;

namespace GestionDeLibros.Menus
{
    public static class MenuModifyBook
    {
        public static void OpenModifyMenu(ServiceUpdateBook _serviceUpdateBook)
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
                    UpdateStock(_serviceUpdateBook);
                    break;
                case 2:
                    UpdatePrice(_serviceUpdateBook);
                    break;
                case 3:
                    UpdateBook(_serviceUpdateBook);
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente nuevamente.");
                    break;
            }
        }

        private static void UpdateBook(ServiceUpdateBook _serviceUpdateBook)
        {
            Console.Clear();
            Console.WriteLine("=== Modificar un libro existente ===");

            Console.Write("Ingrese el ID del libro a modificar: ");
            int id = InputHelper.GetValidInt(0, Common.Exceptions.GlobalErrorMessages.errorMessageInt);
            if (_serviceUpdateBook.GetBookById(id) != null)
                Console.WriteLine($"Ha seleccionado el libro " + _serviceUpdateBook.GetBookById(id).Title);

            Console.Write("Ingrese el nuevo título: ");
            string title = InputHelper.GetNotNullString(Common.Exceptions.GlobalErrorMessages.errorMessageString);

            Console.Write("Ingrese el nuevo autor: ");
            string author = InputHelper.GetNotNullString(Common.Exceptions.GlobalErrorMessages.errorMessageString);

            Console.Write("Ingrese el nuevo precio: ");
            decimal price = InputHelper.GetPositiveDecimal(Common.Exceptions.GlobalErrorMessages.errorMessageDecimal);

            Console.Write("Ingrese el nuevo stock: ");
            int stock = InputHelper.GetValidInt(0, Common.Exceptions.GlobalErrorMessages.errorMessageInt);

            var success = _serviceUpdateBook.UpdateBook(id, title, author, stock, price);

            if (success)
                Console.WriteLine("\nLibro modificado correctamente.");
            else
                Console.WriteLine("\nHubo un error al modificar el libro.");
        }

        private static void UpdateStock(ServiceUpdateBook _serviceUpdateBook)
        {
            Console.Clear();
            Console.WriteLine("=== Modificar el stock de un libro ===");

            Console.Write("Ingrese el ID del libro: ");
            int id = InputHelper.GetValidInt(0, Common.Exceptions.GlobalErrorMessages.errorMessageInt);
            if (_serviceUpdateBook.GetBookById(id) != null)
                Console.WriteLine($"Ha seleccionado el libro " + _serviceUpdateBook.GetBookById(id).Title);

            Console.Write("Ingrese el nuevo stock: ");
            int stock = InputHelper.GetValidInt(0, Common.Exceptions.GlobalErrorMessages.errorMessageInt);

            var success = _serviceUpdateBook.UpdateBookStock(id, stock);

            if (success)
                Console.WriteLine("\nStock modificado correctamente.");
            else
                Console.WriteLine("\nHubo un error al modificar el stock.");
        }

        private static void UpdatePrice(ServiceUpdateBook _serviceUpdateBook)
        {
            Console.Clear();
            Console.WriteLine("=== Modificar el precio de un libro ===");

            Console.Write("Ingrese el ID del libro: ");
            int id = InputHelper.GetValidInt(0, Common.Exceptions.GlobalErrorMessages.errorMessageInt);
            if (_serviceUpdateBook.GetBookById(id) != null)
                Console.WriteLine($"Ha seleccionado el libro " + _serviceUpdateBook.GetBookById(id).Title);

            Console.Write("Ingrese el nuevo precio: ");
            decimal price = InputHelper.GetPositiveDecimal(Common.Exceptions.GlobalErrorMessages.errorMessageDecimal);

            var success = _serviceUpdateBook.UpdateBookPrice(id, price);

            if (success)
                Console.WriteLine("\nPrecio modificado correctamente.");
            else
                Console.WriteLine("\nHubo un error al modificar el precio.");
        }
    }
}
