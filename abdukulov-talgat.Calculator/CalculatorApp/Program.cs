using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using CalculatorLibrary;

namespace CalculatorApp
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            using Calculator calculator = new Calculator();
            bool endApp = false;

            Console.WriteLine("Console Calculator in C#\r");
            Console.WriteLine("------------------------\n");


            while (!endApp)
            {
                double left = RequestNumber("Type a number, and then press Enter: ");
                double right = RequestNumber("Type another number, and then press Enter: ");

                string? op = RequestOperatorInput();

                if (!IsOperatorValid(op))
                {
                    Console.WriteLine("Error: Unrecognized input.");
                }
                else
                {
                    double result = calculator.Calculate(left, right, op);
                    if (double.IsNaN(result))
                        Console.WriteLine("This operation will result in a mathematical error.\n");
                    else
                        Console.WriteLine("Your result: {0:0.##}\n", result);
                }

                Console.WriteLine("------------------------\n");

                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;
                Console.Clear();
            }
        }

        private static double RequestNumber(string prompt,
            string errorMessage = "This is not valid input. Please enter an integer value: ")
        {
            string? input = "";
            Console.Write(prompt);
            input = Console.ReadLine();

            double num = 0;
            while (!double.TryParse(input, out num))
            {
                Console.Write(errorMessage);
                input = Console.ReadLine();
            }

            return num;
        }

        private static string? RequestOperatorInput()
        {
            Console.WriteLine("Choose an operator from the following list:");
            Console.WriteLine("\ta - Add");
            Console.WriteLine("\ts - Subtract");
            Console.WriteLine("\tm - Multiply");
            Console.WriteLine("\td - Divide");
            Console.Write("Your option? ");

            string? op = Console.ReadLine();
            return op;
        }


        private static bool IsOperatorValid([NotNullWhen(true)] string? op)
        {
            return op != null && Regex.IsMatch(op, "[a|s|m|d]");
        }
    }
}