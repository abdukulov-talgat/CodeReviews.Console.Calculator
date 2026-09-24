using CalculatorLibrary;
using Spectre.Console;


namespace CalculatorApp
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
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
                        DoCalculations(calculator);
                        break;
                    case MainMenu.History:
                        DisplayHistory(calculator);
                        break;
                    case MainMenu.Exit:
                        endApp = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }


        private static void DoCalculations(Calculator calculator)
        {
            bool shouldCalculate = true;
            while (shouldCalculate)
            {
                OperationType operationType = AnsiConsole.Prompt(new SelectionPrompt<OperationType>()
                    .Title("Choose an operator:")
                    .AddChoices(Enum.GetValues<OperationType>()));
                double left = AnsiConsole.Ask<double>("Type a number, and then press Enter: ");
                double right = IsUnaryOperation(operationType)
                    ? double.NaN //right is not used for unary operations. For simplicity there is no inheritance to solve this.  
                    : AnsiConsole.Ask<double>("Type another number, and then press Enter: ");


                Operation op = calculator.Calculate(left, right, operationType);
                AnsiConsole.MarkupLine($"[yellow]{op}[/]");

                shouldCalculate = AnsiConsole.Confirm("Calculate again?", true);
                AnsiConsole.Clear();
            }
        }

        private static bool IsUnaryOperation(OperationType operationType)
        {
            return operationType is OperationType.Sqrt or OperationType.Cos or OperationType.Sin;
        }

        private static void DisplayHistory(Calculator calculator)
        {
            AnsiConsole.Clear();
            List<string> choices = [];

            if (calculator.Operations.Count > 0)
            {
                choices.Add("Clear History");
                AnsiConsole.MarkupLine($"Previous operations\n");
                foreach (Operation operation in calculator.Operations)
                {
                    AnsiConsole.MarkupLine($"[yellow]{operation}[/]");
                }

                AnsiConsole.MarkupLine($"\nTotal Count: {calculator.Operations.Count}\n");
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
                calculator.ClearHistory();
            }
        }
    }
}