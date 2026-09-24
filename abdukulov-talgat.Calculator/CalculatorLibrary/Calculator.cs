namespace CalculatorLibrary;

using Newtonsoft.Json;

public class Calculator
{
    private const string LogFileName = "calculator-log.json";

    private readonly List<Operation> _operations = [];

    public IReadOnlyList<Operation> Operations => _operations;

    public Calculator()
    {
        if (!File.Exists(LogFileName)) return;

        try
        {
            using JsonTextReader jsonTextReader = new(File.OpenText(LogFileName));
            JsonSerializer serializer = new();
            List<Operation>? loadedOperations = serializer.Deserialize<List<Operation>>(jsonTextReader);

            if (loadedOperations != null)
            {
                _operations.AddRange(loadedOperations);
            }
        }
        catch (Exception ex)
        {
            DisplayErrorMessage(ex);
        }
    }

    public Operation Calculate(double left, double right, OperationType operationType)
    {
        double result = operationType switch
        {
            OperationType.Add => left + right,
            OperationType.Subtract => left - right,
            OperationType.Multiply => left * right,
            OperationType.Division => right != 0 ? left / right : double.NaN,
            OperationType.Pow => Math.Pow(left, right),
            OperationType.Sqrt => Math.Sqrt(left),
            OperationType.Cos => Math.Cos(left),
            OperationType.Sin => Math.Sin(left),
            _ => throw new ArgumentOutOfRangeException(nameof(operationType), operationType, null)
        };

        Operation op = new(left, right, operationType, result);
        _operations.Add(op);
        Save();
        return op;
    }

    public void ClearHistory()
    {
        _operations.Clear();
        Save();
    }

    private void Save()
    {
        try
        {
            File.WriteAllText(LogFileName, JsonConvert.SerializeObject(_operations, Formatting.Indented));
        }
        catch (Exception ex)
        {
            DisplayErrorMessage(ex);
        }
    }

    private static void DisplayErrorMessage(Exception ex)
    {
        Console.WriteLine($"An exception occurs during loading: {ex.Message}");
    }
}