namespace CalculatorLibrary;

using Newtonsoft.Json;

public class Calculator : IDisposable
{
    private bool _disposed;
    private readonly JsonWriter _writer;

    public Calculator()
    {
        StreamWriter logFile = File.CreateText("calculator-log.json");
        logFile.AutoFlush = true;
        _writer = new JsonTextWriter(logFile);
        _writer.Formatting = Formatting.Indented;
        _writer.WriteStartObject();
        _writer.WritePropertyName("Operations");
        _writer.WriteStartArray();
    }

    public double Calculate(double num1, double num2, string op)
    {
        _writer.WriteStartObject();
        _writer.WritePropertyName("Operand1");
        _writer.WriteValue(num1);
        _writer.WritePropertyName("Operand2");
        _writer.WriteValue(num2);
        _writer.WritePropertyName("Operation");
        double result;

        switch (op)
        {
            case "a":
                result = num1 + num2;
                _writer.WriteValue("Add");
                break;
            case "s":
                result = num1 - num2;
                _writer.WriteValue("Subtract");
                break;
            case "m":
                result = num1 * num2;
                _writer.WriteValue("Multiply");
                break;
            case "d":
                result = num2 != 0 ? num1 / num2 : double.NaN;
                _writer.WriteValue("Divide");
                break;
            default:
                result = double.NaN;
                break;
        }

        _writer.WritePropertyName("Result");
        _writer.WriteValue(result);
        _writer.WriteEndObject();

        return result;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing)
        {
            ((IDisposable)_writer)?.Dispose();
        }

        _disposed = true;
    }
}