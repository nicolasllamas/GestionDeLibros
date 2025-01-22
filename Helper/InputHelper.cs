namespace Helper
{
    public class InputHelper
    {
        public static int GetValidInt(int min, int max, string errorMessage) // min and max are inclusive values
        {
            int result = default;

            while (!int.TryParse(Console.ReadLine(), out result) || result < min || result > max)
            {
                Console.WriteLine(errorMessage);
            }
            return result;
        }
        public static int GetValidInt(int min, string errorMessage) // OVERLOAD, min is a inclusive value
        {
            int result = default;

            while (!int.TryParse(Console.ReadLine(), out result) || result < min || result > int.MaxValue)
            {
                Console.WriteLine(errorMessage);
            }
            return result;
        }

        public static decimal GetPositiveDecimal(string errorMessage) // min is < 0
        {
            decimal result = default;
            while (!decimal.TryParse(Console.ReadLine(), out result) || result <= 0)
            {
                Console.WriteLine(errorMessage);
            }
            return result;
        }

        public static string GetNotNullString(string errorMessage)
        {
            string result = null;

            while (string.IsNullOrWhiteSpace(result))
            {
                result = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(result))
                {
                    Console.WriteLine(errorMessage);
                }
            }
            return result;
        }
    }
}
