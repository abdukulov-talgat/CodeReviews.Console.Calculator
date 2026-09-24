namespace CalculatorLibrary;

public record Operation(double Left, double Right, OperationType OperationType, double Result)
{
    public override string ToString()
    {
        return OperationType switch
        {
            OperationType.Add => $"{Left} + {Right} = {Result:0.####}",
            OperationType.Subtract => $"{Left} - {Right} = {Result:0.####}",
            OperationType.Multiply => $"{Left} * {Right} = {Result:0.####}",
            OperationType.Division => $"{Left} / {Right} = {Result:0.####}",
            OperationType.Pow => $"{Left} ^ {Right} = {Result:0.####}",
            OperationType.Sqrt => $"sqrt({Left}) = {Result:0.####}",
            OperationType.Sin => $"{Left} sin = {Result:0.####}",
            OperationType.Cos => $"{Left} cos = {Result:0.####}",
            _ => throw new ArgumentOutOfRangeException(nameof(OperationType), OperationType, null),
        };
    }
};