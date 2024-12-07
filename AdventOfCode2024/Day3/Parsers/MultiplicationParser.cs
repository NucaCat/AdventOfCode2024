namespace AdventOfCode2024.Day3.Parsers;

internal sealed class MultiplicationParser
{
    public static int? TryParse(string line, ref int index)
    {
        var mul = "mul".AsSpan();
        var multPart = line.SliceWithMove(ref index, mul.Length);
        if (!multPart.SequenceEqual(mul))
            return null;

        var openBracket = line.GetWithMove(ref index);
        if (openBracket is not '(')
            return null;

        var firstNumber = NumberParser.TryParse(line, ref index);
        if (firstNumber is null)
            return null;

        var colon = line.GetWithMove(ref index);
        if (colon is not ',')
            return null;

        var secondNumber = NumberParser.TryParse(line, ref index);
        if (secondNumber is null)
            return null;

        var closeBracket = line.GetWithMove(ref index);
        if (closeBracket is not ')')
            return null;

        return firstNumber.Value * secondNumber.Value;
    }
}