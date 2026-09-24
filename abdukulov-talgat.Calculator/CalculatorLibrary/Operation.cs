namespace CalculatorLibrary;

public record Operation(double Left, double Right, OperationType OperationType, double Result)
{
    
    // I have no idea what it means 10x. I don't think it means 10 * x 
    public override string ToString()
    {
        return OperationType switch
        {
            OperationType.Add => $"{Left:0.##} + {Right:0.##} = {Result:0.##}",
            OperationType.Subtract => $"{Left:0.##} - {Right:0.##} = {Result:0.##}",
            OperationType.Multiply => $"{Left:0.##} * {Right:0.##} = {Result:0.##}",
            OperationType.Division => $"{Left:0.##} / {Right:0.##} = {Result:0.##}",
            OperationType.Pow => $"{Left:0.##} ^ {Right:0.##} = {Result:0.##}",
            OperationType.Sqrt => $"sqrt({Left:0.##}) = {Result:0.##}",
            OperationType.Sin => $"sin {Left:0.##} = {Result:0.##}",
            OperationType.Cos => $"cos {Left:0.##} = {Result:0.##}",
            _ => throw new ArgumentOutOfRangeException(nameof(OperationType), OperationType, null),
        };
    }
};