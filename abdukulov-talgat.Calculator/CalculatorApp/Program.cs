using System.Text.RegularExpressions;
using CalculatorLibrary;
using Spectre.Console;


namespace CalculatorApp
{
    public static class Program
    {
        private static readonly Calculator Calculator = new();

        private static void Main(string[] args)
        {
            bool endApp = false;

            while (!endApp)
            {
                AnsiConsole.Clear();
                MainMenu mainMenuChoice = AnsiConsole.Prompt(new SelectionPrompt<MainMenu>()
                    .Title("Main Menu")
                    .AddChoices(Enum.GetValues<MainMenu>()));

                switch (mainMenuChoice)
                {
                    case MainMenu.Calculate:
                        DoCalculations();
                        break;
                    case MainMenu.History:
                        DisplayHistory();
                        break;
                    case MainMenu.Exit:
                        endApp = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }


        private static void DoCalculations()
        {
            bool shouldCalculate = true;
            while (shouldCalculate)
            {
                OperationType operationType = AnsiConsole.Prompt(new SelectionPrompt<OperationType>()
                    .Title("Choose an operator:")
                    .AddChoices(Enum.GetValues<OperationType>()));
                double left = RequestNumberInput("Type a number or 'p' to chose from previous results: ");
                double right = IsUnaryOperation(operationType)
                    ? double.NaN //right is not used for unary operations. For simplicity there is no inheritance to solve this.  
                    : RequestNumberInput("And again type a number or 'p' to chose from previous results: ");


                Operation op = Calculator.Calculate(left, right, operationType);
                AnsiConsole.MarkupLine($"[yellow]{op}[/]");

                shouldCalculate = AnsiConsole.Confirm("Calculate again?", true);
                AnsiConsole.Clear();
            }
        }

        private static double RequestNumberInput(string message)
        {
            TextPrompt<string> textPrompt = new(message);

            string input = AnsiConsole.Prompt(textPrompt.Validate(
                str => Regex.IsMatch(str, "^p$|\\d+"),
                "Wrong input. Only numbers and 'p' is allowed"));

            return input == "p"
                ? AnsiConsole.Prompt(
                    new SelectionPrompt<Operation>()
                        .AddChoices(Calculator.Operations)).Result
                : double.Parse(input);
        }

        //For simplicity, I didn't use polymorphism
        private static bool IsUnaryOperation(OperationType operationType)
        {
            return operationType is OperationType.Sqrt or OperationType.Cos or OperationType.Sin;
        }


        //History menu view
        private static void DisplayHistory()
        {
            AnsiConsole.Clear();
            List<string> choices = [];

            if (Calculator.Operations.Count > 0)
            {
                choices.Add("Clear History");
                AnsiConsole.MarkupLine($"Previous operations\n");
                foreach (Operation operation in Calculator.Operations)
                {
                    AnsiConsole.MarkupLine($"[yellow]{operation}[/]");
                }

                AnsiConsole.MarkupLine($"\nTotal Count: {Calculator.Operations.Count}\n");
            }
            else
            {
                AnsiConsole.MarkupLine($"There is no previous operations\n");
            }

            choices.Add("Main Menu");
            string prompt = AnsiConsole.Prompt(new SelectionPrompt<string>()
                .AddChoices(choices));
            if (prompt == "Clear History")
            {
                Calculator.ClearHistory();
            }
        }
    }
}